using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Void
{
    partial class VoidTransactionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoidTransactionForm));
            this.labelHeading = new System.Windows.Forms.Label();
            this.customTextBoxTranNo = new CustomTextBox();
            this.customButtonCancel = new CustomButton();
            this.customButtonOK = new CustomButton();
            this.customLabelHeading = new CustomLabel();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(212, 24);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(116, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Void Transaction";
            // 
            // customTextBoxTranNo
            // 
            this.customTextBoxTranNo.CausesValidation = false;
            this.customTextBoxTranNo.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxTranNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxTranNo.Location = new System.Drawing.Point(244, 108);
            this.customTextBoxTranNo.Name = "customTextBoxTranNo";
            this.customTextBoxTranNo.Required = true;
            this.customTextBoxTranNo.Size = new System.Drawing.Size(146, 21);
            this.customTextBoxTranNo.TabIndex = 2;
            this.customTextBoxTranNo.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel.BackgroundImage")));
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(76, 249);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 3;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonOK
            // 
            this.customButtonOK.BackColor = System.Drawing.Color.Transparent;
            this.customButtonOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonOK.BackgroundImage")));
            this.customButtonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonOK.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonOK.FlatAppearance.BorderSize = 0;
            this.customButtonOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonOK.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonOK.ForeColor = System.Drawing.Color.White;
            this.customButtonOK.Location = new System.Drawing.Point(359, 249);
            this.customButtonOK.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonOK.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonOK.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonOK.Name = "customButtonOK";
            this.customButtonOK.Size = new System.Drawing.Size(100, 50);
            this.customButtonOK.TabIndex = 4;
            this.customButtonOK.Text = "Continue";
            this.customButtonOK.UseVisualStyleBackColor = false;
            this.customButtonOK.Click += new System.EventHandler(this.customButtonOK_Click);
            // 
            // customLabelHeading
            // 
            this.customLabelHeading.AutoSize = true;
            this.customLabelHeading.BackColor = System.Drawing.Color.Transparent;
            this.customLabelHeading.Location = new System.Drawing.Point(73, 111);
            this.customLabelHeading.Name = "customLabelHeading";
            this.customLabelHeading.Required = true;
            this.customLabelHeading.Size = new System.Drawing.Size(136, 13);
            this.customLabelHeading.TabIndex = 5;
            this.customLabelHeading.Text = "Enter Transaction Number:";
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.BackColor = System.Drawing.Color.Transparent;
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Location = new System.Drawing.Point(76, 73);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(76, 13);
            this.labelErrorMessage.TabIndex = 6;
            this.labelErrorMessage.Text = "Error Message";
            this.labelErrorMessage.Visible = false;
            // 
            // VoidTransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 320);
            this.Controls.Add(this.labelErrorMessage);
            this.Controls.Add(this.customLabelHeading);
            this.Controls.Add(this.customButtonOK);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customTextBoxTranNo);
            this.Controls.Add(this.labelHeading);
            this.Name = "VoidTransactionForm";
            this.Text = "VoidTransactionForm";
            this.Load += new System.EventHandler(this.VoidTransactionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private CustomTextBox customTextBoxTranNo;
        private CustomButton customButtonCancel;
        private CustomButton customButtonOK;
        private CustomLabel customLabelHeading;
        private System.Windows.Forms.Label labelErrorMessage;
    }
}