using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            try{
                if(Regex.IsMatch(args[0], @"^[a-zA-Z]+$")){

                    Task<int> result; 

                    if(args[1] == "offline")
                    {
                        OfflineCityBikeDataFetcher fetcher = new OfflineCityBikeDataFetcher();
                        result = fetcher.GetBikeCountInStation(args[0]);
                        result.Wait();
                    }
                    else 
                    {
                        RealTimeCityBikeDataFetcher fetcher = new RealTimeCityBikeDataFetcher();
                        result = fetcher.GetBikeCountInStation(args[0]);
                        result.Wait();
                    }
                    
                    if(result.Result == -1)
                    {
                        throw new NotFoundException();
                    }
                    else
                    {
                        Console.WriteLine(args[0] + ": " + result.Result);
                    }
                }
                else 
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid argument: " + args[0]);
            }
            catch (NotFoundException)
            {
                Console.WriteLine("Not found: " + args[0]);
            }
        }
    }
}
