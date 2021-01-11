using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class TravelTask
    {
        public string naam { get; set; }
        public bool isChecked { get; set; }
        public Priority priority { get; set; }
        public string description { get; set; }

        public TravelTask(string naam, Priority priority, string description)
        {
            this.naam = naam ?? throw new ArgumentNullException(nameof(naam));
            this.isChecked = false;
            this.priority = priority;
            this.description = description;
        }

        public TravelTask(string naam): this(naam, Priority.Normal, null){}
    }

    enum Priority
    {
        Low,
        Normal,
        High,
        VeryHigh
    }
}
