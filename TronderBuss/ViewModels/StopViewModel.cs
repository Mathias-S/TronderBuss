using System.ComponentModel;

namespace TronderBuss.ViewModels
{
    public class StopViewModel : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string nameWithAbbrevations;
        private string busStopMaintainer;
        private int locationId;
        private double longitude;
        private double latitude;

        public int BusStopId
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChange("BusStopId");
                }
            }
        }
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
        public string NameWithAbbreviations {
            get { return nameWithAbbrevations; }
            set
            {
                if (nameWithAbbrevations != value)
                {
                    nameWithAbbrevations = value;
                    OnPropertyChange("NameWithAbbreviations");
                }
            }
        }
        public string BusStopMaintainer
        {
            get { return busStopMaintainer; }
            set
            {
                if (busStopMaintainer != value)
                {
                    busStopMaintainer = value;
                    OnPropertyChange("BusStopMaintainer");
                }
            }
        }
        public int LocationId
        {
            get { return locationId; }
            set
            {
                if (locationId != value)
                {
                    locationId = value;
                    OnPropertyChange("LocationId");
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
                    OnPropertyChange("Longitude");
                }
            }
        }
        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (latitude != value)
                {
                    latitude = value;
                    OnPropertyChange("Latitude");
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
