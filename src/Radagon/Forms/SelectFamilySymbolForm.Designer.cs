namespace Radagon.Forms
{
    partial class SelectFamilySymbolForm
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
            this.FamilySymbolComboBox = new System.Windows.Forms.ComboBox();
            this.continueBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FamilySymbolComboBox
            // 
            this.FamilySymbolComboBox.FormattingEnabled = true;
            this.FamilySymbolComboBox.Location = new System.Drawing.Point(12, 27);
            this.FamilySymbolComboBox.Name = "FamilySymbolComboBox";
            this.FamilySymbolComboBox.Size = new System.Drawing.Size(216, 21);
            this.FamilySymbolComboBox.TabIndex = 3;
            // 
            // continueBtn
            // 
            this.continueBtn.Location = new System.Drawing.Point(153, 54);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(75, 23);
            this.continueBtn.TabIndex = 5;
            this.continueBtn.Text = "Continue";
            this.continueBtn.UseVisualStyleBackColor = true;
            this.continueBtn.Click += new System.EventHandler(this.continueBtn_Click);
            // 
            // SelectFamilySymbolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 89);
            this.Controls.Add(this.continueBtn);
            this.Controls.Add(this.FamilySymbolComboBox);
            this.Name = "SelectFamilySymbolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Radagon - Select Family Symbol";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox FamilySymbolComboBox;
        private System.Windows.Forms.Button continueBtn;
    }
}