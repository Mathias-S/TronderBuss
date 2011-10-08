using System;
using System.Collections.Generic;
using RestSharp;
using TronderBuss.ViewModels;
using System.Linq;

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
                        Name = group.First().Name
                    }));
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
    }
}
