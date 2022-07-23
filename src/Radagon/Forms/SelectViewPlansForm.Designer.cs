namespace Radagon.Forms
{
    partial class SelectViewPlansForm
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
            this.viewPlanList = new System.Windows.Forms.CheckedListBox();
            this.continueBtn = new System.Windows.Forms.Button();
            this.infoLabel = new System.Windows.Forms.Label();
            this.selectAllBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // viewPlanList
            // 
            this.viewPlanList.FormattingEnabled = true;
            this.viewPlanList.Location = new System.Drawing.Point(12, 29);
            this.viewPlanList.Name = "viewPlanList";
            this.viewPlanList.Size = new System.Drawing.Size(276, 229);
            this.viewPlanList.TabIndex = 0;
            this.viewPlanList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.viewPlanList_ItemCheck);
            // 
            // continueBtn
            // 
            this.continueBtn.Location = new System.Drawing.Point(213, 266);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(75, 23);
            this.continueBtn.TabIndex = 1;
            this.continueBtn.Text = "Continue";
            this.continueBtn.UseVisualStyleBackColor = true;
            this.continueBtn.Click += new System.EventHandler(this.continueBtn_Click);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(10, 13);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(129, 13);
            this.infoLabel.TabIndex = 2;
            this.infoLabel.Text = "Click to select view plans:";
            // 
            // selectAllBtn
            // 
            this.selectAllBtn.Location = new System.Drawing.Point(132, 266);
            this.selectAllBtn.Name = "selectAllBtn";
            this.selectAllBtn.Size = new System.Drawing.Size(75, 23);
            this.selectAllBtn.TabIndex = 3;
            this.selectAllBtn.Text = "Select All";
            this.selectAllBtn.UseVisualStyleBackColor = true;
            this.selectAllBtn.Click += new System.EventHandler(this.selectAllBtn_Click);
            // 
            // SelectViewPlansForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 299);
            this.Controls.Add(this.selectAllBtn);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.continueBtn);
            this.Controls.Add(this.viewPlanList);
            this.Name = "SelectViewPlansForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radagon - Select View Plans";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox viewPlanList;
        private System.Windows.Forms.Button continueBtn;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button selectAllBtn;
    }
}