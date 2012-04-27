using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Pawn.Forms.UserControls.CashTransferReports;

namespace Pawn.Logic.PrintQueue
{
    partial class BankAndInternalCashTransfer
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
            this.labelSourceHeading = new System.Windows.Forms.Label();
            this.labelbankname = new System.Windows.Forms.Label();
            this.labelchecknumber = new System.Windows.Forms.Label();
            this.labelcchecknoheading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelCarrierSignature = new System.Windows.Forms.Label();
            this.labelTransferAmount = new System.Windows.Forms.Label();
            this.labelAmtTransferredHeading = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelempnumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelempname = new System.Windows.Forms.Label();
            this.labelSignature = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelEmpData = new System.Windows.Forms.Panel();
            this.panelBankData = new System.Windows.Forms.Panel();
            this.cashTransferHeader1 = new CashTransferHeader();
            this.labelToDrawerNoHeading = new System.Windows.Forms.Label();
            this.labelToDrawerName = new System.Windows.Forms.Label();
            this.labelFromDrawerName = new System.Windows.Forms.Label();
            this.labelFromDrawerHeading = new System.Windows.Forms.Label();
            this.panelEmpData.SuspendLayout();
            this.panelBankData.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelSourceHeading
            // 
            this.labelSourceHeading.AutoSize = true;
            this.labelSourceHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceHeading.Location = new System.Drawing.Point(12, 128);
            this.labelSourceHeading.Name = "labelSourceHeading";
            this.labelSourceHeading.Size = new System.Drawing.Size(116, 16);
            this.labelSourceHeading.TabIndex = 22;
            this.labelSourceHeading.Text = "TRANSFER FROM:";
            // 
            // labelbankname
            // 
            this.labelbankname.AutoSize = true;
            this.labelbankname.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelbankname.Location = new System.Drawing.Point(3, 4);
            this.labelbankname.Name = "labelbankname";
            this.labelbankname.Size = new System.Drawing.Size(89, 16);
            this.labelbankname.TabIndex = 24;
            this.labelbankname.Text = "<bank name>";
            // 
            // labelchecknumber
            // 
            this.labelchecknumber.AutoSize = true;
            this.labelchecknumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelchecknumber.Location = new System.Drawing.Point(79, 28);
            this.labelchecknumber.Name = "labelchecknumber";
            this.labelchecknumber.Size = new System.Drawing.Size(106, 16);
            this.labelchecknumber.TabIndex = 25;
            this.labelchecknumber.Text = "<check number>";
            // 
            // labelcchecknoheading
            // 
            this.labelcchecknoheading.AutoSize = true;
            this.labelcchecknoheading.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelcchecknoheading.Location = new System.Drawing.Point(3, 28);
            this.labelcchecknoheading.Name = "labelcchecknoheading";
            this.labelcchecknoheading.Size = new System.Drawing.Size(56, 14);
            this.labelcchecknoheading.TabIndex = 38;
            this.labelcchecknoheading.Text = "Check #";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(6, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 2);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // labelCarrierSignature
            // 
            this.labelCarrierSignature.AutoSize = true;
            this.labelCarrierSignature.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCarrierSignature.Location = new System.Drawing.Point(5, 105);
            this.labelCarrierSignature.Name = "labelCarrierSignature";
            this.labelCarrierSignature.Size = new System.Drawing.Size(111, 14);
            this.labelCarrierSignature.TabIndex = 40;
            this.labelCarrierSignature.Text = "Carrier Signature";
            // 
            // labelTransferAmount
            // 
            this.labelTransferAmount.AutoSize = true;
            this.labelTransferAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferAmount.Location = new System.Drawing.Point(647, 277);
            this.labelTransferAmount.Name = "labelTransferAmount";
            this.labelTransferAmount.Size = new System.Drawing.Size(148, 14);
            this.labelTransferAmount.TabIndex = 49;
            this.labelTransferAmount.Text = "<amount transferred>";
            // 
            // labelAmtTransferredHeading
            // 
            this.labelAmtTransferredHeading.AutoSize = true;
            this.labelAmtTransferredHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmtTransferredHeading.Location = new System.Drawing.Point(428, 273);
            this.labelAmtTransferredHeading.Name = "labelAmtTransferredHeading";
            this.labelAmtTransferredHeading.Size = new System.Drawing.Size(209, 19);
            this.labelAmtTransferredHeading.TabIndex = 48;
            this.labelAmtTransferredHeading.Text = "AMOUNT TRANSFERRED";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 50;
            this.label2.Text = "TRANSFER TO:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 14);
            this.label3.TabIndex = 51;
            this.label3.Text = "Emp #";
            // 
            // labelempnumber
            // 
            this.labelempnumber.AutoSize = true;
            this.labelempnumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelempnumber.Location = new System.Drawing.Point(70, 10);
            this.labelempnumber.Name = "labelempnumber";
            this.labelempnumber.Size = new System.Drawing.Size(129, 16);
            this.labelempnumber.TabIndex = 52;
            this.labelempnumber.Text = "<employee number>";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 14);
            this.label5.TabIndex = 53;
            this.label5.Text = "Emp. Name";
            // 
            // labelempname
            // 
            this.labelempname.AutoSize = true;
            this.labelempname.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelempname.Location = new System.Drawing.Point(84, 31);
            this.labelempname.Name = "labelempname";
            this.labelempname.Size = new System.Drawing.Size(117, 16);
            this.labelempname.TabIndex = 54;
            this.labelempname.Text = "<employee name>";
            // 
            // labelSignature
            // 
            this.labelSignature.AutoSize = true;
            this.labelSignature.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSignature.Location = new System.Drawing.Point(6, 110);
            this.labelSignature.Name = "labelSignature";
            this.labelSignature.Size = new System.Drawing.Size(67, 14);
            this.labelSignature.TabIndex = 56;
            this.labelSignature.Text = "Signature";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(7, 98);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 2);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            // 
            // panelEmpData
            // 
            this.panelEmpData.Controls.Add(this.labelToDrawerName);
            this.panelEmpData.Controls.Add(this.labelToDrawerNoHeading);
            this.panelEmpData.Controls.Add(this.label3);
            this.panelEmpData.Controls.Add(this.labelSignature);
            this.panelEmpData.Controls.Add(this.labelempnumber);
            this.panelEmpData.Controls.Add(this.groupBox2);
            this.panelEmpData.Controls.Add(this.label5);
            this.panelEmpData.Controls.Add(this.labelempname);
            this.panelEmpData.Location = new System.Drawing.Point(17, 326);
            this.panelEmpData.Name = "panelEmpData";
            this.panelEmpData.Size = new System.Drawing.Size(489, 125);
            this.panelEmpData.TabIndex = 57;
            // 
            // panelBankData
            // 
            this.panelBankData.Controls.Add(this.labelFromDrawerName);
            this.panelBankData.Controls.Add(this.labelFromDrawerHeading);
            this.panelBankData.Controls.Add(this.labelbankname);
            this.panelBankData.Controls.Add(this.labelchecknumber);
            this.panelBankData.Controls.Add(this.labelcchecknoheading);
            this.panelBankData.Controls.Add(this.groupBox1);
            this.panelBankData.Controls.Add(this.labelCarrierSignature);
            this.panelBankData.Location = new System.Drawing.Point(17, 147);
            this.panelBankData.Name = "panelBankData";
            this.panelBankData.Size = new System.Drawing.Size(489, 121);
            this.panelBankData.TabIndex = 58;
            // 
            // cashTransferHeader1
            // 
            this.cashTransferHeader1.BackColor = System.Drawing.Color.Transparent;
            this.cashTransferHeader1.Location = new System.Drawing.Point(4, 0);
            this.cashTransferHeader1.Name = "cashTransferHeader1";
            this.cashTransferHeader1.Size = new System.Drawing.Size(808, 125);
            this.cashTransferHeader1.TabIndex = 0;
            // 
            // labelToDrawerNoHeading
            // 
            this.labelToDrawerNoHeading.AutoSize = true;
            this.labelToDrawerNoHeading.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelToDrawerNoHeading.Location = new System.Drawing.Point(261, 12);
            this.labelToDrawerNoHeading.Name = "labelToDrawerNoHeading";
            this.labelToDrawerNoHeading.Size = new System.Drawing.Size(64, 14);
            this.labelToDrawerNoHeading.TabIndex = 57;
            this.labelToDrawerNoHeading.Text = "Drawer #";
            this.labelToDrawerNoHeading.Visible = false;
            // 
            // labelToDrawerName
            // 
            this.labelToDrawerName.AutoSize = true;
            this.labelToDrawerName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelToDrawerName.Location = new System.Drawing.Point(336, 12);
            this.labelToDrawerName.Name = "labelToDrawerName";
            this.labelToDrawerName.Size = new System.Drawing.Size(115, 16);
            this.labelToDrawerName.TabIndex = 58;
            this.labelToDrawerName.Text = "<drawer number>";
            this.labelToDrawerName.Visible = false;
            // 
            // labelFromDrawerName
            // 
            this.labelFromDrawerName.AutoSize = true;
            this.labelFromDrawerName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFromDrawerName.Location = new System.Drawing.Point(336, 6);
            this.labelFromDrawerName.Name = "labelFromDrawerName";
            this.labelFromDrawerName.Size = new System.Drawing.Size(115, 16);
            this.labelFromDrawerName.TabIndex = 60;
            this.labelFromDrawerName.Text = "<drawer number>";
            this.labelFromDrawerName.Visible = false;
            // 
            // labelFromDrawerHeading
            // 
            this.labelFromDrawerHeading.AutoSize = true;
            this.labelFromDrawerHeading.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFromDrawerHeading.Location = new System.Drawing.Point(261, 6);
            this.labelFromDrawerHeading.Name = "labelFromDrawerHeading";
            this.labelFromDrawerHeading.Size = new System.Drawing.Size(64, 14);
            this.labelFromDrawerHeading.TabIndex = 59;
            this.labelFromDrawerHeading.Text = "Drawer #";
            this.labelFromDrawerHeading.Visible = false;
            // 
            // BankAndInternalCashTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(844, 463);
            this.Controls.Add(this.panelBankData);
            this.Controls.Add(this.panelEmpData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTransferAmount);
            this.Controls.Add(this.labelAmtTransferredHeading);
            this.Controls.Add(this.labelSourceHeading);
            this.Controls.Add(this.cashTransferHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BankAndInternalCashTransfer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "BankCashTransfer";
            this.Load += new System.EventHandler(this.BankAndInternalCashTransfer_Load);
            this.panelEmpData.ResumeLayout(false);
            this.panelEmpData.PerformLayout();
            this.panelBankData.ResumeLayout(false);
            this.panelBankData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CashTransferHeader cashTransferHeader1;
        private System.Windows.Forms.Label labelSourceHeading;
        private System.Windows.Forms.Label labelbankname;
        private System.Windows.Forms.Label labelchecknumber;
        private System.Windows.Forms.Label labelcchecknoheading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelCarrierSignature;
        private System.Windows.Forms.Label labelTransferAmount;
        private System.Windows.Forms.Label labelAmtTransferredHeading;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelempnumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelempname;
        private System.Windows.Forms.Label labelSignature;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelEmpData;
        private System.Windows.Forms.Panel panelBankData;
        private System.Windows.Forms.Label labelToDrawerName;
        private System.Windows.Forms.Label labelToDrawerNoHeading;
        private System.Windows.Forms.Label labelFromDrawerName;
        private System.Windows.Forms.Label labelFromDrawerHeading;
    }
}