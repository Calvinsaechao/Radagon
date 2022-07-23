using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radagon.RevitUtilities
{
    internal class FamilySymbolManager
    {
        public IList<FamilySymbol> familySymbols;

        public FamilySymbolManager(Document document)
        {
            initFamilySymbols(document);
        }

        private void initFamilySymbols(Document document)
        {
            FilteredElementCollector collector = new FilteredElementCollector(document);
            collector.OfClass(typeof(FamilySymbol));
            collector.OfCategory(BuiltInCategory.OST_TitleBlocks);
            IList<Element> fs = collector.ToElements();
            familySymbols = fs.Cast<FamilySymbol>().ToList();
        }
    }
}
