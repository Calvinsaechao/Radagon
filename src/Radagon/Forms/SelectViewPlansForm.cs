namespace Radagon.Forms
{
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
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using static System.Windows.Forms.ListBox;
    using Form = System.Windows.Forms.Form;
    using View = Autodesk.Revit.DB.View;
    /// <summary>
    /// Class <c>SelectViewPlansForm</c> A form to select views within a document.
    /// </summary>
    public partial class SelectViewPlansForm : Form
    {
        /// <value>Property <c>viewList</c> contains all the selected view plans.</value>
        public IList<View> viewList;
        private IList<Element> viewPlans;
        private IList<Element> CollectorViewPlans;
        private Document source;
        /// <summary>
        /// This constructor initializes the new SelectViewPlansForm with a specified document.
        /// </summary>
        /// <param name="source">A document that contains view plans to be selected.</param>
        public SelectViewPlansForm(Document source)
        {
            InitializeComponent();
            this.source = source;

            //Form settings
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterParent;

            //view check list settings
            viewPlanList.CheckOnClick = true;

            //Get all plan views to be selected
            ElementClassFilter viewPlanFilter = new ElementClassFilter(typeof(ViewPlan));
            FilteredElementCollector collector = new FilteredElementCollector(source);
            CollectorViewPlans = collector.WherePasses(viewPlanFilter).ToElements();

            viewPlanList.BeginUpdate();
            //Add all views to list
            viewPlans = new List<Element>();
            for(int i = 0; i < CollectorViewPlans.Count; i++)
            {
                View view = CollectorViewPlans[i] as View;
                if (!view.IsTemplate)
                {
                    viewPlans.Add(view);
                    viewPlanList.Items.Add(view.ViewType + "\t" + view.Name);
                }
            }

            viewPlanList.EndUpdate();
            this.Controls.Add(this.viewPlanList);
        }

        private void viewPlanList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            View view = viewPlans[e.Index] as View;
            ElementClassFilter viewPlanFilter = new ElementClassFilter(typeof(ViewPlan));
            ICollection<ElementId> dependents = view.GetDependentViewIds();
            if (e.NewValue == CheckState.Unchecked)
            {
                if (dependents.Count != 0)
                {
                    //Uncheck all dependent views
                    foreach(ElementId dependent in dependents)
                    {
                        int index = getIndexOfElement(dependent);
                        if (viewPlanList.GetItemChecked(index) == true)
                            viewPlanList.SetItemCheckState(index, CheckState.Unchecked);
                    }
                }
                //Change Deselect All button to Select All
                selectAllBtn.Text = "Select All";
            }
            else if (e.NewValue == CheckState.Checked)
            {
                
                if (source.GetElement(view.GetPrimaryViewId()) is View parent && parent.Id != null)
                {
                    int index = getIndexOfElement(parent.Id);
                    if(viewPlanList.GetItemChecked(index) == false)
                        viewPlanList.SetItemChecked(index, true);
                }
            }
        }

        private void continueBtn_Click(object sender, EventArgs e)
        {
            viewList = new List<View>();
            CheckedListBox.CheckedIndexCollection indexes = viewPlanList.CheckedIndices;

            if (indexes.Count == 0)
            {
                TaskDialog.Show("Select View Plans", "No views selected!");
                return;
            }
            foreach(int viewIndex in indexes)
            {
                viewList.Add(viewPlans[viewIndex] as View);
            }
            continueBtn.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            Debug.WriteLine("Continue button was clicked");
            Close();
        }

        private int getIndexOfElement(ElementId Id)
        {
            foreach (Element element in viewPlans)
            {
                if (element.Id == Id)
                {
                    return viewPlans.IndexOf(element);
                }
            }
            return -1;
        }

        private void selectAllBtn_Click(object sender, EventArgs e)
        {
            if (selectAllBtn.Text == "Select All")
            {
                for (int i = 0; i < viewPlanList.Items.Count; i++)
                {
                    viewPlanList.SetItemChecked(i, true);
                }
                selectAllBtn.Text = "Deselect All";
            }
            else
            {
                for (int i = 0; i < viewPlanList.Items.Count; i++)
                {
                    viewPlanList.SetItemChecked(i, false);
                }
                selectAllBtn.Text = "Select All";
            }
        }
    }
}
