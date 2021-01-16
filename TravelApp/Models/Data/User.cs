using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        public ObservableCollection<TravelPlan> Travelplans { get; set; }

        public User(string userName, string password)
        {
            this.UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password;
            Travelplans = new ObservableCollection<TravelPlan>();
        }

        public void addTravelPlan(TravelPlan travelPlan)
        {
            Travelplans.Add(travelPlan);
        }

        public void RemoveTravelPlan(TravelPlan travelplan)
        {
            Travelplans.Remove(travelplan);
        }
    }
}
