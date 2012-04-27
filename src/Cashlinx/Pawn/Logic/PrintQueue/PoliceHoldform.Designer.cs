using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Logic.PrintQueue
{
    partial class PoliceHoldform
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelStoreName = new System.Windows.Forms.Label();
            this.labelStoreAddr1 = new System.Windows.Forms.Label();
            this.labelStoreAddr2 = new System.Windows.Forms.Label();
            this.labelTranDate = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelTranNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1CustName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelTranCost = new System.Windows.Forms.Label();
            this.labelEmpNo = new System.Windows.Forms.Label();
            this.labelCustAddress = new System.Windows.Forms.Label();
            this.label1CustDOB = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.itemDescription = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelOfficerBadge = new System.Windows.Forms.Label();
            this.labelOfficerPhone = new System.Windows.Forms.Label();
            this.labelOfficerName = new System.Windows.Forms.Label();
            this.labelHoldExpire = new System.Windows.Forms.Label();
            this.labelCaseNo = new System.Windows.Forms.Label();
            this.labelAgency = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelReqType = new System.Windows.Forms.Label();
            this.labelHoldComment = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(343, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hold Ticket";
            // 
            // labelStoreName
            // 
            this.labelStoreName.AutoSize = true;
            this.labelStoreName.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStoreName.Location = new System.Drawing.Point(279, 31);
            this.labelStoreName.Name = "labelStoreName";
            this.labelStoreName.Size = new System.Drawing.Size(245, 14);
            this.labelStoreName.TabIndex = 1;
            this.labelStoreName.Text = "Cash America Pawn of Fort Worth #1";
            // 
            // labelStoreAddr1
            // 
            this.labelStoreAddr1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelStoreAddr1.AutoSize = true;
            this.labelStoreAddr1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStoreAddr1.Location = new System.Drawing.Point(331, 50);
            this.labelStoreAddr1.Name = "labelStoreAddr1";
            this.labelStoreAddr1.Size = new System.Drawing.Size(126, 14);
            this.labelStoreAddr1.TabIndex = 2;
            this.labelStoreAddr1.Text = "5103 East 27th St";
            // 
            // labelStoreAddr2
            // 
            this.labelStoreAddr2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelStoreAddr2.AutoSize = true;
            this.labelStoreAddr2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStoreAddr2.Location = new System.Drawing.Point(331, 69);
            this.labelStoreAddr2.Name = "labelStoreAddr2";
            this.labelStoreAddr2.Size = new System.Drawing.Size(147, 14);
            this.labelStoreAddr2.TabIndex = 3;
            this.labelStoreAddr2.Text = "Fort Worth, TX 76133";
            // 
            // labelTranDate
            // 
            this.labelTranDate.AutoSize = true;
            this.labelTranDate.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTranDate.Location = new System.Drawing.Point(12, 101);
            this.labelTranDate.Name = "labelTranDate";
            this.labelTranDate.Size = new System.Drawing.Size(119, 14);
            this.labelTranDate.TabIndex = 4;
            this.labelTranDate.Text = "Date: 12/16/2009";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.50623F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.49377F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 216F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.labelTranNo, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1CustName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelTranCost, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelEmpNo, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelCustAddress, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1CustDOB, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 117);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 100);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // labelTranNo
            // 
            this.labelTranNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTranNo.AutoSize = true;
            this.labelTranNo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTranNo.Location = new System.Drawing.Point(590, 67);
            this.labelTranNo.Name = "labelTranNo";
            this.labelTranNo.Size = new System.Drawing.Size(84, 14);
            this.labelTranNo.TabIndex = 14;
            this.labelTranNo.Text = "1234";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 28);
            this.label6.TabIndex = 5;
            this.label6.Text = "Customer First, Last:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(139, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "DOB:";
            // 
            // label1CustName
            // 
            this.label1CustName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1CustName.AutoSize = true;
            this.label1CustName.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1CustName.Location = new System.Drawing.Point(4, 67);
            this.label1CustName.Name = "label1CustName";
            this.label1CustName.Size = new System.Drawing.Size(128, 14);
            this.label1CustName.TabIndex = 12;
            this.label1CustName.Text = "Robert Smith";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(235, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(210, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "Address:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(452, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 14);
            this.label10.TabIndex = 9;
            this.label10.Text = "Employee Number:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(590, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 28);
            this.label9.TabIndex = 8;
            this.label9.Text = "Transaction Number:";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(681, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 28);
            this.label11.TabIndex = 15;
            this.label11.Text = "Transaction Amt/Cost:";
            // 
            // labelTranCost
            // 
            this.labelTranCost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTranCost.AutoSize = true;
            this.labelTranCost.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTranCost.Location = new System.Drawing.Point(681, 67);
            this.labelTranCost.Name = "labelTranCost";
            this.labelTranCost.Size = new System.Drawing.Size(98, 14);
            this.labelTranCost.TabIndex = 16;
            this.labelTranCost.Text = "1234";
            // 
            // labelEmpNo
            // 
            this.labelEmpNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEmpNo.AutoSize = true;
            this.labelEmpNo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmpNo.Location = new System.Drawing.Point(452, 67);
            this.labelEmpNo.Name = "labelEmpNo";
            this.labelEmpNo.Size = new System.Drawing.Size(131, 14);
            this.labelEmpNo.TabIndex = 11;
            this.labelEmpNo.Text = "1234";
            // 
            // labelCustAddress
            // 
            this.labelCustAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCustAddress.AutoSize = true;
            this.labelCustAddress.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCustAddress.Location = new System.Drawing.Point(235, 60);
            this.labelCustAddress.Name = "labelCustAddress";
            this.labelCustAddress.Size = new System.Drawing.Size(210, 28);
            this.labelCustAddress.TabIndex = 10;
            this.labelCustAddress.Text = "1234 no road, Fort Worth TX 76134";
            // 
            // label1CustDOB
            // 
            this.label1CustDOB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1CustDOB.AutoSize = true;
            this.label1CustDOB.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1CustDOB.Location = new System.Drawing.Point(139, 67);
            this.label1CustDOB.Name = "label1CustDOB";
            this.label1CustDOB.Size = new System.Drawing.Size(89, 14);
            this.label1CustDOB.TabIndex = 13;
            this.label1CustDOB.Text = "11/30/1966";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.itemDescription);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Location = new System.Drawing.Point(11, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(783, 86);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // itemDescription
            // 
            this.itemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.itemDescription.AutoSize = true;
            this.itemDescription.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemDescription.Location = new System.Drawing.Point(15, 19);
            this.itemDescription.Name = "itemDescription";
            this.itemDescription.Size = new System.Drawing.Size(0, 14);
            this.itemDescription.TabIndex = 14;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(238, 14);
            this.label16.TabIndex = 13;
            this.label16.Text = "Description of Item/Serial #, etc";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.48161F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.51839F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.labelOfficerBadge, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelOfficerPhone, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelOfficerName, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelHoldExpire, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelCaseNo, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelAgency, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label24, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label25, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label26, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label27, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label28, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelReqType, 6, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 316);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(783, 100);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // labelOfficerBadge
            // 
            this.labelOfficerBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOfficerBadge.AutoSize = true;
            this.labelOfficerBadge.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOfficerBadge.Location = new System.Drawing.Point(400, 67);
            this.labelOfficerBadge.Name = "labelOfficerBadge";
            this.labelOfficerBadge.Size = new System.Drawing.Size(88, 14);
            this.labelOfficerBadge.TabIndex = 19;
            this.labelOfficerBadge.Text = "xyz";
            // 
            // labelOfficerPhone
            // 
            this.labelOfficerPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOfficerPhone.AutoSize = true;
            this.labelOfficerPhone.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOfficerPhone.Location = new System.Drawing.Point(284, 67);
            this.labelOfficerPhone.Name = "labelOfficerPhone";
            this.labelOfficerPhone.Size = new System.Drawing.Size(109, 14);
            this.labelOfficerPhone.TabIndex = 14;
            this.labelOfficerPhone.Text = "1234";
            // 
            // labelOfficerName
            // 
            this.labelOfficerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOfficerName.AutoSize = true;
            this.labelOfficerName.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOfficerName.Location = new System.Drawing.Point(129, 67);
            this.labelOfficerName.Name = "labelOfficerName";
            this.labelOfficerName.Size = new System.Drawing.Size(148, 14);
            this.labelOfficerName.TabIndex = 13;
            this.labelOfficerName.Text = "John Q";
            // 
            // labelHoldExpire
            // 
            this.labelHoldExpire.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHoldExpire.AutoSize = true;
            this.labelHoldExpire.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHoldExpire.Location = new System.Drawing.Point(4, 67);
            this.labelHoldExpire.Name = "labelHoldExpire";
            this.labelHoldExpire.Size = new System.Drawing.Size(118, 14);
            this.labelHoldExpire.TabIndex = 12;
            this.labelHoldExpire.Text = "345325435";
            // 
            // labelCaseNo
            // 
            this.labelCaseNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCaseNo.AutoSize = true;
            this.labelCaseNo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCaseNo.Location = new System.Drawing.Point(601, 67);
            this.labelCaseNo.Name = "labelCaseNo";
            this.labelCaseNo.Size = new System.Drawing.Size(93, 14);
            this.labelCaseNo.TabIndex = 11;
            this.labelCaseNo.Text = "xyz";
            // 
            // labelAgency
            // 
            this.labelAgency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAgency.AutoSize = true;
            this.labelAgency.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAgency.Location = new System.Drawing.Point(495, 67);
            this.labelAgency.Name = "labelAgency";
            this.labelAgency.Size = new System.Drawing.Size(99, 14);
            this.labelAgency.TabIndex = 10;
            this.labelAgency.Text = "Yes";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(129, 18);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(148, 14);
            this.label24.TabIndex = 6;
            this.label24.Text = "Officer\'s Name";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(284, 11);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(109, 28);
            this.label25.TabIndex = 7;
            this.label25.Text = "Officer\'s Phone Number:";
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(400, 18);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(88, 14);
            this.label26.TabIndex = 8;
            this.label26.Text = "Badge No:";
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(495, 18);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(99, 14);
            this.label27.TabIndex = 9;
            this.label27.Text = "Agency:";
            // 
            // label28
            // 
            this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(601, 18);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(93, 14);
            this.label28.TabIndex = 15;
            this.label28.Text = "Case Number:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "Hold Expires:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(701, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 28);
            this.label3.TabIndex = 17;
            this.label3.Text = "Request Type:";
            // 
            // labelReqType
            // 
            this.labelReqType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelReqType.AutoSize = true;
            this.labelReqType.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReqType.Location = new System.Drawing.Point(701, 67);
            this.labelReqType.Name = "labelReqType";
            this.labelReqType.Size = new System.Drawing.Size(78, 14);
            this.labelReqType.TabIndex = 18;
            this.labelReqType.Text = "xyz";
            // 
            // labelHoldComment
            // 
            this.labelHoldComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHoldComment.AutoSize = true;
            this.labelHoldComment.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHoldComment.Location = new System.Drawing.Point(12, 452);
            this.labelHoldComment.Name = "labelHoldComment";
            this.labelHoldComment.Size = new System.Drawing.Size(28, 14);
            this.labelHoldComment.TabIndex = 16;
            this.labelHoldComment.Text = "xyz";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(9, 428);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(119, 14);
            this.label23.TabIndex = 8;
            this.label23.Text = "Reason for Hold:";
            // 
            // PoliceHoldform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(795, 524);
            this.ControlBox = false;
            this.Controls.Add(this.labelHoldComment);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelTranDate);
            this.Controls.Add(this.labelStoreAddr2);
            this.Controls.Add(this.labelStoreAddr1);
            this.Controls.Add(this.labelStoreName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "PoliceHoldform";
            this.Text = "Holdform";
            this.Load += new System.EventHandler(this.PoliceHoldform_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelStoreName;
        private System.Windows.Forms.Label labelStoreAddr1;
        private System.Windows.Forms.Label labelStoreAddr2;
        private System.Windows.Forms.Label labelTranDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1CustName;
        private System.Windows.Forms.Label labelEmpNo;
        private System.Windows.Forms.Label labelCustAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelTranNo;
        private System.Windows.Forms.Label label1CustDOB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label itemDescription;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelOfficerPhone;
        private System.Windows.Forms.Label labelOfficerName;
        private System.Windows.Forms.Label labelHoldExpire;
        private System.Windows.Forms.Label labelCaseNo;
        private System.Windows.Forms.Label labelAgency;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label labelHoldComment;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelOfficerBadge;
        private System.Windows.Forms.Label labelReqType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelTranCost;
    }
}