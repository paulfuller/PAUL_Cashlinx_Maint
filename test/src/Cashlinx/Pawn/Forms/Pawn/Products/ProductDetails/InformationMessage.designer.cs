using System;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Products.ProductDetails
{
    partial class InformationMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationMessage));
            this.customButtonClose = new CustomButton();
            this.customLabelMessage = new CustomLabel();
            this.customButtonRefresh = new CustomButton();
            this.SuspendLayout();
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
            this.customButtonClose.Location = new System.Drawing.Point(156, 151);
            this.customButtonClose.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClose.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClose.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClose.Name = "customButtonClose";
            this.customButtonClose.Size = new System.Drawing.Size(100, 50);
            this.customButtonClose.TabIndex = 2;
            this.customButtonClose.Text = "Close";
            this.customButtonClose.UseVisualStyleBackColor = false;
            this.customButtonClose.Click += new System.EventHandler(this.customButtonClose_Click);
            // 
            // customLabelMessage
            // 
            this.customLabelMessage.BackColor = System.Drawing.Color.Transparent;
            this.customLabelMessage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelMessage.Location = new System.Drawing.Point(12, 44);
            this.customLabelMessage.Name = "customLabelMessage";
            this.customLabelMessage.Size = new System.Drawing.Size(384, 94);
            this.customLabelMessage.TabIndex = 11;
            this.customLabelMessage.Text = "message";
            // 
            // customButtonRefresh
            // 
            this.customButtonRefresh.BackColor = System.Drawing.Color.Transparent;
            this.customButtonRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonRefresh.BackgroundImage")));
            this.customButtonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonRefresh.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonRefresh.FlatAppearance.BorderSize = 0;
            this.customButtonRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonRefresh.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonRefresh.ForeColor = System.Drawing.Color.White;
            this.customButtonRefresh.Location = new System.Drawing.Point(32, 151);
            this.customButtonRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonRefresh.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonRefresh.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonRefresh.Name = "customButtonRefresh";
            this.customButtonRefresh.Size = new System.Drawing.Size(100, 50);
            this.customButtonRefresh.TabIndex = 12;
            this.customButtonRefresh.Text = "Refresh";
            this.customButtonRefresh.UseVisualStyleBackColor = true;
            this.customButtonRefresh.Visible = false;
            this.customButtonRefresh.Click += new System.EventHandler(this.customButtonRefresh_Click);
            // 
            // InformationMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(450, 210);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonRefresh);
            this.Controls.Add(this.customLabelMessage);
            this.Controls.Add(this.customButtonClose);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InformationMessage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Information";
            this.Load += new System.EventHandler(this.InformationMessage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomButton customButtonClose;
        private CustomLabel customLabelMessage;
        private CustomButton customButtonRefresh;
    }
}