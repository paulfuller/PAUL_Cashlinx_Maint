using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Pawn.Forms.UserControls.CashTransferReports;

namespace Pawn.Logic.PrintQueue
{
    partial class ShopToShopCashTransfer
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
            this.label2 = new System.Windows.Forms.Label();
            this.labelSourceShopNo = new System.Windows.Forms.Label();
            this.labelSourceShopName = new System.Windows.Forms.Label();
            this.labelSourceShopAddress = new System.Windows.Forms.Label();
            this.labelSourceShopAddr2 = new System.Windows.Forms.Label();
            this.labelSourcePhone = new System.Windows.Forms.Label();
            this.labeldestphone = new System.Windows.Forms.Label();
            this.labeldestshopaddr2 = new System.Windows.Forms.Label();
            this.labeldestshopaddr = new System.Windows.Forms.Label();
            this.labeldestshopname = new System.Windows.Forms.Label();
            this.labeldestshopno = new System.Windows.Forms.Label();
            this.labelempnameheading = new System.Windows.Forms.Label();
            this.labelcommentheading = new System.Windows.Forms.Label();
            this.labelempname = new System.Windows.Forms.Label();
            this.labelsourcecomment = new System.Windows.Forms.Label();
            this.labeldestComment = new System.Windows.Forms.Label();
            this.labelDestempName = new System.Windows.Forms.Label();
            this.labeldestcommentheading = new System.Windows.Forms.Label();
            this.labeldestempnameheading = new System.Windows.Forms.Label();
            this.labelAmtTransferredHeading = new System.Windows.Forms.Label();
            this.labelTransferAmount = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cashTransferHeader1 = new CashTransferHeader();
            this.SuspendLayout();
            // 
            // labelSourceHeading
            // 
            this.labelSourceHeading.AutoSize = true;
            this.labelSourceHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceHeading.Location = new System.Drawing.Point(12, 133);
            this.labelSourceHeading.Name = "labelSourceHeading";
            this.labelSourceHeading.Size = new System.Drawing.Size(116, 16);
            this.labelSourceHeading.TabIndex = 21;
            this.labelSourceHeading.Text = "TRANSFER FROM:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(486, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "TRANSFER TO:";
            // 
            // labelSourceShopNo
            // 
            this.labelSourceShopNo.AutoSize = true;
            this.labelSourceShopNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopNo.Location = new System.Drawing.Point(13, 168);
            this.labelSourceShopNo.Name = "labelSourceShopNo";
            this.labelSourceShopNo.Size = new System.Drawing.Size(101, 16);
            this.labelSourceShopNo.TabIndex = 23;
            this.labelSourceShopNo.Text = "<shop number>";
            // 
            // labelSourceShopName
            // 
            this.labelSourceShopName.AutoSize = true;
            this.labelSourceShopName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopName.Location = new System.Drawing.Point(12, 184);
            this.labelSourceShopName.Name = "labelSourceShopName";
            this.labelSourceShopName.Size = new System.Drawing.Size(89, 16);
            this.labelSourceShopName.TabIndex = 24;
            this.labelSourceShopName.Text = "<shop name>";
            // 
            // labelSourceShopAddress
            // 
            this.labelSourceShopAddress.AutoSize = true;
            this.labelSourceShopAddress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopAddress.Location = new System.Drawing.Point(14, 201);
            this.labelSourceShopAddress.Name = "labelSourceShopAddress";
            this.labelSourceShopAddress.Size = new System.Drawing.Size(102, 16);
            this.labelSourceShopAddress.TabIndex = 25;
            this.labelSourceShopAddress.Text = "<shop address>";
            // 
            // labelSourceShopAddr2
            // 
            this.labelSourceShopAddr2.AutoSize = true;
            this.labelSourceShopAddr2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceShopAddr2.Location = new System.Drawing.Point(14, 219);
            this.labelSourceShopAddr2.Name = "labelSourceShopAddr2";
            this.labelSourceShopAddr2.Size = new System.Drawing.Size(257, 16);
            this.labelSourceShopAddr2.TabIndex = 26;
            this.labelSourceShopAddr2.Text = "<shop city>,<shop state> <shop zip code>";
            // 
            // labelSourcePhone
            // 
            this.labelSourcePhone.AutoSize = true;
            this.labelSourcePhone.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourcePhone.Location = new System.Drawing.Point(14, 237);
            this.labelSourcePhone.Name = "labelSourcePhone";
            this.labelSourcePhone.Size = new System.Drawing.Size(92, 16);
            this.labelSourcePhone.TabIndex = 27;
            this.labelSourcePhone.Text = "<shop phone>";
            // 
            // labeldestphone
            // 
            this.labeldestphone.AutoSize = true;
            this.labeldestphone.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldestphone.Location = new System.Drawing.Point(487, 232);
            this.labeldestphone.Name = "labeldestphone";
            this.labeldestphone.Size = new System.Drawing.Size(92, 16);
            this.labeldestphone.TabIndex = 32;
            this.labeldestphone.Text = "<shop phone>";
            // 
            // labeldestshopaddr2
            // 
            this.labeldestshopaddr2.AutoSize = true;
            this.labeldestshopaddr2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldestshopaddr2.Location = new System.Drawing.Point(487, 214);
            this.labeldestshopaddr2.Name = "labeldestshopaddr2";
            this.labeldestshopaddr2.Size = new System.Drawing.Size(257, 16);
            this.labeldestshopaddr2.TabIndex = 31;
            this.labeldestshopaddr2.Text = "<shop city>,<shop state> <shop zip code>";
            // 
            // labeldestshopaddr
            // 
            this.labeldestshopaddr.AutoSize = true;
            this.labeldestshopaddr.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldestshopaddr.Location = new System.Drawing.Point(487, 196);
            this.labeldestshopaddr.Name = "labeldestshopaddr";
            this.labeldestshopaddr.Size = new System.Drawing.Size(102, 16);
            this.labeldestshopaddr.TabIndex = 30;
            this.labeldestshopaddr.Text = "<shop address>";
            // 
            // labeldestshopname
            // 
            this.labeldestshopname.AutoSize = true;
            this.labeldestshopname.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldestshopname.Location = new System.Drawing.Point(485, 179);
            this.labeldestshopname.Name = "labeldestshopname";
            this.labeldestshopname.Size = new System.Drawing.Size(89, 16);
            this.labeldestshopname.TabIndex = 29;
            this.labeldestshopname.Text = "<shop name>";
            // 
            // labeldestshopno
            // 
            this.labeldestshopno.AutoSize = true;
            this.labeldestshopno.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldestshopno.Location = new System.Drawing.Point(486, 163);
            this.labeldestshopno.Name = "labeldestshopno";
            this.labeldestshopno.Size = new System.Drawing.Size(101, 16);
            this.labeldestshopno.TabIndex = 28;
            this.labeldestshopno.Text = "<shop number>";
            // 
            // labelempnameheading
            // 
            this.labelempnameheading.AutoSize = true;
            this.labelempnameheading.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelempnameheading.Location = new System.Drawing.Point(14, 294);
            this.labelempnameheading.Name = "labelempnameheading";
            this.labelempnameheading.Size = new System.Drawing.Size(78, 14);
            this.labelempnameheading.TabIndex = 36;
            this.labelempnameheading.Text = "Emp. Name:";
            // 
            // labelcommentheading
            // 
            this.labelcommentheading.AutoSize = true;
            this.labelcommentheading.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelcommentheading.Location = new System.Drawing.Point(14, 320);
            this.labelcommentheading.Name = "labelcommentheading";
            this.labelcommentheading.Size = new System.Drawing.Size(70, 14);
            this.labelcommentheading.TabIndex = 37;
            this.labelcommentheading.Text = "Comment:";
            // 
            // labelempname
            // 
            this.labelempname.AutoSize = true;
            this.labelempname.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelempname.Location = new System.Drawing.Point(108, 294);
            this.labelempname.Name = "labelempname";
            this.labelempname.Size = new System.Drawing.Size(191, 14);
            this.labelempname.TabIndex = 38;
            this.labelempname.Text = "<Emp Name> <emp number>";
            // 
            // labelsourcecomment
            // 
            this.labelsourcecomment.AutoSize = true;
            this.labelsourcecomment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelsourcecomment.Location = new System.Drawing.Point(108, 320);
            this.labelsourcecomment.Name = "labelsourcecomment";
            this.labelsourcecomment.Size = new System.Drawing.Size(128, 14);
            this.labelsourcecomment.TabIndex = 39;
            this.labelsourcecomment.Text = "<source comment>";
            // 
            // labeldestComment
            // 
            this.labeldestComment.AutoSize = true;
            this.labeldestComment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldestComment.Location = new System.Drawing.Point(579, 320);
            this.labeldestComment.Name = "labeldestComment";
            this.labeldestComment.Size = new System.Drawing.Size(158, 14);
            this.labeldestComment.TabIndex = 45;
            this.labeldestComment.Text = "<destination comment>";
            // 
            // labelDestempName
            // 
            this.labelDestempName.AutoSize = true;
            this.labelDestempName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDestempName.Location = new System.Drawing.Point(579, 294);
            this.labelDestempName.Name = "labelDestempName";
            this.labelDestempName.Size = new System.Drawing.Size(191, 14);
            this.labelDestempName.TabIndex = 44;
            this.labelDestempName.Text = "<Emp Name> <emp number>";
            // 
            // labeldestcommentheading
            // 
            this.labeldestcommentheading.AutoSize = true;
            this.labeldestcommentheading.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldestcommentheading.Location = new System.Drawing.Point(485, 320);
            this.labeldestcommentheading.Name = "labeldestcommentheading";
            this.labeldestcommentheading.Size = new System.Drawing.Size(70, 14);
            this.labeldestcommentheading.TabIndex = 43;
            this.labeldestcommentheading.Text = "Comment:";
            // 
            // labeldestempnameheading
            // 
            this.labeldestempnameheading.AutoSize = true;
            this.labeldestempnameheading.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeldestempnameheading.Location = new System.Drawing.Point(485, 294);
            this.labeldestempnameheading.Name = "labeldestempnameheading";
            this.labeldestempnameheading.Size = new System.Drawing.Size(78, 14);
            this.labeldestempnameheading.TabIndex = 42;
            this.labeldestempnameheading.Text = "Emp. Name:";
            // 
            // labelAmtTransferredHeading
            // 
            this.labelAmtTransferredHeading.AutoSize = true;
            this.labelAmtTransferredHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmtTransferredHeading.Location = new System.Drawing.Point(245, 377);
            this.labelAmtTransferredHeading.Name = "labelAmtTransferredHeading";
            this.labelAmtTransferredHeading.Size = new System.Drawing.Size(209, 19);
            this.labelAmtTransferredHeading.TabIndex = 46;
            this.labelAmtTransferredHeading.Text = "AMOUNT TRANSFERRED";
            // 
            // labelTransferAmount
            // 
            this.labelTransferAmount.AutoSize = true;
            this.labelTransferAmount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransferAmount.Location = new System.Drawing.Point(464, 381);
            this.labelTransferAmount.Name = "labelTransferAmount";
            this.labelTransferAmount.Size = new System.Drawing.Size(148, 14);
            this.labelTransferAmount.TabIndex = 47;
            this.labelTransferAmount.Text = "<amount transferred>";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(15, 464);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 2);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // label30
            // 
            this.label30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(13, 469);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(65, 13);
            this.label30.TabIndex = 48;
            this.label30.Text = "Signature:";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(599, 464);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 2);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(597, 469);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 50;
            this.label3.Text = "Carrier Signature:";
            // 
            // cashTransferHeader1
            // 
            this.cashTransferHeader1.BackColor = System.Drawing.Color.Transparent;
            this.cashTransferHeader1.Location = new System.Drawing.Point(1, 1);
            this.cashTransferHeader1.Name = "cashTransferHeader1";
            this.cashTransferHeader1.Size = new System.Drawing.Size(817, 115);
            this.cashTransferHeader1.TabIndex = 52;
            // 
            // ShopToShopCashTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(876, 492);
            this.Controls.Add(this.cashTransferHeader1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.labelTransferAmount);
            this.Controls.Add(this.labelAmtTransferredHeading);
            this.Controls.Add(this.labeldestComment);
            this.Controls.Add(this.labelDestempName);
            this.Controls.Add(this.labeldestcommentheading);
            this.Controls.Add(this.labeldestempnameheading);
            this.Controls.Add(this.labelsourcecomment);
            this.Controls.Add(this.labelempname);
            this.Controls.Add(this.labelcommentheading);
            this.Controls.Add(this.labelempnameheading);
            this.Controls.Add(this.labeldestphone);
            this.Controls.Add(this.labeldestshopaddr2);
            this.Controls.Add(this.labeldestshopaddr);
            this.Controls.Add(this.labeldestshopname);
            this.Controls.Add(this.labeldestshopno);
            this.Controls.Add(this.labelSourcePhone);
            this.Controls.Add(this.labelSourceShopAddr2);
            this.Controls.Add(this.labelSourceShopAddress);
            this.Controls.Add(this.labelSourceShopName);
            this.Controls.Add(this.labelSourceShopNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelSourceHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShopToShopCashTransfer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShopToShopCashTransfer";
            this.Load += new System.EventHandler(this.ShopToShopCashTransfer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSourceHeading;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSourceShopNo;
        private System.Windows.Forms.Label labelSourceShopName;
        private System.Windows.Forms.Label labelSourceShopAddress;
        private System.Windows.Forms.Label labelSourceShopAddr2;
        private System.Windows.Forms.Label labelSourcePhone;
        private System.Windows.Forms.Label labeldestphone;
        private System.Windows.Forms.Label labeldestshopaddr2;
        private System.Windows.Forms.Label labeldestshopaddr;
        private System.Windows.Forms.Label labeldestshopname;
        private System.Windows.Forms.Label labeldestshopno;
        private System.Windows.Forms.Label labelempnameheading;
        private System.Windows.Forms.Label labelcommentheading;
        private System.Windows.Forms.Label labelempname;
        private System.Windows.Forms.Label labelsourcecomment;
        private System.Windows.Forms.Label labeldestComment;
        private System.Windows.Forms.Label labelDestempName;
        private System.Windows.Forms.Label labeldestcommentheading;
        private System.Windows.Forms.Label labeldestempnameheading;
        private System.Windows.Forms.Label labelAmtTransferredHeading;
        private System.Windows.Forms.Label labelTransferAmount;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private CashTransferHeader cashTransferHeader1;
    }
}