using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radagon.RevitUtilities
{
    internal class ViewPlanManager
    {
        public IList<ViewPlan> viewPlans;
        public IList<ViewPlan> viewPlanTemplates;

        public ViewPlanManager(Document document)
        {
            viewPlanTemplates = new List<ViewPlan>();
            init(document);
        }

        private void init(Document document)
        {
            FilteredElementCollector collector = new FilteredElementCollector(document);
            collector.OfClass(typeof(ViewPlan));
            IList<Element> fs = collector.ToElements();
            viewPlans = fs.Cast<ViewPlan>().ToList();

            for (int i = 0; i < viewPlans.Count; i++)
            {
                ViewPlan view = viewPlans[i];
                if (view.IsTemplate)
                {
                    viewPlanTemplates.Add(view);
                    viewPlans.Remove(view);
                }
            }
        }

        public ViewPlan getViewPlan(string name)
        {
            name = name.ToLower();
            foreach (ViewPlan viewPlan in viewPlans)
            {
                string viewPlanName = viewPlan.Name.ToLower();
                if (viewPlanName.Equals(name))
                {
                    return viewPlan;
                }
            }
            return null;
        }
    }

}
