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

        public BussBuddy()
        {
            client = new RestClient("http://api.busbuddy.norrs.no:8080/api/1.2/");
            client.AddDefaultParameter("apiKey", "HwSJ6xL9wCUnpegC");
        }

        public void GetBussStops(Action<IEnumerable<StopGroupViewModel>> callback)
        {
            var request = new RestRequest("busstops");
            request.RequestFormat = DataFormat.Json;

            client.ExecuteAsync<StopResponse>(request, result =>
            {
                callback(result.Data.BusStops.OrderBy(stop => stop.Name).GroupBy(stop => stop.Name).Select(group => new StopGroupViewModel
                {
                    Ids = group.Select(stop => stop.BusStopId).ToList(),
                    Name = group.First().Name
                }));
            });
        }

        public void GetDepartures(int busStopId, Action<DepartureViewModel> callback)
        {
            var request = new RestRequest("departures/{id}");
            request.AddUrlSegment("id", busStopId.ToString());
            request.RequestFormat = DataFormat.Json;

            client.ExecuteAsync<DeparturesResponse>(request, result =>
            {

            });
        }
    }
}
