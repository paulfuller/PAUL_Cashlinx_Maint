using Common.Libraries.Forms.Components;
//Odd file lock

namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class CashDrawerStatus
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashDrawerStatus));
            this.dataGridViewCashDrawerStatus = new System.Windows.Forms.DataGridView();
            this.cdid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.drawernumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelHeading = new System.Windows.Forms.Label();
            this.customButtonCancel = new CustomButton();
            this.customButtonBalance = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCashDrawerStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCashDrawerStatus
            // 
            this.dataGridViewCashDrawerStatus.AllowUserToAddRows = false;
            this.dataGridViewCashDrawerStatus.AllowUserToDeleteRows = false;
            this.dataGridViewCashDrawerStatus.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCashDrawerStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCashDrawerStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCashDrawerStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cdid,
            this.drawernumber,
            this.status});
            this.dataGridViewCashDrawerStatus.Location = new System.Drawing.Point(52, 81);
            this.dataGridViewCashDrawerStatus.Name = "dataGridViewCashDrawerStatus";
            this.dataGridViewCashDrawerStatus.RowHeadersVisible = false;
            this.dataGridViewCashDrawerStatus.Size = new System.Drawing.Size(411, 224);
            this.dataGridViewCashDrawerStatus.TabIndex = 1;
            this.dataGridViewCashDrawerStatus.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCashDrawerStatus_CellClick);
            // 
            // cdid
            // 
            this.cdid.HeaderText = "ID";
            this.cdid.Name = "cdid";
            this.cdid.ReadOnly = true;
            this.cdid.Visible = false;
            // 
            // drawernumber
            // 
            this.drawernumber.HeaderText = "Drawer #";
            this.drawernumber.Name = "drawernumber";
            this.drawernumber.ReadOnly = true;
            this.drawernumber.Width = 250;
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 150;
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(129, 24);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(149, 19);
            this.labelHeading.TabIndex = 2;
            this.labelHeading.Text = "Open Cash Drawers";
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
            this.customButtonCancel.Location = new System.Drawing.Point(52, 327);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 3;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonBalance
            // 
            this.customButtonBalance.BackColor = System.Drawing.Color.Transparent;
            this.customButtonBalance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonBalance.BackgroundImage")));
            this.customButtonBalance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonBalance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonBalance.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonBalance.FlatAppearance.BorderSize = 0;
            this.customButtonBalance.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonBalance.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonBalance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonBalance.ForeColor = System.Drawing.Color.White;
            this.customButtonBalance.Location = new System.Drawing.Point(378, 327);
            this.customButtonBalance.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBalance.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBalance.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBalance.Name = "customButtonBalance";
            this.customButtonBalance.Size = new System.Drawing.Size(100, 50);
            this.customButtonBalance.TabIndex = 4;
            this.customButtonBalance.Text = "Balance";
            this.customButtonBalance.UseVisualStyleBackColor = false;
            this.customButtonBalance.Click += new System.EventHandler(this.customButtonBalance_Click);
            // 
            // CashDrawerStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 386);
            this.Controls.Add(this.customButtonBalance);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.labelHeading);
            this.Controls.Add(this.dataGridViewCashDrawerStatus);
            this.Name = "CashDrawerStatus";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.CashDrawerStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCashDrawerStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCashDrawerStatus;
        private System.Windows.Forms.Label labelHeading;
        private CustomButton customButtonCancel;
        private CustomButton customButtonBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn cdid;
        private System.Windows.Forms.DataGridViewTextBoxColumn drawernumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}