using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;

namespace TravelApp.Models
{
    public static class Geolocator
    {
        public const double LatitudeHint = 51.0;
        public const double LongitudeHint = 4.0;
        public const string ServiceToken = "1KdhtVBnzG47XhmAM5jw~gRRcuAmD5uOd5vTQCzMdEw~AgpRYMnx8z7FKaoSpjCYiutaPV6SAXj4Qju__oIPWF8XLg4IIQvfj-dpns8CZezd";

        public static async Task<double[]> getCoordinatesAsync(string location)
        {
            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = LatitudeHint;
            queryHint.Longitude = LongitudeHint;
            Geopoint hintPoint = new Geopoint(queryHint);

            //MapService.ServiceToken = ServiceToken;
            MapLocationFinderResult result =
                  await MapLocationFinder.FindLocationsAsync(
                                    location,
                                    hintPoint,
                                    3);

            if (result.Status == MapLocationFinderStatus.Success && result.Locations.Count > 0)
            {
                return new double[] { result.Locations[0].Point.Position.Latitude, result.Locations[0].Point.Position.Longitude};
            }
            else
            {
                return new double[] { 0.0, 0.0 };
            }
        }
    }
}
