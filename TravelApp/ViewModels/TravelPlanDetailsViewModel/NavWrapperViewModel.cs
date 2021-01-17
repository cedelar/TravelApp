using System;
using TravelApp.Models;
using TravelApp.Views;
using TravelApp.Views.TravelPlanDetailsPage;
using ViewModel;

namespace TravelApp.ViewModels
{
    /// <Summary>
    /// View model class for the NavigationView wrapper page
    /// </Summary>
    class NavWrapperViewModel : ComputedBindableBase
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

        #region Constructors
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
        #endregion

        #region Methods
        public EventArgs GetArgs(Type pagetype)
        {
            if(Type.Equals(pagetype, typeof(ItemFrame)) ||
               Type.Equals(pagetype, typeof(TaskFrame)) ||
               Type.Equals(pagetype, typeof(RouteFrame)) ||
               Type.Equals(pagetype, typeof(SettingsFrame))
               )
            {
                return GetNavigationArgs();
            }
            if(Type.Equals(pagetype, typeof(TravelPlanPage)))
            {
                return GetBackArgs();
            }
            return null;
        }

        private NavViewNavigationEventArgs GetNavigationArgs()
        {
            return new NavViewNavigationEventArgs
            {
                Username = UserName,
                TravelPlan = SelectedTravelPlan
            };
        }

        private StartToTravelPlanNavigationEventArgs GetBackArgs()
        {
            return new StartToTravelPlanNavigationEventArgs
            {
                User = _user
            };
        }
        #endregion
    }
}
