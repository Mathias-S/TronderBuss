using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Controls;

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
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ((ListBox)sender).SelectedIndex = -1;
        }
    }
}