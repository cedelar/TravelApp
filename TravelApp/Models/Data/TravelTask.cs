using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class TravelTask
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }

        public TravelTask(string naam, Priority priority, string description)
        {
            this.Name = naam ?? throw new ArgumentNullException(nameof(naam));
            this.IsChecked = false;
            this.Priority = priority;
            this.Description = description;
        }

        public TravelTask(string naam): this(naam, Priority.Normal, null){}

        public override string ToString()
        {
            return Name + ": " + Priority.ToString() + " Priority";
        }
    }

    enum Priority
    {
        Low,
        Normal,
        High,
        VeryHigh
    }
}
