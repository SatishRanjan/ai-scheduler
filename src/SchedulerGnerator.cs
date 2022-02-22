using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ai_scheduler.src.models;

namespace ai_scheduler.src
{
    public class SchedulerGnerator
    {
        private readonly DataProvider _dataProvider;
        public SchedulerGnerator()
        {
            _dataProvider = new DataProvider();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myCountryName">The name of the country that's labeled as my country</param>
        /// <param name="resourcesFileName">The csv file name containing names, weights and other information related to the resources</param>
        /// <param name="initialWorldStateFileName">The csv file name containing initial state of the world, the resources and it's quantity etc.</param>
        /// <param name="outputScheduleFileName">The file name where the output schedule will be saved</param>
        /// <param name="numOutputSchedules">The ordered list of output schedule to be written to the output schedule file</param>
        /// <param name="depthBound">The maximum depth to search assuming the initial depth is 0</param>
        /// <param name="frontierMaxSize">The priority queue size to limit the size of the frontier and the extent of search</param>
        public void CreateMyCountrySchedule(
            string myCountryName,
            string resourcesFileName,
            string initialWorldStateFileName,
            string outputScheduleFileName,
            uint numOutputSchedules,
            uint depthBound,
            uint frontierMaxSize)
        {
            // Validat the inputs
            ValidateInputs(myCountryName, resourcesFileName, initialWorldStateFileName, outputScheduleFileName);

            // Get the list of virtual resources and initial state of the virtual world from the csv files
            List<VirtualResource> virtualResources = _dataProvider.GetResources(resourcesFileName);
            VirtualWorld virtualWorld = _dataProvider.GetVirtualWorld(initialWorldStateFileName, virtualResources);
        }

        private void ValidateInputs(
            string myCountryName,
            string resourcesFileName,
            string initialWorldStateFileName,
            string outputScheduleFileName)
        {
            // If the my country name is null or empty throw an exception
            if (string.IsNullOrEmpty(myCountryName))
            {
                throw new ArgumentException("My country name cannot be null or empty");
            }
            
            // If the resource file name is null or empty or if the file doesn't exist throw an exception
            if (string.IsNullOrEmpty(resourcesFileName) || !File.Exists(resourcesFileName))
            {
                throw new ArgumentException("The resource file name is null or empty or it doesn't exists");
            }

            // If initial world state file name is null or empty or if the file doesn't exist throw an exception
            if (string.IsNullOrEmpty(initialWorldStateFileName) || !File.Exists(initialWorldStateFileName))
            {
                throw new ArgumentException("The initial world state file is null or empty or it doesn't exists");
            }

            // If output schedule file name is null or empty or if the file doesn't exist throw an exception
            if (string.IsNullOrEmpty(outputScheduleFileName) || !File.Exists(outputScheduleFileName))
            {
                throw new ArgumentException("The output schedule file is null or empty or it doesn't exists");
            }
        }
    }
}
