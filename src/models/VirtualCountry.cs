using System;
using System.Collections.Generic;
using System.Text;

namespace ai_scheduler.src.models
{
    /// <summary>
    /// VirtualCoutry consisting of CountryName and a list of VirtualResourceAndQuantity
    /// </summary>
    public class VirtualCountry
    {
        // Initialize and empty VirtualResourceAndQuantity list
        public VirtualCountry()
        {
            ResourcesAndQunatities = new List<VirtualResourceAndQuantity>();
        }
        /// <summary>
        /// Returns the name of the country
        /// </summary>
        public string CountryName
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the list of virtual resources and it's quantities for the country
        /// </summary>
        public List<VirtualResourceAndQuantity> ResourcesAndQunatities
        {
            get;
            set;
        }
    }
}
