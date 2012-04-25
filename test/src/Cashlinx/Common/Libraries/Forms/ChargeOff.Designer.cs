using Common.Libraries.Forms.Components;

namespace Common.Libraries.Forms
{
    partial class ChargeOff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChargeOff));
            this.lblReason = new System.Windows.Forms.Label();
            this.ddlReason = new System.Windows.Forms.ComboBox();
            this.lblHeading = new System.Windows.Forms.Label();
            this.btnSubmit = new CustomButton();
            this.btnCancel = new CustomButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblReasonAsterisk = new System.Windows.Forms.Label();
            this.flowLayoutPanelLabels = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAuthorizedBy = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.lblPoliceCaseNumber = new System.Windows.Forms.Label();
            this.lblATFIncidentNumber = new System.Windows.Forms.Label();
            this.lblCharitableOrganization = new System.Windows.Forms.Label();
            this.lblCharitableOrganizationAddress = new System.Windows.Forms.Label();
            this.lblCharitableOrganizationCity = new System.Windows.Forms.Label();
            this.lblCharitableOrganizationState = new System.Windows.Forms.Label();
            this.lblCharitableOrganizationPostalCode = new System.Windows.Forms.Label();
            this.lblPawnTicketNumber = new System.Windows.Forms.Label();
            this.lblReplacedICN = new System.Windows.Forms.Label();
            this.lblMerchandiseDestroyed = new System.Windows.Forms.Label();
            this.flowLayoutPanelFields = new System.Windows.Forms.FlowLayoutPanel();
            this.txtAuthorizedBy = new System.Windows.Forms.TextBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtPoliceCaseNumber = new System.Windows.Forms.TextBox();
            this.txtATFIncidentNumber = new System.Windows.Forms.TextBox();
            this.txtCharitableOrganization = new System.Windows.Forms.TextBox();
            this.txtCharitableOrganizationAddress = new System.Windows.Forms.TextBox();
            this.txtCharitableOrganizationCity = new System.Windows.Forms.TextBox();
            this.txtCharitableOrganizationState = new System.Windows.Forms.TextBox();
            this.txtCharitableOrganizationPostalCode = new System.Windows.Forms.TextBox();
            this.txtPawnTicketNumber = new System.Windows.Forms.TextBox();
            this.txtReplacedICN = new System.Windows.Forms.TextBox();
            this.chkMerchandiseDestoyed = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelAsterisks = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAuthorizedByAsterisk = new System.Windows.Forms.Label();
            this.lblCommentAsterisk = new System.Windows.Forms.Label();
            this.lblPoliceCaseNumberAsterisk = new System.Windows.Forms.Label();
            this.lblATFIncidentNumberAsterisk = new System.Windows.Forms.Label();
            this.lblCharitableOrganizationAsterisk = new System.Windows.Forms.Label();
            this.lblCharitableOrganizationAddressAsterisk = new System.Windows.Forms.Label();
            this.lblCharitableOrganizationCityAsterisk = new System.Windows.Forms.Label();
            this.lblCharitableOrganizationStateAsterisk = new System.Windows.Forms.Label();
            this.lblCharitableOrganizationPostalCodeAsterisk = new System.Windows.Forms.Label();
            this.lblPawnTicketNumberAsterisk = new System.Windows.Forms.Label();
            this.lblReplacedICNAsterisk = new System.Windows.Forms.Label();
            this.lblMerchandiseDestroyedAsterisk = new System.Windows.Forms.Label();
            this.btnBack = new CustomButton();
            this.lblIcnText = new System.Windows.Forms.Label();
            this.lblICN = new System.Windows.Forms.Label();
            this.flowLayoutPanelAmount = new System.Windows.Forms.FlowLayoutPanel();
            this.lblChargeOffAmount = new System.Windows.Forms.Label();
            this.lblChargeOffAmountText = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanelLabels.SuspendLayout();
            this.flowLayoutPanelFields.SuspendLayout();
            this.flowLayoutPanelAsterisks.SuspendLayout();
            this.flowLayoutPanelAmount.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblReason
            // 
            this.lblReason.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblReason.AutoSize = true;
            this.lblReason.BackColor = System.Drawing.Color.Transparent;
            this.lblReason.Location = new System.Drawing.Point(178, 8);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(50, 13);
            this.lblReason.TabIndex = 145;
            this.lblReason.Text = "Reason: ";
            // 
            // ddlReason
            // 
            this.ddlReason.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ddlReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlReason.FormattingEnabled = true;
            this.ddlReason.Location = new System.Drawing.Point(254, 4);
            this.ddlReason.Name = "ddlReason";
            this.ddlReason.Size = new System.Drawing.Size(225, 21);
            this.ddlReason.TabIndex = 144;
            this.ddlReason.SelectedIndexChanged += new System.EventHandler(this.ddlReason_SelectedIndexChanged);
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.BackColor = System.Drawing.Color.Transparent;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.White;
            this.lblHeading.Location = new System.Drawing.Point(191, 11);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(127, 16);
            this.lblHeading.TabIndex = 143;
            this.lblHeading.Text = "Inventory Charge Off";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.AutoSize = true;
            this.btnSubmit.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSubmit.BackgroundImage")));
            this.btnSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(399, 479);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 50);
            this.btnSubmit.TabIndex = 142;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.AutoSize = true;
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
            this.btnCancel.Location = new System.Drawing.Point(9, 479);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 50);
            this.btnCancel.TabIndex = 141;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblReason, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ddlReason, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblReasonAsterisk, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanelLabels, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanelFields, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanelAsterisks, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 172);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 297);
            this.tableLayoutPanel1.TabIndex = 146;
            // 
            // lblReasonAsterisk
            // 
            this.lblReasonAsterisk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReasonAsterisk.AutoSize = true;
            this.lblReasonAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblReasonAsterisk.Location = new System.Drawing.Point(234, 8);
            this.lblReasonAsterisk.Name = "lblReasonAsterisk";
            this.lblReasonAsterisk.Size = new System.Drawing.Size(13, 13);
            this.lblReasonAsterisk.TabIndex = 152;
            this.lblReasonAsterisk.Text = "*";
            // 
            // flowLayoutPanelLabels
            // 
            this.flowLayoutPanelLabels.Controls.Add(this.lblAuthorizedBy);
            this.flowLayoutPanelLabels.Controls.Add(this.lblComment);
            this.flowLayoutPanelLabels.Controls.Add(this.lblPoliceCaseNumber);
            this.flowLayoutPanelLabels.Controls.Add(this.lblATFIncidentNumber);
            this.flowLayoutPanelLabels.Controls.Add(this.lblCharitableOrganization);
            this.flowLayoutPanelLabels.Controls.Add(this.lblCharitableOrganizationAddress);
            this.flowLayoutPanelLabels.Controls.Add(this.lblCharitableOrganizationCity);
            this.flowLayoutPanelLabels.Controls.Add(this.lblCharitableOrganizationState);
            this.flowLayoutPanelLabels.Controls.Add(this.lblCharitableOrganizationPostalCode);
            this.flowLayoutPanelLabels.Controls.Add(this.lblPawnTicketNumber);
            this.flowLayoutPanelLabels.Controls.Add(this.lblReplacedICN);
            this.flowLayoutPanelLabels.Controls.Add(this.lblMerchandiseDestroyed);
            this.flowLayoutPanelLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelLabels.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelLabels.Location = new System.Drawing.Point(3, 33);
            this.flowLayoutPanelLabels.Name = "flowLayoutPanelLabels";
            this.flowLayoutPanelLabels.Size = new System.Drawing.Size(225, 261);
            this.flowLayoutPanelLabels.TabIndex = 153;
            // 
            // lblAuthorizedBy
            // 
            this.lblAuthorizedBy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAuthorizedBy.Location = new System.Drawing.Point(3, 3);
            this.lblAuthorizedBy.Margin = new System.Windows.Forms.Padding(3);
            this.lblAuthorizedBy.Name = "lblAuthorizedBy";
            this.lblAuthorizedBy.Size = new System.Drawing.Size(222, 21);
            this.lblAuthorizedBy.TabIndex = 146;
            this.lblAuthorizedBy.Text = "Authorized By:";
            this.lblAuthorizedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblComment
            // 
            this.lblComment.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblComment.Location = new System.Drawing.Point(11, 30);
            this.lblComment.Margin = new System.Windows.Forms.Padding(3);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(214, 83);
            this.lblComment.TabIndex = 147;
            this.lblComment.Text = "Comment:";
            this.lblComment.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPoliceCaseNumber
            // 
            this.lblPoliceCaseNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPoliceCaseNumber.Location = new System.Drawing.Point(3, 119);
            this.lblPoliceCaseNumber.Margin = new System.Windows.Forms.Padding(3);
            this.lblPoliceCaseNumber.Name = "lblPoliceCaseNumber";
            this.lblPoliceCaseNumber.Size = new System.Drawing.Size(222, 21);
            this.lblPoliceCaseNumber.TabIndex = 148;
            this.lblPoliceCaseNumber.Text = "Police Case #:";
            this.lblPoliceCaseNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblATFIncidentNumber
            // 
            this.lblATFIncidentNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblATFIncidentNumber.Location = new System.Drawing.Point(3, 146);
            this.lblATFIncidentNumber.Margin = new System.Windows.Forms.Padding(3);
            this.lblATFIncidentNumber.Name = "lblATFIncidentNumber";
            this.lblATFIncidentNumber.Size = new System.Drawing.Size(222, 21);
            this.lblATFIncidentNumber.TabIndex = 152;
            this.lblATFIncidentNumber.Text = "ATF Incident #:";
            this.lblATFIncidentNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCharitableOrganization
            // 
            this.lblCharitableOrganization.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCharitableOrganization.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganization.Location = new System.Drawing.Point(3, 173);
            this.lblCharitableOrganization.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganization.Name = "lblCharitableOrganization";
            this.lblCharitableOrganization.Size = new System.Drawing.Size(222, 21);
            this.lblCharitableOrganization.TabIndex = 153;
            this.lblCharitableOrganization.Text = "Charitable Organization:";
            this.lblCharitableOrganization.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCharitableOrganizationAddress
            // 
            this.lblCharitableOrganizationAddress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCharitableOrganizationAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganizationAddress.Location = new System.Drawing.Point(3, 200);
            this.lblCharitableOrganizationAddress.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganizationAddress.Name = "lblCharitableOrganizationAddress";
            this.lblCharitableOrganizationAddress.Size = new System.Drawing.Size(222, 21);
            this.lblCharitableOrganizationAddress.TabIndex = 154;
            this.lblCharitableOrganizationAddress.Text = "Charitable Organization Address:";
            this.lblCharitableOrganizationAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCharitableOrganizationCity
            // 
            this.lblCharitableOrganizationCity.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCharitableOrganizationCity.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganizationCity.Location = new System.Drawing.Point(3, 227);
            this.lblCharitableOrganizationCity.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganizationCity.Name = "lblCharitableOrganizationCity";
            this.lblCharitableOrganizationCity.Size = new System.Drawing.Size(222, 21);
            this.lblCharitableOrganizationCity.TabIndex = 155;
            this.lblCharitableOrganizationCity.Text = "Charitable Organization City:";
            this.lblCharitableOrganizationCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCharitableOrganizationState
            // 
            this.lblCharitableOrganizationState.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCharitableOrganizationState.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganizationState.Location = new System.Drawing.Point(231, 3);
            this.lblCharitableOrganizationState.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganizationState.Name = "lblCharitableOrganizationState";
            this.lblCharitableOrganizationState.Size = new System.Drawing.Size(222, 21);
            this.lblCharitableOrganizationState.TabIndex = 156;
            this.lblCharitableOrganizationState.Text = "Charitable Organization State:";
            this.lblCharitableOrganizationState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCharitableOrganizationPostalCode
            // 
            this.lblCharitableOrganizationPostalCode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCharitableOrganizationPostalCode.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganizationPostalCode.Location = new System.Drawing.Point(231, 30);
            this.lblCharitableOrganizationPostalCode.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganizationPostalCode.Name = "lblCharitableOrganizationPostalCode";
            this.lblCharitableOrganizationPostalCode.Size = new System.Drawing.Size(222, 21);
            this.lblCharitableOrganizationPostalCode.TabIndex = 157;
            this.lblCharitableOrganizationPostalCode.Text = "Charitable Organization Postal Code:";
            this.lblCharitableOrganizationPostalCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPawnTicketNumber
            // 
            this.lblPawnTicketNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPawnTicketNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblPawnTicketNumber.Location = new System.Drawing.Point(231, 57);
            this.lblPawnTicketNumber.Margin = new System.Windows.Forms.Padding(3);
            this.lblPawnTicketNumber.Name = "lblPawnTicketNumber";
            this.lblPawnTicketNumber.Size = new System.Drawing.Size(222, 21);
            this.lblPawnTicketNumber.TabIndex = 158;
            this.lblPawnTicketNumber.Text = "Pawn Ticket Number:";
            this.lblPawnTicketNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReplacedICN
            // 
            this.lblReplacedICN.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblReplacedICN.BackColor = System.Drawing.Color.Transparent;
            this.lblReplacedICN.Location = new System.Drawing.Point(231, 84);
            this.lblReplacedICN.Margin = new System.Windows.Forms.Padding(3);
            this.lblReplacedICN.Name = "lblReplacedICN";
            this.lblReplacedICN.Size = new System.Drawing.Size(222, 21);
            this.lblReplacedICN.TabIndex = 159;
            this.lblReplacedICN.Text = "Replaced ICN:";
            this.lblReplacedICN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMerchandiseDestroyed
            // 
            this.lblMerchandiseDestroyed.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblMerchandiseDestroyed.BackColor = System.Drawing.Color.Transparent;
            this.lblMerchandiseDestroyed.Location = new System.Drawing.Point(231, 111);
            this.lblMerchandiseDestroyed.Margin = new System.Windows.Forms.Padding(3);
            this.lblMerchandiseDestroyed.Name = "lblMerchandiseDestroyed";
            this.lblMerchandiseDestroyed.Size = new System.Drawing.Size(222, 21);
            this.lblMerchandiseDestroyed.TabIndex = 160;
            this.lblMerchandiseDestroyed.Text = "Merchandise Destroyed:";
            this.lblMerchandiseDestroyed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanelFields
            // 
            this.flowLayoutPanelFields.Controls.Add(this.txtAuthorizedBy);
            this.flowLayoutPanelFields.Controls.Add(this.txtComment);
            this.flowLayoutPanelFields.Controls.Add(this.txtPoliceCaseNumber);
            this.flowLayoutPanelFields.Controls.Add(this.txtATFIncidentNumber);
            this.flowLayoutPanelFields.Controls.Add(this.txtCharitableOrganization);
            this.flowLayoutPanelFields.Controls.Add(this.txtCharitableOrganizationAddress);
            this.flowLayoutPanelFields.Controls.Add(this.txtCharitableOrganizationCity);
            this.flowLayoutPanelFields.Controls.Add(this.txtCharitableOrganizationState);
            this.flowLayoutPanelFields.Controls.Add(this.txtCharitableOrganizationPostalCode);
            this.flowLayoutPanelFields.Controls.Add(this.txtPawnTicketNumber);
            this.flowLayoutPanelFields.Controls.Add(this.txtReplacedICN);
            this.flowLayoutPanelFields.Controls.Add(this.chkMerchandiseDestoyed);
            this.flowLayoutPanelFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelFields.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelFields.Location = new System.Drawing.Point(254, 33);
            this.flowLayoutPanelFields.Name = "flowLayoutPanelFields";
            this.flowLayoutPanelFields.Size = new System.Drawing.Size(225, 261);
            this.flowLayoutPanelFields.TabIndex = 154;
            // 
            // txtAuthorizedBy
            // 
            this.txtAuthorizedBy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAuthorizedBy.Location = new System.Drawing.Point(3, 3);
            this.txtAuthorizedBy.Name = "txtAuthorizedBy";
            this.txtAuthorizedBy.Size = new System.Drawing.Size(225, 21);
            this.txtAuthorizedBy.TabIndex = 149;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(3, 30);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(225, 83);
            this.txtComment.TabIndex = 150;
            // 
            // txtPoliceCaseNumber
            // 
            this.txtPoliceCaseNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPoliceCaseNumber.Location = new System.Drawing.Point(3, 119);
            this.txtPoliceCaseNumber.Name = "txtPoliceCaseNumber";
            this.txtPoliceCaseNumber.Size = new System.Drawing.Size(225, 21);
            this.txtPoliceCaseNumber.TabIndex = 151;
            // 
            // txtATFIncidentNumber
            // 
            this.txtATFIncidentNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtATFIncidentNumber.Location = new System.Drawing.Point(3, 146);
            this.txtATFIncidentNumber.Name = "txtATFIncidentNumber";
            this.txtATFIncidentNumber.Size = new System.Drawing.Size(225, 21);
            this.txtATFIncidentNumber.TabIndex = 152;
            // 
            // txtCharitableOrganization
            // 
            this.txtCharitableOrganization.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCharitableOrganization.Location = new System.Drawing.Point(3, 173);
            this.txtCharitableOrganization.Name = "txtCharitableOrganization";
            this.txtCharitableOrganization.Size = new System.Drawing.Size(225, 21);
            this.txtCharitableOrganization.TabIndex = 153;
            // 
            // txtCharitableOrganizationAddress
            // 
            this.txtCharitableOrganizationAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCharitableOrganizationAddress.Location = new System.Drawing.Point(3, 200);
            this.txtCharitableOrganizationAddress.Name = "txtCharitableOrganizationAddress";
            this.txtCharitableOrganizationAddress.Size = new System.Drawing.Size(225, 21);
            this.txtCharitableOrganizationAddress.TabIndex = 154;
            // 
            // txtCharitableOrganizationCity
            // 
            this.txtCharitableOrganizationCity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCharitableOrganizationCity.Location = new System.Drawing.Point(3, 227);
            this.txtCharitableOrganizationCity.Name = "txtCharitableOrganizationCity";
            this.txtCharitableOrganizationCity.Size = new System.Drawing.Size(225, 21);
            this.txtCharitableOrganizationCity.TabIndex = 155;
            // 
            // txtCharitableOrganizationState
            // 
            this.txtCharitableOrganizationState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCharitableOrganizationState.Location = new System.Drawing.Point(234, 3);
            this.txtCharitableOrganizationState.Name = "txtCharitableOrganizationState";
            this.txtCharitableOrganizationState.Size = new System.Drawing.Size(225, 21);
            this.txtCharitableOrganizationState.TabIndex = 156;
            // 
            // txtCharitableOrganizationPostalCode
            // 
            this.txtCharitableOrganizationPostalCode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCharitableOrganizationPostalCode.Location = new System.Drawing.Point(234, 30);
            this.txtCharitableOrganizationPostalCode.Name = "txtCharitableOrganizationPostalCode";
            this.txtCharitableOrganizationPostalCode.Size = new System.Drawing.Size(225, 21);
            this.txtCharitableOrganizationPostalCode.TabIndex = 157;
            // 
            // txtPawnTicketNumber
            // 
            this.txtPawnTicketNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPawnTicketNumber.Location = new System.Drawing.Point(234, 57);
            this.txtPawnTicketNumber.Name = "txtPawnTicketNumber";
            this.txtPawnTicketNumber.Size = new System.Drawing.Size(225, 21);
            this.txtPawnTicketNumber.TabIndex = 158;
            // 
            // txtReplacedICN
            // 
            this.txtReplacedICN.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtReplacedICN.Location = new System.Drawing.Point(234, 84);
            this.txtReplacedICN.Name = "txtReplacedICN";
            this.txtReplacedICN.Size = new System.Drawing.Size(225, 21);
            this.txtReplacedICN.TabIndex = 159;
            // 
            // chkMerchandiseDestoyed
            // 
            this.chkMerchandiseDestoyed.Location = new System.Drawing.Point(234, 111);
            this.chkMerchandiseDestoyed.Name = "chkMerchandiseDestoyed";
            this.chkMerchandiseDestoyed.Size = new System.Drawing.Size(104, 24);
            this.chkMerchandiseDestoyed.TabIndex = 160;
            // 
            // flowLayoutPanelAsterisks
            // 
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblAuthorizedByAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblCommentAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblPoliceCaseNumberAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblATFIncidentNumberAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblCharitableOrganizationAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblCharitableOrganizationAddressAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblCharitableOrganizationCityAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblCharitableOrganizationStateAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblCharitableOrganizationPostalCodeAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblPawnTicketNumberAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblReplacedICNAsterisk);
            this.flowLayoutPanelAsterisks.Controls.Add(this.lblMerchandiseDestroyedAsterisk);
            this.flowLayoutPanelAsterisks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelAsterisks.Location = new System.Drawing.Point(234, 33);
            this.flowLayoutPanelAsterisks.Name = "flowLayoutPanelAsterisks";
            this.flowLayoutPanelAsterisks.Size = new System.Drawing.Size(14, 261);
            this.flowLayoutPanelAsterisks.TabIndex = 155;
            // 
            // lblAuthorizedByAsterisk
            // 
            this.lblAuthorizedByAsterisk.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAuthorizedByAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblAuthorizedByAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblAuthorizedByAsterisk.Location = new System.Drawing.Point(3, 3);
            this.lblAuthorizedByAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblAuthorizedByAsterisk.Name = "lblAuthorizedByAsterisk";
            this.lblAuthorizedByAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblAuthorizedByAsterisk.TabIndex = 155;
            this.lblAuthorizedByAsterisk.Text = "*";
            this.lblAuthorizedByAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCommentAsterisk
            // 
            this.lblCommentAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblCommentAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblCommentAsterisk.Location = new System.Drawing.Point(3, 30);
            this.lblCommentAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblCommentAsterisk.Name = "lblCommentAsterisk";
            this.lblCommentAsterisk.Size = new System.Drawing.Size(11, 83);
            this.lblCommentAsterisk.TabIndex = 154;
            this.lblCommentAsterisk.Text = "*";
            this.lblCommentAsterisk.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPoliceCaseNumberAsterisk
            // 
            this.lblPoliceCaseNumberAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblPoliceCaseNumberAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblPoliceCaseNumberAsterisk.Location = new System.Drawing.Point(3, 119);
            this.lblPoliceCaseNumberAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblPoliceCaseNumberAsterisk.Name = "lblPoliceCaseNumberAsterisk";
            this.lblPoliceCaseNumberAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblPoliceCaseNumberAsterisk.TabIndex = 155;
            this.lblPoliceCaseNumberAsterisk.Text = "*";
            this.lblPoliceCaseNumberAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblATFIncidentNumberAsterisk
            // 
            this.lblATFIncidentNumberAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblATFIncidentNumberAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblATFIncidentNumberAsterisk.Location = new System.Drawing.Point(3, 146);
            this.lblATFIncidentNumberAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblATFIncidentNumberAsterisk.Name = "lblATFIncidentNumberAsterisk";
            this.lblATFIncidentNumberAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblATFIncidentNumberAsterisk.TabIndex = 156;
            this.lblATFIncidentNumberAsterisk.Text = "*";
            this.lblATFIncidentNumberAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCharitableOrganizationAsterisk
            // 
            this.lblCharitableOrganizationAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganizationAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblCharitableOrganizationAsterisk.Location = new System.Drawing.Point(3, 173);
            this.lblCharitableOrganizationAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganizationAsterisk.Name = "lblCharitableOrganizationAsterisk";
            this.lblCharitableOrganizationAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblCharitableOrganizationAsterisk.TabIndex = 157;
            this.lblCharitableOrganizationAsterisk.Text = "*";
            this.lblCharitableOrganizationAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCharitableOrganizationAddressAsterisk
            // 
            this.lblCharitableOrganizationAddressAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganizationAddressAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblCharitableOrganizationAddressAsterisk.Location = new System.Drawing.Point(3, 200);
            this.lblCharitableOrganizationAddressAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganizationAddressAsterisk.Name = "lblCharitableOrganizationAddressAsterisk";
            this.lblCharitableOrganizationAddressAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblCharitableOrganizationAddressAsterisk.TabIndex = 158;
            this.lblCharitableOrganizationAddressAsterisk.Text = "*";
            this.lblCharitableOrganizationAddressAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCharitableOrganizationCityAsterisk
            // 
            this.lblCharitableOrganizationCityAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganizationCityAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblCharitableOrganizationCityAsterisk.Location = new System.Drawing.Point(3, 227);
            this.lblCharitableOrganizationCityAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganizationCityAsterisk.Name = "lblCharitableOrganizationCityAsterisk";
            this.lblCharitableOrganizationCityAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblCharitableOrganizationCityAsterisk.TabIndex = 159;
            this.lblCharitableOrganizationCityAsterisk.Text = "*";
            this.lblCharitableOrganizationCityAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCharitableOrganizationStateAsterisk
            // 
            this.lblCharitableOrganizationStateAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganizationStateAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblCharitableOrganizationStateAsterisk.Location = new System.Drawing.Point(3, 254);
            this.lblCharitableOrganizationStateAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganizationStateAsterisk.Name = "lblCharitableOrganizationStateAsterisk";
            this.lblCharitableOrganizationStateAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblCharitableOrganizationStateAsterisk.TabIndex = 160;
            this.lblCharitableOrganizationStateAsterisk.Text = "*";
            this.lblCharitableOrganizationStateAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCharitableOrganizationPostalCodeAsterisk
            // 
            this.lblCharitableOrganizationPostalCodeAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblCharitableOrganizationPostalCodeAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblCharitableOrganizationPostalCodeAsterisk.Location = new System.Drawing.Point(3, 281);
            this.lblCharitableOrganizationPostalCodeAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblCharitableOrganizationPostalCodeAsterisk.Name = "lblCharitableOrganizationPostalCodeAsterisk";
            this.lblCharitableOrganizationPostalCodeAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblCharitableOrganizationPostalCodeAsterisk.TabIndex = 161;
            this.lblCharitableOrganizationPostalCodeAsterisk.Text = "*";
            this.lblCharitableOrganizationPostalCodeAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPawnTicketNumberAsterisk
            // 
            this.lblPawnTicketNumberAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblPawnTicketNumberAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblPawnTicketNumberAsterisk.Location = new System.Drawing.Point(3, 308);
            this.lblPawnTicketNumberAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblPawnTicketNumberAsterisk.Name = "lblPawnTicketNumberAsterisk";
            this.lblPawnTicketNumberAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblPawnTicketNumberAsterisk.TabIndex = 162;
            this.lblPawnTicketNumberAsterisk.Text = "*";
            this.lblPawnTicketNumberAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReplacedICNAsterisk
            // 
            this.lblReplacedICNAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblReplacedICNAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblReplacedICNAsterisk.Location = new System.Drawing.Point(3, 335);
            this.lblReplacedICNAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblReplacedICNAsterisk.Name = "lblReplacedICNAsterisk";
            this.lblReplacedICNAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblReplacedICNAsterisk.TabIndex = 163;
            this.lblReplacedICNAsterisk.Text = "*";
            this.lblReplacedICNAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMerchandiseDestroyedAsterisk
            // 
            this.lblMerchandiseDestroyedAsterisk.BackColor = System.Drawing.Color.Transparent;
            this.lblMerchandiseDestroyedAsterisk.ForeColor = System.Drawing.Color.Red;
            this.lblMerchandiseDestroyedAsterisk.Location = new System.Drawing.Point(3, 362);
            this.lblMerchandiseDestroyedAsterisk.Margin = new System.Windows.Forms.Padding(3);
            this.lblMerchandiseDestroyedAsterisk.Name = "lblMerchandiseDestroyedAsterisk";
            this.lblMerchandiseDestroyedAsterisk.Size = new System.Drawing.Size(11, 21);
            this.lblMerchandiseDestroyedAsterisk.TabIndex = 164;
            this.lblMerchandiseDestroyedAsterisk.Text = "*";
            this.lblMerchandiseDestroyedAsterisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBack.AutoSize = true;
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack.BackgroundImage")));
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(109, 479);
            this.btnBack.Margin = new System.Windows.Forms.Padding(0);
            this.btnBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.btnBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 50);
            this.btnBack.TabIndex = 147;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblIcnText
            // 
            this.lblIcnText.AutoSize = true;
            this.lblIcnText.BackColor = System.Drawing.Color.Transparent;
            this.lblIcnText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcnText.Location = new System.Drawing.Point(15, 47);
            this.lblIcnText.Name = "lblIcnText";
            this.lblIcnText.Size = new System.Drawing.Size(29, 13);
            this.lblIcnText.TabIndex = 148;
            this.lblIcnText.Text = "ICN:";
            // 
            // lblICN
            // 
            this.lblICN.AutoSize = true;
            this.lblICN.BackColor = System.Drawing.Color.Transparent;
            this.lblICN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblICN.Location = new System.Drawing.Point(50, 47);
            this.lblICN.Name = "lblICN";
            this.lblICN.Size = new System.Drawing.Size(59, 13);
            this.lblICN.TabIndex = 149;
            this.lblICN.Text = "123456.1";
            // 
            // flowLayoutPanelAmount
            // 
            this.flowLayoutPanelAmount.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelAmount.Controls.Add(this.lblChargeOffAmount);
            this.flowLayoutPanelAmount.Controls.Add(this.lblChargeOffAmountText);
            this.flowLayoutPanelAmount.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelAmount.Location = new System.Drawing.Point(231, 47);
            this.flowLayoutPanelAmount.Name = "flowLayoutPanelAmount";
            this.flowLayoutPanelAmount.Size = new System.Drawing.Size(260, 20);
            this.flowLayoutPanelAmount.TabIndex = 150;
            // 
            // lblChargeOffAmount
            // 
            this.lblChargeOffAmount.AutoSize = true;
            this.lblChargeOffAmount.BackColor = System.Drawing.Color.Transparent;
            this.lblChargeOffAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChargeOffAmount.Location = new System.Drawing.Point(202, 0);
            this.lblChargeOffAmount.Name = "lblChargeOffAmount";
            this.lblChargeOffAmount.Size = new System.Drawing.Size(55, 13);
            this.lblChargeOffAmount.TabIndex = 151;
            this.lblChargeOffAmount.Text = "$ 100.00";
            // 
            // lblChargeOffAmountText
            // 
            this.lblChargeOffAmountText.AutoSize = true;
            this.lblChargeOffAmountText.BackColor = System.Drawing.Color.Transparent;
            this.lblChargeOffAmountText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChargeOffAmountText.Location = new System.Drawing.Point(79, 0);
            this.lblChargeOffAmountText.Name = "lblChargeOffAmountText";
            this.lblChargeOffAmountText.Size = new System.Drawing.Size(117, 13);
            this.lblChargeOffAmountText.TabIndex = 150;
            this.lblChargeOffAmountText.Text = "Charge Off Amount:";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.Gainsboro;
            this.txtDescription.Location = new System.Drawing.Point(12, 76);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(482, 80);
            this.txtDescription.TabIndex = 151;
            this.txtDescription.Text = "";
            // 
            // ChargeOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_768_BlueScale;
            this.ClientSize = new System.Drawing.Size(508, 538);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.flowLayoutPanelAmount);
            this.Controls.Add(this.lblICN);
            this.Controls.Add(this.lblIcnText);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnCancel);
            this.MinimumSize = new System.Drawing.Size(508, 453);
            this.Name = "ChargeOff";
            this.Text = "ChargeOff";
            this.Load += new System.EventHandler(this.ChargeOff_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanelLabels.ResumeLayout(false);
            this.flowLayoutPanelFields.ResumeLayout(false);
            this.flowLayoutPanelFields.PerformLayout();
            this.flowLayoutPanelAsterisks.ResumeLayout(false);
            this.flowLayoutPanelAmount.ResumeLayout(false);
            this.flowLayoutPanelAmount.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.ComboBox ddlReason;
        private System.Windows.Forms.Label lblHeading;
        private CustomButton btnSubmit;
        private CustomButton btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblAuthorizedBy;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblPoliceCaseNumber;
        private System.Windows.Forms.TextBox txtAuthorizedBy;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtPoliceCaseNumber;
        private System.Windows.Forms.Label lblReasonAsterisk;
        private System.Windows.Forms.Label lblCommentAsterisk;
        private System.Windows.Forms.Label lblAuthorizedByAsterisk;
        private CustomButton btnBack;
        private System.Windows.Forms.Label lblIcnText;
        private System.Windows.Forms.Label lblICN;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAmount;
        private System.Windows.Forms.Label lblChargeOffAmount;
        private System.Windows.Forms.Label lblChargeOffAmountText;
        private System.Windows.Forms.RichTextBox txtDescription;
        private System.Windows.Forms.Label lblATFIncidentNumber;
        private System.Windows.Forms.TextBox txtATFIncidentNumber;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLabels;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFields;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAsterisks;
        private System.Windows.Forms.Label lblPoliceCaseNumberAsterisk;
        private System.Windows.Forms.Label lblATFIncidentNumberAsterisk;
        private System.Windows.Forms.Label lblCharitableOrganization;
        private System.Windows.Forms.Label lblCharitableOrganizationAddress;
        private System.Windows.Forms.Label lblCharitableOrganizationCity;
        private System.Windows.Forms.Label lblCharitableOrganizationState;
        private System.Windows.Forms.Label lblCharitableOrganizationPostalCode;
        private System.Windows.Forms.Label lblPawnTicketNumber;
        private System.Windows.Forms.Label lblReplacedICN;
        private System.Windows.Forms.Label lblMerchandiseDestroyed;
        private System.Windows.Forms.TextBox txtCharitableOrganization;
        private System.Windows.Forms.TextBox txtCharitableOrganizationAddress;
        private System.Windows.Forms.TextBox txtCharitableOrganizationCity;
        private System.Windows.Forms.TextBox txtCharitableOrganizationState;
        private System.Windows.Forms.TextBox txtCharitableOrganizationPostalCode;
        private System.Windows.Forms.TextBox txtPawnTicketNumber;
        private System.Windows.Forms.TextBox txtReplacedICN;
        private System.Windows.Forms.CheckBox chkMerchandiseDestoyed;
        private System.Windows.Forms.Label lblCharitableOrganizationAsterisk;
        private System.Windows.Forms.Label lblCharitableOrganizationAddressAsterisk;
        private System.Windows.Forms.Label lblCharitableOrganizationCityAsterisk;
        private System.Windows.Forms.Label lblCharitableOrganizationStateAsterisk;
        private System.Windows.Forms.Label lblCharitableOrganizationPostalCodeAsterisk;
        private System.Windows.Forms.Label lblPawnTicketNumberAsterisk;
        private System.Windows.Forms.Label lblReplacedICNAsterisk;
        private System.Windows.Forms.Label lblMerchandiseDestroyedAsterisk;
    }
}