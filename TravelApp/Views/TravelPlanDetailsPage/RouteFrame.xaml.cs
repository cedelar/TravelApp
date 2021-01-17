using System;
using TravelApp.ViewModels.TravelPlanDetailsViewModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Navigation;


namespace TravelApp.Views.TravelPlanDetailsPage
{
    /// <summary>
    /// Code behind class for the RouteList Frame
    /// </summary>
    public sealed partial class RouteFrame : Page
    {
        #region Properties
        private RouteFrameViewModel _vm;
        #endregion

        #region Constructors
        public RouteFrame()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Methods
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vm = new RouteFrameViewModel((Models.NavViewNavigationEventArgs) e.Parameter);

            _vm.RequestMapUpdate += (s, ea) =>
            {
                UpdateMap();
            };
            base.OnNavigatedTo(e);
        }

        private void UpdateMap()
        {
            LocationMap.MapElements.Clear();

            for (int i = 0; i < _vm.LocationList.Count; i++)
            {
                Geopoint mapPoint = new Geopoint(new BasicGeoposition
                {
                    Latitude = _vm.LocationList[i].Latitude,
                    Longitude = _vm.LocationList[i].Longitude
                });

                try
                {
                    MapIcon mapicon = new MapIcon
                    {
                        Location = mapPoint,
                        NormalizedAnchorPoint = new Point(0.5, 1.0),
                        Title = _vm.LocationList[i].Name,
                        ZIndex = 0
                    };
                    LocationMap.MapElements.Add(mapicon);
                }
                catch(Exception ex)
                {
                    _vm.MessageUpdate(ex.Message);
                }

                if((_vm.SelectedLocation != null && _vm.SelectedLocation.Name == _vm.LocationList[i].Name) ||
                    (_vm.SelectedLocation == null && i == 0))
                {
                    LocationMap.Center = mapPoint;
                }
            }
            LocationMap.ZoomLevel = 12;
        }

        public void Maptest()
        {
            Geopoint myPoint2 = new Geopoint(new BasicGeoposition() { Latitude = 52, Longitude = 0 });
            MapIcon myPOI2 = new MapIcon { Location = myPoint2, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "Test", ZIndex = 0 };
            LocationMap.MapElements.Add(myPOI2);
            LocationMap.ZoomLevel = 10;
        }

        private void NewRouteButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnNewRouteClicked(nameRouteInput.Text, descriptionRouteInput.Text);

            //Reset
            nameRouteInput.Text = "";
            descriptionRouteInput.Text = "";
        }

        private void CloseRouteButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnRouteCloseClicked();
        }

        private async void NewLocationButton_Click(object sender, RoutedEventArgs e)
        {
            string newName = nameLocationInput.Text;
            string newLocation = locationInput.Text;
            double newLatitude = 0.0;
            double newLongitude = 0.0;

            if (!string.IsNullOrEmpty(newName) && !string.IsNullOrEmpty(newLocation))
            {
                //Get geocords location
                MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(newLocation, _vm.hintPoint, 1);

                if (result.Status == MapLocationFinderStatus.Success && result.Locations.Count > 0)
                {
                    newLatitude = result.Locations[0].Point.Position.Latitude;
                    newLongitude = result.Locations[0].Point.Position.Longitude;
                }
            }
            _vm.OnNewLocationClicked(nameLocationInput.Text, locationInput.Text, newLatitude, newLongitude);

            //reset
            nameLocationInput.Text = "";
            locationInput.Text = "";
        }

        private void CloseLocationButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.OnLocationCloseClicked();
        }
        #endregion
    }
}
