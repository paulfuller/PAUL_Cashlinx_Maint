using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Components.EventArgs;

namespace Common.Libraries.Forms.Retail
{
    partial class SelectItems
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.titleLabel = new System.Windows.Forms.Label();
            this.cancelButton = new CustomButton();
            this.continueButton = new CustomButton();
            this.gvMerchandise = new CustomDataGridView();
            this.colICN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMerchandiseDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblError = new System.Windows.Forms.Label();
            this.btnTempICN = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.gvMerchandise)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(278, 26);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(142, 19);
            this.titleLabel.TabIndex = 136;
            this.titleLabel.Text = "Item Search Result";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(12, 322);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 40);
            this.cancelButton.TabIndex = 143;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.continueButton.AutoSize = true;
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.continueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(584, 322);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 40);
            this.continueButton.TabIndex = 144;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // gvMerchandise
            // 
            this.gvMerchandise.AllowUserToAddRows = false;
            this.gvMerchandise.AllowUserToDeleteRows = false;
            this.gvMerchandise.AllowUserToResizeColumns = false;
            this.gvMerchandise.AllowUserToResizeRows = false;
            this.gvMerchandise.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvMerchandise.BackgroundColor = System.Drawing.Color.White;
            this.gvMerchandise.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gvMerchandise.CausesValidation = false;
            this.gvMerchandise.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMerchandise.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvMerchandise.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMerchandise.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colICN,
            this.colMerchandiseDescription,
            this.colStatus,
            this.colRetailPrice});
            this.gvMerchandise.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvMerchandise.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvMerchandise.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvMerchandise.GridColor = System.Drawing.Color.LightGray;
            this.gvMerchandise.Location = new System.Drawing.Point(12, 71);
            this.gvMerchandise.Margin = new System.Windows.Forms.Padding(0);
            this.gvMerchandise.MultiSelect = false;
            this.gvMerchandise.Name = "gvMerchandise";
            this.gvMerchandise.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMerchandise.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvMerchandise.RowHeadersVisible = false;
            this.gvMerchandise.RowHeadersWidth = 20;
            this.gvMerchandise.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvMerchandise.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvMerchandise.RowTemplate.Height = 25;
            this.gvMerchandise.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvMerchandise.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvMerchandise.Size = new System.Drawing.Size(672, 239);
            this.gvMerchandise.TabIndex = 147;
            this.gvMerchandise.GridViewRowSelecting += new System.EventHandler<GridViewRowSelectingEventArgs>(this.gvMerchandise_GridViewRowSelecting);
            this.gvMerchandise.SelectionChanged += new System.EventHandler(this.gvMerchandise_SelectionChanged);
            // 
            // colICN
            // 
            this.colICN.DataPropertyName = "Icn";
            this.colICN.FillWeight = 12.70586F;
            this.colICN.HeaderText = "ICN";
            this.colICN.Name = "colICN";
            this.colICN.ReadOnly = true;
            this.colICN.Width = 140;
            // 
            // colMerchandiseDescription
            // 
            this.colMerchandiseDescription.DataPropertyName = "TicketDescription";
            this.colMerchandiseDescription.FillWeight = 40F;
            this.colMerchandiseDescription.HeaderText = "Merchandise Description";
            this.colMerchandiseDescription.Name = "colMerchandiseDescription";
            this.colMerchandiseDescription.ReadOnly = true;
            this.colMerchandiseDescription.Width = 350;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "ItemStatus";
            this.colStatus.FillWeight = 18.96205F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colRetailPrice
            // 
            this.colRetailPrice.DataPropertyName = "RetailPrice";
            this.colRetailPrice.FillWeight = 6.352931F;
            this.colRetailPrice.HeaderText = "Retail Price";
            this.colRetailPrice.Name = "colRetailPrice";
            this.colRetailPrice.ReadOnly = true;
            this.colRetailPrice.Width = 60;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "colCost";
            this.dataGridViewTextBoxColumn1.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Cost";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "colDescription";
            this.dataGridViewTextBoxColumn2.FillWeight = 12.70586F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Description";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "colRetail";
            this.dataGridViewTextBoxColumn3.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Retail";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "colReason";
            this.dataGridViewTextBoxColumn4.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Reason";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "colTags";
            this.dataGridViewTextBoxColumn5.FillWeight = 47.31137F;
            this.dataGridViewTextBoxColumn5.HeaderText = "# Tags";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblError.Location = new System.Drawing.Point(12, 52);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(517, 15);
            this.lblError.TabIndex = 148;
            this.lblError.Text = "The short code entered is a duplicate.  Please make your selection from the list." +
                "";
            // 
            // btnTempICN
            // 
            this.btnTempICN.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTempICN.AutoSize = true;
            this.btnTempICN.BackColor = System.Drawing.Color.Transparent;
            this.btnTempICN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTempICN.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnTempICN.FlatAppearance.BorderSize = 0;
            this.btnTempICN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTempICN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTempICN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTempICN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTempICN.ForeColor = System.Drawing.Color.White;
            this.btnTempICN.Location = new System.Drawing.Point(432, 322);
            this.btnTempICN.Name = "btnTempICN";
            this.btnTempICN.Size = new System.Drawing.Size(129, 40);
            this.btnTempICN.TabIndex = 149;
            this.btnTempICN.Text = "Temporary ICN";
            this.btnTempICN.UseVisualStyleBackColor = false;
            this.btnTempICN.Click += new System.EventHandler(this.btnTempICN_Click);
            // 
            // SelectItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(698, 374);
            this.ControlBox = false;
            this.Controls.Add(this.btnTempICN);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.gvMerchandise);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.titleLabel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Items";
            ((System.ComponentModel.ISupportInitialize)(this.gvMerchandise)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private CustomButton cancelButton;
        private CustomButton continueButton;
        private CustomDataGridView gvMerchandise;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Label lblError;
        private CustomButton btnTempICN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colICN;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMerchandiseDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRetailPrice;
    }
}
