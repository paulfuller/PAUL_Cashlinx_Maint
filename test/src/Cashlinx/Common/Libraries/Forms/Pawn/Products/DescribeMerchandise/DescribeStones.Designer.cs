using Common.Libraries.Forms.Components;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    partial class DescribeStones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DescribeStones));
            this.label6 = new System.Windows.Forms.Label();
            this.gvEditStones = new System.Windows.Forms.DataGridView();
            this.colDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.continueEditStonesButton = new System.Windows.Forms.Button();
            this.deleteEditStonesButton = new System.Windows.Forms.Button();
            this.cancelEditStonesButton = new System.Windows.Forms.Button();
            this.customButtonAdd = new CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.gvEditStones)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(34, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 19);
            this.label6.TabIndex = 40;
            this.label6.Text = "Describe Stones";
            // 
            // gvEditStones
            // 
            this.gvEditStones.AllowUserToAddRows = false;
            this.gvEditStones.AllowUserToResizeColumns = false;
            this.gvEditStones.AllowUserToResizeRows = false;
            this.gvEditStones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvEditStones.BackgroundColor = System.Drawing.Color.FromArgb(51, 153, 255);
            this.gvEditStones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvEditStones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvEditStones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvEditStones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDelete});
            this.gvEditStones.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvEditStones.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvEditStones.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvEditStones.GridColor = System.Drawing.Color.White;
            this.gvEditStones.Location = new System.Drawing.Point(11, 61);
            this.gvEditStones.MultiSelect = false;
            this.gvEditStones.Name = "gvEditStones";
            this.gvEditStones.RowHeadersVisible = false;
            this.gvEditStones.RowHeadersWidth = 20;
            this.gvEditStones.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gvEditStones.Size = new System.Drawing.Size(669, 215);
            this.gvEditStones.TabIndex = 1;
            this.gvEditStones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEditStones_CellClick);
            this.gvEditStones.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEditStones_CellContentClick);
            this.gvEditStones.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEditStones_CellEndEdit);
            this.gvEditStones.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEditStones_CellLeave);
            this.gvEditStones.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvEditStones_CellMouseLeave);
            this.gvEditStones.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gvEditStones_CellValidating);
            this.gvEditStones.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvEditStones_ColumnHeaderMouseClick);
            this.gvEditStones.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gvEditStones_EditingControlShowing);
            // 
            // colDelete
            // 
            this.colDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colDelete.Name = "colDelete";
            this.colDelete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colDelete.Width = 25;
            // 
            // continueEditStonesButton
            // 
            this.continueEditStonesButton.BackColor = System.Drawing.Color.Transparent;
            this.continueEditStonesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.continueEditStonesButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.continueEditStonesButton.FlatAppearance.BorderSize = 0;
            this.continueEditStonesButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueEditStonesButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueEditStonesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueEditStonesButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueEditStonesButton.ForeColor = System.Drawing.Color.White;
            this.continueEditStonesButton.Location = new System.Drawing.Point(580, 282);
            this.continueEditStonesButton.Margin = new System.Windows.Forms.Padding(4);
            this.continueEditStonesButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.continueEditStonesButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.continueEditStonesButton.Name = "continueEditStonesButton";
            this.continueEditStonesButton.Size = new System.Drawing.Size(100, 50);
            this.continueEditStonesButton.TabIndex = 39;
            this.continueEditStonesButton.Text = "Continue";
            this.continueEditStonesButton.UseVisualStyleBackColor = false;
            this.continueEditStonesButton.Click += new System.EventHandler(this.continueEditStonesButton_Click);
            // 
            // deleteEditStonesButton
            // 
            this.deleteEditStonesButton.BackColor = System.Drawing.Color.Transparent;
            this.deleteEditStonesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.deleteEditStonesButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.deleteEditStonesButton.FlatAppearance.BorderSize = 0;
            this.deleteEditStonesButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.deleteEditStonesButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.deleteEditStonesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteEditStonesButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteEditStonesButton.ForeColor = System.Drawing.Color.White;
            this.deleteEditStonesButton.Location = new System.Drawing.Point(107, 282);
            this.deleteEditStonesButton.Margin = new System.Windows.Forms.Padding(4);
            this.deleteEditStonesButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.deleteEditStonesButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.deleteEditStonesButton.Name = "deleteEditStonesButton";
            this.deleteEditStonesButton.Size = new System.Drawing.Size(100, 50);
            this.deleteEditStonesButton.TabIndex = 38;
            this.deleteEditStonesButton.Text = "Delete";
            this.deleteEditStonesButton.UseVisualStyleBackColor = false;
            this.deleteEditStonesButton.Click += new System.EventHandler(this.deleteEditStonesButton_Click);
            // 
            // cancelEditStonesButton
            // 
            this.cancelEditStonesButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelEditStonesButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelEditStonesButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelEditStonesButton.FlatAppearance.BorderSize = 0;
            this.cancelEditStonesButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelEditStonesButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelEditStonesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelEditStonesButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelEditStonesButton.ForeColor = System.Drawing.Color.White;
            this.cancelEditStonesButton.Location = new System.Drawing.Point(11, 282);
            this.cancelEditStonesButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelEditStonesButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelEditStonesButton.Name = "cancelEditStonesButton";
            this.cancelEditStonesButton.Size = new System.Drawing.Size(100, 50);
            this.cancelEditStonesButton.TabIndex = 37;
            this.cancelEditStonesButton.Text = "Cancel";
            this.cancelEditStonesButton.UseVisualStyleBackColor = false;
            this.cancelEditStonesButton.Click += new System.EventHandler(this.cancelEditStonesButton_Click);
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
            this.customButtonAdd.Location = new System.Drawing.Point(202, 282);
            this.customButtonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAdd.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.Name = "customButtonAdd";
            this.customButtonAdd.Size = new System.Drawing.Size(100, 50);
            this.customButtonAdd.TabIndex = 56;
            this.customButtonAdd.Text = "&Add";
            this.customButtonAdd.UseVisualStyleBackColor = false;
            this.customButtonAdd.Click += new System.EventHandler(this.customButtonAdd_Click);
            // 
            // DescribeStones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(691, 345);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonAdd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gvEditStones);
            this.Controls.Add(this.continueEditStonesButton);
            this.Controls.Add(this.deleteEditStonesButton);
            this.Controls.Add(this.cancelEditStonesButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DescribeStones";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Describe Stones";
            this.Load += new System.EventHandler(this.DescribeStones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvEditStones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView gvEditStones;
        private System.Windows.Forms.Button continueEditStonesButton;
        private System.Windows.Forms.Button deleteEditStonesButton;
        private System.Windows.Forms.Button cancelEditStonesButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDelete;
        private CustomButton customButtonAdd;
    }
}
