using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Assignment1
{
    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string stationName)
        {
            HttpClient httpClient = new HttpClient();
            
            string apiAddress = "http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental";
                        
            HttpResponseMessage response = await httpClient.GetAsync(apiAddress);

            byte[] byteArray = response.Content.ReadAsByteArrayAsync().Result;
            string content = Encoding.UTF8.GetString(byteArray);

            Data stationList = JsonConvert.DeserializeObject<Data>(content);
            
            foreach(Station station in stationList.stations)
            {
                if(station.name == stationName)
                {
                    return await Task.FromResult(station.bikesAvailable);
                }
            }

            return await Task.FromResult(-1);            
        }
    }
}