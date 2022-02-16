using System;
using System.Collections.Generic;
using System.Text;

namespace ai_scheduler.src.actions
{
    public class TransformTemplateBase: TransformAction
    {
        public void IncreaseYield(int numberOfTimes)
        {
            if (numberOfTimes <= 0 || (INPUTS == null || INPUTS.Count == 0) || (OUTPUTS == null || OUTPUTS.Count == 0))
            {
                return;
            }

            // Increase the INPUTS and OUTPUTS with the numberOfTimes
            foreach (KeyValuePair<string, int> item in INPUTS)
            {
                INPUTS[item.Key] = item.Value * numberOfTimes;
            }

            foreach (KeyValuePair<string, int> item in OUTPUTS)
            {
                OUTPUTS[item.Key] = item.Value * numberOfTimes;
            }
        }
    }
}
