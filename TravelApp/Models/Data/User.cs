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
        public string userName { get; set; }
        public ObservableCollection<TravelPlan> travelplans { get; set; }

        public User(string userName)
        {
            this.userName = userName ?? throw new ArgumentNullException(nameof(userName));
            travelplans = new ObservableCollection<TravelPlan>();
        }

        public void addTravelPlan(TravelPlan travelPlan)
        {
            travelplans.Add(travelPlan);
        }
    }
}
