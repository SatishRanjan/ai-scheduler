using System;
using System.Collections.Generic;
using System.Text;

namespace ai_scheduler.src.models
{
    public class VirtualResource
    {
        /// <summary>
        /// Gets or sets the name of the resource
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets and sets the weight of the resource
        /// </summary>
        public double Weight 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The flag indicating if a resource is renewable
        /// </summary>
        public bool IsRenewable 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// The flag indicating if a resource is the waste
        /// </summary>
        public bool IsWaste 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Indicates if a resource is transferrable
        /// </summary>
        public bool IsTransferrable 
        { 
            get; 
            set; 
        }
    }
}
