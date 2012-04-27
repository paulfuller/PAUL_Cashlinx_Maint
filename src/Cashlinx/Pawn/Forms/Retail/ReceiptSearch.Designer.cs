using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Retail
{
    partial class ReceiptSearch
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
            this.txtReceiptNumber = new CustomTextBox();
            this.customLabel1 = new CustomLabel();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtReceiptNumber
            // 
            this.txtReceiptNumber.CausesValidation = false;
            this.txtReceiptNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.txtReceiptNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiptNumber.Location = new System.Drawing.Point(148, 80);
            this.txtReceiptNumber.Name = "txtReceiptNumber";
            this.txtReceiptNumber.Size = new System.Drawing.Size(122, 21);
            this.txtReceiptNumber.TabIndex = 1;
            this.txtReceiptNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Location = new System.Drawing.Point(73, 83);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(68, 13);
            this.customLabel1.TabIndex = 0;
            this.customLabel1.Text = "Enter MSR #";
            // 
            // submitButton
            // 
            this.submitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.submitButton.AutoSize = true;
            this.submitButton.BackColor = System.Drawing.Color.Transparent;
            this.submitButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submitButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(279, 128);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(100, 40);
            this.submitButton.TabIndex = 144;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(60, 128);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 40);
            this.cancelButton.TabIndex = 143;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(32, 40);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(35, 13);
            this.lblError.TabIndex = 145;
            this.lblError.Text = "label1";
            this.lblError.Visible = false;
            // 
            // ReceiptSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(439, 178);
            this.ControlBox = false;
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.txtReceiptNumber);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitButton);
            this.Name = "ReceiptSearch";
            this.Text = "";
            this.Load += new System.EventHandler(this.ReceiptSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTextBox txtReceiptNumber;
        private CustomLabel customLabel1;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label lblError;
    }
}