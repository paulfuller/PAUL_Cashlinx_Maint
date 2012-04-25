using Common.Libraries.Forms.Components;

namespace Pawn.Forms.UserControls
{
    partial class CurrencyEntry
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrencyEntry));
            this.labelHeader = new System.Windows.Forms.Label();
            this.labelCoinCountHeading = new System.Windows.Forms.Label();
            this.labelCoinAmtHeading = new System.Windows.Forms.Label();
            this.labelDollarCountHeading = new System.Windows.Forms.Label();
            this.labelDollarAmtHeading = new System.Windows.Forms.Label();
            this.labelCoinTotalHeading = new System.Windows.Forms.Label();
            this.labelCurrencyTotalHeading = new System.Windows.Forms.Label();
            this.labelcoin1 = new System.Windows.Forms.Label();
            this.labelcoin5 = new System.Windows.Forms.Label();
            this.labelCoin10 = new System.Windows.Forms.Label();
            this.labelCoin25 = new System.Windows.Forms.Label();
            this.labelCoin50 = new System.Windows.Forms.Label();
            this.labelDollar = new System.Windows.Forms.Label();
            this.customTextBoxCurrencyTotal = new CustomTextBox();
            this.customTextBoxCoinTotal = new CustomTextBox();
            this.customTextBoxDollar100Amt = new CustomTextBox();
            this.customTextBoxDollar50Amt = new CustomTextBox();
            this.customTextBoxDollar20Amt = new CustomTextBox();
            this.customTextBoxDollar10Amt = new CustomTextBox();
            this.customTextBoxDollar5Amt = new CustomTextBox();
            this.customTextBoxDollar2Amt = new CustomTextBox();
            this.customTextBoxDollar1Amt = new CustomTextBox();
            this.customTextBoxDollar100Cnt = new CustomTextBox();
            this.customTextBoxDollar50Cnt = new CustomTextBox();
            this.customTextBoxDollar20Cnt = new CustomTextBox();
            this.customTextBoxDollar10Cnt = new CustomTextBox();
            this.customTextBoxDollar5Cnt = new CustomTextBox();
            this.customTextBoxDollar2Cnt = new CustomTextBox();
            this.customTextBoxDollar1Cnt = new CustomTextBox();
            this.customLabelDollar100 = new CustomLabel();
            this.customTextBoxCent100Amt = new CustomTextBox();
            this.customTextBoxCent50Amt = new CustomTextBox();
            this.customTextBoxCent25Amt = new CustomTextBox();
            this.customTextBoxCent10Amt = new CustomTextBox();
            this.customTextBoxCent5Amt = new CustomTextBox();
            this.customTextBoxCent1Amt = new CustomTextBox();
            this.customTextBoxCent100Cnt = new CustomTextBox();
            this.customTextBoxCent50Cnt = new CustomTextBox();
            this.customTextBoxCent25Cnt = new CustomTextBox();
            this.customTextBoxCent10Cnt = new CustomTextBox();
            this.customTextBoxCent5Cnt = new CustomTextBox();
            this.customTextBoxCent1Cnt = new CustomTextBox();
            this.customButtonCalculate = new CustomButton();
            this.labelDollar1 = new System.Windows.Forms.Label();
            this.labelDollar2 = new System.Windows.Forms.Label();
            this.labelDollar5 = new System.Windows.Forms.Label();
            this.labelDollar10 = new System.Windows.Forms.Label();
            this.labelDollar20 = new System.Windows.Forms.Label();
            this.labelDollar50 = new System.Windows.Forms.Label();
            this.customButtonOtherTender = new CustomButton();
            this.customTextBoxOTCount = new CustomTextBox();
            this.customTextBoxOTAmount = new CustomTextBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(9, 11);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(69, 16);
            this.labelHeader.TabIndex = 144;
            this.labelHeader.Text = "Currency";
            // 
            // labelCoinCountHeading
            // 
            this.labelCoinCountHeading.AutoSize = true;
            this.labelCoinCountHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCoinCountHeading.Location = new System.Drawing.Point(62, 36);
            this.labelCoinCountHeading.Name = "labelCoinCountHeading";
            this.labelCoinCountHeading.Size = new System.Drawing.Size(47, 16);
            this.labelCoinCountHeading.TabIndex = 145;
            this.labelCoinCountHeading.Text = "Count";
            // 
            // labelCoinAmtHeading
            // 
            this.labelCoinAmtHeading.AutoSize = true;
            this.labelCoinAmtHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCoinAmtHeading.Location = new System.Drawing.Point(140, 36);
            this.labelCoinAmtHeading.Name = "labelCoinAmtHeading";
            this.labelCoinAmtHeading.Size = new System.Drawing.Size(59, 16);
            this.labelCoinAmtHeading.TabIndex = 146;
            this.labelCoinAmtHeading.Text = "Amount";
            // 
            // labelDollarCountHeading
            // 
            this.labelDollarCountHeading.AutoSize = true;
            this.labelDollarCountHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDollarCountHeading.Location = new System.Drawing.Point(403, 36);
            this.labelDollarCountHeading.Name = "labelDollarCountHeading";
            this.labelDollarCountHeading.Size = new System.Drawing.Size(47, 16);
            this.labelDollarCountHeading.TabIndex = 147;
            this.labelDollarCountHeading.Text = "Count";
            // 
            // labelDollarAmtHeading
            // 
            this.labelDollarAmtHeading.AutoSize = true;
            this.labelDollarAmtHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDollarAmtHeading.Location = new System.Drawing.Point(490, 36);
            this.labelDollarAmtHeading.Name = "labelDollarAmtHeading";
            this.labelDollarAmtHeading.Size = new System.Drawing.Size(59, 16);
            this.labelDollarAmtHeading.TabIndex = 148;
            this.labelDollarAmtHeading.Text = "Amount";
            // 
            // labelCoinTotalHeading
            // 
            this.labelCoinTotalHeading.AutoSize = true;
            this.labelCoinTotalHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCoinTotalHeading.Location = new System.Drawing.Point(26, 231);
            this.labelCoinTotalHeading.Name = "labelCoinTotalHeading";
            this.labelCoinTotalHeading.Size = new System.Drawing.Size(83, 16);
            this.labelCoinTotalHeading.TabIndex = 149;
            this.labelCoinTotalHeading.Text = "Coin Total:";
            // 
            // labelCurrencyTotalHeading
            // 
            this.labelCurrencyTotalHeading.AutoSize = true;
            this.labelCurrencyTotalHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrencyTotalHeading.Location = new System.Drawing.Point(337, 231);
            this.labelCurrencyTotalHeading.Name = "labelCurrencyTotalHeading";
            this.labelCurrencyTotalHeading.Size = new System.Drawing.Size(82, 16);
            this.labelCurrencyTotalHeading.TabIndex = 150;
            this.labelCurrencyTotalHeading.Text = "Bills Total:";
            // 
            // labelcoin1
            // 
            this.labelcoin1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelcoin1.AutoSize = true;
            this.labelcoin1.Location = new System.Drawing.Point(5, 6);
            this.labelcoin1.Name = "labelcoin1";
            this.labelcoin1.Size = new System.Drawing.Size(22, 13);
            this.labelcoin1.TabIndex = 161;
            this.labelcoin1.Text = ".01";
            // 
            // labelcoin5
            // 
            this.labelcoin5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelcoin5.AutoSize = true;
            this.labelcoin5.Location = new System.Drawing.Point(5, 32);
            this.labelcoin5.Name = "labelcoin5";
            this.labelcoin5.Size = new System.Drawing.Size(22, 13);
            this.labelcoin5.TabIndex = 162;
            this.labelcoin5.Text = ".05";
            // 
            // labelCoin10
            // 
            this.labelCoin10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCoin10.AutoSize = true;
            this.labelCoin10.Location = new System.Drawing.Point(5, 55);
            this.labelCoin10.Name = "labelCoin10";
            this.labelCoin10.Size = new System.Drawing.Size(22, 13);
            this.labelCoin10.TabIndex = 163;
            this.labelCoin10.Text = ".10";
            // 
            // labelCoin25
            // 
            this.labelCoin25.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCoin25.AutoSize = true;
            this.labelCoin25.Location = new System.Drawing.Point(5, 75);
            this.labelCoin25.Name = "labelCoin25";
            this.labelCoin25.Size = new System.Drawing.Size(22, 13);
            this.labelCoin25.TabIndex = 164;
            this.labelCoin25.Text = ".25";
            // 
            // labelCoin50
            // 
            this.labelCoin50.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelCoin50.AutoSize = true;
            this.labelCoin50.Location = new System.Drawing.Point(5, 95);
            this.labelCoin50.Name = "labelCoin50";
            this.labelCoin50.Size = new System.Drawing.Size(22, 13);
            this.labelCoin50.TabIndex = 165;
            this.labelCoin50.Text = ".50";
            // 
            // labelDollar
            // 
            this.labelDollar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDollar.AutoSize = true;
            this.labelDollar.Location = new System.Drawing.Point(8, 116);
            this.labelDollar.Name = "labelDollar";
            this.labelDollar.Size = new System.Drawing.Size(19, 13);
            this.labelDollar.TabIndex = 166;
            this.labelDollar.Text = "$1";
            // 
            // customTextBoxCurrencyTotal
            // 
            this.customTextBoxCurrencyTotal.CausesValidation = false;
            this.customTextBoxCurrencyTotal.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCurrencyTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCurrencyTotal.Location = new System.Drawing.Point(493, 230);
            this.customTextBoxCurrencyTotal.Name = "customTextBoxCurrencyTotal";
            this.customTextBoxCurrencyTotal.ReadOnly = true;
            this.customTextBoxCurrencyTotal.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCurrencyTotal.TabIndex = 201;
            this.customTextBoxCurrencyTotal.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxCoinTotal
            // 
            this.customTextBoxCoinTotal.CausesValidation = false;
            this.customTextBoxCoinTotal.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCoinTotal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCoinTotal.Location = new System.Drawing.Point(143, 230);
            this.customTextBoxCoinTotal.Name = "customTextBoxCoinTotal";
            this.customTextBoxCoinTotal.ReadOnly = true;
            this.customTextBoxCoinTotal.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCoinTotal.TabIndex = 200;
            this.customTextBoxCoinTotal.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxDollar100Amt
            // 
            this.customTextBoxDollar100Amt.AllowOnlyNumbers = true;
            this.customTextBoxDollar100Amt.CausesValidation = false;
            this.customTextBoxDollar100Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar100Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar100Amt.Location = new System.Drawing.Point(493, 190);
            this.customTextBoxDollar100Amt.Name = "customTextBoxDollar100Amt";
            this.customTextBoxDollar100Amt.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxDollar100Amt.TabIndex = 199;
            this.customTextBoxDollar100Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar100Amt.Leave += new System.EventHandler(this.customTextBoxDollar100Amt_Leave);
            // 
            // customTextBoxDollar50Amt
            // 
            this.customTextBoxDollar50Amt.AllowOnlyNumbers = true;
            this.customTextBoxDollar50Amt.CausesValidation = false;
            this.customTextBoxDollar50Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar50Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar50Amt.Location = new System.Drawing.Point(493, 168);
            this.customTextBoxDollar50Amt.Name = "customTextBoxDollar50Amt";
            this.customTextBoxDollar50Amt.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxDollar50Amt.TabIndex = 198;
            this.customTextBoxDollar50Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar50Amt.Leave += new System.EventHandler(this.customTextBoxDollar50Amt_Leave);
            // 
            // customTextBoxDollar20Amt
            // 
            this.customTextBoxDollar20Amt.AllowOnlyNumbers = true;
            this.customTextBoxDollar20Amt.CausesValidation = false;
            this.customTextBoxDollar20Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar20Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar20Amt.Location = new System.Drawing.Point(493, 149);
            this.customTextBoxDollar20Amt.Name = "customTextBoxDollar20Amt";
            this.customTextBoxDollar20Amt.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxDollar20Amt.TabIndex = 197;
            this.customTextBoxDollar20Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar20Amt.Leave += new System.EventHandler(this.customTextBoxDollar20Amt_Leave);
            // 
            // customTextBoxDollar10Amt
            // 
            this.customTextBoxDollar10Amt.AllowOnlyNumbers = true;
            this.customTextBoxDollar10Amt.CausesValidation = false;
            this.customTextBoxDollar10Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar10Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar10Amt.Location = new System.Drawing.Point(493, 128);
            this.customTextBoxDollar10Amt.Name = "customTextBoxDollar10Amt";
            this.customTextBoxDollar10Amt.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxDollar10Amt.TabIndex = 196;
            this.customTextBoxDollar10Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar10Amt.Leave += new System.EventHandler(this.customTextBoxDollar10Amt_Leave);
            // 
            // customTextBoxDollar5Amt
            // 
            this.customTextBoxDollar5Amt.AllowOnlyNumbers = true;
            this.customTextBoxDollar5Amt.CausesValidation = false;
            this.customTextBoxDollar5Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar5Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar5Amt.Location = new System.Drawing.Point(493, 108);
            this.customTextBoxDollar5Amt.Name = "customTextBoxDollar5Amt";
            this.customTextBoxDollar5Amt.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxDollar5Amt.TabIndex = 195;
            this.customTextBoxDollar5Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar5Amt.Leave += new System.EventHandler(this.customTextBoxDollar5Amt_Leave);
            // 
            // customTextBoxDollar2Amt
            // 
            this.customTextBoxDollar2Amt.AllowOnlyNumbers = true;
            this.customTextBoxDollar2Amt.CausesValidation = false;
            this.customTextBoxDollar2Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar2Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar2Amt.Location = new System.Drawing.Point(493, 85);
            this.customTextBoxDollar2Amt.Name = "customTextBoxDollar2Amt";
            this.customTextBoxDollar2Amt.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxDollar2Amt.TabIndex = 194;
            this.customTextBoxDollar2Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar2Amt.Leave += new System.EventHandler(this.customTextBoxDollar2Amt_Leave);
            // 
            // customTextBoxDollar1Amt
            // 
            this.customTextBoxDollar1Amt.AllowOnlyNumbers = true;
            this.customTextBoxDollar1Amt.CausesValidation = false;
            this.customTextBoxDollar1Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar1Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar1Amt.Location = new System.Drawing.Point(493, 61);
            this.customTextBoxDollar1Amt.Name = "customTextBoxDollar1Amt";
            this.customTextBoxDollar1Amt.Size = new System.Drawing.Size(87, 21);
            this.customTextBoxDollar1Amt.TabIndex = 193;
            this.customTextBoxDollar1Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar1Amt.Leave += new System.EventHandler(this.customTextBoxDollar1Amt_Leave);
            // 
            // customTextBoxDollar100Cnt
            // 
            this.customTextBoxDollar100Cnt.AllowOnlyNumbers = true;
            this.customTextBoxDollar100Cnt.CausesValidation = false;
            this.customTextBoxDollar100Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar100Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar100Cnt.Location = new System.Drawing.Point(406, 190);
            this.customTextBoxDollar100Cnt.Name = "customTextBoxDollar100Cnt";
            this.customTextBoxDollar100Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxDollar100Cnt.TabIndex = 192;
            this.customTextBoxDollar100Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar100Cnt.Leave += new System.EventHandler(this.customTextBoxDollar100Cnt_Leave);
            // 
            // customTextBoxDollar50Cnt
            // 
            this.customTextBoxDollar50Cnt.AllowOnlyNumbers = true;
            this.customTextBoxDollar50Cnt.CausesValidation = false;
            this.customTextBoxDollar50Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar50Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar50Cnt.Location = new System.Drawing.Point(406, 167);
            this.customTextBoxDollar50Cnt.Name = "customTextBoxDollar50Cnt";
            this.customTextBoxDollar50Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxDollar50Cnt.TabIndex = 191;
            this.customTextBoxDollar50Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar50Cnt.Leave += new System.EventHandler(this.customTextBoxDollar50Cnt_Leave);
            // 
            // customTextBoxDollar20Cnt
            // 
            this.customTextBoxDollar20Cnt.AllowOnlyNumbers = true;
            this.customTextBoxDollar20Cnt.CausesValidation = false;
            this.customTextBoxDollar20Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar20Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar20Cnt.Location = new System.Drawing.Point(406, 147);
            this.customTextBoxDollar20Cnt.Name = "customTextBoxDollar20Cnt";
            this.customTextBoxDollar20Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxDollar20Cnt.TabIndex = 190;
            this.customTextBoxDollar20Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar20Cnt.Leave += new System.EventHandler(this.customTextBoxDollar20Cnt_Leave);
            // 
            // customTextBoxDollar10Cnt
            // 
            this.customTextBoxDollar10Cnt.AllowOnlyNumbers = true;
            this.customTextBoxDollar10Cnt.CausesValidation = false;
            this.customTextBoxDollar10Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar10Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar10Cnt.Location = new System.Drawing.Point(406, 127);
            this.customTextBoxDollar10Cnt.Name = "customTextBoxDollar10Cnt";
            this.customTextBoxDollar10Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxDollar10Cnt.TabIndex = 189;
            this.customTextBoxDollar10Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar10Cnt.Leave += new System.EventHandler(this.customTextBoxDollar10Cnt_Leave);
            // 
            // customTextBoxDollar5Cnt
            // 
            this.customTextBoxDollar5Cnt.AllowOnlyNumbers = true;
            this.customTextBoxDollar5Cnt.CausesValidation = false;
            this.customTextBoxDollar5Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar5Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar5Cnt.Location = new System.Drawing.Point(406, 106);
            this.customTextBoxDollar5Cnt.Name = "customTextBoxDollar5Cnt";
            this.customTextBoxDollar5Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxDollar5Cnt.TabIndex = 188;
            this.customTextBoxDollar5Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar5Cnt.Leave += new System.EventHandler(this.customTextBoxDollar5Cnt_Leave);
            // 
            // customTextBoxDollar2Cnt
            // 
            this.customTextBoxDollar2Cnt.AllowOnlyNumbers = true;
            this.customTextBoxDollar2Cnt.CausesValidation = false;
            this.customTextBoxDollar2Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar2Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar2Cnt.Location = new System.Drawing.Point(406, 86);
            this.customTextBoxDollar2Cnt.Name = "customTextBoxDollar2Cnt";
            this.customTextBoxDollar2Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxDollar2Cnt.TabIndex = 187;
            this.customTextBoxDollar2Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar2Cnt.Leave += new System.EventHandler(this.customTextBoxDollar2Cnt_Leave);
            // 
            // customTextBoxDollar1Cnt
            // 
            this.customTextBoxDollar1Cnt.AllowOnlyNumbers = true;
            this.customTextBoxDollar1Cnt.CausesValidation = false;
            this.customTextBoxDollar1Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar1Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxDollar1Cnt.Location = new System.Drawing.Point(406, 62);
            this.customTextBoxDollar1Cnt.Name = "customTextBoxDollar1Cnt";
            this.customTextBoxDollar1Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxDollar1Cnt.TabIndex = 186;
            this.customTextBoxDollar1Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxDollar1Cnt.Leave += new System.EventHandler(this.customTextBoxDollar1Cnt_Leave);
            // 
            // customLabelDollar100
            // 
            this.customLabelDollar100.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.customLabelDollar100.AutoSize = true;
            this.customLabelDollar100.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customLabelDollar100.Location = new System.Drawing.Point(4, 128);
            this.customLabelDollar100.Name = "customLabelDollar100";
            this.customLabelDollar100.Size = new System.Drawing.Size(31, 13);
            this.customLabelDollar100.TabIndex = 185;
            this.customLabelDollar100.Text = "$100";
            // 
            // customTextBoxCent100Amt
            // 
            this.customTextBoxCent100Amt.AllowOnlyNumbers = true;
            this.customTextBoxCent100Amt.CausesValidation = false;
            this.customTextBoxCent100Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent100Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent100Amt.Location = new System.Drawing.Point(143, 166);
            this.customTextBoxCent100Amt.Name = "customTextBoxCent100Amt";
            this.customTextBoxCent100Amt.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCent100Amt.TabIndex = 178;
            this.customTextBoxCent100Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent100Amt.Leave += new System.EventHandler(this.customTextBoxCent100Amt_Leave);
            // 
            // customTextBoxCent50Amt
            // 
            this.customTextBoxCent50Amt.AllowDecimalNumbers = true;
            this.customTextBoxCent50Amt.CausesValidation = false;
            this.customTextBoxCent50Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent50Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent50Amt.Location = new System.Drawing.Point(143, 142);
            this.customTextBoxCent50Amt.Name = "customTextBoxCent50Amt";
            this.customTextBoxCent50Amt.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCent50Amt.TabIndex = 177;
            this.customTextBoxCent50Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent50Amt.Leave += new System.EventHandler(this.customTextBoxCent50Amt_Leave);
            // 
            // customTextBoxCent25Amt
            // 
            this.customTextBoxCent25Amt.AllowDecimalNumbers = true;
            this.customTextBoxCent25Amt.CausesValidation = false;
            this.customTextBoxCent25Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent25Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent25Amt.Location = new System.Drawing.Point(143, 121);
            this.customTextBoxCent25Amt.Name = "customTextBoxCent25Amt";
            this.customTextBoxCent25Amt.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCent25Amt.TabIndex = 176;
            this.customTextBoxCent25Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent25Amt.Leave += new System.EventHandler(this.customTextBoxCent25Amt_Leave);
            // 
            // customTextBoxCent10Amt
            // 
            this.customTextBoxCent10Amt.AllowDecimalNumbers = true;
            this.customTextBoxCent10Amt.CausesValidation = false;
            this.customTextBoxCent10Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent10Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent10Amt.Location = new System.Drawing.Point(143, 99);
            this.customTextBoxCent10Amt.Name = "customTextBoxCent10Amt";
            this.customTextBoxCent10Amt.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCent10Amt.TabIndex = 175;
            this.customTextBoxCent10Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent10Amt.Leave += new System.EventHandler(this.customTextBoxCent10Amt_Leave);
            // 
            // customTextBoxCent5Amt
            // 
            this.customTextBoxCent5Amt.AllowDecimalNumbers = true;
            this.customTextBoxCent5Amt.CausesValidation = false;
            this.customTextBoxCent5Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent5Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent5Amt.Location = new System.Drawing.Point(143, 77);
            this.customTextBoxCent5Amt.Name = "customTextBoxCent5Amt";
            this.customTextBoxCent5Amt.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCent5Amt.TabIndex = 174;
            this.customTextBoxCent5Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent5Amt.Leave += new System.EventHandler(this.customTextBoxCent5Amt_Leave);
            // 
            // customTextBoxCent1Amt
            // 
            this.customTextBoxCent1Amt.AllowDecimalNumbers = true;
            this.customTextBoxCent1Amt.CausesValidation = false;
            this.customTextBoxCent1Amt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent1Amt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent1Amt.Location = new System.Drawing.Point(143, 56);
            this.customTextBoxCent1Amt.Name = "customTextBoxCent1Amt";
            this.customTextBoxCent1Amt.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxCent1Amt.TabIndex = 173;
            this.customTextBoxCent1Amt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent1Amt.Leave += new System.EventHandler(this.customTextBoxCent1Amt_Leave);
            // 
            // customTextBoxCent100Cnt
            // 
            this.customTextBoxCent100Cnt.AcceptsReturn = true;
            this.customTextBoxCent100Cnt.AllowOnlyNumbers = true;
            this.customTextBoxCent100Cnt.CausesValidation = false;
            this.customTextBoxCent100Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent100Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent100Cnt.Location = new System.Drawing.Point(65, 168);
            this.customTextBoxCent100Cnt.Name = "customTextBoxCent100Cnt";
            this.customTextBoxCent100Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxCent100Cnt.TabIndex = 172;
            this.customTextBoxCent100Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent100Cnt.Leave += new System.EventHandler(this.customTextBoxCent100Cnt_Leave);
            // 
            // customTextBoxCent50Cnt
            // 
            this.customTextBoxCent50Cnt.AllowOnlyNumbers = true;
            this.customTextBoxCent50Cnt.CausesValidation = false;
            this.customTextBoxCent50Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent50Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent50Cnt.Location = new System.Drawing.Point(65, 145);
            this.customTextBoxCent50Cnt.Name = "customTextBoxCent50Cnt";
            this.customTextBoxCent50Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxCent50Cnt.TabIndex = 171;
            this.customTextBoxCent50Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent50Cnt.Leave += new System.EventHandler(this.customTextBoxCent50Cnt_Leave);
            // 
            // customTextBoxCent25Cnt
            // 
            this.customTextBoxCent25Cnt.AllowOnlyNumbers = true;
            this.customTextBoxCent25Cnt.CausesValidation = false;
            this.customTextBoxCent25Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent25Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent25Cnt.Location = new System.Drawing.Point(65, 122);
            this.customTextBoxCent25Cnt.Name = "customTextBoxCent25Cnt";
            this.customTextBoxCent25Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxCent25Cnt.TabIndex = 170;
            this.customTextBoxCent25Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent25Cnt.Leave += new System.EventHandler(this.customTextBoxCent25Cnt_Leave);
            // 
            // customTextBoxCent10Cnt
            // 
            this.customTextBoxCent10Cnt.AllowOnlyNumbers = true;
            this.customTextBoxCent10Cnt.CausesValidation = false;
            this.customTextBoxCent10Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent10Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent10Cnt.Location = new System.Drawing.Point(65, 99);
            this.customTextBoxCent10Cnt.Name = "customTextBoxCent10Cnt";
            this.customTextBoxCent10Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxCent10Cnt.TabIndex = 169;
            this.customTextBoxCent10Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent10Cnt.Leave += new System.EventHandler(this.customTextBoxCent10Cnt_Leave);
            // 
            // customTextBoxCent5Cnt
            // 
            this.customTextBoxCent5Cnt.AllowOnlyNumbers = true;
            this.customTextBoxCent5Cnt.CausesValidation = false;
            this.customTextBoxCent5Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent5Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent5Cnt.Location = new System.Drawing.Point(65, 78);
            this.customTextBoxCent5Cnt.Name = "customTextBoxCent5Cnt";
            this.customTextBoxCent5Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxCent5Cnt.TabIndex = 168;
            this.customTextBoxCent5Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent5Cnt.Leave += new System.EventHandler(this.customTextBoxCent5Cnt_Leave);
            // 
            // customTextBoxCent1Cnt
            // 
            this.customTextBoxCent1Cnt.AllowOnlyNumbers = true;
            this.customTextBoxCent1Cnt.CausesValidation = false;
            this.customTextBoxCent1Cnt.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent1Cnt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxCent1Cnt.Location = new System.Drawing.Point(65, 56);
            this.customTextBoxCent1Cnt.Name = "customTextBoxCent1Cnt";
            this.customTextBoxCent1Cnt.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxCent1Cnt.TabIndex = 167;
            this.customTextBoxCent1Cnt.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxCent1Cnt.Leave += new System.EventHandler(this.customTextBoxCent1Cnt_Leave);
            // 
            // customButtonCalculate
            // 
            this.customButtonCalculate.BackColor = System.Drawing.Color.Transparent;
            this.customButtonCalculate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonCalculate.BackgroundImage")));
            this.customButtonCalculate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonCalculate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonCalculate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonCalculate.FlatAppearance.BorderSize = 0;
            this.customButtonCalculate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonCalculate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonCalculate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonCalculate.ForeColor = System.Drawing.Color.White;
            this.customButtonCalculate.Location = new System.Drawing.Point(493, 259);
            this.customButtonCalculate.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonCalculate.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonCalculate.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonCalculate.Name = "customButtonCalculate";
            this.customButtonCalculate.Size = new System.Drawing.Size(100, 50);
            this.customButtonCalculate.TabIndex = 0;
            this.customButtonCalculate.Text = "Calculate";
            this.customButtonCalculate.UseVisualStyleBackColor = false;
            this.customButtonCalculate.Click += new System.EventHandler(this.customButtonCalculate_Click);
            // 
            // labelDollar1
            // 
            this.labelDollar1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDollar1.AutoSize = true;
            this.labelDollar1.Location = new System.Drawing.Point(16, 4);
            this.labelDollar1.Name = "labelDollar1";
            this.labelDollar1.Size = new System.Drawing.Size(19, 13);
            this.labelDollar1.TabIndex = 202;
            this.labelDollar1.Text = "$1";
            // 
            // labelDollar2
            // 
            this.labelDollar2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDollar2.AutoSize = true;
            this.labelDollar2.Location = new System.Drawing.Point(16, 26);
            this.labelDollar2.Name = "labelDollar2";
            this.labelDollar2.Size = new System.Drawing.Size(19, 13);
            this.labelDollar2.TabIndex = 203;
            this.labelDollar2.Text = "$2";
            // 
            // labelDollar5
            // 
            this.labelDollar5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDollar5.AutoSize = true;
            this.labelDollar5.Location = new System.Drawing.Point(16, 47);
            this.labelDollar5.Name = "labelDollar5";
            this.labelDollar5.Size = new System.Drawing.Size(19, 13);
            this.labelDollar5.TabIndex = 204;
            this.labelDollar5.Text = "$5";
            // 
            // labelDollar10
            // 
            this.labelDollar10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDollar10.AutoSize = true;
            this.labelDollar10.Location = new System.Drawing.Point(10, 67);
            this.labelDollar10.Name = "labelDollar10";
            this.labelDollar10.Size = new System.Drawing.Size(25, 13);
            this.labelDollar10.TabIndex = 205;
            this.labelDollar10.Text = "$10";
            // 
            // labelDollar20
            // 
            this.labelDollar20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDollar20.AutoSize = true;
            this.labelDollar20.Location = new System.Drawing.Point(10, 87);
            this.labelDollar20.Name = "labelDollar20";
            this.labelDollar20.Size = new System.Drawing.Size(25, 13);
            this.labelDollar20.TabIndex = 206;
            this.labelDollar20.Text = "$20";
            // 
            // labelDollar50
            // 
            this.labelDollar50.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelDollar50.AutoSize = true;
            this.labelDollar50.Location = new System.Drawing.Point(10, 107);
            this.labelDollar50.Name = "labelDollar50";
            this.labelDollar50.Size = new System.Drawing.Size(25, 13);
            this.labelDollar50.TabIndex = 207;
            this.labelDollar50.Text = "$50";
            // 
            // customButtonOtherTender
            // 
            this.customButtonOtherTender.BackColor = System.Drawing.Color.Transparent;
            this.customButtonOtherTender.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customButtonOtherTender.BackgroundImage")));
            this.customButtonOtherTender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.customButtonOtherTender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customButtonOtherTender.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButtonOtherTender.FlatAppearance.BorderSize = 0;
            this.customButtonOtherTender.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.customButtonOtherTender.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.customButtonOtherTender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButtonOtherTender.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButtonOtherTender.ForeColor = System.Drawing.Color.White;
            this.customButtonOtherTender.Location = new System.Drawing.Point(18, 259);
            this.customButtonOtherTender.Margin = new System.Windows.Forms.Padding(0);
            this.customButtonOtherTender.MaximumSize = new System.Drawing.Size(100, 50);
            this.customButtonOtherTender.MinimumSize = new System.Drawing.Size(100, 50);
            this.customButtonOtherTender.Name = "customButtonOtherTender";
            this.customButtonOtherTender.Size = new System.Drawing.Size(100, 50);
            this.customButtonOtherTender.TabIndex = 208;
            this.customButtonOtherTender.Text = "Other Tender";
            this.customButtonOtherTender.UseVisualStyleBackColor = false;
            this.customButtonOtherTender.Click += new System.EventHandler(this.customButtonOtherTender_Click);
            // 
            // customTextBoxOTCount
            // 
            this.customTextBoxOTCount.AllowOnlyNumbers = true;
            this.customTextBoxOTCount.CausesValidation = false;
            this.customTextBoxOTCount.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxOTCount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxOTCount.Location = new System.Drawing.Point(143, 275);
            this.customTextBoxOTCount.Name = "customTextBoxOTCount";
            this.customTextBoxOTCount.ReadOnly = true;
            this.customTextBoxOTCount.Size = new System.Drawing.Size(44, 21);
            this.customTextBoxOTCount.TabIndex = 209;
            this.customTextBoxOTCount.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // customTextBoxOTAmount
            // 
            this.customTextBoxOTAmount.AllowDecimalNumbers = true;
            this.customTextBoxOTAmount.CausesValidation = false;
            this.customTextBoxOTAmount.ErrorMessage = global::Pawn.Properties.Resources.OverrideMachineName;
            this.customTextBoxOTAmount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customTextBoxOTAmount.Location = new System.Drawing.Point(208, 275);
            this.customTextBoxOTAmount.Name = "customTextBoxOTAmount";
            this.customTextBoxOTAmount.Size = new System.Drawing.Size(100, 21);
            this.customTextBoxOTAmount.TabIndex = 210;
            this.customTextBoxOTAmount.ValidationExpression = global::Pawn.Properties.Resources.OverrideMachineName;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tableLayoutPanel2);
            this.panelMain.Controls.Add(this.tableLayoutPanel1);
            this.panelMain.Controls.Add(this.labelHeader);
            this.panelMain.Controls.Add(this.customTextBoxOTAmount);
            this.panelMain.Controls.Add(this.labelCoinCountHeading);
            this.panelMain.Controls.Add(this.customTextBoxOTCount);
            this.panelMain.Controls.Add(this.labelCoinAmtHeading);
            this.panelMain.Controls.Add(this.customButtonOtherTender);
            this.panelMain.Controls.Add(this.labelDollarCountHeading);
            this.panelMain.Controls.Add(this.labelDollarAmtHeading);
            this.panelMain.Controls.Add(this.labelCoinTotalHeading);
            this.panelMain.Controls.Add(this.labelCurrencyTotalHeading);
            this.panelMain.Controls.Add(this.customButtonCalculate);
            this.panelMain.Controls.Add(this.customTextBoxCurrencyTotal);
            this.panelMain.Controls.Add(this.customTextBoxCoinTotal);
            this.panelMain.Controls.Add(this.customTextBoxDollar100Amt);
            this.panelMain.Controls.Add(this.customTextBoxDollar50Amt);
            this.panelMain.Controls.Add(this.customTextBoxDollar20Amt);
            this.panelMain.Controls.Add(this.customTextBoxCent1Cnt);
            this.panelMain.Controls.Add(this.customTextBoxDollar10Amt);
            this.panelMain.Controls.Add(this.customTextBoxCent5Cnt);
            this.panelMain.Controls.Add(this.customTextBoxDollar5Amt);
            this.panelMain.Controls.Add(this.customTextBoxCent10Cnt);
            this.panelMain.Controls.Add(this.customTextBoxDollar2Amt);
            this.panelMain.Controls.Add(this.customTextBoxCent25Cnt);
            this.panelMain.Controls.Add(this.customTextBoxDollar1Amt);
            this.panelMain.Controls.Add(this.customTextBoxCent50Cnt);
            this.panelMain.Controls.Add(this.customTextBoxDollar100Cnt);
            this.panelMain.Controls.Add(this.customTextBoxCent100Cnt);
            this.panelMain.Controls.Add(this.customTextBoxDollar50Cnt);
            this.panelMain.Controls.Add(this.customTextBoxCent1Amt);
            this.panelMain.Controls.Add(this.customTextBoxDollar20Cnt);
            this.panelMain.Controls.Add(this.customTextBoxCent5Amt);
            this.panelMain.Controls.Add(this.customTextBoxDollar10Cnt);
            this.panelMain.Controls.Add(this.customTextBoxCent10Amt);
            this.panelMain.Controls.Add(this.customTextBoxDollar5Cnt);
            this.panelMain.Controls.Add(this.customTextBoxCent25Amt);
            this.panelMain.Controls.Add(this.customTextBoxDollar2Cnt);
            this.panelMain.Controls.Add(this.customTextBoxCent50Amt);
            this.panelMain.Controls.Add(this.customTextBoxDollar1Cnt);
            this.panelMain.Controls.Add(this.customTextBoxCent100Amt);
            this.panelMain.Location = new System.Drawing.Point(3, 3);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(697, 320);
            this.panelMain.TabIndex = 211;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.labelcoin1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelcoin5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelCoin10, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.labelCoin25, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labelCoin50, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.labelDollar, 0, 5);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(23, 56);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(30, 133);
            this.tableLayoutPanel2.TabIndex = 212;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.labelDollar1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelDollar2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelDollar5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelDollar10, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelDollar20, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelDollar50, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.customLabelDollar100, 0, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(351, 62);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(38, 145);
            this.tableLayoutPanel1.TabIndex = 211;
            // 
            // CurrencyEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelMain);
            this.Name = "CurrencyEntry";
            this.Size = new System.Drawing.Size(706, 328);
            this.EnabledChanged += new System.EventHandler(this.CurrencyEntry_EnabledChanged);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Label labelCoinCountHeading;
        private System.Windows.Forms.Label labelCoinAmtHeading;
        private System.Windows.Forms.Label labelDollarCountHeading;
        private System.Windows.Forms.Label labelDollarAmtHeading;
        private System.Windows.Forms.Label labelCoinTotalHeading;
        private System.Windows.Forms.Label labelCurrencyTotalHeading;
        private CustomButton customButtonCalculate;
        private System.Windows.Forms.Label labelcoin1;
        private System.Windows.Forms.Label labelcoin5;
        private System.Windows.Forms.Label labelCoin10;
        private System.Windows.Forms.Label labelCoin25;
        private System.Windows.Forms.Label labelCoin50;
        private System.Windows.Forms.Label labelDollar;
        private CustomTextBox customTextBoxCent1Cnt;
        private CustomTextBox customTextBoxCent5Cnt;
        private CustomTextBox customTextBoxCent10Cnt;
        private CustomTextBox customTextBoxCent25Cnt;
        private CustomTextBox customTextBoxCent50Cnt;
        private CustomTextBox customTextBoxCent100Cnt;
        private CustomTextBox customTextBoxCent1Amt;
        private CustomTextBox customTextBoxCent5Amt;
        private CustomTextBox customTextBoxCent10Amt;
        private CustomTextBox customTextBoxCent25Amt;
        private CustomTextBox customTextBoxCent50Amt;
        private CustomTextBox customTextBoxCent100Amt;
        private CustomLabel customLabelDollar100;
        private CustomTextBox customTextBoxDollar1Cnt;
        private CustomTextBox customTextBoxDollar2Cnt;
        private CustomTextBox customTextBoxDollar5Cnt;
        private CustomTextBox customTextBoxDollar10Cnt;
        private CustomTextBox customTextBoxDollar20Cnt;
        private CustomTextBox customTextBoxDollar50Cnt;
        private CustomTextBox customTextBoxDollar100Cnt;
        private CustomTextBox customTextBoxDollar1Amt;
        private CustomTextBox customTextBoxDollar2Amt;
        private CustomTextBox customTextBoxDollar5Amt;
        private CustomTextBox customTextBoxDollar10Amt;
        private CustomTextBox customTextBoxDollar20Amt;
        private CustomTextBox customTextBoxDollar50Amt;
        private CustomTextBox customTextBoxDollar100Amt;
        private CustomTextBox customTextBoxCoinTotal;
        private CustomTextBox customTextBoxCurrencyTotal;
        private System.Windows.Forms.Label labelDollar1;
        private System.Windows.Forms.Label labelDollar2;
        private System.Windows.Forms.Label labelDollar5;
        private System.Windows.Forms.Label labelDollar10;
        private System.Windows.Forms.Label labelDollar20;
        private System.Windows.Forms.Label labelDollar50;
        private CustomButton customButtonOtherTender;
        private CustomTextBox customTextBoxOTCount;
        private CustomTextBox customTextBoxOTAmount;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
