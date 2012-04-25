using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services
{
    partial class TagReprint
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelHeading = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxIcnNumber = new CustomTextBox();
            this.labelIcnNumber = new System.Windows.Forms.Label();
            this.errorPanel = new System.Windows.Forms.Panel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonComplete = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTagData = new System.Windows.Forms.DataGridView();
            this.icn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.merchandisedescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Retail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.errorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTagData)).BeginInit();
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBoxIcnNumber);
            this.panel1.Controls.Add(this.labelIcnNumber);
            this.panel1.Location = new System.Drawing.Point(10, 146);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(655, 36);
            this.panel1.TabIndex = 1;
            // 
            // textBoxIcnNumber
            // 
            this.textBoxIcnNumber.CausesValidation = false;
            this.textBoxIcnNumber.ErrorMessage = "";
            this.textBoxIcnNumber.Location = new System.Drawing.Point(200, 8);
            this.textBoxIcnNumber.MaxLength = 18;
            this.textBoxIcnNumber.Name = "textBoxIcnNumber";
            this.textBoxIcnNumber.Required = true;
            this.textBoxIcnNumber.Size = new System.Drawing.Size(180, 20);
            this.textBoxIcnNumber.TabIndex = 3;
            this.textBoxIcnNumber.ValidationExpression = "";
            // 
            // labelIcnNumber
            // 
            this.labelIcnNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelIcnNumber.AutoSize = true;
            this.labelIcnNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelIcnNumber.Location = new System.Drawing.Point(23, 10);
            this.labelIcnNumber.Name = "labelIcnNumber";
            this.labelIcnNumber.Size = new System.Drawing.Size(174, 16);
            this.labelIcnNumber.TabIndex = 0;
            this.labelIcnNumber.Text = "Bar Code Number (ICN):";
            // 
            // errorPanel
            // 
            this.errorPanel.BackColor = System.Drawing.Color.Transparent;
            this.errorPanel.Controls.Add(this.errorLabel);
            this.errorPanel.Location = new System.Drawing.Point(10, 79);
            this.errorPanel.Name = "errorPanel";
            this.errorPanel.Size = new System.Drawing.Size(655, 61);
            this.errorPanel.TabIndex = 2;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(8, 7);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(42, 16);
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
            this.buttonCancel.Location = new System.Drawing.Point(115, 326);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 50);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonComplete
            // 
            this.buttonComplete.BackColor = System.Drawing.Color.Transparent;
            this.buttonComplete.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonComplete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonComplete.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonComplete.FlatAppearance.BorderSize = 0;
            this.buttonComplete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonComplete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonComplete.ForeColor = System.Drawing.Color.White;
            this.buttonComplete.Location = new System.Drawing.Point(519, 326);
            this.buttonComplete.Margin = new System.Windows.Forms.Padding(4);
            this.buttonComplete.MaximumSize = new System.Drawing.Size(100, 50);
            this.buttonComplete.MinimumSize = new System.Drawing.Size(100, 50);
            this.buttonComplete.Name = "buttonComplete";
            this.buttonComplete.Size = new System.Drawing.Size(100, 50);
            this.buttonComplete.TabIndex = 10;
            this.buttonComplete.Text = "&Complete";
            this.buttonComplete.UseVisualStyleBackColor = false;
            this.buttonComplete.Click += new System.EventHandler(this.buttonComplete_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ICN";
            this.dataGridViewTextBoxColumn1.FillWeight = 99.49239F;
            this.dataGridViewTextBoxColumn1.HeaderText = "ICN#";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 164;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Merchandise Description";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 164;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Status";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 163;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "MD_DESC";
            this.dataGridViewTextBoxColumn4.FillWeight = 101.5228F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Retail";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 400;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 400;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "status_cd";
            this.dataGridViewTextBoxColumn5.FillWeight = 99.49239F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Status";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "RETAIL_PRICE";
            dataGridViewCellStyle1.Format = "C2";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn6.FillWeight = 99.49239F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Retail";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 82;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 82;
            // 
            // dataGridViewTagData
            // 
            this.dataGridViewTagData.AllowUserToAddRows = false;
            this.dataGridViewTagData.AllowUserToDeleteRows = false;
            this.dataGridViewTagData.AllowUserToResizeColumns = false;
            this.dataGridViewTagData.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTagData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTagData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTagData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewTagData.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTagData.CausesValidation = false;
            this.dataGridViewTagData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTagData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTagData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTagData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icn,
            this.merchandisedescription,
            this.status,
            this.Retail});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTagData.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTagData.Location = new System.Drawing.Point(10, 148);
            this.dataGridViewTagData.MultiSelect = false;
            this.dataGridViewTagData.Name = "dataGridViewTagData";
            this.dataGridViewTagData.ReadOnly = true;
            this.dataGridViewTagData.RowHeadersVisible = false;
            this.dataGridViewTagData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewTagData.RowTemplate.Height = 25;
            this.dataGridViewTagData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewTagData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTagData.Size = new System.Drawing.Size(655, 171);
            this.dataGridViewTagData.TabIndex = 11;
            this.dataGridViewTagData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTagData_CellContentClick);
            // 
            // icn
            // 
            this.icn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.icn.DataPropertyName = "ICN";
            this.icn.FillWeight = 99.49239F;
            this.icn.HeaderText = "ICN#";
            this.icn.MinimumWidth = 120;
            this.icn.Name = "icn";
            this.icn.ReadOnly = true;
            this.icn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.icn.Width = 120;
            // 
            // merchandisedescription
            // 
            this.merchandisedescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.merchandisedescription.DataPropertyName = "MD_DESC";
            this.merchandisedescription.FillWeight = 101.5228F;
            this.merchandisedescription.HeaderText = "Merchandise Description";
            this.merchandisedescription.MinimumWidth = 400;
            this.merchandisedescription.Name = "merchandisedescription";
            this.merchandisedescription.ReadOnly = true;
            this.merchandisedescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.merchandisedescription.Width = 400;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.status.DataPropertyName = "status_cd";
            this.status.FillWeight = 99.49239F;
            this.status.HeaderText = "Status";
            this.status.MinimumWidth = 50;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.status.Width = 50;
            // 
            // Retail
            // 
            this.Retail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Retail.DataPropertyName = "RETAIL_PRICE";
            dataGridViewCellStyle4.Format = "C2";
            this.Retail.DefaultCellStyle = dataGridViewCellStyle4;
            this.Retail.FillWeight = 99.49239F;
            this.Retail.HeaderText = "Retail";
            this.Retail.MinimumWidth = 82;
            this.Retail.Name = "Retail";
            this.Retail.ReadOnly = true;
            this.Retail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Retail.Width = 82;
            // 
            // TagReprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(677, 396);
            this.ControlBox = false;
            this.Controls.Add(this.buttonComplete);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.errorPanel);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(this.dataGridViewTagData);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TagReprint";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TagReprint";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.errorPanel.ResumeLayout(false);
            this.errorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTagData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelIcnNumber;
        private CustomTextBox textBoxIcnNumber;
        private System.Windows.Forms.Panel errorPanel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonComplete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridView dataGridViewTagData;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn;
        private System.Windows.Forms.DataGridViewTextBoxColumn merchandisedescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Retail;
    }
}