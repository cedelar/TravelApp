using System;

/// <Summary>
/// EventArgs for passing data on navigation
/// </Summary>
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
