namespace Radagon.Commands
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Selection;
    using Autodesk.Revit.Attributes;
    using System.Collections.Generic;
    using System;
    using Radagon.Forms;
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class GetSelectedIdsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Selection sel = uidoc.Selection;
                ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();

                if (selectedIds.Count == 0)
                {
                    TaskDialog.Show("Radagon", "Select elements first! There are no selected elements.");
                }
                else
                {
                    String info = "IDs of selected elements are: ";
                    foreach (ElementId id in selectedIds)
                    {
                        Element element = uidoc.Document.GetElement(id);
                        info += "\n\t" + element.Name + ": " + element.Id;
                    }
                    TaskDialog.Show("Radagon", info);
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
            return Result.Succeeded;
        }
    }
}
