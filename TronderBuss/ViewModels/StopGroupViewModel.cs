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
    public class StopGroupViewModel : INotifyPropertyChanged
    {
        private string name;
        private List<int> ids;
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
                    OnPropertyChange("Name");
                }
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
                    OnPropertyChange("Ids");
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

        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Loaded
        {
            get { return loaded; }
            set
            {
                if (loaded != value)
                {
                    loaded = value;
                    OnPropertyChange("Loaded");
                }
            }
        }



        public void Load()
        {
            if (loaded)
                return;
            loaded = true;

            var bb = new BussBuddy();
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
                            foreach (var departure in departures.Departures.OrderBy(d => d.TimeView))
                            {
                                towardsCity.Add(departure);
                            }
                        });
                    }
                    else
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            foreach (var departure in departures.Departures.OrderBy(d => d.TimeView))
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
