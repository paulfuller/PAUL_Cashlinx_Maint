using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Return
{
    partial class BuyReturn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuyReturn));
            this.labelHeading = new System.Windows.Forms.Label();
            this.radioButtonBuyNo = new System.Windows.Forms.RadioButton();
            this.radioButtonICN = new System.Windows.Forms.RadioButton();
            this.customTextBoxBuyNo = new CustomTextBox();
            this.customTextBoxICN = new CustomTextBox();
            this.labelStoreNo = new System.Windows.Forms.Label();
            this.customButtonCancel = new CustomButton();
            this.customButtonContinue = new CustomButton();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(26, 25);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(74, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Find Items";
            // 
            // radioButtonBuyNo
            // 
            this.radioButtonBuyNo.AutoSize = true;
            this.radioButtonBuyNo.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonBuyNo.Location = new System.Drawing.Point(29, 104);
            this.radioButtonBuyNo.Name = "radioButtonBuyNo";
            this.radioButtonBuyNo.Size = new System.Drawing.Size(121, 17);
            this.radioButtonBuyNo.TabIndex = 3;
            this.radioButtonBuyNo.Text = "Shop / Buy Number:";
            this.radioButtonBuyNo.UseVisualStyleBackColor = false;
            // 
            // radioButtonICN
            // 
            this.radioButtonICN.AutoSize = true;
            this.radioButtonICN.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonICN.Location = new System.Drawing.Point(29, 146);
            this.radioButtonICN.Name = "radioButtonICN";
            this.radioButtonICN.Size = new System.Drawing.Size(47, 17);
            this.radioButtonICN.TabIndex = 4;
            this.radioButtonICN.Text = "ICN:";
            this.radioButtonICN.UseVisualStyleBackColor = false;
            // 
            // customTextBoxBuyNo
            // 
            this.customTextBoxBuyNo.AllowOnlyNumbers = true;
            this.customTextBoxBuyNo.CausesValidation = false;
            this.customTextBoxBuyNo.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxBuyNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxBuyNo.Location = new System.Drawing.Point(250, 103);
            this.customTextBoxBuyNo.MaxLength = 6;
            this.customTextBoxBuyNo.Name = "customTextBoxBuyNo";
            this.customTextBoxBuyNo.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxBuyNo.TabIndex = 1;
            this.customTextBoxBuyNo.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxBuyNo.Click += new System.EventHandler(this.customTextBoxBuyNo_Click);
            this.customTextBoxBuyNo.GotFocus += new System.EventHandler(this.customTextBoxBuyNo_Click);
            // 
            // customTextBoxICN
            // 
            this.customTextBoxICN.CausesValidation = false;
            this.customTextBoxICN.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxICN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxICN.Location = new System.Drawing.Point(183, 146);
            this.customTextBoxICN.Name = "customTextBoxICN";
            this.customTextBoxICN.Size = new System.Drawing.Size(167, 21);
            this.customTextBoxICN.TabIndex = 2;
            this.customTextBoxICN.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxICN.Click += new System.EventHandler(this.customTextBoxICN_Click);
            this.customTextBoxICN.GotFocus += new System.EventHandler(this.customTextBoxICN_Click);
            // 
            // labelStoreNo
            // 
            this.labelStoreNo.AutoSize = true;
            this.labelStoreNo.BackColor = System.Drawing.Color.Transparent;
            this.labelStoreNo.Location = new System.Drawing.Point(191, 106);
            this.labelStoreNo.Name = "labelStoreNo";
            this.labelStoreNo.Size = new System.Drawing.Size(37, 13);
            this.labelStoreNo.TabIndex = 5;
            this.labelStoreNo.Text = "02030";
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
            this.customButtonCancel.Location = new System.Drawing.Point(29, 240);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 6;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonContinue
            // 
            this.customButtonContinue.BackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonContinue.BackgroundImage")));
            this.customButtonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonContinue.FlatAppearance.BorderSize = 0;
            this.customButtonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonContinue.ForeColor = System.Drawing.Color.White;
            this.customButtonContinue.Location = new System.Drawing.Point(320, 240);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 7;
            this.customButtonContinue.Text = "Continue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.customButtonContinue_Click);
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.BackColor = System.Drawing.Color.Transparent;
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Location = new System.Drawing.Point(26, 72);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(76, 13);
            this.labelErrorMessage.TabIndex = 11;
            this.labelErrorMessage.Text = "Error Message";
            this.labelErrorMessage.Visible = false;
            // 
            // BuyReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 320);
            this.Controls.Add(this.labelErrorMessage);
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.labelStoreNo);
            this.Controls.Add(this.customTextBoxICN);
            this.Controls.Add(this.customTextBoxBuyNo);
            this.Controls.Add(this.radioButtonICN);
            this.Controls.Add(this.radioButtonBuyNo);
            this.Controls.Add(this.labelHeading);
            this.Name = "BuyReturn";
            this.Text = "BuyReturn";
            this.Load += new System.EventHandler(this.BuyReturn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.RadioButton radioButtonBuyNo;
        private System.Windows.Forms.RadioButton radioButtonICN;
        private CustomTextBox customTextBoxBuyNo;
        private CustomTextBox customTextBoxICN;
        private System.Windows.Forms.Label labelStoreNo;
        private CustomButton customButtonCancel;
        private CustomButton customButtonContinue;
        private System.Windows.Forms.Label labelErrorMessage;
    }
}