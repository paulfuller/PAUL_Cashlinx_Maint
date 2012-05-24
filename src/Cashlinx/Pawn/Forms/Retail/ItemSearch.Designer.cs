using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Retail
{
    partial class ItemSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemSearch));
            this.labelHeading = new System.Windows.Forms.Label();
            this.labelStoreNo = new System.Windows.Forms.Label();
            this.customButtonCancel1 = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonClear = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonFind = new Common.Libraries.Forms.Components.CustomButton();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.txtICN = new System.Windows.Forms.TextBox();
            this.pnlItemCostButtons = new System.Windows.Forms.Panel();
            this.customButtonSale = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonLayaway = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonContinue = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonTaxExempt = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonAddComments = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonDeleteItem = new Common.Libraries.Forms.Components.CustomButton();
            this.customButtonCancel2 = new Common.Libraries.Forms.Components.CustomButton();
            this.pnlItemCost = new System.Windows.Forms.Panel();
            this.pnlSearchButtons = new System.Windows.Forms.Panel();
            this.retailCost1 = new RetailCost();
            this.pnlSearchDetails = new DetailedItemSearch();
            this.pnlItemCostButtons.SuspendLayout();
            this.pnlItemCost.SuspendLayout();
            this.pnlSearchButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeading
            // 
            this.labelHeading.AutoSize = true;
            this.labelHeading.BackColor = System.Drawing.Color.Transparent;
            this.labelHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeading.ForeColor = System.Drawing.Color.White;
            this.labelHeading.Location = new System.Drawing.Point(407, 25);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(94, 19);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Item Search";
            // 
            // labelStoreNo
            // 
            this.labelStoreNo.AutoSize = true;
            this.labelStoreNo.BackColor = System.Drawing.Color.Transparent;
            this.labelStoreNo.Location = new System.Drawing.Point(26, 84);
            this.labelStoreNo.Name = "labelStoreNo";
            this.labelStoreNo.Size = new System.Drawing.Size(33, 13);
            this.labelStoreNo.TabIndex = 1;
            this.labelStoreNo.Text = "ICN#";
            // 
            // customButtonCancel1
            // 
            this.customButtonCancel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customButtonCancel1.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel1.BackgroundImage")));
            this.customButtonCancel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.customButtonCancel1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel1.FlatAppearance.BorderSize = 0;
            this.customButtonCancel1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel1.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel1.Location = new System.Drawing.Point(2, 0);
            this.customButtonCancel1.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel1.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel1.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel1.Name = "customButtonCancel1";
            this.customButtonCancel1.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel1.TabIndex = 0;
            this.customButtonCancel1.Text = "Cancel";
            this.customButtonCancel1.UseVisualStyleBackColor = false;
            this.customButtonCancel1.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // customButtonClear
            // 
            this.customButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customButtonClear.BackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonClear.BackgroundImage")));
            this.customButtonClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonClear.FlatAppearance.BorderSize = 0;
            this.customButtonClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonClear.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonClear.ForeColor = System.Drawing.Color.White;
            this.customButtonClear.Location = new System.Drawing.Point(202, 0);
            this.customButtonClear.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonClear.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonClear.Name = "customButtonClear";
            this.customButtonClear.Size = new System.Drawing.Size(100, 50);
            this.customButtonClear.TabIndex = 2;
            this.customButtonClear.Text = "Clear";
            this.customButtonClear.UseVisualStyleBackColor = false;
            this.customButtonClear.Click += new System.EventHandler(this.customButtonClear_Click);
            // 
            // customButtonFind
            // 
            this.customButtonFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customButtonFind.BackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonFind.BackgroundImage")));
            this.customButtonFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonFind.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonFind.FlatAppearance.BorderSize = 0;
            this.customButtonFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonFind.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonFind.ForeColor = System.Drawing.Color.White;
            this.customButtonFind.Location = new System.Drawing.Point(102, 0);
            this.customButtonFind.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonFind.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonFind.Name = "customButtonFind";
            this.customButtonFind.Size = new System.Drawing.Size(100, 50);
            this.customButtonFind.TabIndex = 1;
            this.customButtonFind.Text = "Find";
            this.customButtonFind.UseVisualStyleBackColor = false;
            this.customButtonFind.Click += new System.EventHandler(this.customButtonFind_Click);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.AutoSize = true;
            this.cmdCollapse.BackColor = System.Drawing.Color.Transparent;
            this.cmdCollapse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdCollapse.BackgroundImage")));
            this.cmdCollapse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdCollapse.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cmdCollapse.FlatAppearance.BorderSize = 0;
            this.cmdCollapse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmdCollapse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.ForeColor = System.Drawing.Color.White;
            this.cmdCollapse.Location = new System.Drawing.Point(239, 78);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(24, 27);
            this.cmdCollapse.TabIndex = 1;
            this.cmdCollapse.Text = "+";
            this.cmdCollapse.UseVisualStyleBackColor = false;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // txtICN
            // 
            this.txtICN.Location = new System.Drawing.Point(65, 81);
            this.txtICN.Name = "txtICN";
            this.txtICN.Size = new System.Drawing.Size(167, 21);
            this.txtICN.TabIndex = 0;
            this.txtICN.TextChanged += new System.EventHandler(this.txtICN_TextChanged);
            // 
            // pnlItemCostButtons
            // 
            this.pnlItemCostButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlItemCostButtons.Controls.Add(this.customButtonSale);
            this.pnlItemCostButtons.Controls.Add(this.customButtonLayaway);
            this.pnlItemCostButtons.Controls.Add(this.customButtonContinue);
            this.pnlItemCostButtons.Controls.Add(this.customButtonTaxExempt);
            this.pnlItemCostButtons.Controls.Add(this.customButtonAddComments); //newButton
            this.pnlItemCostButtons.Controls.Add(this.customButtonDeleteItem);
            this.pnlItemCostButtons.Controls.Add(this.customButtonCancel2);
            this.pnlItemCostButtons.Location = new System.Drawing.Point(12, 711);
            this.pnlItemCostButtons.Name = "pnlItemCostButtons";
            this.pnlItemCostButtons.Size = new System.Drawing.Size(876, 51);
            this.pnlItemCostButtons.TabIndex = 146;
            // 
            // customButtonSale
            // 
            this.customButtonSale.BackColor = System.Drawing.Color.Transparent;
            this.customButtonSale.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonSale.BackgroundImage")));
            this.customButtonSale.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonSale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonSale.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonSale.FlatAppearance.BorderSize = 0;
            this.customButtonSale.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonSale.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonSale.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonSale.ForeColor = System.Drawing.Color.White;
            this.customButtonSale.Location = new System.Drawing.Point(775, 0);
            this.customButtonSale.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSale.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSale.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSale.Name = "customButtonSale";
            this.customButtonSale.Size = new System.Drawing.Size(100, 50);
            this.customButtonSale.TabIndex = 12;
            this.customButtonSale.Text = "Sale";
            this.customButtonSale.UseVisualStyleBackColor = false;
            this.customButtonSale.Click += new System.EventHandler(this.customButtonSale_Click);
            // 
            // customButtonLayaway
            // 
            this.customButtonLayaway.BackColor = System.Drawing.Color.Transparent;
            this.customButtonLayaway.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonLayaway.BackgroundImage")));
            this.customButtonLayaway.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonLayaway.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonLayaway.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonLayaway.FlatAppearance.BorderSize = 0;
            this.customButtonLayaway.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonLayaway.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonLayaway.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonLayaway.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonLayaway.ForeColor = System.Drawing.Color.White;
            this.customButtonLayaway.Location = new System.Drawing.Point(676, 0);
            this.customButtonLayaway.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonLayaway.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonLayaway.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonLayaway.Name = "customButtonLayaway";
            this.customButtonLayaway.Size = new System.Drawing.Size(100, 50);
            this.customButtonLayaway.TabIndex = 11;
            this.customButtonLayaway.Text = "Layaway";
            this.customButtonLayaway.UseVisualStyleBackColor = false;
            this.customButtonLayaway.Click += new System.EventHandler(this.customButtonLayaway_Click);
            // 
            // customButtonContinue
            // 
            this.customButtonContinue.BackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonContinue.BackgroundImage")));
            this.customButtonContinue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonContinue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonContinue.FlatAppearance.BorderSize = 0;
            this.customButtonContinue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonContinue.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonContinue.ForeColor = System.Drawing.Color.White;
            this.customButtonContinue.Location = new System.Drawing.Point(775, 0);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 13;
            this.customButtonContinue.Text = "Continue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Visible = false;
            this.customButtonContinue.Click += new System.EventHandler(this.customButtonContinue_Click);
            // 
            // customButtonAddComments //newButton
            // 
            this.customButtonAddComments.BackColor = System.Drawing.Color.Transparent;
            this.customButtonAddComments.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonTaxExempt.BackgroundImage")));
            this.customButtonAddComments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonAddComments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonAddComments.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonAddComments.FlatAppearance.BorderSize = 0;
            this.customButtonAddComments.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddComments.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonAddComments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonAddComments.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonAddComments.ForeColor = System.Drawing.Color.White;
            this.customButtonAddComments.Location = new System.Drawing.Point(476, 0);
            this.customButtonAddComments.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonAddComments.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddComments.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonAddComments.Name = "customButtonAddComments";
            this.customButtonAddComments.Size = new System.Drawing.Size(100, 50);
            this.customButtonAddComments.TabIndex = 10;
            this.customButtonAddComments.Text = "Add Comments";
            this.customButtonAddComments.UseVisualStyleBackColor = false;
            this.customButtonAddComments.Click += new System.EventHandler(this.customButtonAddComments_Click);
            // 
            // customButtonTaxExempt
            // 
            this.customButtonTaxExempt.BackColor = System.Drawing.Color.Transparent;
            this.customButtonTaxExempt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonTaxExempt.BackgroundImage")));
            this.customButtonTaxExempt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonTaxExempt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonTaxExempt.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonTaxExempt.FlatAppearance.BorderSize = 0;
            this.customButtonTaxExempt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonTaxExempt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonTaxExempt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonTaxExempt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonTaxExempt.ForeColor = System.Drawing.Color.White;
            this.customButtonTaxExempt.Location = new System.Drawing.Point(576, 0);
            this.customButtonTaxExempt.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonTaxExempt.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonTaxExempt.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonTaxExempt.Name = "customButtonTaxExempt";
            this.customButtonTaxExempt.Size = new System.Drawing.Size(100, 50);
            this.customButtonTaxExempt.TabIndex = 10;
            this.customButtonTaxExempt.Text = "Tax Exempt";
            this.customButtonTaxExempt.UseVisualStyleBackColor = false;
            this.customButtonTaxExempt.Click += new System.EventHandler(this.customButtonTaxExempt_Click);
            // 
            // customButtonDeleteItem
            // 
            this.customButtonDeleteItem.BackColor = System.Drawing.Color.Transparent;
            this.customButtonDeleteItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonDeleteItem.BackgroundImage")));
            this.customButtonDeleteItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonDeleteItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonDeleteItem.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonDeleteItem.FlatAppearance.BorderSize = 0;
            this.customButtonDeleteItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonDeleteItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonDeleteItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonDeleteItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonDeleteItem.ForeColor = System.Drawing.Color.White;
            this.customButtonDeleteItem.Location = new System.Drawing.Point(148, 0);
            this.customButtonDeleteItem.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonDeleteItem.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonDeleteItem.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonDeleteItem.Name = "customButtonDeleteItem";
            this.customButtonDeleteItem.Size = new System.Drawing.Size(100, 50);
            this.customButtonDeleteItem.TabIndex = 8;
            this.customButtonDeleteItem.Text = "Delete Item";
            this.customButtonDeleteItem.UseVisualStyleBackColor = false;
            this.customButtonDeleteItem.Click += new System.EventHandler(this.customButtonDeleteItem_Click);
            // 
            // customButtonCancel2
            // 
            this.customButtonCancel2.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCancel2.BackgroundImage")));
            this.customButtonCancel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCancel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCancel2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.customButtonCancel2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCancel2.FlatAppearance.BorderSize = 0;
            this.customButtonCancel2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCancel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCancel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCancel2.ForeColor = System.Drawing.Color.White;
            this.customButtonCancel2.Location = new System.Drawing.Point(48, 0);
            this.customButtonCancel2.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel2.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel2.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel2.Name = "customButtonCancel2";
            this.customButtonCancel2.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel2.TabIndex = 7;
            this.customButtonCancel2.Text = "Cancel";
            this.customButtonCancel2.UseVisualStyleBackColor = false;
            this.customButtonCancel2.Click += new System.EventHandler(this.customButtonCancel_Click);
            // 
            // pnlItemCost
            // 
            this.pnlItemCost.AutoScroll = true;
            this.pnlItemCost.BackColor = System.Drawing.Color.Transparent;
            this.pnlItemCost.Controls.Add(this.retailCost1);
            this.pnlItemCost.Location = new System.Drawing.Point(13, 287);
            this.pnlItemCost.Name = "pnlItemCost";
            this.pnlItemCost.Size = new System.Drawing.Size(875, 419);
            this.pnlItemCost.TabIndex = 147;
            // 
            // pnlSearchButtons
            // 
            this.pnlSearchButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearchButtons.Controls.Add(this.customButtonClear);
            this.pnlSearchButtons.Controls.Add(this.customButtonCancel1);
            this.pnlSearchButtons.Controls.Add(this.customButtonFind);
            this.pnlSearchButtons.Location = new System.Drawing.Point(585, 230);
            this.pnlSearchButtons.Name = "pnlSearchButtons";
            this.pnlSearchButtons.Size = new System.Drawing.Size(303, 51);
            this.pnlSearchButtons.TabIndex = 148;
            // 
            // retailCost1
            // 
            this.retailCost1.BackColor = System.Drawing.SystemColors.Window;
            this.retailCost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.retailCost1.LayawayPaymentCalc = null;
            this.retailCost1.Location = new System.Drawing.Point(0, 0);
            this.retailCost1.Name = "retailCost1";
            this.retailCost1.OutTheDoorPrice = new decimal(new int[] {
            0,
            0,
            0,
            65536});
            this.retailCost1.SalesTaxInfo = null;
            this.retailCost1.SelectedItem = null;
            this.retailCost1.ShippingAndHandling = new decimal(new int[] {
            0,
            0,
            0,
            65536});
            this.retailCost1.Size = new System.Drawing.Size(875, 419);
            this.retailCost1.TabIndex = 0;
            this.retailCost1.EditableFieldsChanged += new System.EventHandler(this.retailCost1_EditableFieldsChanged);
            // 
            // pnlSearchDetails
            // 
            this.pnlSearchDetails.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearchDetails.Location = new System.Drawing.Point(13, 102);
            this.pnlSearchDetails.Name = "pnlSearchDetails";
            this.pnlSearchDetails.Size = new System.Drawing.Size(874, 122);
            this.pnlSearchDetails.TabIndex = 2;
            this.pnlSearchDetails.Visible = false;
            this.pnlSearchDetails.ValueChanged += new System.EventHandler(this.pnlSearchDetails_ValueChanged);
            this.pnlSearchDetails.Search += new System.EventHandler(this.pnlSearchDetails_Search);
            // 
            // ItemSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(910, 771);
            this.Controls.Add(this.pnlSearchButtons);
            this.Controls.Add(this.pnlItemCost);
            this.Controls.Add(this.pnlItemCostButtons);
            this.Controls.Add(this.txtICN);
            this.Controls.Add(this.cmdCollapse);
            this.Controls.Add(this.pnlSearchDetails);
            this.Controls.Add(this.labelStoreNo);
            this.Controls.Add(this.labelHeading);
            this.Name = "ItemSearch";
            this.Text = "BuyReturn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemSearch_FormClosing);
            this.Load += new System.EventHandler(this.ItemSearch_Load);
            this.Shown += new System.EventHandler(this.ItemSearch_Shown);
            this.pnlItemCostButtons.ResumeLayout(false);
            this.pnlItemCost.ResumeLayout(false);
            this.pnlSearchButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHeading;
        private System.Windows.Forms.Label labelStoreNo;
        private CustomButton customButtonCancel1;
        private CustomButton customButtonClear;
        private CustomButton customButtonFind;
        private DetailedItemSearch pnlSearchDetails;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.TextBox txtICN;
        private System.Windows.Forms.Panel pnlItemCostButtons;
        private CustomButton customButtonCancel2;
        private CustomButton customButtonDeleteItem;
        private CustomButton customButtonTaxExempt;
        private CustomButton customButtonAddComments;
        private CustomButton customButtonLayaway;
        private CustomButton customButtonSale;
        private System.Windows.Forms.Panel pnlItemCost;
        private System.Windows.Forms.Panel pnlSearchButtons;
        private RetailCost retailCost1;
        private CustomButton customButtonContinue;
    }
}
