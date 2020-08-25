using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WeatherJSON.Models
{
    class WeatherJSON
    {
        public class Temperature
        {
            public string Value { get; set; }

            public string Unit { get; set; }

            public int UnitType { get; set; }
        }

        public class WeatherJSON
        {
            public string DateTime { get; set; }

            public int EpochoDateTime { get; set; }

            public string WeatherIcon { get; set; }

            public string IconPhrase { get; set; }

            public Temperature Temperature { get; set; }

            public int MobilLink { get; set; }

            public int PrecipitationProbability { get; set; }

            public string Link { get; set; }
        }
        public async static Task<List<WeatherJSON>> GetJSON(string url)
        {
            var http = new HttpClient();
            var response = await http.GetAsync(url);
            var resulut = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(List<WeatherJSON>));
            var dataStream = new MemoryStream(Encoding.UTF8.GetBytes(resulut));

            var value = serializer.ReadObject(dataStream) as List<WeatherJSON>;

            return value;
        }

    }
}
