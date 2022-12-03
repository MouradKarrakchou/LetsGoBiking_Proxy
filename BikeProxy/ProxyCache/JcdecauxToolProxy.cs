using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// HttpClient is in the System.web.Http namespace.
using System.Net.Http;
// To use JsonSerializer, you need to add System.Text.Json. Depending on your project type and version, you may find it as assembly reference or Nuget package
// (right-click on the project --> (add --> Reference) or Manage NuGet packages).
// GeoCordinates is in the System.Device.Location namespace, coming from System.Device which is an assembly reference.
using System.Device.Location;
using System.Text.Json;
using Newtonsoft.Json.Serialization;
//
namespace ProxyCache
{
    internal class JcdecauxToolProxy
    {
        public string apiKey = "11391f7ad0ab1a292d6bdebdb53137816422178a";
        string query, url, response;

        public JcdecauxToolProxy()
        {
        }



        public List<JCDContract> getAllContracts()
        {
            query = "apiKey=" + apiKey;
            url = "https://api.jcdecaux.com/vls/v3/contracts";
            response = JCDecauxAPICall(url, query).Result;
            List<JCDContract> allContracts = JsonSerializer.Deserialize<List<JCDContract>>(response);
            return (allContracts);
        }
        public List<JCDStation> getStations(string contract)
        {
            url = "https://api.jcdecaux.com/vls/v3/stations";
            query = "contract=" + contract + "&apiKey=" + apiKey;
            response = JCDecauxAPICall(url, query).Result;
            List<JCDStation> allContracts = JsonSerializer.Deserialize<List<JCDStation>>(response);
            return (allContracts);
        }

        static async Task<string> JCDecauxAPICall(string url, string query)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url + "?" + query);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

    }

    public class JCDContract
    {
        public string name { get; set; }
        public string[] cities { get; set; }
    }

    public class JCDStation
    {
        public int number { get; set; }
        public string name { get; set; }
        public Position position { get; set; }

        internal GeoCoordinate getGeoCoord()
        {
            return new GeoCoordinate(position.latitude, position.longitude);
        }
    }

    public class Position
    {
        public Double latitude { get; set; }
        public Double longitude { get; set; }
    }
}