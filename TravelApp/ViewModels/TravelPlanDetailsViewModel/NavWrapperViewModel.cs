using System;
using System.Collections.ObjectModel;
using TravelApp.Models;
using TravelApp.Views;
using TravelApp.Views.TravelPlanDetailsPage;
using ViewModel;

namespace TravelApp.ViewModels
{

    class NavWrapperViewModel: ComputedBindableBase
    {
        #region Properties
        private User _user;

        private TravelPlan _selectedtravelplan;

        public TravelPlan SelectedTravelPlan
        {
            get
            {
                return _selectedtravelplan;
            }
            set
            {
                Set(ref _selectedtravelplan, value);
            }
        }

        public string UserName
        {
            get
            {
                return _user.UserName;
            }
            set
            {
                _user.UserName = value;
                RaisePropertyChanged("UserName");
            }
        }
        #endregion

        public NavWrapperViewModel(TravelPlanToNavViewNavigationEventArgs e)
        {
            _user = e.User;
            foreach(TravelPlan tl in _user.Travelplans)
            {
                if(tl.Name == e.SelectedTravelPlanName)
                {
                    SelectedTravelPlan = tl;
                }
            }
        }

        public EventArgs GetArgs(Type pagetype)
        {
            if(Type.Equals(pagetype, typeof(ItemFrame)))
            {
                return GetItemArgs();
            }
            if (Type.Equals(pagetype, typeof(TaskFrame)))
            {
                return GetTaskArgs();
            }
            if (Type.Equals(pagetype, typeof(RouteFrame)))
            {
                return GetRouteArgs();
            }
            if(Type.Equals(pagetype, typeof(TravelPlanPage)))
            {
                return GetBackArgs();
            }
            return null;
        }

        private NavViewItemsEventArgs GetItemArgs()
        {
            return new NavViewItemsEventArgs
            {
                Items = SelectedTravelPlan.ItemList
            };
        }

        private NavViewTasksEventArgs GetTaskArgs()
        {
            return new NavViewTasksEventArgs
            {
                Tasks = SelectedTravelPlan.TaskList
            };
        }

        private NavViewRoutesEventArgs GetRouteArgs()
        {
            return new NavViewRoutesEventArgs
            {
                Travelroutes = SelectedTravelPlan.RouteList
            };
        }

        private StartToTravelPlanNavigationEventArgs GetBackArgs()
        {
            return new StartToTravelPlanNavigationEventArgs
            {
                User = _user
            };
        }

    }
}
