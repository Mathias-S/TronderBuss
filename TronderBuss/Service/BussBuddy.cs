using System;
using System.Collections.Generic;
using RestSharp;
using TronderBuss.ViewModels;

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

        public void GetBussStops(Action<List<StopViewModel>> callback)
        {
            var request = new RestRequest("busstops");
            request.RequestFormat = DataFormat.Json;

            client.ExecuteAsync<List<StopViewModel>>(request, result =>
            {
                callback(result.Data);
            });
        }
    }
}
