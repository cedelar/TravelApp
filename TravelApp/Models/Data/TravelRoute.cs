using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class TravelRoute
    {
        public string Name { get; set; }
        public ObservableCollection<TravelLocation> Locations { get; private set; }
        public string Description { get; set; }

        public TravelRoute(string name, string description, ObservableCollection<TravelLocation> locations)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Description = description;
            this.Locations = locations;
        }

        [JsonConstructor]
        public TravelRoute(string name, string description): this(name, description, new ObservableCollection<TravelLocation>())
        {
        }

        public void AddTravelLocation(TravelLocation travelLocation)
        {
            Locations.Add(travelLocation);
        }

        public override string ToString()
        {
            return Name + ": " + Description;
        }
    }
}
