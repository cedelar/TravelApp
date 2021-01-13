using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            List<string> travelnames = new List<string> 
            { 
                "Venetië - 2021", 
                "Perth - 2022", 
                "Praag - Ooit"
            };

            List<DateTime> startdates = new List<DateTime>
            {
                new DateTime(2021, 8, 10),
                new DateTime(2022, 7, 21),
                new DateTime()
            };

            List<DateTime> enddates = new List<DateTime>
            {
                new DateTime(2021, 8, 17),
                new DateTime(2022, 8, 7),
                new DateTime()
            };

            List<String> destinations = new List<string>
            {
                "Venetië",
                "Perth",
                "Praag"
            };

            List<List<TravelItem>> itemnames = new List<List<TravelItem>> 
            {
               new List<TravelItem>{ 
                   new TravelItem("Kam", 2, "Badkamer"),
                   new TravelItem("Shampoo", 1, "Badkamer"),
                   new TravelItem("Handdoek", 4, "Badkamer"),
                   new TravelItem("Lader", 2, "Electronica"),
                   new TravelItem("Ipad", 1, "Electronica"),
                   new TravelItem("Zeep", 2, "Badkamer") 
               },
               new List<TravelItem>{                    
                   new TravelItem("Shampoo", 1, "Badkamer"),
                   new TravelItem("Handdoek", 4, "Badkamer"),
                   new TravelItem("Lader", 2, "Electronica"), 
               },
               new List<TravelItem>{}
            };


            List<List<TravelTask>> tasknames = new List<List<TravelTask>> 
            {
                new List<TravelTask> { 
                    new TravelTask("Biertasting reserveren", Priority.VeryHigh, "Veeeeeeeeeeeery important"),
                    new TravelTask("Tickets kopen", Priority.High, "Might as well"),
                    new TravelTask("Hotel boeken", Priority.Low, "The streets are comfy"),
                    new TravelTask("Vliegtickets kopen", Priority.Low, "I believe i can fly"),
                    new TravelTask("Infocentrum zoeken", Priority.Normal, "What to do?"),
                },
                new List<TravelTask> {
                    new TravelTask("Hotel boeken", Priority.Low, "The streets are comfy"),
                    new TravelTask("Vliegtickets kopen", Priority.Low, "I believe i can fly"),
                },
                new List<TravelTask> {}
            };

            List<List<TravelLocation>> locations = new List<List<TravelLocation>>
            {
                new List<TravelLocation>
                {
                new TravelLocation("Kemzeke", "Kemzeke"),
                new TravelLocation("Sint-Pauwels", "Sint-Pauwels"),
                new TravelLocation("Sint-Niklaas", "Sint-Niklaas"),
                new TravelLocation("Beveren", "Beveren"),
                new TravelLocation("Sinaai", "Sinaai"),
                },
                new List<TravelLocation>
                {
                new TravelLocation("Gent", "Gent"),
                new TravelLocation("Antwerpen", "Antwerpen")
                }

            };

            for(int i = 0; i< travelnames.Count; i++)
            {
                TravelPlan plan = new TravelPlan(travelnames[i]);
                plan.StartDate = startdates[i];
                plan.EndDate = enddates[i];
                plan.Destination = destinations[i];
                itemnames[i].ForEach(s => plan.AddTravelItem(s));
                tasknames[i].ForEach(s => plan.AddTravelTask(s));
                plan.AddTravelRoute(new TravelRoute("route1", "desc1", new ObservableCollection<TravelLocation>(locations[0])));
                plan.AddTravelRoute(new TravelRoute("route2", "desc2", new ObservableCollection<TravelLocation>(locations[1])));
                plan.AddTravelRoute(new TravelRoute("route3", "desc3"));
                user.addTravelPlan(plan);
            }

            return user;
        }
    }
}
