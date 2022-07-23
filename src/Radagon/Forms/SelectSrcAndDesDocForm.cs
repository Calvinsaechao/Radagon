using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Radagon.Forms
{
    public partial class SelectSrcAndDesDocForm : System.Windows.Forms.Form
    {
        private Autodesk.Revit.ApplicationServices.Application app;
        private UIApplication uiapp;
        private DocumentSet documents;
        public Document source;
        public Document destination;
        public SelectSrcAndDesDocForm(ExternalCommandData commandData)
        {
            InitializeComponent();
            uiapp = commandData.Application;
            app = uiapp.Application;
            documents = app.Documents;
            foreach (Document doc in documents)
            {
                sourceDocument.Items.Add(doc.Title);
                destinationDocument.Items.Add(doc.Title);
            }
            //Set default selection
            if (documents.Size > 1)
            {
                sourceDocument.SelectedIndex = 0;
                destinationDocument.SelectedIndex = 1;
            }
            else
            {
                this.DialogResult = DialogResult.Abort;
                Debug.WriteLine("Must have more than one document open.");
                throw new Exception("Must have more than one document open.");
            }
        }

        private void sourceDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void continueBtn_Click(object sender, EventArgs e)
        {
            if (sourceDocument.SelectedItem == null || destinationDocument.SelectedItem == null){
                this.DialogResult = DialogResult.Abort;
                Debug.WriteLine("No documents selected for source or destination.");
                return;
            }
            foreach (Document doc in documents)
            {
                string src = sourceDocument.SelectedItem.ToString();
                string dst = destinationDocument.SelectedItem.ToString();
                if (doc.Title == src)
                    source = doc;
                if (doc.Title == dst)
                    destination = doc;
            }
            continueBtn.DialogResult = DialogResult.OK;
            this.DialogResult = DialogResult.OK;
            Debug.WriteLine("Continue button was clicked");
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            cancelBtn.DialogResult = DialogResult.Cancel;
            this.DialogResult = DialogResult.Cancel;
            Debug.WriteLine("Cancel button was clicked");
        }
    }
}
