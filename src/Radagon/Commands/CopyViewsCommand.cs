
namespace Radagon.Commands
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.Attributes;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using Radagon.Forms;

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CopyViewsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Document source = null;
                Document destination = null;
                ICollection<ElementId> ids = new List<ElementId>();
                List<List<ElementId>> parentAndDependents = new List<List<ElementId>>();
                ICollection<ElementId> independentViews = new List<ElementId>();
                ICollection<ElementId> dependentViews = new List<ElementId>();
                List<int> parentViewsIndex = new List<int>();
                IList<View> views = null;

                //Get source and target document;
                try { GetSourceAndDestinationDocument(commandData, ref source, ref destination); }
                catch (Exception e) 
                { 
                    message = e.Message;
                    if (message == "Cancelled") return Result.Succeeded;
                    return Result.Cancelled;
                }

                //Create filter for view plans
                ElementClassFilter viewsFilter = new ElementClassFilter(typeof(ViewPlan));

                //Create filter for Levels
                ElementClassFilter levelFilter = new ElementClassFilter(typeof(Level));

                //Get all views from document
                try 
                { 
                    SelectViewPlansForm viewPlansForm = new SelectViewPlansForm(source);
                    viewPlansForm.ShowDialog();
                    if (viewPlansForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                        throw new Exception("Cancelled");
                    views = viewPlansForm.viewList;
                }
                catch (Exception e) { message = e.Message; return Result.Succeeded; }

                //Get all Levels from document
                FilteredElementCollector collector = new FilteredElementCollector(source);
                IList<Element> levels = collector.OfClass(typeof(Level)).ToElements();
                ICollection<ElementId> levelIds = new List<ElementId>();
                foreach(Element level in levels)
                    levelIds.Add(level.Id);

                //Populate independentViews list
                foreach(View view in views)
                {
                    if (view.GetPrimaryViewId().Equals(ElementId.InvalidElementId))
                        independentViews.Add(view.Id);
                    else dependentViews.Add(view.Id);
                }

                Transaction targetTrans = new Transaction(destination);
                targetTrans.Start("copyViews");
                //Copy levels
                ElementTransformUtils.CopyElements(source, levelIds, destination, null, new CopyPasteOptions());
                //Copy independent views
                ICollection<ElementId> newIndependentViews = ElementTransformUtils.CopyElements(source, independentViews, destination, null, new CopyPasteOptions());
                ICollection<ElementId> newDependentViews = new List<ElementId>();
                //Rename copied independent views to original names
                for(int i = 0; i < newIndependentViews.Count; i++)
                {
                    destination.GetElement(newIndependentViews.ToList()[i]).Name = source.GetElement(independentViews.ToList()[i]).Name;
                }
                //Copy dependent views
                foreach(ElementId elementId in dependentViews)
                {
                    View childView = source.GetElement(elementId) as View;
                    ElementId parentViewId = childView.GetPrimaryViewId();
                    int indexOfNewParent = independentViews.ToList().IndexOf(parentViewId);
                    View newParent = destination.GetElement(newIndependentViews.ToList()[indexOfNewParent]) as View;
                    View newChild = CreateDependentCopy(newParent);
                    newChild.CropBox = childView.CropBox;
                    newChild.Name = childView.Name;
                    newDependentViews.Add(newChild.Id);
                }
                //Create one list of all new view plans
                IList<View> newViewPlans = new List<View>();
                foreach(ElementId elementId in newIndependentViews)
                    newViewPlans.Add(destination.GetElement(elementId) as View);
                foreach(ElementId elementId in newDependentViews)
                    newViewPlans.Add(destination.GetElement(elementId) as View);
                //Get and apply settings to new view plans
                EditViewPlansForm editViewPlansForm = new EditViewPlansForm(destination, newViewPlans);
                editViewPlansForm.ShowDialog();
                targetTrans.Commit();
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        public View CreateDependentCopy(View view)
        {
            View dependentView = null;
            ElementId newViewId = ElementId.InvalidElementId;
            if (view.CanViewBeDuplicated(ViewDuplicateOption.AsDependent))
            {
                newViewId = view.Duplicate(ViewDuplicateOption.AsDependent);
                dependentView = view.Document.GetElement(newViewId) as View;
            }

            return dependentView;
        }

        /*
         * Select Source and Destination document with a Windows Form
         */
        private static void GetSourceAndDestinationDocument(ExternalCommandData commandData, ref Document source, ref Document destination)
        {
            SelectSrcAndDesDocForm form = new SelectSrcAndDesDocForm(commandData);
            form.ShowDialog();
            if (form.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                throw new Exception("Cancelled");
            else if (form.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                source = form.source;
                destination = form.destination;
            }
            else if (form.DialogResult == System.Windows.Forms.DialogResult.Abort)
            {
                throw new Exception("Aborting! No documents selected for source or destination.");
            }
            else
                throw new Exception("Error");
        }
    }
}
