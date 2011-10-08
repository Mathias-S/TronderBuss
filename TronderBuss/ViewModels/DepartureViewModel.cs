using System.ComponentModel;
using System;

namespace TronderBuss.ViewModels
{
    public class DepartureViewModel : ViewModelBase
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
                    NotifyPropertyChanged("Line");
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
                    NotifyPropertyChanged("Destination");
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
                    NotifyPropertyChanged("RegisteredDepartureTime");
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
                    NotifyPropertyChanged("ScheduledDepartureTime");
                }
            }
        }

        public string TimeView
        {
            get
            {
                if (isRealtimeData)
                {
                    DateTime time = Time;
                    var diff = time.Subtract(DateTime.Now);
                    if(diff >= TimeSpan.FromMinutes(10))
                        return Time.ToString("HH:mm*");
                    return String.Format("{0:0} min", Math.Ceiling(diff.TotalMinutes));
                }
                else
                {
                    return Time.ToString("HH:mm");
                }
            }
        }

        public DateTime Time
        {
            get
            {
                if (isRealtimeData)
                    return ParseTime(registeredDepartureTime);
                else
                    return ParseTime(scheduledDepartureTime);
            }
        }

        private DateTime ParseTime(string timeString)
        {
            var dt =  DateTime.Parse(timeString + "Z");
            dt = DateTime.SpecifyKind(dt, DateTimeKind.Local);
            dt = dt.ToUniversalTime();
            dt = dt.AddDays(Math.Ceiling(DateTime.Now.Subtract(dt).TotalDays));
            return dt;
        }

        public bool IsRealtimeData
        {
            get { return isRealtimeData; }
            set
            {
                if (isRealtimeData != value)
                {
                    isRealtimeData = value;
                    NotifyPropertyChanged("IsRealtimeData");
                }
            }
        }
    }
}
