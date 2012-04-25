using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Void
{
    partial class VoidLoanForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.loanTicketLabel = new CustomLabel();
            this.storeNumberTextBox = new CustomTextBox();
            this.storeNumberLabel = new CustomLabel();
            this.customLabel2 = new CustomLabel();
            this.customLabel3 = new CustomLabel();
            this.ticketNumberTextBox = new CustomTextBox();
            this.submitTicketButton = new CustomButton();
            this.label2 = new System.Windows.Forms.Label();
            this.voidTreeView = new System.Windows.Forms.TreeView();
            this.voidButton = new CustomButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cancelButton = new CustomButton();
            this.errorLabel = new CustomLabel();
            this.loanChainRetrieveWorker = new System.ComponentModel.BackgroundWorker();
            this.label4 = new System.Windows.Forms.Label();
            this.debugLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(264, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Void Loan";
            // 
            // loanTicketLabel
            // 
            this.loanTicketLabel.AutoSize = true;
            this.loanTicketLabel.BackColor = System.Drawing.Color.Transparent;
            this.loanTicketLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loanTicketLabel.Location = new System.Drawing.Point(65, 97);
            this.loanTicketLabel.Name = "loanTicketLabel";
            this.loanTicketLabel.Size = new System.Drawing.Size(196, 13);
            this.loanTicketLabel.TabIndex = 1;
            this.loanTicketLabel.Text = "Please Enter Store And Ticket Number: ";
            this.loanTicketLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // storeNumberTextBox
            // 
            this.storeNumberTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.storeNumberTextBox.CausesValidation = false;
            this.storeNumberTextBox.ErrorMessage = "Store Number Must Be 3 - 5 Digits In Length.";
            this.storeNumberTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeNumberTextBox.ForeColor = System.Drawing.Color.Black;
            this.storeNumberTextBox.Location = new System.Drawing.Point(270, 95);
            this.storeNumberTextBox.MaxLength = 5;
            this.storeNumberTextBox.Name = "storeNumberTextBox";
            this.storeNumberTextBox.RegularExpression = true;
            this.storeNumberTextBox.Required = true;
            this.storeNumberTextBox.Size = new System.Drawing.Size(76, 21);
            this.storeNumberTextBox.TabIndex = 1;
            this.storeNumberTextBox.ValidationExpression = "[0-9]{3,5}";
            this.storeNumberTextBox.TextChanged += new System.EventHandler(this.storeNumberTextBox_TextChanged);
            // 
            // storeNumberLabel
            // 
            this.storeNumberLabel.AutoSize = true;
            this.storeNumberLabel.BackColor = System.Drawing.Color.Transparent;
            this.storeNumberLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.storeNumberLabel.Location = new System.Drawing.Point(273, 79);
            this.storeNumberLabel.Name = "storeNumberLabel";
            this.storeNumberLabel.Size = new System.Drawing.Size(73, 13);
            this.storeNumberLabel.TabIndex = 3;
            this.storeNumberLabel.Text = "Store Number";
            this.storeNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customLabel2
            // 
            this.customLabel2.AutoSize = true;
            this.customLabel2.BackColor = System.Drawing.Color.Transparent;
            this.customLabel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel2.Location = new System.Drawing.Point(350, 98);
            this.customLabel2.Name = "customLabel2";
            this.customLabel2.Size = new System.Drawing.Size(11, 13);
            this.customLabel2.TabIndex = 4;
            this.customLabel2.Text = "-";
            this.customLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customLabel3
            // 
            this.customLabel3.AutoSize = true;
            this.customLabel3.BackColor = System.Drawing.Color.Transparent;
            this.customLabel3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabel3.Location = new System.Drawing.Point(366, 79);
            this.customLabel3.Name = "customLabel3";
            this.customLabel3.Size = new System.Drawing.Size(75, 13);
            this.customLabel3.TabIndex = 6;
            this.customLabel3.Text = "Ticket Number";
            this.customLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ticketNumberTextBox
            // 
            this.ticketNumberTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ticketNumberTextBox.CausesValidation = false;
            this.ticketNumberTextBox.ErrorMessage = "Ticket Number Must Be 1 - 6 Digits In Length.";
            this.ticketNumberTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ticketNumberTextBox.ForeColor = System.Drawing.Color.Black;
            this.ticketNumberTextBox.Location = new System.Drawing.Point(365, 95);
            this.ticketNumberTextBox.MaxLength = 6;
            this.ticketNumberTextBox.Name = "ticketNumberTextBox";
            this.ticketNumberTextBox.RegularExpression = true;
            this.ticketNumberTextBox.Required = true;
            this.ticketNumberTextBox.Size = new System.Drawing.Size(76, 21);
            this.ticketNumberTextBox.TabIndex = 5;
            this.ticketNumberTextBox.ValidationExpression = "[0-9]{1,6}";
            // 
            // submitTicketButton
            // 
            this.submitTicketButton.BackColor = System.Drawing.Color.Transparent;
            this.submitTicketButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.submitTicketButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.submitTicketButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitTicketButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.submitTicketButton.FlatAppearance.BorderSize = 0;
            this.submitTicketButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitTicketButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitTicketButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitTicketButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitTicketButton.ForeColor = System.Drawing.Color.White;
            this.submitTicketButton.Location = new System.Drawing.Point(497, 79);
            this.submitTicketButton.Margin = new System.Windows.Forms.Padding(0);
            this.submitTicketButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.submitTicketButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.submitTicketButton.Name = "submitTicketButton";
            this.submitTicketButton.Size = new System.Drawing.Size(100, 50);
            this.submitTicketButton.TabIndex = 7;
            this.submitTicketButton.Text = "Submit";
            this.submitTicketButton.UseVisualStyleBackColor = false;
            this.submitTicketButton.Click += new System.EventHandler(this.submitTicketButton_Click);
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.BackColor = System.Drawing.Color.Navy;
            this.label2.Enabled = false;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(12, 140);
            this.label2.MaximumSize = new System.Drawing.Size(585, 3);
            this.label2.MinimumSize = new System.Drawing.Size(585, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(585, 3);
            this.label2.TabIndex = 8;
            this.label2.Text = "-";
            // 
            // voidTreeView
            // 
            this.voidTreeView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.voidTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.voidTreeView.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voidTreeView.ForeColor = System.Drawing.Color.Black;
            this.voidTreeView.FullRowSelect = true;
            this.voidTreeView.HideSelection = false;
            this.voidTreeView.Location = new System.Drawing.Point(130, 184);
            this.voidTreeView.MaximumSize = new System.Drawing.Size(350, 250);
            this.voidTreeView.MinimumSize = new System.Drawing.Size(350, 250);
            this.voidTreeView.Name = "voidTreeView";
            this.voidTreeView.Size = new System.Drawing.Size(350, 250);
            this.voidTreeView.TabIndex = 11;
            this.voidTreeView.TabStop = false;
            this.voidTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.voidTreeView_BeforeSelect);
            // 
            // voidButton
            // 
            this.voidButton.BackColor = System.Drawing.Color.Transparent;
            this.voidButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.voidButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.voidButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.voidButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.voidButton.FlatAppearance.BorderSize = 0;
            this.voidButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.voidButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.voidButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.voidButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.voidButton.ForeColor = System.Drawing.Color.White;
            this.voidButton.Location = new System.Drawing.Point(498, 451);
            this.voidButton.Margin = new System.Windows.Forms.Padding(0);
            this.voidButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.voidButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.voidButton.Name = "voidButton";
            this.voidButton.Size = new System.Drawing.Size(100, 50);
            this.voidButton.TabIndex = 13;
            this.voidButton.Text = "Void";
            this.voidButton.UseVisualStyleBackColor = false;
            this.voidButton.Click += new System.EventHandler(this.voidButton_Click);
            // 
            // label3
            // 
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label3.BackColor = System.Drawing.Color.Navy;
            this.label3.Enabled = false;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(13, 448);
            this.label3.MaximumSize = new System.Drawing.Size(585, 3);
            this.label3.MinimumSize = new System.Drawing.Size(585, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(585, 3);
            this.label3.TabIndex = 14;
            this.label3.Text = "-";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.vistabutton_blue;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(15, 451);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.Transparent;
            this.errorLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(65, 61);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(56, 13);
            this.errorLabel.TabIndex = 17;
            this.errorLabel.Text = "errorLabel";
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.errorLabel.Visible = false;
            // 
            // loanChainRetrieveWorker
            // 
            this.loanChainRetrieveWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loanChainRetrieveWorker_DoWork);
            this.loanChainRetrieveWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loanChainRetrieveWorker_RunWorkerCompleted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(266, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Loan Chain";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Enabled = false;
            this.debugLabel.Location = new System.Drawing.Point(12, 25);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(40, 13);
            this.debugLabel.TabIndex = 19;
            this.debugLabel.Text = "DEBUG";
            this.debugLabel.Visible = false;
            // 
            // VoidLoanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.ClientSize = new System.Drawing.Size(610, 520);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.voidButton);
            this.Controls.Add(this.voidTreeView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.submitTicketButton);
            this.Controls.Add(this.customLabel3);
            this.Controls.Add(this.ticketNumberTextBox);
            this.Controls.Add(this.customLabel2);
            this.Controls.Add(this.storeNumberLabel);
            this.Controls.Add(this.storeNumberTextBox);
            this.Controls.Add(this.loanTicketLabel);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(610, 520);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(610, 520);
            this.Name = "VoidLoanForm";
            this.Text = "VoidLoanForm";
            this.Load += new System.EventHandler(this.VoidLoanForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CustomLabel loanTicketLabel;
        private CustomTextBox storeNumberTextBox;
        private CustomLabel storeNumberLabel;
        private CustomLabel customLabel2;
        private CustomLabel customLabel3;
        private CustomTextBox ticketNumberTextBox;
        private CustomButton submitTicketButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView voidTreeView;
        private CustomButton voidButton;
        private System.Windows.Forms.Label label3;
        private CustomButton cancelButton;
        private CustomLabel errorLabel;
        private System.ComponentModel.BackgroundWorker loanChainRetrieveWorker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label debugLabel;
    }
}