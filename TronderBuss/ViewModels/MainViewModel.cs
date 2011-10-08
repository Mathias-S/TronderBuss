using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using TronderBuss.Service;
using TronderBuss.ViewModels;
using System.Device.Location;


namespace TronderBuss
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Stops = new ObservableCollection<StopGroupViewModel>();
            this.Favs = new ObservableCollection<StopGroupViewModel>();
            this.History = new ObservableCollection<StopGroupViewModel>();
            this.Location = new LocationViewModel();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<StopGroupViewModel> Stops { get; private set; }
        public ObservableCollection<StopGroupViewModel> Favs { get; private set; }
        public ObservableCollection<StopGroupViewModel> History { get; private set; }
        public LocationViewModel Location { get; private set; }

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
                    Stops.Clear();
                    foreach (var stop in stops)
                        Stops.Add(stop);
                });
            });
            bb.GetFavs(stops =>
            {
                Favs.Clear();
                foreach (var stop in stops)
                    Favs.Add(stop);
            });
            bb.UpdateHistory();
        }

        private GeoCoordinateWatcher watcher;
        internal void Start()
        {
            if (watcher == null)
                watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);

            

            watcher.MovementThreshold = 20;
            watcher.PositionChanged += (sender, arg) =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    Location.Availible = true;
                    Location.Latitude = arg.Position.Location.Latitude;
                    Location.Longitude = arg.Position.Location.Longitude;
                });
            };

            try { watcher.Start(); }
            catch { }
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