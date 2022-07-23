namespace Radagon.RevitUtilities
{
    using Autodesk.Revit.DB;
    using ExcelDataReader;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    internal class ViewSheetManager
    {
        public IList<ViewSheet> viewSheets;
        private Document document;

        public ViewSheetManager(Document document)
        {
            this.document = document;
            ElementClassFilter viewSheetFilter = new ElementClassFilter(typeof(ViewSheet));
            FilteredElementCollector collector = new FilteredElementCollector(document);
            IList<Element> CollectorViewSheets = collector.WherePasses(viewSheetFilter).ToElements();
            viewSheets = CollectorViewSheets.Cast<ViewSheet>().ToList();
            sortBySheetNumber();
        }
        private void sortBySheetNumber()
        {
            viewSheets = viewSheets.OrderBy(sheet => sheet.SheetNumber).ToList<ViewSheet>();
        }

        /// <summary>
        /// Create sheets from a xlsx file with a custom project parameter DISCIPLINE in the assigned document of this object
        /// </summary>
        /// <param name="filePath">Path to excel file.</param>
        /// <param name="familySymbol">Revit FamilySymbol</param>
        /// <returns>List of created view sheets</returns>
        public IList<ViewSheet> createSheets(string filePath, FamilySymbol familySymbol, ref string message)
        {
            IList<ViewSheet> sheets = new List<ViewSheet>();
            DataRowCollection rows;
            //Verify file extension
            if (!filePath.EndsWith(".xlsx"))
            {
                message += "File does not end with extension .xlsx.";
                return null;
            }
            try
            {
                var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                var reader = ExcelReaderFactory.CreateReader(stream);
                var results = reader.AsDataSet();
                var tables = results.Tables.Cast<DataTable>();
                rows = tables.ElementAt<DataTable>(0).Rows;
            }
            catch {throw; }

            //Verify xlsx columns
            if (rows[0][0].ToString().ToLower() != "sheet number" &&
                rows[0][1].ToString().ToLower() != "sheet name" &&
                rows[0][2].ToString().ToLower() != "discipline" &&
                rows[0][3].ToString().ToLower() != "view plan")
            {
                message += "File does not contain the correct columns of Sheet Number, Sheet Name, Discipline, and View Plan from indexes A1:D1";
                return null;
            }
            rows.RemoveAt(0);
            foreach (DataRow row in rows)
            {
                string sheetNumber = row[0].ToString();
                string sheetName = row[1].ToString();
                string discipline = row[2].ToString();
                string viewPlanName = row[3].ToString();
                ViewSheet viewSheet = createSheet(sheetNumber, sheetName, discipline, familySymbol, document);
                ViewPlanManager viewPlanManager = new ViewPlanManager(document);
                ViewPlan viewPlan = viewPlanManager.getViewPlan(viewPlanName);
                if (viewPlanName != "" && viewPlan != null)
                {
                    Viewport.Create(document, viewSheet.Id, viewPlan.Id, new XYZ(1.58, 1.32, 0));
                }
                sheets.Add(viewSheet);
            }
            return sheets;
        }
        /// <summary>
        /// Finds all view sheets described by a xlsx file by Sheet Number and Sheet Name in this object's viewSheets list
        /// </summary>
        /// <param name="filePath">Path to a xlsx file containing the approriate columns</param>
        /// <returns>returns null if a view sheet is not found or file is not valid</returns>
        public IList<ViewSheet> getViewSheets(string filePath)
        {
            IList<ViewSheet> result = new List<ViewSheet>();
            IList<Dictionary<string, string>> viewSheetsDictionary = getViewSheetsDictionary(filePath);
            if (viewSheetsDictionary != null)
            {
                foreach (var viewSheet in viewSheetsDictionary)
                {
                    ViewSheet viewSheetFound = findViewSheet(viewSheet["sheetNumber"], viewSheet["sheetName"]);
                    if (viewSheetFound != null)
                        result.Add(viewSheetFound);
                    else
                        return null;
                }
            }
            else return null;
            return result;
        }
        /// <summary>
        /// Finds all elements owned by a viewSheet
        /// </summary>
        /// <param name="viewSheet"></param>
        /// <returns></returns>
        public IList<Element> getElementsOnSheet(ViewSheet viewSheet)
        {
            IList<Element> result = new List<Element>();

            foreach (Element e in new FilteredElementCollector(document).OwnedByView(viewSheet.Id))
                result.Add(e);
            return result;
        }
        /// <summary>
        /// Create a sheet in a document and sets the custom project parameter DISCIPLINE
        /// </summary>
        /// <param name="sheetNumber">Sheet number</param>
        /// <param name="sheetName">Sheet name</param>
        /// <param name="discipline">Discipline</param>
        /// <param name="familySymbol">Revit FamilySymbol</param>
        /// <param name="document">Document to create the new sheet in.</param>
        /// <returns></returns>
        private ViewSheet createSheet(string sheetNumber, string sheetName, string discipline, FamilySymbol familySymbol, Document document)
        {
            ViewSheet newSheet = ViewSheet.Create(document, familySymbol.Id);
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
        /// <summary>
        /// Looks for a view sheet in this object's viewSheet list with the parameters provided
        /// </summary>
        /// <param name="sheetNumber"></param>
        /// <param name="sheetName"></param>
        /// <returns>null if view sheet is not found</returns>
        private ViewSheet findViewSheet(string sheetNumber, string sheetName )
        {
            foreach(var viewSheet in viewSheets)
            {
                if (viewSheet.SheetNumber.Equals(sheetNumber) &&
                    viewSheet.Name.Equals(sheetName))
                {
                    return viewSheet;
                }
            }
            return null;
        }
        /// <summary>
        /// Creates a dictionary describing each ViewSheet that is in a provided xlsx file
        /// </summary>
        /// <param name="filePath">A file path to a xlsx file describing view sheets</param>
        /// <returns>null if not xlsx file or xlsx file does not contain the appropriate columns</returns>
        private List<Dictionary<string, string>> getViewSheetsDictionary(string filePath)
        {
            //Verify file extension
            if (!filePath.EndsWith(".xlsx"))
                return null;

            List<Dictionary<string,string>> viewSheetsDictionary = new List<Dictionary<string,string>>();
            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            var results = reader.AsDataSet();
            var tables = results.Tables.Cast<DataTable>();
            var rows = tables.ElementAt<DataTable>(0).Rows;

            //Verify xlsx columns
            if (rows[0][0].ToString().ToLower() != "sheet number" &&
                rows[0][1].ToString().ToLower() != "sheet name" &&
                rows[0][2].ToString().ToLower() != "discipline" &&
                rows[0][3].ToString().ToLower() != "view plan")
                return null;

           
            foreach (DataRow row in rows.Cast<DataRow>().Skip(1))
            {
                string sheetNumber = row[0].ToString();
                string sheetName = row[1].ToString();
                string discipline = row[2].ToString();
                string viewPlanName = row[3].ToString();
                viewSheetsDictionary.Add(toDictionary(sheetNumber, sheetName, discipline, viewPlanName));
            }

            return viewSheetsDictionary;
        }
        /// <summary>
        /// Creates a dictionary given a view sheet's parameters
        /// </summary>
        /// <param name="sheetNumber"></param>
        /// <param name="sheetName"></param>
        /// <param name="discipline"></param>
        /// <param name="viewPlanName"></param>
        /// <returns>Dictionary containing view sheet parameters</returns>
        private Dictionary<string, string> toDictionary(string sheetNumber, string sheetName, string discipline, string viewPlanName)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("sheetNumber", sheetNumber);
            result.Add("sheetName", sheetName);
            result.Add("discipline", discipline);
            result.Add("viewPlanName", viewPlanName);
            return result;
        }
    }
}
