using System;
using System.Collections.ObjectModel;
using TravelApp.Models;
using ViewModel;

namespace TravelApp.ViewModels.TravelPlanDetailsViewModel
{
    class RouteFrameViewModel : ComputedBindableBase
    {
        private ObservableCollection<TravelRoute> _routeList;

        public ObservableCollection<TravelRoute> RouteList
        {
            get
            {
                return _routeList;
            }
            set
            {
                Set(ref _routeList, value);
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

        public event EventHandler<EventArgs> RequestMapUpdate;

        public RouteFrameViewModel(NavViewRoutesEventArgs e)
        {
            _routeList = e.Travelroutes;
        }
    }
}

