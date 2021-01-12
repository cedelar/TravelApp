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

        public RouteFrameViewModel(NavViewRoutesEventArgs e)
        {
            _routeList = e.Travelroutes;
        }
    }
}

