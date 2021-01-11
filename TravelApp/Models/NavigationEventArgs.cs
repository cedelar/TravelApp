using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class StartToTravelPlanNavigationEventArgs : EventArgs
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
