using ai_scheduler.src.models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ai_scheduler.src
{
    public class UtilityCalculator
    {
        public double CalculateCountryStateQuality(VirtualCountry c)
        {
            if (c == null || c.ResourcesAndQunatities.Count == 0)
            {
                return 0;
            }

            // Get the population count for the country
            int populationCount = c.ResourcesAndQunatities.Where(rq => string.Equals(rq.VirtualResource.Name, "Population", StringComparison.OrdinalIgnoreCase)).First().Quantity;
            double totalCreatedResourceWeightTimesQuantity = 0.0;
            double totalWasteResourcesWeightTimesQuantity = 0.0;
            double totalRawResourceWeight = 0.0;

            foreach (VirtualResourceAndQuantity vrq in c.ResourcesAndQunatities)
            {
                if (vrq.VirtualResource.Kind == ResourceKind.Created && vrq.Quantity > 0)
                {
                    totalCreatedResourceWeightTimesQuantity = totalCreatedResourceWeightTimesQuantity + vrq.VirtualResource.Weight * vrq.Quantity;
                }
                else if (vrq.VirtualResource.Kind == ResourceKind.Waste && vrq.Quantity > 0)
                {
                    double wasteWeightFactor = Constants.WASTE_MATERIAL_WEIGHT_REDUCTIONFACTOR;
                    if (string.Equals(vrq.VirtualResource.Name, "ElectronicsWaste", StringComparison.OrdinalIgnoreCase))
                    {
                        wasteWeightFactor = Constants.ELECTRONICS_WASTE_WEIGHT_REDUCTIONFACTOR;
                    }

                    totalWasteResourcesWeightTimesQuantity = totalWasteResourcesWeightTimesQuantity + vrq.VirtualResource.Weight * wasteWeightFactor * vrq.Quantity;
                }
                else
                {
                    totalRawResourceWeight = totalRawResourceWeight + vrq.Quantity;
                }
            }

            double balancedOutWeight = totalCreatedResourceWeightTimesQuantity + totalWasteResourcesWeightTimesQuantity;
            // If there are not created resources or waste resources that means it's initial state, and hence the balancedOutWeight will be the weight of the raw resources
            if (totalCreatedResourceWeightTimesQuantity == 0.0 && totalWasteResourcesWeightTimesQuantity == 0.0)
            {
                balancedOutWeight = totalRawResourceWeight;
            }

            // Normalize the balanced out weight with respect to the population
            double stateQuality = balancedOutWeight / populationCount;
            return stateQuality;
        }
    }
}
