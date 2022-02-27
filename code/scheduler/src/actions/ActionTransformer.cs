using ai_scheduler.src.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ai_scheduler.src.actions
{
    public class ActionTransformer
    {
        public void ApplyTransformTemplate()
        {
        }

        public VirtualWorld ApplyTransferTemplate(VirtualWorld worldState, TransferTemplate transferTemplate)
        {
            VirtualCountry fromCountry = worldState.VirtualCountries.Where(c => string.Equals(c.CountryName, transferTemplate.FromCountry, StringComparison.OrdinalIgnoreCase)).First();
            VirtualCountry toCountry = worldState.VirtualCountries.Where(c => string.Equals(c.CountryName, transferTemplate.ToCountry, StringComparison.OrdinalIgnoreCase)).First();

            // Validate if the from country have the required quantity of the resources to transfer
            // Precondition - if not satisfied return null
            foreach (KeyValuePair<string, int> resourceAndQuantity in transferTemplate.ResourceAndQuantityMapToTransfer)
            {
                // Makes sure that the from country has the resource quantities needed for the transfer
                VirtualResourceAndQuantity fromCountryResourceAndQuantity = fromCountry.ResourcesAndQunatities.Where(rq => string.Equals(rq.VirtualResource.Name, resourceAndQuantity.Key, StringComparison.OrdinalIgnoreCase) && rq.Quantity >= resourceAndQuantity.Value).FirstOrDefault();

                // If there are not enough of either of the resources to transfer there won't be any state change to the world
                // and return true in this case
                if (fromCountryResourceAndQuantity == null)
                {
                    return null;
                }
            }

            // If the preconditions have been satisfied, perform the transfer
            foreach (KeyValuePair<string, int> resourceAndQuantity in transferTemplate.ResourceAndQuantityMapToTransfer)
            {
                // substract the resource quantity from country and add the quantity to the tocountry
                VirtualResourceAndQuantity fromCountryResourceAndQuantity = fromCountry.ResourcesAndQunatities.Where(rq => string.Equals(rq.VirtualResource.Name, resourceAndQuantity.Key, StringComparison.OrdinalIgnoreCase)).First();
                fromCountryResourceAndQuantity.Quantity = fromCountryResourceAndQuantity.Quantity - resourceAndQuantity.Value;

                VirtualResourceAndQuantity toCountryVirtualResourceAndQuantity = toCountry.ResourcesAndQunatities.Where(rq => string.Equals(rq.VirtualResource.Name, resourceAndQuantity.Key, StringComparison.OrdinalIgnoreCase)).First();
                toCountryVirtualResourceAndQuantity.Quantity = toCountryVirtualResourceAndQuantity.Quantity + resourceAndQuantity.Value;
            }

            // Keep track of the operation that changed this state of the world, and the participating countries
            // This forms the data for finding the list of the participating countries in a schedule
            worldState.ScheduleAndItsParticipatingConuntries.Add(transferTemplate, new List<string> { fromCountry.CountryName, toCountry.CountryName });
            return worldState;
        }
    }
}
