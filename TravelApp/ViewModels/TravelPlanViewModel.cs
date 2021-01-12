using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TravelApp.Models;
using TravelApp.Repository;
using ViewModel;
using Windows.UI.Popups;

namespace TravelApp.ViewModels
{
    class TravelPlanViewModel : ComputedBindableBase
    {
        #region properties

        private User _user;

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

        public ObservableCollection<TravelPlan> Travelplans
        {
            get
            {
                return _user.Travelplans;
            }
        }

        private TravelPlan _selectedItem;
        public TravelPlan SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if(_selectedItem != value)
                {
                    Set(ref _selectedItem, value);
                }
            }
        }

        [PropertySource(nameof(SelectedItem))]
        public bool ControlButtonsVisible => _selectedItem != null;

        public event EventHandler<TravelPlanToNavViewNavigationEventArgs> NavigateToNavViewWrapperPage;
        #endregion



        public TravelPlanViewModel(StartToTravelPlanNavigationEventArgs args)
        {
            _user = args.User;

        }

        public void OnNewTravelPlan()
        {
            //TODO
            UserName = "New";
        }

        public void OnEditTravelPlan()
        {
            //TODO
            UserName = "Edit " + SelectedItem.Name;
        }

        public void OnDeleteTravelPlan()
        {
            //TODO
            UserName = "Delete " + SelectedItem.Name;
        }

        public void OnSelectTravelPlan()
        {
            if (_selectedItem != null)
            {
                TravelPlanToNavViewNavigationEventArgs args = new TravelPlanToNavViewNavigationEventArgs
                {
                    User = _user,
                    SelectedTravelPlanName = _selectedItem.Name
                };
                NavigateToNavViewWrapperPage?.Invoke(this, args);
            }
        }


    }
}
