using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using TronderBuss.Service;
using TronderBuss.ViewModels;


namespace TronderBuss
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Stops = new ObservableCollection<StopGroupViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<StopGroupViewModel> Stops { get; private set; }

        private bool loading = false;
        private bool loaded = false;

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

        public bool Loaded
        {
            get { return loaded; }
            set
            {
                if (value != loaded)
                {
                    loaded = value;
                    NotifyPropertyChanged("Loaded");
                }
            }
        }

        internal void Load()
        {
            if (loaded)
                return;

            loaded = true;
            BussBuddy bb = BussBuddy.Instance;
            bb.GetBussStops(stops =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    foreach (var stop in stops)
                        Stops.Add(stop);
                });
            });
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