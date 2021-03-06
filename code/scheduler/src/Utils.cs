using System;
using System.Collections.Generic;
using System.Text;

namespace ai_scheduler.src
{
    public enum Operation
    {
        TRANSFORM,
        TRANSFER
    }

    public enum ResourceKind
    {
        Raw,
        Created,
        Waste
    }

    public class Constants
    {
        public const double GAMMA_VALUE = .9;
        public const double X_0_LOGISTICS_FN = 0;
        public const double K_VAL_LOGISTICS_FN = 1;
        public const double C_VAL_FAILURE_COST = .8;

        // The state quality reduction factor for the waste generated by a country
        // If there are more electronic waste created by a country, it'll reduce the overall state quality more
        public const double ELECTRONICS_WASTE_WEIGHT_REDUCTIONFACTOR = .1;
        public const double WASTE_MATERIAL_WEIGHT_REDUCTIONFACTOR = .05;
    }
}
