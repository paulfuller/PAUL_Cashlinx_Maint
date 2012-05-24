using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Ticket
{
    partial class LookupTicketResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupTicketResults));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelHeading = new System.Windows.Forms.Label();
            this.tktNumberLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTicketHolderType = new System.Windows.Forms.Label();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.customButtonContinue = new CustomButton();
            this.customButtonView = new CustomButton();
            this.customButtonBack = new CustomButton();
            this.customDataGridViewTicketResults = new CustomDataGridView();
            this.custlastname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.custfirstname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewTicketResults)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(142, 13);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(263, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Customer Information for Ticket No";
            // 
            // tktNumberLabel
            // 
            this.tktNumberLabel.AutoSize = true;
            this.tktNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.tktNumberLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tktNumberLabel.ForeColor = System.Drawing.Color.White;
            this.tktNumberLabel.Location = new System.Drawing.Point(473, 13);
            this.tktNumberLabel.Name = "tktNumberLabel";
            this.tktNumberLabel.Size = new System.Drawing.Size(66, 19);
            this.tktNumberLabel.TabIndex = 1;
            this.tktNumberLabel.Text = "Number";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Highlight;
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(8, 211);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 2);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // labelTicketHolderType
            // 
            this.labelTicketHolderType.AutoSize = true;
            this.labelTicketHolderType.BackColor = System.Drawing.Color.Transparent;
            this.labelTicketHolderType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTicketHolderType.ForeColor = System.Drawing.Color.Red;
            this.labelTicketHolderType.Location = new System.Drawing.Point(73, 135);
            this.labelTicketHolderType.Name = "labelTicketHolderType";
            this.labelTicketHolderType.Size = new System.Drawing.Size(295, 14);
            this.labelTicketHolderType.TabIndex = 12;
            this.labelTicketHolderType.Text = "Is Ticket Holder and Pledgor the Same Person?";
            // 
            // radioButtonYes
            // 
            this.radioButtonYes.AutoSize = true;
            this.radioButtonYes.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonYes.Checked = true;
            this.radioButtonYes.Location = new System.Drawing.Point(437, 134);
            this.radioButtonYes.Name = "radioButtonYes";
            this.radioButtonYes.Size = new System.Drawing.Size(42, 17);
            this.radioButtonYes.TabIndex = 13;
            this.radioButtonYes.TabStop = true;
            this.radioButtonYes.Text = "Yes";
            this.radioButtonYes.UseVisualStyleBackColor = false;
            // 
            // radioButtonNo
            // 
            this.radioButtonNo.AutoSize = true;
            this.radioButtonNo.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonNo.Location = new System.Drawing.Point(504, 134);
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.Size = new System.Drawing.Size(38, 17);
            this.radioButtonNo.TabIndex = 14;
            this.radioButtonNo.TabStop = true;
            this.radioButtonNo.Text = "No";
            this.radioButtonNo.UseVisualStyleBackColor = false;
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
            this.customButtonContinue.Location = new System.Drawing.Point(647, 233);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 18;
            this.customButtonContinue.Text = "&Continue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // customButtonView
            // 
            this.customButtonView.BackColor = System.Drawing.Color.Transparent;
            this.customButtonView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonView.BackgroundImage")));
            this.customButtonView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonView.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonView.FlatAppearance.BorderSize = 0;
            this.customButtonView.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonView.ForeColor = System.Drawing.Color.White;
            this.customButtonView.Location = new System.Drawing.Point(530, 233);
            this.customButtonView.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonView.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonView.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonView.Name = "customButtonView";
            this.customButtonView.Size = new System.Drawing.Size(100, 50);
            this.customButtonView.TabIndex = 17;
            this.customButtonView.Text = "&View";
            this.customButtonView.UseVisualStyleBackColor = false;
            this.customButtonView.Click += new System.EventHandler(this.buttonView_Click);
            // 
            // customButtonBack
            // 
            this.customButtonBack.BackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonBack.BackgroundImage")));
            this.customButtonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonBack.FlatAppearance.BorderSize = 0;
            this.customButtonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonBack.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonBack.ForeColor = System.Drawing.Color.White;
            this.customButtonBack.Location = new System.Drawing.Point(18, 231);
            this.customButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.Name = "customButtonBack";
            this.customButtonBack.Size = new System.Drawing.Size(100, 50);
            this.customButtonBack.TabIndex = 16;
            this.customButtonBack.Text = "&Back";
            this.customButtonBack.UseVisualStyleBackColor = false;
            this.customButtonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // customDataGridViewTicketResults
            // 
            this.customDataGridViewTicketResults.AllowUserToAddRows = false;
            this.customDataGridViewTicketResults.AllowUserToDeleteRows = false;
            this.customDataGridViewTicketResults.AllowUserToResizeColumns = false;
            this.customDataGridViewTicketResults.AllowUserToResizeRows = false;
            this.customDataGridViewTicketResults.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewTicketResults.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewTicketResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridViewTicketResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewTicketResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.custlastname,
            this.custfirstname,
            this.address,
            this.dob,
            this.IDData});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewTicketResults.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridViewTicketResults.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewTicketResults.Location = new System.Drawing.Point(18, 47);
            this.customDataGridViewTicketResults.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewTicketResults.Name = "customDataGridViewTicketResults";
            this.customDataGridViewTicketResults.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewTicketResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridViewTicketResults.RowHeadersVisible = false;
            this.customDataGridViewTicketResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewTicketResults.Size = new System.Drawing.Size(742, 46);
            this.customDataGridViewTicketResults.TabIndex = 15;
            // 
            // custlastname
            // 
            this.custlastname.HeaderText = "Last Name";
            this.custlastname.Name = "custlastname";
            this.custlastname.ReadOnly = true;
            this.custlastname.Width = 148;
            // 
            // custfirstname
            // 
            this.custfirstname.HeaderText = "First Name";
            this.custfirstname.Name = "custfirstname";
            this.custfirstname.ReadOnly = true;
            this.custfirstname.Width = 148;
            // 
            // address
            // 
            this.address.HeaderText = "Address";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            this.address.Width = 148;
            // 
            // dob
            // 
            this.dob.HeaderText = "Date Of Birth";
            this.dob.Name = "dob";
            this.dob.ReadOnly = true;
            // 
            // IDData
            // 
            this.IDData.HeaderText = "ID Type & Number";
            this.IDData.Name = "IDData";
            this.IDData.ReadOnly = true;
            this.IDData.Width = 148;
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
            // LookupTicketResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(778, 292);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.customButtonView);
            this.Controls.Add(this.customButtonBack);
            this.Controls.Add(this.customDataGridViewTicketResults);
            this.Controls.Add(this.radioButtonNo);
            this.Controls.Add(this.radioButtonYes);
            this.Controls.Add(this.labelTicketHolderType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tktNumberLabel);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupTicketResults";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lookup Ticket Results";
            this.Load += new System.EventHandler(this.LookupTicketResults_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewTicketResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label tktNumberLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label labelTicketHolderType;
        private System.Windows.Forms.RadioButton radioButtonYes;
        private System.Windows.Forms.RadioButton radioButtonNo;
        private CustomDataGridView customDataGridViewTicketResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn custlastname;
        private System.Windows.Forms.DataGridViewTextBoxColumn custfirstname;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn dob;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDData;
        private CustomButton customButtonBack;
        private CustomButton customButtonView;
        private CustomButton customButtonContinue;
    }
}