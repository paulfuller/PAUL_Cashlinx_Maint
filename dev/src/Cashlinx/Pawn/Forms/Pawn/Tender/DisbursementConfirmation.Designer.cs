using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Tender
{
    partial class DisbursementConfirmation
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisbursementConfirmation));
            this.disbursementLabel = new System.Windows.Forms.Label();
            this.customerNameFieldLabel = new CustomLabel();
            this.customLabel1 = new CustomLabel();
            this.customLabel2 = new CustomLabel();
            this.amountToPayFieldLabel = new CustomLabel();
            this.okButton = new CustomButton();
            this.loanDocListView = new System.Windows.Forms.ListView();
            this.customLabel3 = new CustomLabel();
            this.customButton1 = new CustomButton();
            this.pdfIconList = new System.Windows.Forms.ImageList(this.components);
            this.customLabel4 = new CustomLabel();
            this.tickerNumberFieldLabel = new CustomLabel();
            this.SuspendLayout();
            // 
            // disbursementLabel
            // 
            this.disbursementLabel.AutoSize = true;
            this.disbursementLabel.BackColor = System.Drawing.Color.Transparent;
            this.disbursementLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disbursementLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.disbursementLabel.Location = new System.Drawing.Point(249, 28);
            this.disbursementLabel.Name = "disbursementLabel";
            this.disbursementLabel.Size = new System.Drawing.Size(203, 19);
            this.disbursementLabel.TabIndex = 0;
            this.disbursementLabel.Text = "Disbursement Confirmation";
            // 
            // customerNameFieldLabel
            // 
            this.customerNameFieldLabel.AutoSize = true;
            this.customerNameFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.customerNameFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerNameFieldLabel.Location = new System.Drawing.Point(349, 85);
            this.customerNameFieldLabel.Name = "customerNameFieldLabel";
            this.customerNameFieldLabel.Size = new System.Drawing.Size(139, 16);
            this.customerNameFieldLabel.TabIndex = 6;
            this.customerNameFieldLabel.Text = "<customerNameField>";
            this.customerNameFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customLabel1
            // 
            this.customLabel1.AutoSize = true;
            this.customLabel1.BackColor = System.Drawing.Color.Transparent;
            this.customLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel1.Location = new System.Drawing.Point(211, 85);
            this.customLabel1.Name = "customLabel1";
            this.customLabel1.Size = new System.Drawing.Size(114, 16);
            this.customLabel1.TabIndex = 7;
            this.customLabel1.Text = "Customer Name:";
            this.customLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2.Location = new System.Drawing.Point(203, 112);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(122, 16);
            this.customLabel2.TabIndex = 8;
            this.customLabel2.Text = "Pay To Customer:";
            this.customLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // amountToPayFieldLabel
            // 
            this.amountToPayFieldLabel.AutoSize = true;
            this.amountToPayFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.amountToPayFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountToPayFieldLabel.Location = new System.Drawing.Point(349, 112);
            this.amountToPayFieldLabel.Name = "amountToPayFieldLabel";
            this.amountToPayFieldLabel.Size = new System.Drawing.Size(131, 16);
            this.amountToPayFieldLabel.TabIndex = 9;
            this.amountToPayFieldLabel.Text = "<amountToPayField>";
            this.amountToPayFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.okButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ForeColor = System.Drawing.Color.White;
            this.okButton.Location = new System.Drawing.Point(578, 258);
            this.okButton.Margin = new System.Windows.Forms.Padding(0);
            this.okButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.okButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 50);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // loanDocListView
            // 
            this.loanDocListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.loanDocListView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.loanDocListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanDocListView.ForeColor = System.Drawing.Color.Black;
            this.loanDocListView.Location = new System.Drawing.Point(75, 178);
            this.loanDocListView.MultiSelect = false;
            this.loanDocListView.Name = "loanDocListView";
            this.loanDocListView.Size = new System.Drawing.Size(551, 73);
            this.loanDocListView.TabIndex = 11;
            this.loanDocListView.UseCompatibleStateImageBehavior = false;
            this.loanDocListView.Visible = false;
            // 
            // customLabel3
            // 
            this.customLabel3.AutoSize = true;
            this.customLabel3.BackColor = System.Drawing.Color.Transparent;
            this.customLabel3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel3.Location = new System.Drawing.Point(46, 159);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Size = new System.Drawing.Size(80, 16);
            this.customLabel3.TabIndex = 12;
            this.customLabel3.Text = "Documents";
            this.customLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customLabel3.Visible = false;
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.Color.Transparent;
            this.customButton1.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.customButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButton1.FlatAppearance.BorderSize = 0;
            this.customButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.White;
            this.customButton1.Location = new System.Drawing.Point(49, 258);
            this.customButton1.Margin = new System.Windows.Forms.Padding(0);
            this.customButton1.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButton1.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(100, 50);
            this.customButton1.TabIndex = 13;
            this.customButton1.Text = "View";
            this.customButton1.UseVisualStyleBackColor = false;
            this.customButton1.Visible = false;
            // 
            // pdfIconList
            // 
            this.pdfIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("pdfIconList.ImageStream")));
            this.pdfIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.pdfIconList.Images.SetKeyName(0, "pdf_icon.png");
            // 
            // customLabel4
            // 
            this.customLabel4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabel4.AutoSize = true;
            this.customLabel4.BackColor = System.Drawing.Color.Transparent;
            this.customLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customLabel4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel4.Location = new System.Drawing.Point(221, 141);
            this.customLabel4.Name = "customLabel4";
            this.customLabel4.Size = new System.Drawing.Size(104, 16);
            this.customLabel4.TabIndex = 14;
            this.customLabel4.Text = "Ticket Number:";
            this.customLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tickerNumberFieldLabel
            // 
            this.tickerNumberFieldLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tickerNumberFieldLabel.AutoSize = true;
            this.tickerNumberFieldLabel.BackColor = System.Drawing.Color.Transparent;
            this.tickerNumberFieldLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tickerNumberFieldLabel.Location = new System.Drawing.Point(349, 139);
            this.tickerNumberFieldLabel.Name = "tickerNumberFieldLabel";
            this.tickerNumberFieldLabel.Size = new System.Drawing.Size(128, 16);
            this.tickerNumberFieldLabel.TabIndex = 15;
            this.tickerNumberFieldLabel.Text = "<ticketNumberField>";
            this.tickerNumberFieldLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DisbursementConfirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_320_BlueScale;
            this.ClientSize = new System.Drawing.Size(700, 320);
            this.Controls.Add(this.customLabel4);
            this.Controls.Add(this.tickerNumberFieldLabel);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.customLabel3);
            this.Controls.Add(this.loanDocListView);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.amountToPayFieldLabel);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.customLabel1);
            this.Controls.Add(this.customerNameFieldLabel);
            this.Controls.Add(this.disbursementLabel);
            this.MaximumSize = new System.Drawing.Size(700, 320);
            this.MinimumSize = new System.Drawing.Size(700, 320);
            this.Name = "DisbursementConfirmation";
            this.Text = "DisbursementForm";
            this.Load += new System.EventHandler(this.DisbursementConfirmation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label disbursementLabel;
        private CustomLabel customerNameFieldLabel;
        private CustomLabel customLabel1;
        private CustomLabel customLabel2;
        private CustomLabel amountToPayFieldLabel;
        private CustomButton okButton;
        private System.Windows.Forms.ListView loanDocListView;
        private CustomLabel customLabel3;
        private CustomButton customButton1;
        private System.Windows.Forms.ImageList pdfIconList;
        private CustomLabel customLabel4;
        private CustomLabel tickerNumberFieldLabel;
    }
}
