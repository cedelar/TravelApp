using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class TravelTask
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }

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

        public TravelTask(string naam): this(naam, Priority.Normal, null){}

        public override string ToString()
        {
            return Name + ": " + Priority.ToString() + " Priority";
        }
    }

    public class NetworkTravelTask
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }

        public NetworkTravelTask(string name, Priority priority, string description)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.IsChecked = false;
            this.Priority = (int) priority;
            this.Description = description;
        }

        public override string ToString()
        {
            return Name + ": " + Priority.ToString() + " Priority, desc: " + Description;
        }
    }

    public enum Priority
    {
        Low = -1,
        Normal = 0,
        High = 1,
        VeryHigh = 2
    }
}
