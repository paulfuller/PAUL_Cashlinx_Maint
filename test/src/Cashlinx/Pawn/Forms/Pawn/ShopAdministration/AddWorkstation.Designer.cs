using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class AddWorkstation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWorkstation));
            this.labelHeading = new System.Windows.Forms.Label();
            this.customLabelWorkstationName = new CustomLabel();
            this.customTextBoxWorkstationName = new CustomTextBox();
            this.customButtonSubmit = new CustomButton();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(149, 26);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(157, 24);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Add Workstation";
            // 
            // customLabelWorkstationName
            // 
            this.customLabelWorkstationName.AutoSize = true;
            this.customLabelWorkstationName.BackColor = System.Drawing.Color.Transparent;
            this.customLabelWorkstationName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelWorkstationName.Location = new System.Drawing.Point(50, 117);
            this.customLabelWorkstationName.Name = "customLabelWorkstationName";
            this.customLabelWorkstationName.Required = true;
            this.customLabelWorkstationName.Size = new System.Drawing.Size(127, 17);
            this.customLabelWorkstationName.TabIndex = 1;
            this.customLabelWorkstationName.Text = "Workstation Name:";
            // 
            // customTextBoxWorkstationName
            // 
            this.customTextBoxWorkstationName.CausesValidation = false;
            this.customTextBoxWorkstationName.ErrorMessage = "";
            this.customTextBoxWorkstationName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxWorkstationName.Location = new System.Drawing.Point(233, 117);
            this.customTextBoxWorkstationName.MaxLength = 100;
            this.customTextBoxWorkstationName.Name = "customTextBoxWorkstationName";
            this.customTextBoxWorkstationName.Size = new System.Drawing.Size(203, 24);
            this.customTextBoxWorkstationName.TabIndex = 2;
            this.customTextBoxWorkstationName.ValidationExpression = "";
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
            this.customButtonSubmit.Location = new System.Drawing.Point(233, 249);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 3;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = true;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
            // 
            // AddWorkstation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 320);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customTextBoxWorkstationName);
            this.Controls.Add(this.customLabelWorkstationName);
            this.Controls.Add(this.labelHeading);
            this.Name = "AddWorkstation";
            this.Text = "AddWorkstation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private CustomLabel customLabelWorkstationName;
        private CustomTextBox customTextBoxWorkstationName;
        private CustomButton customButtonSubmit;
    }
}