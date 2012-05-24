using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Retail
{
    partial class ChangeItemAssignmentType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeItemAssignmentType));
            this.labelHeading = new System.Windows.Forms.Label();
            this.txtICN = new System.Windows.Forms.TextBox();
            this.labelStoreNo = new System.Windows.Forms.Label();
            this.pnlItemCostButtons = new System.Windows.Forms.Panel();
            this.customButtonSubmit = new CustomButton();
            this.customButtonCancel2 = new CustomButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.findButton = new System.Windows.Forms.Button();
            this.panelItem = new System.Windows.Forms.Panel();
            this.assignmentTypeCombo = new System.Windows.Forms.ComboBox();
            this.labelItemDescription = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCurrentAssignmentType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlItemCostButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(175, 45);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(205, 16);
            this.labelHeading.TabIndex = 1;
            this.labelHeading.Text = "Change Item Assignment Type";
            // 
            // txtICN
            // 
            this.txtICN.Location = new System.Drawing.Point(171, 3);
            this.txtICN.Name = "txtICN";
            this.txtICN.Size = new System.Drawing.Size(213, 21);
            this.txtICN.TabIndex = 2;
            this.txtICN.TextChanged += new System.EventHandler(this.txtICN_TextChanged);
            // 
            // labelStoreNo
            // 
            this.labelStoreNo.AutoSize = true;
            this.labelStoreNo.BackColor = System.Drawing.Color.Transparent;
            this.labelStoreNo.Location = new System.Drawing.Point(132, 6);
            this.labelStoreNo.Name = "labelStoreNo";
            this.labelStoreNo.Size = new System.Drawing.Size(33, 13);
            this.labelStoreNo.TabIndex = 3;
            this.labelStoreNo.Text = "ICN#";
            // 
            // pnlItemCostButtons
            // 
            this.pnlItemCostButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlItemCostButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlItemCostButtons.Controls.Add(this.customButtonSubmit);
            this.pnlItemCostButtons.Controls.Add(this.customButtonCancel2);
            this.pnlItemCostButtons.Location = new System.Drawing.Point(6, 353);
            this.pnlItemCostButtons.Name = "pnlItemCostButtons";
            this.pnlItemCostButtons.Size = new System.Drawing.Size(522, 57);
            this.pnlItemCostButtons.TabIndex = 147;
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSubmit.BackgroundImage")));
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(421, 4);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 13;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Visible = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
            // 
            // customButtonCancel2
            // 
            this.customButtonCancel2.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel2.BackgroundImage")));
            this.customButtonCancel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.customButtonCancel2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel2.FlatAppearance.BorderSize = 0;
            this.customButtonCancel2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel2.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel2.Location = new System.Drawing.Point(9, 4);
            this.customButtonCancel2.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel2.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel2.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel2.Name = "customButtonCancel2";
            this.customButtonCancel2.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel2.TabIndex = 7;
            this.customButtonCancel2.Text = "Cancel";
            this.customButtonCancel2.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.txtICN);
            this.panel1.Controls.Add(this.labelStoreNo);
            this.panel1.Controls.Add(this.findButton);
            this.panel1.Location = new System.Drawing.Point(6, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 28);
            this.panel1.TabIndex = 148;
            // 
            // findButton
            // 
            this.findButton.AutoSize = true;
            this.findButton.BackColor = System.Drawing.Color.Transparent;
            this.findButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.findButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.findButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.findButton.FlatAppearance.BorderSize = 0;
            this.findButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.findButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.findButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findButton.ForeColor = System.Drawing.Color.White;
            this.findButton.Location = new System.Drawing.Point(390, 2);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(41, 24);
            this.findButton.TabIndex = 4;
            this.findButton.Text = "Find";
            this.findButton.UseVisualStyleBackColor = false;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // panelItem
            // 
            this.panelItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelItem.BackColor = System.Drawing.Color.Transparent;
            this.panelItem.Controls.Add(this.assignmentTypeCombo);
            this.panelItem.Controls.Add(this.labelItemDescription);
            this.panelItem.Controls.Add(this.label3);
            this.panelItem.Controls.Add(this.label2);
            this.panelItem.Controls.Add(this.labelCurrentAssignmentType);
            this.panelItem.Controls.Add(this.label1);
            this.panelItem.Location = new System.Drawing.Point(6, 112);
            this.panelItem.Name = "panelItem";
            this.panelItem.Size = new System.Drawing.Size(523, 235);
            this.panelItem.TabIndex = 149;
            this.panelItem.Visible = false;
            // 
            // assignmentTypeCombo
            // 
            this.assignmentTypeCombo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.assignmentTypeCombo.FormattingEnabled = true;
            this.assignmentTypeCombo.Location = new System.Drawing.Point(239, 189);
            this.assignmentTypeCombo.Name = "assignmentTypeCombo";
            this.assignmentTypeCombo.Size = new System.Drawing.Size(218, 21);
            this.assignmentTypeCombo.TabIndex = 148;
            // 
            // labelItemDescription
            // 
            this.labelItemDescription.AutoSize = true;
            this.labelItemDescription.Location = new System.Drawing.Point(96, 106);
            this.labelItemDescription.Name = "labelItemDescription";
            this.labelItemDescription.Size = new System.Drawing.Size(110, 13);
            this.labelItemDescription.TabIndex = 9;
            this.labelItemDescription.Text = "Item Description here";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(102, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "New Assignment Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(190, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Merchandise Description";
            // 
            // labelCurrentAssignmentType
            // 
            this.labelCurrentAssignmentType.AutoSize = true;
            this.labelCurrentAssignmentType.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentAssignmentType.Location = new System.Drawing.Point(212, 47);
            this.labelCurrentAssignmentType.Name = "labelCurrentAssignmentType";
            this.labelCurrentAssignmentType.Size = new System.Drawing.Size(45, 13);
            this.labelCurrentAssignmentType.TabIndex = 5;
            this.labelCurrentAssignmentType.Text = "Sellback";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Current Assignment Type:";
            // 
            // ChangeItemAssignmentType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 418);
            this.Controls.Add(this.panelItem);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlItemCostButtons);
            this.Controls.Add(this.labelHeading);
            this.Name = "ChangeItemAssignmentType";
            this.Text = "ChangeItemAssignmentType";
            this.pnlItemCostButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelItem.ResumeLayout(false);
            this.panelItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.TextBox txtICN;
        private System.Windows.Forms.Label labelStoreNo;
        private System.Windows.Forms.Panel pnlItemCostButtons;
        private CustomButton customButtonSubmit;
        private CustomButton customButtonCancel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Panel panelItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCurrentAssignmentType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelItemDescription;
        private System.Windows.Forms.ComboBox assignmentTypeCombo;
    }
}