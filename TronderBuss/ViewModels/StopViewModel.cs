using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace TronderBuss.ViewModels
{
    [Table]
    public class StopViewModel : ViewModelBase
    {
        private int id;
        private string name;
        private string nameWithAbbrevations;
        private string busStopMaintainer;
        private int locationId;
        private double longitude;
        private double latitude;

        [Column(IsPrimaryKey = true, IsDbGenerated = false, DbType = "INT NOT NULL", CanBeNull = false)]
        public int BusStopId
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    NotifyPropertyChanged("BusStopId");
                }
            }
        }
        [Column]
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
        [Column]
        public string NameWithAbbreviations {
            get { return nameWithAbbrevations; }
            set
            {
                if (nameWithAbbrevations != value)
                {
                    nameWithAbbrevations = value;
                    NotifyPropertyChanged("NameWithAbbreviations");
                }
            }
        }
        [Column]
        public string BusStopMaintainer
        {
            get { return busStopMaintainer; }
            set
            {
                if (busStopMaintainer != value)
                {
                    busStopMaintainer = value;
                    NotifyPropertyChanged("BusStopMaintainer");
                }
            }
        }
        [Column]
        public int LocationId
        {
            get { return locationId; }
            set
            {
                if (locationId != value)
                {
                    locationId = value;
                    NotifyPropertyChanged("LocationId");
                }
            }
        }
        [Column]
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
        [Column]
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
    }
}
