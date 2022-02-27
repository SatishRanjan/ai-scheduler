﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ai_scheduler.src.actions;

namespace ai_scheduler.src.models
{
    public class VirtualWorld
    {
        private readonly UtilityCalculator _utilityCalculator = new UtilityCalculator();
        // Initialize an empty list of VirtualCountry
        public VirtualWorld()
        {
            VirtualCountries = new List<VirtualCountry>();
            ApplliedOperationsList = new List<TemplateBase>();
            ScheduleAndItsParticipatingConuntries = new Dictionary<TemplateBase, List<string>>();
        }

        /// <summary>
        /// The reference to the parent state
        /// </summary>
        public VirtualWorld Parent { get; set; }

        /// <summary>
        /// The list of applied templates, aka. schedules
        /// </summary>
        public List<TemplateBase> ApplliedOperationsList { get; set; }

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
            VirtualCountry self = VirtualCountries.Where(c => c.IsSelf).First();
            double expectedUtilityForSelf = _utilityCalculator.CalcExpectedUtilityToACountryInASchedule(this, self.CountryName);
            return expectedUtilityForSelf;
        }

        /// <summary>
        /// Deep clone the state of the world so that the original world state is preserved
        /// </summary>
        /// <returns></returns>
        public VirtualWorld Clone()
        {
            // Create a new VirtualWorld object
            VirtualWorld clonedVirtualWorld = new VirtualWorld();
            clonedVirtualWorld.Parent = this.Parent;

            foreach (TemplateBase schedule in this.ApplliedOperationsList)
            {
                ApplliedOperationsList.Add(schedule);
            }

            foreach(KeyValuePair<TemplateBase, List<string>> schCountry in this.ScheduleAndItsParticipatingConuntries)
            {
                clonedVirtualWorld.ScheduleAndItsParticipatingConuntries.Add(schCountry.Key, schCountry.Value);
            }

            // For each country create the new VirtualCountry and new VirtualResourceAndQuantity
            foreach (VirtualCountry vc in this.VirtualCountries)
            {
                clonedVirtualWorld.VirtualCountries.Add(vc.Clone());
            }

            return clonedVirtualWorld;
        }
    }
}
