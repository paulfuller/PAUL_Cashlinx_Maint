using Common.Libraries.Forms.Components;
//using Pawn.Forms.UserControls;
using Support.Forms.UserControls; 
using Support.Libraries.Forms.Components;

//using Pawn.Logic;

namespace Support.Forms.Customer
{
    partial class UpdateCustomerIdentification
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
            this.labelHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customButtonCancel = new Support.Libraries.Forms.Components.SupportButton();
            this.customButtonReset = new Support.Libraries.Forms.Components.SupportButton();
            this.customButtonSubmit = new Support.Libraries.Forms.Components.SupportButton();
            this.identification1 = new Support.Forms.UserControls.Identification();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(192, 23);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(224, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Government Identification";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(3, 329);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(640, 1);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(30, 371);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 24;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // customButtonReset
            // 
            this.customButtonReset.BackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customButtonReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonReset.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonReset.FlatAppearance.BorderSize = 0;
            this.customButtonReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonReset.ForeColor = System.Drawing.Color.White;
            this.customButtonReset.Location = new System.Drawing.Point(410, 376);
            this.customButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReset.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.Name = "customButtonReset";
            this.customButtonReset.Size = new System.Drawing.Size(100, 50);
            this.customButtonReset.TabIndex = 25;
            this.customButtonReset.Text = "&Reset";
            this.customButtonReset.UseVisualStyleBackColor = true;
            this.customButtonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(521, 374);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 26;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = true;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // identification1
            // 
            this.identification1.AutoSize = true;
            this.identification1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.identification1.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.identification1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.identification1.CausesValidation = false;
            this.identification1.Location = new System.Drawing.Point(55, 83);
            this.identification1.Name = "identification1";
            this.identification1.Size = new System.Drawing.Size(531, 104);
            this.identification1.TabIndex = 23;
            // 
            // UpdateCustomerIdentification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.BackgroundImage = global::Support.Properties.Resources.form_800_480;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(645, 435);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonReset);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.identification1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateCustomerIdentification";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Customer Identification";
            this.Load += new System.EventHandler(this.UpdateCustomerIdentification_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private Identification identification1;
        private SupportButton customButtonCancel;
        private SupportButton customButtonReset;
        private SupportButton customButtonSubmit;
    }
}
