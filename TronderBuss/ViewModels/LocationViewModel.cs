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

namespace TronderBuss.ViewModels
{
    public class LocationViewModel : ViewModelBase
    {
        double latitude;
        double longitude;
        bool availible;

        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (latitude != value)
                {
                    latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (longitude != value)
                {
                    longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }

        public bool Availible
        {
            get { return availible; }
            set
            {
                if (availible != value)
                {
                    availible = value;
                    NotifyPropertyChanged("Availible");
                }
            }
        }
    }
}
