using System;
using System.Collections.Generic;
using System.Text;
using ai_scheduler.src;

namespace ai_scheduler.src.actions
{
    public class TransformAction
    {
        public TransformAction()
        {
            ActionName = Operation.TRANSFORM.ToString();
            INPUTS = new Dictionary<string, int>();
            OUTPUTS = new Dictionary<string, int>();
        }

        public string ActionName { get; private set; }
        public Dictionary<string, int> INPUTS { get; set; }
        public Dictionary<string, int> OUTPUTS { get; set; }
    }
}
