using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    partial class ChargeOffPoliceInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChargeOffPoliceInfo));
            this.customLabel1 = new CustomLabel();
            this.customLabel2 = new CustomLabel();
            this.customTextBoxATF = new CustomTextBox();
            this.customTextBoxCaseNo = new CustomTextBox();
            this.customButton1 = new CustomButton();
            this.SuspendLayout();
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Location = new System.Drawing.Point(120, 79);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(112, 13);
            this.customLabel1.TabIndex = 0;
            this.customLabel1.Text = "ATF Incident Number:";
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.Location = new System.Drawing.Point(87, 110);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(161, 13);
            this.customLabel2.TabIndex = 1;
            this.customLabel2.Text = "Law Enforcement Case Number:";
            // 
            // customTextBoxATF
            // 
            this.customTextBoxATF.CausesValidation = false;
            this.customTextBoxATF.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxATF.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxATF.Location = new System.Drawing.Point(280, 79);
            this.customTextBoxATF.MaxLength = 20;
            this.customTextBoxATF.Name = "customTextBoxATF";
            this.customTextBoxATF.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxATF.TabIndex = 2;
            this.customTextBoxATF.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxCaseNo
            // 
            this.customTextBoxCaseNo.CausesValidation = false;
            this.customTextBoxCaseNo.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCaseNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCaseNo.Location = new System.Drawing.Point(280, 110);
            this.customTextBoxCaseNo.MaxLength = 15;
            this.customTextBoxCaseNo.Name = "customTextBoxCaseNo";
            this.customTextBoxCaseNo.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCaseNo.TabIndex = 3;
            this.customTextBoxCaseNo.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.Color.Transparent;
            this.customButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButton1.BackgroundImage")));
            this.customButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButton1.FlatAppearance.BorderSize = 0;
            this.customButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.White;
            this.customButton1.Location = new System.Drawing.Point(216, 148);
            this.customButton1.Margin = new System.Windows.Forms.Padding(0);
            this.customButton1.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButton1.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(100, 50);
            this.customButton1.TabIndex = 9;
            this.customButton1.Text = "Submit";
            this.customButton1.UseVisualStyleBackColor = false;
            this.customButton1.Click += new System.EventHandler(this.customButton1_Click);
            // 
            // ChargeOffPoliceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 214);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.customTextBoxCaseNo);
            this.Controls.Add(this.customTextBoxATF);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.customLabel1);
            this.Name = "ChargeOffPoliceInfo";
            this.Text = "ChargeOffPoliceInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomLabel customLabel1;
        private CustomLabel customLabel2;
        private CustomTextBox customTextBoxATF;
        private CustomTextBox customTextBoxCaseNo;
        private CustomButton customButton1;
    }
}