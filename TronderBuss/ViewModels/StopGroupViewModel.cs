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

namespace TronderBuss.ViewModels
{
    public class StopGroupViewModel : INotifyPropertyChanged
    {
        private string name;
        private List<int> ids;

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

        private void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
