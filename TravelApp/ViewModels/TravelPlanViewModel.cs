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
                return _user.userName;
            }
            set
            {
                _user.userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        public ObservableCollection<TravelPlan> Travelplans
        {
            get
            {
                return _user.travelplans;
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
                    _selectedItem = value;
                    Set(ref _selectedItem, value);
                }
            }
        }

        [PropertySource(nameof(SelectedItem))]
        public bool ControlButtonsVisible => _selectedItem != null;
        #endregion



        public TravelPlanViewModel(StartToTravelPlanNavigationEventArgs args)
        {
            _user = DataProvider.getUserdata(args.username);

        }

        public void onNewTravelPlan()
        {
            //TODO
            UserName = "New";
        }

        public void onEditTravelPlan()
        {
            //TODO
            UserName = "Edit " + SelectedItem.Name;
        }

        public void onDeleteTravelPlan()
        {
            //TODO
            UserName = "Delete " + SelectedItem.Name;
        }

        public void onSelectTravelPlan()
        {
            //TODO
            UserName = "Select " + SelectedItem.Name;
        }


    }
}
