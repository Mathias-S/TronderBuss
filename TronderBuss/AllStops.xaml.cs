using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Controls;
using System;
using TronderBuss.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Device.Location;
namespace TronderBuss
{
    public partial class AllStops : PhoneApplicationPage
    {
        // Constructor
        public AllStops()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(AllStops_Loaded);
        }

        // Load data for the ViewModel Items
        private void AllStops_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.Loaded)
                App.ViewModel.Load();
            App.ViewModel.Start();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                var stop = e.AddedItems[0] as StopGroupViewModel;
                if (stop != null)
                {
                    var qs = ToQueryString(new Dictionary<string, string>
                    {
                        {"stop", stop.Name}
                    });
                    NavigationService.Navigate(new Uri("/StopPage.xaml" + qs, UriKind.Relative));
                }
            }
        }

        private string ToQueryString(Dictionary<string,string> nvc)
        {
            return "?" + string.Join("&", nvc.Select(obj => obj.Key + "=" + System.Net.HttpUtility.UrlEncode(obj.Value)));
        }

    }
}