using System;
using System.Collections.Generic;
using System.Text;
using ai_scheduler.src.actions;
namespace ai_scheduler.src.models
{
    public class VirtualWorld
    {
        // Initialize an empty list of VirtualCountry
        public VirtualWorld()
        {
            VirtualCountries = new List<VirtualCountry>();
            ScheduleList = new List<TemplateBase>();
            ScheduleAndItsParticipatingConuntries = new Dictionary<TemplateBase, List<string>>();
        }

        /// <summary>
        /// The reference to the parent state
        /// </summary>
        public VirtualWorld Parent { get; set; }

        /// <summary>
        /// The list of applied templates, aka. schedules
        /// </summary>
        public List<TemplateBase> ScheduleList { get; set; }

        /// <summary>
        /// The schedule to participating country mapping
        /// </summary>
        public Dictionary<TemplateBase, List<string>> ScheduleAndItsParticipatingConuntries { get; private set; }
        
        /// <summary>
        /// A VirtualWorld consisting of a list of VirtualCountry
        /// </summary>
        public List<VirtualCountry> VirtualCountries { get; set; }

        public double ExpectedUtilityForSelf()
        {
            return new Random().NextDouble();
        }

        /// <summary>
        /// Deep clone the state of the world so that the original world state is preserved
        /// </summary>
        /// <returns></returns>
        public VirtualWorld Clone()
        {
            // Create a new VirtualWorld object
            VirtualWorld clonedVirtualWorld = new VirtualWorld();

            foreach (TemplateBase schedule in this.ScheduleList)
            {
                ScheduleList.Add(schedule);
            }

            // For each country create the new VirtualCountry and new VirtualResourceAndQuantity
            foreach (VirtualCountry vc in this.VirtualCountries)
            {
                clonedVirtualWorld.VirtualCountries.Add(vc.Clone());
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
