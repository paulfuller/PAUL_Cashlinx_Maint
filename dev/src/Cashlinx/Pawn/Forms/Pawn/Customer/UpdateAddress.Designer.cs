using Common.Libraries.Forms.Components;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Pawn.Customer
{
    partial class UpdateAddress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAddress));
            this.addressFormLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelPhysicalAddrHeading = new System.Windows.Forms.Label();
            this.labelPhysicalAddrNotes = new System.Windows.Forms.Label();
            this.labelWorkAddHeading = new System.Windows.Forms.Label();
            this.labelWorkAddrUnit = new System.Windows.Forms.Label();
            this.labelWorkAddrNotes = new System.Windows.Forms.Label();
            this.custPhysicalAddr1 = new CustomTextBox();
            this.custPhysicalAddrUnit = new CustomTextBox();
            this.custPhysicalAddr2 = new CustomTextBox();
            this.custPhysicalAddrState = new State();
            this.custPhysicalAddrNotes = new CustomTextBox();
            this.custWorkAddr1 = new CustomTextBox();
            this.custWorkAddrUnit = new CustomTextBox();
            this.custWorkAddrState = new State();
            this.custWorkAddrNotes = new CustomTextBox();
            this.custWorkAddr2 = new CustomTextBox();
            this.custPhysicalAddrCountry = new System.Windows.Forms.ComboBox();
            this.custWorkAddrCountry = new System.Windows.Forms.ComboBox();
            this.labelPhysicalAddrUnit = new System.Windows.Forms.Label();
            this.customLabelPhysicalAddr = new CustomLabel();
            this.customLabelPhysAddrState = new CustomLabel();
            this.customLabelPhysAddrCountry = new CustomLabel();
            this.customLabelWorkAddr = new CustomLabel();
            this.customLabelWorkAddrState = new CustomLabel();
            this.customLabelWorkAddrCountry = new CustomLabel();
            this.custPhysicalAddrCity = new CustomTextBox();
            this.custPhysicalAddrZipcode = new Zipcode();
            this.customLabelPhysicalAddrZip = new CustomLabel();
            this.customLabelPhysicalAddrCity = new CustomLabel();
            this.customLabelWorkAddrZip = new CustomLabel();
            this.customLabelWorkAddrCity = new CustomLabel();
            this.custWorkAddrCity = new CustomTextBox();
            this.custWorkAddrZipcode = new Zipcode();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelMailingAddrHeading = new System.Windows.Forms.Label();
            this.labelMailingAddrUnit = new System.Windows.Forms.Label();
            this.labelMailingAddrNotes = new System.Windows.Forms.Label();
            this.labelAddlAddressHeading = new System.Windows.Forms.Label();
            this.labelAddlAddress = new System.Windows.Forms.Label();
            this.labelAddlAddrUnit = new System.Windows.Forms.Label();
            this.labelAddlAddrState = new System.Windows.Forms.Label();
            this.labelAddlAddrCountry = new System.Windows.Forms.Label();
            this.labelAddlAddrNotes = new System.Windows.Forms.Label();
            this.checkBoxMailingSameAsPhysical = new System.Windows.Forms.CheckBox();
            this.custMailingAddr1 = new CustomTextBox();
            this.custMailingAddrUnit = new CustomTextBox();
            this.custMailingAddr2 = new CustomTextBox();
            this.custMailingAddrState = new State();
            this.custMailingAddrNotes = new CustomTextBox();
            this.custAddlAddr1 = new CustomTextBox();
            this.custAddlAddr2 = new CustomTextBox();
            this.custAddlAddrState = new State();
            this.custAddlAddrNotes = new CustomTextBox();
            this.custAddlAddrUnit = new CustomTextBox();
            this.custAddlAddrAlternateString = new CustomTextBox();
            this.custMailingAddrCountry = new System.Windows.Forms.ComboBox();
            this.custAddlAddrCountry = new System.Windows.Forms.ComboBox();
            this.customLabelMailingAddr = new CustomLabel();
            this.customLabelMailingAddrState = new CustomLabel();
            this.customLabelMailingAddrCountry = new CustomLabel();
            this.customLabelMailingAddrZip = new CustomLabel();
            this.customLabelMailingAddrCity = new CustomLabel();
            this.custMailingAddrCity = new CustomTextBox();
            this.custMailingAddrZipcode = new Zipcode();
            this.labelAddlAddrZip = new System.Windows.Forms.Label();
            this.labelAddlAddrCity = new System.Windows.Forms.Label();
            this.custAddlAddrCity = new CustomTextBox();
            this.custAddlAddrZipcode = new Zipcode();
            this.customButtonContinue = new CustomButton();
            this.customButtonSubmit = new CustomButton();
            this.customButtonReset = new CustomButton();
            this.customButtonBack = new CustomButton();
            this.customButtonCancel = new CustomButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // addressFormLabel
            // 
            this.addressFormLabel.AutoSize = true;
            this.addressFormLabel.BackColor = System.Drawing.Color.Transparent;
            this.addressFormLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addressFormLabel.ForeColor = System.Drawing.Color.White;
            this.addressFormLabel.Location = new System.Drawing.Point(13, 24);
            this.addressFormLabel.Name = "addressFormLabel";
            this.addressFormLabel.Size = new System.Drawing.Size(112, 16);
            this.addressFormLabel.TabIndex = 0;
            this.addressFormLabel.Text = "Update Address";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CausesValidation = false;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.Controls.Add(this.labelPhysicalAddrHeading, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPhysicalAddrNotes, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelWorkAddHeading, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelWorkAddrUnit, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.labelWorkAddrNotes, 0, 15);
            this.tableLayoutPanel1.Controls.Add(this.custPhysicalAddr1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.custPhysicalAddrUnit, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.custPhysicalAddr2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.custPhysicalAddrState, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.custPhysicalAddrNotes, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.custWorkAddr1, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.custWorkAddrUnit, 3, 9);
            this.tableLayoutPanel1.Controls.Add(this.custWorkAddrState, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.custWorkAddrNotes, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.custWorkAddr2, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.custPhysicalAddrCountry, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.custWorkAddrCountry, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.labelPhysicalAddrUnit, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelPhysicalAddr, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.customLabelPhysAddrState, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.customLabelPhysAddrCountry, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.customLabelWorkAddr, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.customLabelWorkAddrState, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.customLabelWorkAddrCountry, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.custPhysicalAddrCity, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.custPhysicalAddrZipcode, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelPhysicalAddrZip, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.customLabelPhysicalAddrCity, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.customLabelWorkAddrZip, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.customLabelWorkAddrCity, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.custWorkAddrCity, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.custWorkAddrZipcode, 1, 11);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 60);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 16;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(396, 432);
            this.tableLayoutPanel1.TabIndex = 100;
            // 
            // labelPhysicalAddrHeading
            // 
            this.labelPhysicalAddrHeading.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPhysicalAddrHeading.AutoSize = true;
            this.labelPhysicalAddrHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPhysicalAddrHeading.Location = new System.Drawing.Point(3, 8);
            this.labelPhysicalAddrHeading.Name = "labelPhysicalAddrHeading";
            this.labelPhysicalAddrHeading.Size = new System.Drawing.Size(102, 13);
            this.labelPhysicalAddrHeading.TabIndex = 120;
            this.labelPhysicalAddrHeading.Text = "Physical Address";
            // 
            // labelPhysicalAddrNotes
            // 
            this.labelPhysicalAddrNotes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPhysicalAddrNotes.AutoSize = true;
            this.labelPhysicalAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPhysicalAddrNotes.Location = new System.Drawing.Point(3, 192);
            this.labelPhysicalAddrNotes.Name = "labelPhysicalAddrNotes";
            this.labelPhysicalAddrNotes.Size = new System.Drawing.Size(35, 13);
            this.labelPhysicalAddrNotes.TabIndex = 8;
            this.labelPhysicalAddrNotes.Text = "Notes";
            // 
            // labelWorkAddHeading
            // 
            this.labelWorkAddHeading.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelWorkAddHeading.AutoSize = true;
            this.labelWorkAddHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWorkAddHeading.Location = new System.Drawing.Point(3, 219);
            this.labelWorkAddHeading.Name = "labelWorkAddHeading";
            this.labelWorkAddHeading.Size = new System.Drawing.Size(86, 13);
            this.labelWorkAddHeading.TabIndex = 121;
            this.labelWorkAddHeading.Text = "Work Address";
            // 
            // labelWorkAddrUnit
            // 
            this.labelWorkAddrUnit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelWorkAddrUnit.AutoSize = true;
            this.labelWorkAddrUnit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWorkAddrUnit.Location = new System.Drawing.Point(306, 245);
            this.labelWorkAddrUnit.Name = "labelWorkAddrUnit";
            this.labelWorkAddrUnit.Size = new System.Drawing.Size(26, 13);
            this.labelWorkAddrUnit.TabIndex = 11;
            this.labelWorkAddrUnit.Text = "Unit";
            // 
            // labelWorkAddrNotes
            // 
            this.labelWorkAddrNotes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelWorkAddrNotes.AutoSize = true;
            this.labelWorkAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWorkAddrNotes.Location = new System.Drawing.Point(3, 407);
            this.labelWorkAddrNotes.Name = "labelWorkAddrNotes";
            this.labelWorkAddrNotes.Size = new System.Drawing.Size(35, 13);
            this.labelWorkAddrNotes.TabIndex = 16;
            this.labelWorkAddrNotes.Text = "Notes";
            // 
            // custPhysicalAddr1
            // 
            this.custPhysicalAddr1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.custPhysicalAddr1.CausesValidation = false;
            this.custPhysicalAddr1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.custPhysicalAddr1.ErrorMessage = "";
            this.custPhysicalAddr1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custPhysicalAddr1.Location = new System.Drawing.Point(118, 33);
            this.custPhysicalAddr1.MaxLength = 40;
            this.custPhysicalAddr1.Name = "custPhysicalAddr1";
            this.custPhysicalAddr1.Required = true;
            this.custPhysicalAddr1.Size = new System.Drawing.Size(170, 21);
            this.custPhysicalAddr1.TabIndex = 1;
            this.custPhysicalAddr1.ValidationExpression = "";
            this.custPhysicalAddr1.TextChanged += new System.EventHandler(this.custPhysicalAddr1_TextChanged);
            this.custPhysicalAddr1.Leave += new System.EventHandler(this.custPhysicalAddr1_Leave);
            // 
            // custPhysicalAddrUnit
            // 
            this.custPhysicalAddrUnit.CausesValidation = false;
            this.custPhysicalAddrUnit.ErrorMessage = "";
            this.custPhysicalAddrUnit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custPhysicalAddrUnit.Location = new System.Drawing.Point(338, 33);
            this.custPhysicalAddrUnit.MaxLength = 5;
            this.custPhysicalAddrUnit.Name = "custPhysicalAddrUnit";
            this.custPhysicalAddrUnit.Size = new System.Drawing.Size(53, 21);
            this.custPhysicalAddrUnit.TabIndex = 3;
            this.custPhysicalAddrUnit.ValidationExpression = "";
            // 
            // custPhysicalAddr2
            // 
            this.custPhysicalAddr2.CausesValidation = false;
            this.custPhysicalAddr2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel1.SetColumnSpan(this.custPhysicalAddr2, 3);
            this.custPhysicalAddr2.ErrorMessage = "";
            this.custPhysicalAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custPhysicalAddr2.Location = new System.Drawing.Point(118, 58);
            this.custPhysicalAddr2.MaxLength = 39;
            this.custPhysicalAddr2.Name = "custPhysicalAddr2";
            this.custPhysicalAddr2.Size = new System.Drawing.Size(170, 21);
            this.custPhysicalAddr2.TabIndex = 2;
            this.custPhysicalAddr2.ValidationExpression = "";
            // 
            // custPhysicalAddrState
            // 
            this.custPhysicalAddrState.BackColor = System.Drawing.Color.Transparent;
            this.custPhysicalAddrState.CausesValidation = false;
            this.custPhysicalAddrState.DisplayCode = false;
            this.custPhysicalAddrState.ForeColor = System.Drawing.Color.Black;
            this.custPhysicalAddrState.Location = new System.Drawing.Point(118, 134);
            this.custPhysicalAddrState.Name = "custPhysicalAddrState";
            this.custPhysicalAddrState.Required = true;
            this.custPhysicalAddrState.Size = new System.Drawing.Size(50, 20);
            this.custPhysicalAddrState.TabIndex = 6;
            // 
            // custPhysicalAddrNotes
            // 
            this.custPhysicalAddrNotes.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.custPhysicalAddrNotes, 3);
            this.custPhysicalAddrNotes.ErrorMessage = "";
            this.custPhysicalAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custPhysicalAddrNotes.Location = new System.Drawing.Point(118, 188);
            this.custPhysicalAddrNotes.MaxLength = 40;
            this.custPhysicalAddrNotes.Name = "custPhysicalAddrNotes";
            this.custPhysicalAddrNotes.Size = new System.Drawing.Size(212, 21);
            this.custPhysicalAddrNotes.TabIndex = 8;
            this.custPhysicalAddrNotes.ValidationExpression = "";
            // 
            // custWorkAddr1
            // 
            this.custWorkAddr1.CausesValidation = false;
            this.custWorkAddr1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.custWorkAddr1.ErrorMessage = "";
            this.custWorkAddr1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custWorkAddr1.Location = new System.Drawing.Point(118, 241);
            this.custWorkAddr1.MaxLength = 40;
            this.custWorkAddr1.Name = "custWorkAddr1";
            this.custWorkAddr1.Required = true;
            this.custWorkAddr1.Size = new System.Drawing.Size(170, 21);
            this.custWorkAddr1.TabIndex = 9;
            this.custWorkAddr1.ValidationExpression = "";
            this.custWorkAddr1.TextChanged += new System.EventHandler(this.custWorkAddr1_TextChanged);
            this.custWorkAddr1.Leave += new System.EventHandler(this.custWorkAddr1_Leave);
            // 
            // custWorkAddrUnit
            // 
            this.custWorkAddrUnit.CausesValidation = false;
            this.custWorkAddrUnit.ErrorMessage = "";
            this.custWorkAddrUnit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custWorkAddrUnit.Location = new System.Drawing.Point(338, 241);
            this.custWorkAddrUnit.MaxLength = 5;
            this.custWorkAddrUnit.Name = "custWorkAddrUnit";
            this.custWorkAddrUnit.Size = new System.Drawing.Size(53, 21);
            this.custWorkAddrUnit.TabIndex = 11;
            this.custWorkAddrUnit.ValidationExpression = "";
            // 
            // custWorkAddrState
            // 
            this.custWorkAddrState.BackColor = System.Drawing.Color.Transparent;
            this.custWorkAddrState.CausesValidation = false;
            this.custWorkAddrState.DisplayCode = false;
            this.custWorkAddrState.ForeColor = System.Drawing.Color.Black;
            this.custWorkAddrState.Location = new System.Drawing.Point(118, 344);
            this.custWorkAddrState.Name = "custWorkAddrState";
            this.custWorkAddrState.Required = true;
            this.custWorkAddrState.Size = new System.Drawing.Size(50, 20);
            this.custWorkAddrState.TabIndex = 14;
            // 
            // custWorkAddrNotes
            // 
            this.custWorkAddrNotes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.custWorkAddrNotes.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.custWorkAddrNotes, 3);
            this.custWorkAddrNotes.ErrorMessage = "";
            this.custWorkAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custWorkAddrNotes.Location = new System.Drawing.Point(118, 403);
            this.custWorkAddrNotes.MaxLength = 40;
            this.custWorkAddrNotes.Name = "custWorkAddrNotes";
            this.custWorkAddrNotes.Size = new System.Drawing.Size(212, 21);
            this.custWorkAddrNotes.TabIndex = 16;
            this.custWorkAddrNotes.ValidationExpression = "";
            // 
            // custWorkAddr2
            // 
            this.custWorkAddr2.CausesValidation = false;
            this.custWorkAddr2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.custWorkAddr2.ErrorMessage = "";
            this.custWorkAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custWorkAddr2.Location = new System.Drawing.Point(118, 268);
            this.custWorkAddr2.MaxLength = 39;
            this.custWorkAddr2.Name = "custWorkAddr2";
            this.custWorkAddr2.Size = new System.Drawing.Size(170, 21);
            this.custWorkAddr2.TabIndex = 10;
            this.custWorkAddr2.ValidationExpression = "";
            // 
            // custPhysicalAddrCountry
            // 
            this.custPhysicalAddrCountry.CausesValidation = false;
            this.custPhysicalAddrCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.custPhysicalAddrCountry.FormattingEnabled = true;
            this.custPhysicalAddrCountry.Items.AddRange(new object[] {
            "United States",
            "Canada",
            "Mexico"});
            this.custPhysicalAddrCountry.Location = new System.Drawing.Point(118, 160);
            this.custPhysicalAddrCountry.Name = "custPhysicalAddrCountry";
            this.custPhysicalAddrCountry.Size = new System.Drawing.Size(170, 21);
            this.custPhysicalAddrCountry.TabIndex = 7;
            // 
            // custWorkAddrCountry
            // 
            this.custWorkAddrCountry.CausesValidation = false;
            this.custWorkAddrCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.custWorkAddrCountry.FormattingEnabled = true;
            this.custWorkAddrCountry.Items.AddRange(new object[] {
            "United States",
            "Canada",
            "Mexico"});
            this.custWorkAddrCountry.Location = new System.Drawing.Point(118, 371);
            this.custWorkAddrCountry.Name = "custWorkAddrCountry";
            this.custWorkAddrCountry.Size = new System.Drawing.Size(170, 21);
            this.custWorkAddrCountry.TabIndex = 15;
            // 
            // labelPhysicalAddrUnit
            // 
            this.labelPhysicalAddrUnit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelPhysicalAddrUnit.AutoSize = true;
            this.labelPhysicalAddrUnit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPhysicalAddrUnit.Location = new System.Drawing.Point(306, 36);
            this.labelPhysicalAddrUnit.Name = "labelPhysicalAddrUnit";
            this.labelPhysicalAddrUnit.Size = new System.Drawing.Size(26, 13);
            this.labelPhysicalAddrUnit.TabIndex = 3;
            this.labelPhysicalAddrUnit.Text = "Unit";
            // 
            // customLabelPhysicalAddr
            // 
            this.customLabelPhysicalAddr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelPhysicalAddr.AutoSize = true;
            this.customLabelPhysicalAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelPhysicalAddr.Location = new System.Drawing.Point(3, 36);
            this.customLabelPhysicalAddr.Name = "customLabelPhysicalAddr";
            this.customLabelPhysicalAddr.Required = true;
            this.customLabelPhysicalAddr.Size = new System.Drawing.Size(46, 13);
            this.customLabelPhysicalAddr.TabIndex = 122;
            this.customLabelPhysicalAddr.Text = "Address";
            // 
            // customLabelPhysAddrState
            // 
            this.customLabelPhysAddrState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelPhysAddrState.AutoSize = true;
            this.customLabelPhysAddrState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelPhysAddrState.Location = new System.Drawing.Point(3, 137);
            this.customLabelPhysAddrState.Name = "customLabelPhysAddrState";
            this.customLabelPhysAddrState.Required = true;
            this.customLabelPhysAddrState.Size = new System.Drawing.Size(33, 13);
            this.customLabelPhysAddrState.TabIndex = 125;
            this.customLabelPhysAddrState.Text = "State";
            // 
            // customLabelPhysAddrCountry
            // 
            this.customLabelPhysAddrCountry.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelPhysAddrCountry.AutoSize = true;
            this.customLabelPhysAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelPhysAddrCountry.Location = new System.Drawing.Point(3, 164);
            this.customLabelPhysAddrCountry.Name = "customLabelPhysAddrCountry";
            this.customLabelPhysAddrCountry.Required = true;
            this.customLabelPhysAddrCountry.Size = new System.Drawing.Size(46, 13);
            this.customLabelPhysAddrCountry.TabIndex = 126;
            this.customLabelPhysAddrCountry.Text = "Country";
            // 
            // customLabelWorkAddr
            // 
            this.customLabelWorkAddr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelWorkAddr.AutoSize = true;
            this.customLabelWorkAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelWorkAddr.Location = new System.Drawing.Point(3, 245);
            this.customLabelWorkAddr.Name = "customLabelWorkAddr";
            this.customLabelWorkAddr.Required = true;
            this.customLabelWorkAddr.Size = new System.Drawing.Size(46, 13);
            this.customLabelWorkAddr.TabIndex = 127;
            this.customLabelWorkAddr.Text = "Address";
            // 
            // customLabelWorkAddrState
            // 
            this.customLabelWorkAddrState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelWorkAddrState.AutoSize = true;
            this.customLabelWorkAddrState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelWorkAddrState.Location = new System.Drawing.Point(3, 348);
            this.customLabelWorkAddrState.Name = "customLabelWorkAddrState";
            this.customLabelWorkAddrState.Required = true;
            this.customLabelWorkAddrState.Size = new System.Drawing.Size(33, 13);
            this.customLabelWorkAddrState.TabIndex = 130;
            this.customLabelWorkAddrState.Text = "State";
            // 
            // customLabelWorkAddrCountry
            // 
            this.customLabelWorkAddrCountry.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelWorkAddrCountry.AutoSize = true;
            this.customLabelWorkAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelWorkAddrCountry.Location = new System.Drawing.Point(3, 375);
            this.customLabelWorkAddrCountry.Name = "customLabelWorkAddrCountry";
            this.customLabelWorkAddrCountry.Required = true;
            this.customLabelWorkAddrCountry.Size = new System.Drawing.Size(46, 13);
            this.customLabelWorkAddrCountry.TabIndex = 131;
            this.customLabelWorkAddrCountry.Text = "Country";
            // 
            // custPhysicalAddrCity
            // 
            this.custPhysicalAddrCity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.custPhysicalAddrCity.CausesValidation = false;
            this.tableLayoutPanel1.SetColumnSpan(this.custPhysicalAddrCity, 3);
            this.custPhysicalAddrCity.ErrorMessage = "";
            this.custPhysicalAddrCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custPhysicalAddrCity.Location = new System.Drawing.Point(118, 108);
            this.custPhysicalAddrCity.MaxLength = 50;
            this.custPhysicalAddrCity.Name = "custPhysicalAddrCity";
            this.custPhysicalAddrCity.Required = true;
            this.custPhysicalAddrCity.Size = new System.Drawing.Size(170, 21);
            this.custPhysicalAddrCity.TabIndex = 5;
            this.custPhysicalAddrCity.ValidationExpression = "";
            // 
            // custPhysicalAddrZipcode
            // 
            this.custPhysicalAddrZipcode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.custPhysicalAddrZipcode.BackColor = System.Drawing.Color.Transparent;
            this.custPhysicalAddrZipcode.CausesValidation = false;
            this.custPhysicalAddrZipcode.City = null;
            this.custPhysicalAddrZipcode.Location = new System.Drawing.Point(115, 82);
            this.custPhysicalAddrZipcode.Margin = new System.Windows.Forms.Padding(0);
            this.custPhysicalAddrZipcode.Name = "custPhysicalAddrZipcode";
            this.custPhysicalAddrZipcode.Required = true;
            this.custPhysicalAddrZipcode.Size = new System.Drawing.Size(75, 20);
            this.custPhysicalAddrZipcode.State = null;
            this.custPhysicalAddrZipcode.TabIndex = 4;
            this.custPhysicalAddrZipcode.Leave += new System.EventHandler(this.custPhysicalAddrZipcode_Leave);
            // 
            // customLabelPhysicalAddrZip
            // 
            this.customLabelPhysicalAddrZip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelPhysicalAddrZip.AutoSize = true;
            this.customLabelPhysicalAddrZip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelPhysicalAddrZip.Location = new System.Drawing.Point(3, 85);
            this.customLabelPhysicalAddrZip.Name = "customLabelPhysicalAddrZip";
            this.customLabelPhysicalAddrZip.Required = true;
            this.customLabelPhysicalAddrZip.Size = new System.Drawing.Size(64, 13);
            this.customLabelPhysicalAddrZip.TabIndex = 124;
            this.customLabelPhysicalAddrZip.Text = "Postal Code";
            // 
            // customLabelPhysicalAddrCity
            // 
            this.customLabelPhysicalAddrCity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelPhysicalAddrCity.AutoSize = true;
            this.customLabelPhysicalAddrCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelPhysicalAddrCity.Location = new System.Drawing.Point(3, 111);
            this.customLabelPhysicalAddrCity.Name = "customLabelPhysicalAddrCity";
            this.customLabelPhysicalAddrCity.Required = true;
            this.customLabelPhysicalAddrCity.Size = new System.Drawing.Size(26, 13);
            this.customLabelPhysicalAddrCity.TabIndex = 123;
            this.customLabelPhysicalAddrCity.Text = "City";
            // 
            // customLabelWorkAddrZip
            // 
            this.customLabelWorkAddrZip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelWorkAddrZip.AutoSize = true;
            this.customLabelWorkAddrZip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelWorkAddrZip.Location = new System.Drawing.Point(3, 297);
            this.customLabelWorkAddrZip.Name = "customLabelWorkAddrZip";
            this.customLabelWorkAddrZip.Required = true;
            this.customLabelWorkAddrZip.Size = new System.Drawing.Size(64, 13);
            this.customLabelWorkAddrZip.TabIndex = 129;
            this.customLabelWorkAddrZip.Text = "Postal Code";
            // 
            // customLabelWorkAddrCity
            // 
            this.customLabelWorkAddrCity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelWorkAddrCity.AutoSize = true;
            this.customLabelWorkAddrCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelWorkAddrCity.Location = new System.Drawing.Point(3, 322);
            this.customLabelWorkAddrCity.Name = "customLabelWorkAddrCity";
            this.customLabelWorkAddrCity.Required = true;
            this.customLabelWorkAddrCity.Size = new System.Drawing.Size(26, 13);
            this.customLabelWorkAddrCity.TabIndex = 128;
            this.customLabelWorkAddrCity.Text = "City";
            // 
            // custWorkAddrCity
            // 
            this.custWorkAddrCity.CausesValidation = false;
            this.custWorkAddrCity.ErrorMessage = "";
            this.custWorkAddrCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custWorkAddrCity.Location = new System.Drawing.Point(118, 320);
            this.custWorkAddrCity.MaxLength = 50;
            this.custWorkAddrCity.Name = "custWorkAddrCity";
            this.custWorkAddrCity.Required = true;
            this.custWorkAddrCity.Size = new System.Drawing.Size(170, 21);
            this.custWorkAddrCity.TabIndex = 13;
            this.custWorkAddrCity.ValidationExpression = "";
            // 
            // custWorkAddrZipcode
            // 
            this.custWorkAddrZipcode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.custWorkAddrZipcode.BackColor = System.Drawing.Color.Transparent;
            this.custWorkAddrZipcode.CausesValidation = false;
            this.custWorkAddrZipcode.City = null;
            this.custWorkAddrZipcode.Location = new System.Drawing.Point(115, 294);
            this.custWorkAddrZipcode.Margin = new System.Windows.Forms.Padding(0);
            this.custWorkAddrZipcode.Name = "custWorkAddrZipcode";
            this.custWorkAddrZipcode.Required = true;
            this.custWorkAddrZipcode.Size = new System.Drawing.Size(77, 20);
            this.custWorkAddrZipcode.State = null;
            this.custWorkAddrZipcode.TabIndex = 12;
            this.custWorkAddrZipcode.Leave += new System.EventHandler(this.custWorkAddrZipcode_Leave);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.CausesValidation = false;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel2.Controls.Add(this.labelMailingAddrHeading, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelMailingAddrUnit, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelMailingAddrNotes, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.labelAddlAddressHeading, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.labelAddlAddress, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.labelAddlAddrUnit, 2, 9);
            this.tableLayoutPanel2.Controls.Add(this.labelAddlAddrState, 0, 13);
            this.tableLayoutPanel2.Controls.Add(this.labelAddlAddrCountry, 0, 14);
            this.tableLayoutPanel2.Controls.Add(this.labelAddlAddrNotes, 0, 15);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxMailingSameAsPhysical, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.custMailingAddr1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.custMailingAddrUnit, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.custMailingAddr2, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.custMailingAddrState, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.custMailingAddrNotes, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.custAddlAddr1, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.custAddlAddr2, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.custAddlAddrState, 1, 13);
            this.tableLayoutPanel2.Controls.Add(this.custAddlAddrNotes, 1, 15);
            this.tableLayoutPanel2.Controls.Add(this.custAddlAddrUnit, 3, 9);
            this.tableLayoutPanel2.Controls.Add(this.custAddlAddrAlternateString, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.custMailingAddrCountry, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.custAddlAddrCountry, 1, 14);
            this.tableLayoutPanel2.Controls.Add(this.customLabelMailingAddr, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.customLabelMailingAddrState, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.customLabelMailingAddrCountry, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.customLabelMailingAddrZip, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.customLabelMailingAddrCity, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.custMailingAddrCity, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.custMailingAddrZipcode, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelAddlAddrZip, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.labelAddlAddrCity, 0, 12);
            this.tableLayoutPanel2.Controls.Add(this.custAddlAddrCity, 1, 12);
            this.tableLayoutPanel2.Controls.Add(this.custAddlAddrZipcode, 1, 11);
            this.tableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(407, 60);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 16;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(422, 432);
            this.tableLayoutPanel2.TabIndex = 150;
            // 
            // labelMailingAddrHeading
            // 
            this.labelMailingAddrHeading.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMailingAddrHeading.AutoSize = true;
            this.labelMailingAddrHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMailingAddrHeading.Location = new System.Drawing.Point(3, 8);
            this.labelMailingAddrHeading.Name = "labelMailingAddrHeading";
            this.labelMailingAddrHeading.Size = new System.Drawing.Size(96, 13);
            this.labelMailingAddrHeading.TabIndex = 122;
            this.labelMailingAddrHeading.Text = "Mailing Address";
            // 
            // labelMailingAddrUnit
            // 
            this.labelMailingAddrUnit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelMailingAddrUnit.AutoSize = true;
            this.labelMailingAddrUnit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMailingAddrUnit.Location = new System.Drawing.Point(306, 35);
            this.labelMailingAddrUnit.Name = "labelMailingAddrUnit";
            this.labelMailingAddrUnit.Size = new System.Drawing.Size(26, 13);
            this.labelMailingAddrUnit.TabIndex = 146;
            this.labelMailingAddrUnit.Text = "Unit";
            // 
            // labelMailingAddrNotes
            // 
            this.labelMailingAddrNotes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMailingAddrNotes.AutoSize = true;
            this.labelMailingAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMailingAddrNotes.Location = new System.Drawing.Point(3, 190);
            this.labelMailingAddrNotes.Name = "labelMailingAddrNotes";
            this.labelMailingAddrNotes.Size = new System.Drawing.Size(35, 13);
            this.labelMailingAddrNotes.TabIndex = 145;
            this.labelMailingAddrNotes.Text = "Notes";
            // 
            // labelAddlAddressHeading
            // 
            this.labelAddlAddressHeading.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAddlAddressHeading.AutoSize = true;
            this.labelAddlAddressHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddlAddressHeading.Location = new System.Drawing.Point(3, 217);
            this.labelAddlAddressHeading.Name = "labelAddlAddressHeading";
            this.labelAddlAddressHeading.Size = new System.Drawing.Size(113, 13);
            this.labelAddlAddressHeading.TabIndex = 123;
            this.labelAddlAddressHeading.Text = "Additional Address";
            // 
            // labelAddlAddress
            // 
            this.labelAddlAddress.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAddlAddress.AutoSize = true;
            this.labelAddlAddress.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddlAddress.Location = new System.Drawing.Point(3, 244);
            this.labelAddlAddress.Name = "labelAddlAddress";
            this.labelAddlAddress.Size = new System.Drawing.Size(46, 13);
            this.labelAddlAddress.TabIndex = 90;
            this.labelAddlAddress.Text = "Address";
            // 
            // labelAddlAddrUnit
            // 
            this.labelAddlAddrUnit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelAddlAddrUnit.AutoSize = true;
            this.labelAddlAddrUnit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddlAddrUnit.Location = new System.Drawing.Point(306, 244);
            this.labelAddlAddrUnit.Name = "labelAddlAddrUnit";
            this.labelAddlAddrUnit.Size = new System.Drawing.Size(26, 13);
            this.labelAddlAddrUnit.TabIndex = 96;
            this.labelAddlAddrUnit.Text = "Unit";
            // 
            // labelAddlAddrState
            // 
            this.labelAddlAddrState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAddlAddrState.AutoSize = true;
            this.labelAddlAddrState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddlAddrState.Location = new System.Drawing.Point(3, 349);
            this.labelAddlAddrState.Name = "labelAddlAddrState";
            this.labelAddlAddrState.Size = new System.Drawing.Size(33, 13);
            this.labelAddlAddrState.TabIndex = 93;
            this.labelAddlAddrState.Text = "State";
            // 
            // labelAddlAddrCountry
            // 
            this.labelAddlAddrCountry.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAddlAddrCountry.AutoSize = true;
            this.labelAddlAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddlAddrCountry.Location = new System.Drawing.Point(3, 377);
            this.labelAddlAddrCountry.Name = "labelAddlAddrCountry";
            this.labelAddlAddrCountry.Size = new System.Drawing.Size(46, 13);
            this.labelAddlAddrCountry.TabIndex = 94;
            this.labelAddlAddrCountry.Text = "Country";
            // 
            // labelAddlAddrNotes
            // 
            this.labelAddlAddrNotes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAddlAddrNotes.AutoSize = true;
            this.labelAddlAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddlAddrNotes.Location = new System.Drawing.Point(3, 408);
            this.labelAddlAddrNotes.Name = "labelAddlAddrNotes";
            this.labelAddlAddrNotes.Size = new System.Drawing.Size(35, 13);
            this.labelAddlAddrNotes.TabIndex = 95;
            this.labelAddlAddrNotes.Text = "Notes";
            // 
            // checkBoxMailingSameAsPhysical
            // 
            this.checkBoxMailingSameAsPhysical.AutoSize = true;
            this.checkBoxMailingSameAsPhysical.CausesValidation = false;
            this.checkBoxMailingSameAsPhysical.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMailingSameAsPhysical.Location = new System.Drawing.Point(125, 3);
            this.checkBoxMailingSameAsPhysical.Name = "checkBoxMailingSameAsPhysical";
            this.checkBoxMailingSameAsPhysical.Size = new System.Drawing.Size(149, 17);
            this.checkBoxMailingSameAsPhysical.TabIndex = 17;
            this.checkBoxMailingSameAsPhysical.Text = "Same as Physical Address";
            this.checkBoxMailingSameAsPhysical.UseVisualStyleBackColor = true;
            this.checkBoxMailingSameAsPhysical.CheckedChanged += new System.EventHandler(this.checkBoxMailingSameAsPhysical_CheckedChanged);
            // 
            // custMailingAddr1
            // 
            this.custMailingAddr1.CausesValidation = false;
            this.custMailingAddr1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.custMailingAddr1.ErrorMessage = "";
            this.custMailingAddr1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custMailingAddr1.Location = new System.Drawing.Point(125, 33);
            this.custMailingAddr1.MaxLength = 40;
            this.custMailingAddr1.Name = "custMailingAddr1";
            this.custMailingAddr1.Required = true;
            this.custMailingAddr1.Size = new System.Drawing.Size(172, 21);
            this.custMailingAddr1.TabIndex = 18;
            this.custMailingAddr1.ValidationExpression = "";
            this.custMailingAddr1.TextChanged += new System.EventHandler(this.custMailingAddr1_TextChanged);
            this.custMailingAddr1.Leave += new System.EventHandler(this.custMailingAddr1_Leave);
            // 
            // custMailingAddrUnit
            // 
            this.custMailingAddrUnit.CausesValidation = false;
            this.custMailingAddrUnit.ErrorMessage = "";
            this.custMailingAddrUnit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custMailingAddrUnit.Location = new System.Drawing.Point(338, 33);
            this.custMailingAddrUnit.MaxLength = 5;
            this.custMailingAddrUnit.Name = "custMailingAddrUnit";
            this.custMailingAddrUnit.Size = new System.Drawing.Size(53, 21);
            this.custMailingAddrUnit.TabIndex = 20;
            this.custMailingAddrUnit.ValidationExpression = "";
            // 
            // custMailingAddr2
            // 
            this.custMailingAddr2.CausesValidation = false;
            this.custMailingAddr2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel2.SetColumnSpan(this.custMailingAddr2, 3);
            this.custMailingAddr2.ErrorMessage = "";
            this.custMailingAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custMailingAddr2.Location = new System.Drawing.Point(125, 56);
            this.custMailingAddr2.MaxLength = 39;
            this.custMailingAddr2.Name = "custMailingAddr2";
            this.custMailingAddr2.Size = new System.Drawing.Size(189, 21);
            this.custMailingAddr2.TabIndex = 19;
            this.custMailingAddr2.ValidationExpression = "";
            // 
            // custMailingAddrState
            // 
            this.custMailingAddrState.BackColor = System.Drawing.Color.Transparent;
            this.custMailingAddrState.CausesValidation = false;
            this.custMailingAddrState.DisplayCode = false;
            this.custMailingAddrState.ForeColor = System.Drawing.Color.Black;
            this.custMailingAddrState.Location = new System.Drawing.Point(125, 131);
            this.custMailingAddrState.Name = "custMailingAddrState";
            this.custMailingAddrState.Required = true;
            this.custMailingAddrState.Size = new System.Drawing.Size(50, 20);
            this.custMailingAddrState.TabIndex = 23;
            // 
            // custMailingAddrNotes
            // 
            this.custMailingAddrNotes.CausesValidation = false;
            this.tableLayoutPanel2.SetColumnSpan(this.custMailingAddrNotes, 3);
            this.custMailingAddrNotes.ErrorMessage = "";
            this.custMailingAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custMailingAddrNotes.Location = new System.Drawing.Point(125, 187);
            this.custMailingAddrNotes.MaxLength = 40;
            this.custMailingAddrNotes.Name = "custMailingAddrNotes";
            this.custMailingAddrNotes.Size = new System.Drawing.Size(212, 21);
            this.custMailingAddrNotes.TabIndex = 25;
            this.custMailingAddrNotes.ValidationExpression = "";
            // 
            // custAddlAddr1
            // 
            this.custAddlAddr1.CausesValidation = false;
            this.custAddlAddr1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.custAddlAddr1.Enabled = false;
            this.custAddlAddr1.ErrorMessage = "";
            this.custAddlAddr1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custAddlAddr1.Location = new System.Drawing.Point(125, 240);
            this.custAddlAddr1.MaxLength = 40;
            this.custAddlAddr1.Name = "custAddlAddr1";
            this.custAddlAddr1.Size = new System.Drawing.Size(172, 21);
            this.custAddlAddr1.TabIndex = 27;
            this.custAddlAddr1.ValidationExpression = "";
            this.custAddlAddr1.TextChanged += new System.EventHandler(this.custAddlAddr1_TextChanged);
            this.custAddlAddr1.Leave += new System.EventHandler(this.custAddlAddr1_Leave);
            // 
            // custAddlAddr2
            // 
            this.custAddlAddr2.CausesValidation = false;
            this.custAddlAddr2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel2.SetColumnSpan(this.custAddlAddr2, 3);
            this.custAddlAddr2.Enabled = false;
            this.custAddlAddr2.ErrorMessage = "";
            this.custAddlAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custAddlAddr2.Location = new System.Drawing.Point(125, 268);
            this.custAddlAddr2.MaxLength = 39;
            this.custAddlAddr2.Name = "custAddlAddr2";
            this.custAddlAddr2.Size = new System.Drawing.Size(189, 21);
            this.custAddlAddr2.TabIndex = 28;
            this.custAddlAddr2.ValidationExpression = "";
            // 
            // custAddlAddrState
            // 
            this.custAddlAddrState.BackColor = System.Drawing.Color.Transparent;
            this.custAddlAddrState.CausesValidation = false;
            this.custAddlAddrState.DisplayCode = false;
            this.custAddlAddrState.Enabled = false;
            this.custAddlAddrState.ForeColor = System.Drawing.Color.Black;
            this.custAddlAddrState.Location = new System.Drawing.Point(125, 345);
            this.custAddlAddrState.Name = "custAddlAddrState";
            this.custAddlAddrState.Size = new System.Drawing.Size(50, 21);
            this.custAddlAddrState.TabIndex = 32;
            // 
            // custAddlAddrNotes
            // 
            this.custAddlAddrNotes.CausesValidation = false;
            this.tableLayoutPanel2.SetColumnSpan(this.custAddlAddrNotes, 3);
            this.custAddlAddrNotes.Enabled = false;
            this.custAddlAddrNotes.ErrorMessage = "";
            this.custAddlAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custAddlAddrNotes.Location = new System.Drawing.Point(125, 401);
            this.custAddlAddrNotes.MaxLength = 40;
            this.custAddlAddrNotes.Name = "custAddlAddrNotes";
            this.custAddlAddrNotes.Size = new System.Drawing.Size(212, 21);
            this.custAddlAddrNotes.TabIndex = 34;
            this.custAddlAddrNotes.ValidationExpression = "";
            // 
            // custAddlAddrUnit
            // 
            this.custAddlAddrUnit.CausesValidation = false;
            this.custAddlAddrUnit.Enabled = false;
            this.custAddlAddrUnit.ErrorMessage = "";
            this.custAddlAddrUnit.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custAddlAddrUnit.Location = new System.Drawing.Point(338, 240);
            this.custAddlAddrUnit.MaxLength = 5;
            this.custAddlAddrUnit.Name = "custAddlAddrUnit";
            this.custAddlAddrUnit.Size = new System.Drawing.Size(53, 21);
            this.custAddlAddrUnit.TabIndex = 29;
            this.custAddlAddrUnit.ValidationExpression = "";
            // 
            // custAddlAddrAlternateString
            // 
            this.custAddlAddrAlternateString.CausesValidation = false;
            this.custAddlAddrAlternateString.Enabled = false;
            this.custAddlAddrAlternateString.ErrorMessage = "";
            this.custAddlAddrAlternateString.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custAddlAddrAlternateString.Location = new System.Drawing.Point(125, 213);
            this.custAddlAddrAlternateString.MaxLength = 100;
            this.custAddlAddrAlternateString.Name = "custAddlAddrAlternateString";
            this.custAddlAddrAlternateString.Size = new System.Drawing.Size(172, 21);
            this.custAddlAddrAlternateString.TabIndex = 26;
            this.custAddlAddrAlternateString.ValidationExpression = "";
            // 
            // custMailingAddrCountry
            // 
            this.custMailingAddrCountry.CausesValidation = false;
            this.custMailingAddrCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.custMailingAddrCountry.FormattingEnabled = true;
            this.custMailingAddrCountry.Items.AddRange(new object[] {
            "United States",
            "Canada",
            "Mexico"});
            this.custMailingAddrCountry.Location = new System.Drawing.Point(125, 158);
            this.custMailingAddrCountry.Name = "custMailingAddrCountry";
            this.custMailingAddrCountry.Size = new System.Drawing.Size(172, 21);
            this.custMailingAddrCountry.TabIndex = 24;
            // 
            // custAddlAddrCountry
            // 
            this.custAddlAddrCountry.CausesValidation = false;
            this.custAddlAddrCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.custAddlAddrCountry.Enabled = false;
            this.custAddlAddrCountry.FormattingEnabled = true;
            this.custAddlAddrCountry.Items.AddRange(new object[] {
            "United States",
            "Canada",
            "Mexico"});
            this.custAddlAddrCountry.Location = new System.Drawing.Point(125, 373);
            this.custAddlAddrCountry.Name = "custAddlAddrCountry";
            this.custAddlAddrCountry.Size = new System.Drawing.Size(121, 21);
            this.custAddlAddrCountry.TabIndex = 33;
            // 
            // customLabelMailingAddr
            // 
            this.customLabelMailingAddr.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelMailingAddr.AutoSize = true;
            this.customLabelMailingAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelMailingAddr.Location = new System.Drawing.Point(3, 35);
            this.customLabelMailingAddr.Name = "customLabelMailingAddr";
            this.customLabelMailingAddr.Required = true;
            this.customLabelMailingAddr.Size = new System.Drawing.Size(46, 13);
            this.customLabelMailingAddr.TabIndex = 147;
            this.customLabelMailingAddr.Text = "Address";
            // 
            // customLabelMailingAddrState
            // 
            this.customLabelMailingAddrState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelMailingAddrState.AutoSize = true;
            this.customLabelMailingAddrState.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelMailingAddrState.Location = new System.Drawing.Point(3, 135);
            this.customLabelMailingAddrState.Name = "customLabelMailingAddrState";
            this.customLabelMailingAddrState.Required = true;
            this.customLabelMailingAddrState.Size = new System.Drawing.Size(33, 13);
            this.customLabelMailingAddrState.TabIndex = 150;
            this.customLabelMailingAddrState.Text = "State";
            // 
            // customLabelMailingAddrCountry
            // 
            this.customLabelMailingAddrCountry.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelMailingAddrCountry.AutoSize = true;
            this.customLabelMailingAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelMailingAddrCountry.Location = new System.Drawing.Point(3, 163);
            this.customLabelMailingAddrCountry.Name = "customLabelMailingAddrCountry";
            this.customLabelMailingAddrCountry.Required = true;
            this.customLabelMailingAddrCountry.Size = new System.Drawing.Size(46, 13);
            this.customLabelMailingAddrCountry.TabIndex = 151;
            this.customLabelMailingAddrCountry.Text = "Country";
            // 
            // customLabelMailingAddrZip
            // 
            this.customLabelMailingAddrZip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelMailingAddrZip.AutoSize = true;
            this.customLabelMailingAddrZip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelMailingAddrZip.Location = new System.Drawing.Point(3, 82);
            this.customLabelMailingAddrZip.Name = "customLabelMailingAddrZip";
            this.customLabelMailingAddrZip.Required = true;
            this.customLabelMailingAddrZip.Size = new System.Drawing.Size(64, 13);
            this.customLabelMailingAddrZip.TabIndex = 149;
            this.customLabelMailingAddrZip.Text = "Postal Code";
            // 
            // customLabelMailingAddrCity
            // 
            this.customLabelMailingAddrCity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.customLabelMailingAddrCity.AutoSize = true;
            this.customLabelMailingAddrCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelMailingAddrCity.Location = new System.Drawing.Point(3, 108);
            this.customLabelMailingAddrCity.Name = "customLabelMailingAddrCity";
            this.customLabelMailingAddrCity.Required = true;
            this.customLabelMailingAddrCity.Size = new System.Drawing.Size(26, 13);
            this.customLabelMailingAddrCity.TabIndex = 148;
            this.customLabelMailingAddrCity.Text = "City";
            // 
            // custMailingAddrCity
            // 
            this.custMailingAddrCity.CausesValidation = false;
            this.tableLayoutPanel2.SetColumnSpan(this.custMailingAddrCity, 3);
            this.custMailingAddrCity.ErrorMessage = "";
            this.custMailingAddrCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custMailingAddrCity.Location = new System.Drawing.Point(125, 104);
            this.custMailingAddrCity.MaxLength = 50;
            this.custMailingAddrCity.Name = "custMailingAddrCity";
            this.custMailingAddrCity.Required = true;
            this.custMailingAddrCity.Size = new System.Drawing.Size(189, 21);
            this.custMailingAddrCity.TabIndex = 22;
            this.custMailingAddrCity.ValidationExpression = "";
            // 
            // custMailingAddrZipcode
            // 
            this.custMailingAddrZipcode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.custMailingAddrZipcode.BackColor = System.Drawing.Color.Transparent;
            this.custMailingAddrZipcode.CausesValidation = false;
            this.custMailingAddrZipcode.City = null;
            this.custMailingAddrZipcode.Location = new System.Drawing.Point(122, 78);
            this.custMailingAddrZipcode.Margin = new System.Windows.Forms.Padding(0);
            this.custMailingAddrZipcode.Name = "custMailingAddrZipcode";
            this.custMailingAddrZipcode.Required = true;
            this.custMailingAddrZipcode.Size = new System.Drawing.Size(77, 20);
            this.custMailingAddrZipcode.State = null;
            this.custMailingAddrZipcode.TabIndex = 21;
            this.custMailingAddrZipcode.Leave += new System.EventHandler(this.custMailingAddrZipcode_Leave);
            // 
            // labelAddlAddrZip
            // 
            this.labelAddlAddrZip.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAddlAddrZip.AutoSize = true;
            this.labelAddlAddrZip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddlAddrZip.Location = new System.Drawing.Point(3, 297);
            this.labelAddlAddrZip.Name = "labelAddlAddrZip";
            this.labelAddlAddrZip.Size = new System.Drawing.Size(64, 13);
            this.labelAddlAddrZip.TabIndex = 92;
            this.labelAddlAddrZip.Text = "Postal Code";
            // 
            // labelAddlAddrCity
            // 
            this.labelAddlAddrCity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelAddlAddrCity.AutoSize = true;
            this.labelAddlAddrCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddlAddrCity.Location = new System.Drawing.Point(3, 323);
            this.labelAddlAddrCity.Name = "labelAddlAddrCity";
            this.labelAddlAddrCity.Size = new System.Drawing.Size(26, 13);
            this.labelAddlAddrCity.TabIndex = 91;
            this.labelAddlAddrCity.Text = "City";
            // 
            // custAddlAddrCity
            // 
            this.custAddlAddrCity.CausesValidation = false;
            this.custAddlAddrCity.Enabled = false;
            this.custAddlAddrCity.ErrorMessage = "";
            this.custAddlAddrCity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custAddlAddrCity.Location = new System.Drawing.Point(125, 321);
            this.custAddlAddrCity.MaxLength = 50;
            this.custAddlAddrCity.Name = "custAddlAddrCity";
            this.custAddlAddrCity.Size = new System.Drawing.Size(172, 21);
            this.custAddlAddrCity.TabIndex = 31;
            this.custAddlAddrCity.ValidationExpression = "";
            // 
            // custAddlAddrZipcode
            // 
            this.custAddlAddrZipcode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.custAddlAddrZipcode.BackColor = System.Drawing.Color.Transparent;
            this.custAddlAddrZipcode.CausesValidation = false;
            this.custAddlAddrZipcode.City = null;
            this.custAddlAddrZipcode.Enabled = false;
            this.custAddlAddrZipcode.Location = new System.Drawing.Point(122, 294);
            this.custAddlAddrZipcode.Margin = new System.Windows.Forms.Padding(0);
            this.custAddlAddrZipcode.Name = "custAddlAddrZipcode";
            this.custAddlAddrZipcode.Size = new System.Drawing.Size(77, 20);
            this.custAddlAddrZipcode.State = null;
            this.custAddlAddrZipcode.TabIndex = 30;
            this.custAddlAddrZipcode.Leave += new System.EventHandler(this.custAddlAddrZipcode_Leave);
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
            this.customButtonContinue.Location = new System.Drawing.Point(684, 498);
            this.customButtonContinue.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonContinue.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonContinue.Name = "customButtonContinue";
            this.customButtonContinue.Size = new System.Drawing.Size(100, 50);
            this.customButtonContinue.TabIndex = 155;
            this.customButtonContinue.Text = "Co&ntinue";
            this.customButtonContinue.UseVisualStyleBackColor = false;
            this.customButtonContinue.Visible = false;
            this.customButtonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
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
            this.customButtonSubmit.Location = new System.Drawing.Point(568, 498);
            this.customButtonSubmit.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonSubmit.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.Name = "customButtonSubmit";
            this.customButtonSubmit.Size = new System.Drawing.Size(100, 50);
            this.customButtonSubmit.TabIndex = 154;
            this.customButtonSubmit.Text = "&Submit";
            this.customButtonSubmit.UseVisualStyleBackColor = false;
            this.customButtonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
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
            this.customButtonReset.Location = new System.Drawing.Point(442, 498);
            this.customButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonReset.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonReset.Name = "customButtonReset";
            this.customButtonReset.Size = new System.Drawing.Size(100, 50);
            this.customButtonReset.TabIndex = 153;
            this.customButtonReset.Text = "&Reset";
            this.customButtonReset.UseVisualStyleBackColor = false;
            this.customButtonReset.Click += new System.EventHandler(this.buttonReset_Click);
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
            this.customButtonBack.Location = new System.Drawing.Point(110, 498);
            this.customButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonBack.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonBack.Name = "customButtonBack";
            this.customButtonBack.Size = new System.Drawing.Size(100, 50);
            this.customButtonBack.TabIndex = 152;
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
            this.customButtonCancel.Location = new System.Drawing.Point(10, 498);
            this.customButtonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCancel.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCancel.Name = "customButtonCancel";
            this.customButtonCancel.Size = new System.Drawing.Size(100, 50);
            this.customButtonCancel.TabIndex = 151;
            this.customButtonCancel.Text = "&Cancel";
            this.customButtonCancel.UseVisualStyleBackColor = false;
            this.customButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // UpdateAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(864, 585);
            this.ControlBox = false;
            this.Controls.Add(this.customButtonContinue);
            this.Controls.Add(this.customButtonSubmit);
            this.Controls.Add(this.customButtonReset);
            this.Controls.Add(this.customButtonBack);
            this.Controls.Add(this.customButtonCancel);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.addressFormLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateAddress";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateAddress";
            this.Load += new System.EventHandler(this.UpdateAddress_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label addressFormLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelPhysicalAddrHeading;
        private System.Windows.Forms.Label labelPhysicalAddrUnit;
        private System.Windows.Forms.Label labelPhysicalAddrNotes;
        private System.Windows.Forms.Label labelWorkAddHeading;
        private System.Windows.Forms.Label labelWorkAddrUnit;
        private System.Windows.Forms.Label labelWorkAddrNotes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labelMailingAddrHeading;
        private System.Windows.Forms.Label labelMailingAddrUnit;
        private System.Windows.Forms.Label labelMailingAddrNotes;
        private System.Windows.Forms.Label labelAddlAddressHeading;
        private System.Windows.Forms.Label labelAddlAddress;
        private System.Windows.Forms.Label labelAddlAddrUnit;
        private System.Windows.Forms.Label labelAddlAddrCity;
        private System.Windows.Forms.Label labelAddlAddrZip;
        private System.Windows.Forms.Label labelAddlAddrState;
        private System.Windows.Forms.Label labelAddlAddrCountry;
        private System.Windows.Forms.Label labelAddlAddrNotes;
        private System.Windows.Forms.CheckBox checkBoxMailingSameAsPhysical;
        private CustomTextBox custPhysicalAddr1;
        private CustomTextBox custPhysicalAddrUnit;
        private CustomTextBox custPhysicalAddr2;
        private CustomTextBox custPhysicalAddrCity;
        private State custPhysicalAddrState;
        private CustomTextBox custPhysicalAddrNotes;
        private CustomTextBox custWorkAddr1;
        private CustomTextBox custWorkAddrUnit;
        private CustomTextBox custWorkAddrCity;
        private Zipcode custWorkAddrZipcode;
        private State custWorkAddrState;
        private CustomTextBox custWorkAddrNotes;
        private CustomTextBox custMailingAddr1;
        private CustomTextBox custMailingAddrUnit;
        private CustomTextBox custMailingAddr2;
        private CustomTextBox custMailingAddrCity;
        private Zipcode custMailingAddrZipcode;
        private State custMailingAddrState;
        private CustomTextBox custMailingAddrNotes;
        private CustomTextBox custAddlAddr1;
        private CustomTextBox custAddlAddrNotes;
        private CustomTextBox custAddlAddr2;
        private Zipcode custAddlAddrZipcode;
        private State custAddlAddrState;
        private CustomTextBox custAddlAddrCity;
        private CustomTextBox custWorkAddr2;
        private CustomTextBox custAddlAddrUnit;
        private CustomTextBox custAddlAddrAlternateString;
        private System.Windows.Forms.ComboBox custPhysicalAddrCountry;
        private System.Windows.Forms.ComboBox custWorkAddrCountry;
        private System.Windows.Forms.ComboBox custMailingAddrCountry;
        private System.Windows.Forms.ComboBox custAddlAddrCountry;
        private CustomLabel customLabelPhysicalAddr;
        private CustomLabel customLabelPhysicalAddrCity;
        private CustomLabel customLabelPhysicalAddrZip;
        private CustomLabel customLabelPhysAddrState;
        private CustomLabel customLabelPhysAddrCountry;
        private CustomLabel customLabelWorkAddr;
        private CustomLabel customLabelWorkAddrCity;
        private CustomLabel customLabelWorkAddrZip;
        private CustomLabel customLabelWorkAddrState;
        private CustomLabel customLabelWorkAddrCountry;
        private CustomLabel customLabelMailingAddr;
        private CustomLabel customLabelMailingAddrCity;
        private CustomLabel customLabelMailingAddrZip;
        private CustomLabel customLabelMailingAddrState;
        private CustomLabel customLabelMailingAddrCountry;
        private Zipcode custPhysicalAddrZipcode;
        private CustomButton customButtonCancel;
        private CustomButton customButtonBack;
        private CustomButton customButtonReset;
        private CustomButton customButtonSubmit;
        private CustomButton customButtonContinue;
    }
}