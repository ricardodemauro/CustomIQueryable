using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTerraServerProvider.TerraServerReference
{
    public enum PlaceType
    {
        Unknown,
        AirRailStation,
        BayGulf,
        CapePeninsula,
        CityTown,
        HillMountain,
        Island,
        Lake,
        OtherLandFeature,
        OtherWaterFeature,
        ParkBeach,
        PointOfInterest,
        River
    }

    public class MockTerraServerService
    {
        private readonly string baseUri = "http://samples.openweathermap.org/data/2.5/";
        private readonly string restUri = @"weather?q={0}&appid=b1b15e88fa797225412429c1c50c122a1";

        public PlaceFacts[] GetPlaceList(string location, int numResults, bool mustHaveImage)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                var response = client.GetAsync(string.Format(restUri, location)).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Something went wrong with api call");
                }
                PlaceFacts fact = JsonConvert.DeserializeObject<PlaceFacts>(response.Content.ReadAsStringAsync().Result);

                PlaceFacts[] factsColl = new PlaceFacts[numResults];
                for (int i = 0; i < factsColl.Length; i++)
                {
                    factsColl[i] = fact;
                }
                return factsColl;
            }
        }
    }

    public class PlaceFacts
    {
        [JsonProperty("coord")]
        public Coordinates Coordinate { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        public int Visibility { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Cod { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }

        public string Main { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }
    }
}
