namespace Radagon.Forms
{
    partial class SelectViewSheetsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectAllBtn = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.continueBtn = new System.Windows.Forms.Button();
            this.viewSheetList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // selectAllBtn
            // 
            this.selectAllBtn.Location = new System.Drawing.Point(133, 264);
            this.selectAllBtn.Name = "selectAllBtn";
            this.selectAllBtn.Size = new System.Drawing.Size(75, 23);
            this.selectAllBtn.TabIndex = 7;
            this.selectAllBtn.Text = "Select All";
            this.selectAllBtn.UseVisualStyleBackColor = true;
            this.selectAllBtn.Click += new System.EventHandler(this.selectAllBtn_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(11, 11);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(107, 13);
            this.infoLabel.TabIndex = 6;
            this.infoLabel.Text = "Click to select sheets";
            // 
            // continueBtn
            // 
            this.continueBtn.Location = new System.Drawing.Point(214, 264);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(75, 23);
            this.continueBtn.TabIndex = 5;
            this.continueBtn.Text = "Continue";
            this.continueBtn.UseVisualStyleBackColor = true;
            this.continueBtn.Click += new System.EventHandler(this.continueBtn_Click);
            // 
            // viewSheetList
            // 
            this.viewSheetList.CheckOnClick = true;
            this.viewSheetList.FormattingEnabled = true;
            this.viewSheetList.Location = new System.Drawing.Point(13, 27);
            this.viewSheetList.Name = "viewSheetList";
            this.viewSheetList.Size = new System.Drawing.Size(276, 229);
            this.viewSheetList.TabIndex = 4;
            // 
            // SelectViewSheetsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 299);
            this.Controls.Add(this.selectAllBtn);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.continueBtn);
            this.Controls.Add(this.viewSheetList);
            this.Name = "SelectViewSheetsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radagon - Select Sheets";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectAllBtn;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button continueBtn;
        private System.Windows.Forms.CheckedListBox viewSheetList;
    }
}