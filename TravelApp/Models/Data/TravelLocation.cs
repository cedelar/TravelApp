using System;

namespace TravelApp.Models
{
    /// <Summary>
    /// Data class for the TraveLocation
    /// </Summary>
    public class TravelLocation
    {
        #region Properties
        public string Name { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion

        #region Constructors
        public TravelLocation(string name, string location, double latitude, double longitude)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Latitude = latitude;
            Longitude = longitude;
        }
        #endregion

        #region Methods
        public string GetCoordinateString()
        {
            return Latitude.ToString("0.###") + "° Latitude, " + Longitude.ToString("0.###") + "° Longitude";
        }

        public override string ToString()
        {
            return Name + ": " + Location + " (" + GetCoordinateString() + ")";
        }
        #endregion
    }
}
