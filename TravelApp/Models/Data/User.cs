using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class User
    {
        public string UserName { get; set; }
        public ObservableCollection<TravelPlan> Travelplans { get; set; }

        public User(string userName)
        {
            this.UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Travelplans = new ObservableCollection<TravelPlan>();
        }

        public void addTravelPlan(TravelPlan travelPlan)
        {
            Travelplans.Add(travelPlan);
        }
    }
}
