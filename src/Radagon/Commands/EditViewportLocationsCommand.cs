namespace Radagon.Commands
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.Attributes;
    using System.Linq;
    using Radagon.RevitUtilities;
    using Radagon.Forms;
    using System.Collections.Generic;
    using System;

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class EditViewportLocationsCommand : IExternalCommand
    {
        /// <summary>
        /// Will error if a view sheet contains more than one view plan/viewport but will not error if a view sheet does not contain any view plans/viewports
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document document = uidoc.Document;
            Transaction trans = new Transaction(document);

            //Select file
            SelectFileForm selectFileForm = new SelectFileForm(".xlsx");
            selectFileForm.ShowDialog();
            if (selectFileForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return Result.Cancelled;

            ViewSheetManager viewSheetsManager = new ViewSheetManager(document);
            IList<ViewSheet> viewSheets = viewSheetsManager.getViewSheets(selectFileForm.filePath);
            IList<ViewSheet> finalViewSheets = new List<ViewSheet>();
            IList<Viewport> viewports = new List<Viewport>();

            foreach(ViewSheet viewSheet in viewSheets)
            {
                Boolean viewportFound = false;
                foreach (Element element in viewSheetsManager.getElementsOnSheet(viewSheet))
                {
                    if (element is Viewport)
                    {
                        if (viewportFound)
                        {
                            message += "Each view sheet on file should only contain a single view plan/viewport. ";
                            return Result.Failed;
                        }
                        viewports.Add(element as Viewport);
                        viewportFound = true;
                    }
                }
                if (viewportFound)
                    finalViewSheets.Add(viewSheet);

            }

            trans.Start("edit viewport locations");
            EditViewportLocationsForm editViewportLocationsForm = new EditViewportLocationsForm(document, finalViewSheets, viewports);
            editViewportLocationsForm.ShowDialog();
            if (editViewportLocationsForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return Result.Cancelled;
            trans.Commit();
            return Result.Succeeded;
        }
    }
}
