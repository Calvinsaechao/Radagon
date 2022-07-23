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
    internal class EditSelectedViewportLocationsCommand : IExternalCommand
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

            SelectViewSheetsForm selectViewSheetsForm = new SelectViewSheetsForm(document);
            selectViewSheetsForm.ShowDialog();
            if (selectViewSheetsForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return Result.Cancelled;
            else if (selectViewSheetsForm.DialogResult == System.Windows.Forms.DialogResult.Abort)
            {
                message += "Document does not contain any view sheets.";
                return Result.Failed;
            }

            trans.Start("edit viewport locations");
            EditSelectedViewportLocationsForm editSelectedViewportLocationsForm = new EditSelectedViewportLocationsForm(selectViewSheetsForm.viewSheets, document);
            editSelectedViewportLocationsForm.ShowDialog();
            trans.Commit();
            return Result.Succeeded;
        }
    }
}
