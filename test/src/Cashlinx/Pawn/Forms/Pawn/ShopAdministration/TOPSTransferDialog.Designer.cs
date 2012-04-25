using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    partial class TOPSTransferDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TOPSTransferDialog));
            this.labelHeading = new System.Windows.Forms.Label();
            this.panelTOPSTransferAuto = new System.Windows.Forms.Panel();
            this.panelTOPSTransferManual = new System.Windows.Forms.Panel();
            this.comboBoxTransferType = new System.Windows.Forms.ComboBox();
            this.richTextBoxTopsTransfer = new System.Windows.Forms.RichTextBox();
            this.customTextBoxAmt = new CustomTextBox();
            this.customButtonCancel = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTOPSTransferAuto.SuspendLayout();
            this.panelTOPSTransferManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(190, 7);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(127, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "TOPS Transfer";
            // 
            // panelTOPSTransferAuto
            // 
            this.panelTOPSTransferAuto.BackColor = System.Drawing.Color.Transparent;
            this.panelTOPSTransferAuto.Controls.Add(this.richTextBoxTopsTransfer);
            this.panelTOPSTransferAuto.Location = new System.Drawing.Point(21, 46);
            this.panelTOPSTransferAuto.Name = "panelTOPSTransferAuto";
            this.panelTOPSTransferAuto.Size = new System.Drawing.Size(464, 99);
            this.panelTOPSTransferAuto.TabIndex = 12;
            // 
            // panelTOPSTransferManual
            // 
            this.panelTOPSTransferManual.BackColor = System.Drawing.Color.Transparent;
            this.panelTOPSTransferManual.Controls.Add(this.customTextBoxAmt);
            this.panelTOPSTransferManual.Controls.Add(this.comboBoxTransferType);
            this.panelTOPSTransferManual.Location = new System.Drawing.Point(53, 151);
            this.panelTOPSTransferManual.Name = "panelTOPSTransferManual";
            this.panelTOPSTransferManual.Size = new System.Drawing.Size(409, 34);
            this.panelTOPSTransferManual.TabIndex = 13;
            this.panelTOPSTransferManual.Visible = false;
            // 
            // comboBoxTransferType
            // 
            this.comboBoxTransferType.FormattingEnabled = true;
            this.comboBoxTransferType.Items.AddRange(new object[] {
            "Create Transfer To TOPS From Cashlinx",
            "Create Transfer From TOPS to Cashlinx"});
            this.comboBoxTransferType.Location = new System.Drawing.Point(12, 5);
            this.comboBoxTransferType.Name = "comboBoxTransferType";
            this.comboBoxTransferType.Size = new System.Drawing.Size(261, 21);
            this.comboBoxTransferType.TabIndex = 0;
            // 
            // richTextBoxTopsTransfer
            // 
            this.richTextBoxTopsTransfer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxTopsTransfer.Location = new System.Drawing.Point(12, 9);
            this.richTextBoxTopsTransfer.Name = "richTextBoxTopsTransfer";
            this.richTextBoxTopsTransfer.ReadOnly = true;
            this.richTextBoxTopsTransfer.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBoxTopsTransfer.Size = new System.Drawing.Size(440, 87);
            this.richTextBoxTopsTransfer.TabIndex = 14;
            this.richTextBoxTopsTransfer.Text = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxAmt
            // 
            this.customTextBoxAmt.AllowDecimalNumbers = true;
            this.customTextBoxAmt.CausesValidation = false;
            this.customTextBoxAmt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxAmt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxAmt.Location = new System.Drawing.Point(288, 5);
            this.customTextBoxAmt.Name = "customTextBoxAmt";
            this.customTextBoxAmt.Required = true;
            this.customTextBoxAmt.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxAmt.TabIndex = 1;
            this.customTextBoxAmt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
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
            this.customButtonCancel.Location = new System.Drawing.Point(21, 195);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 9;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.cancelButton_Click);
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
            this.customButtonSubmit.Location = new System.Drawing.Point(362, 195);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 8;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.customButtonSubmit_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Ticket Number";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Reason for Override";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // TOPSTransferDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(519, 254);
            this.ControlBox = false;
            this.Controls.Add(this.panelTOPSTransferManual);
            this.Controls.Add(this.panelTOPSTransferAuto);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TOPSTransferDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OverrideReason";
            this.Load += new System.EventHandler(this.TOPSTransferDialog_Load);
            this.panelTOPSTransferAuto.ResumeLayout(false);
            this.panelTOPSTransferManual.ResumeLayout(false);
            this.panelTOPSTransferManual.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private CustomButton customButtonSubmit;
        private CustomButton customButtonCancel;
        private System.Windows.Forms.Panel panelTOPSTransferAuto;
        private System.Windows.Forms.Panel panelTOPSTransferManual;
        private System.Windows.Forms.ComboBox comboBoxTransferType;
        private CustomTextBox customTextBoxAmt;
        private System.Windows.Forms.RichTextBox richTextBoxTopsTransfer;
    }
}