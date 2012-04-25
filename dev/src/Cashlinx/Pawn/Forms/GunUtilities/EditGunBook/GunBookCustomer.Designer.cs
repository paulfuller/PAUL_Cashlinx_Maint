using CashlinxDesktop.UserControls;
using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.GunUtilities.EditGunBook
{
    partial class GunBookCustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GunBookCustomer));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.currentName = new System.Windows.Forms.Label();
            this.address1 = new System.Windows.Forms.Label();
            this.address2 = new System.Windows.Forms.Label();
            this.idHeader = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.Label();
            this.customerNo = new System.Windows.Forms.Label();
            this.customerNumber = new System.Windows.Forms.Label();
            this.labelCustNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.customerName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.currentID = new System.Windows.Forms.Label();
            this.customTextBoxaddr1 = new CustomTextBox();
            this.customTextBoxAddr2 = new CustomTextBox();
            this.customTextBoxCity = new CustomTextBox();
            this.zipcode1 = new Zipcode();
            this.customButtonCancel = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.state1 = new State();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Edit Receipt Customer Information";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(5, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 29);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current Customer Information";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(5, 186);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(671, 29);
            this.panel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "New Customer Information";
            // 
            // currentName
            // 
            this.currentName.AutoSize = true;
            this.currentName.BackColor = System.Drawing.Color.Transparent;
            this.currentName.Location = new System.Drawing.Point(42, 120);
            this.currentName.Name = "currentName";
            this.currentName.Size = new System.Drawing.Size(33, 13);
            this.currentName.TabIndex = 3;
            this.currentName.Text = "name";
            // 
            // address1
            // 
            this.address1.AutoSize = true;
            this.address1.BackColor = System.Drawing.Color.Transparent;
            this.address1.Location = new System.Drawing.Point(42, 143);
            this.address1.Name = "address1";
            this.address1.Size = new System.Drawing.Size(52, 13);
            this.address1.TabIndex = 4;
            this.address1.Text = "Address1";
            // 
            // address2
            // 
            this.address2.AutoSize = true;
            this.address2.BackColor = System.Drawing.Color.Transparent;
            this.address2.Location = new System.Drawing.Point(42, 165);
            this.address2.Name = "address2";
            this.address2.Size = new System.Drawing.Size(52, 13);
            this.address2.TabIndex = 5;
            this.address2.Text = "Address2";
            // 
            // idHeader
            // 
            this.idHeader.AutoSize = true;
            this.idHeader.BackColor = System.Drawing.Color.Transparent;
            this.idHeader.Location = new System.Drawing.Point(274, 120);
            this.idHeader.Name = "idHeader";
            this.idHeader.Size = new System.Drawing.Size(22, 13);
            this.idHeader.TabIndex = 6;
            this.idHeader.Text = "ID:";
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.BackColor = System.Drawing.Color.Transparent;
            this.id.Location = new System.Drawing.Point(312, 120);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(15, 13);
            this.id.TabIndex = 7;
            this.id.Text = "id";
            // 
            // customerNo
            // 
            this.customerNo.AutoSize = true;
            this.customerNo.BackColor = System.Drawing.Color.Transparent;
            this.customerNo.Location = new System.Drawing.Point(504, 120);
            this.customerNo.Name = "customerNo";
            this.customerNo.Size = new System.Drawing.Size(68, 13);
            this.customerNo.TabIndex = 8;
            this.customerNo.Text = "Customer #:";
            // 
            // customerNumber
            // 
            this.customerNumber.AutoSize = true;
            this.customerNumber.BackColor = System.Drawing.Color.Transparent;
            this.customerNumber.Location = new System.Drawing.Point(578, 120);
            this.customerNumber.Name = "customerNumber";
            this.customerNumber.Size = new System.Drawing.Size(43, 13);
            this.customerNumber.TabIndex = 9;
            this.customerNumber.Text = "200000";
            // 
            // labelCustNumber
            // 
            this.labelCustNumber.AutoSize = true;
            this.labelCustNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelCustNumber.Location = new System.Drawing.Point(578, 227);
            this.labelCustNumber.Name = "labelCustNumber";
            this.labelCustNumber.Size = new System.Drawing.Size(43, 13);
            this.labelCustNumber.TabIndex = 11;
            this.labelCustNumber.Text = "200000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(504, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Customer #:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(42, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Customer:";
            // 
            // customerName
            // 
            this.customerName.AutoSize = true;
            this.customerName.BackColor = System.Drawing.Color.Transparent;
            this.customerName.Location = new System.Drawing.Point(105, 227);
            this.customerName.Name = "customerName";
            this.customerName.Size = new System.Drawing.Size(33, 13);
            this.customerName.TabIndex = 13;
            this.customerName.Text = "name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(42, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Address:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(300, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "ID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(42, 302);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "City:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(259, 309);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "State:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(469, 309);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Zip Code:";
            // 
            // currentID
            // 
            this.currentID.AutoSize = true;
            this.currentID.BackColor = System.Drawing.Color.Transparent;
            this.currentID.Location = new System.Drawing.Point(328, 255);
            this.currentID.Name = "currentID";
            this.currentID.Size = new System.Drawing.Size(67, 13);
            this.currentID.TabIndex = 19;
            this.currentID.Text = "TX DL 22222";
            // 
            // customTextBoxaddr1
            // 
            this.customTextBoxaddr1.CausesValidation = false;
            this.customTextBoxaddr1.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxaddr1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxaddr1.Location = new System.Drawing.Point(98, 252);
            this.customTextBoxaddr1.MaxLength = 40;
            this.customTextBoxaddr1.Name = "customTextBoxaddr1";
            this.customTextBoxaddr1.RegularExpression = true;
            this.customTextBoxaddr1.Size = new System.Drawing.Size(130, 21);
            this.customTextBoxaddr1.TabIndex = 21;
            this.customTextBoxaddr1.ValidationExpression = "^[\\w#\\ ]*$";
            this.customTextBoxaddr1.Leave += new System.EventHandler(this.customTextBoxaddr1_Leave);
            // 
            // customTextBoxAddr2
            // 
            this.customTextBoxAddr2.CausesValidation = false;
            this.customTextBoxAddr2.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxAddr2.Location = new System.Drawing.Point(98, 279);
            this.customTextBoxAddr2.MaxLength = 40;
            this.customTextBoxAddr2.Name = "customTextBoxAddr2";
            this.customTextBoxAddr2.Size = new System.Drawing.Size(130, 21);
            this.customTextBoxAddr2.TabIndex = 22;
            this.customTextBoxAddr2.ValidationExpression = "^[\\w#\\ ]*$";
            // 
            // customTextBoxCity
            // 
            this.customTextBoxCity.CausesValidation = false;
            this.customTextBoxCity.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCity.Location = new System.Drawing.Point(98, 306);
            this.customTextBoxCity.MaxLength = 50;
            this.customTextBoxCity.Name = "customTextBoxCity";
            this.customTextBoxCity.Size = new System.Drawing.Size(130, 21);
            this.customTextBoxCity.TabIndex = 23;
            this.customTextBoxCity.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCity.Leave += new System.EventHandler(this.customTextBoxCity_Leave);
            // 
            // zipcode1
            // 
            this.zipcode1.BackColor = System.Drawing.Color.Transparent;
            this.zipcode1.CausesValidation = false;
            this.zipcode1.City = null;
            this.zipcode1.Location = new System.Drawing.Point(540, 306);
            this.zipcode1.Margin = new System.Windows.Forms.Padding(0);
            this.zipcode1.Name = "zipcode1";
            this.zipcode1.Size = new System.Drawing.Size(74, 21);
            this.zipcode1.State = null;
            this.zipcode1.TabIndex = 24;
            this.zipcode1.Leave += new System.EventHandler(this.zipcode1_Leave);
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
            this.customButtonCancel.Location = new System.Drawing.Point(14, 353);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 25;
            this.customButtonCancel.Text = "Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonSubmit
            // 
            this.customButtonSubmit.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSubmit.BackgroundImage")));
            this.customButtonSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSubmit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSubmit.FlatAppearance.BorderSize = 0;
            this.customButtonSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSubmit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSubmit.ForeColor = System.Drawing.Color.White;
            this.customButtonSubmit.Location = new System.Drawing.Point(507, 353);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 26;
            this.customButtonSubmit.Text = "Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
            // 
            // state1
            // 
            this.state1.BackColor = System.Drawing.Color.Transparent;
            this.state1.DisplayCode = false;
            this.state1.ForeColor = System.Drawing.Color.Black;
            this.state1.Location = new System.Drawing.Point(315, 309);
            this.state1.Name = "state1";
            this.state1.selectedValue = global::Pawn.Properties.Resources.OverrideMachineName;
            this.state1.Size = new System.Drawing.Size(50, 21);
            this.state1.TabIndex = 27;
            this.state1.Leave += new System.EventHandler(this.state1_Leave);
            // 
            // GunBookCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 416);
            this.Controls.Add(this.state1);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.zipcode1);
            this.Controls.Add(this.customTextBoxCity);
            this.Controls.Add(this.customTextBoxAddr2);
            this.Controls.Add(this.customTextBoxaddr1);
            this.Controls.Add(this.currentID);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.customerName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelCustNumber);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.customerNumber);
            this.Controls.Add(this.customerNo);
            this.Controls.Add(this.id);
            this.Controls.Add(this.idHeader);
            this.Controls.Add(this.address2);
            this.Controls.Add(this.address1);
            this.Controls.Add(this.currentName);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "GunBookCustomer";
            this.Text = "GunBookCustomer";
            this.Load += new System.EventHandler(this.GunBookCustomer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label currentName;
        private System.Windows.Forms.Label address1;
        private System.Windows.Forms.Label address2;
        private System.Windows.Forms.Label idHeader;
        private System.Windows.Forms.Label id;
        private System.Windows.Forms.Label customerNo;
        private System.Windows.Forms.Label customerNumber;
        private System.Windows.Forms.Label labelCustNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label customerName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label currentID;
        private CustomTextBox customTextBoxaddr1;
        private CustomTextBox customTextBoxAddr2;
        private CustomTextBox customTextBoxCity;
        private Zipcode zipcode1;
        private CustomButton customButtonCancel;
        private CustomButton customButtonSubmit;
        private State state1;
    }
}