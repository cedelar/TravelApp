using System;
using System.ComponentModel;
using System.Windows.Input;
using TravelApp.Models;
using ViewModel;

namespace TravelApp.ViewModels
{
    class StartPageViewModel: BindableBase
    {
        #region Properties
        public event EventHandler<StartToTravelPlanNavigationEventArgs> NavigateToTravelPlanPage;
        #endregion

        public StartPageViewModel()
        {

        }


        public void OnLogin(string username, string password)
        {
            if(!string.IsNullOrEmpty(username) && username == password)
            {
                StartToTravelPlanNavigationEventArgs args = new StartToTravelPlanNavigationEventArgs();
                args.username = username;
                args.password = password;
                NavigateToTravelPlanPage?.Invoke(this, args);
            }
        }

        public void onAddUser()
        {
            //TODO
        }
    }
}
