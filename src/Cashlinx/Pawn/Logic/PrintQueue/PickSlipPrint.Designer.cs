using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Logic.PrintQueue
{
    partial class PickSlipPrint
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
            this.labelTransactionType = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelOriginalLoanNoHeading = new System.Windows.Forms.Label();
            this.labelEmpNoHeading = new System.Windows.Forms.Label();
            this.labelorigLoanNo = new System.Windows.Forms.Label();
            this.labelEmployeeNo = new System.Windows.Forms.Label();
            this.labelPreviousLoanNo = new System.Windows.Forms.Label();
            this.labelCurrentLoanNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelCurrLoanNoHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelPageNo = new System.Windows.Forms.Label();
            this.labelCustHeading = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelCustName = new System.Windows.Forms.Label();
            this.labelLoanAmt = new System.Windows.Forms.Label();
            this.labelLoanAmtHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanelItem = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanelItemData = new System.Windows.Forms.TableLayoutPanel();
            this.pageFooterMarkerLabel = new System.Windows.Forms.Label();
            this.labelCurrentPrincipal = new System.Windows.Forms.Label();
            this.labelCurrentPrincipalHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanelItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTransactionType
            // 
            this.labelTransactionType.AutoSize = true;
            this.labelTransactionType.Location = new System.Drawing.Point(357, 13);
            this.labelTransactionType.Name = "labelTransactionType";
            this.labelTransactionType.Size = new System.Drawing.Size(94, 13);
            this.labelTransactionType.TabIndex = 0;
            this.labelTransactionType.Text = "P. U. Picking Slips";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(12, 13);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(129, 13);
            this.labelDate.TabIndex = 2;
            this.labelDate.Text = "mm/dd/yyyy hh:mm:ss am";
            // 
            // labelOriginalLoanNoHeading
            // 
            this.labelOriginalLoanNoHeading.AutoSize = true;
            this.labelOriginalLoanNoHeading.Location = new System.Drawing.Point(626, 13);
            this.labelOriginalLoanNoHeading.Name = "labelOriginalLoanNoHeading";
            this.labelOriginalLoanNoHeading.Size = new System.Drawing.Size(82, 13);
            this.labelOriginalLoanNoHeading.TabIndex = 3;
            this.labelOriginalLoanNoHeading.Text = "Original Loan #:";
            // 
            // labelEmpNoHeading
            // 
            this.labelEmpNoHeading.AutoSize = true;
            this.labelEmpNoHeading.Location = new System.Drawing.Point(12, 53);
            this.labelEmpNoHeading.Name = "labelEmpNoHeading";
            this.labelEmpNoHeading.Size = new System.Drawing.Size(66, 13);
            this.labelEmpNoHeading.TabIndex = 4;
            this.labelEmpNoHeading.Text = "Employee #:";
            // 
            // labelorigLoanNo
            // 
            this.labelorigLoanNo.AutoSize = true;
            this.labelorigLoanNo.Location = new System.Drawing.Point(714, 13);
            this.labelorigLoanNo.Name = "labelorigLoanNo";
            this.labelorigLoanNo.Size = new System.Drawing.Size(31, 13);
            this.labelorigLoanNo.TabIndex = 5;
            this.labelorigLoanNo.Text = "9999";
            // 
            // labelEmployeeNo
            // 
            this.labelEmployeeNo.AutoSize = true;
            this.labelEmployeeNo.Location = new System.Drawing.Point(99, 53);
            this.labelEmployeeNo.Name = "labelEmployeeNo";
            this.labelEmployeeNo.Size = new System.Drawing.Size(31, 13);
            this.labelEmployeeNo.TabIndex = 6;
            this.labelEmployeeNo.Text = "9999";
            // 
            // labelPreviousLoanNo
            // 
            this.labelPreviousLoanNo.AutoSize = true;
            this.labelPreviousLoanNo.Location = new System.Drawing.Point(423, 53);
            this.labelPreviousLoanNo.Name = "labelPreviousLoanNo";
            this.labelPreviousLoanNo.Size = new System.Drawing.Size(31, 13);
            this.labelPreviousLoanNo.TabIndex = 10;
            this.labelPreviousLoanNo.Text = "9999";
            // 
            // labelCurrentLoanNo
            // 
            this.labelCurrentLoanNo.AutoSize = true;
            this.labelCurrentLoanNo.Location = new System.Drawing.Point(715, 53);
            this.labelCurrentLoanNo.Name = "labelCurrentLoanNo";
            this.labelCurrentLoanNo.Size = new System.Drawing.Size(31, 13);
            this.labelCurrentLoanNo.TabIndex = 9;
            this.labelCurrentLoanNo.Text = "9999";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(329, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Previous Loan #:";
            // 
            // labelCurrLoanNoHeading
            // 
            this.labelCurrLoanNoHeading.AutoSize = true;
            this.labelCurrLoanNoHeading.Location = new System.Drawing.Point(627, 53);
            this.labelCurrLoanNoHeading.Name = "labelCurrLoanNoHeading";
            this.labelCurrLoanNoHeading.Size = new System.Drawing.Size(81, 13);
            this.labelCurrLoanNoHeading.TabIndex = 7;
            this.labelCurrLoanNoHeading.Text = "Current Loan #:";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(15, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 2);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // labelPageNo
            // 
            this.labelPageNo.AutoSize = true;
            this.labelPageNo.Location = new System.Drawing.Point(778, 53);
            this.labelPageNo.Name = "labelPageNo";
            this.labelPageNo.Size = new System.Drawing.Size(41, 13);
            this.labelPageNo.TabIndex = 12;
            this.labelPageNo.Text = "Page 1";
            // 
            // labelCustHeading
            // 
            this.labelCustHeading.AutoSize = true;
            this.labelCustHeading.Location = new System.Drawing.Point(11, 119);
            this.labelCustHeading.Name = "labelCustHeading";
            this.labelCustHeading.Size = new System.Drawing.Size(54, 13);
            this.labelCustHeading.TabIndex = 13;
            this.labelCustHeading.Text = "Customer:";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(19, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(800, 2);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // labelCustName
            // 
            this.labelCustName.AutoSize = true;
            this.labelCustName.Location = new System.Drawing.Point(71, 119);
            this.labelCustName.Name = "labelCustName";
            this.labelCustName.Size = new System.Drawing.Size(101, 13);
            this.labelCustName.TabIndex = 15;
            this.labelCustName.Text = "Lastname,Firstname";
            // 
            // labelLoanAmt
            // 
            this.labelLoanAmt.AutoSize = true;
            this.labelLoanAmt.Location = new System.Drawing.Point(417, 119);
            this.labelLoanAmt.Name = "labelLoanAmt";
            this.labelLoanAmt.Size = new System.Drawing.Size(34, 13);
            this.labelLoanAmt.TabIndex = 17;
            this.labelLoanAmt.Text = "$0.00";
            // 
            // labelLoanAmtHeading
            // 
            this.labelLoanAmtHeading.AutoSize = true;
            this.labelLoanAmtHeading.Location = new System.Drawing.Point(357, 119);
            this.labelLoanAmtHeading.Name = "labelLoanAmtHeading";
            this.labelLoanAmtHeading.Size = new System.Drawing.Size(55, 13);
            this.labelLoanAmtHeading.TabIndex = 16;
            this.labelLoanAmtHeading.Text = "Loan Amt:";
            // 
            // tableLayoutPanelItem
            // 
            this.tableLayoutPanelItem.ColumnCount = 5;
            this.tableLayoutPanelItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 469F));
            this.tableLayoutPanelItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanelItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanelItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelItem.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanelItem.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanelItem.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanelItem.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanelItem.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanelItem.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanelItem.Location = new System.Drawing.Point(19, 163);
            this.tableLayoutPanelItem.Name = "tableLayoutPanelItem";
            this.tableLayoutPanelItem.RowCount = 1;
            this.tableLayoutPanelItem.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelItem.Size = new System.Drawing.Size(796, 40);
            this.tableLayoutPanelItem.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(472, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 26);
            this.label5.TabIndex = 20;
            this.label5.Text = "Gun Number";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Item Description";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(688, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Other";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(608, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Shelf";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(543, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Aisle";
            // 
            // tableLayoutPanelItemData
            // 
            this.tableLayoutPanelItemData.AutoSize = true;
            this.tableLayoutPanelItemData.ColumnCount = 1;
            this.tableLayoutPanelItemData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.82902F));
            this.tableLayoutPanelItemData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.17098F));
            this.tableLayoutPanelItemData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanelItemData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanelItemData.Location = new System.Drawing.Point(18, 206);
            this.tableLayoutPanelItemData.Name = "tableLayoutPanelItemData";
            this.tableLayoutPanelItemData.RowCount = 1;
            this.tableLayoutPanelItemData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelItemData.Size = new System.Drawing.Size(796, 40);
            this.tableLayoutPanelItemData.TabIndex = 19;
            // 
            // pageFooterMarkerLabel
            // 
            this.pageFooterMarkerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pageFooterMarkerLabel.AutoSize = true;
            this.pageFooterMarkerLabel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageFooterMarkerLabel.Location = new System.Drawing.Point(16, 492);
            this.pageFooterMarkerLabel.Name = "pageFooterMarkerLabel";
            this.pageFooterMarkerLabel.Size = new System.Drawing.Size(152, 16);
            this.pageFooterMarkerLabel.TabIndex = 31;
            this.pageFooterMarkerLabel.Text = "Page Footer Marker";
            this.pageFooterMarkerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pageFooterMarkerLabel.Visible = false;
            // 
            // labelCurrentPrincipal
            // 
            this.labelCurrentPrincipal.AutoSize = true;
            this.labelCurrentPrincipal.Location = new System.Drawing.Point(713, 119);
            this.labelCurrentPrincipal.Name = "labelCurrentPrincipal";
            this.labelCurrentPrincipal.Size = new System.Drawing.Size(34, 13);
            this.labelCurrentPrincipal.TabIndex = 33;
            this.labelCurrentPrincipal.Text = "$0.00";
            // 
            // labelCurrentPrincipalHeading
            // 
            this.labelCurrentPrincipalHeading.AutoSize = true;
            this.labelCurrentPrincipalHeading.Location = new System.Drawing.Point(621, 119);
            this.labelCurrentPrincipalHeading.Name = "labelCurrentPrincipalHeading";
            this.labelCurrentPrincipalHeading.Size = new System.Drawing.Size(87, 13);
            this.labelCurrentPrincipalHeading.TabIndex = 32;
            this.labelCurrentPrincipalHeading.Text = "Current Principal:";
            // 
            // PickSlipPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(838, 514);
            this.Controls.Add(this.labelCurrentPrincipal);
            this.Controls.Add(this.labelCurrentPrincipalHeading);
            this.Controls.Add(this.pageFooterMarkerLabel);
            this.Controls.Add(this.tableLayoutPanelItemData);
            this.Controls.Add(this.tableLayoutPanelItem);
            this.Controls.Add(this.labelLoanAmt);
            this.Controls.Add(this.labelLoanAmtHeading);
            this.Controls.Add(this.labelCustName);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labelCustHeading);
            this.Controls.Add(this.labelPageNo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelPreviousLoanNo);
            this.Controls.Add(this.labelCurrentLoanNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelCurrLoanNoHeading);
            this.Controls.Add(this.labelEmployeeNo);
            this.Controls.Add(this.labelorigLoanNo);
            this.Controls.Add(this.labelEmpNoHeading);
            this.Controls.Add(this.labelOriginalLoanNoHeading);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelTransactionType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PickSlipPrint";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PickSlipPrint";
            this.Load += new System.EventHandler(this.PickSlipPrint_Load);
            this.tableLayoutPanelItem.ResumeLayout(false);
            this.tableLayoutPanelItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTransactionType;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelOriginalLoanNoHeading;
        private System.Windows.Forms.Label labelEmpNoHeading;
        private System.Windows.Forms.Label labelorigLoanNo;
        private System.Windows.Forms.Label labelEmployeeNo;
        private System.Windows.Forms.Label labelPreviousLoanNo;
        private System.Windows.Forms.Label labelCurrentLoanNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelCurrLoanNoHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelPageNo;
        private System.Windows.Forms.Label labelCustHeading;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelCustName;
        private System.Windows.Forms.Label labelLoanAmt;
        private System.Windows.Forms.Label labelLoanAmtHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelItemData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label pageFooterMarkerLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelCurrentPrincipal;
        private System.Windows.Forms.Label labelCurrentPrincipalHeading;
    }
}