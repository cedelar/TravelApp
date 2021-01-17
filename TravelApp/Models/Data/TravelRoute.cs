using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace TravelApp.Models
{
    /// <Summary>
    /// Data class for the TravelRoute
    /// </Summary>
    public class TravelRoute
    {
        #region Properties
        public string Name { get; set; }
        public ObservableCollection<TravelLocation> Locations { get; private set; }
        public string Description { get; set; }
        #endregion

        #region Constructors
        [JsonConstructor]
        public TravelRoute(string name, string description)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Description = description;
            this.Locations = new ObservableCollection<TravelLocation>();
        }
        #endregion

        #region Methods
        public void AddTravelLocation(TravelLocation travelLocation)
        {
            Locations.Add(travelLocation);
        }

        public override string ToString()
        {
            return Name + ": " + Description;
        }
        #endregion
    }
}
