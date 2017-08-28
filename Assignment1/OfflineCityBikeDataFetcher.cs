using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Assignment1
{
    public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string stationName)
        {
            string[] result = File.ReadAllLines(@"C:\Users\rajaniemi\Documents\GitHub\Peliserverikurssi\Assignment1\bikes.txt");
            foreach(string line in result)
            {
                string[] splitLine = line.Split(':');
                //Console.WriteLine("Teksti: " + splitLine[0] + " Numero: " + splitLine[1]);
                if(splitLine[0] == stationName)
                {
                    return await Task.FromResult(Int32.Parse(splitLine[1]));
                }
            }
            return await Task.FromResult(-1);
        }
    }
}