using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    partial class ChargeOffReplacementData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChargeOffReplacementData));
            this.customLabel1 = new CustomLabel();
            this.CustomerLabel = new CustomLabel();
            this.customLabel2 = new CustomLabel();
            this.customButtonSubmit = new CustomButton();
            this.customTextBoxLoanNo = new CustomTextBox();
            this.customLabelCustName = new CustomLabel();
            this.customLabelReplacedICN = new CustomLabel();
            this.customButtonFind = new CustomButton();
            this.labelHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Location = new System.Drawing.Point(81, 93);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(74, 13);
            this.customLabel1.TabIndex = 0;
            this.customLabel1.Text = "Loan Number:";
            // 
            // CustomerLabel
            // 
            this.CustomerLabel.AutoSize = true;
            this.CustomerLabel.BackColor = System.Drawing.Color.Transparent;
            this.CustomerLabel.Location = new System.Drawing.Point(98, 128);
            this.CustomerLabel.Name = "CustomerLabel";
            this.CustomerLabel.Size = new System.Drawing.Size(57, 13);
            this.CustomerLabel.TabIndex = 1;
            this.CustomerLabel.Text = "Customer:";
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.Location = new System.Drawing.Point(84, 164);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(76, 13);
            this.customLabel2.TabIndex = 2;
            this.customLabel2.Text = "ICN Replaced:";
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
            this.customButtonSubmit.Location = new System.Drawing.Point(186, 202);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 9;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
            // 
            // customTextBoxLoanNo
            // 
            this.customTextBoxLoanNo.CausesValidation = false;
            this.customTextBoxLoanNo.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxLoanNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxLoanNo.Location = new System.Drawing.Point(186, 86);
            this.customTextBoxLoanNo.Name = "customTextBoxLoanNo";
            this.customTextBoxLoanNo.Size = new System.Drawing.Size(112, 21);
            this.customTextBoxLoanNo.TabIndex = 10;
            this.customTextBoxLoanNo.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customLabelCustName
            // 
            this.customLabelCustName.AutoSize = true;
            this.customLabelCustName.BackColor = System.Drawing.Color.Transparent;
            this.customLabelCustName.Location = new System.Drawing.Point(186, 128);
            this.customLabelCustName.Name = "customLabelCustName";
            this.customLabelCustName.Size = new System.Drawing.Size(0, 13);
            this.customLabelCustName.TabIndex = 11;
            // 
            // customLabelReplacedICN
            // 
            this.customLabelReplacedICN.AutoSize = true;
            this.customLabelReplacedICN.BackColor = System.Drawing.Color.Transparent;
            this.customLabelReplacedICN.Location = new System.Drawing.Point(186, 164);
            this.customLabelReplacedICN.Name = "customLabelReplacedICN";
            this.customLabelReplacedICN.Size = new System.Drawing.Size(0, 13);
            this.customLabelReplacedICN.TabIndex = 12;
            // 
            // customButtonFind
            // 
            this.customButtonFind.BackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonFind.BackgroundImage")));
            this.customButtonFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonFind.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonFind.FlatAppearance.BorderSize = 0;
            this.customButtonFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonFind.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonFind.ForeColor = System.Drawing.Color.White;
            this.customButtonFind.Location = new System.Drawing.Point(330, 70);
            this.customButtonFind.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonFind.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.Name = "customButtonFind";
            this.customButtonFind.Size = new System.Drawing.Size(100, 50);
            this.customButtonFind.TabIndex = 13;
            this.customButtonFind.Text = "Find";
            this.customButtonFind.UseVisualStyleBackColor = false;
            this.customButtonFind.Click += new System.EventHandler(this.customButtonFind_Click);
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.BackColor = System.Drawing.Color.Transparent;
            this.labelHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.ForeColor = System.Drawing.Color.White;
            this.labelHeader.Location = new System.Drawing.Point(98, 20);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(149, 16);
            this.labelHeader.TabIndex = 14;
            this.labelHeader.Text = "Replacement of Property";
            // 
            // ChargeOffReplacementData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 263);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.customButtonFind);
            this.Controls.Add(this.customLabelReplacedICN);
            this.Controls.Add(this.customLabelCustName);
            this.Controls.Add(this.customTextBoxLoanNo);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.CustomerLabel);
            this.Controls.Add(this.customLabel1);
            this.Name = "ChargeOffReplacementData";
            this.Text = "ChargeOffReplacementData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomLabel customLabel1;
        private CustomLabel CustomerLabel;
        private CustomLabel customLabel2;
        private CustomButton customButtonSubmit;
        private CustomTextBox customTextBoxLoanNo;
        private CustomLabel customLabelCustName;
        private CustomLabel customLabelReplacedICN;
        private CustomButton customButtonFind;
        private System.Windows.Forms.Label labelHeader;
    }
}
