namespace Radagon.Forms
{
    partial class EditSelectedViewportLocationsForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ViewsGrid = new System.Windows.Forms.DataGridView();
            this.YCoordinate = new System.Windows.Forms.TextBox();
            this.XCoordinate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SetAllBtn = new System.Windows.Forms.Button();
            this.continueBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.MinimumSize = new System.Drawing.Size(720, 399);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ViewsGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.YCoordinate);
            this.splitContainer1.Panel2.Controls.Add(this.XCoordinate);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.SetAllBtn);
            this.splitContainer1.Panel2.Controls.Add(this.continueBtn);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 391;
            this.splitContainer1.TabIndex = 3;
            // 
            // ViewsGrid
            // 
            this.ViewsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ViewsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewsGrid.Location = new System.Drawing.Point(0, 0);
            this.ViewsGrid.Name = "ViewsGrid";
            this.ViewsGrid.Size = new System.Drawing.Size(800, 391);
            this.ViewsGrid.TabIndex = 0;
            // 
            // YCoordinate
            // 
            this.YCoordinate.Location = new System.Drawing.Point(451, 17);
            this.YCoordinate.Name = "YCoordinate";
            this.YCoordinate.Size = new System.Drawing.Size(100, 20);
            this.YCoordinate.TabIndex = 5;
            this.YCoordinate.Validating += new System.ComponentModel.CancelEventHandler(this.Coordinate_Validating);
            // 
            // XCoordinate
            // 
            this.XCoordinate.Location = new System.Drawing.Point(322, 17);
            this.XCoordinate.Name = "XCoordinate";
            this.XCoordinate.Size = new System.Drawing.Size(100, 20);
            this.XCoordinate.TabIndex = 4;
            this.XCoordinate.Validating += new System.ComponentModel.CancelEventHandler(this.Coordinate_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(428, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "X:";
            // 
            // SetAllBtn
            // 
            this.SetAllBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SetAllBtn.Location = new System.Drawing.Point(632, 20);
            this.SetAllBtn.Name = "SetAllBtn";
            this.SetAllBtn.Size = new System.Drawing.Size(75, 23);
            this.SetAllBtn.TabIndex = 1;
            this.SetAllBtn.Text = "Set All";
            this.SetAllBtn.UseVisualStyleBackColor = true;
            this.SetAllBtn.Click += new System.EventHandler(this.SetAllBtn_Click);
            // 
            // continueBtn
            // 
            this.continueBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.continueBtn.Location = new System.Drawing.Point(713, 20);
            this.continueBtn.Name = "continueBtn";
            this.continueBtn.Size = new System.Drawing.Size(75, 23);
            this.continueBtn.TabIndex = 0;
            this.continueBtn.Text = "Continue";
            this.continueBtn.UseVisualStyleBackColor = true;
            this.continueBtn.Click += new System.EventHandler(this.continueBtn_Click);
            // 
            // EditSelectedViewportLocationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(720, 399);
            this.Name = "EditSelectedViewportLocationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditSelectedViewportLocationsForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView ViewsGrid;
        private System.Windows.Forms.TextBox YCoordinate;
        private System.Windows.Forms.TextBox XCoordinate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SetAllBtn;
        private System.Windows.Forms.Button continueBtn;
    }
}