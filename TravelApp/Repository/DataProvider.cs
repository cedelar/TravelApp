using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;

namespace TravelApp.Repository
{
    static class DataProvider
    {
        public static User getUserdata(string name)
        {
            User user = new User(name);

            List<string> travelnames = new List<string> { 
                "Venetië - 2021", 
                "Perth - 2022", 
                "Praag - Ooit"};

            List<List<string>> itemnames = new List<List<string>> {
               new List<string>{ "kam", "shampoo", "lader", "kaart", "gsm" },
               new List<string>{ "kam", "lader", "gsm" },
               new List<string>{}
            };

            List<List<string>> tasknames = new List<List<string>> {
                new List<string> { "Tickets kopen", "Biertasting plannen", "hotel boeken" },
                new List<string> { "Biertasting plannen", "hotel boeken" },
                new List<string> {}
            };

            for(int i = 0; i< travelnames.Count; i++)
            {
                TravelPlan plan = new TravelPlan(travelnames[i]);
                itemnames[i].ForEach(s => plan.AddTravelItem(new TravelItem(s)));
                tasknames[i].ForEach(s => plan.AddTravelTask(new TravelTask(s)));
                user.addTravelPlan(plan);
            }

            return user;
        }
    }
}
