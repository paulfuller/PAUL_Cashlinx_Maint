namespace Pawn.Forms.Pawn.Tender.Base
{
    partial class TenderTablePanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.totalLabel = new System.Windows.Forms.Label();
            this.amountLabel = new System.Windows.Forms.Label();
            this.tenderInDataGridView = new System.Windows.Forms.DataGridView();
            this.tenderTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referenceNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenderOutPurchaseDataGridView = new System.Windows.Forms.DataGridView();
            this.purchaseTenderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchaseReferenceNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenderOutRefundDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RefundedCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remainingLabel = new System.Windows.Forms.Label();
            this.remainingAmountFieldLabel = new System.Windows.Forms.Label();
            this.tenderOutPartialRefundDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tenderInDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenderOutPurchaseDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenderOutRefundDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenderOutPartialRefundDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLabel.Location = new System.Drawing.Point(277, 518);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(45, 16);
            this.totalLabel.TabIndex = 1;
            this.totalLabel.Text = "Total:";
            this.totalLabel.Visible = false;
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Location = new System.Drawing.Point(356, 518);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(69, 16);
            this.amountLabel.TabIndex = 2;
            this.amountLabel.Text = "<amount>";
            this.amountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.amountLabel.Visible = false;
            // 
            // tenderInDataGridView
            // 
            this.tenderInDataGridView.AllowUserToAddRows = false;
            this.tenderInDataGridView.AllowUserToDeleteRows = false;
            this.tenderInDataGridView.AllowUserToResizeColumns = false;
            this.tenderInDataGridView.AllowUserToResizeRows = false;
            this.tenderInDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tenderInDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.tenderInDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tenderInDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.tenderInDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.tenderInDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tenderInDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tenderInDataGridView.ColumnHeadersHeight = 40;
            this.tenderInDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.tenderInDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tenderTypeColumn,
            this.referenceNumberColumn,
            this.typeColumn,
            this.amountColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tenderInDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.tenderInDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tenderInDataGridView.EnableHeadersVisualStyles = false;
            this.tenderInDataGridView.GridColor = System.Drawing.Color.Black;
            this.tenderInDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tenderInDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.tenderInDataGridView.MultiSelect = false;
            this.tenderInDataGridView.Name = "tenderInDataGridView";
            this.tenderInDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tenderInDataGridView.RowHeadersVisible = false;
            this.tenderInDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tenderInDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tenderInDataGridView.ShowCellErrors = false;
            this.tenderInDataGridView.ShowCellToolTips = false;
            this.tenderInDataGridView.ShowEditingIcon = false;
            this.tenderInDataGridView.ShowRowErrors = false;
            this.tenderInDataGridView.Size = new System.Drawing.Size(445, 160);
            this.tenderInDataGridView.TabIndex = 4;
            this.tenderInDataGridView.TabStop = false;
            this.tenderInDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tenderInDataGridView_CellContentDoubleClick);
            // 
            // tenderTypeColumn
            // 
            this.tenderTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tenderTypeColumn.FillWeight = 88.46336F;
            this.tenderTypeColumn.HeaderText = "Tender Type";
            this.tenderTypeColumn.MinimumWidth = 105;
            this.tenderTypeColumn.Name = "tenderTypeColumn";
            this.tenderTypeColumn.Width = 105;
            // 
            // referenceNumberColumn
            // 
            this.referenceNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.referenceNumberColumn.FillWeight = 153.8483F;
            this.referenceNumberColumn.HeaderText = "Reference Number";
            this.referenceNumberColumn.MinimumWidth = 195;
            this.referenceNumberColumn.Name = "referenceNumberColumn";
            this.referenceNumberColumn.Width = 195;
            // 
            // typeColumn
            // 
            this.typeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.typeColumn.FillWeight = 66.31776F;
            this.typeColumn.HeaderText = "Type";
            this.typeColumn.MinimumWidth = 55;
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.Width = 55;
            // 
            // amountColumn
            // 
            this.amountColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.amountColumn.FillWeight = 91.37055F;
            this.amountColumn.HeaderText = "Amount";
            this.amountColumn.MinimumWidth = 90;
            this.amountColumn.Name = "amountColumn";
            this.amountColumn.Width = 90;
            // 
            // tenderOutPurchaseDataGridView
            // 
            this.tenderOutPurchaseDataGridView.AllowUserToAddRows = false;
            this.tenderOutPurchaseDataGridView.AllowUserToDeleteRows = false;
            this.tenderOutPurchaseDataGridView.AllowUserToResizeColumns = false;
            this.tenderOutPurchaseDataGridView.AllowUserToResizeRows = false;
            this.tenderOutPurchaseDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tenderOutPurchaseDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.tenderOutPurchaseDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tenderOutPurchaseDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.tenderOutPurchaseDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.tenderOutPurchaseDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tenderOutPurchaseDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tenderOutPurchaseDataGridView.ColumnHeadersHeight = 40;
            this.tenderOutPurchaseDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.tenderOutPurchaseDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.purchaseTenderType,
            this.purchaseReferenceNumberColumn,
            this.Type,
            this.dataGridViewTextBoxColumn3});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tenderOutPurchaseDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.tenderOutPurchaseDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tenderOutPurchaseDataGridView.EnableHeadersVisualStyles = false;
            this.tenderOutPurchaseDataGridView.GridColor = System.Drawing.Color.Black;
            this.tenderOutPurchaseDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tenderOutPurchaseDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.tenderOutPurchaseDataGridView.MultiSelect = false;
            this.tenderOutPurchaseDataGridView.Name = "tenderOutPurchaseDataGridView";
            this.tenderOutPurchaseDataGridView.ReadOnly = true;
            this.tenderOutPurchaseDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tenderOutPurchaseDataGridView.RowHeadersVisible = false;
            this.tenderOutPurchaseDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.tenderOutPurchaseDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tenderOutPurchaseDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tenderOutPurchaseDataGridView.ShowCellErrors = false;
            this.tenderOutPurchaseDataGridView.ShowCellToolTips = false;
            this.tenderOutPurchaseDataGridView.ShowEditingIcon = false;
            this.tenderOutPurchaseDataGridView.ShowRowErrors = false;
            this.tenderOutPurchaseDataGridView.Size = new System.Drawing.Size(454, 160);
            this.tenderOutPurchaseDataGridView.TabIndex = 5;
            this.tenderOutPurchaseDataGridView.TabStop = false;
            this.tenderOutPurchaseDataGridView.Visible = false;
            // 
            // purchaseTenderType
            // 
            this.purchaseTenderType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.purchaseTenderType.DataPropertyName = "Purchase Tender Type";
            this.purchaseTenderType.Frozen = true;
            this.purchaseTenderType.HeaderText = "Purchase Tender Type";
            this.purchaseTenderType.MaxInputLength = 16;
            this.purchaseTenderType.MinimumWidth = 115;
            this.purchaseTenderType.Name = "purchaseTenderType";
            this.purchaseTenderType.ReadOnly = true;
            this.purchaseTenderType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.purchaseTenderType.Width = 115;
            // 
            // purchaseReferenceNumberColumn
            // 
            this.purchaseReferenceNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.purchaseReferenceNumberColumn.DataPropertyName = "Reference Number";
            this.purchaseReferenceNumberColumn.FillWeight = 130.5556F;
            this.purchaseReferenceNumberColumn.Frozen = true;
            this.purchaseReferenceNumberColumn.HeaderText = "Reference Number";
            this.purchaseReferenceNumberColumn.MaxInputLength = 200;
            this.purchaseReferenceNumberColumn.MinimumWidth = 195;
            this.purchaseReferenceNumberColumn.Name = "purchaseReferenceNumberColumn";
            this.purchaseReferenceNumberColumn.ReadOnly = true;
            this.purchaseReferenceNumberColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.purchaseReferenceNumberColumn.Width = 195;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Type.Frozen = true;
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 55;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Type.Width = 55;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.FillWeight = 69.44444F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 90;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 90;
            // 
            // tenderOutRefundDataGridView
            // 
            this.tenderOutRefundDataGridView.AllowUserToAddRows = false;
            this.tenderOutRefundDataGridView.AllowUserToDeleteRows = false;
            this.tenderOutRefundDataGridView.AllowUserToResizeColumns = false;
            this.tenderOutRefundDataGridView.AllowUserToResizeRows = false;
            this.tenderOutRefundDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tenderOutRefundDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.tenderOutRefundDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tenderOutRefundDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.tenderOutRefundDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.tenderOutRefundDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tenderOutRefundDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.tenderOutRefundDataGridView.ColumnHeadersHeight = 40;
            this.tenderOutRefundDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.tenderOutRefundDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.RefundedCardType,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tenderOutRefundDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.tenderOutRefundDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tenderOutRefundDataGridView.Enabled = false;
            this.tenderOutRefundDataGridView.EnableHeadersVisualStyles = false;
            this.tenderOutRefundDataGridView.GridColor = System.Drawing.Color.Black;
            this.tenderOutRefundDataGridView.Location = new System.Drawing.Point(0, 164);
            this.tenderOutRefundDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.tenderOutRefundDataGridView.MultiSelect = false;
            this.tenderOutRefundDataGridView.Name = "tenderOutRefundDataGridView";
            this.tenderOutRefundDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tenderOutRefundDataGridView.RowHeadersVisible = false;
            this.tenderOutRefundDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.tenderOutRefundDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tenderOutRefundDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tenderOutRefundDataGridView.ShowCellErrors = false;
            this.tenderOutRefundDataGridView.ShowCellToolTips = false;
            this.tenderOutRefundDataGridView.ShowEditingIcon = false;
            this.tenderOutRefundDataGridView.ShowRowErrors = false;
            this.tenderOutRefundDataGridView.Size = new System.Drawing.Size(454, 153);
            this.tenderOutRefundDataGridView.TabIndex = 6;
            this.tenderOutRefundDataGridView.TabStop = false;
            this.tenderOutRefundDataGridView.Visible = false;
            this.tenderOutRefundDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tenderOutRefundDataGridView_CellContentDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Refunded Tender Type";
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Refunded Tender Type";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 16;
            this.dataGridViewTextBoxColumn1.MinimumWidth = 105;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 105;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Reference Number";
            this.dataGridViewTextBoxColumn2.FillWeight = 130.5556F;
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "Reference Number";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 256;
            this.dataGridViewTextBoxColumn2.MinimumWidth = 195;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.Width = 195;
            // 
            // RefundedCardType
            // 
            this.RefundedCardType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RefundedCardType.DataPropertyName = "Refunded Card Type";
            this.RefundedCardType.HeaderText = "Type";
            this.RefundedCardType.MinimumWidth = 55;
            this.RefundedCardType.Name = "RefundedCardType";
            this.RefundedCardType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.RefundedCardType.Width = 55;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.FillWeight = 69.44444F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 90;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 90;
            // 
            // remainingLabel
            // 
            this.remainingLabel.AutoSize = true;
            this.remainingLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remainingLabel.Location = new System.Drawing.Point(243, 541);
            this.remainingLabel.Name = "remainingLabel";
            this.remainingLabel.Size = new System.Drawing.Size(92, 16);
            this.remainingLabel.TabIndex = 7;
            this.remainingLabel.Text = "Balance Due:";
            // 
            // remainingAmountFieldLabel
            // 
            this.remainingAmountFieldLabel.AutoSize = true;
            this.remainingAmountFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remainingAmountFieldLabel.Location = new System.Drawing.Point(356, 541);
            this.remainingAmountFieldLabel.Name = "remainingAmountFieldLabel";
            this.remainingAmountFieldLabel.Size = new System.Drawing.Size(69, 16);
            this.remainingAmountFieldLabel.TabIndex = 8;
            this.remainingAmountFieldLabel.Text = "<amount>";
            this.remainingAmountFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tenderOutPartialRefundDataGridView
            // 
            this.tenderOutPartialRefundDataGridView.AllowUserToAddRows = false;
            this.tenderOutPartialRefundDataGridView.AllowUserToDeleteRows = false;
            this.tenderOutPartialRefundDataGridView.AllowUserToResizeColumns = false;
            this.tenderOutPartialRefundDataGridView.AllowUserToResizeRows = false;
            this.tenderOutPartialRefundDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tenderOutPartialRefundDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.tenderOutPartialRefundDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tenderOutPartialRefundDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.tenderOutPartialRefundDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.tenderOutPartialRefundDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tenderOutPartialRefundDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.tenderOutPartialRefundDataGridView.ColumnHeadersHeight = 40;
            this.tenderOutPartialRefundDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.tenderOutPartialRefundDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tenderOutPartialRefundDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.tenderOutPartialRefundDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.tenderOutPartialRefundDataGridView.Enabled = false;
            this.tenderOutPartialRefundDataGridView.EnableHeadersVisualStyles = false;
            this.tenderOutPartialRefundDataGridView.GridColor = System.Drawing.Color.Black;
            this.tenderOutPartialRefundDataGridView.Location = new System.Drawing.Point(0, 164);
            this.tenderOutPartialRefundDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.tenderOutPartialRefundDataGridView.MultiSelect = false;
            this.tenderOutPartialRefundDataGridView.Name = "tenderOutPartialRefundDataGridView";
            this.tenderOutPartialRefundDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.tenderOutPartialRefundDataGridView.RowHeadersVisible = false;
            this.tenderOutPartialRefundDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.tenderOutPartialRefundDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tenderOutPartialRefundDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tenderOutPartialRefundDataGridView.ShowCellErrors = false;
            this.tenderOutPartialRefundDataGridView.ShowCellToolTips = false;
            this.tenderOutPartialRefundDataGridView.ShowEditingIcon = false;
            this.tenderOutPartialRefundDataGridView.ShowRowErrors = false;
            this.tenderOutPartialRefundDataGridView.Size = new System.Drawing.Size(454, 132);
            this.tenderOutPartialRefundDataGridView.TabIndex = 9;
            this.tenderOutPartialRefundDataGridView.TabStop = false;
            this.tenderOutPartialRefundDataGridView.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Refunded Tender Type";
            this.dataGridViewTextBoxColumn5.Frozen = true;
            this.dataGridViewTextBoxColumn5.HeaderText = "Refunded Tender Type";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 16;
            this.dataGridViewTextBoxColumn5.MinimumWidth = 105;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.Width = 105;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Reference Number";
            this.dataGridViewTextBoxColumn6.FillWeight = 130.5556F;
            this.dataGridViewTextBoxColumn6.Frozen = true;
            this.dataGridViewTextBoxColumn6.HeaderText = "Reference Number";
            this.dataGridViewTextBoxColumn6.MaxInputLength = 256;
            this.dataGridViewTextBoxColumn6.MinimumWidth = 195;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.Width = 195;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Refunded Card Type";
            this.dataGridViewTextBoxColumn7.HeaderText = "Type";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 55;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn7.Width = 55;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn8.FillWeight = 69.44444F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 90;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 90;
            // 
            // TenderTablePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tenderOutPartialRefundDataGridView);
            this.Controls.Add(this.tenderInDataGridView);
            this.Controls.Add(this.remainingAmountFieldLabel);
            this.Controls.Add(this.remainingLabel);
            this.Controls.Add(this.tenderOutRefundDataGridView);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.totalLabel);
            this.Controls.Add(this.tenderOutPurchaseDataGridView);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(490, 680);
            this.MinimumSize = new System.Drawing.Size(490, 480);
            this.Name = "TenderTablePanel";
            this.Size = new System.Drawing.Size(490, 594);
            this.Load += new System.EventHandler(this.TenderTablePanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tenderInDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenderOutPurchaseDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenderOutRefundDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenderOutPartialRefundDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Label amountLabel;
        private System.Windows.Forms.DataGridView tenderInDataGridView;
        private System.Windows.Forms.DataGridView tenderOutPurchaseDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenderTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn referenceNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountColumn;
        private System.Windows.Forms.DataGridView tenderOutRefundDataGridView;
        private System.Windows.Forms.Label remainingLabel;
        private System.Windows.Forms.Label remainingAmountFieldLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefundedCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchaseTenderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchaseReferenceNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridView tenderOutPartialRefundDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}
