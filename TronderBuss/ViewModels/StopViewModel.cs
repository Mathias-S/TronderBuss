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
                    OnPropertyChange("BusStopId");
                    id = value;
                }
            }
        }
        public string Name { get; set; }
        public string NameWithAbbreviations { get; set; }
        public string BusStopMaintainer { get; set; }
        public int LocationId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
