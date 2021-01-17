using System;
using System.Collections.ObjectModel;

namespace TravelApp.Models
{
    /// <Summary>
    /// Data class for the User
    /// </Summary>
    public class User
    {
        #region Properties
        public string UserName { get; set; }
        public string Password { get; set; }
        public ObservableCollection<TravelPlan> Travelplans { get; set; }
        #endregion

        #region Constructors
        public User(string userName, string password)
        {
            this.UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password;
            Travelplans = new ObservableCollection<TravelPlan>();
        }
        #endregion

        #region Methods
        public void AddTravelPlan(TravelPlan travelPlan)
        {
            Travelplans.Add(travelPlan);
        }
        public void RemoveTravelPlan(TravelPlan travelplan)
        {
            Travelplans.Remove(travelplan);
        }
        #endregion
    }
}
