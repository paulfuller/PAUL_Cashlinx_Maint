namespace AuditQueries.Forms
{
    partial class AuditQueriesForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            ""}, -1);
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.queriesGroupBox = new System.Windows.Forms.GroupBox();
            this.queryListView = new System.Windows.Forms.ListView();
            this.queryIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.queryNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.queryDescColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.selectQueryButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.connectionGroupBox.SuspendLayout();
            this.queriesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.connectionGroupBox.Controls.Add(this.passwordTextBox);
            this.connectionGroupBox.Controls.Add(this.userNameTextBox);
            this.connectionGroupBox.Controls.Add(this.label2);
            this.connectionGroupBox.Controls.Add(this.label1);
            this.connectionGroupBox.Controls.Add(this.loginButton);
            this.connectionGroupBox.Location = new System.Drawing.Point(166, 12);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Size = new System.Drawing.Size(429, 141);
            this.connectionGroupBox.TabIndex = 0;
            this.connectionGroupBox.TabStop = false;
            this.connectionGroupBox.Text = "Login";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.Color.White;
            this.passwordTextBox.ForeColor = System.Drawing.Color.Black;
            this.passwordTextBox.Location = new System.Drawing.Point(122, 52);
            this.passwordTextBox.MaxLength = 20;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(267, 23);
            this.passwordTextBox.TabIndex = 4;
            this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.BackColor = System.Drawing.Color.White;
            this.userNameTextBox.ForeColor = System.Drawing.Color.Black;
            this.userNameTextBox.Location = new System.Drawing.Point(122, 16);
            this.userNameTextBox.MaxLength = 20;
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(267, 23);
            this.userNameTextBox.TabIndex = 3;
            this.userNameTextBox.TextChanged += new System.EventHandler(this.userNameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Name:";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(177, 98);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 30);
            this.loginButton.TabIndex = 0;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // queriesGroupBox
            // 
            this.queriesGroupBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.queriesGroupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.queriesGroupBox.Controls.Add(this.queryListView);
            this.queriesGroupBox.Controls.Add(this.selectQueryButton);
            this.queriesGroupBox.Controls.Add(this.label3);
            this.queriesGroupBox.Enabled = false;
            this.queriesGroupBox.Location = new System.Drawing.Point(12, 159);
            this.queriesGroupBox.Name = "queriesGroupBox";
            this.queriesGroupBox.Size = new System.Drawing.Size(736, 223);
            this.queriesGroupBox.TabIndex = 1;
            this.queriesGroupBox.TabStop = false;
            this.queriesGroupBox.Text = "Queries";
            // 
            // queryListView
            // 
            this.queryListView.AutoArrange = false;
            this.queryListView.BackColor = System.Drawing.Color.White;
            this.queryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.queryIdColumn,
            this.queryNameColumn,
            this.queryDescColumn});
            this.queryListView.ForeColor = System.Drawing.Color.Black;
            this.queryListView.FullRowSelect = true;
            this.queryListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.queryListView.HideSelection = false;
            this.queryListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.queryListView.Location = new System.Drawing.Point(9, 45);
            this.queryListView.MultiSelect = false;
            this.queryListView.Name = "queryListView";
            this.queryListView.Size = new System.Drawing.Size(721, 136);
            this.queryListView.TabIndex = 4;
            this.queryListView.TabStop = false;
            this.queryListView.UseCompatibleStateImageBehavior = false;
            this.queryListView.View = System.Windows.Forms.View.Details;
            this.queryListView.SelectedIndexChanged += new System.EventHandler(this.queryListView_SelectedIndexChanged);
            // 
            // queryIdColumn
            // 
            this.queryIdColumn.Text = "Query ID";
            this.queryIdColumn.Width = 66;
            // 
            // queryNameColumn
            // 
            this.queryNameColumn.Text = "Name";
            this.queryNameColumn.Width = 186;
            // 
            // queryDescColumn
            // 
            this.queryDescColumn.Text = "Description";
            this.queryDescColumn.Width = 464;
            // 
            // selectQueryButton
            // 
            this.selectQueryButton.Location = new System.Drawing.Point(655, 187);
            this.selectQueryButton.Name = "selectQueryButton";
            this.selectQueryButton.Size = new System.Drawing.Size(75, 30);
            this.selectQueryButton.TabIndex = 3;
            this.selectQueryButton.Text = "Select";
            this.selectQueryButton.UseVisualStyleBackColor = true;
            this.selectQueryButton.Click += new System.EventHandler(this.selectQueryButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Choose a query then hit Select:";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(343, 388);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 30);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // AuditQueriesForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(760, 432);
            this.ControlBox = false;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.queriesGroupBox);
            this.Controls.Add(this.connectionGroupBox);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AuditQueriesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audit Queries Tool";
            this.Load += new System.EventHandler(this.AuditQueriesForm_Load);
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.queriesGroupBox.ResumeLayout(false);
            this.queriesGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connectionGroupBox;
        private System.Windows.Forms.GroupBox queriesGroupBox;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button selectQueryButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView queryListView;
        private System.Windows.Forms.ColumnHeader queryIdColumn;
        private System.Windows.Forms.ColumnHeader queryNameColumn;
        private System.Windows.Forms.ColumnHeader queryDescColumn;
    }
}

