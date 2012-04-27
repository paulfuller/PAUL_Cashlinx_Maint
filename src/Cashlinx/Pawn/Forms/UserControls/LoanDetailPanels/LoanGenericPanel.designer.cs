using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Forms.UserControls.LoanDetailPanels
{
    partial class LoanGenericPanel
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
            this.PH_Generic1Text = new System.Windows.Forms.Label();
            this.PH_Generic1Label = new System.Windows.Forms.Label();
            this.PH_Generic2Text = new System.Windows.Forms.Label();
            this.PH_Generic2Label = new System.Windows.Forms.Label();
            this.PH_Generic3Text = new System.Windows.Forms.Label();
            this.PH_Generic3Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PH_Generic1Text
            // 
            this.PH_Generic1Text.BackColor = System.Drawing.Color.Transparent;
            this.PH_Generic1Text.ForeColor = System.Drawing.Color.Black;
            this.PH_Generic1Text.Location = new System.Drawing.Point(193, 3);
            this.PH_Generic1Text.Name = "PH_Generic1Text";
            this.PH_Generic1Text.Size = new System.Drawing.Size(86, 30);
            this.PH_Generic1Text.TabIndex = 130;
            this.PH_Generic1Text.Text = "Value1";
            this.PH_Generic1Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PH_Generic1Label
            // 
            this.PH_Generic1Label.BackColor = System.Drawing.Color.Transparent;
            this.PH_Generic1Label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_Generic1Label.ForeColor = System.Drawing.Color.Black;
            this.PH_Generic1Label.Location = new System.Drawing.Point(3, 3);
            this.PH_Generic1Label.Name = "PH_Generic1Label";
            this.PH_Generic1Label.Size = new System.Drawing.Size(184, 30);
            this.PH_Generic1Label.TabIndex = 129;
            this.PH_Generic1Label.Text = "Field1:";
            this.PH_Generic1Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PH_Generic2Text
            // 
            this.PH_Generic2Text.BackColor = System.Drawing.Color.Transparent;
            this.PH_Generic2Text.ForeColor = System.Drawing.Color.Black;
            this.PH_Generic2Text.Location = new System.Drawing.Point(193, 33);
            this.PH_Generic2Text.Name = "PH_Generic2Text";
            this.PH_Generic2Text.Size = new System.Drawing.Size(86, 30);
            this.PH_Generic2Text.TabIndex = 132;
            this.PH_Generic2Text.Text = "Value2";
            this.PH_Generic2Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PH_Generic2Text.Visible = false;
            // 
            // PH_Generic2Label
            // 
            this.PH_Generic2Label.BackColor = System.Drawing.Color.Transparent;
            this.PH_Generic2Label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_Generic2Label.ForeColor = System.Drawing.Color.Black;
            this.PH_Generic2Label.Location = new System.Drawing.Point(6, 33);
            this.PH_Generic2Label.Name = "PH_Generic2Label";
            this.PH_Generic2Label.Size = new System.Drawing.Size(181, 30);
            this.PH_Generic2Label.TabIndex = 131;
            this.PH_Generic2Label.Text = "Field2:";
            this.PH_Generic2Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PH_Generic2Label.Visible = false;
            // 
            // PH_Generic3Text
            // 
            this.PH_Generic3Text.BackColor = System.Drawing.Color.Transparent;
            this.PH_Generic3Text.ForeColor = System.Drawing.Color.Black;
            this.PH_Generic3Text.Location = new System.Drawing.Point(193, 63);
            this.PH_Generic3Text.Name = "PH_Generic3Text";
            this.PH_Generic3Text.Size = new System.Drawing.Size(86, 30);
            this.PH_Generic3Text.TabIndex = 134;
            this.PH_Generic3Text.Text = "Value3";
            this.PH_Generic3Text.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PH_Generic3Text.Visible = false;
            // 
            // PH_Generic3Label
            // 
            this.PH_Generic3Label.BackColor = System.Drawing.Color.Transparent;
            this.PH_Generic3Label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PH_Generic3Label.ForeColor = System.Drawing.Color.Black;
            this.PH_Generic3Label.Location = new System.Drawing.Point(6, 63);
            this.PH_Generic3Label.Name = "PH_Generic3Label";
            this.PH_Generic3Label.Size = new System.Drawing.Size(181, 30);
            this.PH_Generic3Label.TabIndex = 133;
            this.PH_Generic3Label.Text = "Field3:";
            this.PH_Generic3Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.PH_Generic3Label.Visible = false;
            // 
            // LoanGenericPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.PH_Generic3Text);
            this.Controls.Add(this.PH_Generic3Label);
            this.Controls.Add(this.PH_Generic2Text);
            this.Controls.Add(this.PH_Generic2Label);
            this.Controls.Add(this.PH_Generic1Text);
            this.Controls.Add(this.PH_Generic1Label);
            this.Name = "LoanGenericPanel";
            this.Size = new System.Drawing.Size(280, 227);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PH_Generic1Text;
        private System.Windows.Forms.Label PH_Generic1Label;
        private System.Windows.Forms.Label PH_Generic2Text;
        private System.Windows.Forms.Label PH_Generic2Label;
        private System.Windows.Forms.Label PH_Generic3Text;
        private System.Windows.Forms.Label PH_Generic3Label;
    }
}
