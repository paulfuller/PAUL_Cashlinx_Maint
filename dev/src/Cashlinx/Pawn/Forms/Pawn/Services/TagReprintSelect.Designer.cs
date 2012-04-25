using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services
{
    partial class TagReprintSelect
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.labelTicketHolderType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelTicketNumber = new System.Windows.Forms.Label();
            this.labelDocType = new System.Windows.Forms.Label();
            this.labelRetailPrice = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelBarCodeNumberICN = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.errorPanel = new System.Windows.Forms.Panel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.customTextBoxNumberToPrint = new CustomTextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxFeaturesPrint = new CustomTextBox();
            this.errorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelHeading.Location = new System.Drawing.Point(7, 18);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(159, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Bar Code Tag Reprint";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DimGray;
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(189, 133);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(2, 164);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(437, 345);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 50);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "C&ancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click_1);
            // 
            // buttonContinue
            // 
            this.buttonContinue.BackColor = System.Drawing.Color.Transparent;
            this.buttonContinue.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonContinue.FlatAppearance.BorderSize = 0;
            this.buttonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonContinue.ForeColor = System.Drawing.Color.White;
            this.buttonContinue.Location = new System.Drawing.Point(553, 345);
            this.buttonContinue.Margin = new System.Windows.Forms.Padding(4);
            this.buttonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(100, 50);
            this.buttonContinue.TabIndex = 11;
            this.buttonContinue.Text = "&Continue";
            this.buttonContinue.UseVisualStyleBackColor = false;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // labelTicketHolderType
            // 
            this.labelTicketHolderType.AutoSize = true;
            this.labelTicketHolderType.BackColor = System.Drawing.Color.Transparent;
            this.labelTicketHolderType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTicketHolderType.ForeColor = System.Drawing.Color.Black;
            this.labelTicketHolderType.Location = new System.Drawing.Point(46, 373);
            this.labelTicketHolderType.Name = "labelTicketHolderType";
            this.labelTicketHolderType.Size = new System.Drawing.Size(237, 14);
            this.labelTicketHolderType.TabIndex = 12;
            this.labelTicketHolderType.Text = "How man bar code tags to reprint :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 14);
            this.label1.TabIndex = 15;
            this.label1.Text = "Print Features Tag if available ? (Y/N) :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "Bar Code Number (ICN)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(62, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 14);
            this.label3.TabIndex = 17;
            this.label3.Text = "Item Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(130, 253);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 14);
            this.label4.TabIndex = 18;
            this.label4.Text = "Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(96, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "Retail Price";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(111, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 14);
            this.label6.TabIndex = 20;
            this.label6.Text = "Doc Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(76, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 14);
            this.label7.TabIndex = 21;
            this.label7.Text = "Ticket Number";
            // 
            // labelTicketNumber
            // 
            this.labelTicketNumber.AutoSize = true;
            this.labelTicketNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelTicketNumber.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTicketNumber.ForeColor = System.Drawing.Color.Black;
            this.labelTicketNumber.Location = new System.Drawing.Point(211, 164);
            this.labelTicketNumber.Name = "labelTicketNumber";
            this.labelTicketNumber.Size = new System.Drawing.Size(0, 14);
            this.labelTicketNumber.TabIndex = 27;
            this.labelTicketNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelDocType
            // 
            this.labelDocType.AutoSize = true;
            this.labelDocType.BackColor = System.Drawing.Color.Transparent;
            this.labelDocType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDocType.ForeColor = System.Drawing.Color.Black;
            this.labelDocType.Location = new System.Drawing.Point(211, 193);
            this.labelDocType.Name = "labelDocType";
            this.labelDocType.Size = new System.Drawing.Size(0, 14);
            this.labelDocType.TabIndex = 26;
            this.labelDocType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelRetailPrice
            // 
            this.labelRetailPrice.AutoSize = true;
            this.labelRetailPrice.BackColor = System.Drawing.Color.Transparent;
            this.labelRetailPrice.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRetailPrice.ForeColor = System.Drawing.Color.Black;
            this.labelRetailPrice.Location = new System.Drawing.Point(211, 223);
            this.labelRetailPrice.Name = "labelRetailPrice";
            this.labelRetailPrice.Size = new System.Drawing.Size(0, 14);
            this.labelRetailPrice.TabIndex = 25;
            this.labelRetailPrice.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.Black;
            this.labelStatus.Location = new System.Drawing.Point(211, 253);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 14);
            this.labelStatus.TabIndex = 24;
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelBarCodeNumberICN
            // 
            this.labelBarCodeNumberICN.AutoSize = true;
            this.labelBarCodeNumberICN.BackColor = System.Drawing.Color.Transparent;
            this.labelBarCodeNumberICN.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBarCodeNumberICN.ForeColor = System.Drawing.Color.Black;
            this.labelBarCodeNumberICN.Location = new System.Drawing.Point(211, 133);
            this.labelBarCodeNumberICN.Name = "labelBarCodeNumberICN";
            this.labelBarCodeNumberICN.Size = new System.Drawing.Size(0, 14);
            this.labelBarCodeNumberICN.TabIndex = 22;
            this.labelBarCodeNumberICN.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelDesc
            // 
            this.labelDesc.AutoSize = true;
            this.labelDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelDesc.Location = new System.Drawing.Point(211, 281);
            this.labelDesc.MaximumSize = new System.Drawing.Size(460, 56);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(0, 13);
            this.labelDesc.TabIndex = 30;
            // 
            // errorPanel
            // 
            this.errorPanel.BackColor = System.Drawing.Color.Transparent;
            this.errorPanel.Controls.Add(this.errorLabel);
            this.errorPanel.Location = new System.Drawing.Point(17, 56);
            this.errorPanel.Name = "errorPanel";
            this.errorPanel.Size = new System.Drawing.Size(655, 66);
            this.errorPanel.TabIndex = 31;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(3, 0);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(42, 16);
            this.errorLabel.TabIndex = 0;
            this.errorLabel.Text = "Error";
            this.errorLabel.Visible = false;
            // 
            // customTextBoxNumberToPrint
            // 
            this.customTextBoxNumberToPrint.AllowOnlyNumbers = true;
            this.customTextBoxNumberToPrint.BackColor = System.Drawing.SystemColors.Control;
            this.customTextBoxNumberToPrint.CausesValidation = false;
            this.customTextBoxNumberToPrint.ErrorMessage = "";
            this.customTextBoxNumberToPrint.Location = new System.Drawing.Point(285, 369);
            this.customTextBoxNumberToPrint.MaxLength = 6;
            this.customTextBoxNumberToPrint.Name = "customTextBoxNumberToPrint";
            this.customTextBoxNumberToPrint.RegularExpression = true;
            this.customTextBoxNumberToPrint.Required = true;
            this.customTextBoxNumberToPrint.Size = new System.Drawing.Size(30, 20);
            this.customTextBoxNumberToPrint.TabIndex = 28;
            this.customTextBoxNumberToPrint.ValidationExpression = "^\\d+$";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Last Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "First Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Address";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Date Of Birth";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "ID Type & Number";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 130;
            // 
            // textBoxFeaturesPrint
            // 
            this.textBoxFeaturesPrint.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxFeaturesPrint.CausesValidation = false;
            this.textBoxFeaturesPrint.ErrorMessage = "";
            this.textBoxFeaturesPrint.Location = new System.Drawing.Point(285, 343);
            this.textBoxFeaturesPrint.MaxLength = 6;
            this.textBoxFeaturesPrint.Name = "textBoxFeaturesPrint";
            this.textBoxFeaturesPrint.Required = true;
            this.textBoxFeaturesPrint.Size = new System.Drawing.Size(30, 20);
            this.textBoxFeaturesPrint.TabIndex = 33;
            this.textBoxFeaturesPrint.ValidationExpression = "";
            // 
            // TagReprintSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(688, 418);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxFeaturesPrint);
            this.Controls.Add(this.errorPanel);
            this.Controls.Add(this.labelDesc);
            this.Controls.Add(this.customTextBoxNumberToPrint);
            this.Controls.Add(this.labelTicketNumber);
            this.Controls.Add(this.labelDocType);
            this.Controls.Add(this.labelRetailPrice);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelBarCodeNumberICN);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTicketHolderType);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TagReprintSelect";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bar Code Tag Reprint";
            this.Load += new System.EventHandler(this.TagReprintSelect_Load);
            this.errorPanel.ResumeLayout(false);
            this.errorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label labelTicketHolderType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelTicketNumber;
        private System.Windows.Forms.Label labelDocType;
        private System.Windows.Forms.Label labelRetailPrice;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelBarCodeNumberICN;
        private CustomTextBox customTextBoxNumberToPrint;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.Panel errorPanel;
        private System.Windows.Forms.Label errorLabel;
        private CustomTextBox textBoxFeaturesPrint;
    }
}