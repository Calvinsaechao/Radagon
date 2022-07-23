namespace Radagon.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    public partial class SelectFileForm : Form
    {
        private string fileExtension;
        public string filePath;
        public SelectFileForm(string fileExtension)
        {
            InitializeComponent();
            this.fileExtension = fileExtension;
        }

        private void SelectFileBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            string tempFilePath = openFileDialog1.FileName;
            if (tempFilePath.EndsWith(fileExtension))
            {
                if (!filesBox.Items.Contains(tempFilePath))
                {
                    filePath = tempFilePath;
                    filesBox.Items.Add(filePath);
                    filesBox.SelectedIndex = 0;
                }
                else
                {
                    filesBox.SelectedIndex = filesBox.Items.IndexOf(tempFilePath);
                }
            }
            else if (tempFilePath.Equals("")) { }
            else MessageBox.Show("Selected file extension must be " + fileExtension + ": " + tempFilePath,
                "Radagon",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void continueBtn_Click(object sender, EventArgs e)
        {
            if (filesBox.SelectedIndex == -1)
                MessageBox.Show("A file has not been selected.",
                    "Radagon",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            else
            {
                filePath = filesBox.Items[filesBox.SelectedIndex].ToString();
                this.DialogResult = DialogResult.OK;
                Close();
            }
            System.Diagnostics.Debug.WriteLine("Continue button was clicked");
        }
    }
}
