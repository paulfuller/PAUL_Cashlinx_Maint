using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Ticket
{
    partial class LookupTicket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupTicket));
            this.labelHeading = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxTicketNumber = new CustomTextBox();
            this.labelHyphen = new System.Windows.Forms.Label();
            this.textBoxStoreNumber = new CustomTextBox();
            this.labelTicketNumber = new System.Windows.Forms.Label();
            this.errorPanel = new System.Windows.Forms.Panel();
            this.ddlProduct = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customButtonCancel = new CustomButton();
            this.customButtonClear = new CustomButton();
            this.customButtonFind = new CustomButton();
            this.panel1.SuspendLayout();
            this.errorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(275, 25);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(126, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Find Transaction";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBoxTicketNumber);
            this.panel1.Controls.Add(this.labelHyphen);
            this.panel1.Controls.Add(this.textBoxStoreNumber);
            this.panel1.Controls.Add(this.labelTicketNumber);
            this.panel1.Location = new System.Drawing.Point(10, 173);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 36);
            this.panel1.TabIndex = 1;
            // 
            // textBoxTicketNumber
            // 
            this.textBoxTicketNumber.AllowOnlyNumbers = true;
            this.textBoxTicketNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxTicketNumber.CausesValidation = false;
            this.textBoxTicketNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.textBoxTicketNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTicketNumber.ForeColor = System.Drawing.Color.Black;
            this.textBoxTicketNumber.Location = new System.Drawing.Point(222, 6);
            this.textBoxTicketNumber.MaxLength = 6;
            this.textBoxTicketNumber.Name = "textBoxTicketNumber";
            this.textBoxTicketNumber.Required = true;
            this.textBoxTicketNumber.Size = new System.Drawing.Size(76, 21);
            this.textBoxTicketNumber.TabIndex = 3;
            this.textBoxTicketNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // labelHyphen
            // 
            this.labelHyphen.AutoSize = true;
            this.labelHyphen.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHyphen.Location = new System.Drawing.Point(203, 8);
            this.labelHyphen.Name = "labelHyphen";
            this.labelHyphen.Size = new System.Drawing.Size(14, 16);
            this.labelHyphen.TabIndex = 2;
            this.labelHyphen.Text = "-";
            // 
            // textBoxStoreNumber
            // 
            this.textBoxStoreNumber.AllowOnlyNumbers = true;
            this.textBoxStoreNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxStoreNumber.CausesValidation = false;
            this.textBoxStoreNumber.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.textBoxStoreNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStoreNumber.ForeColor = System.Drawing.Color.Black;
            this.textBoxStoreNumber.Location = new System.Drawing.Point(143, 6);
            this.textBoxStoreNumber.MaxLength = 5;
            this.textBoxStoreNumber.Name = "textBoxStoreNumber";
            this.textBoxStoreNumber.Required = true;
            this.textBoxStoreNumber.Size = new System.Drawing.Size(55, 21);
            this.textBoxStoreNumber.TabIndex = 1;
            this.textBoxStoreNumber.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.textBoxStoreNumber.TextChanged += new System.EventHandler(this.textBoxStoreNumber_TextChanged);
            this.textBoxStoreNumber.Click += new System.EventHandler(this.textBoxStoreNumber_Click);
            // 
            // labelTicketNumber
            // 
            this.labelTicketNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTicketNumber.AutoSize = true;
            this.labelTicketNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTicketNumber.ForeColor = System.Drawing.Color.Black;
            this.labelTicketNumber.Location = new System.Drawing.Point(8, 8);
            this.labelTicketNumber.Name = "labelTicketNumber";
            this.labelTicketNumber.Size = new System.Drawing.Size(108, 16);
            this.labelTicketNumber.TabIndex = 0;
            this.labelTicketNumber.Text = "Ticket Number :";
            // 
            // errorPanel
            // 
            this.errorPanel.BackColor = System.Drawing.Color.Transparent;
            this.errorPanel.Controls.Add(this.ddlProduct);
            this.errorPanel.Controls.Add(this.label1);
            this.errorPanel.Controls.Add(this.errorLabel);
            this.errorPanel.Location = new System.Drawing.Point(11, 79);
            this.errorPanel.Name = "errorPanel";
            this.errorPanel.Size = new System.Drawing.Size(655, 88);
            this.errorPanel.TabIndex = 0;
            // 
            // ddlProduct
            // 
            this.ddlProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlProduct.FormattingEnabled = true;
            this.ddlProduct.Items.AddRange(new object[] {
            "Pawn Loan",
            "Layaway"});
            this.ddlProduct.Location = new System.Drawing.Point(143, 54);
            this.ddlProduct.Name = "ddlProduct";
            this.ddlProduct.Size = new System.Drawing.Size(155, 21);
            this.ddlProduct.TabIndex = 2;
            this.ddlProduct.SelectedIndexChanged += new System.EventHandler(this.ddlProduct_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(48, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Product :";
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(140, 20);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(41, 16);
            this.errorLabel.TabIndex = 0;
            this.errorLabel.Text = "Error";
            this.errorLabel.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(0, 320);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(673, 2);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
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
            this.customButtonCancel.Location = new System.Drawing.Point(313, 326);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 2;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // customButtonClear
            // 
            this.customButtonClear.BackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonClear.BackgroundImage")));
            this.customButtonClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonClear.FlatAppearance.BorderSize = 0;
            this.customButtonClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonClear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonClear.ForeColor = System.Drawing.Color.White;
            this.customButtonClear.Location = new System.Drawing.Point(423, 325);
            this.customButtonClear.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClear.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.Name = "customButtonClear";
            this.customButtonClear.Size = new System.Drawing.Size(100, 50);
            this.customButtonClear.TabIndex = 3;
            this.customButtonClear.Text = "C&lear";
            this.customButtonClear.UseVisualStyleBackColor = false;
            this.customButtonClear.Click += new System.EventHandler(this.buttonClear_Click);
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
            this.customButtonFind.Location = new System.Drawing.Point(534, 326);
            this.customButtonFind.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonFind.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.Name = "customButtonFind";
            this.customButtonFind.Size = new System.Drawing.Size(100, 50);
            this.customButtonFind.TabIndex = 4;
            this.customButtonFind.Text = "&Find";
            this.customButtonFind.UseVisualStyleBackColor = false;
            this.customButtonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // LookupTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_400_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(677, 396);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonFind);
            this.Controls.Add(this.customButtonClear);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.errorPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupTicket";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LookupTicket";
            this.Load += new System.EventHandler(this.LookupTicket_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.errorPanel.ResumeLayout(false);
            this.errorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTicketNumber;
        private CustomTextBox textBoxStoreNumber;
        private CustomTextBox textBoxTicketNumber;
        private System.Windows.Forms.Label labelHyphen;
        private System.Windows.Forms.Panel errorPanel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomButton customButtonCancel;
        private CustomButton customButtonClear;
        private CustomButton customButtonFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlProduct;
    }
}