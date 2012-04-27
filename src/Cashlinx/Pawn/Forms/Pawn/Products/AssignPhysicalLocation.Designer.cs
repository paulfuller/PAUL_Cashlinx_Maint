using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Products
{
    partial class AssignPhysicalLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignPhysicalLocation));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.AssignLocationLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.viewTransactionComboBox = new System.Windows.Forms.ComboBox();
            this.pageIndexLabel = new System.Windows.Forms.Label();
            this.firstButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.lastButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rowIndexLabel = new System.Windows.Forms.Label();
            this.reassignSearchPanel = new System.Windows.Forms.Panel();
            this.searchType = new System.Windows.Forms.ComboBox();
            this.searchCriteria = new CustomTextBox();
            this.searchButton = new CustomButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gvLocation = new PagedGridView(this.components);
            this.userId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ticket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status_cd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.md_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loc_aisle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loc_shelf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gun_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storenumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn_doc_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn_store = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.disp_doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn_sub_item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icn_year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.reassignSearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // AssignLocationLabel
            // 
            this.AssignLocationLabel.AutoSize = true;
            this.AssignLocationLabel.BackColor = System.Drawing.Color.Transparent;
            this.AssignLocationLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssignLocationLabel.ForeColor = System.Drawing.Color.White;
            this.AssignLocationLabel.Location = new System.Drawing.Point(355, 38);
            this.AssignLocationLabel.Name = "AssignLocationLabel";
            this.AssignLocationLabel.Size = new System.Drawing.Size(219, 19);
            this.AssignLocationLabel.TabIndex = 40;
            this.AssignLocationLabel.Text = "Assign Item Physical Location";
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.Transparent;
            this.submitButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.submitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(824, 625);
            this.submitButton.Margin = new System.Windows.Forms.Padding(0);
            this.submitButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.submitButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(100, 50);
            this.submitButton.TabIndex = 9;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // printButton
            // 
            this.printButton.BackColor = System.Drawing.Color.Transparent;
            this.printButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.printButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.printButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.printButton.FlatAppearance.BorderSize = 0;
            this.printButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.printButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printButton.ForeColor = System.Drawing.Color.White;
            this.printButton.Location = new System.Drawing.Point(728, 625);
            this.printButton.Margin = new System.Windows.Forms.Padding(0);
            this.printButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.printButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(100, 50);
            this.printButton.TabIndex = 8;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = false;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(12, 625);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // viewTransactionComboBox
            // 
            this.viewTransactionComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.viewTransactionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.viewTransactionComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewTransactionComboBox.ForeColor = System.Drawing.Color.Black;
            this.viewTransactionComboBox.FormattingEnabled = true;
            this.viewTransactionComboBox.Items.AddRange(new object[] {
            "Locate Merchandise - My Transactions",
            "Locate Merchandise - All Transactions",
            "Relocate Merchandise"});
            this.viewTransactionComboBox.Location = new System.Drawing.Point(12, 108);
            this.viewTransactionComboBox.Name = "viewTransactionComboBox";
            this.viewTransactionComboBox.Size = new System.Drawing.Size(229, 21);
            this.viewTransactionComboBox.TabIndex = 1;
            this.viewTransactionComboBox.SelectedIndexChanged += new System.EventHandler(this.viewTransactionComboBox_SelectedIndexChanged);
            // 
            // pageIndexLabel
            // 
            this.pageIndexLabel.BackColor = System.Drawing.Color.Transparent;
            this.pageIndexLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageIndexLabel.Location = new System.Drawing.Point(221, 23);
            this.pageIndexLabel.Name = "pageIndexLabel";
            this.pageIndexLabel.Size = new System.Drawing.Size(102, 19);
            this.pageIndexLabel.TabIndex = 42;
            this.pageIndexLabel.Text = "1 of 9";
            this.pageIndexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // firstButton
            // 
            this.firstButton.BackColor = System.Drawing.Color.Transparent;
            this.firstButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.firstButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.firstButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.firstButton.FlatAppearance.BorderSize = 0;
            this.firstButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.firstButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.firstButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.firstButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstButton.ForeColor = System.Drawing.Color.White;
            this.firstButton.Location = new System.Drawing.Point(6, 6);
            this.firstButton.Margin = new System.Windows.Forms.Padding(0);
            this.firstButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.firstButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.firstButton.Name = "firstButton";
            this.firstButton.Size = new System.Drawing.Size(100, 50);
            this.firstButton.TabIndex = 4;
            this.firstButton.Text = "First";
            this.firstButton.UseVisualStyleBackColor = false;
            // 
            // previousButton
            // 
            this.previousButton.BackColor = System.Drawing.Color.Transparent;
            this.previousButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.previousButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.previousButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.previousButton.FlatAppearance.BorderSize = 0;
            this.previousButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.previousButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.previousButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.previousButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previousButton.ForeColor = System.Drawing.Color.White;
            this.previousButton.Location = new System.Drawing.Point(112, 6);
            this.previousButton.Margin = new System.Windows.Forms.Padding(0);
            this.previousButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.previousButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(100, 50);
            this.previousButton.TabIndex = 5;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = false;
            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.Color.Transparent;
            this.nextButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.nextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nextButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.nextButton.FlatAppearance.BorderSize = 0;
            this.nextButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.nextButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextButton.ForeColor = System.Drawing.Color.White;
            this.nextButton.Location = new System.Drawing.Point(326, 5);
            this.nextButton.Margin = new System.Windows.Forms.Padding(0);
            this.nextButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.nextButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(100, 50);
            this.nextButton.TabIndex = 6;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = false;
            // 
            // lastButton
            // 
            this.lastButton.BackColor = System.Drawing.Color.Transparent;
            this.lastButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.lastButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.lastButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.lastButton.FlatAppearance.BorderSize = 0;
            this.lastButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.lastButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.lastButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lastButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastButton.ForeColor = System.Drawing.Color.White;
            this.lastButton.Location = new System.Drawing.Point(431, 5);
            this.lastButton.Margin = new System.Windows.Forms.Padding(0);
            this.lastButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.lastButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.lastButton.Name = "lastButton";
            this.lastButton.Size = new System.Drawing.Size(100, 50);
            this.lastButton.TabIndex = 7;
            this.lastButton.Text = "Last";
            this.lastButton.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.firstButton);
            this.panel1.Controls.Add(this.lastButton);
            this.panel1.Controls.Add(this.pageIndexLabel);
            this.panel1.Controls.Add(this.nextButton);
            this.panel1.Controls.Add(this.previousButton);
            this.panel1.Location = new System.Drawing.Point(178, 620);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 60);
            this.panel1.TabIndex = 48;
            this.panel1.Visible = false;
            // 
            // rowIndexLabel
            // 
            this.rowIndexLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.rowIndexLabel.BackColor = System.Drawing.Color.Transparent;
            this.rowIndexLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rowIndexLabel.Location = new System.Drawing.Point(785, 111);
            this.rowIndexLabel.Name = "rowIndexLabel";
            this.rowIndexLabel.Size = new System.Drawing.Size(132, 18);
            this.rowIndexLabel.TabIndex = 49;
            this.rowIndexLabel.Text = "asdfsdgsdfgsdfgdf";
            this.rowIndexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // reassignSearchPanel
            // 
            this.reassignSearchPanel.BackColor = System.Drawing.Color.Transparent;
            this.reassignSearchPanel.Controls.Add(this.searchType);
            this.reassignSearchPanel.Controls.Add(this.searchCriteria);
            this.reassignSearchPanel.Controls.Add(this.searchButton);
            this.reassignSearchPanel.Location = new System.Drawing.Point(12, 133);
            this.reassignSearchPanel.Name = "reassignSearchPanel";
            this.reassignSearchPanel.Size = new System.Drawing.Size(460, 57);
            this.reassignSearchPanel.TabIndex = 50;
            // 
            // searchType
            // 
            this.searchType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.searchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchType.ForeColor = System.Drawing.Color.Black;
            this.searchType.FormattingEnabled = true;
            this.searchType.Items.AddRange(new object[] {
            "ICN",
            "Pawn Ticket Number",
            "Buy Ticket Number"});
            this.searchType.Location = new System.Drawing.Point(18, 20);
            this.searchType.Name = "searchType";
            this.searchType.Size = new System.Drawing.Size(153, 21);
            this.searchType.TabIndex = 1;
            this.searchType.SelectionChangeCommitted += new System.EventHandler(this.searchTypeChanged);
            // 
            // searchCriteria
            // 
            this.searchCriteria.AllowDecimalNumbers = true;
            this.searchCriteria.CausesValidation = false;
            this.searchCriteria.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.searchCriteria.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchCriteria.Location = new System.Drawing.Point(177, 20);
            this.searchCriteria.MaxLength = 18;
            this.searchCriteria.Name = "searchCriteria";
            this.searchCriteria.Size = new System.Drawing.Size(136, 21);
            this.searchCriteria.TabIndex = 2;
            this.searchCriteria.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.searchCriteria.TextChanged += new System.EventHandler(this.searchCriteria_lengthCheck);
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.Transparent;
            this.searchButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("searchButton.BackgroundImage")));
            this.searchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.searchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.searchButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.ForeColor = System.Drawing.Color.White;
            this.searchButton.Location = new System.Drawing.Point(347, 4);
            this.searchButton.Margin = new System.Windows.Forms.Padding(0);
            this.searchButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.searchButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(100, 50);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Find";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "colUser";
            this.dataGridViewTextBoxColumn1.HeaderText = "User";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "colTicketNumber";
            this.dataGridViewTextBoxColumn2.HeaderText = "Ticket No.";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "colStatus";
            this.dataGridViewTextBoxColumn3.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "colDescription";
            this.dataGridViewTextBoxColumn4.HeaderText = "Description";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "colAisle";
            this.dataGridViewTextBoxColumn5.HeaderText = "Aisle";
            this.dataGridViewTextBoxColumn5.MaxInputLength = 4;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "colShelf";
            this.dataGridViewTextBoxColumn6.HeaderText = "Shelf";
            this.dataGridViewTextBoxColumn6.MaxInputLength = 4;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "colOther";
            this.dataGridViewTextBoxColumn7.HeaderText = "Other";
            this.dataGridViewTextBoxColumn7.MaxInputLength = 50;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "colGunNumber";
            this.dataGridViewTextBoxColumn8.HeaderText = "Gun No.";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "recID";
            this.dataGridViewTextBoxColumn9.FillWeight = 1F;
            this.dataGridViewTextBoxColumn9.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.dataGridViewTextBoxColumn9.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn9.Visible = false;
            this.dataGridViewTextBoxColumn9.Width = 2;
            // 
            // gvLocation
            // 
            this.gvLocation.AllowUserToAddRows = false;
            this.gvLocation.AllowUserToDeleteRows = false;
            this.gvLocation.AllowUserToResizeColumns = false;
            this.gvLocation.AllowUserToResizeRows = false;
            this.gvLocation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvLocation.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.gvLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLocation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gvLocation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userId,
            this.Ticket,
            this.status_cd,
            this.md_desc,
            this.loc_aisle,
            this.loc_shelf,
            this.location,
            this.gun_number,
            this.recID,
            this.storenumber,
            this.icn_doc,
            this.icn_doc_type,
            this.icn_item,
            this.icn_store,
            this.disp_doc,
            this.icn_sub_item,
            this.icn_year});
            this.gvLocation.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvLocation.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvLocation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvLocation.GridColor = System.Drawing.Color.LightGray;
            this.gvLocation.Location = new System.Drawing.Point(12, 196);
            this.gvLocation.MultiSelect = false;
            this.gvLocation.Name = "gvLocation";
            this.gvLocation.pageSize = 20;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvLocation.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvLocation.RowHeadersVisible = false;
            this.gvLocation.RowHeadersWidth = 20;
            this.gvLocation.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            this.gvLocation.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvLocation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gvLocation.Size = new System.Drawing.Size(905, 414);
            this.gvLocation.TabIndex = 2;
            this.gvLocation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvLocation_CellClick);
            this.gvLocation.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvLocation_CellEndEdit);
            this.gvLocation.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvLocation_CellEnter);
            this.gvLocation.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvLocation_CellMouseEnter);
            // 
            // userId
            // 
            this.userId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.userId.DataPropertyName = "userId";
            this.userId.HeaderText = "User";
            this.userId.Name = "userId";
            this.userId.ReadOnly = true;
            this.userId.Width = 54;
            // 
            // Ticket
            // 
            this.Ticket.DataPropertyName = "Ticket";
            this.Ticket.HeaderText = "Ticket No.";
            this.Ticket.Name = "Ticket";
            // 
            // status_cd
            // 
            this.status_cd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.status_cd.DataPropertyName = "status_cd";
            this.status_cd.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.status_cd.Name = "status_cd";
            this.status_cd.ReadOnly = true;
            this.status_cd.Width = 19;
            // 
            // md_desc
            // 
            this.md_desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.md_desc.DataPropertyName = "md_desc";
            this.md_desc.HeaderText = "Description";
            this.md_desc.Name = "md_desc";
            this.md_desc.ReadOnly = true;
            this.md_desc.Width = 448;
            // 
            // loc_aisle
            // 
            this.loc_aisle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.loc_aisle.DataPropertyName = "loc_aisle";
            this.loc_aisle.HeaderText = "Aisle";
            this.loc_aisle.MaxInputLength = 4;
            this.loc_aisle.Name = "loc_aisle";
            this.loc_aisle.Width = 54;
            // 
            // loc_shelf
            // 
            this.loc_shelf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.loc_shelf.DataPropertyName = "loc_shelf";
            this.loc_shelf.HeaderText = "Shelf";
            this.loc_shelf.MaxInputLength = 4;
            this.loc_shelf.Name = "loc_shelf";
            this.loc_shelf.Width = 56;
            // 
            // location
            // 
            this.location.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.location.DataPropertyName = "location";
            this.location.HeaderText = "Other";
            this.location.MaxInputLength = 5;
            this.location.Name = "location";
            this.location.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.location.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.location.Width = 120;
            // 
            // gun_number
            // 
            this.gun_number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.gun_number.DataPropertyName = "gun_number";
            this.gun_number.HeaderText = "Gun No.";
            this.gun_number.Name = "gun_number";
            this.gun_number.ReadOnly = true;
            this.gun_number.Width = 71;
            // 
            // recID
            // 
            this.recID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.recID.DataPropertyName = "recID";
            this.recID.FillWeight = 1F;
            this.recID.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.recID.MinimumWidth = 2;
            this.recID.Name = "recID";
            this.recID.ReadOnly = true;
            this.recID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.recID.Visible = false;
            this.recID.Width = 2;
            // 
            // storenumber
            // 
            this.storenumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.storenumber.DataPropertyName = "storenumber";
            this.storenumber.FillWeight = 1F;
            this.storenumber.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.storenumber.MinimumWidth = 2;
            this.storenumber.Name = "storenumber";
            this.storenumber.Visible = false;
            this.storenumber.Width = 2;
            // 
            // icn_doc
            // 
            this.icn_doc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.icn_doc.DataPropertyName = "icn_doc";
            this.icn_doc.HeaderText = "";
            this.icn_doc.Name = "icn_doc";
            this.icn_doc.ReadOnly = true;
            this.icn_doc.Visible = false;
            this.icn_doc.Width = 19;
            // 
            // icn_doc_type
            // 
            this.icn_doc_type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.icn_doc_type.DataPropertyName = "icn_doc_type";
            this.icn_doc_type.FillWeight = 1F;
            this.icn_doc_type.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.icn_doc_type.MinimumWidth = 2;
            this.icn_doc_type.Name = "icn_doc_type";
            this.icn_doc_type.Visible = false;
            this.icn_doc_type.Width = 2;
            // 
            // icn_item
            // 
            this.icn_item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.icn_item.DataPropertyName = "icn_item";
            this.icn_item.FillWeight = 1F;
            this.icn_item.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.icn_item.MinimumWidth = 2;
            this.icn_item.Name = "icn_item";
            this.icn_item.Visible = false;
            this.icn_item.Width = 2;
            // 
            // icn_store
            // 
            this.icn_store.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.icn_store.DataPropertyName = "icn_store";
            this.icn_store.FillWeight = 1F;
            this.icn_store.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.icn_store.MinimumWidth = 2;
            this.icn_store.Name = "icn_store";
            this.icn_store.Visible = false;
            this.icn_store.Width = 2;
            // 
            // disp_doc
            // 
            this.disp_doc.DataPropertyName = "disp_doc";
            this.disp_doc.HeaderText = "";
            this.disp_doc.Name = "disp_doc";
            this.disp_doc.Visible = false;
            // 
            // icn_sub_item
            // 
            this.icn_sub_item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.icn_sub_item.DataPropertyName = "icn_sub_item";
            this.icn_sub_item.FillWeight = 1F;
            this.icn_sub_item.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.icn_sub_item.MinimumWidth = 2;
            this.icn_sub_item.Name = "icn_sub_item";
            this.icn_sub_item.Visible = false;
            this.icn_sub_item.Width = 2;
            // 
            // icn_year
            // 
            this.icn_year.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.icn_year.DataPropertyName = "icn_year";
            this.icn_year.FillWeight = 1F;
            this.icn_year.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.icn_year.MinimumWidth = 2;
            this.icn_year.Name = "icn_year";
            this.icn_year.Visible = false;
            this.icn_year.Width = 2;
            // 
            // AssignPhysicalLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(929, 696);
            this.ControlBox = false;
            this.Controls.Add(this.reassignSearchPanel);
            this.Controls.Add(this.rowIndexLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.viewTransactionComboBox);
            this.Controls.Add(this.AssignLocationLabel);
            this.Controls.Add(this.gvLocation);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.cancelButton);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AssignPhysicalLocation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AssignPhysicalLocation_Load);
            this.panel1.ResumeLayout(false);
            this.reassignSearchPanel.ResumeLayout(false);
            this.reassignSearchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLocation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AssignLocationLabel;
        private PagedGridView gvLocation;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox viewTransactionComboBox;
        private System.Windows.Forms.Label pageIndexLabel;
        private System.Windows.Forms.Button firstButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button lastButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label rowIndexLabel;
        private System.Windows.Forms.Panel reassignSearchPanel;
        private CustomButton searchButton;
        private CustomTextBox searchCriteria;
        private System.Windows.Forms.ComboBox searchType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn userId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ticket;
        private System.Windows.Forms.DataGridViewTextBoxColumn status_cd;
        private System.Windows.Forms.DataGridViewTextBoxColumn md_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn loc_aisle;
        private System.Windows.Forms.DataGridViewTextBoxColumn loc_shelf;
        private System.Windows.Forms.DataGridViewTextBoxColumn location;
        private System.Windows.Forms.DataGridViewTextBoxColumn gun_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn recID;
        private System.Windows.Forms.DataGridViewTextBoxColumn storenumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn_doc_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn_store;
        private System.Windows.Forms.DataGridViewTextBoxColumn disp_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn_sub_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn icn_year;
    }
}