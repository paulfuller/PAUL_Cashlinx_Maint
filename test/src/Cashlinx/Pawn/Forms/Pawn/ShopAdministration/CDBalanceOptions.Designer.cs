using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class CDBalanceOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CDBalanceOptions));
            this.labelHeading = new System.Windows.Forms.Label();
            this.customButtonExit = new CustomButton();
            this.customButtonBalanceCashDrawer = new CustomButton();
            this.customButtonBalanceSafe = new CustomButton();
            this.labelMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(22, 23);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(122, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Balance Options";
            // 
            // customButtonExit
            // 
            this.customButtonExit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonExit.BackgroundImage")));
            this.customButtonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonExit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonExit.FlatAppearance.BorderSize = 0;
            this.customButtonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonExit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonExit.ForeColor = System.Drawing.Color.White;
            this.customButtonExit.Location = new System.Drawing.Point(51, 175);
            this.customButtonExit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonExit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonExit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonExit.Name = "customButtonExit";
            this.customButtonExit.Size = new System.Drawing.Size(100, 50);
            this.customButtonExit.TabIndex = 1;
            this.customButtonExit.Text = "Cancel";
            this.customButtonExit.UseVisualStyleBackColor = false;
            this.customButtonExit.Click += new System.EventHandler(this.customButtonExit_Click);
            // 
            // customButtonBalanceCashDrawer
            // 
            this.customButtonBalanceCashDrawer.BackColor = System.Drawing.Color.Transparent;
            this.customButtonBalanceCashDrawer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonBalanceCashDrawer.BackgroundImage")));
            this.customButtonBalanceCashDrawer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonBalanceCashDrawer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonBalanceCashDrawer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonBalanceCashDrawer.FlatAppearance.BorderSize = 0;
            this.customButtonBalanceCashDrawer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonBalanceCashDrawer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonBalanceCashDrawer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonBalanceCashDrawer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonBalanceCashDrawer.ForeColor = System.Drawing.Color.White;
            this.customButtonBalanceCashDrawer.Location = new System.Drawing.Point(188, 175);
            this.customButtonBalanceCashDrawer.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBalanceCashDrawer.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBalanceCashDrawer.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBalanceCashDrawer.Name = "customButtonBalanceCashDrawer";
            this.customButtonBalanceCashDrawer.Size = new System.Drawing.Size(100, 50);
            this.customButtonBalanceCashDrawer.TabIndex = 2;
            this.customButtonBalanceCashDrawer.Text = "Balance Other Drawers";
            this.customButtonBalanceCashDrawer.UseVisualStyleBackColor = false;
            this.customButtonBalanceCashDrawer.Click += new System.EventHandler(this.customButtonBalanceCashDrawer_Click);
            // 
            // customButtonBalanceSafe
            // 
            this.customButtonBalanceSafe.BackColor = System.Drawing.Color.Transparent;
            this.customButtonBalanceSafe.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonBalanceSafe.BackgroundImage")));
            this.customButtonBalanceSafe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonBalanceSafe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonBalanceSafe.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonBalanceSafe.FlatAppearance.BorderSize = 0;
            this.customButtonBalanceSafe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonBalanceSafe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonBalanceSafe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonBalanceSafe.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonBalanceSafe.ForeColor = System.Drawing.Color.White;
            this.customButtonBalanceSafe.Location = new System.Drawing.Point(317, 175);
            this.customButtonBalanceSafe.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBalanceSafe.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBalanceSafe.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBalanceSafe.Name = "customButtonBalanceSafe";
            this.customButtonBalanceSafe.Size = new System.Drawing.Size(100, 50);
            this.customButtonBalanceSafe.TabIndex = 3;
            this.customButtonBalanceSafe.Text = "Trial Balance";
            this.customButtonBalanceSafe.UseVisualStyleBackColor = false;
            this.customButtonBalanceSafe.Click += new System.EventHandler(this.customButtonBalanceSafe_Click);
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.BackColor = System.Drawing.Color.Transparent;
            this.labelMessage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.Location = new System.Drawing.Point(64, 85);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(376, 16);
            this.labelMessage.TabIndex = 4;
            this.labelMessage.Text = "One or more cash drawers are not balanced at this time.";
            // 
            // ExitOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 252);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.customButtonBalanceSafe);
            this.Controls.Add(this.customButtonBalanceCashDrawer);
            this.Controls.Add(this.customButtonExit);
            this.Controls.Add(this.labelHeading);
            this.Name = "ExitOptions";
            this.Text = "ExitOptions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private CustomButton customButtonExit;
        private CustomButton customButtonBalanceCashDrawer;
        private CustomButton customButtonBalanceSafe;
        private System.Windows.Forms.Label labelMessage;
    }
}