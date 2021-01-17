using Newtonsoft.Json;
using System;

namespace TravelApp.Models
{
    /// <Summary>
    /// Data class for the TravelTask
    /// </Summary>
    public class TravelTask
    {
        #region Properties
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }
        #endregion

        #region Constructors
        public TravelTask(string name, Priority priority, string description)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.IsChecked = false;
            this.Priority = priority;
            this.Description = description;
        }

        [JsonConstructor]
        public TravelTask(string name, int priority, string description)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.IsChecked = false;
            this.Priority = (Priority) priority;
            this.Description = description;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Name + ": " + Priority.ToString() + " Priority";
        }
        #endregion
    }

    /// <Summary>
    /// Wrapperclass for sending TravelTasks to the server
    /// </Summary>
    public class NetworkTravelTask
    {
        #region Properties
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        #endregion

        #region Constructors
        public NetworkTravelTask(string name, Priority priority, string description)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.IsChecked = false;
            this.Priority = (int) priority;
            this.Description = description;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Name + ": " + Priority.ToString() + " Priority, desc: " + Description;
        }
        #endregion
    }

    /// <Summary>
    /// Enum for the NetworkTaskproperty Priority
    /// </Summary>
    public enum Priority
    {
        Low = -1,
        Normal = 0,
        High = 1,
        VeryHigh = 2
    }
}
