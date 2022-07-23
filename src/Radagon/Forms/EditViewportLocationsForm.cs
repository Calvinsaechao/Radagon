namespace Radagon.Forms
{
    using Autodesk.Revit.DB;
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
    public partial class EditViewportLocationsForm : System.Windows.Forms.Form
    {
        private IList<Viewport> viewports;
        public EditViewportLocationsForm(Document document, IList<ViewSheet> viewSheets, IList<Viewport> viewports)
        {
            InitializeComponent();
            this.viewports = viewports;
            SetAllBtn.Enabled = false;

            DataGridViewColumnCollection viewColumns = ViewsGrid.Columns;

            //Column settings
            ViewsGrid.ColumnCount = 6;
            viewColumns[0].Name = "Sheet Name";
            viewColumns[1].Name = "Sheet Number";
            viewColumns[2].Name = "View Plan";
            viewColumns[3].Name = "X";
            viewColumns[4].Name = "Y";
            viewColumns[0].ReadOnly = true;
            viewColumns[1].ReadOnly = true;
            viewColumns[2].ReadOnly = true;
            viewColumns[5].ReadOnly = true;
            viewColumns[5].Visible = false;

            for (int i = 0; i < viewSheets.Count; i++)
            {
                ViewSheet viewSheet = viewSheets[i];
                Viewport viewport = viewports[i];
                XYZ viewportLocation = viewport.GetBoxCenter();

                string[] row = new string[6];
                row[0] = viewSheet.Name;
                row[1] = viewSheet.SheetNumber;
                row[2] = (document.GetElement(viewport.ViewId) as ViewPlan).Name;
                row[3] = viewportLocation.X.ToString();
                row[4] = viewportLocation.Y.ToString();
                row[5] = i.ToString();
                ViewsGrid.Rows.Add(row);
            }

            //view grid and window settings
            ViewsGrid.Columns[3].ValueType = typeof(double);
            ViewsGrid.Columns[4].ValueType = typeof(double);
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
            ViewsGrid.Show();
        }

        private void continueBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ViewsGrid.Rows.Count; i++)
            {
                DataGridViewRow row = ViewsGrid.Rows[i];
                double X = Double.Parse(row.Cells[3].Value.ToString());
                double Y = Double.Parse(row.Cells[4].Value.ToString());
                int position = Int32.Parse(row.Cells[5].Value.ToString());
                viewports[position].SetBoxCenter(new XYZ(X, Y, 0));
            }
            continueBtn.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            Debug.WriteLine("Continue button was clicked");
            Close();
        }

        private void SetAllBtn_Click(object sender, EventArgs e)
        {
            double X = Double.Parse(XCoordinate.Text.ToString());
            double Y = Double.Parse(YCoordinate.Text.ToString());
            for (int i = 0; i < ViewsGrid.Rows.Count; i++)
            {
                DataGridViewRow row = ViewsGrid.Rows[i];
                row.Cells[3].Value = X.ToString();
                row.Cells[4].Value = Y.ToString();
            }
        }

        private void XCoordinate_Validating(object sender, CancelEventArgs e)
        {
            if (validateCoordinates())
                SetAllBtn.Enabled = false;
            else
                SetAllBtn.Enabled = true;
        }

        private void YCoordinate_Validating(object sender, CancelEventArgs e)
        {
            if (validateCoordinates())
                SetAllBtn.Enabled = false;
            else
                SetAllBtn.Enabled = true;
        }
        private Boolean validateCoordinates()
        {
            double result = 0;
            return !Double.TryParse(YCoordinate.Text.ToString(), out result) || !Double.TryParse(XCoordinate.Text.ToString(), out result);
        }
    }
}
