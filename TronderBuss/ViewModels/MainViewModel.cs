using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TronderBuss.ViewModels;


namespace TronderBuss
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Stops = new ObservableCollection<StopViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<StopViewModel> Stops { get; private set; }

        private bool loading = true;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public bool Loading
        {
            get
            {
                return loading;
            }
            set
            {
                if (value != loading)
                {
                    loading = value;
                    NotifyPropertyChanged("Loading");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}