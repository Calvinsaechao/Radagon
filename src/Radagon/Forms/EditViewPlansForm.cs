namespace Radagon.Forms
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using View = Autodesk.Revit.DB.View;
    using System.Diagnostics;
    public partial class EditViewPlansForm : System.Windows.Forms.Form
    {
        private Document document;
        private IList<View> viewPlans;
        IList<View> viewTemplates;
        public EditViewPlansForm(Document document, IList<View> viewPlans)
        {
            InitializeComponent();
            this.document = document;
            this.viewPlans = viewPlans;

            //Find all View Templates in Document
            FilteredElementCollector collector = new FilteredElementCollector(document);
            ElementClassFilter viewsFilter = new ElementClassFilter(typeof(ViewPlan));
            IList<Element> allViews = collector.WherePasses(viewsFilter).ToElements();
            viewTemplates = new List<View>();

            DataGridViewColumnCollection viewColumns = ViewsGrid.Columns;
            ViewsGrid.ColumnCount = 5;
            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();

            //Populate view templates list
            dgvCmb.Items.Add("None");
            foreach (Element element in allViews)
            {
                View view = element as View;
                if (view.IsTemplate)
                {
                    viewTemplates.Add(view);
                }
            }

            dgvCmb.HeaderText = "View Template";
            dgvCmb.Name = "View Template";
            ViewsGrid.Columns.Add(dgvCmb);

            //column settings
            viewColumns[0].Name = "View Type";
            viewColumns[1].Name = "View Name";
            viewColumns[2].Name = "Parent Name";
            viewColumns[3].Name = "New View Name";
            viewColumns[4].Name = "View ID";
            viewColumns[0].ReadOnly = true;
            viewColumns[1].ReadOnly = true;
            viewColumns[2].ReadOnly = true;
            viewColumns[4].ReadOnly = true;
            viewColumns[4].Visible = false;

            //View Type, Parent Name, View Name, View Template
            foreach (View view in viewPlans)
            {
                View parent = document.GetElement(view.GetPrimaryViewId()) as View;
                string[] row = new string[5];
                row[0] = view.ViewType.ToString();
                row[1] = view.Name;
                if (parent != null)
                    row[2] = parent.Name;
                else row[2] = "None";
                row[3] = view.Name;
                row[4] = view.Id.ToString();
                int index = ViewsGrid.Rows.Add(row);
                DataGridViewComboBoxCell comboCell = ViewsGrid.Rows[index].Cells[5] as DataGridViewComboBoxCell;
                View viewTemplate = document.GetElement(view.ViewTemplateId) as View;
                if (viewTemplate != null)
                    comboCell.Value = viewTemplate.Name;
                else comboCell.Value = "None";
                foreach(View viewTemplate1 in viewTemplates)
                {
                    if(viewTemplate1.ViewType == view.ViewType)
                        comboCell.Items.Add(viewTemplate1.Name);
                }
            }

            //view grid settings
            ViewsGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            ViewsGrid.AutoSize = true;
            ViewsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ViewsGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ViewsGrid.BackgroundColor = System.Drawing.Color.White;
            ViewsGrid.AllowUserToResizeColumns = false;
            ViewsGrid.AllowUserToResizeRows = false;
            this.MaximumSize = new System.Drawing.Size((int)Math.Floor(Screen.PrimaryScreen.Bounds.Width * 0.75), (int)Math.Floor(Screen.PrimaryScreen.Bounds.Height * 0.75));
            this.Update();
            ViewsGrid.Show();
        }

        private void continueBtn_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < ViewsGrid.Rows.Count; i++)
            {
                View view = document.GetElement(new ElementId(Int32.Parse(ViewsGrid.Rows[i].Cells[4].Value.ToString()))) as View;
                view.Name = ViewsGrid.Rows[i].Cells[3].Value.ToString();
                DataGridViewComboBoxCell cell = ViewsGrid.Rows[i].Cells[5] as DataGridViewComboBoxCell;
                String selectedViewTemplate = cell.Value.ToString();
                foreach (View viewTemplate in viewTemplates)
                {
                    if (viewTemplate.Name == selectedViewTemplate)
                        view.ViewTemplateId = viewTemplate.Id;
                }
            }
            continueBtn.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            Debug.WriteLine("Continue button was clicked");
            Close();
        }
    }
}
