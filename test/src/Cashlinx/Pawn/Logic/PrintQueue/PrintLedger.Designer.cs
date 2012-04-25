using Common.Libraries.Forms.Components;

namespace Pawn.Logic.PrintQueue
{
    partial class PreliminaryLedger
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
            this.tableLayoutPanelHeading = new System.Windows.Forms.TableLayoutPanel();
            this.labelTranNoHeading = new System.Windows.Forms.Label();
            this.labelEmpNoHeading = new System.Windows.Forms.Label();
            this.labelPrevNoHeading = new System.Windows.Forms.Label();
            this.labelCustNameHeading = new System.Windows.Forms.Label();
            this.labelMethodOfPmtHeading = new System.Windows.Forms.Label();
            this.labelTranTypeHeading = new System.Windows.Forms.Label();
            this.labelDisbursedAmtHeading = new System.Windows.Forms.Label();
            this.labelReceiptAmtHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanelTranData = new System.Windows.Forms.TableLayoutPanel();
            this.labelDateValue = new System.Windows.Forms.Label();
            this.pictureBoxLogoImage = new System.Windows.Forms.PictureBox();
            this.groupBoxFinal = new System.Windows.Forms.GroupBox();
            this.customLabelEndHeading = new CustomLabel();
            this.customLabelReportNo = new CustomLabel();
            this.customLabelReportNoHeading = new CustomLabel();
            this.customLabelPageNo = new CustomLabel();
            this.customLabelFooter = new CustomLabel();
            this.customLabelEmployee = new CustomLabel();
            this.customLabelCashDrawerName = new CustomLabel();
            this.customLabelEmployeeHeading = new CustomLabel();
            this.customLabelDrawerIDHeading = new CustomLabel();
            this.customLabelDate = new CustomLabel();
            this.customLabelStoreName = new CustomLabel();
            this.customLabelHeading = new CustomLabel();
            this.customLabelStatus = new CustomLabel();
            this.customLabelTranAmt = new CustomLabel();
            this.tableLayoutPanelHeading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelHeading
            // 
            this.tableLayoutPanelHeading.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelHeading.ColumnCount = 10;
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 187F));
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanelHeading.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanelHeading.Controls.Add(this.labelTranNoHeading, 0, 0);
            this.tableLayoutPanelHeading.Controls.Add(this.labelEmpNoHeading, 0, 0);
            this.tableLayoutPanelHeading.Controls.Add(this.labelPrevNoHeading, 1, 0);
            this.tableLayoutPanelHeading.Controls.Add(this.labelCustNameHeading, 3, 0);
            this.tableLayoutPanelHeading.Controls.Add(this.labelMethodOfPmtHeading, 4, 0);
            this.tableLayoutPanelHeading.Controls.Add(this.labelTranTypeHeading, 6, 0);
            this.tableLayoutPanelHeading.Controls.Add(this.customLabelStatus, 5, 0);
            this.tableLayoutPanelHeading.Controls.Add(this.labelDisbursedAmtHeading, 9, 0);
            this.tableLayoutPanelHeading.Controls.Add(this.labelReceiptAmtHeading, 8, 0);
            this.tableLayoutPanelHeading.Controls.Add(this.customLabelTranAmt, 7, 0);
            this.tableLayoutPanelHeading.Location = new System.Drawing.Point(12, 91);
            this.tableLayoutPanelHeading.Name = "tableLayoutPanelHeading";
            this.tableLayoutPanelHeading.RowCount = 1;
            this.tableLayoutPanelHeading.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHeading.Size = new System.Drawing.Size(871, 44);
            this.tableLayoutPanelHeading.TabIndex = 0;
            // 
            // labelTranNoHeading
            // 
            this.labelTranNoHeading.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTranNoHeading.AutoSize = true;
            this.labelTranNoHeading.Location = new System.Drawing.Point(76, 15);
            this.labelTranNoHeading.Name = "labelTranNoHeading";
            this.labelTranNoHeading.Size = new System.Drawing.Size(42, 13);
            this.labelTranNoHeading.TabIndex = 9;
            this.labelTranNoHeading.Text = "Tran. #";
            // 
            // labelEmpNoHeading
            // 
            this.labelEmpNoHeading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelEmpNoHeading.AutoSize = true;
            this.labelEmpNoHeading.Location = new System.Drawing.Point(16, 15);
            this.labelEmpNoHeading.Name = "labelEmpNoHeading";
            this.labelEmpNoHeading.Size = new System.Drawing.Size(41, 13);
            this.labelEmpNoHeading.TabIndex = 0;
            this.labelEmpNoHeading.Text = "Emp. #";
            // 
            // labelPrevNoHeading
            // 
            this.labelPrevNoHeading.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPrevNoHeading.AutoSize = true;
            this.labelPrevNoHeading.Location = new System.Drawing.Point(139, 15);
            this.labelPrevNoHeading.Name = "labelPrevNoHeading";
            this.labelPrevNoHeading.Size = new System.Drawing.Size(42, 13);
            this.labelPrevNoHeading.TabIndex = 2;
            this.labelPrevNoHeading.Text = "Prev. #";
            // 
            // labelCustNameHeading
            // 
            this.labelCustNameHeading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCustNameHeading.AutoSize = true;
            this.labelCustNameHeading.Location = new System.Drawing.Point(255, 15);
            this.labelCustNameHeading.Name = "labelCustNameHeading";
            this.labelCustNameHeading.Size = new System.Drawing.Size(82, 13);
            this.labelCustNameHeading.TabIndex = 5;
            this.labelCustNameHeading.Text = "Customer Name";
            // 
            // labelMethodOfPmtHeading
            // 
            this.labelMethodOfPmtHeading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelMethodOfPmtHeading.AutoSize = true;
            this.labelMethodOfPmtHeading.Location = new System.Drawing.Point(411, 15);
            this.labelMethodOfPmtHeading.Name = "labelMethodOfPmtHeading";
            this.labelMethodOfPmtHeading.Size = new System.Drawing.Size(31, 13);
            this.labelMethodOfPmtHeading.TabIndex = 6;
            this.labelMethodOfPmtHeading.Text = "MOP";
            // 
            // labelTranTypeHeading
            // 
            this.labelTranTypeHeading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTranTypeHeading.AutoSize = true;
            this.labelTranTypeHeading.Location = new System.Drawing.Point(546, 15);
            this.labelTranTypeHeading.Name = "labelTranTypeHeading";
            this.labelTranTypeHeading.Size = new System.Drawing.Size(59, 13);
            this.labelTranTypeHeading.TabIndex = 4;
            this.labelTranTypeHeading.Text = "Tran. Type";
            // 
            // labelDisbursedAmtHeading
            // 
            this.labelDisbursedAmtHeading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDisbursedAmtHeading.AutoSize = true;
            this.labelDisbursedAmtHeading.Location = new System.Drawing.Point(793, 15);
            this.labelDisbursedAmtHeading.Name = "labelDisbursedAmtHeading";
            this.labelDisbursedAmtHeading.Size = new System.Drawing.Size(71, 13);
            this.labelDisbursedAmtHeading.TabIndex = 8;
            this.labelDisbursedAmtHeading.Text = "Disbursement";
            // 
            // labelReceiptAmtHeading
            // 
            this.labelReceiptAmtHeading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelReceiptAmtHeading.AutoSize = true;
            this.labelReceiptAmtHeading.Location = new System.Drawing.Point(720, 15);
            this.labelReceiptAmtHeading.Name = "labelReceiptAmtHeading";
            this.labelReceiptAmtHeading.Size = new System.Drawing.Size(44, 13);
            this.labelReceiptAmtHeading.TabIndex = 7;
            this.labelReceiptAmtHeading.Text = "Receipt";
            // 
            // tableLayoutPanelTranData
            // 
            this.tableLayoutPanelTranData.AutoSize = true;
            this.tableLayoutPanelTranData.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelTranData.ColumnCount = 10;
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 187F));
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanelTranData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanelTranData.Location = new System.Drawing.Point(12, 134);
            this.tableLayoutPanelTranData.Name = "tableLayoutPanelTranData";
            this.tableLayoutPanelTranData.RowCount = 1;
            this.tableLayoutPanelTranData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTranData.Size = new System.Drawing.Size(871, 44);
            this.tableLayoutPanelTranData.TabIndex = 1;
            // 
            // labelDateValue
            // 
            this.labelDateValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDateValue.AutoSize = true;
            this.labelDateValue.Location = new System.Drawing.Point(716, 28);
            this.labelDateValue.Name = "labelDateValue";
            this.labelDateValue.Size = new System.Drawing.Size(65, 13);
            this.labelDateValue.TabIndex = 4;
            this.labelDateValue.Text = "mm/dd/yyyy";
            // 
            // pictureBoxLogoImage
            // 
            this.pictureBoxLogoImage.Image = Pawn.Properties.Resources.logo;
            this.pictureBoxLogoImage.Location = new System.Drawing.Point(11, 8);
            this.pictureBoxLogoImage.Name = "pictureBoxLogoImage";
            this.pictureBoxLogoImage.Size = new System.Drawing.Size(192, 42);
            this.pictureBoxLogoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogoImage.TabIndex = 5;
            this.pictureBoxLogoImage.TabStop = false;
            // 
            // groupBoxFinal
            // 
            this.groupBoxFinal.Location = new System.Drawing.Point(11, 601);
            this.groupBoxFinal.Name = "groupBoxFinal";
            this.groupBoxFinal.Size = new System.Drawing.Size(869, 2);
            this.groupBoxFinal.TabIndex = 17;
            this.groupBoxFinal.TabStop = false;
            this.groupBoxFinal.Text = "groupBox1";
            this.groupBoxFinal.Visible = false;
            // 
            // customLabelEndHeading
            // 
            this.customLabelEndHeading.AutoSize = true;
            this.customLabelEndHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelEndHeading.Location = new System.Drawing.Point(408, 616);
            this.customLabelEndHeading.Name = "customLabelEndHeading";
            this.customLabelEndHeading.Size = new System.Drawing.Size(112, 13);
            this.customLabelEndHeading.TabIndex = 16;
            this.customLabelEndHeading.Text = "***End Of Report***";
            this.customLabelEndHeading.Visible = false;
            // 
            // customLabelReportNo
            // 
            this.customLabelReportNo.AutoSize = true;
            this.customLabelReportNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelReportNo.Location = new System.Drawing.Point(709, 44);
            this.customLabelReportNo.Name = "customLabelReportNo";
            this.customLabelReportNo.Size = new System.Drawing.Size(25, 13);
            this.customLabelReportNo.TabIndex = 15;
            this.customLabelReportNo.Text = "226";
            // 
            // customLabelReportNoHeading
            // 
            this.customLabelReportNoHeading.AutoSize = true;
            this.customLabelReportNoHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelReportNoHeading.Location = new System.Drawing.Point(647, 44);
            this.customLabelReportNoHeading.Name = "customLabelReportNoHeading";
            this.customLabelReportNoHeading.Size = new System.Drawing.Size(55, 13);
            this.customLabelReportNoHeading.TabIndex = 14;
            this.customLabelReportNoHeading.Text = "Report #:";
            // 
            // customLabelPageNo
            // 
            this.customLabelPageNo.AutoSize = true;
            this.customLabelPageNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelPageNo.Location = new System.Drawing.Point(720, 644);
            this.customLabelPageNo.Name = "customLabelPageNo";
            this.customLabelPageNo.Size = new System.Drawing.Size(62, 13);
            this.customLabelPageNo.TabIndex = 13;
            this.customLabelPageNo.Text = "Page 1 of 1";
            // 
            // customLabelFooter
            // 
            this.customLabelFooter.AutoSize = true;
            this.customLabelFooter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelFooter.Location = new System.Drawing.Point(26, 644);
            this.customLabelFooter.Name = "customLabelFooter";
            this.customLabelFooter.Size = new System.Drawing.Size(131, 13);
            this.customLabelFooter.TabIndex = 12;
            this.customLabelFooter.Text = "Preliminary Ledger Report";
            // 
            // customLabelEmployee
            // 
            this.customLabelEmployee.AutoSize = true;
            this.customLabelEmployee.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelEmployee.Location = new System.Drawing.Point(395, 64);
            this.customLabelEmployee.Name = "customLabelEmployee";
            this.customLabelEmployee.Size = new System.Drawing.Size(111, 13);
            this.customLabelEmployee.TabIndex = 11;
            this.customLabelEmployee.Text = "1234 - Test Employee";
            // 
            // customLabelCashDrawerName
            // 
            this.customLabelCashDrawerName.AutoSize = true;
            this.customLabelCashDrawerName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelCashDrawerName.Location = new System.Drawing.Point(78, 64);
            this.customLabelCashDrawerName.Name = "customLabelCashDrawerName";
            this.customLabelCashDrawerName.Size = new System.Drawing.Size(27, 13);
            this.customLabelCashDrawerName.TabIndex = 10;
            this.customLabelCashDrawerName.Text = "CD1";
            // 
            // customLabelEmployeeHeading
            // 
            this.customLabelEmployeeHeading.AutoSize = true;
            this.customLabelEmployeeHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelEmployeeHeading.Location = new System.Drawing.Point(332, 64);
            this.customLabelEmployeeHeading.Name = "customLabelEmployeeHeading";
            this.customLabelEmployeeHeading.Size = new System.Drawing.Size(57, 13);
            this.customLabelEmployeeHeading.TabIndex = 9;
            this.customLabelEmployeeHeading.Text = "Employee:";
            // 
            // customLabelDrawerIDHeading
            // 
            this.customLabelDrawerIDHeading.AutoSize = true;
            this.customLabelDrawerIDHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelDrawerIDHeading.Location = new System.Drawing.Point(12, 64);
            this.customLabelDrawerIDHeading.Name = "customLabelDrawerIDHeading";
            this.customLabelDrawerIDHeading.Size = new System.Drawing.Size(60, 13);
            this.customLabelDrawerIDHeading.TabIndex = 8;
            this.customLabelDrawerIDHeading.Text = "Drawer ID:";
            // 
            // customLabelDate
            // 
            this.customLabelDate.AutoSize = true;
            this.customLabelDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelDate.Location = new System.Drawing.Point(647, 28);
            this.customLabelDate.Name = "customLabelDate";
            this.customLabelDate.Size = new System.Drawing.Size(56, 13);
            this.customLabelDate.TabIndex = 7;
            this.customLabelDate.Text = "Run Date:";
            // 
            // customLabelStoreName
            // 
            this.customLabelStoreName.AutoSize = true;
            this.customLabelStoreName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelStoreName.Location = new System.Drawing.Point(646, 13);
            this.customLabelStoreName.Name = "customLabelStoreName";
            this.customLabelStoreName.Size = new System.Drawing.Size(135, 13);
            this.customLabelStoreName.TabIndex = 6;
            this.customLabelStoreName.Text = "Super Pawn of Dallas ###";
            // 
            // customLabelHeading
            // 
            this.customLabelHeading.AutoSize = true;
            this.customLabelHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelHeading.Location = new System.Drawing.Point(350, 13);
            this.customLabelHeading.Name = "customLabelHeading";
            this.customLabelHeading.Size = new System.Drawing.Size(131, 13);
            this.customLabelHeading.TabIndex = 2;
            this.customLabelHeading.Text = "Preliminary Ledger Report";
            // 
            // customLabelStatus
            // 
            this.customLabelStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.customLabelStatus.AutoSize = true;
            this.customLabelStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelStatus.Location = new System.Drawing.Point(475, 15);
            this.customLabelStatus.Name = "customLabelStatus";
            this.customLabelStatus.Size = new System.Drawing.Size(38, 13);
            this.customLabelStatus.TabIndex = 10;
            this.customLabelStatus.Text = "Status";
            // 
            // customLabelTranAmt
            // 
            this.customLabelTranAmt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.customLabelTranAmt.AutoSize = true;
            this.customLabelTranAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelTranAmt.Location = new System.Drawing.Point(630, 15);
            this.customLabelTranAmt.Name = "customLabelTranAmt";
            this.customLabelTranAmt.Size = new System.Drawing.Size(73, 13);
            this.customLabelTranAmt.TabIndex = 11;
            this.customLabelTranAmt.Text = "Tran. Amount";
            // 
            // PreliminaryLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 666);
            this.Controls.Add(this.groupBoxFinal);
            this.Controls.Add(this.customLabelEndHeading);
            this.Controls.Add(this.customLabelReportNo);
            this.Controls.Add(this.customLabelReportNoHeading);
            this.Controls.Add(this.customLabelPageNo);
            this.Controls.Add(this.customLabelFooter);
            this.Controls.Add(this.customLabelEmployee);
            this.Controls.Add(this.customLabelCashDrawerName);
            this.Controls.Add(this.customLabelEmployeeHeading);
            this.Controls.Add(this.customLabelDrawerIDHeading);
            this.Controls.Add(this.customLabelDate);
            this.Controls.Add(this.customLabelStoreName);
            this.Controls.Add(this.pictureBoxLogoImage);
            this.Controls.Add(this.labelDateValue);
            this.Controls.Add(this.customLabelHeading);
            this.Controls.Add(this.tableLayoutPanelTranData);
            this.Controls.Add(this.tableLayoutPanelHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PreliminaryLedger";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PreliminaryLedger";
            this.Load += new System.EventHandler(this.PreliminaryLedger_Load);
            this.tableLayoutPanelHeading.ResumeLayout(false);
            this.tableLayoutPanelHeading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogoImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHeading;
        private System.Windows.Forms.Label labelDisbursedAmtHeading;
        private System.Windows.Forms.Label labelReceiptAmtHeading;
        private System.Windows.Forms.Label labelMethodOfPmtHeading;
        private System.Windows.Forms.Label labelTranTypeHeading;
        private System.Windows.Forms.Label labelPrevNoHeading;
        private System.Windows.Forms.Label labelEmpNoHeading;
        private System.Windows.Forms.Label labelCustNameHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTranData;
        private CustomLabel customLabelHeading;
        private System.Windows.Forms.Label labelDateValue;
        private System.Windows.Forms.PictureBox pictureBoxLogoImage;
        private System.Windows.Forms.Label labelTranNoHeading;
        private CustomLabel customLabelStoreName;
        private CustomLabel customLabelDate;
        private CustomLabel customLabelDrawerIDHeading;
        private CustomLabel customLabelEmployeeHeading;
        private CustomLabel customLabelCashDrawerName;
        private CustomLabel customLabelEmployee;
        private CustomLabel customLabelFooter;
        private CustomLabel customLabelPageNo;
        private CustomLabel customLabelStatus;
        private CustomLabel customLabelTranAmt;
        private CustomLabel customLabelReportNoHeading;
        private CustomLabel customLabelReportNo;
        private CustomLabel customLabelEndHeading;
        private System.Windows.Forms.GroupBox groupBoxFinal;
    }
}
