using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class TravelItem
    {
        public string naam { get; set; }
        public bool isChecked { get; set; }
        public int amount { get; set; }
        public string category { get; set; }

        public TravelItem(string naam, int amount, string category)
        {
            this.naam = naam ?? throw new ArgumentNullException(nameof(naam));
            this.amount = amount;
            this.category = category ?? throw new ArgumentNullException(nameof(category));
            isChecked = false;
        }

        public TravelItem(string naam) : this(naam, 0, "default") { }
        
    }
}
