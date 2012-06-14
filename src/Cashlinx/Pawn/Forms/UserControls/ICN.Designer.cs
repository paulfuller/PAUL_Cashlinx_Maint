using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;

namespace Pawn.Forms.UserControls
{
    partial class ICN
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.ICN_sub_item_tb = new System.Windows.Forms.TextBox();
            this.ICN_item_tb = new System.Windows.Forms.TextBox();
            this.ICN_doc_type_tb = new System.Windows.Forms.TextBox();
            this.ICN_doc_tb = new System.Windows.Forms.TextBox();
            this.ICN_year_tb = new System.Windows.Forms.TextBox();
            this.ICN_shop_tb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ICN_sub_item_tb
            // 
            this.ICN_sub_item_tb.Location = new System.Drawing.Point(335, 10);
            this.ICN_sub_item_tb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ICN_sub_item_tb.MaxLength = 2;
            this.ICN_sub_item_tb.Name = "ICN_sub_item_tb";
            this.ICN_sub_item_tb.Size = new System.Drawing.Size(31, 26);
            this.ICN_sub_item_tb.TabIndex = 30;
            // 
            // ICN_item_tb
            // 
            this.ICN_item_tb.Location = new System.Drawing.Point(286, 10);
            this.ICN_item_tb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ICN_item_tb.MaxLength = 3;
            this.ICN_item_tb.Name = "ICN_item_tb";
            this.ICN_item_tb.Size = new System.Drawing.Size(38, 26);
            this.ICN_item_tb.TabIndex = 25;
            // 
            // ICN_doc_type_tb
            // 
            this.ICN_doc_type_tb.Location = new System.Drawing.Point(246, 10);
            this.ICN_doc_type_tb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ICN_doc_type_tb.MaxLength = 1;
            this.ICN_doc_type_tb.Name = "ICN_doc_type_tb";
            this.ICN_doc_type_tb.Size = new System.Drawing.Size(26, 26);
            this.ICN_doc_type_tb.TabIndex = 20;
            // 
            // ICN_doc_tb
            // 
            this.ICN_doc_tb.Location = new System.Drawing.Point(164, 10);
            this.ICN_doc_tb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ICN_doc_tb.MaxLength = 6;
            this.ICN_doc_tb.Name = "ICN_doc_tb";
            this.ICN_doc_tb.Size = new System.Drawing.Size(68, 26);
            this.ICN_doc_tb.TabIndex = 15;
            // 
            // ICN_year_tb
            // 
            this.ICN_year_tb.Location = new System.Drawing.Point(126, 10);
            this.ICN_year_tb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ICN_year_tb.MaxLength = 1;
            this.ICN_year_tb.Name = "ICN_year_tb";
            this.ICN_year_tb.Size = new System.Drawing.Size(26, 26);
            this.ICN_year_tb.TabIndex = 10;
            this.ICN_year_tb.TextChanged += new System.EventHandler(this.ICN_year_tb_TextChanged);
            // 
            // ICN_shop_tb
            // 
            this.ICN_shop_tb.Location = new System.Drawing.Point(59, 10);
            this.ICN_shop_tb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ICN_shop_tb.MaxLength = 5;
            this.ICN_shop_tb.Name = "ICN_shop_tb";
            this.ICN_shop_tb.Size = new System.Drawing.Size(54, 26);
            this.ICN_shop_tb.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "ICN:";
            // 
            // ICN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ICN_sub_item_tb);
            this.Controls.Add(this.ICN_item_tb);
            this.Controls.Add(this.ICN_doc_type_tb);
            this.Controls.Add(this.ICN_doc_tb);
            this.Controls.Add(this.ICN_year_tb);
            this.Controls.Add(this.ICN_shop_tb);
            this.Controls.Add(this.label1);
            this.Name = "ICN";
            this.Size = new System.Drawing.Size(380, 48);
            this.Load += new System.EventHandler(this.ICN_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ICN_sub_item_tb;
        private System.Windows.Forms.TextBox ICN_item_tb;
        private System.Windows.Forms.TextBox ICN_doc_type_tb;
        private System.Windows.Forms.TextBox ICN_doc_tb;
        private System.Windows.Forms.TextBox ICN_year_tb;
        private System.Windows.Forms.TextBox ICN_shop_tb;
        private System.Windows.Forms.Label label1;

    }
}
