using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelApp.ViewModels.TravelPlanDetailsViewModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelApp.Views.TravelPlanDetailsPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RouteFrame : Page
    {
        private RouteFrameViewModel _vm;

        public RouteFrame()
        {
            this.InitializeComponent();
            geocode("Kemzeke");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _vm = new RouteFrameViewModel((Models.NavViewRoutesEventArgs) e.Parameter);
            _vm.RequestMapUpdate += (s, ea) =>
            {
                UpdateMapAsync();
            };

            base.OnNavigatedTo(e);
        }

        private async void UpdateMapAsync()
        {
            LocationMap.MapElements.Clear();
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = Models.Geolocator.LatitudeHint;
            queryHint.Longitude = Models.Geolocator.LongitudeHint;
            Geopoint hintPoint = new Geopoint(queryHint);

            for (int i = 0; i < _vm.LocationList.Count; i++)
            {
                MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(_vm.LocationList[i].Location, hintPoint, 3);
                Geopoint geopoint = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 0,
                    Longitude = 0
                });

                if (result.Status == MapLocationFinderStatus.Success && result.Locations.Count > 0)
                {
                    geopoint = new Geopoint(new BasicGeoposition()
                    {
                        Latitude = result.Locations[0].Point.Position.Latitude,
                        Longitude = result.Locations[0].Point.Position.Longitude
                    });

                }
                try
                {
                    MapIcon mapicon = new MapIcon
                    {
                        Location = geopoint,
                        NormalizedAnchorPoint = new Point(0.5, 1.0),
                        Title = _vm.LocationList[i].Name,
                        ZIndex = 0
                    };
                    LocationMap.MapElements.Add(mapicon);
                }
                catch(ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if(_vm.SelectedLocation != null &&_vm.SelectedLocation.Name == _vm.LocationList[i].Name)
                {
                    LocationMap.Center = geopoint;
                }
            }
            LocationMap.ZoomLevel = 10;
        }

        public void Maptest()
        {
            Geopoint myPoint2 = new Geopoint(new BasicGeoposition() { Latitude = 52, Longitude = 0 });
            MapIcon myPOI2 = new MapIcon { Location = myPoint2, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "Test", ZIndex = 0 };
            LocationMap.MapElements.Add(myPOI2);
            LocationMap.ZoomLevel = 10;

        }
        private async void geocode(string location)
        {

            // The nearby location to use as a query hint.
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 54;
            queryHint.Longitude = 2;
            Geopoint hintPoint = new Geopoint(queryHint);

            // Geocode the specified address, using the specified reference point
            // as a query hint. Return no more than 3 results.
            MapLocationFinderResult result =
                  await MapLocationFinder.FindLocationsAsync(
                                    location,
                                    hintPoint,
                                    3);

            // If the query returns results, display the coordinates
            // of the first result.
            if (result.Status == MapLocationFinderStatus.Success && result.Locations.Count > 0)
            {
                Geopoint myPoint1 = new Geopoint(new BasicGeoposition()
                {
                    Latitude = result.Locations[0].Point.Position.Latitude,
                    Longitude = result.Locations[0].Point.Position.Longitude
                });
                MapIcon myPOI1 = new MapIcon { Location = myPoint1, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "My position", ZIndex = 0 };
                LocationMap.MapElements.Add(myPOI1);
                LocationMap.Center = myPoint1;
                LocationMap.ZoomLevel = 10;

            }
            else
            {
                Geopoint myPoint1 = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 0,
                    Longitude = 0
                });
                MapIcon myPOI1 = new MapIcon { Location = myPoint1, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "My position", ZIndex = 0 };
                LocationMap.MapElements.Add(myPOI1);
                LocationMap.Center = myPoint1;
                LocationMap.ZoomLevel = 10;
            }


        }
    }
}
