using System;
using System.Collections.Generic;
using System.IO;
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

            List<string> lines = new List<string>();
            using (FileStream readFs = new FileStream(virtualWorldFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(readFs))
                {
                    while (sr.Peek() >= 0) // reading the live data if exists
                    {
                        string str = sr.ReadLine();
                        if (str != null)
                        {
                            lines.Add(str);
                        }
                    }
                }
            }

            string[] fileLines = lines.ToArray();

            DataProvider dataProvider = new DataProvider();
            var resources = dataProvider.GetResources(resourceFilePath);
            var virtualWorld = dataProvider.GetVirtualWorld(virtualWorldFilePath, resources);
        }
    }
}
