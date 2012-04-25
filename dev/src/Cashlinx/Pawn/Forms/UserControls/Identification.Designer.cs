namespace Pawn.Forms.UserControls
{
    partial class Identification
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewCustomerID = new System.Windows.Forms.DataGridView();
            this.Ident_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ident_Issuer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ident_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ident_Exp_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ident_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomerID)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCustomerID
            // 
            this.dataGridViewCustomerID.AllowUserToDeleteRows = false;
            this.dataGridViewCustomerID.AllowUserToResizeColumns = false;
            this.dataGridViewCustomerID.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridViewCustomerID.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCustomerID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dataGridViewCustomerID.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewCustomerID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewCustomerID.CausesValidation = false;
            this.dataGridViewCustomerID.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCustomerID.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCustomerID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomerID.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ident_Type,
            this.Ident_Issuer,
            this.Ident_Number,
            this.Ident_Exp_Date,
            this.Ident_Id});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCustomerID.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewCustomerID.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewCustomerID.GridColor = System.Drawing.Color.LightGray;
            this.dataGridViewCustomerID.Location = new System.Drawing.Point(2, -1);
            this.dataGridViewCustomerID.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewCustomerID.MultiSelect = false;
            this.dataGridViewCustomerID.Name = "dataGridViewCustomerID";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCustomerID.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewCustomerID.RowHeadersVisible = false;
            this.dataGridViewCustomerID.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewCustomerID.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewCustomerID.Size = new System.Drawing.Size(529, 105);
            this.dataGridViewCustomerID.TabIndex = 0;
            this.dataGridViewCustomerID.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCustomerID_CellValueChanged);
            this.dataGridViewCustomerID.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCustomerID_CellLeave);
            this.dataGridViewCustomerID.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCustomerID_CellClick);
            this.dataGridViewCustomerID.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCustomerID_CellEnter);
            this.dataGridViewCustomerID.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCustomerID_CellContentClick);
            // 
            // Ident_Type
            // 
            this.Ident_Type.FillWeight = 19.10344F;
            this.Ident_Type.HeaderText = "Identification Type";
            this.Ident_Type.Name = "Ident_Type";
            this.Ident_Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Ident_Type.Width = 150;
            // 
            // Ident_Issuer
            // 
            this.Ident_Issuer.FillWeight = 76.25954F;
            this.Ident_Issuer.HeaderText = "Agency";
            this.Ident_Issuer.Name = "Ident_Issuer";
            this.Ident_Issuer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Ident_Issuer.Width = 150;
            // 
            // Ident_Number
            // 
            this.Ident_Number.FillWeight = 152.3528F;
            this.Ident_Number.HeaderText = "Number";
            this.Ident_Number.Name = "Ident_Number";
            this.Ident_Number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Ident_Number.Width = 120;
            // 
            // Ident_Exp_Date
            // 
            this.Ident_Exp_Date.FillWeight = 152.2843F;
            this.Ident_Exp_Date.HeaderText = "Expiration";
            this.Ident_Exp_Date.Name = "Ident_Exp_Date";
            this.Ident_Exp_Date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Ident_Id
            // 
            this.Ident_Id.HeaderText = "Identification ID";
            this.Ident_Id.Name = "Ident_Id";
            this.Ident_Id.Visible = false;
            this.Ident_Id.Width = 25;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Identification Type";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Issuer";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "ID Number";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Expiration";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 75;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Identification ID";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // Identification
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CausesValidation = false;
            this.Controls.Add(this.dataGridViewCustomerID);
            this.DoubleBuffered = true;
            this.Name = "Identification";
            this.Size = new System.Drawing.Size(531, 105);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomerID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.DataGridView dataGridViewCustomerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ident_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ident_Issuer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ident_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ident_Exp_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ident_Id;
    }
}
