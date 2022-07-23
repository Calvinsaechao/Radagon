namespace Radagon.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.DB;
    using View = Autodesk.Revit.DB.View;
    using System.Diagnostics;

    public partial class DuplicateViewsForm : System.Windows.Forms.Form
    {
        private Document document;
        private IList<View> viewPlans;
        public IList<View> duplicatedViews;

        public DuplicateViewsForm(Document document, IList<View> viewPlans)
        {
            InitializeComponent();
            this.document = document;
            this.viewPlans = viewPlans;

            DataGridViewColumnCollection viewColumns = ViewsGrid.Columns;

            //Column settings
            ViewsGrid.ColumnCount = 5;
            viewColumns[0].Name = "View Type";
            viewColumns[1].Name = "View Name";
            viewColumns[2].Name = "Parent Name";
            viewColumns[3].Name = "Duplication Count";
            viewColumns[4].Name = "ID";
            viewColumns[0].ReadOnly = true;
            viewColumns[1].ReadOnly = true;
            viewColumns[2].ReadOnly = true;
            viewColumns[4].ReadOnly = true;
            viewColumns[4].Visible = false;

            //Populate rows
            int count = 0;
            foreach (View view in viewPlans)
            {
                View parent = document.GetElement(view.GetPrimaryViewId()) as View;
                string[] row = new string[5];
                row[0] = view.ViewType.ToString();
                row[1] = view.Name;
                if (parent != null)
                    row[2] = parent.Name;
                else row[2] = "None";
                if (parent == null)
                    row[3] = 1.ToString();
                else row[3] = "N/A";
                row[4] = count.ToString();
                int position = ViewsGrid.Rows.Add(row);
                // Disable duplication count if view is a child.
                if (row[3] == "N/A") ViewsGrid.Rows[position].Cells[3].ReadOnly = true;
                count++;
            }

            //view grid and window settings
            ViewsGrid.Columns[3].ValueType = typeof(int);
            ViewsGrid.AllowUserToAddRows = false;
            ViewsGrid.AllowUserToDeleteRows = false;
            ViewsGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            ViewsGrid.AutoSize = true;
            ViewsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            ViewsGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            ViewsGrid.BackgroundColor = System.Drawing.Color.White;
            ViewsGrid.AllowUserToResizeColumns = false;
            ViewsGrid.AllowUserToResizeRows = false;
            this.MaximumSize = new System.Drawing.Size((int)Math.Floor(Screen.PrimaryScreen.Bounds.Width * 0.75), (int)Math.Floor(Screen.PrimaryScreen.Bounds.Height * 0.75));
            withDetailingCheckBox.Checked = true;
            ViewsGrid.Show();
        }

        private void close(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Debug.WriteLine("X button was clicked");
            Close();
        }

        /// <summary>
        /// Duplicates the list of views in viewList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continueBtn_Click(object sender, EventArgs e)
        {
            //Create list of dependent views selected
            IList<View> selectedDependentViews = getDependentViews();
            duplicatedViews = new List<View>();
            for(int i = 0; i < ViewsGrid.Rows.Count; i++)
            {
                DataGridViewRow row = ViewsGrid.Rows[i];
                Boolean dependent = (row.Cells[2].Value.ToString() != "None");
                int index = Int32.Parse(row.Cells[4].Value.ToString());
                if (!dependent) //Copy view with its dependents
                {
                    int count = Int32.Parse(row.Cells[3].Value.ToString());
                    //Find its dependents and add them to the list.
                    View view = viewPlans[index];
                    IList<View> viewDependents = new List<View>();
                    ICollection<ElementId> allViewDependents = view.GetDependentViewIds();
                    //Get all the viewDependents of the current view that are selected to be duplicated.
                    foreach(ElementId viewDependentId in allViewDependents)
                    {
                        //if any of the dependent views are in selectedDependentViews, add them to the viewDependents list.
                        foreach(View dependentView in selectedDependentViews)
                        {
                            if(dependentView.Id.IntegerValue.Equals(viewDependentId.IntegerValue)) viewDependents.Add(dependentView);
                        }
                    }
                    for(int j = 0; j < count; j++)
                    {
                        ViewDuplicateOption viewDuplicateOption = ViewDuplicateOption.Duplicate;
                        if (withDetailingCheckBox.Checked)
                        {
                            viewDuplicateOption = ViewDuplicateOption.WithDetailing;
                        }
                        //duplicate parent
                        View duplicatedView = document.GetElement(view.Duplicate(viewDuplicateOption)) as View;
                        duplicatedViews.Add(duplicatedView);
                        //duplicate copy all selected dependents
                        foreach(View dependentView in viewDependents)
                        {
                            View duplicatedChild = document.GetElement(duplicatedView.Duplicate(ViewDuplicateOption.AsDependent)) as View;
                            duplicatedChild.CropBox = dependentView.CropBox;
                            duplicatedViews.Add(duplicatedChild);
                        }
                    }
                }
            }
            continueBtn.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            Debug.WriteLine("Continue button was clicked");
            Close();
        }

        private List<View> getDependentViews()
        {
            List<View> dependentViews = new List<View>();
            foreach (DataGridViewRow row in ViewsGrid.Rows)
            {
                if (row.Cells[2].Value.ToString() != "None")
                {
                    int index = Int32.Parse(row.Cells[4].Value.ToString());
                    dependentViews.Add(viewPlans[index]);
                }
            }
            return dependentViews;
        }
    }
}
