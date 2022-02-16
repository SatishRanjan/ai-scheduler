using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ai_scheduler.src.models;

namespace ai_scheduler.src
{
    public class DataProvider
    {
        public VirtualWorld GetcountryAndResourcesFromCsvFiles()
        {
            VirtualWorld virtualWorld = new VirtualWorld();
            virtualWorld.VirtualCountries = new List<VirtualCountry>();
            string countryAndResourceFilePath = @"C:\Users\satishra\Dropbox\MyDocsLib\MS-Vanderbilt\0-Courses\cs-5260-Artificial-Intelligence\ai-scheduler\initial_data\country_resource_seeding_data.csv";
            string[] countryResourceFileLines = File.ReadAllLines(countryAndResourceFilePath);
            if (countryResourceFileLines.Length > 1)
            {
                string[] resources = countryResourceFileLines[0].Split(',');
                for (int i = 1; i < countryResourceFileLines.Length; ++i)
                {
                    string[] countryAndResourceQuantities = countryResourceFileLines[i].Split(',');
                    string countryName = countryAndResourceQuantities[0].Trim();
                    VirtualCountry virtualCountry = new VirtualCountry
                    {
                        CountryName = countryName,
                        ResourcesAndQunatities = new List<VirtualResourceAndQuantity>()
                    };

                    for (int j = 1; j < countryAndResourceQuantities.Length; ++j)
                    {
                        VirtualResourceAndQuantity virtualResourceAndQuantity = new VirtualResourceAndQuantity();
                        virtualResourceAndQuantity.VirtualResource = new VirtualResource
                        {
                            Name = resources[j].Trim()
                        };

                        virtualResourceAndQuantity.Quantity = int.Parse(countryAndResourceQuantities[j].Trim());
                        virtualCountry.ResourcesAndQunatities.Add(virtualResourceAndQuantity);
                    }

                    virtualWorld.VirtualCountries.Add(virtualCountry);
                }
            }

            return virtualWorld;
        }

        public List<VirtualResource> GetResourcesInfo()
        {
            List<VirtualResource> resourceInfoList = new List<VirtualResource>();
            string resourceInfoFilePath = @"C:\Users\satishra\Dropbox\MyDocsLib\MS-Vanderbilt\0-Courses\cs-5260-Artificial-Intelligence\ai-scheduler\initial_data\resource_seeding_data.csv";
            string[] resourceInfoLines = File.ReadAllLines(resourceInfoFilePath);
            if (resourceInfoLines.Length > 1)
            {
                VirtualResource vr = null;
                for (int i = 1; i < resourceInfoLines.Length - 1; ++i)
                {
                    string[] eachLines = resourceInfoLines[i].Split(',');
                    vr = new VirtualResource(eachLines[0].Trim(), double.Parse(eachLines[1].Trim()));
                    resourceInfoList.Add(vr);
                }
            }

            return resourceInfoList;
        }


    }
}
