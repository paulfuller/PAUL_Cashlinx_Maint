using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class AddCashDrawer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCashDrawer));
            this.labelHeading = new System.Windows.Forms.Label();
            this.customLabel1 = new CustomLabel();
            this.customLabel2 = new CustomLabel();
            this.customTextBoxUserName = new CustomTextBox();
            this.customButtonCancel = new CustomButton();
            this.customButtonAdd = new CustomButton();
            this.customLabelInvalidUserMessage = new CustomLabel();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(12, 19);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(150, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Add Cash Drawer";
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(26, 77);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(132, 13);
            this.customLabel1.TabIndex = 3;
            this.customLabel1.Text = "User For the Cash Drawer";
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2.Location = new System.Drawing.Point(95, 106);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Required = true;
            this.customLabel2.Size = new System.Drawing.Size(63, 13);
            this.customLabel2.TabIndex = 4;
            this.customLabel2.Text = "User Name:";
            // 
            // customTextBoxUserName
            // 
            this.customTextBoxUserName.CausesValidation = false;
            this.customTextBoxUserName.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxUserName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxUserName.Location = new System.Drawing.Point(178, 103);
            this.customTextBoxUserName.Name = "customTextBoxUserName";
            this.customTextBoxUserName.Size = new System.Drawing.Size(139, 21);
            this.customTextBoxUserName.TabIndex = 10;
            this.customTextBoxUserName.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
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
            this.customButtonCancel.Location = new System.Drawing.Point(50, 171);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 11;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
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
            this.customButtonAdd.Location = new System.Drawing.Point(281, 171);
            this.customButtonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAdd.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.Name = "customButtonAdd";
            this.customButtonAdd.Size = new System.Drawing.Size(100, 50);
            this.customButtonAdd.TabIndex = 12;
            this.customButtonAdd.Text = "Add";
            this.customButtonAdd.UseVisualStyleBackColor = false;
            this.customButtonAdd.Click += new System.EventHandler(this.customButtonAdd_Click);
            // 
            // customLabelInvalidUserMessage
            // 
            this.customLabelInvalidUserMessage.AutoSize = true;
            this.customLabelInvalidUserMessage.BackColor = System.Drawing.Color.Transparent;
            this.customLabelInvalidUserMessage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelInvalidUserMessage.Location = new System.Drawing.Point(177, 77);
            this.customLabelInvalidUserMessage.Name = "customLabelInvalidUserMessage";
            this.customLabelInvalidUserMessage.Size = new System.Drawing.Size(76, 13);
            this.customLabelInvalidUserMessage.TabIndex = 14;
            this.customLabelInvalidUserMessage.Text = "Error Message";
            this.customLabelInvalidUserMessage.Visible = false;
            // 
            // AddCashDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 267);
            this.Controls.Add(this.customLabelInvalidUserMessage);
            this.Controls.Add(this.customButtonAdd);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customTextBoxUserName);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.labelHeading);
            this.Name = "AddCashDrawer";
            this.Text = "AddCashDrawer";
            this.Load += new System.EventHandler(this.AddCashDrawer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private CustomLabel customLabel1;
        private CustomLabel customLabel2;
        private CustomTextBox customTextBoxUserName;
        private CustomButton customButtonCancel;
        private CustomButton customButtonAdd;
        private CustomLabel customLabelInvalidUserMessage;
    }
}