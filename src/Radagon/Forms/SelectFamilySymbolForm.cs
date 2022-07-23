using Autodesk.Revit.DB;
using Radagon.RevitUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Radagon.Forms
{
    public partial class SelectFamilySymbolForm : System.Windows.Forms.Form
    {
        public FamilySymbol familySymbol;
        private FamilySymbolManager fsm;
        public SelectFamilySymbolForm(Document document)
        {
            InitializeComponent();
            fsm = new FamilySymbolManager(document);
            foreach(FamilySymbol symbol in fsm.familySymbols)
            {
                FamilySymbolComboBox.Items.Add(symbol.Name);
            }
            if (FamilySymbolComboBox.Items.Count != 0)
                FamilySymbolComboBox.SelectedIndex = 0;
        }

        private void continueBtn_Click(object sender, EventArgs e)
        {
            familySymbol = fsm.familySymbols[FamilySymbolComboBox.SelectedIndex];
            this.DialogResult = DialogResult.OK;
            System.Diagnostics.Debug.WriteLine("Continue button was clicked");
            Close();
        }
    }
}
