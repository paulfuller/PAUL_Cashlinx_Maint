using Common.Libraries.Forms.Components;

namespace Audit.Forms.Inventory
{
    partial class AuditManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditManager));
            this.tableLayoutPanelDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblUploadFromTrakker = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDownloadToTrakker = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAuditCompleteDate = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblAuditStartDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAuditor = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAuditScope = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanelPreAudit = new System.Windows.Forms.TableLayoutPanel();
            this.lblDvdDiscsCost = new System.Windows.Forms.Label();
            this.lblDvdDiscsQty = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.lblPremiumVideoGamesCost = new System.Windows.Forms.Label();
            this.lblPremiumVideoGamesQty = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.lblStandardVideoGamesCost = new System.Windows.Forms.Label();
            this.lblStandardVideoGamesQty = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.lblVideoTapesCost = new System.Windows.Forms.Label();
            this.lblVideoTapesQty = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblCompactDiscsCost = new System.Windows.Forms.Label();
            this.lblCompactDiscsQty = new System.Windows.Forms.Label();
            this.lblExpectedCaccCost = new System.Windows.Forms.Label();
            this.lblExpectedCaccQty = new System.Windows.Forms.Label();
            this.lblExpectedGeneralMerchandiseCost = new System.Windows.Forms.Label();
            this.lblExpectedGeneralMerchandiseQty = new System.Windows.Forms.Label();
            this.lblExpectedJewelryCost = new System.Windows.Forms.Label();
            this.lblExpectedJewelryQty = new System.Windows.Forms.Label();
            this.lblExpectedItemsCost = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblExpectedItemsQty = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tableLayoutPanelResults = new System.Windows.Forms.TableLayoutPanel();
            this.label27 = new System.Windows.Forms.Label();
            this.lblCaccItemsMissingQty = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblItemsMissingQty = new System.Windows.Forms.Label();
            this.ItemsUnexpectedQty = new System.Windows.Forms.Label();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnCountCacc = new CustomButton();
            this.btnDownloadToTrakker = new CustomButton();
            this.btnUploadFromTrakker = new CustomButton();
            this.btnProcessMissing = new CustomButton();
            this.btnProcessUnexpected = new CustomButton();
            this.tableLayoutPanelContainer = new System.Windows.Forms.TableLayoutPanel();
            this.label18 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.btnCancel = new CustomButton();
            this.btnPostAudit = new CustomButton();
            this.btnPreAuditReport = new CustomButton();
            this.btnTrakkerUploadReport = new CustomButton();
            this.tableLayoutPanelDetails.SuspendLayout();
            this.tableLayoutPanelPreAudit.SuspendLayout();
            this.tableLayoutPanelResults.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.tableLayoutPanelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelDetails
            // 
            this.tableLayoutPanelDetails.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelDetails.ColumnCount = 4;
            this.tableLayoutPanelContainer.SetColumnSpan(this.tableLayoutPanelDetails, 2);
            this.tableLayoutPanelDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelDetails.Controls.Add(this.lblUploadFromTrakker, 3, 2);
            this.tableLayoutPanelDetails.Controls.Add(this.label11, 2, 2);
            this.tableLayoutPanelDetails.Controls.Add(this.lblDownloadToTrakker, 1, 2);
            this.tableLayoutPanelDetails.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanelDetails.Controls.Add(this.lblAuditCompleteDate, 3, 1);
            this.tableLayoutPanelDetails.Controls.Add(this.label7, 2, 1);
            this.tableLayoutPanelDetails.Controls.Add(this.lblAuditStartDate, 1, 1);
            this.tableLayoutPanelDetails.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanelDetails.Controls.Add(this.lblAuditor, 3, 0);
            this.tableLayoutPanelDetails.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanelDetails.Controls.Add(this.lblAuditScope, 1, 0);
            this.tableLayoutPanelDetails.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDetails.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelDetails.Name = "tableLayoutPanelDetails";
            this.tableLayoutPanelDetails.RowCount = 3;
            this.tableLayoutPanelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanelDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanelDetails.Size = new System.Drawing.Size(770, 93);
            this.tableLayoutPanelDetails.TabIndex = 0;
            // 
            // lblUploadFromTrakker
            // 
            this.lblUploadFromTrakker.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUploadFromTrakker.AutoSize = true;
            this.lblUploadFromTrakker.Location = new System.Drawing.Point(579, 70);
            this.lblUploadFromTrakker.Name = "lblUploadFromTrakker";
            this.lblUploadFromTrakker.Size = new System.Drawing.Size(41, 13);
            this.lblUploadFromTrakker.TabIndex = 11;
            this.lblUploadFromTrakker.Text = "label12";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(446, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(127, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Upload from Trakker:";
            // 
            // lblDownloadToTrakker
            // 
            this.lblDownloadToTrakker.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDownloadToTrakker.AutoSize = true;
            this.lblDownloadToTrakker.Location = new System.Drawing.Point(195, 70);
            this.lblDownloadToTrakker.Name = "lblDownloadToTrakker";
            this.lblDownloadToTrakker.Size = new System.Drawing.Size(41, 13);
            this.lblDownloadToTrakker.TabIndex = 9;
            this.lblDownloadToTrakker.Text = "label10";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(61, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Download to Trakker:";
            // 
            // lblAuditCompleteDate
            // 
            this.lblAuditCompleteDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuditCompleteDate.AutoSize = true;
            this.lblAuditCompleteDate.Location = new System.Drawing.Point(579, 38);
            this.lblAuditCompleteDate.Name = "lblAuditCompleteDate";
            this.lblAuditCompleteDate.Size = new System.Drawing.Size(35, 13);
            this.lblAuditCompleteDate.TabIndex = 7;
            this.lblAuditCompleteDate.Text = "label8";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(446, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Audit Complete Date:";
            // 
            // lblAuditStartDate
            // 
            this.lblAuditStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuditStartDate.AutoSize = true;
            this.lblAuditStartDate.Location = new System.Drawing.Point(195, 38);
            this.lblAuditStartDate.Name = "lblAuditStartDate";
            this.lblAuditStartDate.Size = new System.Drawing.Size(35, 13);
            this.lblAuditStartDate.TabIndex = 5;
            this.lblAuditStartDate.Text = "label6";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(87, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Audit Start Date:";
            // 
            // lblAuditor
            // 
            this.lblAuditor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuditor.AutoSize = true;
            this.lblAuditor.Location = new System.Drawing.Point(579, 8);
            this.lblAuditor.Name = "lblAuditor";
            this.lblAuditor.Size = new System.Drawing.Size(35, 13);
            this.lblAuditor.TabIndex = 3;
            this.lblAuditor.Text = "label4";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(521, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Auditor:";
            // 
            // lblAuditScope
            // 
            this.lblAuditScope.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuditScope.AutoSize = true;
            this.lblAuditScope.Location = new System.Drawing.Point(195, 8);
            this.lblAuditScope.Name = "lblAuditScope";
            this.lblAuditScope.Size = new System.Drawing.Size(35, 13);
            this.lblAuditScope.TabIndex = 1;
            this.lblAuditScope.Text = "label2";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(112, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Audit Scope:";
            // 
            // tableLayoutPanelPreAudit
            // 
            this.tableLayoutPanelPreAudit.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelPreAudit.ColumnCount = 3;
            this.tableLayoutPanelPreAudit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPreAudit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelPreAudit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblDvdDiscsCost, 2, 10);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblDvdDiscsQty, 1, 10);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label50, 0, 10);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblPremiumVideoGamesCost, 2, 9);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblPremiumVideoGamesQty, 1, 9);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label47, 0, 9);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblStandardVideoGamesCost, 2, 8);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblStandardVideoGamesQty, 1, 8);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label44, 0, 8);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblVideoTapesCost, 2, 7);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblVideoTapesQty, 1, 7);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label41, 0, 7);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblCompactDiscsCost, 2, 6);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblCompactDiscsQty, 1, 6);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblExpectedCaccCost, 2, 5);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblExpectedCaccQty, 1, 5);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblExpectedGeneralMerchandiseCost, 2, 3);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblExpectedGeneralMerchandiseQty, 1, 3);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblExpectedJewelryCost, 2, 2);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblExpectedJewelryQty, 1, 2);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblExpectedItemsCost, 2, 1);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label13, 1, 0);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label14, 2, 0);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label17, 0, 1);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label15, 0, 2);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label16, 0, 3);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label19, 0, 6);
            this.tableLayoutPanelPreAudit.Controls.Add(this.lblExpectedItemsQty, 1, 1);
            this.tableLayoutPanelPreAudit.Controls.Add(this.label20, 0, 5);
            this.tableLayoutPanelPreAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelPreAudit.Location = new System.Drawing.Point(3, 122);
            this.tableLayoutPanelPreAudit.Name = "tableLayoutPanelPreAudit";
            this.tableLayoutPanelPreAudit.RowCount = 11;
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelPreAudit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelPreAudit.Size = new System.Drawing.Size(632, 349);
            this.tableLayoutPanelPreAudit.TabIndex = 1;
            // 
            // lblDvdDiscsCost
            // 
            this.lblDvdDiscsCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDvdDiscsCost.AutoSize = true;
            this.lblDvdDiscsCost.Location = new System.Drawing.Point(511, 323);
            this.lblDvdDiscsCost.Name = "lblDvdDiscsCost";
            this.lblDvdDiscsCost.Size = new System.Drawing.Size(29, 13);
            this.lblDvdDiscsCost.TabIndex = 33;
            this.lblDvdDiscsCost.Text = "0.00";
            // 
            // lblDvdDiscsQty
            // 
            this.lblDvdDiscsQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDvdDiscsQty.AutoSize = true;
            this.lblDvdDiscsQty.Location = new System.Drawing.Point(308, 323);
            this.lblDvdDiscsQty.Name = "lblDvdDiscsQty";
            this.lblDvdDiscsQty.Size = new System.Drawing.Size(13, 13);
            this.lblDvdDiscsQty.TabIndex = 32;
            this.lblDvdDiscsQty.Text = "0";
            // 
            // label50
            // 
            this.label50.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(141, 323);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(66, 13);
            this.label50.TabIndex = 31;
            this.label50.Text = "DVD Disc(s):";
            // 
            // lblPremiumVideoGamesCost
            // 
            this.lblPremiumVideoGamesCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPremiumVideoGamesCost.AutoSize = true;
            this.lblPremiumVideoGamesCost.Location = new System.Drawing.Point(511, 288);
            this.lblPremiumVideoGamesCost.Name = "lblPremiumVideoGamesCost";
            this.lblPremiumVideoGamesCost.Size = new System.Drawing.Size(29, 13);
            this.lblPremiumVideoGamesCost.TabIndex = 30;
            this.lblPremiumVideoGamesCost.Text = "0.00";
            // 
            // lblPremiumVideoGamesQty
            // 
            this.lblPremiumVideoGamesQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPremiumVideoGamesQty.AutoSize = true;
            this.lblPremiumVideoGamesQty.Location = new System.Drawing.Point(308, 288);
            this.lblPremiumVideoGamesQty.Name = "lblPremiumVideoGamesQty";
            this.lblPremiumVideoGamesQty.Size = new System.Drawing.Size(13, 13);
            this.lblPremiumVideoGamesQty.TabIndex = 29;
            this.lblPremiumVideoGamesQty.Text = "0";
            // 
            // label47
            // 
            this.label47.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(36, 288);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(171, 13);
            this.label47.TabIndex = 28;
            this.label47.Text = "Premium Video Game Cartridge(s):";
            // 
            // lblStandardVideoGamesCost
            // 
            this.lblStandardVideoGamesCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStandardVideoGamesCost.AutoSize = true;
            this.lblStandardVideoGamesCost.Location = new System.Drawing.Point(511, 257);
            this.lblStandardVideoGamesCost.Name = "lblStandardVideoGamesCost";
            this.lblStandardVideoGamesCost.Size = new System.Drawing.Size(29, 13);
            this.lblStandardVideoGamesCost.TabIndex = 27;
            this.lblStandardVideoGamesCost.Text = "0.00";
            // 
            // lblStandardVideoGamesQty
            // 
            this.lblStandardVideoGamesQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStandardVideoGamesQty.AutoSize = true;
            this.lblStandardVideoGamesQty.Location = new System.Drawing.Point(308, 257);
            this.lblStandardVideoGamesQty.Name = "lblStandardVideoGamesQty";
            this.lblStandardVideoGamesQty.Size = new System.Drawing.Size(13, 13);
            this.lblStandardVideoGamesQty.TabIndex = 26;
            this.lblStandardVideoGamesQty.Text = "0";
            // 
            // label44
            // 
            this.label44.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(32, 257);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(175, 13);
            this.label44.TabIndex = 25;
            this.label44.Text = "Standard Video Game Cartridge(s):";
            // 
            // lblVideoTapesCost
            // 
            this.lblVideoTapesCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVideoTapesCost.AutoSize = true;
            this.lblVideoTapesCost.Location = new System.Drawing.Point(511, 226);
            this.lblVideoTapesCost.Name = "lblVideoTapesCost";
            this.lblVideoTapesCost.Size = new System.Drawing.Size(29, 13);
            this.lblVideoTapesCost.TabIndex = 24;
            this.lblVideoTapesCost.Text = "0.00";
            // 
            // lblVideoTapesQty
            // 
            this.lblVideoTapesQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVideoTapesQty.AutoSize = true;
            this.lblVideoTapesQty.Location = new System.Drawing.Point(308, 226);
            this.lblVideoTapesQty.Name = "lblVideoTapesQty";
            this.lblVideoTapesQty.Size = new System.Drawing.Size(13, 13);
            this.lblVideoTapesQty.TabIndex = 23;
            this.lblVideoTapesQty.Text = "0";
            // 
            // label41
            // 
            this.label41.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(130, 226);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(77, 13);
            this.label41.TabIndex = 22;
            this.label41.Text = "Video Tape(s):";
            // 
            // lblCompactDiscsCost
            // 
            this.lblCompactDiscsCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCompactDiscsCost.AutoSize = true;
            this.lblCompactDiscsCost.Location = new System.Drawing.Point(511, 195);
            this.lblCompactDiscsCost.Name = "lblCompactDiscsCost";
            this.lblCompactDiscsCost.Size = new System.Drawing.Size(29, 13);
            this.lblCompactDiscsCost.TabIndex = 21;
            this.lblCompactDiscsCost.Text = "0.00";
            // 
            // lblCompactDiscsQty
            // 
            this.lblCompactDiscsQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCompactDiscsQty.AutoSize = true;
            this.lblCompactDiscsQty.Location = new System.Drawing.Point(308, 195);
            this.lblCompactDiscsQty.Name = "lblCompactDiscsQty";
            this.lblCompactDiscsQty.Size = new System.Drawing.Size(13, 13);
            this.lblCompactDiscsQty.TabIndex = 20;
            this.lblCompactDiscsQty.Text = "0";
            // 
            // lblExpectedCaccCost
            // 
            this.lblExpectedCaccCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblExpectedCaccCost.AutoSize = true;
            this.lblExpectedCaccCost.Location = new System.Drawing.Point(511, 164);
            this.lblExpectedCaccCost.Name = "lblExpectedCaccCost";
            this.lblExpectedCaccCost.Size = new System.Drawing.Size(29, 13);
            this.lblExpectedCaccCost.TabIndex = 19;
            this.lblExpectedCaccCost.Text = "0.00";
            // 
            // lblExpectedCaccQty
            // 
            this.lblExpectedCaccQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblExpectedCaccQty.AutoSize = true;
            this.lblExpectedCaccQty.Location = new System.Drawing.Point(308, 164);
            this.lblExpectedCaccQty.Name = "lblExpectedCaccQty";
            this.lblExpectedCaccQty.Size = new System.Drawing.Size(13, 13);
            this.lblExpectedCaccQty.TabIndex = 18;
            this.lblExpectedCaccQty.Text = "0";
            // 
            // lblExpectedGeneralMerchandiseCost
            // 
            this.lblExpectedGeneralMerchandiseCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblExpectedGeneralMerchandiseCost.AutoSize = true;
            this.lblExpectedGeneralMerchandiseCost.Location = new System.Drawing.Point(511, 102);
            this.lblExpectedGeneralMerchandiseCost.Name = "lblExpectedGeneralMerchandiseCost";
            this.lblExpectedGeneralMerchandiseCost.Size = new System.Drawing.Size(29, 13);
            this.lblExpectedGeneralMerchandiseCost.TabIndex = 15;
            this.lblExpectedGeneralMerchandiseCost.Text = "0.00";
            // 
            // lblExpectedGeneralMerchandiseQty
            // 
            this.lblExpectedGeneralMerchandiseQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblExpectedGeneralMerchandiseQty.AutoSize = true;
            this.lblExpectedGeneralMerchandiseQty.Location = new System.Drawing.Point(308, 102);
            this.lblExpectedGeneralMerchandiseQty.Name = "lblExpectedGeneralMerchandiseQty";
            this.lblExpectedGeneralMerchandiseQty.Size = new System.Drawing.Size(13, 13);
            this.lblExpectedGeneralMerchandiseQty.TabIndex = 14;
            this.lblExpectedGeneralMerchandiseQty.Text = "0";
            // 
            // lblExpectedJewelryCost
            // 
            this.lblExpectedJewelryCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblExpectedJewelryCost.AutoSize = true;
            this.lblExpectedJewelryCost.Location = new System.Drawing.Point(511, 71);
            this.lblExpectedJewelryCost.Name = "lblExpectedJewelryCost";
            this.lblExpectedJewelryCost.Size = new System.Drawing.Size(29, 13);
            this.lblExpectedJewelryCost.TabIndex = 13;
            this.lblExpectedJewelryCost.Text = "0.00";
            // 
            // lblExpectedJewelryQty
            // 
            this.lblExpectedJewelryQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblExpectedJewelryQty.AutoSize = true;
            this.lblExpectedJewelryQty.Location = new System.Drawing.Point(308, 71);
            this.lblExpectedJewelryQty.Name = "lblExpectedJewelryQty";
            this.lblExpectedJewelryQty.Size = new System.Drawing.Size(13, 13);
            this.lblExpectedJewelryQty.TabIndex = 12;
            this.lblExpectedJewelryQty.Text = "0";
            // 
            // lblExpectedItemsCost
            // 
            this.lblExpectedItemsCost.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblExpectedItemsCost.AutoSize = true;
            this.lblExpectedItemsCost.Location = new System.Drawing.Point(511, 40);
            this.lblExpectedItemsCost.Name = "lblExpectedItemsCost";
            this.lblExpectedItemsCost.Size = new System.Drawing.Size(29, 13);
            this.lblExpectedItemsCost.TabIndex = 11;
            this.lblExpectedItemsCost.Text = "0.00";
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(301, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Qty";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(510, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Cost";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(108, 40);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(99, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "Expected Items:";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(98, 71);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(109, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Expected Jewelry:";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(26, 102);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(181, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "Expected General Merchandise";
            // 
            // label19
            // 
            this.label19.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(119, 195);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(88, 13);
            this.label19.TabIndex = 7;
            this.label19.Text = "Compact Disc(s):";
            // 
            // lblExpectedItemsQty
            // 
            this.lblExpectedItemsQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblExpectedItemsQty.AutoSize = true;
            this.lblExpectedItemsQty.Location = new System.Drawing.Point(308, 40);
            this.lblExpectedItemsQty.Name = "lblExpectedItemsQty";
            this.lblExpectedItemsQty.Size = new System.Drawing.Size(13, 13);
            this.lblExpectedItemsQty.TabIndex = 9;
            this.lblExpectedItemsQty.Text = "0";
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(113, 164);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(94, 13);
            this.label20.TabIndex = 8;
            this.label20.Text = "Expected CACC:";
            // 
            // tableLayoutPanelResults
            // 
            this.tableLayoutPanelResults.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelResults.ColumnCount = 2;
            this.tableLayoutPanelResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelResults.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelResults.Controls.Add(this.label27, 0, 3);
            this.tableLayoutPanelResults.Controls.Add(this.lblCaccItemsMissingQty, 0, 3);
            this.tableLayoutPanelResults.Controls.Add(this.label22, 0, 1);
            this.tableLayoutPanelResults.Controls.Add(this.label23, 0, 2);
            this.tableLayoutPanelResults.Controls.Add(this.label21, 1, 0);
            this.tableLayoutPanelResults.Controls.Add(this.lblItemsMissingQty, 1, 1);
            this.tableLayoutPanelResults.Controls.Add(this.ItemsUnexpectedQty, 1, 2);
            this.tableLayoutPanelResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelResults.Location = new System.Drawing.Point(3, 497);
            this.tableLayoutPanelResults.Name = "tableLayoutPanelResults";
            this.tableLayoutPanelResults.RowCount = 4;
            this.tableLayoutPanelResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelResults.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelResults.Size = new System.Drawing.Size(632, 124);
            this.tableLayoutPanelResults.TabIndex = 2;
            // 
            // label27
            // 
            this.label27.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(192, 102);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(121, 13);
            this.label27.TabIndex = 7;
            this.label27.Text = "CACC Items Missing:";
            this.label27.Visible = false;
            // 
            // lblCaccItemsMissingQty
            // 
            this.lblCaccItemsMissingQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCaccItemsMissingQty.AutoSize = true;
            this.lblCaccItemsMissingQty.Location = new System.Drawing.Point(467, 102);
            this.lblCaccItemsMissingQty.Name = "lblCaccItemsMissingQty";
            this.lblCaccItemsMissingQty.Size = new System.Drawing.Size(13, 13);
            this.lblCaccItemsMissingQty.TabIndex = 6;
            this.lblCaccItemsMissingQty.Text = "0";
            this.lblCaccItemsMissingQty.Visible = false;
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(206, 40);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(107, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "Items Remaining:";
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(198, 71);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(115, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Items Unexpected:";
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(460, 9);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(27, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Qty";
            // 
            // lblItemsMissingQty
            // 
            this.lblItemsMissingQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblItemsMissingQty.AutoSize = true;
            this.lblItemsMissingQty.Location = new System.Drawing.Point(467, 40);
            this.lblItemsMissingQty.Name = "lblItemsMissingQty";
            this.lblItemsMissingQty.Size = new System.Drawing.Size(13, 13);
            this.lblItemsMissingQty.TabIndex = 4;
            this.lblItemsMissingQty.Text = "0";
            // 
            // ItemsUnexpectedQty
            // 
            this.ItemsUnexpectedQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ItemsUnexpectedQty.AutoSize = true;
            this.ItemsUnexpectedQty.Location = new System.Drawing.Point(467, 71);
            this.ItemsUnexpectedQty.Name = "ItemsUnexpectedQty";
            this.ItemsUnexpectedQty.Size = new System.Drawing.Size(13, 13);
            this.ItemsUnexpectedQty.TabIndex = 5;
            this.ItemsUnexpectedQty.Text = "0";
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelButtons.ColumnCount = 1;
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelButtons.Controls.Add(this.btnCountCacc, 0, 4);
            this.tableLayoutPanelButtons.Controls.Add(this.btnDownloadToTrakker, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnUploadFromTrakker, 0, 1);
            this.tableLayoutPanelButtons.Controls.Add(this.btnProcessMissing, 0, 2);
            this.tableLayoutPanelButtons.Controls.Add(this.btnProcessUnexpected, 0, 3);
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(641, 102);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 5;
            this.tableLayoutPanelContainer.SetRowSpan(this.tableLayoutPanelButtons, 4);
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(132, 519);
            this.tableLayoutPanelButtons.TabIndex = 3;
            // 
            // btnCountCacc
            // 
            this.btnCountCacc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCountCacc.BackColor = System.Drawing.Color.Transparent;
            this.btnCountCacc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCountCacc.BackgroundImage")));
            this.btnCountCacc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCountCacc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCountCacc.Enabled = false;
            this.btnCountCacc.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCountCacc.FlatAppearance.BorderSize = 0;
            this.btnCountCacc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCountCacc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCountCacc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCountCacc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCountCacc.ForeColor = System.Drawing.Color.White;
            this.btnCountCacc.Location = new System.Drawing.Point(16, 440);
            this.btnCountCacc.Margin = new System.Windows.Forms.Padding(0);
            this.btnCountCacc.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnCountCacc.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnCountCacc.Name = "btnCountCacc";
            this.btnCountCacc.Size = new System.Drawing.Size(100, 50);
            this.btnCountCacc.TabIndex = 10;
            this.btnCountCacc.Text = "Count CACC";
            this.btnCountCacc.UseVisualStyleBackColor = false;
            this.btnCountCacc.Click += new System.EventHandler(this.btnCountCacc_Click);
            // 
            // btnDownloadToTrakker
            // 
            this.btnDownloadToTrakker.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDownloadToTrakker.BackColor = System.Drawing.Color.Transparent;
            this.btnDownloadToTrakker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDownloadToTrakker.BackgroundImage")));
            this.btnDownloadToTrakker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDownloadToTrakker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownloadToTrakker.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDownloadToTrakker.FlatAppearance.BorderSize = 0;
            this.btnDownloadToTrakker.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDownloadToTrakker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDownloadToTrakker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadToTrakker.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadToTrakker.ForeColor = System.Drawing.Color.White;
            this.btnDownloadToTrakker.Location = new System.Drawing.Point(16, 26);
            this.btnDownloadToTrakker.Margin = new System.Windows.Forms.Padding(0);
            this.btnDownloadToTrakker.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnDownloadToTrakker.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnDownloadToTrakker.Name = "btnDownloadToTrakker";
            this.btnDownloadToTrakker.Size = new System.Drawing.Size(100, 50);
            this.btnDownloadToTrakker.TabIndex = 6;
            this.btnDownloadToTrakker.Text = "Download to Trakker";
            this.btnDownloadToTrakker.UseVisualStyleBackColor = false;
            this.btnDownloadToTrakker.Click += new System.EventHandler(this.btnDownloadToTrakker_Click);
            // 
            // btnUploadFromTrakker
            // 
            this.btnUploadFromTrakker.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUploadFromTrakker.BackColor = System.Drawing.Color.Transparent;
            this.btnUploadFromTrakker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUploadFromTrakker.BackgroundImage")));
            this.btnUploadFromTrakker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnUploadFromTrakker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadFromTrakker.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUploadFromTrakker.FlatAppearance.BorderSize = 0;
            this.btnUploadFromTrakker.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnUploadFromTrakker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnUploadFromTrakker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadFromTrakker.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadFromTrakker.ForeColor = System.Drawing.Color.White;
            this.btnUploadFromTrakker.Location = new System.Drawing.Point(16, 129);
            this.btnUploadFromTrakker.Margin = new System.Windows.Forms.Padding(0);
            this.btnUploadFromTrakker.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnUploadFromTrakker.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnUploadFromTrakker.Name = "btnUploadFromTrakker";
            this.btnUploadFromTrakker.Size = new System.Drawing.Size(100, 50);
            this.btnUploadFromTrakker.TabIndex = 7;
            this.btnUploadFromTrakker.Text = "Upload from Trakker";
            this.btnUploadFromTrakker.UseVisualStyleBackColor = false;
            this.btnUploadFromTrakker.Click += new System.EventHandler(this.btnUploadFromTrakker_Click);
            // 
            // btnProcessMissing
            // 
            this.btnProcessMissing.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProcessMissing.BackColor = System.Drawing.Color.Transparent;
            this.btnProcessMissing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProcessMissing.BackgroundImage")));
            this.btnProcessMissing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnProcessMissing.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcessMissing.Enabled = false;
            this.btnProcessMissing.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProcessMissing.FlatAppearance.BorderSize = 0;
            this.btnProcessMissing.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnProcessMissing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnProcessMissing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessMissing.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessMissing.ForeColor = System.Drawing.Color.White;
            this.btnProcessMissing.Location = new System.Drawing.Point(16, 232);
            this.btnProcessMissing.Margin = new System.Windows.Forms.Padding(0);
            this.btnProcessMissing.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnProcessMissing.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnProcessMissing.Name = "btnProcessMissing";
            this.btnProcessMissing.Size = new System.Drawing.Size(100, 50);
            this.btnProcessMissing.TabIndex = 8;
            this.btnProcessMissing.Text = "Process Missing";
            this.btnProcessMissing.UseVisualStyleBackColor = false;
            this.btnProcessMissing.Click += new System.EventHandler(this.btnProcessMissing_Click);
            // 
            // btnProcessUnexpected
            // 
            this.btnProcessUnexpected.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProcessUnexpected.BackColor = System.Drawing.Color.Transparent;
            this.btnProcessUnexpected.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnProcessUnexpected.BackgroundImage")));
            this.btnProcessUnexpected.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnProcessUnexpected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcessUnexpected.Enabled = false;
            this.btnProcessUnexpected.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProcessUnexpected.FlatAppearance.BorderSize = 0;
            this.btnProcessUnexpected.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnProcessUnexpected.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnProcessUnexpected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessUnexpected.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessUnexpected.ForeColor = System.Drawing.Color.White;
            this.btnProcessUnexpected.Location = new System.Drawing.Point(16, 335);
            this.btnProcessUnexpected.Margin = new System.Windows.Forms.Padding(0);
            this.btnProcessUnexpected.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnProcessUnexpected.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnProcessUnexpected.Name = "btnProcessUnexpected";
            this.btnProcessUnexpected.Size = new System.Drawing.Size(100, 50);
            this.btnProcessUnexpected.TabIndex = 9;
            this.btnProcessUnexpected.Text = "Process Unexpected";
            this.btnProcessUnexpected.UseVisualStyleBackColor = false;
            this.btnProcessUnexpected.Click += new System.EventHandler(this.btnProcessUnexpected_Click);
            // 
            // tableLayoutPanelContainer
            // 
            this.tableLayoutPanelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelContainer.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelContainer.ColumnCount = 2;
            this.tableLayoutPanelContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 638F));
            this.tableLayoutPanelContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanelContainer.Controls.Add(this.tableLayoutPanelButtons, 1, 1);
            this.tableLayoutPanelContainer.Controls.Add(this.tableLayoutPanelResults, 0, 4);
            this.tableLayoutPanelContainer.Controls.Add(this.tableLayoutPanelPreAudit, 0, 2);
            this.tableLayoutPanelContainer.Controls.Add(this.tableLayoutPanelDetails, 0, 0);
            this.tableLayoutPanelContainer.Controls.Add(this.label18, 0, 1);
            this.tableLayoutPanelContainer.Controls.Add(this.label29, 0, 3);
            this.tableLayoutPanelContainer.Location = new System.Drawing.Point(12, 82);
            this.tableLayoutPanelContainer.Name = "tableLayoutPanelContainer";
            this.tableLayoutPanelContainer.RowCount = 5;
            this.tableLayoutPanelContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanelContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 355F));
            this.tableLayoutPanelContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelContainer.Size = new System.Drawing.Size(776, 624);
            this.tableLayoutPanelContainer.TabIndex = 4;
            this.tableLayoutPanelContainer.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tableLayoutPanelContainer_CellPaint);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(3, 99);
            this.label18.Name = "label18";
            this.label18.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.label18.Size = new System.Drawing.Size(110, 16);
            this.label18.TabIndex = 4;
            this.label18.Text = "Pre Audit";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(3, 474);
            this.label29.Name = "label29";
            this.label29.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.label29.Size = new System.Drawing.Size(132, 16);
            this.label29.TabIndex = 5;
            this.label29.Text = "Audit Results";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(9, 709);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPostAudit
            // 
            this.btnPostAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPostAudit.BackColor = System.Drawing.Color.Transparent;
            this.btnPostAudit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPostAudit.BackgroundImage")));
            this.btnPostAudit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPostAudit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPostAudit.Enabled = false;
            this.btnPostAudit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPostAudit.FlatAppearance.BorderSize = 0;
            this.btnPostAudit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPostAudit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPostAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPostAudit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPostAudit.ForeColor = System.Drawing.Color.White;
            this.btnPostAudit.Location = new System.Drawing.Point(691, 709);
            this.btnPostAudit.Margin = new System.Windows.Forms.Padding(0);
            this.btnPostAudit.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnPostAudit.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnPostAudit.Name = "btnPostAudit";
            this.btnPostAudit.Size = new System.Drawing.Size(100, 50);
            this.btnPostAudit.TabIndex = 12;
            this.btnPostAudit.Text = "&Post Audit";
            this.btnPostAudit.UseVisualStyleBackColor = false;
            this.btnPostAudit.Click += new System.EventHandler(this.btnPostAudit_Click);
            // 
            // btnPreAuditReport
            // 
            this.btnPreAuditReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreAuditReport.BackColor = System.Drawing.Color.Transparent;
            this.btnPreAuditReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPreAuditReport.BackgroundImage")));
            this.btnPreAuditReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPreAuditReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreAuditReport.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPreAuditReport.FlatAppearance.BorderSize = 0;
            this.btnPreAuditReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPreAuditReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPreAuditReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreAuditReport.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreAuditReport.ForeColor = System.Drawing.Color.White;
            this.btnPreAuditReport.Location = new System.Drawing.Point(109, 709);
            this.btnPreAuditReport.Margin = new System.Windows.Forms.Padding(0);
            this.btnPreAuditReport.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnPreAuditReport.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnPreAuditReport.Name = "btnPreAuditReport";
            this.btnPreAuditReport.Size = new System.Drawing.Size(100, 50);
            this.btnPreAuditReport.TabIndex = 13;
            this.btnPreAuditReport.Text = "Pre Audit Report";
            this.btnPreAuditReport.UseVisualStyleBackColor = false;
            this.btnPreAuditReport.Click += new System.EventHandler(this.btnPreAuditReport_Click);
            // 
            // btnTrakkerUploadReport
            // 
            this.btnTrakkerUploadReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTrakkerUploadReport.BackColor = System.Drawing.Color.Transparent;
            this.btnTrakkerUploadReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTrakkerUploadReport.BackgroundImage")));
            this.btnTrakkerUploadReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTrakkerUploadReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrakkerUploadReport.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTrakkerUploadReport.FlatAppearance.BorderSize = 0;
            this.btnTrakkerUploadReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTrakkerUploadReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTrakkerUploadReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrakkerUploadReport.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrakkerUploadReport.ForeColor = System.Drawing.Color.White;
            this.btnTrakkerUploadReport.Location = new System.Drawing.Point(210, 709);
            this.btnTrakkerUploadReport.Margin = new System.Windows.Forms.Padding(0);
            this.btnTrakkerUploadReport.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnTrakkerUploadReport.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnTrakkerUploadReport.Name = "btnTrakkerUploadReport";
            this.btnTrakkerUploadReport.Size = new System.Drawing.Size(100, 50);
            this.btnTrakkerUploadReport.TabIndex = 14;
            this.btnTrakkerUploadReport.Text = "Trakker Upload Report";
            this.btnTrakkerUploadReport.UseVisualStyleBackColor = false;
            this.btnTrakkerUploadReport.Click += new System.EventHandler(this.btnTrakkerUploadReport_Click);
            // 
            // AuditManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Audit.Properties.Resources.newDialog_600_BlueScale;
            this.ClientSize = new System.Drawing.Size(800, 768);
            this.Controls.Add(this.btnTrakkerUploadReport);
            this.Controls.Add(this.btnPreAuditReport);
            this.Controls.Add(this.btnPostAudit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tableLayoutPanelContainer);
            this.Name = "AuditManager";
            this.Text = "AuditManager";
            this.Activated += new System.EventHandler(this.AuditManager_Activated);
            this.Load += new System.EventHandler(this.AuditManager_Load);
            this.Shown += new System.EventHandler(this.AuditManager_Shown);
            this.tableLayoutPanelDetails.ResumeLayout(false);
            this.tableLayoutPanelDetails.PerformLayout();
            this.tableLayoutPanelPreAudit.ResumeLayout(false);
            this.tableLayoutPanelPreAudit.PerformLayout();
            this.tableLayoutPanelResults.ResumeLayout(false);
            this.tableLayoutPanelResults.PerformLayout();
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.tableLayoutPanelContainer.ResumeLayout(false);
            this.tableLayoutPanelContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPreAudit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelResults;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelContainer;
        private CustomButton btnCancel;
        private CustomButton btnDownloadToTrakker;
        private CustomButton btnUploadFromTrakker;
        private CustomButton btnProcessMissing;
        private CustomButton btnProcessUnexpected;
        private CustomButton btnCountCacc;
        private CustomButton btnPostAudit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAuditScope;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAuditor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAuditStartDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblAuditCompleteDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDownloadToTrakker;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblUploadFromTrakker;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label ItemsUnexpectedQty;
        private System.Windows.Forms.Label lblCaccItemsMissingQty;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblExpectedItemsQty;
        private System.Windows.Forms.Label lblExpectedItemsCost;
        private System.Windows.Forms.Label lblExpectedJewelryQty;
        private System.Windows.Forms.Label lblExpectedJewelryCost;
        private System.Windows.Forms.Label lblExpectedGeneralMerchandiseQty;
        private System.Windows.Forms.Label lblExpectedGeneralMerchandiseCost;
        private System.Windows.Forms.Label lblExpectedCaccQty;
        private System.Windows.Forms.Label lblExpectedCaccCost;
        private System.Windows.Forms.Label lblCompactDiscsQty;
        private System.Windows.Forms.Label lblCompactDiscsCost;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lblVideoTapesQty;
        private System.Windows.Forms.Label lblVideoTapesCost;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label lblStandardVideoGamesQty;
        private System.Windows.Forms.Label lblStandardVideoGamesCost;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label lblPremiumVideoGamesQty;
        private System.Windows.Forms.Label lblPremiumVideoGamesCost;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label lblDvdDiscsQty;
        private System.Windows.Forms.Label lblDvdDiscsCost;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblItemsMissingQty;
        private CustomButton btnPreAuditReport;
        private CustomButton btnTrakkerUploadReport;
    }
}
