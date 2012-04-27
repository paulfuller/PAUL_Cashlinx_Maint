using Common.Libraries.Forms.Components;

namespace Support.Forms.Pawn.Customer
{
    partial class ExistingCustomer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExistingCustomer));
            this.labelExistingCustomerInfoHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelRedHeader = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelExistingCustomerHeading = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelNewCustomerHeader = new System.Windows.Forms.Label();
            this.customDataGridViewExistingCustomer = new CustomDataGridView();
            this.customDataGridViewNewCustomer = new CustomDataGridView();
            this.customButtonBack = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.customButtonUpdate = new CustomButton();
            this.customButtonContinue = new CustomButton();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewExistingCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewNewCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // labelExistingCustomerInfoHeader
            // 
            this.labelExistingCustomerInfoHeader.AutoSize = true;
            this.labelExistingCustomerInfoHeader.BackColor = System.Drawing.Color.Transparent;
            this.labelExistingCustomerInfoHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExistingCustomerInfoHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelExistingCustomerInfoHeader.Location = new System.Drawing.Point(300, 29);
            this.labelExistingCustomerInfoHeader.Name = "labelExistingCustomerInfoHeader";
            this.labelExistingCustomerInfoHeader.Size = new System.Drawing.Size(226, 19);
            this.labelExistingCustomerInfoHeader.TabIndex = 0;
            this.labelExistingCustomerInfoHeader.Text = "Existing Customer Information";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.labelMessage);
            this.panel1.Controls.Add(this.labelRedHeader);
            this.panel1.Location = new System.Drawing.Point(7, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(808, 73);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(678, 24);
            this.panel2.TabIndex = 4;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.Location = new System.Drawing.Point(18, 33);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(49, 13);
            this.labelMessage.TabIndex = 1;
            this.labelMessage.Text = "message";
            // 
            // labelRedHeader
            // 
            this.labelRedHeader.AutoSize = true;
            this.labelRedHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRedHeader.ForeColor = System.Drawing.Color.Red;
            this.labelRedHeader.Location = new System.Drawing.Point(48, 10);
            this.labelRedHeader.Name = "labelRedHeader";
            this.labelRedHeader.Size = new System.Drawing.Size(75, 14);
            this.labelRedHeader.TabIndex = 0;
            this.labelRedHeader.Text = "headertext";
            // 
            // panel3
            // 
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.BackColor = System.Drawing.Color.Blue;
            this.panel3.Controls.Add(this.labelExistingCustomerHeading);
            this.panel3.Location = new System.Drawing.Point(7, 309);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(808, 24);
            this.panel3.TabIndex = 5;
            // 
            // labelExistingCustomerHeading
            // 
            this.labelExistingCustomerHeading.AutoSize = true;
            this.labelExistingCustomerHeading.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExistingCustomerHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelExistingCustomerHeading.Location = new System.Drawing.Point(6, 4);
            this.labelExistingCustomerHeading.Name = "labelExistingCustomerHeading";
            this.labelExistingCustomerHeading.Size = new System.Drawing.Size(111, 15);
            this.labelExistingCustomerHeading.TabIndex = 0;
            this.labelExistingCustomerHeading.Text = "Existing Customer";
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.BackColor = System.Drawing.Color.Blue;
            this.panel4.Controls.Add(this.labelNewCustomerHeader);
            this.panel4.Location = new System.Drawing.Point(7, 131);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(808, 24);
            this.panel4.TabIndex = 6;
            // 
            // labelNewCustomerHeader
            // 
            this.labelNewCustomerHeader.AutoSize = true;
            this.labelNewCustomerHeader.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNewCustomerHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelNewCustomerHeader.Location = new System.Drawing.Point(6, 4);
            this.labelNewCustomerHeader.Name = "labelNewCustomerHeader";
            this.labelNewCustomerHeader.Size = new System.Drawing.Size(91, 15);
            this.labelNewCustomerHeader.TabIndex = 0;
            this.labelNewCustomerHeader.Text = "New Customer";
            // 
            // customDataGridViewExistingCustomer
            // 
            this.customDataGridViewExistingCustomer.AllowUserToAddRows = false;
            this.customDataGridViewExistingCustomer.AllowUserToDeleteRows = false;
            this.customDataGridViewExistingCustomer.AllowUserToResizeColumns = false;
            this.customDataGridViewExistingCustomer.AllowUserToResizeRows = false;
            this.customDataGridViewExistingCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.customDataGridViewExistingCustomer.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.customDataGridViewExistingCustomer.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewExistingCustomer.CausesValidation = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewExistingCustomer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.customDataGridViewExistingCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewExistingCustomer.DefaultCellStyle = dataGridViewCellStyle8;
            this.customDataGridViewExistingCustomer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridViewExistingCustomer.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewExistingCustomer.Location = new System.Drawing.Point(7, 331);
            this.customDataGridViewExistingCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewExistingCustomer.Name = "customDataGridViewExistingCustomer";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewExistingCustomer.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.customDataGridViewExistingCustomer.RowHeadersVisible = false;
            this.customDataGridViewExistingCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewExistingCustomer.Size = new System.Drawing.Size(808, 150);
            this.customDataGridViewExistingCustomer.TabIndex = 12;
            this.customDataGridViewExistingCustomer.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewExistingCustomer_CellContentClick);
            // 
            // customDataGridViewNewCustomer
            // 
            this.customDataGridViewNewCustomer.AllowUserToAddRows = false;
            this.customDataGridViewNewCustomer.AllowUserToDeleteRows = false;
            this.customDataGridViewNewCustomer.AllowUserToResizeColumns = false;
            this.customDataGridViewNewCustomer.AllowUserToResizeRows = false;
            this.customDataGridViewNewCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.customDataGridViewNewCustomer.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.customDataGridViewNewCustomer.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewNewCustomer.CausesValidation = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewNewCustomer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.customDataGridViewNewCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewNewCustomer.DefaultCellStyle = dataGridViewCellStyle11;
            this.customDataGridViewNewCustomer.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewNewCustomer.Location = new System.Drawing.Point(7, 153);
            this.customDataGridViewNewCustomer.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewNewCustomer.Name = "customDataGridViewNewCustomer";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewNewCustomer.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.customDataGridViewNewCustomer.RowHeadersVisible = false;
            this.customDataGridViewNewCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewNewCustomer.Size = new System.Drawing.Size(808, 150);
            this.customDataGridViewNewCustomer.TabIndex = 11;
            this.customDataGridViewNewCustomer.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewNewCustomer_CellContentClick);
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
            this.customButtonBack.Location = new System.Drawing.Point(28, 488);
            this.customButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.Name = "customButtonBack";
            this.customButtonBack.Size = new System.Drawing.Size(100, 50);
            this.customButtonBack.TabIndex = 13;
            this.customButtonBack.Text = "&Back";
            this.customButtonBack.UseVisualStyleBackColor = false;
            this.customButtonBack.Click += new System.EventHandler(this.buttonBack_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(224, 488);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 14;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // customButtonUpdate
            // 
            this.customButtonUpdate.BackColor = System.Drawing.Color.Transparent;
            this.customButtonUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonUpdate.BackgroundImage")));
            this.customButtonUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonUpdate.Enabled = false;
            this.customButtonUpdate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonUpdate.FlatAppearance.BorderSize = 0;
            this.customButtonUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonUpdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonUpdate.ForeColor = System.Drawing.Color.White;
            this.customButtonUpdate.Location = new System.Drawing.Point(585, 488);
            this.customButtonUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonUpdate.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonUpdate.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonUpdate.Name = "customButtonUpdate";
            this.customButtonUpdate.Size = new System.Drawing.Size(100, 50);
            this.customButtonUpdate.TabIndex = 15;
            this.customButtonUpdate.Text = "&Update";
            this.customButtonUpdate.UseVisualStyleBackColor = false;
            this.customButtonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
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
            this.customButtonContinue.Location = new System.Drawing.Point(697, 488);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 16;
            this.customButtonContinue.Text = "Co&ntinue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // ExistingCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(827, 570);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.customButtonUpdate);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customButtonBack);
            this.Controls.Add(this.customDataGridViewExistingCustomer);
            this.Controls.Add(this.customDataGridViewNewCustomer);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelExistingCustomerInfoHeader);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExistingCustomer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Existing Customer";
            this.Load += new System.EventHandler(this.ExistingCustomer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewExistingCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewNewCustomer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelExistingCustomerInfoHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label labelRedHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelExistingCustomerHeading;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labelNewCustomerHeader;
        private CustomDataGridView customDataGridViewNewCustomer;
        private CustomDataGridView customDataGridViewExistingCustomer;
        private CustomButton customButtonBack;
        private CustomButton customButtonCancel;
        private CustomButton customButtonUpdate;
        private CustomButton customButtonContinue;
    }
}
