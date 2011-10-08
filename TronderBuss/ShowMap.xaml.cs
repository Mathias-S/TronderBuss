using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using TronderBuss.ViewModels;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;

namespace TronderBuss
{
    public partial class ShowMap : PhoneApplicationPage
    {
        private StopGroupViewModel model;
        public ShowMap()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string name = null;
            if (!NavigationContext.QueryString.TryGetValue("stop", out name))
                NavigationService.GoBack();

            var stop = model = App.ViewModel.Stops.Where(s => s.Name == name).SingleOrDefault();
            if (stop == null)
                NavigationService.GoBack();
            else
            {
                DataContext = stop;
                if (!stop.Loaded)
                    stop.Load();
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            double latmid = model.Locations.Select(l => l.Latitude).Sum() / model.Locations.Count();
            double longmid = model.Locations.Select(l => l.Longitude).Sum() / model.Locations.Count();
            Map.Center = new System.Device.Location.GeoCoordinate(latmid, longmid);
            Map.ZoomLevel = 17;
        }
    }
}