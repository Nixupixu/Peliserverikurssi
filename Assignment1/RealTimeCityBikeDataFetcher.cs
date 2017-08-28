using System;
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

            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var content = Encoding.UTF8.GetString(byteArray);

            Data stationList = JsonConvert.DeserializeObject<Data>(content);
            
            int index;
            int numberOfBikes = 0;
            for(index = 0; index < stationList.stations.Length; index++)
            {
                if(stationList.stations[index].name == stationName)
                {
                    numberOfBikes = stationList.stations[index].bikesAvailable;
                }
            }

            return await Task.FromResult(numberOfBikes);
        }
    }
}