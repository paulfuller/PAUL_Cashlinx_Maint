using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    partial class CategorySelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategorySelect));
            this.ContinueBtn = new CustomButton();
            this.ClearBtn = new CustomButton();
            this.CancelBtn = new CustomButton();
            this.labelHeading = new System.Windows.Forms.Label();
            this.CategoryTree = new TriStateTreeView();  // System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // ContinueBtn
            // 
            this.ContinueBtn.BackColor = System.Drawing.Color.Transparent;
            this.ContinueBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ContinueBtn.BackgroundImage")));
            this.ContinueBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ContinueBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContinueBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ContinueBtn.FlatAppearance.BorderSize = 0;
            this.ContinueBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ContinueBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ContinueBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContinueBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueBtn.ForeColor = System.Drawing.Color.White;
            this.ContinueBtn.Location = new System.Drawing.Point(313, 228);
            this.ContinueBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ContinueBtn.MaximumSize = new System.Drawing.Size(100, 50);
            this.ContinueBtn.MinimumSize = new System.Drawing.Size(100, 50);
            this.ContinueBtn.Name = "ContinueBtn";
            this.ContinueBtn.Size = new System.Drawing.Size(100, 50);
            this.ContinueBtn.TabIndex = 0;
            this.ContinueBtn.Text = "Continue";
            this.ContinueBtn.UseVisualStyleBackColor = false;
            this.ContinueBtn.Click += new System.EventHandler(this.ContinueBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.BackColor = System.Drawing.Color.Transparent;
            this.ClearBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ClearBtn.BackgroundImage")));
            this.ClearBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClearBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ClearBtn.FlatAppearance.BorderSize = 0;
            this.ClearBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.ClearBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.ClearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearBtn.ForeColor = System.Drawing.Color.White;
            this.ClearBtn.Location = new System.Drawing.Point(204, 228);
            this.ClearBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ClearBtn.MaximumSize = new System.Drawing.Size(100, 50);
            this.ClearBtn.MinimumSize = new System.Drawing.Size(100, 50);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(100, 50);
            this.ClearBtn.TabIndex = 1;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.UseVisualStyleBackColor = false;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.Transparent;
            this.CancelBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CancelBtn.BackgroundImage")));
            this.CancelBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CancelBtn.FlatAppearance.BorderSize = 0;
            this.CancelBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CancelBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelBtn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.ForeColor = System.Drawing.Color.White;
            this.CancelBtn.Location = new System.Drawing.Point(104, 228);
            this.CancelBtn.Margin = new System.Windows.Forms.Padding(0);
            this.CancelBtn.MaximumSize = new System.Drawing.Size(100, 50);
            this.CancelBtn.MinimumSize = new System.Drawing.Size(100, 50);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(100, 50);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // labelHeading
            // 
            this.labelHeading.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(9, 9);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(210, 19);
            this.labelHeading.TabIndex = 3;
            this.labelHeading.Text = "Select Merchandise Category";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CategoryTree
            // 
            this.CategoryTree.CheckBoxes = true;
            this.CategoryTree.Location = new System.Drawing.Point(3, 31);
            this.CategoryTree.Name = "CategoryTree";
            this.CategoryTree.ShowLines = false;
            this.CategoryTree.Size = new System.Drawing.Size(416, 194);
            this.CategoryTree.TabIndex = 1;
            this.CategoryTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.CategoryTree_AfterCheck);
            // 
            // CategorySelect
            // 
            this.AcceptButton = this.ContinueBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(422, 288);
            this.ControlBox = false;
            this.Controls.Add(this.CategoryTree);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.ContinueBtn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CategorySelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton ContinueBtn;
        private CustomButton ClearBtn;
        private CustomButton CancelBtn;
        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.TreeView CategoryTree;
    }
}