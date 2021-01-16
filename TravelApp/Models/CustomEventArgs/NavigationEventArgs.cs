using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class StartToTravelPlanNavigationEventArgs : EventArgs
    { 
        public User User { get; set; }
    }

    class TravelPlanToNavViewNavigationEventArgs : EventArgs
    {
        public User User { get; set; }

        public string SelectedTravelPlanName { get; set; }
    }

    class NavViewNavigationEventArgs : EventArgs
    {
        public string Username { get; set; }
        public TravelPlan TravelPlan { get; set; }
    }
}
