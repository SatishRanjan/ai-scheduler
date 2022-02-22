using System;
using ai_scheduler.src;

namespace ai_scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            string resourceFilePath = @"C:\Users\satishra\Dropbox\MyDocsLib\MS-Vanderbilt\0-Courses\cs-5260-Artificial-Intelligence\ai-scheduler\initial_data\resource_seeding_data.csv";
            string virtualWorldFilePath = @"C:\Users\satishra\Dropbox\MyDocsLib\MS-Vanderbilt\0-Courses\cs-5260-Artificial-Intelligence\ai-scheduler\initial_data\country_resource_seeding_data.csv";
            if (args != null && args.Length > 1)
            {
                resourceFilePath = args[0];
                virtualWorldFilePath = args[1];
            }

            DataProvider dataProvider = new DataProvider();
            var resources = dataProvider.GetResources(resourceFilePath);
            var virtualWorld = dataProvider.GetVirtualWorld(virtualWorldFilePath, resources);
        }
    }
}
