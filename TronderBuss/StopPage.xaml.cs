using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Linq;

namespace TronderBuss
{
    public partial class StopPage : PhoneApplicationPage
    {
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

            string id = null;
            if(!NavigationContext.QueryString.TryGetValue("stop", out id))
                NavigationService.GoBack();

            int iId = 0;
            if(!int.TryParse(id, out iId))
                NavigationService.GoBack();

            var stop = App.ViewModel.Stops.Where(s => s.BusStopId == iId).SingleOrDefault();
            if (stop == null)
                NavigationService.GoBack();
        }
    }
}