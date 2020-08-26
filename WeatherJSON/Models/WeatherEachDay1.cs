﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WeatherJSON.Models
{
    public class WeatherEachDay1
    {
        public class Headline
        {
            public string EffectiveDate { get; set; }

            public int EffectiveEpochDate { get; set; }

            public int Severity { get; set; }

            public string Text { get; set; }

            public string Category { get; set; }

            public string EndDate { get; set; }

            public int EndEpochDate { get; set; }

            public string MobileLink { get; set; }

            public string Link { get; set; }
        }
        public class Minimum
        {
            public double Value { get; set; }

            public string Unit { get; set; }

            public int UnitType { get; set; }
        }

        public class Maximum
        {
            public double Value { get; set; }

            public string Unit { get; set; }

            public int UnitType { get; set; }
        }

        public class Tempereture1
        {
            public Minimum Minimum { get; set; }

            public Maximum Maximum { get; set; }
        }

        public class Day
        {
            public string Icon { get; set; }

            public string IconPhrase { get; set; }
        }

        public class DailyForecast1
        {
            public string Date { get; set; }

            public int EpochDate { get; set; }

            public Tempereture1 Tempereture1 { get; set; }

            public Day Day { get; set; }

            public List<string> Sources { get; set; }

            public string MobileLink { get; set; }

            public string Link { get; set; }

            
        }
        public class WeatherEachDay
        {
            public Headline Headline { get; set; }

            public List<DailyForecast1> DailyForecasts { get; set; }
        }

        public async static Task<Models.WeatherEachDay1> GetWeatherEach(string url)
        {
            var http = new HttpClient();
            var response = await http.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Models.WeatherEachDay1));
            var dataStream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var value = serializer.ReadObject(dataStream) as Models.WeatherEachDay1;

            return value;


        }
    }
}
