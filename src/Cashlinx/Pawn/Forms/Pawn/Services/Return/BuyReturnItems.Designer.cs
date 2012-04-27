using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Return
{
    partial class BuyReturnItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuyReturnItems));
            this.label1 = new System.Windows.Forms.Label();
            this.labelBuyNoHeading = new System.Windows.Forms.Label();
            this.labelStoreNo = new System.Windows.Forms.Label();
            this.labelBuyNumber = new System.Windows.Forms.Label();
            this.labelStatusHeading = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelCustName = new System.Windows.Forms.Label();
            this.labelReasonHeading = new System.Windows.Forms.Label();
            this.customTextBoxRetReason = new CustomTextBox();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.ICN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.holddesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonCancel = new CustomButton();
            this.customButtonBack = new CustomButton();
            this.customButtonAddItem = new CustomButton();
            this.customButtonContinue = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Items for Return";
            // 
            // labelBuyNoHeading
            // 
            this.labelBuyNoHeading.AutoSize = true;
            this.labelBuyNoHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelBuyNoHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBuyNoHeading.Location = new System.Drawing.Point(25, 85);
            this.labelBuyNoHeading.Name = "labelBuyNoHeading";
            this.labelBuyNoHeading.Size = new System.Drawing.Size(124, 16);
            this.labelBuyNoHeading.TabIndex = 2;
            this.labelBuyNoHeading.Text = "Shop / Buy Number:";
            // 
            // labelStoreNo
            // 
            this.labelStoreNo.AutoSize = true;
            this.labelStoreNo.BackColor = System.Drawing.Color.Transparent;
            this.labelStoreNo.Location = new System.Drawing.Point(172, 87);
            this.labelStoreNo.Name = "labelStoreNo";
            this.labelStoreNo.Size = new System.Drawing.Size(37, 13);
            this.labelStoreNo.TabIndex = 3;
            this.labelStoreNo.Text = "02030";
            // 
            // labelBuyNumber
            // 
            this.labelBuyNumber.AutoSize = true;
            this.labelBuyNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelBuyNumber.Location = new System.Drawing.Point(233, 87);
            this.labelBuyNumber.Name = "labelBuyNumber";
            this.labelBuyNumber.Size = new System.Drawing.Size(43, 13);
            this.labelBuyNumber.TabIndex = 4;
            this.labelBuyNumber.Text = "123456";
            // 
            // labelStatusHeading
            // 
            this.labelStatusHeading.AutoSize = true;
            this.labelStatusHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelStatusHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatusHeading.Location = new System.Drawing.Point(464, 87);
            this.labelStatusHeading.Name = "labelStatusHeading";
            this.labelStatusHeading.Size = new System.Drawing.Size(49, 16);
            this.labelStatusHeading.TabIndex = 5;
            this.labelStatusHeading.Text = "Status:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelStatus.Location = new System.Drawing.Point(530, 90);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(26, 13);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "BUY";
            // 
            // labelCustName
            // 
            this.labelCustName.AutoSize = true;
            this.labelCustName.BackColor = System.Drawing.Color.Transparent;
            this.labelCustName.Location = new System.Drawing.Point(25, 114);
            this.labelCustName.Name = "labelCustName";
            this.labelCustName.Size = new System.Drawing.Size(58, 13);
            this.labelCustName.TabIndex = 7;
            this.labelCustName.Text = "JOHN DOE";
            // 
            // labelReasonHeading
            // 
            this.labelReasonHeading.AutoSize = true;
            this.labelReasonHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelReasonHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReasonHeading.Location = new System.Drawing.Point(28, 153);
            this.labelReasonHeading.Name = "labelReasonHeading";
            this.labelReasonHeading.Size = new System.Drawing.Size(97, 16);
            this.labelReasonHeading.TabIndex = 8;
            this.labelReasonHeading.Text = "Return Reason:";
            // 
            // customTextBoxRetReason
            // 
            this.customTextBoxRetReason.CausesValidation = false;
            this.customTextBoxRetReason.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxRetReason.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxRetReason.Location = new System.Drawing.Point(156, 153);
            this.customTextBoxRetReason.MaxLength = 256;
            this.customTextBoxRetReason.Name = "customTextBoxRetReason";
            this.customTextBoxRetReason.Size = new System.Drawing.Size(380, 21);
            this.customTextBoxRetReason.TabIndex = 9;
            this.customTextBoxRetReason.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.BackColor = System.Drawing.Color.Transparent;
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Location = new System.Drawing.Point(153, 125);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(76, 13);
            this.labelErrorMessage.TabIndex = 10;
            this.labelErrorMessage.Text = "Error Message";
            this.labelErrorMessage.Visible = false;
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.AllowUserToAddRows = false;
            this.dataGridViewItems.AllowUserToDeleteRows = false;
            this.dataGridViewItems.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ICN,
            this.description,
            this.Status,
            this.Amount,
            this.holddesc});
            this.dataGridViewItems.Location = new System.Drawing.Point(12, 193);
            this.dataGridViewItems.MultiSelect = false;
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.ReadOnly = true;
            this.dataGridViewItems.RowHeadersVisible = false;
            this.dataGridViewItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewItems.Size = new System.Drawing.Size(691, 106);
            this.dataGridViewItems.TabIndex = 11;
            this.dataGridViewItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItems_CellClick);
            // 
            // ICN
            // 
            this.ICN.HeaderText = "ICN";
            this.ICN.Name = "ICN";
            this.ICN.ReadOnly = true;
            this.ICN.Width = 150;
            // 
            // description
            // 
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Width = 300;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Purchase Amt.";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // holddesc
            // 
            this.holddesc.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.holddesc.Name = "holddesc";
            this.holddesc.ReadOnly = true;
            this.holddesc.Visible = false;
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
            this.customButtonCancel.Location = new System.Drawing.Point(12, 353);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 12;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
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
            this.customButtonBack.Location = new System.Drawing.Point(129, 353);
            this.customButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.Name = "customButtonBack";
            this.customButtonBack.Size = new System.Drawing.Size(100, 50);
            this.customButtonBack.TabIndex = 13;
            this.customButtonBack.Text = "Back";
            this.customButtonBack.UseVisualStyleBackColor = false;
            this.customButtonBack.Click += new System.EventHandler(this.customButtonBack_Click);
            // 
            // customButtonAddItem
            // 
            this.customButtonAddItem.BackColor = System.Drawing.Color.Transparent;
            this.customButtonAddItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonAddItem.BackgroundImage")));
            this.customButtonAddItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonAddItem.Enabled = false;
            this.customButtonAddItem.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonAddItem.FlatAppearance.BorderSize = 0;
            this.customButtonAddItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonAddItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonAddItem.ForeColor = System.Drawing.Color.White;
            this.customButtonAddItem.Location = new System.Drawing.Point(250, 353);
            this.customButtonAddItem.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAddItem.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddItem.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddItem.Name = "customButtonAddItem";
            this.customButtonAddItem.Size = new System.Drawing.Size(100, 50);
            this.customButtonAddItem.TabIndex = 14;
            this.customButtonAddItem.Text = "Add Item";
            this.customButtonAddItem.UseVisualStyleBackColor = false;
            this.customButtonAddItem.Click += new System.EventHandler(this.customButtonAddItem_Click);
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
            this.customButtonContinue.Location = new System.Drawing.Point(587, 353);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 15;
            this.customButtonContinue.Text = "Continue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.customButtonContinue_Click);
            // 
            // BuyReturnItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 424);
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.customButtonAddItem);
            this.Controls.Add(this.customButtonBack);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.dataGridViewItems);
            this.Controls.Add(this.labelErrorMessage);
            this.Controls.Add(this.customTextBoxRetReason);
            this.Controls.Add(this.labelReasonHeading);
            this.Controls.Add(this.labelCustName);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelStatusHeading);
            this.Controls.Add(this.labelBuyNumber);
            this.Controls.Add(this.labelStoreNo);
            this.Controls.Add(this.labelBuyNoHeading);
            this.Controls.Add(this.label1);
            this.Name = "BuyReturnItems";
            this.Text = "BuyReturnItems";
            this.Load += new System.EventHandler(this.BuyReturnItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelBuyNoHeading;
        private System.Windows.Forms.Label labelStoreNo;
        private System.Windows.Forms.Label labelBuyNumber;
        private System.Windows.Forms.Label labelStatusHeading;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelCustName;
        private System.Windows.Forms.Label labelReasonHeading;
        private CustomTextBox customTextBoxRetReason;
        private System.Windows.Forms.Label labelErrorMessage;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private CustomButton customButtonCancel;
        private CustomButton customButtonBack;
        private CustomButton customButtonAddItem;
        private CustomButton customButtonContinue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ICN;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn holddesc;
    }
}