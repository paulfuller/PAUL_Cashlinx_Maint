using Common.Libraries.Forms.Components;
//Odd file lock

namespace Pawn.Forms.Inquiry
{
    partial class SearchResults
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
            System.Windows.Forms.Label searchResults_lb;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchResults));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Back_btn = new CustomButton();
            this.Print_btn = new CustomButton();
            this.Cancel_btn = new CustomButton();
            this.resultsGrid_dg = new CustomDataGridView();
            this.ViewDetail = new System.Windows.Forms.DataGridViewImageColumn();
            this.searchCriteria_lb = new System.Windows.Forms.Label();
            this.windowHeading_lb = new System.Windows.Forms.Label();
            this.criteriaDetails_panel = new System.Windows.Forms.Panel();
            this.criteriaSummary_txt = new System.Windows.Forms.TextBox();
            this.NrLoans_tb = new System.Windows.Forms.Label();
            this.LoanCtr_tb = new System.Windows.Forms.Label();
            this.criteriaSummary_txt = new System.Windows.Forms.TextBox();
            searchResults_lb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.resultsGrid_dg)).BeginInit();
            this.criteriaDetails_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchResults_lb
            // 
            searchResults_lb.AutoSize = true;
            searchResults_lb.BackColor = System.Drawing.Color.Transparent;
            searchResults_lb.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            searchResults_lb.Location = new System.Drawing.Point(12, 228);
            searchResults_lb.Name = "searchResults_lb";
            searchResults_lb.Size = new System.Drawing.Size(129, 19);
            searchResults_lb.TabIndex = 24;
            searchResults_lb.Text = "Search Results";
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
            this.Back_btn.Location = new System.Drawing.Point(22, 433);
            this.Back_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Back_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Back_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Back_btn.Name = "Back_btn";
            this.Back_btn.Size = new System.Drawing.Size(100, 50);
            this.Back_btn.TabIndex = 20;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = false;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
            // 
            // Print_btn
            // 
            this.Print_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Print_btn.BackColor = System.Drawing.Color.Transparent;
            this.Print_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Print_btn.BackgroundImage")));
            this.Print_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Print_btn.Enabled = false;
            this.Print_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Print_btn.FlatAppearance.BorderSize = 0;
            this.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Print_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Print_btn.ForeColor = System.Drawing.Color.White;
            this.Print_btn.Location = new System.Drawing.Point(608, 433);
            this.Print_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Print_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Print_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Print_btn.Name = "Print_btn";
            this.Print_btn.Size = new System.Drawing.Size(100, 50);
            this.Print_btn.TabIndex = 21;
            this.Print_btn.Text = "Print";
            this.Print_btn.UseVisualStyleBackColor = false;
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.Cancel_btn.Location = new System.Drawing.Point(727, 433);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Cancel_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.Cancel_btn.TabIndex = 22;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // resultsGrid_dg
            // 
            this.resultsGrid_dg.AllowUserToAddRows = false;
            this.resultsGrid_dg.AllowUserToDeleteRows = false;
            this.resultsGrid_dg.AllowUserToResizeColumns = false;
            this.resultsGrid_dg.AllowUserToResizeRows = false;
            this.resultsGrid_dg.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.resultsGrid_dg.CausesValidation = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.resultsGrid_dg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.resultsGrid_dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsGrid_dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ViewDetail});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.resultsGrid_dg.DefaultCellStyle = dataGridViewCellStyle7;
            this.resultsGrid_dg.GridColor = System.Drawing.Color.LightGray;
            this.resultsGrid_dg.Location = new System.Drawing.Point(16, 250);
            this.resultsGrid_dg.Margin = new System.Windows.Forms.Padding(0);
            this.resultsGrid_dg.MultiSelect = false;
            this.resultsGrid_dg.Name = "resultsGrid_dg";
            this.resultsGrid_dg.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.resultsGrid_dg.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.resultsGrid_dg.RowHeadersVisible = false;
            this.resultsGrid_dg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultsGrid_dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.resultsGrid_dg.Size = new System.Drawing.Size(811, 171);
            this.resultsGrid_dg.TabIndex = 23;
            this.resultsGrid_dg.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.resultsGrid_dg_RowEnter);
            // 
            // ViewDetail
            // 
            this.ViewDetail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle6.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle6.NullValue")));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Transparent;
            this.ViewDetail.DefaultCellStyle = dataGridViewCellStyle6;
            this.ViewDetail.FillWeight = 50F;
            this.ViewDetail.Frozen = true;
            this.ViewDetail.HeaderText = global::Pawn.Properties.Resources.OverrideMachineName;
            this.ViewDetail.Image = global::Pawn.Properties.Resources.blueglossy_select2;
            this.ViewDetail.MinimumWidth = 55;
            this.ViewDetail.Name = "ViewDetail";
            this.ViewDetail.ReadOnly = true;
            this.ViewDetail.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ViewDetail.Width = 55;
            // 
            // searchCriteria_lb
            // 
            this.searchCriteria_lb.AutoSize = true;
            this.searchCriteria_lb.BackColor = System.Drawing.Color.Transparent;
            this.searchCriteria_lb.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchCriteria_lb.Location = new System.Drawing.Point(12, 79);
            this.searchCriteria_lb.Name = "searchCriteria_lb";
            this.searchCriteria_lb.Size = new System.Drawing.Size(131, 19);
            this.searchCriteria_lb.TabIndex = 25;
            this.searchCriteria_lb.Text = "Search Criteria";
            // 
            // windowHeading_lb
            // 
            this.windowHeading_lb.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.windowHeading_lb.AutoSize = true;
            this.windowHeading_lb.BackColor = System.Drawing.Color.Transparent;
            this.windowHeading_lb.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowHeading_lb.ForeColor = System.Drawing.Color.White;
            this.windowHeading_lb.Location = new System.Drawing.Point(332, 37);
            this.windowHeading_lb.Name = "windowHeading_lb";
            this.windowHeading_lb.Size = new System.Drawing.Size(171, 19);
            this.windowHeading_lb.TabIndex = 26;
            this.windowHeading_lb.Text = "Pawn Loan Inquiry List";
            this.windowHeading_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // criteriaDetails_panel
            // 
            this.criteriaDetails_panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.criteriaDetails_panel.Controls.Add(this.criteriaSummary_txt);
            this.criteriaDetails_panel.Location = new System.Drawing.Point(22, 101);
            this.criteriaDetails_panel.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.criteriaDetails_panel.Name = "criteriaDetails_panel";
            this.criteriaDetails_panel.Size = new System.Drawing.Size(805, 104);
            this.criteriaDetails_panel.TabIndex = 27;
            // 
            // criteriaSummary_txt
            // 
            this.criteriaSummary_txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.criteriaSummary_txt.Location = new System.Drawing.Point(0, 0);
            this.criteriaSummary_txt.Multiline = true;
            this.criteriaSummary_txt.Name = "criteriaSummary_txt";
            this.criteriaSummary_txt.ReadOnly = true;
            this.criteriaSummary_txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.criteriaSummary_txt.Size = new System.Drawing.Size(801, 100);
            this.criteriaSummary_txt.TabIndex = 0;
            // 
            // NrLoans_tb
            // 
            this.NrLoans_tb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NrLoans_tb.AutoSize = true;
            this.NrLoans_tb.BackColor = System.Drawing.Color.Transparent;
            this.NrLoans_tb.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NrLoans_tb.Location = new System.Drawing.Point(710, 232);
            this.NrLoans_tb.Name = "NrLoans_tb";
            this.NrLoans_tb.Size = new System.Drawing.Size(76, 14);
            this.NrLoans_tb.TabIndex = 28;
            this.NrLoans_tb.Text = " of 0 Loans";
            // 
            // LoanCtr_tb
            // 
            this.LoanCtr_tb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoanCtr_tb.AutoSize = true;
            this.LoanCtr_tb.BackColor = System.Drawing.Color.Transparent;
            this.LoanCtr_tb.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoanCtr_tb.Location = new System.Drawing.Point(670, 232);
            this.LoanCtr_tb.Name = "LoanCtr_tb";
            this.LoanCtr_tb.Size = new System.Drawing.Size(23, 14);
            this.LoanCtr_tb.TabIndex = 29;
            this.LoanCtr_tb.Text = "  0";
            // 
            // criteriaSummary_txt
            this.criteriaSummary_txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.criteriaSummary_txt.Location = new System.Drawing.Point(0, 0);
            this.criteriaSummary_txt.Multiline = true;
            this.criteriaSummary_txt.Name = "criteriaSummary_txt";
            this.criteriaSummary_txt.ReadOnly = true;
            this.criteriaSummary_txt.Size = new System.Drawing.Size(801, 100);
            this.criteriaSummary_txt.TabIndex = 0;
            // SearchResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(849, 514);
            this.ControlBox = false;
            this.Controls.Add(this.LoanCtr_tb);
            this.Controls.Add(this.NrLoans_tb);
            this.Controls.Add(this.criteriaDetails_panel);
            this.Controls.Add(this.windowHeading_lb);
            this.Controls.Add(this.searchCriteria_lb);
            this.Controls.Add(searchResults_lb);
            this.Controls.Add(this.resultsGrid_dg);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.Print_btn);
            this.Controls.Add(this.Back_btn);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SearchResults";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.resultsGrid_dg)).EndInit();
            this.criteriaDetails_panel.ResumeLayout(false);
            this.criteriaDetails_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected CustomButton Back_btn;
        protected CustomButton Print_btn;
        protected CustomButton Cancel_btn;
        protected System.Windows.Forms.Label searchCriteria_lb;
        protected System.Windows.Forms.Label windowHeading_lb;
        protected System.Windows.Forms.Panel criteriaDetails_panel;
        protected CustomDataGridView resultsGrid_dg;
        private System.Windows.Forms.Label NrLoans_tb;
        private System.Windows.Forms.Label LoanCtr_tb;
        private System.Windows.Forms.DataGridViewImageColumn ViewDetail;
        protected System.Windows.Forms.TextBox criteriaSummary_txt;
    }
}
