namespace Radagon.Commands
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Radagon.Forms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class DuplicateViewsCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidocument = commandData.Application.ActiveUIDocument;
            Document document = uidocument.Document;
            Transaction transaction = new Transaction(document);
            try
            {
                SelectViewPlansForm selectViewPlansForm = new SelectViewPlansForm(document);
                selectViewPlansForm.ShowDialog();
                if (selectViewPlansForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                    return Result.Cancelled;
                transaction.Start("Duplicate Views");
                DuplicateViewsForm form = new DuplicateViewsForm(document, selectViewPlansForm.viewList);
                form.ShowDialog();
                if (form.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                    return Result.Cancelled;
                EditViewPlansForm editViewPlansForm = new EditViewPlansForm(document, form.duplicatedViews);
                editViewPlansForm.ShowDialog();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return Result.Succeeded;
        }
    }
}
