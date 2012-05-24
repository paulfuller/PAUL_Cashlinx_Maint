using Common.Libraries.Forms.Components;

namespace Support.Forms.Customer.Pawn.Loan
{
    partial class TenderCash
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
           this.labelFormHeading = new System.Windows.Forms.Label();
           this.labelDueFromCustomerHeading = new System.Windows.Forms.Label();
           this.labelCashInHeading = new System.Windows.Forms.Label();
           this.labelChangeDueCustomerHeading = new System.Windows.Forms.Label();
           this.groupBox1 = new System.Windows.Forms.GroupBox();
           this.labelCashIn = new System.Windows.Forms.Label();
           this.labelChangeDueCustomer = new System.Windows.Forms.Label();
           this.buttonCancel = new System.Windows.Forms.Button();
           this.buttonContinue = new System.Windows.Forms.Button();
           this.textBoxDueFromCustomer = new System.Windows.Forms.TextBox();
           this.label1 = new System.Windows.Forms.Label();
           this.label2 = new System.Windows.Forms.Label();
           this.customTextBoxCashIn = new CustomTextBox();
           this.SuspendLayout();
           // 
           // labelFormHeading
           // 
           this.labelFormHeading.AutoSize = true;
           this.labelFormHeading.BackColor = System.Drawing.Color.Transparent;
           this.labelFormHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.labelFormHeading.ForeColor = System.Drawing.Color.White;
           this.labelFormHeading.Location = new System.Drawing.Point(206, 19);
           this.labelFormHeading.Name = "labelFormHeading";
           this.labelFormHeading.Size = new System.Drawing.Size(118, 19);
           this.labelFormHeading.TabIndex = 0;
           this.labelFormHeading.Text = "Tender Cash In";
           // 
           // labelDueFromCustomerHeading
           // 
           this.labelDueFromCustomerHeading.AutoSize = true;
           this.labelDueFromCustomerHeading.BackColor = System.Drawing.Color.Transparent;
           this.labelDueFromCustomerHeading.Location = new System.Drawing.Point(93, 55);
           this.labelDueFromCustomerHeading.Name = "labelDueFromCustomerHeading";
           this.labelDueFromCustomerHeading.Size = new System.Drawing.Size(106, 13);
           this.labelDueFromCustomerHeading.TabIndex = 1;
           this.labelDueFromCustomerHeading.Text = "Due From Customer:";
           // 
           // labelCashInHeading
           // 
           this.labelCashInHeading.AutoSize = true;
           this.labelCashInHeading.BackColor = System.Drawing.Color.Transparent;
           this.labelCashInHeading.Location = new System.Drawing.Point(93, 85);
           this.labelCashInHeading.Name = "labelCashInHeading";
           this.labelCashInHeading.Size = new System.Drawing.Size(48, 13);
           this.labelCashInHeading.TabIndex = 2;
           this.labelCashInHeading.Text = "Cash In:";
           // 
           // labelChangeDueCustomerHeading
           // 
           this.labelChangeDueCustomerHeading.AutoSize = true;
           this.labelChangeDueCustomerHeading.BackColor = System.Drawing.Color.Transparent;
           this.labelChangeDueCustomerHeading.Location = new System.Drawing.Point(93, 132);
           this.labelChangeDueCustomerHeading.Name = "labelChangeDueCustomerHeading";
           this.labelChangeDueCustomerHeading.Size = new System.Drawing.Size(119, 13);
           this.labelChangeDueCustomerHeading.TabIndex = 3;
           this.labelChangeDueCustomerHeading.Text = "Change Due Customer:";
           // 
           // groupBox1
           // 
           this.groupBox1.BackColor = System.Drawing.Color.Black;
           this.groupBox1.Location = new System.Drawing.Point(237, 112);
           this.groupBox1.Name = "groupBox1";
           this.groupBox1.Size = new System.Drawing.Size(200, 2);
           this.groupBox1.TabIndex = 4;
           this.groupBox1.TabStop = false;
           // 
           // labelCashIn
           // 
           this.labelCashIn.AutoSize = true;
           this.labelCashIn.BackColor = System.Drawing.Color.Transparent;
           this.labelCashIn.Location = new System.Drawing.Point(269, 85);
           this.labelCashIn.Name = "labelCashIn";
           this.labelCashIn.Size = new System.Drawing.Size(13, 13);
           this.labelCashIn.TabIndex = 6;
           this.labelCashIn.Text = "$";
           // 
           // labelChangeDueCustomer
           // 
           this.labelChangeDueCustomer.AutoSize = true;
           this.labelChangeDueCustomer.BackColor = System.Drawing.Color.Transparent;
           this.labelChangeDueCustomer.Location = new System.Drawing.Point(313, 132);
           this.labelChangeDueCustomer.Name = "labelChangeDueCustomer";
           this.labelChangeDueCustomer.Size = new System.Drawing.Size(29, 13);
           this.labelChangeDueCustomer.TabIndex = 7;
           this.labelChangeDueCustomer.Text = "0.00";
           this.labelChangeDueCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
           // 
           // buttonCancel
           // 
           this.buttonCancel.BackColor = System.Drawing.Color.Transparent;
           this.buttonCancel.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
           this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
           this.buttonCancel.CausesValidation = false;
           this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
           this.buttonCancel.FlatAppearance.BorderSize = 0;
           this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
           this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
           this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.buttonCancel.ForeColor = System.Drawing.Color.White;
           this.buttonCancel.Location = new System.Drawing.Point(26, 212);
           this.buttonCancel.Margin = new System.Windows.Forms.Padding(0);
           this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
           this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
           this.buttonCancel.Name = "buttonCancel";
           this.buttonCancel.Size = new System.Drawing.Size(100, 50);
           this.buttonCancel.TabIndex = 2;
           this.buttonCancel.Text = "&Cancel";
           this.buttonCancel.UseVisualStyleBackColor = false;
           this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
           // 
           // buttonContinue
           // 
           this.buttonContinue.BackColor = System.Drawing.Color.Transparent;
           this.buttonContinue.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
           this.buttonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
           this.buttonContinue.CausesValidation = false;
           this.buttonContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           this.buttonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
           this.buttonContinue.FlatAppearance.BorderSize = 0;
           this.buttonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
           this.buttonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
           this.buttonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
           this.buttonContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.buttonContinue.ForeColor = System.Drawing.Color.White;
           this.buttonContinue.Location = new System.Drawing.Point(404, 212);
           this.buttonContinue.Margin = new System.Windows.Forms.Padding(0);
           this.buttonContinue.MaximumSize = new System.Drawing.Size(100, 50);
           this.buttonContinue.MinimumSize = new System.Drawing.Size(100, 50);
           this.buttonContinue.Name = "buttonContinue";
           this.buttonContinue.Size = new System.Drawing.Size(100, 50);
           this.buttonContinue.TabIndex = 3;
           this.buttonContinue.Text = "C&alculate";
           this.buttonContinue.UseVisualStyleBackColor = false;
           this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
           // 
           // textBoxDueFromCustomer
           // 
           this.textBoxDueFromCustomer.BackColor = System.Drawing.Color.Silver;
           this.textBoxDueFromCustomer.ForeColor = System.Drawing.Color.Black;
           this.textBoxDueFromCustomer.Location = new System.Drawing.Point(283, 51);
           this.textBoxDueFromCustomer.Name = "textBoxDueFromCustomer";
           this.textBoxDueFromCustomer.ReadOnly = true;
           this.textBoxDueFromCustomer.Size = new System.Drawing.Size(59, 21);
           this.textBoxDueFromCustomer.TabIndex = 54;
           this.textBoxDueFromCustomer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
           // 
           // label1
           // 
           this.label1.AutoSize = true;
           this.label1.BackColor = System.Drawing.Color.Transparent;
           this.label1.Location = new System.Drawing.Point(269, 55);
           this.label1.Name = "label1";
           this.label1.Size = new System.Drawing.Size(13, 13);
           this.label1.TabIndex = 55;
           this.label1.Text = "$";
           // 
           // label2
           // 
           this.label2.AutoSize = true;
           this.label2.BackColor = System.Drawing.Color.Transparent;
           this.label2.Location = new System.Drawing.Point(269, 132);
           this.label2.Name = "label2";
           this.label2.Size = new System.Drawing.Size(13, 13);
           this.label2.TabIndex = 56;
           this.label2.Text = "$";
           // 
           // customTextBoxCashIn
           // 
           this.customTextBoxCashIn.AllowDecimalNumbers = true;
           this.customTextBoxCashIn.BackColor = System.Drawing.Color.WhiteSmoke;
           this.customTextBoxCashIn.CausesValidation = false;
           this.customTextBoxCashIn.ErrorMessage = "";
           this.customTextBoxCashIn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.customTextBoxCashIn.ForeColor = System.Drawing.Color.Black;
           this.customTextBoxCashIn.Location = new System.Drawing.Point(283, 84);
           this.customTextBoxCashIn.MaxLength = 8;
           this.customTextBoxCashIn.Name = "customTextBoxCashIn";
           this.customTextBoxCashIn.RegularExpression = true;
           this.customTextBoxCashIn.Size = new System.Drawing.Size(59, 21);
           this.customTextBoxCashIn.TabIndex = 1;
           this.customTextBoxCashIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
           this.customTextBoxCashIn.ValidationExpression = "[0-9]{1,}";
           this.customTextBoxCashIn.Leave += new System.EventHandler(this.customTextBoxCashIn_Leave);
           // 
           // TenderCash
           // 
           this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
           this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           this.BackColor = System.Drawing.Color.White;
           this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
           this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
           this.ClientSize = new System.Drawing.Size(530, 277);
           this.ControlBox = false;
           this.Controls.Add(this.label2);
           this.Controls.Add(this.label1);
           this.Controls.Add(this.textBoxDueFromCustomer);
           this.Controls.Add(this.customTextBoxCashIn);
           this.Controls.Add(this.buttonContinue);
           this.Controls.Add(this.buttonCancel);
           this.Controls.Add(this.labelChangeDueCustomer);
           this.Controls.Add(this.labelCashIn);
           this.Controls.Add(this.groupBox1);
           this.Controls.Add(this.labelChangeDueCustomerHeading);
           this.Controls.Add(this.labelCashInHeading);
           this.Controls.Add(this.labelDueFromCustomerHeading);
           this.Controls.Add(this.labelFormHeading);
           this.DoubleBuffered = true;
           this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           this.ForeColor = System.Drawing.Color.Black;
           this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.Name = "TenderCash";
           this.ShowIcon = false;
           this.ShowInTaskbar = false;
           this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
           this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
           this.Text = "TenderCash";
           this.Load += new System.EventHandler(this.TenderCash_Load);
           this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TenderCash_FormClosing);
           this.ResumeLayout(false);
           this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFormHeading;
        private System.Windows.Forms.Label labelDueFromCustomerHeading;
        private System.Windows.Forms.Label labelCashInHeading;
        private System.Windows.Forms.Label labelChangeDueCustomerHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelCashIn;
        private System.Windows.Forms.Label labelChangeDueCustomer;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonContinue;
        private CustomTextBox customTextBoxCashIn;
        private System.Windows.Forms.TextBox textBoxDueFromCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
