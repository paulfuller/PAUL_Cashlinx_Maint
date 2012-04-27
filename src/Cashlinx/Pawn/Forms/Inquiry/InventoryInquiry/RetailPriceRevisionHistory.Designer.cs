using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    partial class RetailPriceRevisionHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RetailPriceRevisionHistory));
            this.labelMerchandiseDescription = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelICN = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.windowHeading_lb = new System.Windows.Forms.Label();
            this.resultsGrid_dg = new CustomDataGridView();
            this.Back_btn = new CustomButton();
            this.Cancel_btn = new CustomButton();
            this.Print_btn = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.resultsGrid_dg)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMerchandiseDescription
            // 
            this.labelMerchandiseDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelMerchandiseDescription.Location = new System.Drawing.Point(445, 85);
            this.labelMerchandiseDescription.Name = "labelMerchandiseDescription";
            this.labelMerchandiseDescription.Size = new System.Drawing.Size(392, 44);
            this.labelMerchandiseDescription.TabIndex = 61;
            this.labelMerchandiseDescription.Text = "Description of the Merchandise; MORE DESC; LESS DESC; RANDOM TEXT; MORE RANDOM ST" +
    "UFF; WOO HOO!!; SHIP IT!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(267, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 16);
            this.label3.TabIndex = 60;
            this.label3.Text = "Merchandise Description:";
            // 
            // labelICN
            // 
            this.labelICN.AutoSize = true;
            this.labelICN.BackColor = System.Drawing.Color.Transparent;
            this.labelICN.Location = new System.Drawing.Point(62, 87);
            this.labelICN.Name = "labelICN";
            this.labelICN.Size = new System.Drawing.Size(130, 13);
            this.labelICN.TabIndex = 59;
            this.labelICN.Text = "00000 4 22222 3 444 555";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 58;
            this.label1.Text = "ICN:";
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
            this.windowHeading_lb.Size = new System.Drawing.Size(145, 19);
            this.windowHeading_lb.TabIndex = 57;
            this.windowHeading_lb.Text = "Retail Price Change";
            this.windowHeading_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resultsGrid_dg
            // 
            this.resultsGrid_dg.AllowUserToAddRows = false;
            this.resultsGrid_dg.AllowUserToDeleteRows = false;
            this.resultsGrid_dg.AllowUserToResizeColumns = false;
            this.resultsGrid_dg.AllowUserToResizeRows = false;
            this.resultsGrid_dg.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.resultsGrid_dg.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.resultsGrid_dg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.resultsGrid_dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.resultsGrid_dg.DefaultCellStyle = dataGridViewCellStyle2;
            this.resultsGrid_dg.GridColor = System.Drawing.Color.LightGray;
            this.resultsGrid_dg.Location = new System.Drawing.Point(16, 132);
            this.resultsGrid_dg.Margin = new System.Windows.Forms.Padding(0);
            this.resultsGrid_dg.MultiSelect = false;
            this.resultsGrid_dg.Name = "resultsGrid_dg";
            this.resultsGrid_dg.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.resultsGrid_dg.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.resultsGrid_dg.RowHeadersVisible = false;
            this.resultsGrid_dg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultsGrid_dg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.resultsGrid_dg.Size = new System.Drawing.Size(811, 289);
            this.resultsGrid_dg.TabIndex = 56;
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
            this.Back_btn.TabIndex = 53;
            this.Back_btn.Text = "Back";
            this.Back_btn.UseVisualStyleBackColor = false;
            this.Back_btn.Click += new System.EventHandler(this.Back_btn_Click);
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
            this.Cancel_btn.Location = new System.Drawing.Point(122, 433);
            this.Cancel_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Cancel_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(100, 50);
            this.Cancel_btn.TabIndex = 55;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // Print_btn
            // 
            this.Print_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Print_btn.BackColor = System.Drawing.Color.Transparent;
            this.Print_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Print_btn.BackgroundImage")));
            this.Print_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Print_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Print_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Print_btn.FlatAppearance.BorderSize = 0;
            this.Print_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Print_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Print_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Print_btn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Print_btn.ForeColor = System.Drawing.Color.White;
            this.Print_btn.Location = new System.Drawing.Point(727, 433);
            this.Print_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Print_btn.MaximumSize = new System.Drawing.Size(100, 50);
            this.Print_btn.MinimumSize = new System.Drawing.Size(100, 50);
            this.Print_btn.Name = "Print_btn";
            this.Print_btn.Size = new System.Drawing.Size(100, 50);
            this.Print_btn.TabIndex = 54;
            this.Print_btn.Text = "Print";
            this.Print_btn.UseVisualStyleBackColor = false;
            this.Print_btn.Click += new System.EventHandler(this.Print_btn_Click);
            // 
            // RetailPriceRevisionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(849, 514);
            this.ControlBox = false;
            this.Controls.Add(this.labelMerchandiseDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelICN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.windowHeading_lb);
            this.Controls.Add(this.resultsGrid_dg);
            this.Controls.Add(this.Back_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.Print_btn);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RetailPriceRevisionHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RetailPriceRevisionHistory";
            this.Load += new System.EventHandler(this.ItemCostRevisionHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultsGrid_dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMerchandiseDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelICN;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Label windowHeading_lb;
        protected CustomDataGridView resultsGrid_dg;
        protected CustomButton Back_btn;
        protected CustomButton Cancel_btn;
        protected CustomButton Print_btn;
    }
}