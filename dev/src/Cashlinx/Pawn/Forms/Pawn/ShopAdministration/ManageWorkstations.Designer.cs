using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class ManageWorkstations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageWorkstations));
            this.customDataGridViewWorkstations = new CustomDataGridView();
            this.WorkstationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoreNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelHeading = new System.Windows.Forms.Label();
            this.customButtonAdd = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.customButtonCancel = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewWorkstations)).BeginInit();
            this.SuspendLayout();
            // 
            // customDataGridViewWorkstations
            // 
            this.customDataGridViewWorkstations.AllowUserToAddRows = false;
            this.customDataGridViewWorkstations.AllowUserToDeleteRows = false;
            this.customDataGridViewWorkstations.AllowUserToResizeColumns = false;
            this.customDataGridViewWorkstations.AllowUserToResizeRows = false;
            this.customDataGridViewWorkstations.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.customDataGridViewWorkstations.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewWorkstations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridViewWorkstations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewWorkstations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WorkstationName,
            this.Status,
            this.StoreNumber});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewWorkstations.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridViewWorkstations.GridColor = System.Drawing.Color.LightGray;
            this.customDataGridViewWorkstations.Location = new System.Drawing.Point(85, 91);
            this.customDataGridViewWorkstations.Margin = new System.Windows.Forms.Padding(0);
            this.customDataGridViewWorkstations.Name = "customDataGridViewWorkstations";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewWorkstations.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridViewWorkstations.RowHeadersVisible = false;
            this.customDataGridViewWorkstations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.customDataGridViewWorkstations.Size = new System.Drawing.Size(463, 266);
            this.customDataGridViewWorkstations.TabIndex = 1;
            this.customDataGridViewWorkstations.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridViewWorkstations_CellEnter);
            // 
            // WorkstationName
            // 
            this.WorkstationName.HeaderText = "Workstation Name";
            this.WorkstationName.Name = "WorkstationName";
            this.WorkstationName.Width = 200;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Width = 150;
            // 
            // StoreNumber
            // 
            this.StoreNumber.HeaderText = "Store Number";
            this.StoreNumber.Name = "StoreNumber";
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(243, 29);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(159, 19);
            this.labelHeading.TabIndex = 2;
            this.labelHeading.Text = "Manage Workstations";
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
            this.customButtonAdd.Location = new System.Drawing.Point(321, 371);
            this.customButtonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAdd.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.Name = "customButtonAdd";
            this.customButtonAdd.Size = new System.Drawing.Size(100, 50);
            this.customButtonAdd.TabIndex = 3;
            this.customButtonAdd.Text = "Add";
            this.customButtonAdd.UseVisualStyleBackColor = false;
            this.customButtonAdd.Click += new System.EventHandler(this.customButtonAdd_Click);
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
            this.customButtonSubmit.Location = new System.Drawing.Point(448, 371);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 4;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(85, 371);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 5;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // ManageWorkstations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 442);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonAdd);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(this.customDataGridViewWorkstations);
            this.Name = "ManageWorkstations";
            this.Text = "ManageWorkstations";
            this.Load += new System.EventHandler(this.ManageWorkstations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewWorkstations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomDataGridView customDataGridViewWorkstations;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkstationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoreNumber;
        private System.Windows.Forms.Label labelHeading;
        private CustomButton customButtonAdd;
        private CustomButton customButtonSubmit;
        private CustomButton customButtonCancel;
    }
}