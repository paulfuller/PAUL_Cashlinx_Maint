using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.ChargeOff
{
    partial class InventoryChargeOffSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryChargeOffSearch));
            this.titleLabel = new System.Windows.Forms.Label();
            this.continueButton = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancelButton = new System.Windows.Forms.Button();
            this.txtICN = new System.Windows.Forms.TextBox();
            this.customLabel1 = new Common.Libraries.Forms.Components.CustomLabel();
            this.customLabelError = new Common.Libraries.Forms.Components.CustomLabel();
            this.lblQty = new Common.Libraries.Forms.Components.CustomLabel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(143, 16);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(94, 19);
            this.titleLabel.TabIndex = 136;
            this.titleLabel.Text = "Item Search";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // continueButton
            // 
            this.continueButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.continueButton.AutoSize = true;
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("continueButton.BackgroundImage")));
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(394, 130);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 40);
            this.continueButton.TabIndex = 144;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
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
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelButton.BackgroundImage")));
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(88, 130);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 40);
            this.cancelButton.TabIndex = 145;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // txtICN
            // 
            this.txtICN.Location = new System.Drawing.Point(156, 58);
            this.txtICN.Name = "txtICN";
            this.txtICN.Size = new System.Drawing.Size(227, 21);
            this.txtICN.TabIndex = 152;
            this.txtICN.TextChanged += new System.EventHandler(this.txtICN_TextChanged);
            this.txtICN.Leave += new System.EventHandler(this.txtICN_Leave);
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Location = new System.Drawing.Point(94, 66);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Required = true;
            this.customLabel1.Size = new System.Drawing.Size(29, 13);
            this.customLabel1.TabIndex = 153;
            this.customLabel1.Text = "ICN:";
            // 
            // customLabelError
            // 
            this.customLabelError.AutoSize = true;
            this.customLabelError.BackColor = System.Drawing.Color.Transparent;
            this.customLabelError.ForeColor = System.Drawing.Color.Red;
            this.customLabelError.Location = new System.Drawing.Point(12, 92);
            this.customLabelError.Name = "customLabelError";
            this.customLabelError.Size = new System.Drawing.Size(76, 13);
            this.customLabelError.TabIndex = 154;
            this.customLabelError.Text = "Error Message";
            this.customLabelError.Visible = false;
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.BackColor = System.Drawing.Color.Transparent;
            this.lblQty.Location = new System.Drawing.Point(422, 66);
            this.lblQty.Name = "lblQty";
            this.lblQty.Required = true;
            this.lblQty.Size = new System.Drawing.Size(31, 13);
            this.lblQty.TabIndex = 155;
            this.lblQty.Text = "QTY:";
            this.lblQty.Visible = false;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(478, 63);
            this.txtQty.MaxLength = 3;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(46, 21);
            this.txtQty.TabIndex = 156;
            this.txtQty.Text = "0";
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQty.Visible = false;
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // InventoryChargeOffSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(583, 182);
            this.ControlBox = false;
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.customLabelError);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.txtICN);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.titleLabel);
            this.Name = "InventoryChargeOffSearch";
            this.Load += new System.EventHandler(this.InventoryChargeOffSearch_Load);
            this.Shown += new System.EventHandler(this.ChangeRetailPriceSearch_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox txtICN;
        private CustomLabel customLabel1;
        private CustomLabel customLabelError;
        private CustomLabel lblQty;
        private System.Windows.Forms.TextBox txtQty;
    }
}
