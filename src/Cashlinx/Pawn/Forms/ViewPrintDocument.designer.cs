using Common.Libraries.Forms.Components;

namespace Pawn.Forms
{
    partial class ViewPrintDocument : CustomBaseForm
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
            this.reprintFormHeaderLabel = new System.Windows.Forms.Label();
            this.customLabel1 = new CustomLabel();
            this.documentNameLabel = new CustomLabel();
            this.viewDocButton = new CustomButton();
            this.printDocButton = new CustomButton();
            this.cancelButton = new CustomButton();
            this.SuspendLayout();
            // 
            // reprintFormHeaderLabel
            // 
            this.reprintFormHeaderLabel.AutoSize = true;
            this.reprintFormHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.reprintFormHeaderLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reprintFormHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.reprintFormHeaderLabel.Location = new System.Drawing.Point(105, 19);
            this.reprintFormHeaderLabel.Name = "reprintFormHeaderLabel";
            this.reprintFormHeaderLabel.Size = new System.Drawing.Size(137, 19);
            this.reprintFormHeaderLabel.TabIndex = 0;
            this.reprintFormHeaderLabel.Text = "Reprint Document";
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.customLabel1.Location = new System.Drawing.Point(12, 82);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(89, 13);
            this.customLabel1.TabIndex = 1;
            this.customLabel1.Text = "Document Name:";
            this.customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // documentNameLabel
            // 
            this.documentNameLabel.AutoSize = true;
            this.documentNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.documentNameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentNameLabel.Location = new System.Drawing.Point(111, 82);
            this.documentNameLabel.Name = "documentNameLabel";
            this.documentNameLabel.Size = new System.Drawing.Size(89, 13);
            this.documentNameLabel.TabIndex = 2;
            this.documentNameLabel.Text = "DOC NAME HERE";
            // 
            // viewDocButton
            // 
            this.viewDocButton.BackColor = System.Drawing.Color.Transparent;
            this.viewDocButton.BackgroundImage = Common.Properties.Resources.vistabutton_blue;
            this.viewDocButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.viewDocButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.viewDocButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.viewDocButton.FlatAppearance.BorderSize = 0;
            this.viewDocButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.viewDocButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.viewDocButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewDocButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewDocButton.ForeColor = System.Drawing.Color.White;
            this.viewDocButton.Location = new System.Drawing.Point(238, 119);
            this.viewDocButton.Margin = new System.Windows.Forms.Padding(0);
            this.viewDocButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.viewDocButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.viewDocButton.Name = "viewDocButton";
            this.viewDocButton.Size = new System.Drawing.Size(100, 50);
            this.viewDocButton.TabIndex = 3;
            this.viewDocButton.Text = "View";
            this.viewDocButton.UseVisualStyleBackColor = false;
            this.viewDocButton.Click += new System.EventHandler(this.viewDocButton_Click);
            // 
            // printDocButton
            // 
            this.printDocButton.BackColor = System.Drawing.Color.Transparent;
            this.printDocButton.BackgroundImage = Common.Properties.Resources.vistabutton_blue;
            this.printDocButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.printDocButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.printDocButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.printDocButton.FlatAppearance.BorderSize = 0;
            this.printDocButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.printDocButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.printDocButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printDocButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printDocButton.ForeColor = System.Drawing.Color.White;
            this.printDocButton.Location = new System.Drawing.Point(238, 169);
            this.printDocButton.Margin = new System.Windows.Forms.Padding(0);
            this.printDocButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.printDocButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.printDocButton.Name = "printDocButton";
            this.printDocButton.Size = new System.Drawing.Size(100, 50);
            this.printDocButton.TabIndex = 6;
            this.printDocButton.Text = "Print";
            this.printDocButton.UseVisualStyleBackColor = false;
            this.printDocButton.Click += new System.EventHandler(this.printDocButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = Common.Properties.Resources.vistabutton_blue;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(9, 169);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ReprintDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Common.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(347, 228);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.printDocButton);
            this.Controls.Add(this.viewDocButton);
            this.Controls.Add(this.documentNameLabel);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.reprintFormHeaderLabel);
            this.Name = "ReprintDocument";
            this.Text = "Reprint Document";
            this.Load += new System.EventHandler(this.ReprintDocument_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reprintFormHeaderLabel;
        private CustomLabel customLabel1;
        private CustomLabel documentNameLabel;
        private CustomButton viewDocButton;
        private CustomButton printDocButton;
        private CustomButton cancelButton;
    }
}