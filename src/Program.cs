using System;
using ai_scheduler.src;

namespace ai_scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            DataProvider dataProvider = new DataProvider();
            var resources = dataProvider.GetResourcesInfo();
            var virtualWorld = dataProvider.GetcountryAndResourcesFromCsvFiles();
            Console.WriteLine("Hello World!");
        }
    }
}
