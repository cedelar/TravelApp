using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class TravelLocation
    {

        public string Name { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public TravelLocation(string name, string location)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Location = location ?? throw new ArgumentNullException(nameof(location));
            double[] coords = Geolocator.getCoordinatesAsync(Location).Result;
            Latitude = coords[0];
            Longitude = coords[1];
        }

        public String GetCoordinateString()
        {
            return Latitude + "° Latitude, " + Longitude + "° Longitude";
        }
    }
}
