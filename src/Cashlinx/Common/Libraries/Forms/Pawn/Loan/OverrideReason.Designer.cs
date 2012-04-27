using Common.Libraries.Forms.Components;

namespace Common.Libraries.Forms.Pawn.Loan
{
    partial class OverrideReason
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverrideReason));
            this.labelHeading = new System.Windows.Forms.Label();
            this.customDataGridViewOverrideTransactions = new CustomDataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.loannumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reasontext = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overridetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactiontype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonCancel = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.customLabelOverrideMsg = new CustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewOverrideTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelHeading.Location = new System.Drawing.Point(193, 13);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(164, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Override Transactions";
            // 
            // customDataGridViewOverrideTransactions
            // 
            this.customDataGridViewOverrideTransactions.AllowUserToAddRows = false;
            this.customDataGridViewOverrideTransactions.AllowUserToDeleteRows = false;
            this.customDataGridViewOverrideTransactions.AllowUserToResizeColumns = false;
            this.customDataGridViewOverrideTransactions.AllowUserToResizeRows = false;
            this.customDataGridViewOverrideTransactions.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewOverrideTransactions.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewOverrideTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridViewOverrideTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewOverrideTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.loannumber,
            this.reasontext,
            this.overridetype,
            this.transactiontype});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewOverrideTransactions.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridViewOverrideTransactions.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewOverrideTransactions.Location = new System.Drawing.Point(14, 82);
            this.customDataGridViewOverrideTransactions.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewOverrideTransactions.Name = "customDataGridViewOverrideTransactions";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewOverrideTransactions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridViewOverrideTransactions.RowHeadersVisible = false;
            this.customDataGridViewOverrideTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewOverrideTransactions.Size = new System.Drawing.Size(522, 119);
            this.customDataGridViewOverrideTransactions.TabIndex = 7;
            this.customDataGridViewOverrideTransactions.CurrentCellDirtyStateChanged += new System.EventHandler(this.customDataGridViewOverrideTransactions_CurrentCellDirtyStateChanged);
            // 
            // select
            // 
            this.select.HeaderText = "Select";
            this.select.Name = "select";
            this.select.Width = 50;
            // 
            // loannumber
            // 
            this.loannumber.HeaderText = "Ticket Number";
            this.loannumber.Name = "loannumber";
            this.loannumber.ReadOnly = true;
            // 
            // reasontext
            // 
            this.reasontext.HeaderText = "Reason For Override";
            this.reasontext.Name = "reasontext";
            this.reasontext.ReadOnly = true;
            this.reasontext.Width = 300;
            // 
            // overridetype
            // 
            this.overridetype.HeaderText = "overridetype";
            this.overridetype.Name = "overridetype";
            this.overridetype.ReadOnly = true;
            this.overridetype.Visible = false;
            // 
            // transactiontype
            // 
            this.transactiontype.HeaderText = "transactiontype";
            this.transactiontype.Name = "transactiontype";
            this.transactiontype.ReadOnly = true;
            this.transactiontype.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Ticket Number";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Reason for Override";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 300;
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
            this.customButtonCancel.Location = new System.Drawing.Point(9, 210);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 8;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.cancelButton_Click);
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
            this.customButtonSubmit.Location = new System.Drawing.Point(411, 210);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 9;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // customLabelOverrideMsg
            // 
            this.customLabelOverrideMsg.AutoSize = true;
            this.customLabelOverrideMsg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelOverrideMsg.Location = new System.Drawing.Point(12, 48);
            this.customLabelOverrideMsg.Name = "customLabelOverrideMsg";
            this.customLabelOverrideMsg.Size = new System.Drawing.Size(27, 13);
            this.customLabelOverrideMsg.TabIndex = 10;
            this.customLabelOverrideMsg.Text = "text";
            // 
            // OverrideReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(550, 273);
            this.ControlBox = false;
            this.Controls.Add(this.customLabelOverrideMsg);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customDataGridViewOverrideTransactions);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OverrideReason";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OverrideReason";
            this.Load += new System.EventHandler(this.OverrideReason_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewOverrideTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private CustomDataGridView customDataGridViewOverrideTransactions;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn loannumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn reasontext;
        private System.Windows.Forms.DataGridViewTextBoxColumn overridetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn transactiontype;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSubmit;
        private CustomLabel customLabelOverrideMsg;
    }
}