using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Layaway
{
    partial class LayawayDetailViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblLayawayDetailDescription = new System.Windows.Forms.Label();
            this.lblDateAndTimeValue = new System.Windows.Forms.Label();
            this.lblPaymentList = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblForfeitureAmountValue = new System.Windows.Forms.Label();
            this.lblForfeitureAmount = new System.Windows.Forms.Label();
            this.lblPaymentDueDateValue = new System.Windows.Forms.Label();
            this.lblPaymentDueDate = new System.Windows.Forms.Label();
            this.lblCurrentStatusValue = new System.Windows.Forms.Label();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblTotalLayawayCostValue = new System.Windows.Forms.Label();
            this.lblTotalLayawayCost = new System.Windows.Forms.Label();
            this.lblLayawayNumber = new System.Windows.Forms.Label();
            this.lblLayawayNumberValue = new System.Windows.Forms.Label();
            this.lblDateAndTime = new System.Windows.Forms.Label();
            this.flowLayoutPanelPayments = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.gvLayawayItems = new CustomDataGridView();
            this.colIcn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAisleShelfLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCostAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanelPayments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLayawayItems)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLayawayDetailDescription
            // 
            this.lblLayawayDetailDescription.AutoSize = true;
            this.lblLayawayDetailDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblLayawayDetailDescription.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayawayDetailDescription.ForeColor = System.Drawing.Color.White;
            this.lblLayawayDetailDescription.Location = new System.Drawing.Point(12, 37);
            this.lblLayawayDetailDescription.Name = "lblLayawayDetailDescription";
            this.lblLayawayDetailDescription.Size = new System.Drawing.Size(205, 17);
            this.lblLayawayDetailDescription.TabIndex = 0;
            this.lblLayawayDetailDescription.Text = "Layaway Detail Description - ";
            // 
            // lblDateAndTimeValue
            // 
            this.lblDateAndTimeValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDateAndTimeValue.AutoSize = true;
            this.lblDateAndTimeValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDateAndTimeValue.Location = new System.Drawing.Point(390, 6);
            this.lblDateAndTimeValue.Name = "lblDateAndTimeValue";
            this.lblDateAndTimeValue.Size = new System.Drawing.Size(41, 13);
            this.lblDateAndTimeValue.TabIndex = 10;
            this.lblDateAndTimeValue.Text = "label12";
            // 
            // lblPaymentList
            // 
            this.lblPaymentList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPaymentList.AutoSize = true;
            this.lblPaymentList.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentList.Location = new System.Drawing.Point(31, 50);
            this.lblPaymentList.Name = "lblPaymentList";
            this.lblPaymentList.Size = new System.Drawing.Size(84, 13);
            this.lblPaymentList.TabIndex = 10;
            this.lblPaymentList.Text = "Payment List:";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel2.Controls.Add(this.lblForfeitureAmountValue, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblForfeitureAmount, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblPaymentDueDateValue, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblPaymentDueDate, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentStatusValue, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentStatus, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalLayawayCostValue, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalLayawayCost, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblLayawayNumber, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblPaymentList, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblLayawayNumberValue, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblDateAndTime, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblDateAndTimeValue, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanelPayments, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 72);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(775, 100);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // lblForfeitureAmountValue
            // 
            this.lblForfeitureAmountValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblForfeitureAmountValue.AutoSize = true;
            this.lblForfeitureAmountValue.BackColor = System.Drawing.Color.Transparent;
            this.lblForfeitureAmountValue.Location = new System.Drawing.Point(660, 31);
            this.lblForfeitureAmountValue.Name = "lblForfeitureAmountValue";
            this.lblForfeitureAmountValue.Size = new System.Drawing.Size(41, 13);
            this.lblForfeitureAmountValue.TabIndex = 18;
            this.lblForfeitureAmountValue.Text = "label25";
            // 
            // lblForfeitureAmount
            // 
            this.lblForfeitureAmount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblForfeitureAmount.AutoSize = true;
            this.lblForfeitureAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblForfeitureAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForfeitureAmount.Location = new System.Drawing.Point(540, 31);
            this.lblForfeitureAmount.Name = "lblForfeitureAmount";
            this.lblForfeitureAmount.Size = new System.Drawing.Size(114, 13);
            this.lblForfeitureAmount.TabIndex = 17;
            this.lblForfeitureAmount.Text = "Forfeiture Amount:";
            // 
            // lblPaymentDueDateValue
            // 
            this.lblPaymentDueDateValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPaymentDueDateValue.AutoSize = true;
            this.lblPaymentDueDateValue.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentDueDateValue.Location = new System.Drawing.Point(390, 31);
            this.lblPaymentDueDateValue.Name = "lblPaymentDueDateValue";
            this.lblPaymentDueDateValue.Size = new System.Drawing.Size(41, 13);
            this.lblPaymentDueDateValue.TabIndex = 16;
            this.lblPaymentDueDateValue.Text = "label23";
            // 
            // lblPaymentDueDate
            // 
            this.lblPaymentDueDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPaymentDueDate.AutoSize = true;
            this.lblPaymentDueDate.BackColor = System.Drawing.Color.Transparent;
            this.lblPaymentDueDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentDueDate.Location = new System.Drawing.Point(268, 31);
            this.lblPaymentDueDate.Name = "lblPaymentDueDate";
            this.lblPaymentDueDate.Size = new System.Drawing.Size(116, 13);
            this.lblPaymentDueDate.TabIndex = 15;
            this.lblPaymentDueDate.Text = "Payment Due Date:";
            // 
            // lblCurrentStatusValue
            // 
            this.lblCurrentStatusValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentStatusValue.AutoSize = true;
            this.lblCurrentStatusValue.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentStatusValue.Location = new System.Drawing.Point(121, 31);
            this.lblCurrentStatusValue.Name = "lblCurrentStatusValue";
            this.lblCurrentStatusValue.Size = new System.Drawing.Size(41, 13);
            this.lblCurrentStatusValue.TabIndex = 14;
            this.lblCurrentStatusValue.Text = "label21";
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCurrentStatus.AutoSize = true;
            this.lblCurrentStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentStatus.Location = new System.Drawing.Point(22, 31);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Size = new System.Drawing.Size(93, 13);
            this.lblCurrentStatus.TabIndex = 13;
            this.lblCurrentStatus.Text = "Current Status:";
            // 
            // lblTotalLayawayCostValue
            // 
            this.lblTotalLayawayCostValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalLayawayCostValue.AutoSize = true;
            this.lblTotalLayawayCostValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalLayawayCostValue.Location = new System.Drawing.Point(660, 6);
            this.lblTotalLayawayCostValue.Name = "lblTotalLayawayCostValue";
            this.lblTotalLayawayCostValue.Size = new System.Drawing.Size(41, 13);
            this.lblTotalLayawayCostValue.TabIndex = 12;
            this.lblTotalLayawayCostValue.Text = "label19";
            // 
            // lblTotalLayawayCost
            // 
            this.lblTotalLayawayCost.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTotalLayawayCost.AutoSize = true;
            this.lblTotalLayawayCost.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalLayawayCost.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLayawayCost.Location = new System.Drawing.Point(534, 6);
            this.lblTotalLayawayCost.Name = "lblTotalLayawayCost";
            this.lblTotalLayawayCost.Size = new System.Drawing.Size(120, 13);
            this.lblTotalLayawayCost.TabIndex = 11;
            this.lblTotalLayawayCost.Text = "Total Layaway Cost:";
            // 
            // lblLayawayNumber
            // 
            this.lblLayawayNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLayawayNumber.AutoSize = true;
            this.lblLayawayNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblLayawayNumber.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLayawayNumber.Location = new System.Drawing.Point(8, 6);
            this.lblLayawayNumber.Name = "lblLayawayNumber";
            this.lblLayawayNumber.Size = new System.Drawing.Size(107, 13);
            this.lblLayawayNumber.TabIndex = 1;
            this.lblLayawayNumber.Text = "Layaway Number:";
            // 
            // lblLayawayNumberValue
            // 
            this.lblLayawayNumberValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblLayawayNumberValue.AutoSize = true;
            this.lblLayawayNumberValue.BackColor = System.Drawing.Color.Transparent;
            this.lblLayawayNumberValue.Location = new System.Drawing.Point(121, 6);
            this.lblLayawayNumberValue.Name = "lblLayawayNumberValue";
            this.lblLayawayNumberValue.Size = new System.Drawing.Size(41, 13);
            this.lblLayawayNumberValue.TabIndex = 2;
            this.lblLayawayNumberValue.Text = "label16";
            // 
            // lblDateAndTime
            // 
            this.lblDateAndTime.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDateAndTime.AutoSize = true;
            this.lblDateAndTime.BackColor = System.Drawing.Color.Transparent;
            this.lblDateAndTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateAndTime.Location = new System.Drawing.Point(292, 6);
            this.lblDateAndTime.Name = "lblDateAndTime";
            this.lblDateAndTime.Size = new System.Drawing.Size(92, 13);
            this.lblDateAndTime.TabIndex = 3;
            this.lblDateAndTime.Text = "Date and Time:";
            // 
            // flowLayoutPanelPayments
            // 
            this.flowLayoutPanelPayments.AutoScroll = true;
            this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanelPayments, 5);
            this.flowLayoutPanelPayments.Controls.Add(this.label2);
            this.flowLayoutPanelPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelPayments.Location = new System.Drawing.Point(121, 53);
            this.flowLayoutPanelPayments.Name = "flowLayoutPanelPayments";
            this.flowLayoutPanelPayments.Size = new System.Drawing.Size(651, 44);
            this.flowLayoutPanelPayments.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "1] 05/29/2010  $2300.00";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSize = true;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(688, 460);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 40);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.AutoSize = true;
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(117, 460);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 40);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.AutoSize = true;
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrevious.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnPrevious.FlatAppearance.BorderSize = 0;
            this.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.Location = new System.Drawing.Point(12, 460);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(100, 40);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // gvLayawayItems
            // 
            this.gvLayawayItems.AllowUserToAddRows = false;
            this.gvLayawayItems.AllowUserToDeleteRows = false;
            this.gvLayawayItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvLayawayItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvLayawayItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvLayawayItems.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvLayawayItems.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvLayawayItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvLayawayItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLayawayItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIcn,
            this.colDescription,
            this.colAisleShelfLocation,
            this.colCostAmount});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvLayawayItems.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvLayawayItems.GridColor = System.Drawing.Color.LightGray;
            this.gvLayawayItems.Location = new System.Drawing.Point(12, 179);
            this.gvLayawayItems.Margin = new System.Windows.Forms.Padding(0);
            this.gvLayawayItems.MultiSelect = false;
            this.gvLayawayItems.Name = "gvLayawayItems";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLayawayItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvLayawayItems.RowHeadersVisible = false;
            this.gvLayawayItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvLayawayItems.Size = new System.Drawing.Size(775, 275);
            this.gvLayawayItems.TabIndex = 15;
            // 
            // colIcn
            // 
            this.colIcn.HeaderText = "ICN";
            this.colIcn.Name = "colIcn";
            this.colIcn.ReadOnly = true;
            // 
            // colDescription
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colDescription.DefaultCellStyle = dataGridViewCellStyle2;
            this.colDescription.FillWeight = 200F;
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colAisleShelfLocation
            // 
            this.colAisleShelfLocation.HeaderText = "Aisle Shelf Location";
            this.colAisleShelfLocation.Name = "colAisleShelfLocation";
            this.colAisleShelfLocation.ReadOnly = true;
            // 
            // colCostAmount
            // 
            this.colCostAmount.HeaderText = "Cost Amount";
            this.colCostAmount.Name = "colCostAmount";
            this.colCostAmount.ReadOnly = true;
            // 
            // lblPage
            // 
            this.lblPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPage.BackColor = System.Drawing.Color.Transparent;
            this.lblPage.Location = new System.Drawing.Point(300, 469);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(200, 23);
            this.lblPage.TabIndex = 16;
            this.lblPage.Text = "Page 1 of 1";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnComplete
            // 
            this.btnComplete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComplete.AutoSize = true;
            this.btnComplete.BackColor = System.Drawing.Color.Transparent;
            this.btnComplete.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnComplete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnComplete.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnComplete.FlatAppearance.BorderSize = 0;
            this.btnComplete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnComplete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComplete.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Location = new System.Drawing.Point(582, 460);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(100, 40);
            this.btnComplete.TabIndex = 17;
            this.btnComplete.Text = "Complete";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.AutoSize = true;
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = Common.Properties.Resources.blueglossy_small;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(223, 460);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.TabIndex = 18;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // LayawayDetailViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Common.Properties.Resources.newDialog_512_BlueScale;
            this.ClientSize = new System.Drawing.Size(800, 512);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.lblPage);
            this.Controls.Add(this.gvLayawayItems);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.lblLayawayDetailDescription);
            this.Name = "LayawayDetailViewer";
            this.Text = "Layaway Detail Viewer";
            this.Shown += new System.EventHandler(this.LayawayDetailViewer_Shown);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanelPayments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvLayawayItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLayawayDetailDescription;
        private System.Windows.Forms.Label lblDateAndTimeValue;
        private System.Windows.Forms.Label lblPaymentList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblLayawayNumberValue;
        private System.Windows.Forms.Label lblDateAndTime;
        private System.Windows.Forms.Label lblTotalLayawayCost;
        private System.Windows.Forms.Label lblTotalLayawayCostValue;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Label lblCurrentStatusValue;
        private System.Windows.Forms.Label lblPaymentDueDate;
        private System.Windows.Forms.Label lblPaymentDueDateValue;
        private System.Windows.Forms.Label lblForfeitureAmount;
        private System.Windows.Forms.Label lblForfeitureAmountValue;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPayments;
        private System.Windows.Forms.Label lblLayawayNumber;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private CustomDataGridView gvLayawayItems;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIcn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAisleShelfLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCostAmount;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnBack;
    }
}
