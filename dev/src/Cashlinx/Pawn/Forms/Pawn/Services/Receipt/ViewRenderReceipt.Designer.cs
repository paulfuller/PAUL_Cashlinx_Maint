using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.Services.Receipt
{
    partial class ViewRenderReceipt
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
            this.viewReceiptLabel = new System.Windows.Forms.Label();
            this.receiptRender = new System.Windows.Forms.RichTextBox();
            this.cancelButton = new CustomButton();
            this.okButton = new CustomButton();
            this.SuspendLayout();
            // 
            // viewReceiptLabel
            // 
            this.viewReceiptLabel.AutoSize = true;
            this.viewReceiptLabel.BackColor = System.Drawing.Color.Transparent;
            this.viewReceiptLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewReceiptLabel.ForeColor = System.Drawing.Color.White;
            this.viewReceiptLabel.Location = new System.Drawing.Point(178, 28);
            this.viewReceiptLabel.Name = "viewReceiptLabel";
            this.viewReceiptLabel.Size = new System.Drawing.Size(104, 19);
            this.viewReceiptLabel.TabIndex = 0;
            this.viewReceiptLabel.Text = "View Receipt ";
            this.viewReceiptLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // receiptRender
            // 
            this.receiptRender.BackColor = System.Drawing.Color.WhiteSmoke;
            this.receiptRender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.receiptRender.Cursor = System.Windows.Forms.Cursors.Cross;
            this.receiptRender.ForeColor = System.Drawing.Color.Black;
            this.receiptRender.Location = new System.Drawing.Point(60, 89);
            this.receiptRender.Name = "receiptRender";
            this.receiptRender.ReadOnly = true;
            this.receiptRender.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.receiptRender.Size = new System.Drawing.Size(340, 456);
            this.receiptRender.TabIndex = 1;
            this.receiptRender.TabStop = false;
            this.receiptRender.Text = "";
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(9, 571);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BackgroundImage = global::Common.Properties.Resources.blueglossy_small;
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.okButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.okButton.FlatAppearance.BorderSize = 0;
            this.okButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.ForeColor = System.Drawing.Color.White;
            this.okButton.Location = new System.Drawing.Point(351, 571);
            this.okButton.Margin = new System.Windows.Forms.Padding(0);
            this.okButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.okButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 50);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "Done";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // ViewRenderReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.ClientSize = new System.Drawing.Size(460, 630);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.receiptRender);
            this.Controls.Add(this.viewReceiptLabel);
            this.MaximumSize = new System.Drawing.Size(460, 630);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(460, 630);
            this.Name = "ViewRenderReceipt";
            this.Text = "ViewRenderReceipt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label viewReceiptLabel;
        private System.Windows.Forms.RichTextBox receiptRender;
        private CustomButton cancelButton;
        private CustomButton okButton;
    }
}