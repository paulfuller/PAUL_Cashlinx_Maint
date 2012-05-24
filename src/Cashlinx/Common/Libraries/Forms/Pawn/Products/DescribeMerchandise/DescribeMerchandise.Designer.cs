namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    partial class DescribeMerchandise
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
            this.secondaryContinueButton = new System.Windows.Forms.Button();
            this.secondaryModelTextBox = new System.Windows.Forms.TextBox();
            this.secondaryModelLabel = new System.Windows.Forms.Label();
            this.secondaryManufacturerLabel = new System.Windows.Forms.Label();
            this.secondaryManufacturerTextBox = new System.Windows.Forms.TextBox();
            this.modelListBox = new System.Windows.Forms.ListBox();
            this.continueButton = new System.Windows.Forms.Button();
            this.modelTextBox = new System.Windows.Forms.TextBox();
            this.enterModelLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.selectMerchandiseCategoryLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.orLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nestFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelFormButton = new System.Windows.Forms.Button();
            this.manufacturerTextBox = new System.Windows.Forms.TextBox();
            this.CategoryCodeTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.categoryCodeTextBox = new System.Windows.Forms.TextBox();
            this.findCategoryCodeButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // secondaryContinueButton
            // 
            this.secondaryContinueButton.BackColor = System.Drawing.Color.Transparent;
            this.secondaryContinueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.secondaryContinueButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.secondaryContinueButton.FlatAppearance.BorderSize = 0;
            this.secondaryContinueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.secondaryContinueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.secondaryContinueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.secondaryContinueButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondaryContinueButton.ForeColor = System.Drawing.Color.White;
            this.secondaryContinueButton.Location = new System.Drawing.Point(400, 129);
            this.secondaryContinueButton.Margin = new System.Windows.Forms.Padding(4);
            this.secondaryContinueButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.secondaryContinueButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.secondaryContinueButton.Name = "secondaryContinueButton";
            this.secondaryContinueButton.Size = new System.Drawing.Size(100, 50);
            this.secondaryContinueButton.TabIndex = 45;
            this.secondaryContinueButton.Text = "Continue";
            this.secondaryContinueButton.UseVisualStyleBackColor = false;
            this.secondaryContinueButton.Visible = false;
            this.secondaryContinueButton.Click += new System.EventHandler(this.secondaryContinueButton_Click);
            // 
            // secondaryModelTextBox
            // 
            this.secondaryModelTextBox.AcceptsTab = true;
            this.secondaryModelTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.secondaryModelTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.secondaryModelTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.secondaryModelTextBox.Location = new System.Drawing.Point(265, 155);
            this.secondaryModelTextBox.MaxLength = 30;
            this.secondaryModelTextBox.Name = "secondaryModelTextBox";
            this.secondaryModelTextBox.Size = new System.Drawing.Size(126, 20);
            this.secondaryModelTextBox.TabIndex = 44;
            this.secondaryModelTextBox.Tag = "findManufacturerButton";
            this.secondaryModelTextBox.Visible = false;
            this.secondaryModelTextBox.TextChanged += new System.EventHandler(this.secondaryModelTextBox_TextChanged);
            this.secondaryModelTextBox.Enter += new System.EventHandler(this.controlEnter);
            this.secondaryModelTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.secondaryModelTextBox_PreviewKeyDown);
            // 
            // secondaryModelLabel
            // 
            this.secondaryModelLabel.AutoSize = true;
            this.secondaryModelLabel.BackColor = System.Drawing.Color.Transparent;
            this.secondaryModelLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondaryModelLabel.Location = new System.Drawing.Point(19, 155);
            this.secondaryModelLabel.Name = "secondaryModelLabel";
            this.secondaryModelLabel.Size = new System.Drawing.Size(109, 13);
            this.secondaryModelLabel.TabIndex = 62;
            this.secondaryModelLabel.Text = "Select or Enter Model";
            this.secondaryModelLabel.Visible = false;
            // 
            // secondaryManufacturerLabel
            // 
            this.secondaryManufacturerLabel.AutoSize = true;
            this.secondaryManufacturerLabel.BackColor = System.Drawing.Color.Transparent;
            this.secondaryManufacturerLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondaryManufacturerLabel.Location = new System.Drawing.Point(19, 133);
            this.secondaryManufacturerLabel.Name = "secondaryManufacturerLabel";
            this.secondaryManufacturerLabel.Size = new System.Drawing.Size(101, 13);
            this.secondaryManufacturerLabel.TabIndex = 61;
            this.secondaryManufacturerLabel.Text = "Enter Manufacturer";
            // 
            // secondaryManufacturerTextBox
            // 
            this.secondaryManufacturerTextBox.AcceptsTab = true;
            this.secondaryManufacturerTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.secondaryManufacturerTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.secondaryManufacturerTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.secondaryManufacturerTextBox.Location = new System.Drawing.Point(265, 133);
            this.secondaryManufacturerTextBox.MaxLength = 30;
            this.secondaryManufacturerTextBox.Name = "secondaryManufacturerTextBox";
            this.secondaryManufacturerTextBox.Size = new System.Drawing.Size(126, 20);
            this.secondaryManufacturerTextBox.TabIndex = 43;
            this.secondaryManufacturerTextBox.Tag = "findManufacturerButton";
            this.secondaryManufacturerTextBox.TextChanged += new System.EventHandler(this.secondaryManufacturerTextBox_TextChanged);
            this.secondaryManufacturerTextBox.Enter += new System.EventHandler(this.controlEnter);
            this.secondaryManufacturerTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.secondaryManufacturerTextBox_PreviewKeyDown);
            // 
            // modelListBox
            // 
            this.modelListBox.FormattingEnabled = true;
            this.modelListBox.Location = new System.Drawing.Point(577, 85);
            this.modelListBox.Name = "modelListBox";
            this.modelListBox.Size = new System.Drawing.Size(156, 368);
            this.modelListBox.TabIndex = 45;
            this.modelListBox.TabStop = false;
            this.modelListBox.Visible = false;
            this.modelListBox.Click += new System.EventHandler(this.enterModelListBox_Click);
            // 
            // continueButton
            // 
            this.continueButton.BackColor = System.Drawing.Color.Transparent;
            this.continueButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(398, 85);
            this.continueButton.Margin = new System.Windows.Forms.Padding(4);
            this.continueButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.continueButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(100, 50);
            this.continueButton.TabIndex = 42;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Visible = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // modelTextBox
            // 
            this.modelTextBox.AcceptsTab = true;
            this.modelTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.modelTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.modelTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.modelTextBox.Location = new System.Drawing.Point(265, 107);
            this.modelTextBox.MaxLength = 30;
            this.modelTextBox.Name = "modelTextBox";
            this.modelTextBox.Size = new System.Drawing.Size(126, 20);
            this.modelTextBox.TabIndex = 41;
            this.modelTextBox.Tag = "findManufacturerButton";
            this.modelTextBox.Visible = false;
            this.modelTextBox.TextChanged += new System.EventHandler(this.modelTextBox_TextChanged);
            this.modelTextBox.Enter += new System.EventHandler(this.controlEnter);
            this.modelTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.modelTextBox_PreviewKeyDown);
            // 
            // enterModelLabel
            // 
            this.enterModelLabel.AutoSize = true;
            this.enterModelLabel.BackColor = System.Drawing.Color.Transparent;
            this.enterModelLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enterModelLabel.Location = new System.Drawing.Point(19, 110);
            this.enterModelLabel.Name = "enterModelLabel";
            this.enterModelLabel.Size = new System.Drawing.Size(109, 13);
            this.enterModelLabel.TabIndex = 57;
            this.enterModelLabel.Text = "Select or Enter Model";
            this.enterModelLabel.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(291, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(161, 19);
            this.label6.TabIndex = 56;
            this.label6.Text = "Describe Merchandise";
            // 
            // selectMerchandiseCategoryLabel
            // 
            this.selectMerchandiseCategoryLabel.AutoSize = true;
            this.selectMerchandiseCategoryLabel.BackColor = System.Drawing.Color.Transparent;
            this.selectMerchandiseCategoryLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectMerchandiseCategoryLabel.Location = new System.Drawing.Point(19, 201);
            this.selectMerchandiseCategoryLabel.Name = "selectMerchandiseCategoryLabel";
            this.selectMerchandiseCategoryLabel.Size = new System.Drawing.Size(147, 13);
            this.selectMerchandiseCategoryLabel.TabIndex = 55;
            this.selectMerchandiseCategoryLabel.Text = "Select Merchandise Category";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(374, 390);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "OR";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(402, 390);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Enter Merchandise Category";
            this.label3.Visible = false;
            // 
            // orLabel
            // 
            this.orLabel.AutoSize = true;
            this.orLabel.BackColor = System.Drawing.Color.Transparent;
            this.orLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orLabel.Location = new System.Drawing.Point(19, 180);
            this.orLabel.Name = "orLabel";
            this.orLabel.Size = new System.Drawing.Size(22, 13);
            this.orLabel.TabIndex = 52;
            this.orLabel.Text = "OR";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Enter Manufacturer";
            // 
            // nestFlowLayoutPanel
            // 
            this.nestFlowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.nestFlowLayoutPanel.Location = new System.Drawing.Point(12, 217);
            this.nestFlowLayoutPanel.Name = "nestFlowLayoutPanel";
            this.nestFlowLayoutPanel.Size = new System.Drawing.Size(552, 40);
            this.nestFlowLayoutPanel.TabIndex = 50;
            // 
            // cancelFormButton
            // 
            this.cancelFormButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelFormButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancelFormButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancelFormButton.FlatAppearance.BorderSize = 0;
            this.cancelFormButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cancelFormButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cancelFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelFormButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelFormButton.ForeColor = System.Drawing.Color.White;
            this.cancelFormButton.Location = new System.Drawing.Point(633, 508);
            this.cancelFormButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelFormButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.cancelFormButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.cancelFormButton.Name = "cancelFormButton";
            this.cancelFormButton.Size = new System.Drawing.Size(100, 50);
            this.cancelFormButton.TabIndex = 47;
            this.cancelFormButton.TabStop = false;
            this.cancelFormButton.Text = "Cancel";
            this.cancelFormButton.UseVisualStyleBackColor = false;
            this.cancelFormButton.Click += new System.EventHandler(this.cancelFormButton_Click);
            // 
            // manufacturerTextBox
            // 
            this.manufacturerTextBox.AcceptsTab = true;
            this.manufacturerTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.manufacturerTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.manufacturerTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.manufacturerTextBox.Location = new System.Drawing.Point(265, 85);
            this.manufacturerTextBox.MaxLength = 30;
            this.manufacturerTextBox.Name = "manufacturerTextBox";
            this.manufacturerTextBox.Size = new System.Drawing.Size(126, 20);
            this.manufacturerTextBox.TabIndex = 40;
            this.manufacturerTextBox.Tag = "findManufacturerButton";
            this.manufacturerTextBox.TextChanged += new System.EventHandler(this.manufacturerTextBox_TextChanged);
            this.manufacturerTextBox.Enter += new System.EventHandler(this.controlEnter);
            this.manufacturerTextBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.manufacturerTextBox_PreviewKeyDown);
            // 
            // CategoryCodeTableLayoutPanel
            // 
            this.CategoryCodeTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CategoryCodeTableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.CategoryCodeTableLayoutPanel.ColumnCount = 1;
            this.CategoryCodeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CategoryCodeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CategoryCodeTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.CategoryCodeTableLayoutPanel.Location = new System.Drawing.Point(22, 259);
            this.CategoryCodeTableLayoutPanel.Name = "CategoryCodeTableLayoutPanel";
            this.CategoryCodeTableLayoutPanel.RowCount = 9;
            this.CategoryCodeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CategoryCodeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CategoryCodeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CategoryCodeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CategoryCodeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CategoryCodeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CategoryCodeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CategoryCodeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CategoryCodeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.CategoryCodeTableLayoutPanel.Size = new System.Drawing.Size(250, 270);
            this.CategoryCodeTableLayoutPanel.TabIndex = 46;
            this.CategoryCodeTableLayoutPanel.Enter += new System.EventHandler(this.CategoryCodeTableLayoutPanel_Enter);
            // 
            // categoryCodeTextBox
            // 
            this.categoryCodeTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.categoryCodeTextBox.Location = new System.Drawing.Point(400, 406);
            this.categoryCodeTextBox.MaxLength = 4;
            this.categoryCodeTextBox.Name = "categoryCodeTextBox";
            this.categoryCodeTextBox.Size = new System.Drawing.Size(126, 20);
            this.categoryCodeTextBox.TabIndex = 43;
            this.categoryCodeTextBox.Tag = "findCategoryCodeButton";
            this.categoryCodeTextBox.Visible = false;
            // 
            // findCategoryCodeButton
            // 
            this.findCategoryCodeButton.BackColor = System.Drawing.Color.Transparent;
            this.findCategoryCodeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.findCategoryCodeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.findCategoryCodeButton.FlatAppearance.BorderSize = 0;
            this.findCategoryCodeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.findCategoryCodeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.findCategoryCodeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findCategoryCodeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findCategoryCodeButton.ForeColor = System.Drawing.Color.White;
            this.findCategoryCodeButton.Location = new System.Drawing.Point(400, 426);
            this.findCategoryCodeButton.Margin = new System.Windows.Forms.Padding(4);
            this.findCategoryCodeButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.findCategoryCodeButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.findCategoryCodeButton.Name = "findCategoryCodeButton";
            this.findCategoryCodeButton.Size = new System.Drawing.Size(100, 50);
            this.findCategoryCodeButton.TabIndex = 44;
            this.findCategoryCodeButton.Text = "Find";
            this.findCategoryCodeButton.UseVisualStyleBackColor = false;
            this.findCategoryCodeButton.Visible = false;
            this.findCategoryCodeButton.Click += new System.EventHandler(this.findCategoryCodeButton_Click);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.Location = new System.Drawing.Point(525, 508);
            this.backButton.Margin = new System.Windows.Forms.Padding(4);
            this.backButton.MaximumSize = new System.Drawing.Size(100, 50);
            this.backButton.MinimumSize = new System.Drawing.Size(100, 50);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(100, 50);
            this.backButton.TabIndex = 63;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Visible = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // DescribeMerchandise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(742, 571);
            this.ControlBox = false;
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.secondaryContinueButton);
            this.Controls.Add(this.secondaryModelTextBox);
            this.Controls.Add(this.secondaryModelLabel);
            this.Controls.Add(this.secondaryManufacturerLabel);
            this.Controls.Add(this.secondaryManufacturerTextBox);
            this.Controls.Add(this.modelListBox);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.modelTextBox);
            this.Controls.Add(this.enterModelLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.selectMerchandiseCategoryLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.orLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nestFlowLayoutPanel);
            this.Controls.Add(this.cancelFormButton);
            this.Controls.Add(this.manufacturerTextBox);
            this.Controls.Add(this.CategoryCodeTableLayoutPanel);
            this.Controls.Add(this.categoryCodeTextBox);
            this.Controls.Add(this.findCategoryCodeButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DescribeMerchandise";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Describe Merchandise";
            this.Load += new System.EventHandler(this.DescribeMerchandise_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DescribeMerchandise_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DescribeMerchandise_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button secondaryContinueButton;
        private System.Windows.Forms.TextBox secondaryModelTextBox;
        private System.Windows.Forms.Label secondaryModelLabel;
        private System.Windows.Forms.Label secondaryManufacturerLabel;
        private System.Windows.Forms.TextBox secondaryManufacturerTextBox;
        private System.Windows.Forms.ListBox modelListBox;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.TextBox modelTextBox;
        private System.Windows.Forms.Label enterModelLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label selectMerchandiseCategoryLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label orLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel nestFlowLayoutPanel;
        private System.Windows.Forms.Button cancelFormButton;
        private System.Windows.Forms.TextBox manufacturerTextBox;
        private System.Windows.Forms.TableLayoutPanel CategoryCodeTableLayoutPanel;
        private System.Windows.Forms.TextBox categoryCodeTextBox;
        private System.Windows.Forms.Button findCategoryCodeButton;
        //private System.Windows.Forms.Label merchandiseCountValueLabel;
        //private System.Windows.Forms.Label merchandiseCountLabel;
        private System.Windows.Forms.Button backButton;

    }
}
