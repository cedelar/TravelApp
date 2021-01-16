using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class TravelLocation
    {

        public string Name { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public TravelLocation(string name, string location, double latitude, double longitude)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Latitude = latitude;
            Longitude = longitude;
        }

        public string GetCoordinateString()
        {
            return Latitude + "° Latitude, " + Longitude + "° Longitude";
        }

        public override string ToString()
        {
            return Name + ": " + Location + " (" + GetCoordinateString() + ")";
        }
    }
}
