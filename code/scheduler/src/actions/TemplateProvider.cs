using System;
using System.Collections.Generic;
using System.Text;

namespace ai_scheduler.src.actions
{
    public class TemplateProvider
    {
        public static List<TemplateBase> GetTemplates()
        {
            List<TemplateBase> templates = new List<TemplateBase>();
            templates.Add(new AlloyTransformTemplate());
            templates.Add(new ElectronicsTransformTemplate());
            templates.Add(new HousingTransformTemplate());
            templates.Add(new TransferTemplate());
            return templates;
        }
    }
}
