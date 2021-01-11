using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class TravelRoute
    {
        public string name { get; set; }
        public ObservableCollection<TravelLocation> locations { get; private set; }
        public string description { get; set; }

        public TravelRoute(string name, string description)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.description = description;
            this.locations = new ObservableCollection<TravelLocation>();
        }

        public void addTravelLocation(TravelLocation travelLocation)
        {
            locations.Add(travelLocation);
        }
    }
}
