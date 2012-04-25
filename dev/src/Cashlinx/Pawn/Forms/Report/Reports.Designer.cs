using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Report
{
    partial class Reports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reports));
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelReportName = new System.Windows.Forms.Label();
            this.labelAsterisk2 = new System.Windows.Forms.Label();
            this.comboboxReportName = new System.Windows.Forms.ComboBox();
            this.labelAsterisk4 = new System.Windows.Forms.Label();
            this.labelReportEndDate = new System.Windows.Forms.Label();
            this.labelAsterisk6 = new System.Windows.Forms.Label();
            this.comboboxSortType = new System.Windows.Forms.ComboBox();
            this.labelReportSortType = new System.Windows.Forms.Label();
            this.comboboxReportDetail = new System.Windows.Forms.ComboBox();
            this.labelAsterisk5 = new System.Windows.Forms.Label();
            this.labelReportDetail = new System.Windows.Forms.Label();
            this.labelReportType = new System.Windows.Forms.Label();
            this.labelAsterisk3 = new System.Windows.Forms.Label();
            this.labelReportStartDate = new System.Windows.Forms.Label();
            this.comboboxReportType = new System.Windows.Forms.ComboBox();
            this.labelAsterisk1 = new System.Windows.Forms.Label();
            this.textboxReport = new System.Windows.Forms.TextBox();
            this.labelAsterisk7 = new System.Windows.Forms.Label();
            this.comboboxShopNumber = new System.Windows.Forms.ComboBox();
            this.labelReportShopNumber = new System.Windows.Forms.Label();
            this.lblAisle = new System.Windows.Forms.Label();
            this.lblShelf = new System.Windows.Forms.Label();
            this.lblOther = new System.Windows.Forms.Label();
            this.cmbOther = new System.Windows.Forms.ComboBox();
            this.txtAisle = new System.Windows.Forms.TextBox();
            this.txtShelf = new System.Windows.Forms.TextBox();
            this.custombuttonView = new Common.Libraries.Forms.Components.CustomButton();
            this.custombuttonCancel = new Common.Libraries.Forms.Components.CustomButton();
            this.dateCalendarStart = new DateCalendar();
            this.dateCalendarEnd = new DateCalendar();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(28, 18);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(63, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Reports";
            // 
            // labelReportName
            // 
            this.labelReportName.AutoSize = true;
            this.labelReportName.BackColor = System.Drawing.Color.Transparent;
            this.labelReportName.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelReportName.Location = new System.Drawing.Point(397, 63);
            this.labelReportName.Name = "labelReportName";
            this.labelReportName.Size = new System.Drawing.Size(100, 18);
            this.labelReportName.TabIndex = 3;
            this.labelReportName.Text = "Report Name:";
            // 
            // labelAsterisk2
            // 
            this.labelAsterisk2.AutoSize = true;
            this.labelAsterisk2.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterisk2.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk2.Location = new System.Drawing.Point(491, 63);
            this.labelAsterisk2.Name = "labelAsterisk2";
            this.labelAsterisk2.Size = new System.Drawing.Size(13, 16);
            this.labelAsterisk2.TabIndex = 15;
            this.labelAsterisk2.Text = "*";
            // 
            // comboboxReportName
            // 
            this.comboboxReportName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboboxReportName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxReportName.ForeColor = System.Drawing.Color.Black;
            this.comboboxReportName.FormattingEnabled = true;
            this.comboboxReportName.Location = new System.Drawing.Point(510, 63);
            this.comboboxReportName.Name = "comboboxReportName";
            this.comboboxReportName.Size = new System.Drawing.Size(204, 21);
            this.comboboxReportName.TabIndex = 1;
            this.comboboxReportName.SelectedIndexChanged += new System.EventHandler(this.comboboxReportName_SelectedIndexChanged);
            this.comboboxReportName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.comboboxReportName_MouseClick);
            // 
            // labelAsterisk4
            // 
            this.labelAsterisk4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAsterisk4.AutoSize = true;
            this.labelAsterisk4.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterisk4.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk4.Location = new System.Drawing.Point(466, 142);
            this.labelAsterisk4.Name = "labelAsterisk4";
            this.labelAsterisk4.Size = new System.Drawing.Size(13, 16);
            this.labelAsterisk4.TabIndex = 17;
            this.labelAsterisk4.Text = "*";
            // 
            // labelReportEndDate
            // 
            this.labelReportEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelReportEndDate.AutoSize = true;
            this.labelReportEndDate.BackColor = System.Drawing.Color.Transparent;
            this.labelReportEndDate.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelReportEndDate.Location = new System.Drawing.Point(397, 142);
            this.labelReportEndDate.Name = "labelReportEndDate";
            this.labelReportEndDate.Size = new System.Drawing.Size(73, 18);
            this.labelReportEndDate.TabIndex = 6;
            this.labelReportEndDate.Text = "End Date:";
            // 
            // labelAsterisk6
            // 
            this.labelAsterisk6.AutoSize = true;
            this.labelAsterisk6.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterisk6.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk6.Location = new System.Drawing.Point(472, 103);
            this.labelAsterisk6.Name = "labelAsterisk6";
            this.labelAsterisk6.Size = new System.Drawing.Size(13, 16);
            this.labelAsterisk6.TabIndex = 19;
            this.labelAsterisk6.Text = "*";
            // 
            // comboboxSortType
            // 
            this.comboboxSortType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboboxSortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxSortType.ForeColor = System.Drawing.Color.Black;
            this.comboboxSortType.FormattingEnabled = true;
            this.comboboxSortType.Location = new System.Drawing.Point(510, 103);
            this.comboboxSortType.Name = "comboboxSortType";
            this.comboboxSortType.Size = new System.Drawing.Size(144, 21);
            this.comboboxSortType.TabIndex = 13;
            // 
            // labelReportSortType
            // 
            this.labelReportSortType.AutoSize = true;
            this.labelReportSortType.BackColor = System.Drawing.Color.Transparent;
            this.labelReportSortType.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelReportSortType.Location = new System.Drawing.Point(397, 103);
            this.labelReportSortType.Name = "labelReportSortType";
            this.labelReportSortType.Size = new System.Drawing.Size(78, 18);
            this.labelReportSortType.TabIndex = 12;
            this.labelReportSortType.Text = "Sort Type:";
            // 
            // comboboxReportDetail
            // 
            this.comboboxReportDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboboxReportDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxReportDetail.FormattingEnabled = true;
            this.comboboxReportDetail.Location = new System.Drawing.Point(136, 102);
            this.comboboxReportDetail.Name = "comboboxReportDetail";
            this.comboboxReportDetail.Size = new System.Drawing.Size(204, 21);
            this.comboboxReportDetail.TabIndex = 11;
            this.comboboxReportDetail.SelectedIndexChanged += new System.EventHandler(this.comboboxReportDetail_SelectedIndexChanged);
            // 
            // labelAsterisk5
            // 
            this.labelAsterisk5.AutoSize = true;
            this.labelAsterisk5.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterisk5.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk5.Location = new System.Drawing.Point(114, 102);
            this.labelAsterisk5.Name = "labelAsterisk5";
            this.labelAsterisk5.Size = new System.Drawing.Size(13, 16);
            this.labelAsterisk5.TabIndex = 18;
            this.labelAsterisk5.Text = "*";
            // 
            // labelReportDetail
            // 
            this.labelReportDetail.AutoSize = true;
            this.labelReportDetail.BackColor = System.Drawing.Color.Transparent;
            this.labelReportDetail.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelReportDetail.Location = new System.Drawing.Point(23, 102);
            this.labelReportDetail.Name = "labelReportDetail";
            this.labelReportDetail.Size = new System.Drawing.Size(96, 18);
            this.labelReportDetail.TabIndex = 10;
            this.labelReportDetail.Text = "Report Detail:";
            // 
            // labelReportType
            // 
            this.labelReportType.AutoSize = true;
            this.labelReportType.BackColor = System.Drawing.Color.Transparent;
            this.labelReportType.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelReportType.Location = new System.Drawing.Point(23, 63);
            this.labelReportType.Name = "labelReportType";
            this.labelReportType.Size = new System.Drawing.Size(95, 18);
            this.labelReportType.TabIndex = 2;
            this.labelReportType.Text = "Report Type:";
            // 
            // labelAsterisk3
            // 
            this.labelAsterisk3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAsterisk3.AutoSize = true;
            this.labelAsterisk3.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterisk3.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk3.Location = new System.Drawing.Point(100, 142);
            this.labelAsterisk3.Name = "labelAsterisk3";
            this.labelAsterisk3.Size = new System.Drawing.Size(13, 16);
            this.labelAsterisk3.TabIndex = 16;
            this.labelAsterisk3.Text = "*";
            // 
            // labelReportStartDate
            // 
            this.labelReportStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelReportStartDate.AutoSize = true;
            this.labelReportStartDate.BackColor = System.Drawing.Color.Transparent;
            this.labelReportStartDate.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelReportStartDate.Location = new System.Drawing.Point(23, 142);
            this.labelReportStartDate.Name = "labelReportStartDate";
            this.labelReportStartDate.Size = new System.Drawing.Size(80, 18);
            this.labelReportStartDate.TabIndex = 7;
            this.labelReportStartDate.Text = "Start Date:";
            // 
            // comboboxReportType
            // 
            this.comboboxReportType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboboxReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxReportType.ForeColor = System.Drawing.Color.Black;
            this.comboboxReportType.FormattingEnabled = true;
            this.comboboxReportType.Items.AddRange(new object[] {
            "Select",
            "Monthly",
            "Daily",
            "Inquiry"});
            this.comboboxReportType.Location = new System.Drawing.Point(136, 63);
            this.comboboxReportType.Name = "comboboxReportType";
            this.comboboxReportType.Size = new System.Drawing.Size(144, 21);
            this.comboboxReportType.TabIndex = 0;
            this.comboboxReportType.SelectedIndexChanged += new System.EventHandler(this.comboboxReportType_SelectedIndexChanged);
            // 
            // labelAsterisk1
            // 
            this.labelAsterisk1.AutoSize = true;
            this.labelAsterisk1.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterisk1.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk1.Location = new System.Drawing.Point(115, 63);
            this.labelAsterisk1.Name = "labelAsterisk1";
            this.labelAsterisk1.Size = new System.Drawing.Size(13, 16);
            this.labelAsterisk1.TabIndex = 14;
            this.labelAsterisk1.Text = "*";
            // 
            // textboxReport
            // 
            this.textboxReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textboxReport.Location = new System.Drawing.Point(140, 144);
            this.textboxReport.Name = "textboxReport";
            this.textboxReport.Size = new System.Drawing.Size(98, 21);
            this.textboxReport.TabIndex = 20;
            // 
            // labelAsterisk7
            // 
            this.labelAsterisk7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAsterisk7.AutoSize = true;
            this.labelAsterisk7.BackColor = System.Drawing.Color.Transparent;
            this.labelAsterisk7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsterisk7.ForeColor = System.Drawing.Color.Red;
            this.labelAsterisk7.Location = new System.Drawing.Point(358, 142);
            this.labelAsterisk7.Name = "labelAsterisk7";
            this.labelAsterisk7.Size = new System.Drawing.Size(13, 16);
            this.labelAsterisk7.TabIndex = 23;
            this.labelAsterisk7.Text = "*";
            // 
            // comboboxShopNumber
            // 
            this.comboboxShopNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboboxShopNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboboxShopNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxShopNumber.ForeColor = System.Drawing.Color.Black;
            this.comboboxShopNumber.FormattingEnabled = true;
            this.comboboxShopNumber.Location = new System.Drawing.Point(397, 141);
            this.comboboxShopNumber.Name = "comboboxShopNumber";
            this.comboboxShopNumber.Size = new System.Drawing.Size(144, 21);
            this.comboboxShopNumber.TabIndex = 22;
            // 
            // labelReportShopNumber
            // 
            this.labelReportShopNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelReportShopNumber.AutoSize = true;
            this.labelReportShopNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelReportShopNumber.Font = new System.Drawing.Font("Tahoma", 11F);
            this.labelReportShopNumber.Location = new System.Drawing.Point(262, 142);
            this.labelReportShopNumber.Name = "labelReportShopNumber";
            this.labelReportShopNumber.Size = new System.Drawing.Size(102, 18);
            this.labelReportShopNumber.TabIndex = 21;
            this.labelReportShopNumber.Text = "Shop Number:";
            // 
            // lblAisle
            // 
            this.lblAisle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAisle.AutoSize = true;
            this.lblAisle.BackColor = System.Drawing.Color.Transparent;
            this.lblAisle.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblAisle.Location = new System.Drawing.Point(55, 187);
            this.lblAisle.Name = "lblAisle";
            this.lblAisle.Size = new System.Drawing.Size(36, 18);
            this.lblAisle.TabIndex = 24;
            this.lblAisle.Text = "Aisle";
            // 
            // lblShelf
            // 
            this.lblShelf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblShelf.AutoSize = true;
            this.lblShelf.BackColor = System.Drawing.Color.Transparent;
            this.lblShelf.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblShelf.Location = new System.Drawing.Point(304, 187);
            this.lblShelf.Name = "lblShelf";
            this.lblShelf.Size = new System.Drawing.Size(39, 18);
            this.lblShelf.TabIndex = 25;
            this.lblShelf.Text = "Shelf";
            // 
            // lblOther
            // 
            this.lblOther.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOther.AutoSize = true;
            this.lblOther.BackColor = System.Drawing.Color.Transparent;
            this.lblOther.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblOther.Location = new System.Drawing.Point(542, 187);
            this.lblOther.Name = "lblOther";
            this.lblOther.Size = new System.Drawing.Size(45, 18);
            this.lblOther.TabIndex = 26;
            this.lblOther.Text = "Other";
            // 
            // cmbOther
            // 
            this.cmbOther.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbOther.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbOther.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOther.ForeColor = System.Drawing.Color.Black;
            this.cmbOther.FormattingEnabled = true;
            this.cmbOther.Location = new System.Drawing.Point(593, 184);
            this.cmbOther.Name = "cmbOther";
            this.cmbOther.Size = new System.Drawing.Size(144, 21);
            this.cmbOther.TabIndex = 29;
            // 
            // txtAisle
            // 
            this.txtAisle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAisle.Location = new System.Drawing.Point(136, 184);
            this.txtAisle.Name = "txtAisle";
            this.txtAisle.Size = new System.Drawing.Size(155, 21);
            this.txtAisle.TabIndex = 30;
            // 
            // txtShelf
            // 
            this.txtShelf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtShelf.Location = new System.Drawing.Point(349, 184);
            this.txtShelf.Name = "txtShelf";
            this.txtShelf.Size = new System.Drawing.Size(136, 21);
            this.txtShelf.TabIndex = 31;
            // 
            // custombuttonView
            // 
            this.custombuttonView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.custombuttonView.BackColor = System.Drawing.Color.Transparent;
            this.custombuttonView.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("custombuttonView.BackgroundImage")));
            this.custombuttonView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.custombuttonView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.custombuttonView.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.custombuttonView.FlatAppearance.BorderSize = 0;
            this.custombuttonView.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.custombuttonView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.custombuttonView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.custombuttonView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custombuttonView.ForeColor = System.Drawing.Color.White;
            this.custombuttonView.Location = new System.Drawing.Point(677, 220);
            this.custombuttonView.Margin = new System.Windows.Forms.Padding(0);
            this.custombuttonView.MaximumSize = new System.Drawing.Size(100, 50);
            this.custombuttonView.MinimumSize = new System.Drawing.Size(100, 50);
            this.custombuttonView.Name = "custombuttonView";
            this.custombuttonView.Size = new System.Drawing.Size(100, 50);
            this.custombuttonView.TabIndex = 2;
            this.custombuttonView.Text = "View";
            this.custombuttonView.UseVisualStyleBackColor = false;
            this.custombuttonView.Click += new System.EventHandler(this.custombuttonViewReport_Click);
            // 
            // custombuttonCancel
            // 
            this.custombuttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.custombuttonCancel.BackColor = System.Drawing.Color.Transparent;
            this.custombuttonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("custombuttonCancel.BackgroundImage")));
            this.custombuttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.custombuttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.custombuttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.custombuttonCancel.FlatAppearance.BorderSize = 0;
            this.custombuttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.custombuttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.custombuttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.custombuttonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custombuttonCancel.ForeColor = System.Drawing.Color.White;
            this.custombuttonCancel.Location = new System.Drawing.Point(41, 220);
            this.custombuttonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.custombuttonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.custombuttonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.custombuttonCancel.Name = "custombuttonCancel";
            this.custombuttonCancel.Size = new System.Drawing.Size(100, 50);
            this.custombuttonCancel.TabIndex = 1;
            this.custombuttonCancel.Text = "Cancel";
            this.custombuttonCancel.UseVisualStyleBackColor = false;
            this.custombuttonCancel.Click += new System.EventHandler(this.custombuttonCancelReport_Click);
            // 
            // dateCalendarStart
            // 
            this.dateCalendarStart.AllowKeyUpAndDown = false;
            this.dateCalendarStart.AllowMonthlySelection = false;
            this.dateCalendarStart.AllowWeekends = true;
            this.dateCalendarStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateCalendarStart.AutoSize = true;
            this.dateCalendarStart.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarStart.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarStart.Location = new System.Drawing.Point(136, 142);
            this.dateCalendarStart.Name = "dateCalendarStart";
            this.dateCalendarStart.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarStart.SelectedDate = "10/12/2009";
            this.dateCalendarStart.Size = new System.Drawing.Size(255, 32);
            this.dateCalendarStart.TabIndex = 8;
            this.dateCalendarStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateCalendarStart.Leave += new System.EventHandler(this.dateCalendarStart_Leave);
            this.dateCalendarStart.Validating += new System.ComponentModel.CancelEventHandler(this.dateCalendarStart_Validating);
            // 
            // dateCalendarEnd
            // 
            this.dateCalendarEnd.AllowKeyUpAndDown = false;
            this.dateCalendarEnd.AllowMonthlySelection = false;
            this.dateCalendarEnd.AllowWeekends = true;
            this.dateCalendarEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateCalendarEnd.AutoSize = true;
            this.dateCalendarEnd.BackColor = System.Drawing.Color.Transparent;
            this.dateCalendarEnd.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateCalendarEnd.Location = new System.Drawing.Point(510, 142);
            this.dateCalendarEnd.Name = "dateCalendarEnd";
            this.dateCalendarEnd.PositionPopupCalendarOverTextbox = false;
            this.dateCalendarEnd.SelectedDate = "10/12/2009";
            this.dateCalendarEnd.Size = new System.Drawing.Size(267, 32);
            this.dateCalendarEnd.TabIndex = 9;
            this.dateCalendarEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.dateCalendarEnd.Leave += new System.EventHandler(this.dateCalendarEnd_Leave);
            this.dateCalendarEnd.Resize += new System.EventHandler(this.dateCalendarEnd_Resize);
            this.dateCalendarEnd.Validating += new System.ComponentModel.CancelEventHandler(this.dateCalendarEnd_Validating);
            // 
            // Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(845, 295);
            this.ControlBox = false;
            this.Controls.Add(this.txtShelf);
            this.Controls.Add(this.txtAisle);
            this.Controls.Add(this.cmbOther);
            this.Controls.Add(this.lblOther);
            this.Controls.Add(this.lblShelf);
            this.Controls.Add(this.lblAisle);
            this.Controls.Add(this.labelAsterisk7);
            this.Controls.Add(this.comboboxShopNumber);
            this.Controls.Add(this.labelReportShopNumber);
            this.Controls.Add(this.dateCalendarStart);
            this.Controls.Add(this.dateCalendarEnd);
            this.Controls.Add(this.custombuttonView);
            this.Controls.Add(this.custombuttonCancel);
            this.Controls.Add(this.labelAsterisk6);
            this.Controls.Add(this.labelAsterisk5);
            this.Controls.Add(this.labelAsterisk4);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(this.labelAsterisk3);
            this.Controls.Add(this.labelAsterisk2);
            this.Controls.Add(this.comboboxReportType);
            this.Controls.Add(this.labelAsterisk1);
            this.Controls.Add(this.comboboxReportName);
            this.Controls.Add(this.labelReportType);
            this.Controls.Add(this.labelReportName);
            this.Controls.Add(this.comboboxSortType);
            this.Controls.Add(this.labelReportEndDate);
            this.Controls.Add(this.labelReportSortType);
            this.Controls.Add(this.labelReportStartDate);
            this.Controls.Add(this.comboboxReportDetail);
            this.Controls.Add(this.labelReportDetail);
            this.Controls.Add(this.textboxReport);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reports";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.Reports_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private CustomButton custombuttonCancel;
        private CustomButton custombuttonView;
        private System.Windows.Forms.Label labelReportName;
        private System.Windows.Forms.Label labelAsterisk2;
        private System.Windows.Forms.ComboBox comboboxReportName;
        private DateCalendar dateCalendarEnd;
        private System.Windows.Forms.Label labelAsterisk4;
        private System.Windows.Forms.Label labelReportEndDate;
        private System.Windows.Forms.Label labelAsterisk6;
        private System.Windows.Forms.ComboBox comboboxSortType;
        private System.Windows.Forms.Label labelReportSortType;
        private System.Windows.Forms.ComboBox comboboxReportDetail;
        private System.Windows.Forms.Label labelAsterisk5;
        private System.Windows.Forms.Label labelReportDetail;
        private System.Windows.Forms.Label labelReportType;
        private System.Windows.Forms.Label labelAsterisk3;
        private System.Windows.Forms.Label labelReportStartDate;
        private DateCalendar dateCalendarStart;
        private System.Windows.Forms.ComboBox comboboxReportType;
        private System.Windows.Forms.Label labelAsterisk1;
        private System.Windows.Forms.TextBox textboxReport;
        private System.Windows.Forms.Label labelAsterisk7;
        private System.Windows.Forms.ComboBox comboboxShopNumber;
        private System.Windows.Forms.Label labelReportShopNumber;
        private System.Windows.Forms.Label lblAisle;
        private System.Windows.Forms.Label lblShelf;
        private System.Windows.Forms.Label lblOther;
        private System.Windows.Forms.ComboBox cmbOther;
        private System.Windows.Forms.TextBox txtAisle;
        private System.Windows.Forms.TextBox txtShelf;
    }
}
