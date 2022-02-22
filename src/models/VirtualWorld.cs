using System;
using System.Collections.Generic;
using System.Text;

namespace ai_scheduler.src.models
{
    public class VirtualWorld
    {
        // Initialize an empty list of VirtualCountry
        public VirtualWorld()
        {
            VirtualCountries = new List<VirtualCountry>();
        }

        /// <summary>
        /// A VirtualWorld consisting of a list of VirtualCountry
        /// </summary>
        public List<VirtualCountry> VirtualCountries { get; set; }

        /// <summary>
        /// Deep clone the state of the world so that the original world state is preserved
        /// </summary>
        /// <returns></returns>
        public VirtualWorld Clone()
        {
            // Create a new VirtualWorld object
            VirtualWorld clonedVirtualWorld = new VirtualWorld
            {
                VirtualCountries = new List<VirtualCountry>()
            };

            // For each country create the new VirtualCountry and new VirtualResourceAndQuantity
            foreach (VirtualCountry vc in this.VirtualCountries)
            {
                VirtualCountry newVc = new VirtualCountry
                {
                    CountryName = vc.CountryName,
                    ResourcesAndQunatities = new List<VirtualResourceAndQuantity>()
                };

                foreach (VirtualResourceAndQuantity vRQ in vc.ResourcesAndQunatities)
                {
                    VirtualResourceAndQuantity newVrQ = new VirtualResourceAndQuantity
                    {
                        VirtualResource = new VirtualResource
                        {
                            Name = vRQ.VirtualResource.Name,
                            Kind = vRQ.VirtualResource.Kind,
                            Weight = vRQ.VirtualResource.Weight,
                            IsWaste = vRQ.VirtualResource.IsWaste,
                            IsRenewable = vRQ.VirtualResource.IsRenewable,
                            IsTransferrable = vRQ.VirtualResource.IsTransferrable
                        },
                        Quantity = vRQ.Quantity
                    };

                    newVc.ResourcesAndQunatities.Add(newVrQ);
                }

                clonedVirtualWorld.VirtualCountries.Add(newVc);
            }

            return clonedVirtualWorld;
        }

        public void ApplyTransformTemplate()
        {

        }

        public void ApplyTransferTemplate()
        {

        }
    }
}
