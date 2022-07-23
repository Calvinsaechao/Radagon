namespace Radagon.Commands
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.Attributes;
    using System.Linq;
    using Radagon.Forms;
    using ExcelDataReader;
    using System.IO;
    using System.Data;
    using System.Collections.Generic;
    using Radagon.RevitUtilities;
    using System;

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class CreateSheetsCommand : IExternalCommand
    {
        UIApplication uiapp;
        UIDocument uidoc;
        Document document;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            document = uidoc.Document;
            Transaction transaction = new Transaction(document);
            string fileExtension = ".xlsx";

            //Select xlsx file that contains the correct information
            SelectFileForm selectFileForm = new SelectFileForm(fileExtension);
            selectFileForm.ShowDialog();
            if (selectFileForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return Result.Cancelled;

            //Select a Family Symbol to be used for all sheets.
            SelectFamilySymbolForm selectFamilySymbolForm = new SelectFamilySymbolForm(document);
            selectFamilySymbolForm.ShowDialog();
            if (selectFamilySymbolForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return Result.Cancelled;
            FamilySymbol familySymbol = selectFamilySymbolForm.familySymbol;

            //Setup ViewSheetManager
            ViewSheetManager viewSheetManager = new ViewSheetManager(document);

            //Create the new sheets
            transaction.Start("New Sheet");
            try
            {
                if (viewSheetManager.createSheets(selectFileForm.filePath, familySymbol, ref message) == null)
                    return Result.Failed;
            }
            catch (Exception e)
            {
                message += e.Message;
                return Result.Failed;
            }
            //createSheets(selectFileForm.filePath, familySymbol, document);
            transaction.Commit();
            return Result.Succeeded;
        }
        /// <summary>
        /// Create sheets from a excel file and sets the custom project parameter DISCIPLINE
        /// </summary>
        /// <param name="filePath">Path to excel file.</param>
        /// <param name="document">Revit document that will contain the new sheets created.</param>
        public void createSheets(string filePath, FamilySymbol fs, Document document)
        {
            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            var results = reader.AsDataSet();
            var tables = results.Tables.Cast<DataTable>();
            var rows = tables.ElementAt<DataTable>(0).Rows;

            foreach (DataRow row in rows)
            {
                string sheetNumber = "", sheetName = "", discipline = "", viewPlanName = "";
                if (!(row[0].ToString().ToLowerInvariant() == "sheet number"))
                {
                    sheetNumber =row[0].ToString();
                    sheetName = row[1].ToString();
                    discipline = row[2].ToString();
                    viewPlanName = row[3].ToString();
                    ViewSheet sheet = createSheet(sheetNumber, sheetName, discipline, fs, document);
                    ViewPlanManager viewPlanManager = new ViewPlanManager(document);
                    ViewPlan viewPlan = viewPlanManager.getViewPlan(viewPlanName);
                    if (viewPlanName != "" && viewPlan != null)
                    {
                        Viewport.Create(document, sheet.Id, viewPlan.Id, new XYZ(1.58, 1.32, 0));
                    }
                }
            }
        }
        /// <summary>
        /// Create a sheet in a document and sets the custom project parameter DISCIPLINE
        /// </summary>
        /// <param name="sheetNumber">Sheet number</param>
        /// <param name="sheetName">Sheet name</param>
        /// <param name="discipline">Discipline</param>
        /// <param name="document">Document to create the new sheet in.</param>
        /// <returns></returns>
        public ViewSheet createSheet(string sheetNumber, string sheetName, string discipline, FamilySymbol fs, Document document)
        {
            ViewSheet newSheet = ViewSheet.Create(document, fs.Id);
            newSheet.SheetNumber = sheetNumber;
            newSheet.Name = sheetName;
            ParameterSet set = newSheet.Parameters;
            foreach (Parameter param in set)
            {
                string name = param.Definition.Name.ToLower();
                if (name.Equals("discipline"))
                {
                    param.Set(discipline);
                    break;
                }
            }
            return newSheet;
        }
    }
}