using System;
using System.Collections.ObjectModel;
using TravelApp.Models;
using ViewModel;

namespace TravelApp.ViewModels.TravelPlanDetailsViewModel
{
    class RouteFrameViewModel : ComputedBindableBase
    {

        #region Properties
        private const string BASE_URL = "http://localhost:51758/api/";

        public ObservableCollection<TravelRoute> RouteList
        {
            get
            {
                return TravelPlan.RouteList;
            }
        }

        private TravelRoute _selectedRoute;
        public TravelRoute SelectedRoute
        {
            get
            {
                return _selectedRoute;
            }
            set
            {
                if (_selectedRoute != value)
                {
                    Set(ref _selectedRoute, value);
                    RequestMapUpdate?.Invoke(this, new EventArgs());
                }
            }
        }

        [PropertySource(nameof(SelectedRoute))]
        public ObservableCollection<TravelLocation> LocationList
        {
            get
            {
                if(_selectedRoute != null)
                {
                    return _selectedRoute.Locations;
                }
                return new ObservableCollection<TravelLocation>();
            }
        }


        private TravelLocation _selectedLocation;
        public TravelLocation SelectedLocation
        {
            get
            {
                return _selectedLocation;
            }
            set
            {
                if (_selectedLocation != value)
                {
                    Set(ref _selectedLocation, value);
                    RequestMapUpdate?.Invoke(this, new EventArgs());
                }
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                Set(ref _isLoading, value);
            }
        }

        [PropertySource(nameof(IsLoading))]
        public bool ButtonsEnabled => !IsLoading;


        [PropertySource(nameof(SelectedLocation))]
        public bool ControlButtonsVisible => _selectedLocation != null;

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                Set(ref _message, value);
            }
        }

        private bool _showNewRouteFields;
        public bool ShowNewRouteFields
        {
            get
            {
                return _showNewRouteFields;
            }
            set
            {
                Set(ref _showNewRouteFields, value);
            }
        }

        private bool _showNewLocationFields;
        public bool ShowNewLocationFields
        {
            get
            {
                return _showNewLocationFields;
            }
            set
            {
                Set(ref _showNewLocationFields, value);
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                Set(ref _userName, value);
            }
        }

        private TravelPlan _travelPlan;
        public TravelPlan TravelPlan
        {
            get
            {
                return _travelPlan;
            }
            set
            {
                Set(ref _travelPlan, value);
            }
        }

        public event EventHandler<EventArgs> RequestMapUpdate;
        #endregion

        public RouteFrameViewModel(NavViewNavigationEventArgs e)
        {
            UserName = e.Username;
            TravelPlan = e.TravelPlan;
            IsLoading = false;
            ResetMessage();
            ShowNewRouteFields = false;
        }

        private void ResetMessage()
        {
            Message = "";
        }

        public void OnNewRouteClicked(string newName, string newDescription)
        {
            if (!ShowNewRouteFields)
            {
                ShowNewRouteFields = true;
            }
            else
            {
                if (string.IsNullOrEmpty(newName))
                {
                    Message = "A name is required, try again.";
                    return;
                }

                IsLoading = true;
                Message = "Processing, please wait.";
                string newDescriptionRef = string.IsNullOrEmpty(newDescription) ? "No Description" : newDescription;
                TravelRoute newRoute = new TravelRoute(newName, newDescriptionRef);
                Message = newRoute.ToString();

                //REST

                ShowNewRouteFields = false;
                IsLoading = false;
            }
        }

        public void OnNewLocationClicked(string newName, string newLocation, double newLatitude, double newLongitude)
        {
            if (!ShowNewLocationFields)
            {
                ShowNewLocationFields = true;
            }
            else
            {
                if (string.IsNullOrEmpty(newName))
                {
                    Message = "A name is required, try again.";
                    return;
                }

                if (string.IsNullOrEmpty(newLocation))
                {
                    Message = "A location is required, try again.";
                    return;
                }

                IsLoading = true;
                Message = "Processing, please wait.";
                TravelLocation newTravelLocation = new TravelLocation(newName, newLocation, newLatitude, newLongitude);
                Message = newTravelLocation.ToString();

                //REST

                ShowNewRouteFields = false;
                IsLoading = false;
            }
        }

        public void OnRouteCloseClicked()
        {
            ShowNewRouteFields = false;
        }

        public void OnLocationCloseClicked()
        {
            ShowNewLocationFields = false;
        }
    }
}

