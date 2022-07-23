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

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class TestFormCommand : IExternalCommand
    {
        UIApplication uiapp;
        UIDocument uidoc;
        Document document;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            document = uidoc.Document;
            FamilySymbolManager familySymbolManager = new FamilySymbolManager(document);
            Transaction transaction = new Transaction(document);
            SelectViewPlansForm selectViewPlansForm = new SelectViewPlansForm(document);
            //selectViewPlansForm.ShowDialog();
            IList<ViewSheet> viewSheets = getViewSheets(document);
            ViewSheet viewSheet = viewSheets[4];
            FamilySymbol familySymbol = null;
            IList<Element> elementsOnViewport = new List<Element>();

            //get all elementsOnViewPort
            foreach (Element e in new FilteredElementCollector(document).OwnedByView(new ElementId(272072)))
            {
                elementsOnViewport.Add(e);
            }

            //find family symbol
            IList<Element> ElementsOnSheet = new List<Element>();

            foreach (Element e in new FilteredElementCollector(document).OwnedByView(viewSheet.Id))
            {
                ElementsOnSheet.Add(e);
            }

            foreach (Element e in ElementsOnSheet)
            {
                foreach (FamilySymbol fs in familySymbolManager.familySymbols)
                {
                    if (e.GetTypeId().IntegerValue == fs.Id.IntegerValue)
                    {
                        familySymbol = fs;
                    }
                }
            }

            IList<ElementId> elementIdsOnSheet = new List<ElementId>();
            foreach (Element e in ElementsOnSheet)
            {
                elementIdsOnSheet.Add(e.Id);
            }

            //FamilyInstance is a instance of a FamilySymbol
            FamilyInstance titleBlock = ElementsOnSheet[1] as FamilyInstance;
            Viewport viewport = ElementsOnSheet[2] as Viewport;
            XYZ viewportLocation = viewport.GetBoxCenter();

            //IList<ElementId> shapeHandles = AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(ElementsOnSheet[1] as FamilyInstance);

            //Reference r = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, "Select element:");
            //Element shapeHandle = document.GetElement(r.ElementId);

            //Create the new sheet
            /*transaction.Start("New Sheet");
            document.Delete(new ElementId(272071));
            Viewport.Create(document, viewSheet.Id, selectViewPlansForm.viewList[1].Id, new XYZ(0, 0, 0));
            transaction.Commit();*/
            return Result.Succeeded;
        }

        List<ViewSheet> getViewSheets(Document source)
        {
            ElementClassFilter viewSheetFilter = new ElementClassFilter(typeof(ViewSheet));
            FilteredElementCollector collector = new FilteredElementCollector(source);
            IList<Element> CollectorViewSheets = collector.WherePasses(viewSheetFilter).ToElements();
            return CollectorViewSheets.Cast<ViewSheet>().ToList();
        }
    }
}
