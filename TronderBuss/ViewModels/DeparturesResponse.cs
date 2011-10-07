using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TronderBuss.ViewModels
{
    public class DeparturesResponse : INotifyPropertyChanged
    {
        private bool isGoingTowardsCentrum;
        private List<DepartureViewModel> departures;

        public bool IsGoingTowardsCentrum
        {
            get { return isGoingTowardsCentrum; }
            set
            {
                if (isGoingTowardsCentrum != value)
                {
                    isGoingTowardsCentrum = value;
                    OnPropertyChange("IsGoingTowardsCentrum");
                }
            }
        }
        public List<DepartureViewModel> Departures
        {
            get { return departures; }
            set
            {
                if (departures != value)
                {
                    departures = value;
                    OnPropertyChange("Departures");
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
