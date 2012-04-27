namespace Support.Forms.AutoTransfer
{
    partial class FrmAutoTransfer
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
            this.changePasswordHeaderLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbTransferType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTransferType = new System.Windows.Forms.Label();
            this.lblFromShop = new System.Windows.Forms.Label();
            this.lblToShop = new System.Windows.Forms.Label();
            this.lblTransferNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFromShop = new System.Windows.Forms.TextBox();
            this.txtToShop = new System.Windows.Forms.TextBox();
            this.txtTransferNum = new System.Windows.Forms.TextBox();
            this.rbInbound = new System.Windows.Forms.RadioButton();
            this.rbRecreate = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // changePasswordHeaderLabel
            // 
            this.changePasswordHeaderLabel.AutoSize = true;
            this.changePasswordHeaderLabel.BackColor = System.Drawing.Color.Transparent;
            this.changePasswordHeaderLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePasswordHeaderLabel.ForeColor = System.Drawing.Color.White;
            this.changePasswordHeaderLabel.Location = new System.Drawing.Point(142, 27);
            this.changePasswordHeaderLabel.Name = "changePasswordHeaderLabel";
            this.changePasswordHeaderLabel.Size = new System.Drawing.Size(196, 19);
            this.changePasswordHeaderLabel.TabIndex = 2;
            this.changePasswordHeaderLabel.Text = "Merchandise Transfer Files";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(24, 344);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 38);
            this.button1.TabIndex = 12;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbTransferType
            // 
            this.cbTransferType.BackColor = System.Drawing.Color.White;
            this.cbTransferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTransferType.ForeColor = System.Drawing.Color.Black;
            this.cbTransferType.FormattingEnabled = true;
            this.cbTransferType.Location = new System.Drawing.Point(237, 164);
            this.cbTransferType.Name = "cbTransferType";
            this.cbTransferType.Size = new System.Drawing.Size(131, 21);
            this.cbTransferType.TabIndex = 1;
            this.cbTransferType.SelectedIndexChanged += new System.EventHandler(this.cbTransferType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(194, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "*";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTransferType
            // 
            this.lblTransferType.AutoSize = true;
            this.lblTransferType.BackColor = System.Drawing.Color.Transparent;
            this.lblTransferType.Location = new System.Drawing.Point(113, 168);
            this.lblTransferType.Name = "lblTransferType";
            this.lblTransferType.Size = new System.Drawing.Size(79, 13);
            this.lblTransferType.TabIndex = 14;
            this.lblTransferType.Text = "Transfer Type:";
            this.lblTransferType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFromShop
            // 
            this.lblFromShop.AutoSize = true;
            this.lblFromShop.BackColor = System.Drawing.Color.Transparent;
            this.lblFromShop.Location = new System.Drawing.Point(130, 205);
            this.lblFromShop.Name = "lblFromShop";
            this.lblFromShop.Size = new System.Drawing.Size(62, 13);
            this.lblFromShop.TabIndex = 16;
            this.lblFromShop.Text = "From Shop:";
            this.lblFromShop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblToShop
            // 
            this.lblToShop.AutoSize = true;
            this.lblToShop.BackColor = System.Drawing.Color.Transparent;
            this.lblToShop.Location = new System.Drawing.Point(142, 242);
            this.lblToShop.Name = "lblToShop";
            this.lblToShop.Size = new System.Drawing.Size(50, 13);
            this.lblToShop.TabIndex = 17;
            this.lblToShop.Text = "To Shop:";
            this.lblToShop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTransferNum
            // 
            this.lblTransferNum.AutoSize = true;
            this.lblTransferNum.BackColor = System.Drawing.Color.Transparent;
            this.lblTransferNum.Location = new System.Drawing.Point(100, 279);
            this.lblTransferNum.Name = "lblTransferNum";
            this.lblTransferNum.Size = new System.Drawing.Size(92, 13);
            this.lblTransferNum.TabIndex = 18;
            this.lblTransferNum.Text = "Transfer Number:";
            this.lblTransferNum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(194, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(194, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "*";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(194, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::Support.Properties.Resources.Red_button_100_50;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(357, 344);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 38);
            this.button2.TabIndex = 22;
            this.button2.Text = "Create File";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFromShop
            // 
            this.txtFromShop.Location = new System.Drawing.Point(237, 197);
            this.txtFromShop.MaxLength = 5;
            this.txtFromShop.Name = "txtFromShop";
            this.txtFromShop.Size = new System.Drawing.Size(90, 21);
            this.txtFromShop.TabIndex = 2;
            this.txtFromShop.Leave += new System.EventHandler(this.txtFromShop_Leave);
            // 
            // txtToShop
            // 
            this.txtToShop.Location = new System.Drawing.Point(237, 234);
            this.txtToShop.MaxLength = 5;
            this.txtToShop.Name = "txtToShop";
            this.txtToShop.Size = new System.Drawing.Size(90, 21);
            this.txtToShop.TabIndex = 3;
            this.txtToShop.Leave += new System.EventHandler(this.txtToShop_Leave);
            // 
            // txtTransferNum
            // 
            this.txtTransferNum.Location = new System.Drawing.Point(237, 271);
            this.txtTransferNum.MaxLength = 10;
            this.txtTransferNum.Name = "txtTransferNum";
            this.txtTransferNum.Size = new System.Drawing.Size(90, 21);
            this.txtTransferNum.TabIndex = 4;
            this.txtTransferNum.Leave += new System.EventHandler(this.txtTransferNum_Leave);
            // 
            // rbInbound
            // 
            this.rbInbound.AutoSize = true;
            this.rbInbound.BackColor = System.Drawing.Color.Transparent;
            this.rbInbound.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbInbound.Location = new System.Drawing.Point(248, 92);
            this.rbInbound.Name = "rbInbound";
            this.rbInbound.Size = new System.Drawing.Size(192, 17);
            this.rbInbound.TabIndex = 24;
            this.rbInbound.TabStop = true;
            this.rbInbound.Text = "Process Inbound Transfer File";
            this.rbInbound.UseVisualStyleBackColor = false;
            this.rbInbound.Visible = false;
            this.rbInbound.CheckedChanged += new System.EventHandler(this.rbInbound_CheckedChanged);
            // 
            // rbRecreate
            // 
            this.rbRecreate.AutoSize = true;
            this.rbRecreate.BackColor = System.Drawing.Color.Transparent;
            this.rbRecreate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbRecreate.Location = new System.Drawing.Point(36, 92);
            this.rbRecreate.Name = "rbRecreate";
            this.rbRecreate.Size = new System.Drawing.Size(180, 17);
            this.rbRecreate.TabIndex = 23;
            this.rbRecreate.TabStop = true;
            this.rbRecreate.Text = "Recreate Auto Transfer File";
            this.rbRecreate.UseVisualStyleBackColor = false;
            this.rbRecreate.Visible = false;
            this.rbRecreate.CheckedChanged += new System.EventHandler(this.rbRecreate_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(152, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Recreate Auto Transfer File";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmAutoTransfer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Support.Properties.Resources.form_480_400;
            this.ClientSize = new System.Drawing.Size(480, 400);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbInbound);
            this.Controls.Add(this.rbRecreate);
            this.Controls.Add(this.txtTransferNum);
            this.Controls.Add(this.txtToShop);
            this.Controls.Add(this.txtFromShop);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTransferNum);
            this.Controls.Add(this.lblToShop);
            this.Controls.Add(this.lblFromShop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTransferType);
            this.Controls.Add(this.cbTransferType);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.changePasswordHeaderLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(480, 400);
            this.MinimumSize = new System.Drawing.Size(480, 400);
            this.Name = "FrmAutoTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmAutoTransfer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label changePasswordHeaderLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbTransferType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTransferType;
        private System.Windows.Forms.Label lblFromShop;
        private System.Windows.Forms.Label lblToShop;
        private System.Windows.Forms.Label lblTransferNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtFromShop;
        private System.Windows.Forms.TextBox txtToShop;
        private System.Windows.Forms.TextBox txtTransferNum;
        private System.Windows.Forms.RadioButton rbInbound;
        private System.Windows.Forms.RadioButton rbRecreate;
        private System.Windows.Forms.Label label5;
    }
}