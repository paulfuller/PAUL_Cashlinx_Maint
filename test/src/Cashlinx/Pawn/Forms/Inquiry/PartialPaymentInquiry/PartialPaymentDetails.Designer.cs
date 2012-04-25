using System.Windows.Forms;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;

namespace  Pawn.Forms.Inquiry.PartialPaymentInquiry
{
    partial class PartialPaymentDetails
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
            System.Windows.Forms.Label label27;
            System.Windows.Forms.Label label24;
            System.Windows.Forms.Label label23;
            System.Windows.Forms.Label label22;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label42;
            System.Windows.Forms.Label label14;
            System.Windows.Forms.Label label15;
            System.Windows.Forms.Label label16;
            System.Windows.Forms.Label label17;
            System.Windows.Forms.Label label18;
            System.Windows.Forms.Label label19;
            System.Windows.Forms.Label label20;
            System.Windows.Forms.Label label28;
            System.Windows.Forms.Label label35;
            System.Windows.Forms.Label label36;
            System.Windows.Forms.Label label37;
            System.Windows.Forms.Label label38;
            System.Windows.Forms.Label label39;
            System.Windows.Forms.Label label40;
            System.Windows.Forms.Label label43;
            System.Windows.Forms.Label label44;
            System.Windows.Forms.Label label45;
            System.Windows.Forms.Label label46;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartialPaymentDetails));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCustomerEyes = new System.Windows.Forms.Label();
            this.lblCustomerHair = new System.Windows.Forms.Label();
            this.lblCustomerHeight = new System.Windows.Forms.Label();
            this.lblCustomerRace = new System.Windows.Forms.Label();
            this.lblCustomerSuppId = new System.Windows.Forms.Label();
            this.lblCustomerSex = new System.Windows.Forms.Label();
            this.lblCustomerWeight = new System.Windows.Forms.Label();
            this.lblCAIEmpNum = new System.Windows.Forms.Label();
            this.lblCustomerDOB = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblCustomerSSN = new System.Windows.Forms.Label();
            this.lblCustomerID = new System.Windows.Forms.Label();
            this.lblCustomerPhone = new System.Windows.Forms.Label();
            this.lblCustomerZip = new System.Windows.Forms.Label();
            this.lblCustomerState = new System.Windows.Forms.Label();
            this.lblCustomerCity = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCustomerAddress = new System.Windows.Forms.Label();
            this.lblCustomerNumber = new System.Windows.Forms.Label();
            this.lblPartialPaymentPaymentMethod = new System.Windows.Forms.Label();
            this.lblPartialPaymentStatus = new System.Windows.Forms.Label();
            this.lblPartialPaymentPaymentDate = new System.Windows.Forms.Label();
            this.lblPartialPaymentTotalPayment = new System.Windows.Forms.Label();
            this.lblPartialPaymentLateFee = new System.Windows.Forms.Label();
            this.lblPartialPaymentServiceCharge = new System.Windows.Forms.Label();
            this.lblPartialPaymentInterest = new System.Windows.Forms.Label();
            this.lblPartialPaymentPrincipalReduction = new System.Windows.Forms.Label();
            this.windowHeading_lb = new System.Windows.Forms.Label();
            this.pageInd = new Common.Libraries.Forms.Components.CustomLabel();
            this.prevPage = new Common.Libraries.Forms.Components.CustomButtonTiny();
            this.ItemsList_dg = new Common.Libraries.Forms.Components.CustomDataGridView();
            this.firstPage = new Common.Libraries.Forms.Components.CustomButtonTiny();
            this.lastPage = new Common.Libraries.Forms.Components.CustomButtonTiny();
            this.Refine_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.nextPage = new Common.Libraries.Forms.Components.CustomButtonTiny();
            this.Print_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.Cancel_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.Back_btn = new Common.Libraries.Forms.Components.CustomButton();
            this.TICKET_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATE_MADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PFI_ELIG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOAN_STATUS_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATE_DUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOAN_AMOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CURRENT_PRIN_AMOUNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            label27 = new System.Windows.Forms.Label();
            label24 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label42 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            label28 = new System.Windows.Forms.Label();
            label35 = new System.Windows.Forms.Label();
            label36 = new System.Windows.Forms.Label();
            label37 = new System.Windows.Forms.Label();
            label38 = new System.Windows.Forms.Label();
            label39 = new System.Windows.Forms.Label();
            label40 = new System.Windows.Forms.Label();
            label43 = new System.Windows.Forms.Label();
            label44 = new System.Windows.Forms.Label();
            label45 = new System.Windows.Forms.Label();
            label46 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsList_dg)).BeginInit();
            this.SuspendLayout();
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new System.Drawing.Point(195, 251);
            label27.Name = "label27";
            label27.Size = new System.Drawing.Size(78, 13);
            label27.TabIndex = 27;
            label27.Text = "Total Payment:";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new System.Drawing.Point(221, 232);
            label24.Name = "label24";
            label24.Size = new System.Drawing.Size(52, 13);
            label24.TabIndex = 24;
            label24.Text = "Late Fee:";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new System.Drawing.Point(190, 194);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(83, 13);
            label23.TabIndex = 23;
            label23.Text = "Service Charge:";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(228, 213);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(45, 13);
            label22.TabIndex = 22;
            label22.Text = "Interest:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(171, 176);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(102, 13);
            label7.TabIndex = 7;
            label7.Text = "Principal Reduction:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(457, 176);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(77, 13);
            label6.TabIndex = 88;
            label6.Text = "Payment Date:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(494, 194);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(40, 13);
            label8.TabIndex = 90;
            label8.Text = "Status:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(444, 213);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(90, 13);
            label10.TabIndex = 92;
            label10.Text = "Payment Method:";
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            label42.Location = new System.Drawing.Point(14, 161);
            label42.Name = "label42";
            label42.Size = new System.Drawing.Size(111, 13);
            label42.TabIndex = 94;
            label42.Text = "PARTIAL PAYMENT";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(100, 106);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(25, 13);
            label14.TabIndex = 100;
            label14.Text = "Zip:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(90, 87);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(35, 13);
            label15.TabIndex = 99;
            label15.Text = "State:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(98, 68);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(27, 13);
            label16.TabIndex = 98;
            label16.Text = "City:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(87, 30);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(38, 13);
            label17.TabIndex = 97;
            label17.Text = "Name:";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(77, 49);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(48, 13);
            label18.TabIndex = 96;
            label18.Text = "Address:";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new System.Drawing.Point(61, 12);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(64, 13);
            label19.TabIndex = 95;
            label19.Text = "Customer #:";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(84, 125);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(41, 13);
            label20.TabIndex = 107;
            label20.Text = "Phone:";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new System.Drawing.Point(609, 30);
            label28.Name = "label28";
            label28.Size = new System.Drawing.Size(28, 13);
            label28.TabIndex = 121;
            label28.Text = "Sex:";
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Location = new System.Drawing.Point(593, 12);
            label35.Name = "label35";
            label35.Size = new System.Drawing.Size(44, 13);
            label35.TabIndex = 114;
            label35.Text = "Weight:";
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Location = new System.Drawing.Point(473, 232);
            label36.Name = "label36";
            label36.Size = new System.Drawing.Size(61, 13);
            label36.TabIndex = 113;
            label36.Text = "CAI Emp #:";
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Location = new System.Drawing.Point(309, 68);
            label37.Name = "label37";
            label37.Size = new System.Drawing.Size(69, 13);
            label37.TabIndex = 112;
            label37.Text = "Date of Birth:";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Location = new System.Drawing.Point(329, 30);
            label38.Name = "label38";
            label38.Size = new System.Drawing.Size(49, 13);
            label38.TabIndex = 111;
            label38.Text = "Supp ID:";
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Location = new System.Drawing.Point(346, 49);
            label39.Name = "label39";
            label39.Size = new System.Drawing.Size(32, 13);
            label39.TabIndex = 110;
            label39.Text = "SSN:";
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Location = new System.Drawing.Point(357, 12);
            label40.Name = "label40";
            label40.Size = new System.Drawing.Size(21, 13);
            label40.TabIndex = 109;
            label40.Text = "ID:";
            // 
            // label43
            // 
            label43.AutoSize = true;
            label43.Location = new System.Drawing.Point(601, 49);
            label43.Name = "label43";
            label43.Size = new System.Drawing.Size(36, 13);
            label43.TabIndex = 124;
            label43.Text = "Race:";
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.Location = new System.Drawing.Point(596, 68);
            label44.Name = "label44";
            label44.Size = new System.Drawing.Size(41, 13);
            label44.TabIndex = 125;
            label44.Text = "Height:";
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Location = new System.Drawing.Point(608, 87);
            label45.Name = "label45";
            label45.Size = new System.Drawing.Size(29, 13);
            label45.TabIndex = 126;
            label45.Text = "Hair:";
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.Location = new System.Drawing.Point(604, 106);
            label46.Name = "label46";
            label46.Size = new System.Drawing.Size(33, 13);
            label46.TabIndex = 127;
            label46.Text = "Eyes:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Gray;
            this.groupBox2.Location = new System.Drawing.Point(17, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(732, 3);
            this.groupBox2.TabIndex = 87;
            this.groupBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblCustomerEyes);
            this.panel1.Controls.Add(this.lblCustomerHair);
            this.panel1.Controls.Add(this.lblCustomerHeight);
            this.panel1.Controls.Add(this.lblCustomerRace);
            this.panel1.Controls.Add(label46);
            this.panel1.Controls.Add(label45);
            this.panel1.Controls.Add(label44);
            this.panel1.Controls.Add(label43);
            this.panel1.Controls.Add(this.lblCustomerSuppId);
            this.panel1.Controls.Add(this.lblCustomerSex);
            this.panel1.Controls.Add(label28);
            this.panel1.Controls.Add(this.lblCustomerWeight);
            this.panel1.Controls.Add(this.lblCAIEmpNum);
            this.panel1.Controls.Add(this.lblCustomerDOB);
            this.panel1.Controls.Add(this.label32);
            this.panel1.Controls.Add(this.lblCustomerSSN);
            this.panel1.Controls.Add(this.lblCustomerID);
            this.panel1.Controls.Add(label35);
            this.panel1.Controls.Add(label36);
            this.panel1.Controls.Add(label37);
            this.panel1.Controls.Add(label38);
            this.panel1.Controls.Add(label39);
            this.panel1.Controls.Add(label40);
            this.panel1.Controls.Add(this.lblCustomerPhone);
            this.panel1.Controls.Add(label20);
            this.panel1.Controls.Add(this.lblCustomerZip);
            this.panel1.Controls.Add(this.lblCustomerState);
            this.panel1.Controls.Add(this.lblCustomerCity);
            this.panel1.Controls.Add(this.lblCustomerName);
            this.panel1.Controls.Add(this.lblCustomerAddress);
            this.panel1.Controls.Add(this.lblCustomerNumber);
            this.panel1.Controls.Add(label14);
            this.panel1.Controls.Add(label15);
            this.panel1.Controls.Add(label16);
            this.panel1.Controls.Add(label17);
            this.panel1.Controls.Add(label18);
            this.panel1.Controls.Add(label19);
            this.panel1.Controls.Add(label42);
            this.panel1.Controls.Add(this.lblPartialPaymentPaymentMethod);
            this.panel1.Controls.Add(label10);
            this.panel1.Controls.Add(this.lblPartialPaymentStatus);
            this.panel1.Controls.Add(label8);
            this.panel1.Controls.Add(this.lblPartialPaymentPaymentDate);
            this.panel1.Controls.Add(label6);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.lblPartialPaymentTotalPayment);
            this.panel1.Controls.Add(this.lblPartialPaymentLateFee);
            this.panel1.Controls.Add(this.lblPartialPaymentServiceCharge);
            this.panel1.Controls.Add(this.lblPartialPaymentInterest);
            this.panel1.Controls.Add(this.lblPartialPaymentPrincipalReduction);
            this.panel1.Controls.Add(label27);
            this.panel1.Controls.Add(label24);
            this.panel1.Controls.Add(label23);
            this.panel1.Controls.Add(label22);
            this.panel1.Controls.Add(label7);
            this.panel1.Location = new System.Drawing.Point(13, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 290);
            this.panel1.TabIndex = 42;
            // 
            // lblCustomerEyes
            // 
            this.lblCustomerEyes.AutoSize = true;
            this.lblCustomerEyes.Location = new System.Drawing.Point(643, 106);
            this.lblCustomerEyes.Name = "lblCustomerEyes";
            this.lblCustomerEyes.Size = new System.Drawing.Size(20, 13);
            this.lblCustomerEyes.TabIndex = 131;
            this.lblCustomerEyes.Text = "BL";
            // 
            // lblCustomerHair
            // 
            this.lblCustomerHair.AutoSize = true;
            this.lblCustomerHair.Location = new System.Drawing.Point(643, 87);
            this.lblCustomerHair.Name = "lblCustomerHair";
            this.lblCustomerHair.Size = new System.Drawing.Size(20, 13);
            this.lblCustomerHair.TabIndex = 130;
            this.lblCustomerHair.Text = "BL";
            // 
            // lblCustomerHeight
            // 
            this.lblCustomerHeight.AutoSize = true;
            this.lblCustomerHeight.Location = new System.Drawing.Point(643, 68);
            this.lblCustomerHeight.Name = "lblCustomerHeight";
            this.lblCustomerHeight.Size = new System.Drawing.Size(21, 13);
            this.lblCustomerHeight.TabIndex = 129;
            this.lblCustomerHeight.Text = "6\'0";
            // 
            // lblCustomerRace
            // 
            this.lblCustomerRace.AutoSize = true;
            this.lblCustomerRace.Location = new System.Drawing.Point(643, 49);
            this.lblCustomerRace.Name = "lblCustomerRace";
            this.lblCustomerRace.Size = new System.Drawing.Size(18, 13);
            this.lblCustomerRace.TabIndex = 128;
            this.lblCustomerRace.Text = "W";
            // 
            // lblCustomerSuppId
            // 
            this.lblCustomerSuppId.AutoSize = true;
            this.lblCustomerSuppId.Location = new System.Drawing.Point(384, 30);
            this.lblCustomerSuppId.Name = "lblCustomerSuppId";
            this.lblCustomerSuppId.Size = new System.Drawing.Size(102, 13);
            this.lblCustomerSuppId.TabIndex = 123;
            this.lblCustomerSuppId.Text = "CHL TX 777777777";
            // 
            // lblCustomerSex
            // 
            this.lblCustomerSex.AutoSize = true;
            this.lblCustomerSex.Location = new System.Drawing.Point(643, 30);
            this.lblCustomerSex.Name = "lblCustomerSex";
            this.lblCustomerSex.Size = new System.Drawing.Size(16, 13);
            this.lblCustomerSex.TabIndex = 122;
            this.lblCustomerSex.Text = "M";
            // 
            // lblCustomerWeight
            // 
            this.lblCustomerWeight.AutoSize = true;
            this.lblCustomerWeight.Location = new System.Drawing.Point(643, 11);
            this.lblCustomerWeight.Name = "lblCustomerWeight";
            this.lblCustomerWeight.Size = new System.Drawing.Size(25, 13);
            this.lblCustomerWeight.TabIndex = 120;
            this.lblCustomerWeight.Text = "200";
            // 
            // lblCAIEmpNum
            // 
            this.lblCAIEmpNum.AutoSize = true;
            this.lblCAIEmpNum.Location = new System.Drawing.Point(540, 232);
            this.lblCAIEmpNum.Name = "lblCAIEmpNum";
            this.lblCAIEmpNum.Size = new System.Drawing.Size(107, 13);
            this.lblCAIEmpNum.TabIndex = 119;
            this.lblCAIEmpNum.Text = "emp num 987654321";
            // 
            // lblCustomerDOB
            // 
            this.lblCustomerDOB.AutoSize = true;
            this.lblCustomerDOB.Location = new System.Drawing.Point(384, 68);
            this.lblCustomerDOB.Name = "lblCustomerDOB";
            this.lblCustomerDOB.Size = new System.Drawing.Size(65, 13);
            this.lblCustomerDOB.TabIndex = 118;
            this.lblCustomerDOB.Text = "01/01/1901";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(384, 30);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(0, 13);
            this.label32.TabIndex = 117;
            // 
            // lblCustomerSSN
            // 
            this.lblCustomerSSN.AutoSize = true;
            this.lblCustomerSSN.Location = new System.Drawing.Point(384, 49);
            this.lblCustomerSSN.Name = "lblCustomerSSN";
            this.lblCustomerSSN.Size = new System.Drawing.Size(72, 13);
            this.lblCustomerSSN.TabIndex = 116;
            this.lblCustomerSSN.Text = "XXX-XX-0000";
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.AutoSize = true;
            this.lblCustomerID.Location = new System.Drawing.Point(384, 12);
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.Size = new System.Drawing.Size(95, 13);
            this.lblCustomerID.TabIndex = 115;
            this.lblCustomerID.Text = "DL TX 111112222";
            // 
            // lblCustomerPhone
            // 
            this.lblCustomerPhone.AutoSize = true;
            this.lblCustomerPhone.Location = new System.Drawing.Point(131, 125);
            this.lblCustomerPhone.Name = "lblCustomerPhone";
            this.lblCustomerPhone.Size = new System.Drawing.Size(73, 13);
            this.lblCustomerPhone.TabIndex = 108;
            this.lblCustomerPhone.Text = "123-123-1234";
            // 
            // lblCustomerZip
            // 
            this.lblCustomerZip.AutoSize = true;
            this.lblCustomerZip.Location = new System.Drawing.Point(131, 106);
            this.lblCustomerZip.Name = "lblCustomerZip";
            this.lblCustomerZip.Size = new System.Drawing.Size(37, 13);
            this.lblCustomerZip.TabIndex = 106;
            this.lblCustomerZip.Text = "76012";
            // 
            // lblCustomerState
            // 
            this.lblCustomerState.AutoSize = true;
            this.lblCustomerState.Location = new System.Drawing.Point(131, 87);
            this.lblCustomerState.Name = "lblCustomerState";
            this.lblCustomerState.Size = new System.Drawing.Size(21, 13);
            this.lblCustomerState.TabIndex = 105;
            this.lblCustomerState.Text = "TX";
            // 
            // lblCustomerCity
            // 
            this.lblCustomerCity.AutoSize = true;
            this.lblCustomerCity.Location = new System.Drawing.Point(131, 68);
            this.lblCustomerCity.Name = "lblCustomerCity";
            this.lblCustomerCity.Size = new System.Drawing.Size(48, 13);
            this.lblCustomerCity.TabIndex = 104;
            this.lblCustomerCity.Text = "Arlington";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Location = new System.Drawing.Point(131, 30);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(81, 13);
            this.lblCustomerName.TabIndex = 103;
            this.lblCustomerName.Text = "Wages, John Q";
            // 
            // lblCustomerAddress
            // 
            this.lblCustomerAddress.AutoSize = true;
            this.lblCustomerAddress.Location = new System.Drawing.Point(131, 49);
            this.lblCustomerAddress.Name = "lblCustomerAddress";
            this.lblCustomerAddress.Size = new System.Drawing.Size(77, 13);
            this.lblCustomerAddress.TabIndex = 102;
            this.lblCustomerAddress.Text = "100 Any Street";
            // 
            // lblCustomerNumber
            // 
            this.lblCustomerNumber.AutoSize = true;
            this.lblCustomerNumber.Location = new System.Drawing.Point(131, 12);
            this.lblCustomerNumber.Name = "lblCustomerNumber";
            this.lblCustomerNumber.Size = new System.Drawing.Size(100, 13);
            this.lblCustomerNumber.TabIndex = 101;
            this.lblCustomerNumber.Text = "1-000000-085796-0";
            // 
            // lblPartialPaymentPaymentMethod
            // 
            this.lblPartialPaymentPaymentMethod.AutoSize = true;
            this.lblPartialPaymentPaymentMethod.Location = new System.Drawing.Point(540, 213);
            this.lblPartialPaymentPaymentMethod.Name = "lblPartialPaymentPaymentMethod";
            this.lblPartialPaymentPaymentMethod.Size = new System.Drawing.Size(36, 13);
            this.lblPartialPaymentPaymentMethod.TabIndex = 93;
            this.lblPartialPaymentPaymentMethod.Text = "CASH";
            // 
            // lblPartialPaymentStatus
            // 
            this.lblPartialPaymentStatus.AutoSize = true;
            this.lblPartialPaymentStatus.Location = new System.Drawing.Point(540, 194);
            this.lblPartialPaymentStatus.Name = "lblPartialPaymentStatus";
            this.lblPartialPaymentStatus.Size = new System.Drawing.Size(28, 13);
            this.lblPartialPaymentStatus.TabIndex = 91;
            this.lblPartialPaymentStatus.Text = "ACT";
            // 
            // lblPartialPaymentPaymentDate
            // 
            this.lblPartialPaymentPaymentDate.AutoSize = true;
            this.lblPartialPaymentPaymentDate.Location = new System.Drawing.Point(540, 176);
            this.lblPartialPaymentPaymentDate.Name = "lblPartialPaymentPaymentDate";
            this.lblPartialPaymentPaymentDate.Size = new System.Drawing.Size(65, 13);
            this.lblPartialPaymentPaymentDate.TabIndex = 89;
            this.lblPartialPaymentPaymentDate.Text = "01/01/1901";
            // 
            // lblPartialPaymentTotalPayment
            // 
            this.lblPartialPaymentTotalPayment.AutoSize = true;
            this.lblPartialPaymentTotalPayment.Location = new System.Drawing.Point(279, 251);
            this.lblPartialPaymentTotalPayment.Name = "lblPartialPaymentTotalPayment";
            this.lblPartialPaymentTotalPayment.Size = new System.Drawing.Size(43, 13);
            this.lblPartialPaymentTotalPayment.TabIndex = 57;
            this.lblPartialPaymentTotalPayment.Text = "$ 00.00";
            // 
            // lblPartialPaymentLateFee
            // 
            this.lblPartialPaymentLateFee.AutoSize = true;
            this.lblPartialPaymentLateFee.Location = new System.Drawing.Point(279, 232);
            this.lblPartialPaymentLateFee.Name = "lblPartialPaymentLateFee";
            this.lblPartialPaymentLateFee.Size = new System.Drawing.Size(43, 13);
            this.lblPartialPaymentLateFee.TabIndex = 54;
            this.lblPartialPaymentLateFee.Text = "$ 00.00";
            // 
            // lblPartialPaymentServiceCharge
            // 
            this.lblPartialPaymentServiceCharge.AutoSize = true;
            this.lblPartialPaymentServiceCharge.Location = new System.Drawing.Point(279, 194);
            this.lblPartialPaymentServiceCharge.Name = "lblPartialPaymentServiceCharge";
            this.lblPartialPaymentServiceCharge.Size = new System.Drawing.Size(43, 13);
            this.lblPartialPaymentServiceCharge.TabIndex = 53;
            this.lblPartialPaymentServiceCharge.Text = "$ 00.00";
            // 
            // lblPartialPaymentInterest
            // 
            this.lblPartialPaymentInterest.AutoSize = true;
            this.lblPartialPaymentInterest.Location = new System.Drawing.Point(279, 213);
            this.lblPartialPaymentInterest.Name = "lblPartialPaymentInterest";
            this.lblPartialPaymentInterest.Size = new System.Drawing.Size(45, 13);
            this.lblPartialPaymentInterest.TabIndex = 52;
            this.lblPartialPaymentInterest.Text = "00.00 %";
            // 
            // lblPartialPaymentPrincipalReduction
            // 
            this.lblPartialPaymentPrincipalReduction.AutoSize = true;
            this.lblPartialPaymentPrincipalReduction.Location = new System.Drawing.Point(279, 176);
            this.lblPartialPaymentPrincipalReduction.Name = "lblPartialPaymentPrincipalReduction";
            this.lblPartialPaymentPrincipalReduction.Size = new System.Drawing.Size(43, 13);
            this.lblPartialPaymentPrincipalReduction.TabIndex = 51;
            this.lblPartialPaymentPrincipalReduction.Text = "$ 00.00";
            // 
            // windowHeading_lb
            // 
            this.windowHeading_lb.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.windowHeading_lb.AutoSize = true;
            this.windowHeading_lb.BackColor = System.Drawing.Color.Transparent;
            this.windowHeading_lb.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowHeading_lb.ForeColor = System.Drawing.Color.White;
            this.windowHeading_lb.Location = new System.Drawing.Point(272, 41);
            this.windowHeading_lb.Name = "windowHeading_lb";
            this.windowHeading_lb.Size = new System.Drawing.Size(238, 19);
            this.windowHeading_lb.TabIndex = 40;
            this.windowHeading_lb.Text = "Partial Payment Inquiry - Details";
            this.windowHeading_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pageInd
            // 
            this.pageInd.AutoSize = true;
            this.pageInd.BackColor = System.Drawing.Color.Transparent;
            this.pageInd.Location = new System.Drawing.Point(546, 389);
            this.pageInd.Name = "pageInd";
            this.pageInd.Size = new System.Drawing.Size(78, 13);
            this.pageInd.TabIndex = 33;
            this.pageInd.Text = "Page {0} of {1}";
            // 
            // prevPage
            // 
            this.prevPage.BackColor = System.Drawing.Color.Transparent;
            this.prevPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("prevPage.BackgroundImage")));
            this.prevPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.prevPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.prevPage.Enabled = false;
            this.prevPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.prevPage.FlatAppearance.BorderSize = 0;
            this.prevPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.prevPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.prevPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prevPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevPage.ForeColor = System.Drawing.Color.White;
            this.prevPage.Location = new System.Drawing.Point(480, 380);
            this.prevPage.Margin = new System.Windows.Forms.Padding(0);
            this.prevPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.prevPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.prevPage.Name = "prevPage";
            this.prevPage.Size = new System.Drawing.Size(75, 35);
            this.prevPage.TabIndex = 32;
            this.prevPage.Text = "<   ";
            this.prevPage.UseVisualStyleBackColor = false;
            this.prevPage.Click += new System.EventHandler(this.prevPage_Click);
            // 
            // ItemsList_dg
            // 
            this.ItemsList_dg.AllowUserToAddRows = false;
            this.ItemsList_dg.AllowUserToDeleteRows = false;
            this.ItemsList_dg.AllowUserToResizeColumns = false;
            this.ItemsList_dg.AllowUserToResizeRows = false;
            this.ItemsList_dg.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.ItemsList_dg.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsList_dg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ItemsList_dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsList_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TICKET_NUMBER,
            this.DATE_MADE,
            this.PFI_ELIG,
            this.LOAN_STATUS_CD,
            this.DATE_DUE,
            this.LOAN_AMOUNT,
            this.CURRENT_PRIN_AMOUNT});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsList_dg.DefaultCellStyle = dataGridViewCellStyle9;
            this.ItemsList_dg.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ItemsList_dg.Enabled = false;
            this.ItemsList_dg.GridColor = System.Drawing.Color.LightGray;
            this.ItemsList_dg.Location = new System.Drawing.Point(22, 420);
            this.ItemsList_dg.Margin = new System.Windows.Forms.Padding(0);
            this.ItemsList_dg.MultiSelect = false;
            this.ItemsList_dg.Name = "ItemsList_dg";
            this.ItemsList_dg.ReadOnly = true;
            this.ItemsList_dg.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsList_dg.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.ItemsList_dg.RowHeadersVisible = false;
            this.ItemsList_dg.RowTemplate.Height = 100;
            this.ItemsList_dg.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ItemsList_dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ItemsList_dg.Size = new System.Drawing.Size(746, 121);
            this.ItemsList_dg.TabIndex = 41;
            this.ItemsList_dg.TabStop = false;
            // 
            // firstPage
            // 
            this.firstPage.BackColor = System.Drawing.Color.Transparent;
            this.firstPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("firstPage.BackgroundImage")));
            this.firstPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.firstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.firstPage.Enabled = false;
            this.firstPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.firstPage.FlatAppearance.BorderSize = 0;
            this.firstPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.firstPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.firstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.firstPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstPage.ForeColor = System.Drawing.Color.White;
            this.firstPage.Location = new System.Drawing.Point(422, 380);
            this.firstPage.Margin = new System.Windows.Forms.Padding(0);
            this.firstPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.firstPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.firstPage.Name = "firstPage";
            this.firstPage.Size = new System.Drawing.Size(75, 35);
            this.firstPage.TabIndex = 36;
            this.firstPage.Text = "<<   ";
            this.firstPage.UseVisualStyleBackColor = false;
            this.firstPage.Click += new System.EventHandler(this.firstPage_Click);
            // 
            // lastPage
            // 
            this.lastPage.BackColor = System.Drawing.Color.Transparent;
            this.lastPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lastPage.BackgroundImage")));
            this.lastPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.lastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lastPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.lastPage.FlatAppearance.BorderSize = 0;
            this.lastPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.lastPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.lastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lastPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastPage.ForeColor = System.Drawing.Color.White;
            this.lastPage.Location = new System.Drawing.Point(693, 380);
            this.lastPage.Margin = new System.Windows.Forms.Padding(0);
            this.lastPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.lastPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.lastPage.Name = "lastPage";
            this.lastPage.Size = new System.Drawing.Size(75, 35);
            this.lastPage.TabIndex = 35;
            this.lastPage.Text = ">>   ";
            this.lastPage.UseVisualStyleBackColor = false;
            this.lastPage.Click += new System.EventHandler(this.lastPage_Click);
            // 
            // Refine_btn
            // 
            this.Refine_btn.BackColor = System.Drawing.Color.Transparent;
            this.Refine_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Refine_btn.BackgroundImage")));
            this.Refine_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Refine_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Refine_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Refine_btn.FlatAppearance.BorderSize = 0;
            this.Refine_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Refine_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Refine_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Refine_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Refine_btn.ForeColor = System.Drawing.Color.White;
            this.Refine_btn.Location = new System.Drawing.Point(116, 551);
            this.Refine_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Refine_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Refine_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Refine_btn.Name = "Refine_btn";
            this.Refine_btn.Size = new System.Drawing.Size(100, 50);
            this.Refine_btn.TabIndex = 39;
            this.Refine_btn.Text = "Refine Search";
            this.Refine_btn.UseVisualStyleBackColor = false;
            this.Refine_btn.Click += new System.EventHandler(this.Refine_btn_Click);
            // 
            // nextPage
            // 
            this.nextPage.BackColor = System.Drawing.Color.Transparent;
            this.nextPage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextPage.BackgroundImage")));
            this.nextPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextPage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.nextPage.FlatAppearance.BorderSize = 0;
            this.nextPage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.nextPage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.nextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextPage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextPage.ForeColor = System.Drawing.Color.White;
            this.nextPage.Location = new System.Drawing.Point(630, 380);
            this.nextPage.Margin = new System.Windows.Forms.Padding(0);
            this.nextPage.MaximumSize = new System.Drawing.Size(75, 35);
            this.nextPage.MinimumSize = new System.Drawing.Size(75, 35);
            this.nextPage.Name = "nextPage";
            this.nextPage.Size = new System.Drawing.Size(75, 35);
            this.nextPage.TabIndex = 31;
            this.nextPage.Text = ">   ";
            this.nextPage.UseVisualStyleBackColor = false;
            this.nextPage.Click += new System.EventHandler(this.nextPage_Click);
            // 
            // Print_btn
            // 
            this.Print_btn.BackColor = System.Drawing.Color.Transparent;
            this.Print_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Print_btn.BackgroundImage")));
            this.Print_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Print_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Print_btn.FlatAppearance.BorderSize = 0;
            this.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Print_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Print_btn.ForeColor = System.Drawing.Color.White;
            this.Print_btn.Location = new System.Drawing.Point(562, 551);
            this.Print_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Print_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Print_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Print_btn.Name = "Print_btn";
            this.Print_btn.Size = new System.Drawing.Size(100, 50);
            this.Print_btn.TabIndex = 38;
            this.Print_btn.Text = "Print";
            this.Print_btn.UseVisualStyleBackColor = false;
            this.Print_btn.Click += new System.EventHandler(this.Print_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.BackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cancel_btn.BackgroundImage")));
            this.Cancel_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Cancel_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Cancel_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Cancel_btn.FlatAppearance.BorderSize = 0;
            this.Cancel_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Cancel_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancel_btn.ForeColor = System.Drawing.Color.White;
            this.Cancel_btn.Location = new System.Drawing.Point(662, 551);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Cancel_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.Cancel_btn.TabIndex = 37;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // Back_btn
            // 
            this.Back_btn.BackColor = System.Drawing.Color.Transparent;
            this.Back_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Back_btn.BackgroundImage")));
            this.Back_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Back_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Back_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Back_btn.FlatAppearance.BorderSize = 0;
            this.Back_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Back_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Back_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back_btn.ForeColor = System.Drawing.Color.White;
            this.Back_btn.Location = new System.Drawing.Point(16, 551);
            this.Back_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Back_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Back_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(100, 50);
            this.Back_btn.TabIndex = 34;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = false;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // TICKET_NUMBER
            // 
            this.TICKET_NUMBER.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TICKET_NUMBER.DataPropertyName = "TICKET_NUMBER";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = null;
            this.TICKET_NUMBER.DefaultCellStyle = dataGridViewCellStyle2;
            this.TICKET_NUMBER.HeaderText = "Loan Number";
            this.TICKET_NUMBER.Name = "TICKET_NUMBER";
            this.TICKET_NUMBER.ReadOnly = true;
            this.TICKET_NUMBER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TICKET_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TICKET_NUMBER.Width = 110;
            // 
            // DATE_MADE
            // 
            this.DATE_MADE.DataPropertyName = "DATE_MADE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DATE_MADE.DefaultCellStyle = dataGridViewCellStyle3;
            this.DATE_MADE.HeaderText = "Loan Date Made";
            this.DATE_MADE.Name = "DATE_MADE";
            this.DATE_MADE.ReadOnly = true;
            this.DATE_MADE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DATE_MADE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DATE_MADE.Width = 130;
            // 
            // PFI_ELIG
            // 
            this.PFI_ELIG.DataPropertyName = "PFI_ELIG";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = null;
            this.PFI_ELIG.DefaultCellStyle = dataGridViewCellStyle4;
            this.PFI_ELIG.HeaderText = "PFI Elgible";
            this.PFI_ELIG.Name = "PFI_ELIG";
            this.PFI_ELIG.ReadOnly = true;
            this.PFI_ELIG.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PFI_ELIG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LOAN_STATUS_CD
            // 
            this.LOAN_STATUS_CD.DataPropertyName = "LOAN_STATUS_CD";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = null;
            this.LOAN_STATUS_CD.DefaultCellStyle = dataGridViewCellStyle5;
            this.LOAN_STATUS_CD.HeaderText = "Status";
            this.LOAN_STATUS_CD.Name = "LOAN_STATUS_CD";
            this.LOAN_STATUS_CD.ReadOnly = true;
            this.LOAN_STATUS_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LOAN_STATUS_CD.Width = 50;
            // 
            // DATE_DUE
            // 
            this.DATE_DUE.DataPropertyName = "DATE_DUE";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.NullValue = null;
            this.DATE_DUE.DefaultCellStyle = dataGridViewCellStyle6;
            this.DATE_DUE.HeaderText = "Loan Due Date";
            this.DATE_DUE.Name = "DATE_DUE";
            this.DATE_DUE.ReadOnly = true;
            this.DATE_DUE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DATE_DUE.Width = 110;
            // 
            // LOAN_AMOUNT
            // 
            this.LOAN_AMOUNT.DataPropertyName = "LOAN_AMOUNT";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "C2";
            dataGridViewCellStyle7.NullValue = null;
            this.LOAN_AMOUNT.DefaultCellStyle = dataGridViewCellStyle7;
            this.LOAN_AMOUNT.HeaderText = "Loan Amount";
            this.LOAN_AMOUNT.Name = "LOAN_AMOUNT";
            this.LOAN_AMOUNT.ReadOnly = true;
            this.LOAN_AMOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LOAN_AMOUNT.Width = 110;
            // 
            // CURRENT_PRIN_AMOUNT
            // 
            this.CURRENT_PRIN_AMOUNT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CURRENT_PRIN_AMOUNT.DataPropertyName = "CURRENT_PRIN_AMOUNT";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Format = "C2";
            dataGridViewCellStyle8.NullValue = null;
            this.CURRENT_PRIN_AMOUNT.DefaultCellStyle = dataGridViewCellStyle8;
            this.CURRENT_PRIN_AMOUNT.HeaderText = "Current Principal Amount";
            this.CURRENT_PRIN_AMOUNT.Name = "CURRENT_PRIN_AMOUNT";
            this.CURRENT_PRIN_AMOUNT.ReadOnly = true;
            this.CURRENT_PRIN_AMOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PartialPaymentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(797, 620);
            this.ControlBox = false;
            this.Controls.Add(this.pageInd);
            this.Controls.Add(this.prevPage);
            this.Controls.Add(this.ItemsList_dg);
            this.Controls.Add(this.firstPage);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowHeading_lb);
            this.Controls.Add(this.lastPage);
            this.Controls.Add(this.Refine_btn);
            this.Controls.Add(this.nextPage);
            this.Controls.Add(this.Print_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.Back_btn);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PartialPaymentDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PartialPaymentDetails";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PartialPaymentDetails_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsList_dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private CustomDataGridView ItemsList_dg;
        private CustomLabel pageInd;
        private CustomButtonTiny firstPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblPartialPaymentTotalPayment;
        private System.Windows.Forms.Label lblPartialPaymentLateFee;
        private System.Windows.Forms.Label lblPartialPaymentServiceCharge;
        private System.Windows.Forms.Label lblPartialPaymentInterest;
        private System.Windows.Forms.Label lblPartialPaymentPrincipalReduction;
        private CustomButtonTiny prevPage;
        private System.Windows.Forms.Label windowHeading_lb;
        private CustomButtonTiny lastPage;
        private CustomButton Refine_btn;
        private CustomButtonTiny nextPage;
        private CustomButton Print_btn;
        private CustomButton Cancel_btn;
        private CustomButton Back_btn;
        private System.Windows.Forms.Label lblPartialPaymentPaymentMethod;
        private System.Windows.Forms.Label lblPartialPaymentStatus;
        private System.Windows.Forms.Label lblPartialPaymentPaymentDate;
        private System.Windows.Forms.Label lblCustomerEyes;
        private System.Windows.Forms.Label lblCustomerHair;
        private System.Windows.Forms.Label lblCustomerHeight;
        private System.Windows.Forms.Label lblCustomerRace;
        private System.Windows.Forms.Label lblCustomerSuppId;
        private System.Windows.Forms.Label lblCustomerSex;
        private System.Windows.Forms.Label lblCustomerWeight;
        private System.Windows.Forms.Label lblCAIEmpNum;
        private System.Windows.Forms.Label lblCustomerDOB;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label lblCustomerSSN;
        private System.Windows.Forms.Label lblCustomerID;
        private System.Windows.Forms.Label lblCustomerPhone;
        private System.Windows.Forms.Label lblCustomerZip;
        private System.Windows.Forms.Label lblCustomerState;
        private System.Windows.Forms.Label lblCustomerCity;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerAddress;
        private System.Windows.Forms.Label lblCustomerNumber;
        private DataGridViewTextBoxColumn TICKET_NUMBER;
        private DataGridViewTextBoxColumn DATE_MADE;
        private DataGridViewTextBoxColumn PFI_ELIG;
        private DataGridViewTextBoxColumn LOAN_STATUS_CD;
        private DataGridViewTextBoxColumn DATE_DUE;
        private DataGridViewTextBoxColumn LOAN_AMOUNT;
        private DataGridViewTextBoxColumn CURRENT_PRIN_AMOUNT;
    }
}