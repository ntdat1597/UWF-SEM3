using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace WeatherAppDemo
{
    class LocationData
    {
        public async static Task<Geoposition> GetGeoposition()
        {
            var accesssStatus = await Geolocator.RequestAccessAsync();
            if (accesssStatus != GeolocationAccessStatus.Allowed) throw new Exception();
            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            var postion = await geolocator.GetGeopositionAsync();
            return postion;
        }
    }
}
