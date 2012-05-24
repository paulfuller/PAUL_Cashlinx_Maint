using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration.Assignments
{
    partial class EmployeeSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeSearch));
            this.labelHeading = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.shopNumberComboBox = new System.Windows.Forms.ComboBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.employeeNumberTextBox = new CustomTextBox();
            this.customButtonSearch = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(23, 17);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(118, 16);
            this.labelHeading.TabIndex = 138;
            this.labelHeading.Text = "Employee Search";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(39, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 142;
            this.label4.Text = "Shop Number:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(39, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 143;
            this.label1.Text = "OR";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(39, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 144;
            this.label2.Text = "Employee Number:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // shopNumberComboBox
            // 
            this.shopNumberComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shopNumberComboBox.FormattingEnabled = true;
            this.shopNumberComboBox.Location = new System.Drawing.Point(162, 78);
            this.shopNumberComboBox.Name = "shopNumberComboBox";
            this.shopNumberComboBox.Size = new System.Drawing.Size(164, 21);
            this.shopNumberComboBox.TabIndex = 145;
            this.shopNumberComboBox.SelectedIndexChanged += new System.EventHandler(this.shopIDComboBox_SelectedIndexChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(23, 44);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(325, 31);
            this.lblMessage.TabIndex = 152;
            this.lblMessage.Text = "[message]";
            this.lblMessage.Visible = false;
            // 
            // employeeNumberTextBox
            // 
            this.employeeNumberTextBox.CausesValidation = false;
            this.employeeNumberTextBox.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.employeeNumberTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.employeeNumberTextBox.Location = new System.Drawing.Point(162, 123);
            this.employeeNumberTextBox.Name = "employeeNumberTextBox";
            this.employeeNumberTextBox.Size = new System.Drawing.Size(134, 21);
            this.employeeNumberTextBox.TabIndex = 155;
            this.employeeNumberTextBox.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customButtonSearch
            // 
            this.customButtonSearch.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSearch.BackgroundImage")));
            this.customButtonSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSearch.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSearch.FlatAppearance.BorderSize = 0;
            this.customButtonSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSearch.ForeColor = System.Drawing.Color.White;
            this.customButtonSearch.Location = new System.Drawing.Point(226, 157);
            this.customButtonSearch.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSearch.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSearch.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSearch.Name = "customButtonSearch";
            this.customButtonSearch.Size = new System.Drawing.Size(100, 50);
            this.customButtonSearch.TabIndex = 154;
            this.customButtonSearch.Text = "Search";
            this.customButtonSearch.UseVisualStyleBackColor = false;
            this.customButtonSearch.Click += new System.EventHandler(this.customButtonSearch_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(26, 157);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 153;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // EmployeeSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(374, 216);
            this.Controls.Add(this.employeeNumberTextBox);
            this.Controls.Add(this.customButtonSearch);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.shopNumberComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EmployeeSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeSearch";
            this.Load += new System.EventHandler(this.EmployeeSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox shopNumberComboBox;
        private System.Windows.Forms.Label lblMessage;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSearch;
        private CustomTextBox employeeNumberTextBox;
    }
}