using Common.Libraries.Forms.Components;
using Support.Forms;

namespace Support.Forms.Pawn.Customer
{
    partial class UpdatePhysicalDesc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdatePhysicalDesc));
            this.labelHeading = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.otherslabel = new System.Windows.Forms.Label();
            this.pwnapp_height = new CustomTextBox();
            this.pwnapp_weight = new CustomTextBox();
            this.pwnapp_heightinches = new CustomTextBox();
            this.htInlabel = new System.Windows.Forms.Label();
            this.lbsLabel = new System.Windows.Forms.Label();
            this.textBoxOthers = new System.Windows.Forms.TextBox();
            this.pwnapp_race_label = new CustomLabel();
            this.pwnapp_sex_label = new CustomLabel();
            this.pwnapp_hair_label = new CustomLabel();
            this.pwnapp_eyes_label = new CustomLabel();
            this.pwnapp_height_label = new CustomLabel();
            this.htftlabel = new System.Windows.Forms.Label();
            this.pwnapp_weight_label = new CustomLabel();
            this.customButtonBack = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.customButtonReset = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.pwnapp_race = new Race();
            this.pwnapp_sex = new Gender();
            this.pwnapp_hair = new Haircolor();
            this.pwnapp_eyes = new EyeColor();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(169, 21);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(187, 16);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Update Physical Description";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 423F));
            this.tableLayoutPanel1.Controls.Add(this.otherslabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_race, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_sex, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_hair, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_eyes, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_height, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_weight, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_heightinches, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.htInlabel, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.lbsLabel, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.textBoxOthers, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_race_label, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_sex_label, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_hair_label, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_eyes_label, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_height_label, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.htftlabel, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.pwnapp_weight_label, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(480, 195);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // otherslabel
            // 
            this.otherslabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.otherslabel.AutoSize = true;
            this.otherslabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.otherslabel.Location = new System.Drawing.Point(3, 177);
            this.otherslabel.Name = "otherslabel";
            this.otherslabel.Size = new System.Drawing.Size(40, 13);
            this.otherslabel.TabIndex = 6;
            this.otherslabel.Text = "Others";
            // 
            // pwnapp_height
            // 
            this.pwnapp_height.CausesValidation = false;
            this.pwnapp_height.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.pwnapp_height.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_height.Location = new System.Drawing.Point(98, 124);
            this.pwnapp_height.MaxLength = 1;
            this.pwnapp_height.Name = "pwnapp_height";
            this.pwnapp_height.RegularExpression = true;
            this.pwnapp_height.Size = new System.Drawing.Size(30, 21);
            this.pwnapp_height.TabIndex = 5;
            this.pwnapp_height.ValidationExpression = "^([2-9])";
            // 
            // pwnapp_weight
            // 
            this.pwnapp_weight.AcceptsReturn = true;
            this.pwnapp_weight.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.pwnapp_weight, 2);
            this.pwnapp_weight.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.pwnapp_weight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_weight.Location = new System.Drawing.Point(98, 151);
            this.pwnapp_weight.MaxLength = 3;
            this.pwnapp_weight.Name = "pwnapp_weight";
            this.pwnapp_weight.RegularExpression = true;
            this.pwnapp_weight.Size = new System.Drawing.Size(45, 21);
            this.pwnapp_weight.TabIndex = 7;
            this.pwnapp_weight.ValidationExpression = "(0*[1-9][0-9]*[0-9]*)";
            // 
            // pwnapp_heightinches
            // 
            this.pwnapp_heightinches.CausesValidation = false;
            this.pwnapp_heightinches.ErrorMessage = global::Support.Properties.Resources.OverrideMachineName;
            this.pwnapp_heightinches.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_heightinches.Location = new System.Drawing.Point(157, 124);
            this.pwnapp_heightinches.MaxLength = 2;
            this.pwnapp_heightinches.Name = "pwnapp_heightinches";
            this.pwnapp_heightinches.RegularExpression = true;
            this.pwnapp_heightinches.Size = new System.Drawing.Size(22, 21);
            this.pwnapp_heightinches.TabIndex = 6;
            this.pwnapp_heightinches.ValidationExpression = "(^[1-9]{1}$|^[1-9]{1}[0]{1}$|^11$)";
            // 
            // htInlabel
            // 
            this.htInlabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.htInlabel.AutoSize = true;
            this.htInlabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.htInlabel.Location = new System.Drawing.Point(185, 128);
            this.htInlabel.Name = "htInlabel";
            this.htInlabel.Size = new System.Drawing.Size(15, 13);
            this.htInlabel.TabIndex = 15;
            this.htInlabel.Text = "in";
            // 
            // lbsLabel
            // 
            this.lbsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbsLabel.AutoSize = true;
            this.lbsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbsLabel.Location = new System.Drawing.Point(157, 154);
            this.lbsLabel.Name = "lbsLabel";
            this.lbsLabel.Size = new System.Drawing.Size(20, 13);
            this.lbsLabel.TabIndex = 16;
            this.lbsLabel.Text = "lbs";
            // 
            // textBoxOthers
            // 
            this.textBoxOthers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel1.SetColumnSpan(this.textBoxOthers, 4);
            this.textBoxOthers.Location = new System.Drawing.Point(98, 176);
            this.textBoxOthers.MaxLength = 1024;
            this.textBoxOthers.Name = "textBoxOthers";
            this.textBoxOthers.Size = new System.Drawing.Size(219, 20);
            this.textBoxOthers.TabIndex = 8;
            // 
            // pwnapp_race_label
            // 
            this.pwnapp_race_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_race_label.AutoSize = true;
            this.pwnapp_race_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_race_label.Location = new System.Drawing.Point(3, 8);
            this.pwnapp_race_label.Name = "pwnapp_race_label";
            this.pwnapp_race_label.Size = new System.Drawing.Size(31, 13);
            this.pwnapp_race_label.TabIndex = 17;
            this.pwnapp_race_label.Text = "Race";
            // 
            // pwnapp_sex_label
            // 
            this.pwnapp_sex_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_sex_label.AutoSize = true;
            this.pwnapp_sex_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_sex_label.Location = new System.Drawing.Point(3, 37);
            this.pwnapp_sex_label.Name = "pwnapp_sex_label";
            this.pwnapp_sex_label.Size = new System.Drawing.Size(42, 13);
            this.pwnapp_sex_label.TabIndex = 18;
            this.pwnapp_sex_label.Text = "Gender";
            // 
            // pwnapp_hair_label
            // 
            this.pwnapp_hair_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_hair_label.AutoSize = true;
            this.pwnapp_hair_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_hair_label.Location = new System.Drawing.Point(3, 68);
            this.pwnapp_hair_label.Name = "pwnapp_hair_label";
            this.pwnapp_hair_label.Size = new System.Drawing.Size(54, 13);
            this.pwnapp_hair_label.TabIndex = 19;
            this.pwnapp_hair_label.Text = "Hair Color";
            // 
            // pwnapp_eyes_label
            // 
            this.pwnapp_eyes_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_eyes_label.AutoSize = true;
            this.pwnapp_eyes_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_eyes_label.Location = new System.Drawing.Point(3, 99);
            this.pwnapp_eyes_label.Name = "pwnapp_eyes_label";
            this.pwnapp_eyes_label.Size = new System.Drawing.Size(53, 13);
            this.pwnapp_eyes_label.TabIndex = 20;
            this.pwnapp_eyes_label.Text = "Eye Color";
            // 
            // pwnapp_height_label
            // 
            this.pwnapp_height_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_height_label.AutoSize = true;
            this.pwnapp_height_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_height_label.Location = new System.Drawing.Point(3, 128);
            this.pwnapp_height_label.Name = "pwnapp_height_label";
            this.pwnapp_height_label.Size = new System.Drawing.Size(38, 13);
            this.pwnapp_height_label.TabIndex = 21;
            this.pwnapp_height_label.Text = "Height";
            // 
            // htftlabel
            // 
            this.htftlabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.htftlabel.AutoSize = true;
            this.htftlabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.htftlabel.Location = new System.Drawing.Point(136, 128);
            this.htftlabel.Name = "htftlabel";
            this.htftlabel.Size = new System.Drawing.Size(15, 13);
            this.htftlabel.TabIndex = 13;
            this.htftlabel.Text = "ft";
            // 
            // pwnapp_weight_label
            // 
            this.pwnapp_weight_label.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_weight_label.AutoSize = true;
            this.pwnapp_weight_label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwnapp_weight_label.Location = new System.Drawing.Point(3, 154);
            this.pwnapp_weight_label.Name = "pwnapp_weight_label";
            this.pwnapp_weight_label.Size = new System.Drawing.Size(41, 13);
            this.pwnapp_weight_label.TabIndex = 22;
            this.pwnapp_weight_label.Text = "Weight";
            // 
            // customButtonBack
            // 
            this.customButtonBack.BackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonBack.BackgroundImage")));
            this.customButtonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonBack.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonBack.FlatAppearance.BorderSize = 0;
            this.customButtonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonBack.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonBack.ForeColor = System.Drawing.Color.White;
            this.customButtonBack.Location = new System.Drawing.Point(9, 334);
            this.customButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.Name = "customButtonBack";
            this.customButtonBack.Size = new System.Drawing.Size(100, 50);
            this.customButtonBack.TabIndex = 20;
            this.customButtonBack.Text = "&Back";
            this.customButtonBack.UseVisualStyleBackColor = false;
            this.customButtonBack.Visible = false;
            this.customButtonBack.Click += new System.EventHandler(this.buttonBack_Click);
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
            this.customButtonCancel.Location = new System.Drawing.Point(173, 333);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 21;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // customButtonReset
            // 
            this.customButtonReset.BackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonReset.BackgroundImage")));
            this.customButtonReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonReset.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonReset.FlatAppearance.BorderSize = 0;
            this.customButtonReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonReset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonReset.ForeColor = System.Drawing.Color.White;
            this.customButtonReset.Location = new System.Drawing.Point(282, 333);
            this.customButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReset.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.Name = "customButtonReset";
            this.customButtonReset.Size = new System.Drawing.Size(100, 50);
            this.customButtonReset.TabIndex = 22;
            this.customButtonReset.Text = "&Reset";
            this.customButtonReset.UseVisualStyleBackColor = false;
            this.customButtonReset.Click += new System.EventHandler(this.buttonReset_Click);
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
            this.customButtonSubmit.Location = new System.Drawing.Point(390, 333);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 23;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // pwnapp_race
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pwnapp_race, 4);
            this.pwnapp_race.Location = new System.Drawing.Point(99, 4);
            this.pwnapp_race.Margin = new System.Windows.Forms.Padding(4);
            this.pwnapp_race.Name = "pwnapp_race";
            this.pwnapp_race.Size = new System.Drawing.Size(136, 21);
            this.pwnapp_race.TabIndex = 1;
            // 
            // pwnapp_sex
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pwnapp_sex, 4);
            this.pwnapp_sex.isValid = true;
            this.pwnapp_sex.Location = new System.Drawing.Point(99, 33);
            this.pwnapp_sex.Margin = new System.Windows.Forms.Padding(4);
            this.pwnapp_sex.Name = "pwnapp_sex";
            this.pwnapp_sex.Size = new System.Drawing.Size(82, 22);
            this.pwnapp_sex.TabIndex = 2;
            // 
            // pwnapp_hair
            // 
            this.pwnapp_hair.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pwnapp_hair.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.pwnapp_hair, 4);
            this.pwnapp_hair.Location = new System.Drawing.Point(99, 63);
            this.pwnapp_hair.Margin = new System.Windows.Forms.Padding(4);
            this.pwnapp_hair.Name = "pwnapp_hair";
            this.pwnapp_hair.Size = new System.Drawing.Size(135, 24);
            this.pwnapp_hair.TabIndex = 3;
            // 
            // pwnapp_eyes
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pwnapp_eyes, 4);
            this.pwnapp_eyes.isValid = true;
            this.pwnapp_eyes.Location = new System.Drawing.Point(99, 95);
            this.pwnapp_eyes.Margin = new System.Windows.Forms.Padding(4);
            this.pwnapp_eyes.Name = "pwnapp_eyes";
            this.pwnapp_eyes.Size = new System.Drawing.Size(136, 22);
            this.pwnapp_eyes.TabIndex = 4;
            // 
            // UpdatePhysicalDesc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_440_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(542, 402);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonReset);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.customButtonBack);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.labelHeading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatePhysicalDesc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Physical Desc";
            this.Load += new System.EventHandler(this.UpdatePhysicalDesc_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label otherslabel;
        private Race pwnapp_race;
        private Gender pwnapp_sex;
        private Haircolor pwnapp_hair;
        private EyeColor pwnapp_eyes;
        private CustomTextBox pwnapp_height;
        private CustomTextBox pwnapp_weight;
        private System.Windows.Forms.Label htftlabel;
        private CustomTextBox pwnapp_heightinches;
        private System.Windows.Forms.Label htInlabel;
        private System.Windows.Forms.Label lbsLabel;
        private System.Windows.Forms.TextBox textBoxOthers;
        private CustomLabel pwnapp_race_label;
        private CustomLabel pwnapp_sex_label;
        private CustomLabel pwnapp_hair_label;
        private CustomLabel pwnapp_eyes_label;
        private CustomLabel pwnapp_height_label;
        private CustomLabel pwnapp_weight_label;
        private CustomButton customButtonBack;
        private CustomButton customButtonCancel;
        private CustomButton customButtonReset;
        private CustomButton customButtonSubmit;
    }
}
