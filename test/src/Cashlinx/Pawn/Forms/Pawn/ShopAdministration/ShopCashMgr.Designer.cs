namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class ShopCashMgr
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShopCashMgr));
            this.cashDrawerListView = new System.Windows.Forms.ListView();
            this.cashDrawerIconImageList = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.assignedUserLabel = new System.Windows.Forms.Label();
            this.assignedUserListView = new System.Windows.Forms.ListView();
            this.assignedUserNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.navLeftButton = new System.Windows.Forms.Button();
            this.navRightButton = new System.Windows.Forms.Button();
            this.availableUsersListView = new System.Windows.Forms.ListView();
            this.availableUserNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.availUsersLabel = new System.Windows.Forms.Label();
            this.removeUserButton = new System.Windows.Forms.Button();
            this.addUserButton = new System.Windows.Forms.Button();
            this.shopMgmtFormLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.buttonAddDrawer = new System.Windows.Forms.Button();
            this.buttonDeleteDrawer = new System.Windows.Forms.Button();
            this.labelBacktoAssignments = new System.Windows.Forms.Label();
            this.labelSafeUserHeading = new System.Windows.Forms.Label();
            this.listViewSafeUsers = new System.Windows.Forms.ListView();
            this.UserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // cashDrawerListView
            // 
            this.cashDrawerListView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cashDrawerListView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cashDrawerListView.ForeColor = System.Drawing.Color.Black;
            this.cashDrawerListView.HideSelection = false;
            this.cashDrawerListView.LargeImageList = this.cashDrawerIconImageList;
            this.cashDrawerListView.Location = new System.Drawing.Point(48, 204);
            this.cashDrawerListView.MultiSelect = false;
            this.cashDrawerListView.Name = "cashDrawerListView";
            this.cashDrawerListView.Size = new System.Drawing.Size(508, 120);
            this.cashDrawerListView.TabIndex = 0;
            this.cashDrawerListView.UseCompatibleStateImageBehavior = false;
            this.cashDrawerListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.cashDrawerListView_ItemSelectionChanged);
            // 
            // cashDrawerIconImageList
            // 
            this.cashDrawerIconImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("cashDrawerIconImageList.ImageStream")));
            this.cashDrawerIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.cashDrawerIconImageList.Images.SetKeyName(0, "cashdrawer.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cash Drawers";
            // 
            // assignedUserLabel
            // 
            this.assignedUserLabel.AutoSize = true;
            this.assignedUserLabel.BackColor = System.Drawing.Color.Transparent;
            this.assignedUserLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assignedUserLabel.Location = new System.Drawing.Point(48, 420);
            this.assignedUserLabel.Name = "assignedUserLabel";
            this.assignedUserLabel.Size = new System.Drawing.Size(107, 16);
            this.assignedUserLabel.TabIndex = 6;
            this.assignedUserLabel.Text = "Assigned Users";
            // 
            // assignedUserListView
            // 
            this.assignedUserListView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.assignedUserListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.assignedUserNameColumnHeader});
            this.assignedUserListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assignedUserListView.ForeColor = System.Drawing.Color.Black;
            this.assignedUserListView.FullRowSelect = true;
            this.assignedUserListView.GridLines = true;
            this.assignedUserListView.Location = new System.Drawing.Point(49, 439);
            this.assignedUserListView.MultiSelect = false;
            this.assignedUserListView.Name = "assignedUserListView";
            this.assignedUserListView.Size = new System.Drawing.Size(146, 157);
            this.assignedUserListView.TabIndex = 8;
            this.assignedUserListView.UseCompatibleStateImageBehavior = false;
            this.assignedUserListView.View = System.Windows.Forms.View.Details;
            this.assignedUserListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.assignedUserListView_ItemSelectionChanged);
            this.assignedUserListView.SelectedIndexChanged += new System.EventHandler(this.assignedUserListView_SelectedIndexChanged);
            // 
            // assignedUserNameColumnHeader
            // 
            this.assignedUserNameColumnHeader.Text = "User Name";
            this.assignedUserNameColumnHeader.Width = 140;
            // 
            // navLeftButton
            // 
            this.navLeftButton.BackColor = System.Drawing.Color.Transparent;
            this.navLeftButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.navLeftButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.navLeftButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.navLeftButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.navLeftButton.FlatAppearance.BorderSize = 0;
            this.navLeftButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.navLeftButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.navLeftButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navLeftButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navLeftButton.ForeColor = System.Drawing.Color.White;
            this.navLeftButton.Location = new System.Drawing.Point(193, 339);
            this.navLeftButton.Margin = new System.Windows.Forms.Padding(0);
            this.navLeftButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.navLeftButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.navLeftButton.Name = "navLeftButton";
            this.navLeftButton.Size = new System.Drawing.Size(100, 50);
            this.navLeftButton.TabIndex = 9;
            this.navLeftButton.Text = "<";
            this.navLeftButton.UseVisualStyleBackColor = false;
            this.navLeftButton.Click += new System.EventHandler(this.navLeftButton_Click);
            // 
            // navRightButton
            // 
            this.navRightButton.BackColor = System.Drawing.Color.Transparent;
            this.navRightButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.navRightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.navRightButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.navRightButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.navRightButton.FlatAppearance.BorderSize = 0;
            this.navRightButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.navRightButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.navRightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.navRightButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navRightButton.ForeColor = System.Drawing.Color.White;
            this.navRightButton.Location = new System.Drawing.Point(314, 339);
            this.navRightButton.Margin = new System.Windows.Forms.Padding(0);
            this.navRightButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.navRightButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.navRightButton.Name = "navRightButton";
            this.navRightButton.Size = new System.Drawing.Size(100, 50);
            this.navRightButton.TabIndex = 10;
            this.navRightButton.Text = ">";
            this.navRightButton.UseVisualStyleBackColor = false;
            this.navRightButton.Click += new System.EventHandler(this.navRightButton_Click);
            // 
            // availableUsersListView
            // 
            this.availableUsersListView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.availableUsersListView.CausesValidation = false;
            this.availableUsersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.availableUserNameColumnHeader});
            this.availableUsersListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableUsersListView.ForeColor = System.Drawing.Color.Black;
            this.availableUsersListView.FullRowSelect = true;
            this.availableUsersListView.GridLines = true;
            this.availableUsersListView.HideSelection = false;
            this.availableUsersListView.Location = new System.Drawing.Point(377, 439);
            this.availableUsersListView.MultiSelect = false;
            this.availableUsersListView.Name = "availableUsersListView";
            this.availableUsersListView.ShowGroups = false;
            this.availableUsersListView.Size = new System.Drawing.Size(179, 157);
            this.availableUsersListView.TabIndex = 12;
            this.availableUsersListView.UseCompatibleStateImageBehavior = false;
            this.availableUsersListView.View = System.Windows.Forms.View.Details;
            this.availableUsersListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.availableUsersListView_ItemSelectionChanged);
            // 
            // availableUserNameColumnHeader
            // 
            this.availableUserNameColumnHeader.Text = "User Name";
            this.availableUserNameColumnHeader.Width = 173;
            // 
            // userColumn
            // 
            this.userColumn.Text = "User Name";
            this.userColumn.Width = 90;
            // 
            // availUsersLabel
            // 
            this.availUsersLabel.AutoSize = true;
            this.availUsersLabel.BackColor = System.Drawing.Color.Transparent;
            this.availUsersLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availUsersLabel.Location = new System.Drawing.Point(377, 420);
            this.availUsersLabel.Name = "availUsersLabel";
            this.availUsersLabel.Size = new System.Drawing.Size(107, 16);
            this.availUsersLabel.TabIndex = 13;
            this.availUsersLabel.Text = "Available Users";
            // 
            // removeUserButton
            // 
            this.removeUserButton.BackColor = System.Drawing.Color.Transparent;
            this.removeUserButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.removeUserButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.removeUserButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removeUserButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.removeUserButton.FlatAppearance.BorderSize = 0;
            this.removeUserButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.removeUserButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.removeUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeUserButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeUserButton.ForeColor = System.Drawing.Color.White;
            this.removeUserButton.Location = new System.Drawing.Point(250, 525);
            this.removeUserButton.Margin = new System.Windows.Forms.Padding(0);
            this.removeUserButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.removeUserButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.removeUserButton.Name = "removeUserButton";
            this.removeUserButton.Size = new System.Drawing.Size(100, 50);
            this.removeUserButton.TabIndex = 11;
            this.removeUserButton.Text = ">>";
            this.removeUserButton.UseVisualStyleBackColor = false;
            this.removeUserButton.Click += new System.EventHandler(this.removeUserButton_Click);
            // 
            // addUserButton
            // 
            this.addUserButton.BackColor = System.Drawing.Color.Transparent;
            this.addUserButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.addUserButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addUserButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addUserButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.addUserButton.FlatAppearance.BorderSize = 0;
            this.addUserButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.addUserButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.addUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addUserButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addUserButton.ForeColor = System.Drawing.Color.White;
            this.addUserButton.Location = new System.Drawing.Point(250, 459);
            this.addUserButton.Margin = new System.Windows.Forms.Padding(0);
            this.addUserButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.addUserButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(100, 50);
            this.addUserButton.TabIndex = 7;
            this.addUserButton.Text = "<<";
            this.addUserButton.UseVisualStyleBackColor = false;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // shopMgmtFormLabel
            // 
            this.shopMgmtFormLabel.AutoSize = true;
            this.shopMgmtFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.shopMgmtFormLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shopMgmtFormLabel.ForeColor = System.Drawing.Color.White;
            this.shopMgmtFormLabel.Location = new System.Drawing.Point(183, 33);
            this.shopMgmtFormLabel.Name = "shopMgmtFormLabel";
            this.shopMgmtFormLabel.Size = new System.Drawing.Size(235, 19);
            this.shopMgmtFormLabel.TabIndex = 14;
            this.shopMgmtFormLabel.Text = "Shop Cash Drawer Management";
            this.shopMgmtFormLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.Transparent;
            this.submitButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.submitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.submitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(447, 609);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(109, 50);
            this.submitButton.TabIndex = 15;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(48, 608);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(109, 50);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // buttonAddDrawer
            // 
            this.buttonAddDrawer.BackColor = System.Drawing.Color.Transparent;
            this.buttonAddDrawer.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonAddDrawer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAddDrawer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAddDrawer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonAddDrawer.FlatAppearance.BorderSize = 0;
            this.buttonAddDrawer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonAddDrawer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonAddDrawer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddDrawer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddDrawer.ForeColor = System.Drawing.Color.White;
            this.buttonAddDrawer.Location = new System.Drawing.Point(323, 608);
            this.buttonAddDrawer.Name = "buttonAddDrawer";
            this.buttonAddDrawer.Size = new System.Drawing.Size(109, 50);
            this.buttonAddDrawer.TabIndex = 18;
            this.buttonAddDrawer.Text = "Add Cash Drawer";
            this.buttonAddDrawer.UseVisualStyleBackColor = false;
            this.buttonAddDrawer.Click += new System.EventHandler(this.buttonAddDrawer_Click);
            // 
            // buttonDeleteDrawer
            // 
            this.buttonDeleteDrawer.BackColor = System.Drawing.Color.Transparent;
            this.buttonDeleteDrawer.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.buttonDeleteDrawer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDeleteDrawer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDeleteDrawer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonDeleteDrawer.FlatAppearance.BorderSize = 0;
            this.buttonDeleteDrawer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonDeleteDrawer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonDeleteDrawer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteDrawer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteDrawer.ForeColor = System.Drawing.Color.White;
            this.buttonDeleteDrawer.Location = new System.Drawing.Point(193, 608);
            this.buttonDeleteDrawer.Name = "buttonDeleteDrawer";
            this.buttonDeleteDrawer.Size = new System.Drawing.Size(109, 50);
            this.buttonDeleteDrawer.TabIndex = 19;
            this.buttonDeleteDrawer.Text = "Delete Cash Drawer";
            this.buttonDeleteDrawer.UseVisualStyleBackColor = false;
            this.buttonDeleteDrawer.Click += new System.EventHandler(this.buttonDeleteDrawer_Click);
            // 
            // labelBacktoAssignments
            // 
            this.labelBacktoAssignments.AutoSize = true;
            this.labelBacktoAssignments.BackColor = System.Drawing.Color.Transparent;
            this.labelBacktoAssignments.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBacktoAssignments.ForeColor = System.Drawing.Color.Blue;
            this.labelBacktoAssignments.Location = new System.Drawing.Point(48, 394);
            this.labelBacktoAssignments.Name = "labelBacktoAssignments";
            this.labelBacktoAssignments.Size = new System.Drawing.Size(178, 16);
            this.labelBacktoAssignments.TabIndex = 20;
            this.labelBacktoAssignments.Text = "Cash Drawer Assignments";
            this.labelBacktoAssignments.Click += new System.EventHandler(this.labelBacktoAssignments_Click);
            // 
            // labelSafeUserHeading
            // 
            this.labelSafeUserHeading.AutoSize = true;
            this.labelSafeUserHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelSafeUserHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSafeUserHeading.Location = new System.Drawing.Point(45, 74);
            this.labelSafeUserHeading.Name = "labelSafeUserHeading";
            this.labelSafeUserHeading.Size = new System.Drawing.Size(77, 16);
            this.labelSafeUserHeading.TabIndex = 21;
            this.labelSafeUserHeading.Text = "Safe Users";
            // 
            // listViewSafeUsers
            // 
            this.listViewSafeUsers.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listViewSafeUsers.CausesValidation = false;
            this.listViewSafeUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserName});
            this.listViewSafeUsers.GridLines = true;
            this.listViewSafeUsers.Location = new System.Drawing.Point(51, 94);
            this.listViewSafeUsers.MultiSelect = false;
            this.listViewSafeUsers.Name = "listViewSafeUsers";
            this.listViewSafeUsers.Size = new System.Drawing.Size(505, 90);
            this.listViewSafeUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewSafeUsers.TabIndex = 22;
            this.listViewSafeUsers.UseCompatibleStateImageBehavior = false;
            this.listViewSafeUsers.View = System.Windows.Forms.View.List;
            // 
            // UserName
            // 
            this.UserName.Width = 100;
            // 
            // ShopCashMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(600, 670);
            this.Controls.Add(this.listViewSafeUsers);
            this.Controls.Add(this.labelSafeUserHeading);
            this.Controls.Add(this.labelBacktoAssignments);
            this.Controls.Add(this.buttonDeleteDrawer);
            this.Controls.Add(this.buttonAddDrawer);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.shopMgmtFormLabel);
            this.Controls.Add(this.availUsersLabel);
            this.Controls.Add(this.availableUsersListView);
            this.Controls.Add(this.removeUserButton);
            this.Controls.Add(this.navRightButton);
            this.Controls.Add(this.navLeftButton);
            this.Controls.Add(this.assignedUserListView);
            this.Controls.Add(this.addUserButton);
            this.Controls.Add(this.assignedUserLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cashDrawerListView);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShopCashMgr";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shop Cash Register Management";
            this.Load += new System.EventHandler(this.ShopCashMgr_Load);
            this.Shown += new System.EventHandler(this.ShopCashMgr_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView cashDrawerListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label assignedUserLabel;
        private System.Windows.Forms.ListView assignedUserListView;
        private System.Windows.Forms.Button navLeftButton;
        private System.Windows.Forms.Button navRightButton;
        private System.Windows.Forms.ListView availableUsersListView;
        private System.Windows.Forms.Label availUsersLabel;
        private System.Windows.Forms.Button removeUserButton;
        private System.Windows.Forms.Button addUserButton;
        private System.Windows.Forms.Label shopMgmtFormLabel;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ImageList cashDrawerIconImageList;
        private System.Windows.Forms.ColumnHeader userColumn;
        //private System.Windows.Forms.ColumnHeader userIdColumn;
        private System.Windows.Forms.ColumnHeader assignedUserNameColumnHeader;
        private System.Windows.Forms.Button buttonAddDrawer;
        private System.Windows.Forms.Button buttonDeleteDrawer;
        private System.Windows.Forms.Label labelBacktoAssignments;
        private System.Windows.Forms.ColumnHeader availableUserNameColumnHeader;
        private System.Windows.Forms.Label labelSafeUserHeading;
        private System.Windows.Forms.ListView listViewSafeUsers;
        private System.Windows.Forms.ColumnHeader UserName;
        //private System.Windows.Forms.Button buttonWokstationManagement;
    }
}