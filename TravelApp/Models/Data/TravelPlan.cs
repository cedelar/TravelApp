using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models
{
    class TravelPlan
    {
        #region properties
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

        public TravelPlan(string name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.StartDate = new DateTime();
            this.EndDate = new DateTime();
            this.Destination = "-";
            ItemList = new ObservableCollection<TravelItem>();
            TaskList = new ObservableCollection<TravelTask>();
            RouteList = new ObservableCollection<TravelRoute>();
        }

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
    }
}
