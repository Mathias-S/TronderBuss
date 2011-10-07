using System.ComponentModel;
using System;

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

        public string TimeView
        {
            get
            {
                if (isRealtimeData)
                {
                    DateTime time = ParseTime(registeredDepartureTime);
                    return String.Format("{0:00} min", Math.Ceiling(time.Subtract(DateTime.Now).TotalMinutes));
                }
                else
                {
                    return ParseTime(scheduledDepartureTime).ToString("hh:mm");
                }
            }
        }

        private DateTime ParseTime(string timeString)
        {
            var dt =  DateTime.Parse(timeString + "Z");
            dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);
            return dt.ToUniversalTime();
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
