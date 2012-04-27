using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Customer
{
    partial class UpdateCommentsandNotes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateCommentsandNotes));
            this.labelCommentsHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customDataGridViewComments = new CustomDataGridView();
            this.shop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateandTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Enteredby = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustProdNoteId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customButtonSubmit = new CustomButton();
            this.customButtonReset = new CustomButton();
            this.customButtonClose = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.customButtonAdd = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewComments)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCommentsHeading
            // 
            this.labelCommentsHeading.AutoSize = true;
            this.labelCommentsHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelCommentsHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCommentsHeading.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelCommentsHeading.Location = new System.Drawing.Point(13, 22);
            this.labelCommentsHeading.Name = "labelCommentsHeading";
            this.labelCommentsHeading.Size = new System.Drawing.Size(128, 16);
            this.labelCommentsHeading.TabIndex = 0;
            this.labelCommentsHeading.Text = "Comments / Notes";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 389);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(751, 1);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // customDataGridViewComments
            // 
            this.customDataGridViewComments.AllowUserToAddRows = false;
            this.customDataGridViewComments.AllowUserToDeleteRows = false;
            this.customDataGridViewComments.AllowUserToResizeColumns = false;
            this.customDataGridViewComments.AllowUserToResizeRows = false;
            this.customDataGridViewComments.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewComments.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewComments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridViewComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewComments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.shop,
            this.DateandTime,
            this.Comment,
            this.Enteredby,
            this.CustProdNoteId});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewComments.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridViewComments.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewComments.Location = new System.Drawing.Point(51, 88);
            this.customDataGridViewComments.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewComments.Name = "customDataGridViewComments";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewComments.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridViewComments.RowHeadersVisible = false;
            this.customDataGridViewComments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewComments.Size = new System.Drawing.Size(606, 266);
            this.customDataGridViewComments.TabIndex = 54;
            this.customDataGridViewComments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComments_CellClick);
            this.customDataGridViewComments.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComments_CellEnter);
            // 
            // shop
            // 
            this.shop.HeaderText = "Shop";
            this.shop.Name = "shop";
            this.shop.ReadOnly = true;
            this.shop.Width = 71;
            // 
            // DateandTime
            // 
            this.DateandTime.HeaderText = "Date/Time";
            this.DateandTime.Name = "DateandTime";
            this.DateandTime.ReadOnly = true;
            this.DateandTime.Width = 140;
            // 
            // Comment
            // 
            this.Comment.HeaderText = "Comments/Notes";
            this.Comment.Name = "Comment";
            this.Comment.Width = 300;
            // 
            // Enteredby
            // 
            this.Enteredby.HeaderText = "User ID";
            this.Enteredby.Name = "Enteredby";
            this.Enteredby.ReadOnly = true;
            this.Enteredby.Width = 80;
            // 
            // CustProdNoteId
            // 
            this.CustProdNoteId.HeaderText = "custprodnoteid";
            this.CustProdNoteId.Name = "CustProdNoteId";
            this.CustProdNoteId.ReadOnly = true;
            this.CustProdNoteId.Visible = false;
            this.CustProdNoteId.Width = 40;
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSubmit.BackgroundImage")));
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(643, 405);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 53;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // customButtonReset
            // 
            this.customButtonReset.BackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonReset.BackgroundImage")));
            this.customButtonReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonReset.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonReset.FlatAppearance.BorderSize = 0;
            this.customButtonReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonReset.ForeColor = System.Drawing.Color.White;
            this.customButtonReset.Location = new System.Drawing.Point(526, 405);
            this.customButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReset.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.Name = "customButtonReset";
            this.customButtonReset.Size = new System.Drawing.Size(100, 50);
            this.customButtonReset.TabIndex = 52;
            this.customButtonReset.Text = "&Reset";
            this.customButtonReset.UseVisualStyleBackColor = false;
            this.customButtonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // customButtonClose
            // 
            this.customButtonClose.BackColor = System.Drawing.Color.Transparent;
            this.customButtonClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonClose.BackgroundImage")));
            this.customButtonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonClose.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonClose.FlatAppearance.BorderSize = 0;
            this.customButtonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonClose.ForeColor = System.Drawing.Color.White;
            this.customButtonClose.Location = new System.Drawing.Point(316, 405);
            this.customButtonClose.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClose.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClose.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClose.Name = "customButtonClose";
            this.customButtonClose.Size = new System.Drawing.Size(100, 50);
            this.customButtonClose.TabIndex = 51;
            this.customButtonClose.Text = "C&lose";
            this.customButtonClose.UseVisualStyleBackColor = false;
            this.customButtonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // customButtonCancel
            // 
            this.customButtonCancel.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel.BackgroundImage")));
            this.customButtonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel.FlatAppearance.BorderSize = 0;
            this.customButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel.Location = new System.Drawing.Point(27, 405);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 50;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // customButtonAdd
            // 
            this.customButtonAdd.BackColor = System.Drawing.Color.Transparent;
            this.customButtonAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonAdd.BackgroundImage")));
            this.customButtonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonAdd.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonAdd.FlatAppearance.BorderSize = 0;
            this.customButtonAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonAdd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonAdd.ForeColor = System.Drawing.Color.White;
            this.customButtonAdd.Location = new System.Drawing.Point(426, 405);
            this.customButtonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAdd.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.Name = "customButtonAdd";
            this.customButtonAdd.Size = new System.Drawing.Size(100, 50);
            this.customButtonAdd.TabIndex = 55;
            this.customButtonAdd.Text = "&Add";
            this.customButtonAdd.UseVisualStyleBackColor = false;
            this.customButtonAdd.Click += new System.EventHandler(this.customButtonAdd_Click);
            // 
            // UpdateCommentsandNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_480_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(762, 468);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonAdd);
            this.Controls.Add(this.customDataGridViewComments);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonReset);
            this.Controls.Add(this.customButtonClose);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelCommentsHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateCommentsandNotes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewCommentsandNotes";
            this.Load += new System.EventHandler(this.UpdateCommentsandNotes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewComments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCommentsHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomButton customButtonCancel;
        private CustomButton customButtonClose;
        private CustomButton customButtonReset;
        private CustomButton customButtonSubmit;
        private CustomDataGridView customDataGridViewComments;
        private System.Windows.Forms.DataGridViewTextBoxColumn shop;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateandTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Enteredby;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustProdNoteId;
        private CustomButton customButtonAdd;
    }
}
