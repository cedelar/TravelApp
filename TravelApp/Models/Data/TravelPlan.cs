using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TravelApp.Models
{
    /// <Summary>
    /// Data class for the TravelPlan
    /// </Summary>
    public class TravelPlan
    {
        #region Properties
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Destination  { get; set; }

        public ObservableCollection<TravelItem> ItemList { get; private set; }
        public ObservableCollection<TravelTask> TaskList { get; private set; }
        public ObservableCollection<TravelRoute> RouteList { get; private set; }

        public int CompletedTaskCount
        {
            get
            {
                return TaskList.Where(item => item.IsChecked == true).Count();
            }
        }

        public int TotalTaskCount
        {
            get
            {
                return TaskList.Count();
            }
        }

        public int CompletedItemCount
        {
            get
            {
                return ItemList.Where(item => item.IsChecked == true).Count();
            }
        }
        public int TotalItemCount
        {
            get
            {
                return ItemList.Count();
            }
        }
        public int TotalLocationCount
        {
            get
            {
                return RouteList.Select(l => l.Locations.Count()).Sum();
            }
        }
        #endregion

        #region Constructors
        [JsonConstructor]
        public TravelPlan(string name, DateTime startDate, DateTime endDate, string destination)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.StartDate = startDate == null ? new DateTime() : startDate;
            this.EndDate = endDate == null ? new DateTime() : endDate;
            this.Destination = destination == null ? "-" : destination;
            ItemList = new ObservableCollection<TravelItem>();
            TaskList = new ObservableCollection<TravelTask>();
            RouteList = new ObservableCollection<TravelRoute>();
        }
        #endregion

        #region Methods
        public void AddTravelItem(TravelItem travelItem)
        {
            ItemList.Add(travelItem);
        }

        public void AddTravelTask(TravelTask travelTask)
        {
            TaskList.Add(travelTask);
        }

        public void AddTravelRoute(TravelRoute travelRoute)
        {
            RouteList.Add(travelRoute);
        }

        public override string ToString()
        {
            return Name + ") Items: " + CompletedItemCount + "/" + TotalItemCount + ", Tasks: " + CompletedTaskCount + "/" + TotalTaskCount;
        }

        public string ItemsCompletedString()
        {
            return CompletedItemCount + "/" + TotalItemCount;
        }

        public string TasksCompletedString()
        {
            return CompletedTaskCount + "/" + TotalTaskCount;
        }
        #endregion
    }
}
