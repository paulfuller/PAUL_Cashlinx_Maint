using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration.Assignments
{
    partial class EmployeeAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeAdd));
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.customLabel1 = new CustomLabel();
            this.customTextBoxEmployeeNo = new CustomTextBox();
            this.customButtonAdd = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(28, 29);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(325, 31);
            this.lblMessage.TabIndex = 151;
            this.lblMessage.Text = "[message]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 14);
            this.label1.TabIndex = 156;
            this.label1.Text = "Add Employee";
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(37, 67);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Required = true;
            this.customLabel1.Size = new System.Drawing.Size(97, 13);
            this.customLabel1.TabIndex = 155;
            this.customLabel1.Text = "Employee Number:";
            // 
            // customTextBoxEmployeeNo
            // 
            this.customTextBoxEmployeeNo.AllowOnlyNumbers = true;
            this.customTextBoxEmployeeNo.CausesValidation = false;
            this.customTextBoxEmployeeNo.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxEmployeeNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxEmployeeNo.Location = new System.Drawing.Point(152, 64);
            this.customTextBoxEmployeeNo.MaxLength = 5;
            this.customTextBoxEmployeeNo.Name = "customTextBoxEmployeeNo";
            this.customTextBoxEmployeeNo.Size = new System.Drawing.Size(165, 21);
            this.customTextBoxEmployeeNo.TabIndex = 154;
            this.customTextBoxEmployeeNo.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
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
            this.customButtonAdd.Location = new System.Drawing.Point(226, 96);
            this.customButtonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAdd.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAdd.Name = "customButtonAdd";
            this.customButtonAdd.Size = new System.Drawing.Size(100, 50);
            this.customButtonAdd.TabIndex = 153;
            this.customButtonAdd.Text = "Add";
            this.customButtonAdd.UseVisualStyleBackColor = false;
            this.customButtonAdd.Click += new System.EventHandler(this.addButton_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(28, 96);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 152;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EmployeeAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(380, 155);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.customTextBoxEmployeeNo);
            this.Controls.Add(this.customButtonAdd);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.lblMessage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EmployeeAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeAdd";
            this.Load += new System.EventHandler(this.EmployeeAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private CustomButton customButtonCancel;
        private CustomButton customButtonAdd;
        private CustomTextBox customTextBoxEmployeeNo;
        private CustomLabel customLabel1;
        private System.Windows.Forms.Label label1;
    }
}