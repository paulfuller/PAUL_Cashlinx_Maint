using Common.Libraries.Forms.Components;

namespace Pawn.Forms
{
    partial class ShopOffset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShopOffset));
            this.label1 = new System.Windows.Forms.Label();
            this.yearLabel = new System.Windows.Forms.Label();
            this.yearTextBox = new System.Windows.Forms.TextBox();
            this.monthTextBox = new System.Windows.Forms.TextBox();
            this.monthLabel = new System.Windows.Forms.Label();
            this.dayTextBox = new System.Windows.Forms.TextBox();
            this.dayLabel = new System.Windows.Forms.Label();
            this.hourTextBox = new System.Windows.Forms.TextBox();
            this.hourLabel = new System.Windows.Forms.Label();
            this.minuteTextBox = new System.Windows.Forms.TextBox();
            this.minuteLabel = new System.Windows.Forms.Label();
            this.secondTextBox = new System.Windows.Forms.TextBox();
            this.secondLabel = new System.Windows.Forms.Label();
            this.millisecondTextBox = new System.Windows.Forms.TextBox();
            this.millisecondLabel = new System.Windows.Forms.Label();
            this.submitButton = new Common.Libraries.Forms.Components.CustomButton();
            this.cancelButton = new Common.Libraries.Forms.Components.CustomButton();
            this.specificDateTextBox = new System.Windows.Forms.DateTimePicker();
            this.specificDateLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(190, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Change Shop Offsets";
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.BackColor = System.Drawing.Color.Transparent;
            this.yearLabel.Location = new System.Drawing.Point(141, 150);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(46, 13);
            this.yearLabel.TabIndex = 1;
            this.yearLabel.Text = "Year(s):";
            // 
            // yearTextBox
            // 
            this.yearTextBox.Location = new System.Drawing.Point(193, 147);
            this.yearTextBox.Name = "yearTextBox";
            this.yearTextBox.Size = new System.Drawing.Size(214, 21);
            this.yearTextBox.TabIndex = 2;
            // 
            // monthTextBox
            // 
            this.monthTextBox.Location = new System.Drawing.Point(193, 174);
            this.monthTextBox.Name = "monthTextBox";
            this.monthTextBox.Size = new System.Drawing.Size(214, 21);
            this.monthTextBox.TabIndex = 4;
            // 
            // monthLabel
            // 
            this.monthLabel.AutoSize = true;
            this.monthLabel.BackColor = System.Drawing.Color.Transparent;
            this.monthLabel.Location = new System.Drawing.Point(133, 177);
            this.monthLabel.Name = "monthLabel";
            this.monthLabel.Size = new System.Drawing.Size(54, 13);
            this.monthLabel.TabIndex = 3;
            this.monthLabel.Text = "Month(s):";
            // 
            // dayTextBox
            // 
            this.dayTextBox.Location = new System.Drawing.Point(193, 201);
            this.dayTextBox.Name = "dayTextBox";
            this.dayTextBox.Size = new System.Drawing.Size(214, 21);
            this.dayTextBox.TabIndex = 6;
            // 
            // dayLabel
            // 
            this.dayLabel.AutoSize = true;
            this.dayLabel.BackColor = System.Drawing.Color.Transparent;
            this.dayLabel.Location = new System.Drawing.Point(144, 204);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(43, 13);
            this.dayLabel.TabIndex = 5;
            this.dayLabel.Text = "Day(s):";
            // 
            // hourTextBox
            // 
            this.hourTextBox.Location = new System.Drawing.Point(193, 228);
            this.hourTextBox.Name = "hourTextBox";
            this.hourTextBox.Size = new System.Drawing.Size(214, 21);
            this.hourTextBox.TabIndex = 8;
            // 
            // hourLabel
            // 
            this.hourLabel.AutoSize = true;
            this.hourLabel.BackColor = System.Drawing.Color.Transparent;
            this.hourLabel.Location = new System.Drawing.Point(140, 231);
            this.hourLabel.Name = "hourLabel";
            this.hourLabel.Size = new System.Drawing.Size(47, 13);
            this.hourLabel.TabIndex = 7;
            this.hourLabel.Text = "Hour(s):";
            // 
            // minuteTextBox
            // 
            this.minuteTextBox.Location = new System.Drawing.Point(193, 255);
            this.minuteTextBox.Name = "minuteTextBox";
            this.minuteTextBox.Size = new System.Drawing.Size(214, 21);
            this.minuteTextBox.TabIndex = 10;
            // 
            // minuteLabel
            // 
            this.minuteLabel.AutoSize = true;
            this.minuteLabel.BackColor = System.Drawing.Color.Transparent;
            this.minuteLabel.Location = new System.Drawing.Point(131, 258);
            this.minuteLabel.Name = "minuteLabel";
            this.minuteLabel.Size = new System.Drawing.Size(56, 13);
            this.minuteLabel.TabIndex = 9;
            this.minuteLabel.Text = "Minute(s):";
            // 
            // secondTextBox
            // 
            this.secondTextBox.Location = new System.Drawing.Point(193, 282);
            this.secondTextBox.Name = "secondTextBox";
            this.secondTextBox.Size = new System.Drawing.Size(214, 21);
            this.secondTextBox.TabIndex = 12;
            // 
            // secondLabel
            // 
            this.secondLabel.AutoSize = true;
            this.secondLabel.BackColor = System.Drawing.Color.Transparent;
            this.secondLabel.Location = new System.Drawing.Point(128, 285);
            this.secondLabel.Name = "secondLabel";
            this.secondLabel.Size = new System.Drawing.Size(59, 13);
            this.secondLabel.TabIndex = 11;
            this.secondLabel.Text = "Second(s):";
            // 
            // millisecondTextBox
            // 
            this.millisecondTextBox.Location = new System.Drawing.Point(193, 309);
            this.millisecondTextBox.Name = "millisecondTextBox";
            this.millisecondTextBox.Size = new System.Drawing.Size(214, 21);
            this.millisecondTextBox.TabIndex = 14;
            // 
            // millisecondLabel
            // 
            this.millisecondLabel.AutoSize = true;
            this.millisecondLabel.BackColor = System.Drawing.Color.Transparent;
            this.millisecondLabel.Location = new System.Drawing.Point(113, 312);
            this.millisecondLabel.Name = "millisecondLabel";
            this.millisecondLabel.Size = new System.Drawing.Size(74, 13);
            this.millisecondLabel.TabIndex = 13;
            this.millisecondLabel.Text = "Millisecond(s):";
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.Transparent;
            this.submitButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("submitButton.BackgroundImage")));
            this.submitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.submitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.submitButton.FlatAppearance.BorderSize = 0;
            this.submitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.submitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.submitButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.ForeColor = System.Drawing.Color.White;
            this.submitButton.Location = new System.Drawing.Point(430, 347);
            this.submitButton.Margin = new System.Windows.Forms.Padding(0);
            this.submitButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.submitButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(100, 50);
            this.submitButton.TabIndex = 17;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancelButton.BackgroundImage")));
            this.cancelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(12, 347);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 50);
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // specificDateTextBox
            // 
            this.specificDateTextBox.Location = new System.Drawing.Point(194, 88);
            this.specificDateTextBox.Name = "specificDateTextBox";
            this.specificDateTextBox.Size = new System.Drawing.Size(214, 21);
            this.specificDateTextBox.TabIndex = 19;
            this.specificDateTextBox.Value = new System.DateTime(2012, 7, 13, 0, 0, 0, 0);
            // 
            // specificDateLabel
            // 
            this.specificDateLabel.AutoSize = true;
            this.specificDateLabel.BackColor = System.Drawing.Color.Transparent;
            this.specificDateLabel.Location = new System.Drawing.Point(115, 94);
            this.specificDateLabel.Name = "specificDateLabel";
            this.specificDateLabel.Size = new System.Drawing.Size(73, 13);
            this.specificDateLabel.TabIndex = 20;
            this.specificDateLabel.Text = "Specific Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(263, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "-Or-";
            // 
            // ShopOffset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 406);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.specificDateLabel);
            this.Controls.Add(this.specificDateTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.millisecondTextBox);
            this.Controls.Add(this.millisecondLabel);
            this.Controls.Add(this.secondTextBox);
            this.Controls.Add(this.secondLabel);
            this.Controls.Add(this.minuteTextBox);
            this.Controls.Add(this.minuteLabel);
            this.Controls.Add(this.hourTextBox);
            this.Controls.Add(this.hourLabel);
            this.Controls.Add(this.dayTextBox);
            this.Controls.Add(this.dayLabel);
            this.Controls.Add(this.monthTextBox);
            this.Controls.Add(this.monthLabel);
            this.Controls.Add(this.yearTextBox);
            this.Controls.Add(this.yearLabel);
            this.Controls.Add(this.label1);
            this.Name = "ShopOffset";
            this.Text = "ShopOffset";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.TextBox yearTextBox;
        private System.Windows.Forms.TextBox monthTextBox;
        private System.Windows.Forms.Label monthLabel;
        private System.Windows.Forms.TextBox dayTextBox;
        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.TextBox hourTextBox;
        private System.Windows.Forms.Label hourLabel;
        private System.Windows.Forms.TextBox minuteTextBox;
        private System.Windows.Forms.Label minuteLabel;
        private System.Windows.Forms.TextBox secondTextBox;
        private System.Windows.Forms.Label secondLabel;
        private System.Windows.Forms.TextBox millisecondTextBox;
        private System.Windows.Forms.Label millisecondLabel;
        private CustomButton submitButton;
        private CustomButton cancelButton;
        private System.Windows.Forms.DateTimePicker specificDateTextBox;
        private System.Windows.Forms.Label specificDateLabel;
        private System.Windows.Forms.Label label3;

    }
}