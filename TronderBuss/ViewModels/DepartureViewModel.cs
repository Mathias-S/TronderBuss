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

namespace TronderBuss.ViewModels
{
    public class DepartureViewModel : INotifyPropertyChanged
    {
        private string line;
        private string destination;
        private string registeredDepartureTime;
        private string scheduledDepartureTime;
        private bool isRealtimeData;

        public string Line
        {
            get { return line; }
            set
            {
                if (line != value)
                {
                    line = value;
                    OnPropertyChange("Line");
                }
            }
        }
        public string Destination
        {
            get { return destination; }
            set
            {
                if (destination != value)
                {
                    destination = value;
                    OnPropertyChange("Destination");
                }
            }
        }
        public string RegisteredDepartureTime
        {
            get { return registeredDepartureTime; }
            set
            {
                if (registeredDepartureTime != value)
                {
                    registeredDepartureTime = value;
                    OnPropertyChange("RegisteredDepartureTime");
                }
            }
        }
        public string ScheduledDepartureTime
        {
            get { return scheduledDepartureTime; }
            set
            {
                if (scheduledDepartureTime != value)
                {
                    scheduledDepartureTime = value;
                    OnPropertyChange("ScheduledDepartureTime");
                }
            }
        }
        public bool IsRealtimeData
        {
            get { return isRealtimeData; }
            set
            {
                if (isRealtimeData != value)
                {
                    isRealtimeData = value;
                    OnPropertyChange("IsRealtimeData");
                }
            }
        }

        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
