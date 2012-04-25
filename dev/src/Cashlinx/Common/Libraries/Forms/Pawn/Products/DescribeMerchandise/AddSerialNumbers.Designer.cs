using Common.Libraries.Forms.Components;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    partial class AddSerialNumbers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSerialNumbers));
            this.labelHeading = new System.Windows.Forms.Label();
            this.dataGridViewSerialNos = new System.Windows.Forms.DataGridView();
            this.customButtonCancel = new CustomButton();
            this.customButtonContinue = new CustomButton();
            this.itemnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSerialNos)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(232, 43);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(159, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Enter Serial Numbers";
            // 
            // dataGridViewSerialNos
            // 
            this.dataGridViewSerialNos.AllowUserToAddRows = false;
            this.dataGridViewSerialNos.AllowUserToDeleteRows = false;
            this.dataGridViewSerialNos.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewSerialNos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSerialNos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemnumber,
            this.SerialNumber});
            this.dataGridViewSerialNos.Location = new System.Drawing.Point(73, 132);
            this.dataGridViewSerialNos.MultiSelect = false;
            this.dataGridViewSerialNos.Name = "dataGridViewSerialNos";
            this.dataGridViewSerialNos.RowHeadersVisible = false;
            this.dataGridViewSerialNos.Size = new System.Drawing.Size(421, 150);
            this.dataGridViewSerialNos.TabIndex = 1;
            this.dataGridViewSerialNos.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSerialNos_CellLeave);
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
            this.customButtonCancel.Location = new System.Drawing.Point(53, 412);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 2;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonContinue
            // 
            this.customButtonContinue.BackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonContinue.BackgroundImage")));
            this.customButtonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonContinue.FlatAppearance.BorderSize = 0;
            this.customButtonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonContinue.ForeColor = System.Drawing.Color.White;
            this.customButtonContinue.Location = new System.Drawing.Point(423, 412);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 3;
            this.customButtonContinue.Text = "Continue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Click += new System.EventHandler(this.customButtonContinue_Click);
            // 
            // itemnumber
            // 
            this.itemnumber.HeaderText = "Item #";
            this.itemnumber.Name = "itemnumber";
            this.itemnumber.ReadOnly = true;
            this.itemnumber.Width = 150;
            // 
            // SerialNumber
            // 
            this.SerialNumber.HeaderText = "Serial #";
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.Width = 250;
            // 
            // AddSerialNumbers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 491);
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.dataGridViewSerialNos);
            this.Controls.Add(this.labelHeading);
            this.Name = "AddSerialNumbers";
            this.Text = "AddSerialNumbers";
            this.Load += new System.EventHandler(this.AddSerialNumbers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSerialNos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.DataGridView dataGridViewSerialNos;
        private CustomButton customButtonCancel;
        private CustomButton customButtonContinue;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
    }
}