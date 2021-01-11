using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class TravelLocation
    {
        public string name { get; set; }
        public string adress { get; set; }

        public TravelLocation(string name, string adress)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.adress = adress ?? throw new ArgumentNullException(nameof(adress));
        }
    }
}
