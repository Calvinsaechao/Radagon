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
    //TODO: Add a way to identify View Plans of different types of View Plans
        //Maybe append category to Name
    public partial class SelectViewPlanForm : System.Windows.Forms.Form
    {
        public ViewPlan viewPlan;
        private ViewPlanManager viewPlanManager;
        public SelectViewPlanForm(Document document)
        {
            InitializeComponent();
            viewPlanManager = new ViewPlanManager(document);
            foreach(ViewPlan viewPlan in viewPlanManager.viewPlans)
                ViewPlanComboBox.Items.Add(viewPlan.Name);
            if (ViewPlanComboBox.Items.Count != 0)
                ViewPlanComboBox.SelectedIndex = 0;
        }
        private void continueBtn_Click(object sender, EventArgs e)
        {
            viewPlan = viewPlanManager.viewPlans[ViewPlanComboBox.SelectedIndex];
            this.DialogResult = DialogResult.OK;
            System.Diagnostics.Debug.WriteLine("Continue button was clicked");
            Close();
        }
    }
}
