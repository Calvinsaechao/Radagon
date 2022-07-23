using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Radagon.RevitUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Radagon.Forms
{
    public partial class SelectViewSheetsForm : System.Windows.Forms.Form
    {
        public IList<ViewSheet> viewSheets;
        private ViewSheetManager viewSheetManager;
        public SelectViewSheetsForm(Document document)
        {
            viewSheets = new List<ViewSheet>();
            InitializeComponent();
            viewSheetManager = new ViewSheetManager(document);
            //Check if document contains any view sheets
            if (viewSheetManager.viewSheets.Count == 0)
            {
                DialogResult = DialogResult.Abort;
                Close();
            }

            foreach(ViewSheet viewSheet in viewSheetManager.viewSheets)
            {
                string sheetName = viewSheet.Name;
                string sheetNumber = viewSheet.SheetNumber;
                viewSheetList.Items.Add(sheetNumber + " - " + sheetName);
            }
        }

        private void continueBtn_Click(object sender, EventArgs e)
        {
            CheckedListBox.CheckedIndexCollection indexes = viewSheetList.CheckedIndices;
            if (indexes.Count == 0)
            {
                TaskDialog.Show("Select View Sheets", "No view sheets selected!");
                return;
            }
            foreach (int viewIndex in indexes)
            {
                ViewSheet viewSheet = viewSheetManager.viewSheets[viewIndex] as ViewSheet;
                if (viewSheet != null)
                    viewSheets.Add(viewSheet);
            }
            continueBtn.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            Debug.WriteLine("Continue button was clicked");
            Close();
        }

        private void selectAllBtn_Click(object sender, EventArgs e)
        {
            if (selectAllBtn.Text == "Select All")
            {
                for (int i = 0; i < viewSheetList.Items.Count; i++)
                {
                    viewSheetList.SetItemChecked(i, true);
                }
                selectAllBtn.Text = "Deselect All";
            }
            else
            {
                for (int i = 0; i < viewSheetList.Items.Count; i++)
                {
                    viewSheetList.SetItemChecked(i, false);
                }
                selectAllBtn.Text = "Select All";
            }
        }
    }
}
