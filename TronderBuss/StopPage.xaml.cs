using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Linq;
using TronderBuss.Service;
using TronderBuss.ViewModels;
using System;

namespace TronderBuss
{
    public partial class StopPage : PhoneApplicationPage
    {
        StopGroupViewModel model;
        // Constructor
        public StopPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(StopPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void StopPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.Loaded)
                App.ViewModel.Load();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string name = null;
            if(!NavigationContext.QueryString.TryGetValue("stop", out name))
                NavigationService.GoBack();

            var stop = model = App.ViewModel.Stops.Where(s => s.Name == name).SingleOrDefault();
            if (stop == null)
                NavigationService.GoBack();

            DataContext = stop;
            if (!stop.Loaded)
                stop.Load();
        }

        private void ShowMap_Click(object sender, System.EventArgs e)
        {
            
        }

        private void FavButton_Click(object sender, System.EventArgs e)
        {
            if(BussBuddy.Instance.IsFav(model.Name))
                BussBuddy.Instance.RemoveAsFav(model.Name);
            else
                BussBuddy.Instance.AddAsFav(model.Name, -1);
        }
    }
}