using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.Generic;
using TronderBuss.Service;
using System.Collections.ObjectModel;
using System.Linq;

namespace TronderBuss.ViewModels
{
    public class StopGroupViewModel : ViewModelBase
    {
        private string name;
        private List<int> ids;
        private List<LocationViewModel> locations;
        private bool loaded;
        private ObservableCollection<DepartureViewModel> towardsCity = new ObservableCollection<DepartureViewModel>();
        private ObservableCollection<DepartureViewModel> fromCity = new ObservableCollection<DepartureViewModel>();


        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public bool IsFav
        {
            get { return BussBuddy.Instance.IsFav(Name); }
            set
            {
                if (value)
                    BussBuddy.Instance.AddAsFav(Name, -1);
                else
                    BussBuddy.Instance.RemoveAsFav(Name);
                NotifyPropertyChanged("IsFav");
            }
        }

        public List<int> Ids
        {
            get { return ids; }
            set
            {
                if (ids != value)
                {
                    ids = value;
                    NotifyPropertyChanged("Ids");
                }
            }
        }

        public List<LocationViewModel> Locations
        {
            get { return locations; }
            set
            {
                if (locations != value)
                {
                    locations = value;
                    NotifyPropertyChanged("Locations");
                }
            }
        }

        public ObservableCollection<DepartureViewModel> TowardsCity
        {
            get { return towardsCity; }
        }

        public ObservableCollection<DepartureViewModel> FromCity
        {
            get { return fromCity; }
        }

        public double LatMid
        {
            get { return this.Locations[0].Latitude; }
        }

        public double LonMid
        {
            get { return this.Locations[0].Longitude; }
        }
        public bool Loaded
        {
            get { return loaded; }
            set
            {
                if (loaded != value)
                {
                    loaded = value;
                    NotifyPropertyChanged("Loaded");
                }
            }
        }



        public void Load()
        {
            if (loaded)
                return;
            loaded = true;

            var bb = BussBuddy.Instance;
            foreach (var id in ids)
            {
                bb.GetDepartures(id, departures =>
                {
                    if (departures == null)
                    {
                        Console.WriteLine("null");
                        return;
                    }
                    if (departures.IsGoingTowardsCentrum)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            var old = towardsCity.ToList();
                            towardsCity.Clear();
                            foreach (var departure in departures.Departures.Concat(old).OrderBy(d => d.Time))
                            {
                                towardsCity.Add(departure);
                            }
                        });
                    }
                    else
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            var old = towardsCity.ToList();
                            fromCity.Clear();
                            foreach (var departure in departures.Departures.Concat(old).OrderBy(d => d.Time))
                            {
                                fromCity.Add(departure);
                            }
                        });
                    }
                });
            }
        }
    }
}
