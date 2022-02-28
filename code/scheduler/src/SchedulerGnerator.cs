using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using ai_scheduler.src.models;
using ai_scheduler.src.actions;

namespace ai_scheduler.src
{
    public class SchedulerGnerator
    {
        private readonly DataProvider _dataProvider;
        private List<VirtualResource> _resources = new List<VirtualResource>();
        private VirtualWorld _initialStateOfWorld = new VirtualWorld();
        private readonly ActionTransformer _actionTransformer = new ActionTransformer();

        public SchedulerGnerator()
        {
            _dataProvider = new DataProvider();
        }

        /// <summary>
        /// Creates the schedule and saves the schedules for my country
        /// </summary>
        /// <param name="myCountryName">The name of the country that's labeled as my country</param>
        /// <param name="resourcesFileName">The csv file name containing names, weights and other information related to the resources</param>
        /// <param name="initialWorldStateFileName">The csv file name containing initial state of the world, the resources and it's quantity etc.</param>
        /// <param name="outputScheduleFileName">The file name where the output schedule will be saved</param>
        /// <param name="numOutputSchedules">The number of ordered list of output schedule to be written to the output schedule file</param>
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
            _resources = _dataProvider.GetResources(resourcesFileName);
            _initialStateOfWorld = _dataProvider.GetVirtualWorld(initialWorldStateFileName, _resources);

            // Set one of the country as self
            VirtualCountry myCountry = null;
            if (!string.IsNullOrEmpty(myCountryName))
            {
                myCountry = _initialStateOfWorld.VirtualCountries.FirstOrDefault(c => string.Equals(c.CountryName, myCountryName, StringComparison.OrdinalIgnoreCase));
            }
            // Else set the first country as the self by default
            else
            {
                myCountry = _initialStateOfWorld.VirtualCountries.First();
            }

            // Set self to my country
            if (myCountry != null)
            {
                myCountry.IsSelf = true;
            }

            // Call the function that generate the schedule
            GenerateSchedules(_initialStateOfWorld, depthBound, frontierMaxSize);
        }

        public void GenerateSchedules(VirtualWorld initialState, uint depthBound, uint frontierMaxSize)
        {
            if (initialState == null || initialState.VirtualCountries.Count == 0)
            {
                throw new ArgumentNullException("The countries with initial state cannot be null or empty");
            }

            // Initialize the solutions as an empty priority queue
            PriorityQueue solutions = new PriorityQueue();

            // Initialize and add the initial world state to the frontier priority queue
            PriorityQueue frontier = new PriorityQueue(frontierMaxSize);
            frontier.Enqueue(initialState);

            while (!frontier.IsEmpty())
            {
                VirtualWorld worldState = frontier.Dequeue();
                // If the schedule list to the world state reaches the search depth bound, add it to the solution priority queue 
                if (worldState.ScheduleAndItsParticipatingConuntries.Count == depthBound)
                {
                    solutions.Enqueue(worldState);
                }
                else
                {
                    List<VirtualWorld> successors = GenerateSuccessors(worldState);
                    if (successors == null || successors.Count == 0)
                    {
                        continue;
                    }

                    foreach (VirtualWorld successor in successors)
                    {
                        frontier.Enqueue(successor);
                    }
                }
            }
        }

        public List<VirtualWorld> GenerateSuccessors(VirtualWorld parentState)
        {
            List<VirtualWorld> successors = new List<VirtualWorld>();
            if (parentState == null)
            {
                return successors;
            }

            VirtualCountry myCountry = parentState.VirtualCountries.Where(c => c.IsSelf).FirstOrDefault();
            List<TemplateBase> templates = TemplateProvider.GetAllTemplates();

            // If either of the created resources are 0, perform the transform to generate the created resources              
            foreach (VirtualCountry vc in parentState.VirtualCountries)
            {
                // Perform the transform to generate the created resource for the countries in the parent state
                VirtualResourceAndQuantity vrq = vc.ResourcesAndQunatities.Where(rq => rq.Quantity == 0 && rq.VirtualResource.Kind == ResourceKind.Created).FirstOrDefault();
                if (vrq != null)
                {
                    TransformTemplate transformTemplate = TemplateProvider.GetTransformTemplate(vrq.VirtualResource.Name);
                    transformTemplate.CountryName = vc.CountryName;
                    transformTemplate.IncreaseYield(5);
                    VirtualWorld clonedState = parentState.Clone();
                    clonedState.Parent = parentState;
                    VirtualWorld successor = _actionTransformer.ApplyTransformTemplate(clonedState, transformTemplate);
                    if (successor != null)
                    {
                        successors.Add(successor);
                    }

                    VirtualCountry countryWithMaxTimber = null;
                    int maxQuantity = 0;

                    // If there is not successor generated, the schedule cannot proceed
                    if (successor == null)
                    {
                        continue;
                    }

                    foreach (VirtualCountry c in successor.VirtualCountries)
                    {
                        if (c.IsSelf)
                        {
                            continue;
                        }

                        int timberQuantity = c.ResourcesAndQunatities.Where(rq => rq.VirtualResource.Name == "Timber").First().Quantity;
                        if (timberQuantity > maxQuantity)
                        {
                            maxQuantity = timberQuantity;
                            countryWithMaxTimber = c;
                        }
                    }

                    // Transfer timber from country with max timber to the self
                    TransferTemplate transferTemplate = TemplateProvider.GetTransferTemplate();
                    transferTemplate.FromCountry = countryWithMaxTimber.CountryName;
                    transferTemplate.ToCountry = myCountry.CountryName;
                    VirtualWorld transferClone1 = clonedState.Clone();
                    transferClone1.Parent = successor;
                    transferTemplate.ResourceAndQuantityMapToTransfer.Add("Timber", 50);
                    VirtualWorld transfer1 = _actionTransformer.ApplyTransferTemplate(transferClone1, transferTemplate);
                    if (transfer1 != null)
                    {
                        successors.Add(transfer1);
                    }

                    // Transfer housing from country with max timber to the self
                    TransferTemplate returnTransferTemplate = TemplateProvider.GetTransferTemplate();
                    returnTransferTemplate.FromCountry = myCountry.CountryName;
                    returnTransferTemplate.ToCountry = countryWithMaxTimber.CountryName;
                    VirtualWorld transferClone2 = transferClone1.Clone();
                    transferClone2.Parent = transferClone1;
                    transferTemplate.ResourceAndQuantityMapToTransfer.Add("Housing", 2);
                    VirtualWorld transfer2 =  _actionTransformer.ApplyTransferTemplate(transferClone2, returnTransferTemplate);
                    if (transfer2 != null)
                    {
                        successors.Add(transfer1);
                    }
                }
            }

           
            /*
            foreach (TemplateBase template in templates)
            {
            }*/

            return successors;
        }

        #region privatemembers
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

        private void PopulateResourceNames(List<VirtualResource> virtualResources)
        {
        }
        #endregion privatemembers
    }
}
