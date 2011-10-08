using System;
using System.Collections.Generic;
using RestSharp;
using TronderBuss.ViewModels;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Windows;

namespace TronderBuss.Service
{
    public class BussBuddy
    {
        private RestClient client;
        public BussDbContext context;
        private static BussBuddy singelton = null;
        private static readonly object sLock = new object();

        public static BussBuddy Instance
        {
            get
            {
                if (singelton == null)
                    lock (sLock)
                        if (singelton == null)
                            singelton = new BussBuddy();
                return singelton;
            }
        }

        private BussBuddy()
        {
            context = new BussDbContext("Data Source=isostore:/Data.sdf");
            if (!context.DatabaseExists())
                context.CreateDatabase();
            client = new RestClient("http://api.busbuddy.norrs.no:8080/api/1.2/");
            client.AddDefaultParameter("apiKey", "HwSJ6xL9wCUnpegC");
        }

        public void GetBussStops(Action<IEnumerable<StopGroupViewModel>> callback)
        {
            var dbStops = context.Stops.OrderBy(stop => stop.Name).ToList();
            int count = dbStops.Count;
            callback(dbStops.GroupBy(stop => stop.Name).Select(group => new StopGroupViewModel
            {
                Ids = group.Select(stop => stop.BusStopId).ToList(),
                Locations = group.Select(stop => new LocationViewModel
                {
                    Availible = true,
                    Latitude = stop.Latitude,
                    Longitude = stop.Longitude
                }).ToList(),
                Name = group.First().Name
            }));

            var request = new RestRequest("busstops");
            request.RequestFormat = DataFormat.Json;

            client.ExecuteAsync<StopResponse>(request, result =>
            {
                context.Stops.DeleteAllOnSubmit(context.Stops);
                context.Stops.InsertAllOnSubmit(result.Data.BusStops);
                context.SubmitChanges();
                if(count == 0)
                    callback(result.Data.BusStops.OrderBy(stop => stop.Name).GroupBy(stop => stop.Name).Select(group => new StopGroupViewModel
                    {
                        Ids = group.Select(stop => stop.BusStopId).ToList(),
                        Locations = group.Select(stop => new LocationViewModel
                        {
                            Availible = true,
                            Latitude = stop.Latitude,
                            Longitude = stop.Longitude
                        }).ToList(),
                        Name = group.First().Name
                    }));
            });
        }

        public void GetFavs(Action<IEnumerable<StopGroupViewModel>> callback)
        {
            var favs = context.Favs.OrderByDescending(f => f.Pos).ToList().Select(f => context.Stops.Where(s => s.Name == f.Name));
            var stops = from f in favs
                        from s in f
                        select s;
            callback(stops.GroupBy(stop => stop.Name).Select(group => new StopGroupViewModel
            {
                Ids = group.Select(stop => stop.BusStopId).ToList(),
                Locations = group.Select(stop => new LocationViewModel
                {
                    Availible = true,
                    Latitude = stop.Latitude,
                    Longitude = stop.Longitude
                }).ToList(),
                Name = group.First().Name
            }));
        }

        public void AddAsFav(string name, int pos)
        {
            if (pos == -1)
            {
                pos = context.Favs.Count();
            }
            context.Favs.InsertOnSubmit(new Fav
            {
                Name = name,
                Pos = pos
            });
            context.SubmitChanges();
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                App.ViewModel.Favs.Add(App.ViewModel.Stops.Where(s => s.Name == name).First());
            });
        }

        public void RemoveAsFav(string name)
        {
            context.Favs.DeleteAllOnSubmit(context.Favs.Where(f => f.Name == name));
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                App.ViewModel.Favs.Remove(App.ViewModel.Favs.Where(f => f.Name == name).First());
            });
        }

        public bool IsFav(string name)
        {
            return context.Favs.Where(f => f.Name == name).ToList().Any();
        }

        public void BumpHistory(string name)
        {
            context.History.DeleteAllOnSubmit(context.History.Where(h => h.Name == name));
            context.History.InsertOnSubmit(new History
            {
                Name = name,
                LastAccess = DateTime.Now
            });
            context.History.DeleteAllOnSubmit(context.History.OrderByDescending(h => h.LastAccess).Skip(50));
            context.SubmitChanges();
            UpdateHistory();
        }

        public void UpdateHistory()
        {
            var lastHistory = context.History.OrderByDescending(h => h.LastAccess).Take(20).ToList();
            var hist = from h in lastHistory
                       let g = context.Stops.Where(c => c.Name == h.Name).ToList()
                       select new StopGroupViewModel
                       {
                           Ids = g.Select(s => s.BusStopId).ToList(),
                           Name = h.Name,
                           Locations = g.Select(s => new LocationViewModel
                           {
                               Longitude = s.Longitude,
                               Latitude = s.Latitude,
                               Availible = true
                           }).ToList()
                       };
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                App.ViewModel.History.Clear();
                foreach (var h in hist)
                    App.ViewModel.History.Add(h);
            });

        }

        public void GetDepartures(int busStopId, Action<DeparturesResponse> callback)
        {
            var request = new RestRequest("departures/{id}");
            request.AddUrlSegment("id", busStopId.ToString());
            request.RequestFormat = DataFormat.Json;

            client.ExecuteAsync<DeparturesResponse>(request, result =>
            {
                callback(result.Data);
            });
        }

        [Table]
        public class Fav
        {
            [Column(CanBeNull = false, IsDbGenerated = false, IsPrimaryKey = true)]
            public string Name { get; set; }

            [Column]
            public int Pos { get; set; }
        }

        [Table]
        public class History
        {
            [Column(CanBeNull = false, IsDbGenerated = false, IsPrimaryKey = true)]
            public string Name { get; set; }

            [Column]
            public DateTime LastAccess { get; set; }
        }
    }
}
