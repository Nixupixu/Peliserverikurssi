using System;
using System.Threading.Tasks;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            RealTimeCityBikeDataFetcher fetcher = new RealTimeCityBikeDataFetcher();
            Task<int> result = fetcher.GetBikeCountInStation(args[0]);
            result.Wait();
            Console.WriteLine(result.Result);
        }
    }
}
