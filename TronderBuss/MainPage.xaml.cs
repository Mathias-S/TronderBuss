using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Controls;
using System;
using TronderBuss.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Device.Location;
using TronderBuss.Service;
namespace TronderBuss
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
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
                    BussBuddy.Instance.BumpHistory(stop.Name);
                    NavigationService.Navigate(new Uri("/StopPage.xaml" + qs, UriKind.Relative));
                }
            }
        }

        private string ToQueryString(Dictionary<string,string> nvc)
        {
            return "?" + string.Join("&", nvc.Select(obj => obj.Key + "=" + System.Net.HttpUtility.UrlEncode(obj.Value)));
        }

        private void stopSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //TextBox
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            HistoryLabel.Visibility = System.Windows.Visibility.Collapsed;
            HistoryListBox.ItemsSource = null;
        }

        private void SearchTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string text = SearchTextBox.Text;
            StartFilter(text);
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                HistoryListBox.ItemsSource = App.ViewModel.History;
                HistoryLabel.Visibility = System.Windows.Visibility.Visible;
            }
        }

        Action stop = null;
        private void StartFilter(string filterText)
        {
            if (this.stop != null)
                this.stop();
            this.stop = null;
            bool stop = false;
            this.stop = () => stop = true;
            new Thread((ThreadStart)delegate
            {
                List<StopGroupViewModel> fin = new List<StopGroupViewModel>();
                foreach (var item in App.ViewModel.Stops)
                {
                    if (stop)
                        return;

                    if (filterText.ToLower().Split(' ').All(s => item.Name.ToLower().Contains(s)))
                        fin.Add(item);
                }
                Dispatcher.BeginInvoke(() => HistoryListBox.ItemsSource = fin);
            }).Start();
        }

        private void ListAllStops_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AllStops.xaml", UriKind.Relative));
        }
    }
}