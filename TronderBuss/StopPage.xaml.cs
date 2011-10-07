using System.Windows;
using Microsoft.Phone.Controls;

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
    }
}