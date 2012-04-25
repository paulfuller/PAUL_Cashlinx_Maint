/*************************************************************************************
* Namespace:       CommonUI.DesktopForms.Pawn.Products.DescribeMerchandise
* Class:           CategoryNode
* 
* Description      Form to View/Edit item on active Pawn Loan
* 
* History
* David D Wise, Initial Development
* Sreelatha Rengarajan    Changed the calculation for watch movement for when
* the user entered grams is less than 15.55 from reducing by 2% to 20%
* 04/14/2010 DW Moved the PFI Tag printing functionality here from PFI_Posting.cs page
* 
*  no ticket SMurphy 4/21/2010 addressed missing Proknow values when entering from read only direction
*  no ticket SMurphy 5/6/2010 issue when going Back during PFI processing
* SR 4/17/2010 Added stones flow for watches
*  PWNU00000677 SMurphy 6/1/2010 put in message and disallow Submit when description > 200 char
 *  
*  SMurphy 6/9/2010 move "Gauge" to "Caliber" in QuickInfo when needed - to handle Shotguns
*  SMurphy 6/9/2010 don't set model & manu to null if N/A - store whatever is in DescribeItem
*  Jkings  11/10/2010 Alignment correction for Get Values button
*  Madhu 11/18/2010 fix for defect PWNU00001443
*  Madhu 11/18/2010 fix for the defect PWNU00001456
*  Madhu 01/14/2011 fix for BZ defect 78
*  Madhu 02/07/2011 fix for BZ # 27 to disable the hover functionality 
*  SR 02/09/2011 fix for BZ 156 to not check for loan amount exceeding state max if its a temp icn
*  being created as part of retail sale
*  
*************************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Pawn.Loan;
using Common.Libraries.Forms.Pawn.Services.PFI;
using Common.Libraries.Forms.Retail;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Common.ProKnowService;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class DescribeItem : Form
    {
        private const int jewlleryCategoryCode = 1999;
        private int _CategoryMask;                          // Calling Application passed Category Mast
        private int _CategoryCode;

        private CurrentContext _CurrentContext { get; set; }
        public DesktopSession DesktopSession { get; private set; }
        // Variable holding desktop flow current context
        private int _CurrentPawnIndex;                      // Stores current active Pawn Record Index
        private Item _Item;                         // Holds Current Pawn Item in memory
        private Item _TempItem;
        private Item _OriginalItem; //Holds the original values of the item for edit functionality
        // Used during unchecking Merchandise checkbox
        private List<string> _RequiredAttributes;           // List of Required Attributes
        private decimal _ProKnow_SuggestedLoanAmount_NonJewelry;
        private decimal _ProKnow_SuggestedLoanAmount;
        private bool _ReadOnly;
        private Form _OwnerForm;
        private bool continueProcess = true;
        private const string SCRAP_STRING = "SCRAP";
        private int ItemOrder = 0;
        private bool purchaseFlow;
        private bool saleFlow;
        private bool changeRetailPriceFlow;
        private bool layawayFlow;
        private bool customerPurchasePFI;
        private bool vendorPurchaseFlow;
        private decimal totStoneValue;
        public NavBox NavControlBox;
        public JewelryDisplay JewelryDisplayForm;

        public ProKnowMatch SelectedProKnowMatch { get; set; }

        // Instantiation for Pawn Item Edits
        public DescribeItem(DesktopSession desktopSession, CurrentContext FlowCurrentContext, int iCurrentPawnIndex)
        {
            DesktopSession = desktopSession;
            InitializeComponent();
            this.editStonesButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.backButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.replaceButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.chargeOffButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.BackgroundImage = DesktopSession.ResourceProperties.newDialog_600_BlueScale;
            this.NavControlBox = new NavBox();
            DateTime pfiDate;
            _CurrentContext = FlowCurrentContext;
            _ReadOnly = _CurrentContext == CurrentContext.READ_ONLY || _CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE;
            _CurrentPawnIndex = iCurrentPawnIndex;
            if (SelectedProKnowMatch == null)
                SelectedProKnowMatch = DesktopSession.DescribeItemSelectedProKnowMatch;
            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.CUSTOMERPURCHASE.ToString() ||
                DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.DESCRIBEITEMCUSTOMERPURCHASE ||
                DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.VENDORPURCHASE ||
                DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.CUSTOMERPURCHASEPFI ||
                DesktopSession.PurchasePFIAddItem)
            {
                purchaseFlow = true;
            }
            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.CUSTOMERPURCHASEPFI)
                customerPurchasePFI = true; 
            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.VENDORPURCHASE)
                vendorPurchaseFlow = true;
            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.RETAIL)
                saleFlow = true;
            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.CHANGERETAILPRICE)
                changeRetailPriceFlow = true;
            if ((DesktopSession.HistorySession.Trigger.ToUpper() == Commons.TriggerTypes.LOOKUPTICKET.ToUpper() ||
                 DesktopSession.HistorySession.Trigger.ToUpper() == Commons.TriggerTypes.LOOKUPCUSTOMER.ToUpper()) &&
                 DesktopSession.ActiveLayaway != null)
                layawayFlow = true;

            if (purchaseFlow)
            {
                _CategoryMask = DesktopSession.ActivePurchase.Items[iCurrentPawnIndex].CategoryMask;
                _CategoryCode = DesktopSession.ActivePurchase.Items[iCurrentPawnIndex].CategoryCode;
                _Item = DesktopSession.ActivePurchase.Items[iCurrentPawnIndex];
            }
            else if (saleFlow || changeRetailPriceFlow)
            {
                pfiDate = ((CustomerProductDataVO)DesktopSession.ActiveRetail).Items[iCurrentPawnIndex].PfiDate;
                _CategoryMask = ((CustomerProductDataVO)DesktopSession.ActiveRetail).Items[iCurrentPawnIndex].CategoryMask;
                _CategoryCode = ((CustomerProductDataVO)DesktopSession.ActiveRetail).Items[iCurrentPawnIndex].CategoryCode;
                _Item = ((CustomerProductDataVO)DesktopSession.ActiveRetail).Items[iCurrentPawnIndex];
                _Item.PfiDate = pfiDate;
            }
            else if (layawayFlow)
            {
                if (DesktopSession.ActiveLayaway != null)
                {
                    pfiDate = ((CustomerProductDataVO)DesktopSession.ActiveLayaway).Items[iCurrentPawnIndex].PfiDate;
                    _CategoryMask = ((CustomerProductDataVO)DesktopSession.ActiveLayaway).Items[iCurrentPawnIndex].CategoryMask;
                    _CategoryCode = ((CustomerProductDataVO)DesktopSession.ActiveLayaway).Items[iCurrentPawnIndex].CategoryCode;
                    _Item = ((CustomerProductDataVO)DesktopSession.ActiveLayaway).Items[iCurrentPawnIndex];
                    _Item.PfiDate = pfiDate;
                }
            }
            else
            {
                if (DesktopSession.ActivePawnLoan != null)
                {
                    _CategoryMask = DesktopSession.ActivePawnLoan.Items[iCurrentPawnIndex].CategoryMask;
                    _CategoryCode = DesktopSession.ActivePawnLoan.Items[iCurrentPawnIndex].CategoryCode;
                    _Item = DesktopSession.ActivePawnLoan.Items[iCurrentPawnIndex];
                }
            }
            if (_CurrentContext == CurrentContext.EDIT_MMP)
            {
                _OriginalItem = Utilities.CloneObject(_Item);
            }
            this.JewelryDisplayForm = new JewelryDisplay(DesktopSession.ResourceProperties);
            this.JewelryDisplayForm.Hide();
        }

        private void DescribeItem_Load(object sender, EventArgs e)
        {
            if (_CategoryMask > 0 && _CategoryCode == 0)
                _CategoryCode = _CategoryMask;
            Setup();
            //SR 02/16/2010
            //Check the logged in user's privileges to determine if the
            //charge off button should be shown
            //Make that check only if the button is visible
            if (this.chargeOffButton.Visible)
            {
                if (!(SecurityProfileProcedures.CanUserViewResource("CHARGEOFF", DesktopSession.LoggedInUserSecurityProfile, DesktopSession)))
                    this.chargeOffButton.Enabled = false;
            }

            if (!continueProcess)
                return;
        }

        /// <summary>
        /// Initial Setup of Form
        /// </summary>
        private void Setup()
        {
            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer, true);

            _OwnerForm = this.Owner;
            this.NavControlBox.Owner = this;
            grpFeatures.Visible = false;
            _RequiredAttributes = new List<string>();
            HideGroups();
            labelGramValueHeading.Visible = false;
            labelStoneValueHeading.Visible = false;
            labelGramValue.Visible = false;
            labelStoneValue.Visible = false;

            // If calling page did not set Category Mask property, return
            if (_CategoryMask < 0 && _Item == null)
            {
                MessageBox.Show("Category Mask was not provided.", "Setup Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ExitOutToCallingPage(0);
                return;
            }

            if (_Item.CategoryCode == 0)
                _Item.CategoryCode = _CategoryCode;
            if (_Item.CategoryMask == 0)
                _Item.CategoryMask = _CategoryMask;

            if (_CurrentContext == CurrentContext.PFI_REDESCRIBE)
            {
                _ReadOnly = _Item.HoldType == "2";
                costAmountLabel.Visible = true;
                attribute_costAmount_TextBox.Visible = true;
                attribute_costAmount_TextBox.Enabled = !_ReadOnly;
                attribute_loanAmount_TextBox.ReadOnly = true;
            }
            else if (_CurrentContext == CurrentContext.PFI_ADD)
            {
                if (!purchaseFlow)
                {
                    _Item.mDocNumber = DesktopSession.ActivePawnLoan.OrigTicketNumber;
                    _Item.mDocType = "1";
                    _Item.mItemOrder = DesktopSession.ActivePawnLoan.Items.Count;
                    _Item.mYear = DesktopSession.ActivePawnLoan.Items[0].mYear;
                }
                else
                {
                    _Item.mDocNumber = DesktopSession.ActivePurchase.TicketNumber;
                    _Item.mDocType = "2";
                    _Item.mItemOrder = DesktopSession.ActivePurchase.Items.Count;
                    _Item.mYear = DesktopSession.ActivePurchase.Items[0].mYear;
                }
            }
            else if (_CurrentContext == CurrentContext.PFI_REPLACE)
            {
                ItemOrder = DesktopSession.SelectedItemOrder;
                if (!purchaseFlow)
                {
                    if (DesktopSession.ActivePawnLoan.Items[0].IsGun && !_Item.IsGun)
                    {
                        MessageBox.Show(
                            "You cannot replace a Gun item with non-Gun item.",
                            "Gun Replacement Verification",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        _Item = Utilities.CloneObject<Item>(DesktopSession.ActivePawnLoan.Items[0]);
                        _CurrentContext = CurrentContext.PFI_REDESCRIBE;
                    }
                    else
                    {
                        DesktopSession.ActivePawnLoan.Items = new List<Item>();
                        _Item.mItemOrder = 1;
                        _Item.mDocNumber = DesktopSession.ReplaceDocNumber;
                        _Item.mDocType = DesktopSession.ReplaceDocType;
                        _Item.Icn = DesktopSession.ReplaceICN;
                        _Item.GunNumber = DesktopSession.ReplaceGunNumber;
                        _Item.PfiTags = DesktopSession.ReplaceNumberOfTags;
                        DesktopSession.ActivePawnLoan.Items.Add(_Item);
                    }
                }
                else
                {
                    if (DesktopSession.ActivePurchase.Items[0].IsGun && !_Item.IsGun)
                    {
                        MessageBox.Show(
                            "You cannot replace a Gun item with non-Gun item.",
                            "Gun Replacement Verification",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        _Item = Utilities.CloneObject<Item>(DesktopSession.ActivePurchase.Items[0]);
                        _CurrentContext = CurrentContext.PFI_REDESCRIBE;
                    }
                    else
                    {
                        DesktopSession.ActivePurchase.Items = new List<Item>();
                        _Item.mItemOrder = 1;
                        _Item.mDocNumber = DesktopSession.ReplaceDocNumber;
                        _Item.mDocType = DesktopSession.ReplaceDocType;
                        _Item.Icn = DesktopSession.ReplaceICN;
                        _Item.GunNumber = DesktopSession.ReplaceGunNumber;
                        _Item.PfiTags = DesktopSession.ReplaceNumberOfTags;
                        DesktopSession.ActivePurchase.Items.Add(_Item);
                    }
                }
                _CurrentPawnIndex = 0;
                DesktopSession.ReplaceDocNumber = 0;
                DesktopSession.ReplaceDocType = string.Empty;
                DesktopSession.ReplaceICN = string.Empty;
            }
            else if (_CurrentContext == CurrentContext.PFI_MERGE)
            {
                costAmountLabel.Visible = true;
                attribute_costAmount_TextBox.Visible = true;
                attribute_loanAmount_TextBox.ReadOnly = true;
                attribute_RetailPrice_TextBox.Visible = true;
                decimal totItemAmount = 0.0M;
                if (purchaseFlow)
                {
                    foreach (int i in DesktopSession.SelectedPFIMergeItemIndex)
                    {
                        if (_Item.mDocNumber == 0)
                            _Item.mDocNumber = DesktopSession.ActivePurchase.Items[i].mDocNumber;
                        
                        totItemAmount += DesktopSession.ActivePurchase.Items[i].ItemAmount;
                    }
                }
                else
                {
                    foreach (int i in DesktopSession.SelectedPFIMergeItemIndex)
                    {
                        if (_Item.mDocNumber == 0)
                            _Item.mDocNumber = DesktopSession.ActivePawnLoan.Items[i].mDocNumber;
                        totItemAmount += DesktopSession.ActivePawnLoan.Items[i].ItemAmount;
                    }
                }
                attribute_costAmount_TextBox.Text = String.Format("{0:0.00}", totItemAmount);
                attribute_loanAmount_TextBox.Text = String.Format("{0:0.00}", totItemAmount);
            }
            else if (_CurrentContext == CurrentContext.EDIT_MMP)
            {
                _Item.ItemAmount_Original = _Item.ItemAmount;
                costAmountLabel.Visible = false;
                attribute_costAmount_TextBox.Visible = false;
                attribute_loanAmount_TextBox.ReadOnly = false;
            }
            else if (_CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE)
            {
                costAmountLabel.Visible = true;
                attribute_costAmount_TextBox.Visible = true;
                attribute_costAmount_TextBox.Enabled = false;

                loanAmountLabel.Visible = false;
                attribute_loanAmount_TextBox.Visible = false;

                retailPriceLabel.Text = "New Retail Price[$]:";
            }
            else if (_CurrentContext == CurrentContext.READ_ONLY)
            {
                attribute_costAmount_TextBox.Enabled = false;
                attribute_loanAmount_TextBox.Enabled = false;
                attribute_RetailPrice_TextBox.Enabled = false;
            }
            else if (_CurrentContext == CurrentContext.AUDITCHARGEON)
            {
                costAmountLabel.Visible = true;
                attribute_costAmount_TextBox.Visible = true;
                attribute_costAmount_TextBox.Enabled = true;

                loanAmountLabel.Visible = false;
                attribute_loanAmount_TextBox.Visible = false;

            }
            else if (_CurrentContext == CurrentContext.GUNEDIT ||
                _CurrentContext == CurrentContext.GUNREPLACE)
            {
                costAmountLabel.Visible = false;
                attribute_costAmount_TextBox.Visible = false;
                loanAmountLabel.Visible = false;
                attribute_loanAmount_TextBox.Visible = false;
            }
            else
            {
                costAmountLabel.Visible = false;
                attribute_costAmount_TextBox.Visible = false;
                attribute_loanAmount_TextBox.ReadOnly = false;
            }

            if (continueProcess)
            {
                if (SelectedProKnowMatch != null)
                {
                    _Item.SelectedProKnowMatch = SelectedProKnowMatch;
                    ProKnowCleanUp();
                }

                // If coming in through the Describe Merchandise Button Navigation and no Active Pawn Exists
                if (purchaseFlow)
                {
                    if (DesktopSession.ActivePurchase == null)
                    {
                        DesktopSession.Purchases = new List<PurchaseVO>();
                        DesktopSession.Purchases.Add(new PurchaseVO());
                    }
                }
                else if (layawayFlow)
                {
                    if (DesktopSession.ActiveLayaway == null)
                    {
                        DesktopSession.Layaways = new List<LayawayVO>();
                        DesktopSession.Layaways.Add(new LayawayVO());
                    }
                }
                else
                {
                    if (DesktopSession.ActivePawnLoan == null)
                    {
                        DesktopSession.PawnLoans = new List<PawnLoan>();
                        DesktopSession.PawnLoans.Add(new PawnLoan());
                    }
                }

#if DEBUG
                categoryMaskLabel.Text = "Mask: " + _CategoryMask.ToString();
#else
                categoryMaskLabel.Text = string.Empty;
#endif

                editStonesButton.Enabled = _ReadOnly;
                getValuesButton.Visible = !_ReadOnly;
                submitButton.Visible = DisplaySubmitButton();
                //SR 8/14/2009 Changes done to show the pro know values even in the read only context
                if (_ReadOnly)
                {
                    //getValuesToShow();
                    getValuePrep();

                    if (_Item.IsJewelry)
                        getJewelryValuesToShow();
                    else
                        getValuesToShow();
                }
                //end modification by SR

                replaceButton.Visible = (
                (
                _Item.ItemReason == ItemReason.BLNK
                || _Item.ItemReason == ItemReason.COFFBRKN
                || _Item.ItemReason == ItemReason.CACC
                || _Item.ItemReason == ItemReason.COFFNXT
                || _Item.ItemReason == ItemReason.COFFSTRU
                )
                && (
                _CurrentContext == CurrentContext.PFI_ADD
                || _CurrentContext == CurrentContext.PFI_MERGE
                || _CurrentContext == CurrentContext.PFI_REDESCRIBE
                || _CurrentContext == CurrentContext.GUNEDIT
                )
                && _Item.Tag != ItemReason.MERGED.ToString()
                && _Item.Tag != ItemReason.ADDD.ToString()
                && !_ReadOnly
                );

                chargeOffButton.Visible =
                (
                (
                _CurrentContext == CurrentContext.PFI_ADD
                || _CurrentContext == CurrentContext.PFI_MERGE
                || _CurrentContext == CurrentContext.PFI_REDESCRIBE
                ) &&
                !_Item.IsGun
                    /*&&
                    (
                    _Item.ItemReason == ItemReason.ADDD
                    ||
                    _Item.ItemReason == ItemReason.BLNK
                    )*/
                && !_ReadOnly
                );

                merchandiseAvailableCheckbox.Checked = true;
                merchandiseAvailableCheckbox.Visible =
                (
                _CurrentContext == CurrentContext.PFI_REDESCRIBE
                && !_ReadOnly
                );
                cancelButton.Visible = DisplayCancelButton();
                panelCurrentRetailPrice.Visible = _CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE;
                if (_CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE && !merchandiseAvailableCheckbox.Visible)
                {
                    panelCurrentRetailPrice.Left = merchandiseAvailableCheckbox.Left;
                }

                if (_CurrentContext == CurrentContext.PFI_REDESCRIBE)
                    _TempItem = Utilities.CloneObject(_Item);

                getValuesButton.Location = cancelButton.Visible == false ? new Point(437, 637) : new Point(495, 575);

                if (!_ReadOnly)
                {
                    if ((merchandiseAvailableCheckbox.Visible
                         && _Item.CaccLevel != 0)
                         || (_CurrentContext == CurrentContext.PFI_ADD && _Item.CaccLevel != 0)
                         || _CurrentContext == CurrentContext.PFI_REPLACE
                         || _CurrentContext == CurrentContext.PFI_MERGE
                         || (vendorPurchaseFlow)
                        || _CurrentContext == CurrentContext.AUDITCHARGEON )
                    {
                        retailPriceLabel.Visible = true;
                        attribute_RetailPrice_TextBox.Visible = true;
                    }
                    else
                    {
                        retailPriceLabel.Visible = false;
                        attribute_RetailPrice_TextBox.Visible = false;
                    }
                }

                if (_Item.CaccLevel == 0 || _Item.RetailPrice <= 0)
                {
                    attribute_RetailPrice_TextBox.Text = "0.00";

                }

                // In case form is populated with existing saved Item, check to enable buttons
                bool bEnable = _RequiredAttributes.Count == 0 || RequiredAttributesCollected();
                if (_Item.CaccLevel == 0 || _CurrentContext==CurrentContext.GUNEDIT || _CurrentContext==CurrentContext.GUNREPLACE)
                {
                    getValuesButton.Enabled = false;
                    submitButton.Enabled = bEnable;
                }
                else
                    getValuesButton.Enabled = bEnable;
                editStonesButton.Enabled = bEnable;

                if (purchaseFlow)
                {
                    loanAmountLabel.Text = "Purchase Amount[$]:";
                    //Madhu 11/18/2010 fix for defect PWNU00001443
                    this.lblLoanValues_SuggestedLoanLabel.Text = "Suggested Purchase Value:";
                    this.lblLoanValues_LoanValueHighLabel.Text = "Purchase Value - High:";
                    this.lblLoanValues_LoanValueLowLabel.Text = "Purchase Value - Low:";
                    this.grpLoanValues.Text = "PURCHASE VALUES";
                }
                if (vendorPurchaseFlow)
                {
                    loanAmountLabel.Text = "Per Item Amount";
                    retailPriceLabel.Text = "Per Item Retail";
                    if (_Item.CaccLevel == 0)
                    {
                        retailPriceLabel.Visible = false;
                        attribute_RetailPrice_TextBox.Visible = false;
                    }
                    else
                    {
                        retailPriceLabel.Visible = true;
                        attribute_RetailPrice_TextBox.Visible = true;
                    }
                    //Madhu 11/18/2010 fix for defect PWNU00001443
                    this.lblLoanValues_SuggestedLoanLabel.Text = "Suggested Purchase Value:";
                    this.lblLoanValues_LoanValueHighLabel.Text = "Purchase Value - High:";
                    this.lblLoanValues_LoanValueLowLabel.Text = "Purchase Value - Low:";
                    this.grpLoanValues.Text = "PURCHASE VALUES";
                }
                if ((saleFlow || layawayFlow) && !_ReadOnly)
                    loanAmountLabel.Text = "Retail Amount";
                else if ((saleFlow || layawayFlow) && _ReadOnly) // Fix for BZ # 239
                {
                    loanAmountLabel.Visible = false;
                    attribute_loanAmount_TextBox.Visible = false;
                    //costAmountLabel.Visible = true;
                    //attribute_costAmount_TextBox.Visible = true;
                    //attribute_costAmount_TextBox.ReadOnly = true;
                    costAmountLabel.Top = descriptonTextBox.Location.Y + descriptonTextBox.Height + 5;
                    attribute_costAmount_TextBox.Top = descriptonTextBox.Location.Y + descriptonTextBox.Height + 5;
                    if (_Item != null)
                        attribute_costAmount_TextBox.Text = String.Format("{0:0.00}", _Item.ItemAmount);
                }//end - BZ # 239

                if (_CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE)
                {
                    submitButton.Enabled = true;
                    editStonesButton.Visible = false;
                }
            }
        }

        private bool DisplaySubmitButton()
        {
            if (_CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE)
            {
                return true;
            }

            return !_ReadOnly;
        }

        private bool DisplayCancelButton()
        {
            if (_CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE)
            {
                return true;
            }

            if (_ReadOnly)
            {
                return false;
            }

            return (_CurrentContext != CurrentContext.PFI_ADD
                    && _CurrentContext != CurrentContext.PFI_MERGE
                    && _CurrentContext != CurrentContext.PFI_REDESCRIBE
                    && _CurrentContext != CurrentContext.PFI_REPLACE);
        }

        // FTN 2.4 Initial ProKnow cleansing process
        private void ProKnowCleanUp()
        {
            if (_Item.Attributes != null)
            {
                int iFoundItemAttributeIndex = -1;
                int iFoundAnswerIndex = -1;

                // Get preAnswered questions and store in Item Attribute's Answer object
                if (_Item.SelectedProKnowMatch.preAnsweredQuestions != null)
                {
                    foreach (Answer origAnswer in _Item.SelectedProKnowMatch.preAnsweredQuestions)
                    {
                        int iMaskOrder = Convert.ToInt32(origAnswer.AnswerText.Replace("mask", ""));

                        iFoundItemAttributeIndex = _Item.Attributes.FindIndex(delegate(ItemAttribute iaSearch)
                        {
                            return iaSearch.MaskOrder == iMaskOrder;
                        });

                        if (iFoundItemAttributeIndex >= 0)
                        {
                            iFoundAnswerIndex = _Item.Attributes[iFoundItemAttributeIndex].AnswerList.FindIndex(delegate(Answer aSearch)
                            {
                                return aSearch.AnswerCode == origAnswer.AnswerCode;
                            });

                            ItemAttribute foundItemAttribute = _Item.Attributes[iFoundItemAttributeIndex];

                            if (iFoundAnswerIndex >= 0)
                            {
                                foundItemAttribute.IsPreAnswered = true;
                                foundItemAttribute.Answer = new Answer();
                                foundItemAttribute.Answer = _Item.Attributes[iFoundItemAttributeIndex].AnswerList[iFoundAnswerIndex];
                            }
                            else
                            {
                                foundItemAttribute.IsPreAnswered = false;
                            }
                            _Item.Attributes.RemoveAt(iFoundItemAttributeIndex);
                            _Item.Attributes.Insert(iFoundItemAttributeIndex, foundItemAttribute);
                        }
                    }
                }

                // Find AttributeCode = 1 and update Manufacturer information
                iFoundItemAttributeIndex = _Item.Attributes.FindIndex(delegate(ItemAttribute iaSearch)
                {
                    return iaSearch.AttributeCode == 1;
                });

                if (iFoundItemAttributeIndex >= 0 && _Item.SelectedProKnowMatch.manufacturerModelInfo != null)
                {
                    Answer answerManufacturerInfo = _Item.SelectedProKnowMatch.manufacturerModelInfo[0];

                    if (_Item.SelectedProKnowMatch.preAnsweredQuestions != null)
                    {
                        iFoundAnswerIndex = _Item.SelectedProKnowMatch.preAnsweredQuestions.FindIndex(delegate(Answer aSearch)
                        //               iFoundAnswerIndex = _Item.Attributes[iFoundItemAttributeIndex].AnswerList.FindIndex(delegate(Answer aSearch)
                        {
                            return aSearch.AnswerText == "mask" + _Item.Attributes[iFoundItemAttributeIndex].MaskOrder.ToString();
                        });

                        if (iFoundAnswerIndex >= 0)
                        {
                            answerManufacturerInfo.AnswerCode = _Item.SelectedProKnowMatch.preAnsweredQuestions[iFoundAnswerIndex].AnswerCode;
                            //                        answerManufacturerInfo.AnswerCode = _Item.Attributes[iFoundItemAttributeIndex].AnswerList[iFoundAnswerIndex].AnswerCode;

                            _Item.SelectedProKnowMatch.manufacturerModelInfo.RemoveAt(0);
                            _Item.SelectedProKnowMatch.manufacturerModelInfo.Insert(0, answerManufacturerInfo);
                        }
                    }

                    if (!_Item.Attributes[iFoundItemAttributeIndex].IsPreAnswered)
                    {
                        ItemAttribute foundItemAttribute = _Item.Attributes[iFoundItemAttributeIndex];
                        foundItemAttribute.Answer = _Item.SelectedProKnowMatch.manufacturerModelInfo[0];
                        foundItemAttribute.IsPreAnswered = true;

                        _Item.Attributes.RemoveAt(iFoundItemAttributeIndex);
                        _Item.Attributes.Insert(iFoundItemAttributeIndex, foundItemAttribute);
                    }
                }

                // Find AttributeCode = 3 and update Model information
                iFoundItemAttributeIndex = _Item.Attributes.FindIndex(delegate(ItemAttribute iaSearch)
                {
                    return iaSearch.AttributeCode == 3;
                });

                if (iFoundItemAttributeIndex >= 0 && _Item.SelectedProKnowMatch.manufacturerModelInfo != null)
                {
                    Answer answerModelInfo = _Item.SelectedProKnowMatch.manufacturerModelInfo[1];

                    if (_Item.SelectedProKnowMatch.preAnsweredQuestions != null)
                    {
                        iFoundAnswerIndex = _Item.SelectedProKnowMatch.preAnsweredQuestions.FindIndex(delegate(Answer aSearch)
                        //                 iFoundAnswerIndex = _Item.Attributes[iFoundItemAttributeIndex].AnswerList.FindIndex(delegate(Answer aSearch)
                        {
                            return aSearch.AnswerText == "mask" + _Item.Attributes[iFoundItemAttributeIndex].MaskOrder.ToString();
                        });
                        if (iFoundAnswerIndex >= 0)
                        {
                            answerModelInfo.AnswerCode = _Item.SelectedProKnowMatch.preAnsweredQuestions[iFoundAnswerIndex].AnswerCode;
                            //             answerModelInfo.AnswerCode = _Item.Attributes[iFoundItemAttributeIndex].AnswerList[iFoundAnswerIndex].AnswerCode;

                            _Item.SelectedProKnowMatch.manufacturerModelInfo.RemoveAt(1);
                            _Item.SelectedProKnowMatch.manufacturerModelInfo.Insert(1, answerModelInfo);
                        }
                    }

                    if (!_Item.Attributes[iFoundItemAttributeIndex].IsPreAnswered)
                    {
                        ItemAttribute foundItemAttribute = _Item.Attributes[iFoundItemAttributeIndex];
                        foundItemAttribute.Answer = _Item.SelectedProKnowMatch.manufacturerModelInfo[1];
                        foundItemAttribute.IsPreAnswered = true;

                        _Item.Attributes.RemoveAt(iFoundItemAttributeIndex);
                        _Item.Attributes.Insert(iFoundItemAttributeIndex, foundItemAttribute);
                    }
                }

                // Check for TransitionStatus.MAN_MODEL_PROKNOW_COMBO and update ItemAttribute information
                if (_Item.SelectedProKnowMatch.manufacturerModelInfo != null)
                {
                    if (_Item.SelectedProKnowMatch.transitionStatus == TransitionStatus.MAN_MODEL_PROKNOW_COMBO || _Item.SelectedProKnowMatch.manufacturerModelInfo.Count == 4)
                    {
                        // Get Secondary Manufacturer ItemAttribute
                        iFoundItemAttributeIndex = _Item.Attributes.FindIndex(delegate(ItemAttribute iaSearch)
                        {
                            return iaSearch.Description == _Item.SelectedProKnowMatch.manufacturerModelInfo[2].InputKey;
                        });

                        if (iFoundItemAttributeIndex >= 0)
                        {
                            ItemAttribute foundManufacturerItemAttribute = _Item.Attributes[iFoundItemAttributeIndex];

                            foundManufacturerItemAttribute.IsPreAnswered = true;
                            foundManufacturerItemAttribute.Answer = new Answer();
                            foundManufacturerItemAttribute.Answer = _Item.SelectedProKnowMatch.manufacturerModelInfo[2];

                            _Item.Attributes.RemoveAt(iFoundItemAttributeIndex);
                            _Item.Attributes.Insert(iFoundItemAttributeIndex, foundManufacturerItemAttribute);

                            // Get Secondary Model ItemAttribute
                            iFoundItemAttributeIndex = _Item.Attributes.FindIndex(delegate(ItemAttribute iaSearch)
                            {
                                return iaSearch.Description == _Item.SelectedProKnowMatch.manufacturerModelInfo[3].InputKey;
                            });

                            ItemAttribute foundModelItemAttribute = _Item.Attributes[iFoundItemAttributeIndex];

                            if (iFoundItemAttributeIndex >= 0)
                            {
                                foundModelItemAttribute.IsPreAnswered = true;
                                foundModelItemAttribute.Answer = new Answer();
                                foundModelItemAttribute.Answer = _Item.SelectedProKnowMatch.manufacturerModelInfo[3];
                            }

                            _Item.Attributes.RemoveAt(iFoundItemAttributeIndex);
                            _Item.Attributes.Insert(iFoundItemAttributeIndex, foundModelItemAttribute);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to establish Order and Attributes to add to form for User Entry
        /// </summary>
        private void AddCategoryAttributes()
        {
            categoryAttributeTableLayoutPanel.Visible = false;
            Application.DoEvents();
            submitButton.Enabled = false;
            editStonesButton.Enabled = false;
            getValuesButton.Enabled = false;
            categoryAttributeTableLayoutPanel.Controls.Clear();
            try
            {
                if (_Item != null)
                {
                    //Add quantity textbox if its vendor purchase
                    if (vendorPurchaseFlow)
                    {
                        if (_Item.CaccLevel != 0)
                        {
                            TextBox attributeQtyTagTextBox = new TextBox();
                            attributeQtyTagTextBox.MaxLength = 3;
                            attributeQtyTagTextBox.Width = 20;

                            attributeQtyTagTextBox.Name = "attribute_qnty_TextBox";
                            if (_Item.Quantity == 0)
                                _Item.Quantity = 1;
                            BuildOutAttributeTextBox(ref attributeQtyTagTextBox, new List<string> { _Item.Quantity.ToString() }, "", (_ReadOnly || !merchandiseAvailableCheckbox.Checked), true, true);
                            AddAttributeToForm("Quantity", attributeQtyTagTextBox, true);
                        }
                    }
                    // FTN 3.3.a
                    if (_ReadOnly
                        || _CurrentContext == CurrentContext.NEW
                        || _CurrentContext == CurrentContext.EDIT_MMP
                        || _CurrentContext == CurrentContext.PFI_ADD
                        || _CurrentContext == CurrentContext.PFI_MERGE
                        || _CurrentContext == CurrentContext.PFI_REDESCRIBE
                        || _CurrentContext == CurrentContext.PFI_REPLACE
                        || _CurrentContext == CurrentContext.AUDITCHARGEON
                        || _CurrentContext == CurrentContext.GUNEDIT
                        || _CurrentContext == CurrentContext.GUNREPLACE
                    )
                    {
                        // FTN 3.3.a.i
                        _Item.Attributes.Sort(delegate(ItemAttribute ItemAttribute_1, ItemAttribute ItemAttribute_2)
                        {
                            return ItemAttribute_1.LoanOrder.CompareTo(ItemAttribute_2.LoanOrder);
                        });

                        // FTN 3.3.a.ii
                        _Item.Attributes.ForEach(delegate(ItemAttribute iaItem)
                        {
                            string sDescription = iaItem.Description;
                            bool bIsReadOnly = iaItem.IsPreAnswered || _ReadOnly || !merchandiseAvailableCheckbox.Checked;
                            bool bIsRequired = iaItem.IsRequired;
                            int iOptionIdx = -1;     // Default Index for Combo Boxes
                            bool bIsRestricted = false;
                            string sMaskDefault = string.Empty;

                            List<string> answerList = new List<string>();

                            if (bIsReadOnly)
                            {
                                // FTN 3.3.a.ii.1.b
                                answerList.Add(iaItem.Answer.AnswerText);
                                TextBox attributeTextBox = new TextBox();
                                attributeTextBox.TabStop = false;
                                attributeTextBox.Name = "attribute_" + iaItem.AttributeCode.ToString() + "_TextBox";
                                if (iaItem.ValidationDataType == null)
                                    iaItem.ValidationDataType = string.Empty;
                                BuildOutAttributeTextBox(ref attributeTextBox, answerList, iaItem.ValidationDataType.ToString(), bIsReadOnly, true, false);

                                //SM
                                attributeTextBox.MaxLength = 30;
                                AddAttributeToForm(sDescription, attributeTextBox, bIsRequired);
                            }
                            else
                            {
                                // FTN 3.3.a.ii.1.c
                                if (iaItem.AnswerList.Count > 0)
                                {
                                    switch (iaItem.InputControl)
                                    {
                                        case ControlType.COMBOBOX_ONLY:                                                 // FTN 3.3.a.ii.1.c.i
                                            iaItem.AnswerList.Sort(delegate(Answer Anwser_1, Answer Answer_2)       // FTN 3.3.a.ii.1.c.i.1
                                            {
                                                return Anwser_1.DisplayOrder.CompareTo(Answer_2.DisplayOrder);
                                            });
                                            // FTN 3.3.a.ii.1.c.i.2
                                            bIsRestricted = true;
                                            iaItem.AnswerList.ForEach(delegate(Answer ansAnswerList)
                                            {
                                                answerList.Add(ansAnswerList.AnswerText);
                                            });
                                            // FTN 3.3.a.ii.1.c.i.3.a-b
                                            sMaskDefault = iaItem.MaskDefault;
                                            if (_CurrentContext != CurrentContext.NEW)
                                            {
                                                if (!string.IsNullOrEmpty(iaItem.Answer.AnswerText))
                                                    sMaskDefault = iaItem.Answer.AnswerText;
                                            }

                                            if (!string.IsNullOrEmpty(sMaskDefault))
                                            {
                                                iOptionIdx = iaItem.AnswerList.FindIndex(delegate(Answer ansAnswerList)
                                                {
                                                    return sMaskDefault == ((_CurrentContext == CurrentContext.NEW || _CurrentContext == CurrentContext.PFI_ADD) ? ansAnswerList.InputKey : ansAnswerList.AnswerText);
                                                });
                                            }
                                            // FTN 3.3.a.ii.1.c.i.4
                                            if (string.IsNullOrEmpty(sMaskDefault) && iOptionIdx == -1)
                                            {
                                                answerList.Add("");
                                                iOptionIdx = answerList.Count - 1;
                                            }
                                            else if (iOptionIdx == -1 && answerList.Count > 1)
                                            {
                                                answerList.Add("");
                                                iOptionIdx = answerList.Count - 1;
                                            }
                                            if (answerList.Count == 1)
                                                iOptionIdx = 0;

                                            // Build out the ComboBox based upon above criteria
                                            ComboBox attributeComboBox_Only = new ComboBox();
                                            attributeComboBox_Only.Name = "attribute_" + iaItem.AttributeCode.ToString() + "_ComboBox";
                                            BuildOutAttributeComboBox(ref attributeComboBox_Only, answerList, iaItem.ValidationDataType.ToString(), iOptionIdx, bIsReadOnly, bIsRestricted, bIsRequired);
                                            //SM
                                            attributeComboBox_Only.MaxLength = 30;
                                            AddAttributeToForm(sDescription, attributeComboBox_Only, bIsRequired);
                                            break;
                                        case ControlType.COMBOBOX_TEXT_ENABLED:                                         // FTN 3.3.a.ii.1.c.ii
                                            iaItem.AnswerList.Sort(delegate(Answer Anwser_1, Answer Answer_2)       // FTN 3.3.a.ii.1.c.ii.1
                                            {
                                                return Anwser_1.DisplayOrder.CompareTo(Answer_2.DisplayOrder);
                                            });
                                            // FTN 3.3.a.ii.1.c.ii.2
                                            bIsRestricted = false;
                                            iaItem.AnswerList.ForEach(delegate(Answer ansAnswerList)
                                            {
                                                answerList.Add(ansAnswerList.AnswerText);
                                            });
                                            // FTN 3.3.a.ii.1.c.ii.3.a-b
                                            sMaskDefault = iaItem.MaskDefault;
                                            if (_CurrentContext != CurrentContext.NEW)
                                            {
                                                if (!string.IsNullOrEmpty(iaItem.Answer.AnswerText))
                                                    sMaskDefault = iaItem.Answer.AnswerText;
                                            }

                                            if (!string.IsNullOrEmpty(sMaskDefault))
                                            {
                                                iOptionIdx = iaItem.AnswerList.FindIndex(delegate(Answer ansAnswerList)
                                                {
                                                    return sMaskDefault == ((_CurrentContext == CurrentContext.NEW) ? ansAnswerList.InputKey : ansAnswerList.AnswerText);
                                                });
                                            }
                                            // FTN 3.3.a.ii.1.c.ii.4
                                            if (string.IsNullOrEmpty(sMaskDefault) && iOptionIdx == -1)
                                            {
                                                answerList.Add("");
                                                iOptionIdx = answerList.Count - 1;
                                            }
                                            else if (iOptionIdx == -1)
                                            {
                                                answerList.Add(sMaskDefault);
                                                iOptionIdx = answerList.Count - 1;
                                            }

                                            // Build out the ComboBox based upon above criteria
                                            ComboBox attributeComboBox_Text_Enabled = new ComboBox();
                                            attributeComboBox_Text_Enabled.MaxLength = 30;
                                            //SR 05/31/2011 Fix for making sure caliber cannot be more than 10 characters. Need to find out if anything in iaitem can tell us what the max length can be - loanorder? descriptionorder?
                                            if (iaItem.Description == "Caliber")
                                                attributeComboBox_Text_Enabled.MaxLength = 30;

                                            attributeComboBox_Text_Enabled.Name = "attribute_" + iaItem.AttributeCode.ToString() + "_ComboBox";
                                            BuildOutAttributeComboBox(ref attributeComboBox_Text_Enabled, answerList, iaItem.ValidationDataType.ToString(), iOptionIdx, bIsReadOnly, bIsRestricted, bIsRequired);
                                            AddAttributeToForm(sDescription, attributeComboBox_Text_Enabled, bIsRequired);
                                            break;
                                    }
                                }
                                else
                                {
                                    // FTN 3.3.a.ii.1.c.iii
                                    if (iaItem.InputControl == ControlType.TEXTFIELD)
                                    {
                                        if (_CurrentContext != CurrentContext.NEW || _Item.ItemStatus == ProductStatus.PU || _Item.mDocNumber != 0)
                                        {
                                            if (!string.IsNullOrEmpty(iaItem.Answer.AnswerText))
                                                sMaskDefault = iaItem.Answer.AnswerText;
                                        }
                                        answerList.Add(sMaskDefault);
                                        TextBox attributeBlankTextBox = new TextBox();
                                        if (_Item.CategoryCode <= jewlleryCategoryCode)  // Max length is setting based on Attribute ID.
                                        {
                                            if (iaItem.AttributeCode == 10)
                                                attributeBlankTextBox.MaxLength = 6;
                                            else if (iaItem.AttributeCode == 903 || iaItem.AttributeCode == 18)
                                                attributeBlankTextBox.MaxLength = 5;
                                            else//SM
                                                attributeBlankTextBox.MaxLength = 30;
                                        }
                                        else
                                            attributeBlankTextBox.MaxLength = 30;
                                        attributeBlankTextBox.Name = "attribute_" + iaItem.AttributeCode.ToString() + "_TextBox";
                                        BuildOutAttributeTextBox(ref attributeBlankTextBox, answerList, iaItem.ValidationDataType, bIsReadOnly, true, bIsRequired);
                                        AddAttributeToForm(sDescription, attributeBlankTextBox, bIsRequired);
                                    }
                                }
                            }
                        });

                        // If Gun item, add a Trigger Lock CheckBox
                        if (_Item.CategoryCode == 4390 || _Item.MerchandiseType == "H" || _Item.MerchandiseType == "L")
                        {
                            CheckBox attributeTriggerLockCheckBox = new CheckBox();
                            if (_CurrentContext != CurrentContext.NEW)
                                attributeTriggerLockCheckBox.Checked = _Item.HasGunLock;
                            attributeTriggerLockCheckBox.Name = "attribute_GunLock_CheckBox";
                            BuildOutAttributeCheckBox(ref attributeTriggerLockCheckBox, !(_ReadOnly || !merchandiseAvailableCheckbox.Checked));
                            attributeTriggerLockCheckBox.CheckedChanged += new EventHandler(control_TextChanged);
                            AddAttributeToForm("Gun Lock", attributeTriggerLockCheckBox, false);
                            /* if (CDS.ActiveCustomer.Age == 0 && _CurrentContext != CurrentContext.PFI_REDESCRIBE)
                            {
                            if (_Item.MerchandiseType == "H")
                            {
                            MessageBox.Show("The age limit for selling or pawning a handgun is " + BusinessRulesProcedures.GetMinimumAgeHandGun(CDS.CurrentSiteId));
                            }
                            else if (_Item.MerchandiseType == "L")
                            {
                            MessageBox.Show("The age limit for selling or pawning a long gun is " + BusinessRulesProcedures.GetMinimumAgeLongGun(CDS.CurrentSiteId));
                            }
                            }*/
                        }

                        // FTN 3.3.3 Add Comment field after dynamic fields
                        TextBox attributeCommentTextBox = new TextBox();
                        attributeCommentTextBox.MaxLength = 30;

                        if (_CurrentContext != CurrentContext.NEW)
                        {
                            attributeCommentTextBox.Text = _Item.Comment;
                        }

                        attributeCommentTextBox.Name = "attribute_Comment_TextBox";
                        BuildOutAttributeTextBox(ref attributeCommentTextBox, new List<string> { _Item.Comment }, "", (_ReadOnly || !merchandiseAvailableCheckbox.Checked), true, false);
                        AddAttributeToForm("Comments", attributeCommentTextBox, false);

                        if (purchaseFlow || _CurrentContext == CurrentContext.PFI_ADD
                            || _CurrentContext == CurrentContext.PFI_MERGE
                            || _CurrentContext == CurrentContext.PFI_REDESCRIBE
                            || _CurrentContext == CurrentContext.PFI_REPLACE)
                        {
                            TextBox attributePfiTagTextBox = new TextBox();
                            attributePfiTagTextBox.MaxLength = 2;
                            attributePfiTagTextBox.Width = 20;

                            attributePfiTagTextBox.Name = "attribute_PfiTags_TextBox";
                            if ((_CurrentContext == CurrentContext.PFI_REDESCRIBE
                                || _CurrentContext == CurrentContext.PFI_ADD
                                 || _CurrentContext == CurrentContext.PFI_MERGE) && _Item.PfiTags == 0)
                            {
                                if (_Item.CaccLevel == 0)
                                {
                                    _Item.PfiTags = 0;
                                    attributePfiTagTextBox.Enabled = false;
                                }
                                else
                                {
                                    _Item.PfiTags = 1;
                                    attributePfiTagTextBox.Enabled = true;
                                }
                            }
                            if (purchaseFlow && _CurrentContext == CurrentContext.NEW)
                            {
                                if (vendorPurchaseFlow)
                                {
                                    if (_Item.CaccLevel == 0)
                                    {
                                        _Item.PfiTags = 0;
                                        attributePfiTagTextBox.Enabled = false;
                                    }
                                    else
                                    {
                                        _Item.PfiTags = 1;
                                        attributePfiTagTextBox.Enabled = true;
                                    }
                                }
                                else
                                {
                                    _Item.PfiTags = 1;
                                    attributePfiTagTextBox.Enabled = true;
                                }
                            }

                            BuildOutAttributeTextBox(ref attributePfiTagTextBox, new List<string> { _Item.PfiTags.ToString() }, "", (_ReadOnly || !merchandiseAvailableCheckbox.Checked), true, true);
                            AddAttributeToForm("Number of tags per item", attributePfiTagTextBox, false);
                        }
                        if (vendorPurchaseFlow && !_Item.IsGun)
                        {
                            CheckBox attributeExpenseItem = new CheckBox();
                            attributeExpenseItem.Name = "attribute_expense_chkbox";
                            //Madhu Veldanda 01/17/2011 fix to address BZ defect 78
                            attributeExpenseItem.CheckedChanged += new System.EventHandler(this.expenseCheckBox_Checked);
                            BuildOutAttributeCheckBox(ref attributeExpenseItem, true);
                            AddAttributeToForm("Expense Item", attributeExpenseItem, false);
                        }

                        // FTN 3.3.5 Add Loan Amount if exist
                        //SR 9/13/2010 The following is not needed for pfi merge
                        if (_CurrentContext != CurrentContext.PFI_MERGE)
                        {
                            if (_Item.ItemAmount >= 0)
                            {
                                if (attribute_loanAmount_TextBox != null)
                                {
                                    attribute_loanAmount_TextBox.Text = String.Format("{0:0.00}", _Item.ItemAmount);
                                }
                                if (attribute_costAmount_TextBox != null)
                                {
                                    attribute_costAmount_TextBox.Text = String.Format("{0:0.00}", _Item.ItemAmount);

                                    if (_Item.ItemAmount_Original != _Item.ItemAmount)
                                    {
                                        if (attribute_loanAmount_TextBox != null)
                                        {
                                            attribute_loanAmount_TextBox.Text = String.Format("{0:0.00}", _Item.ItemAmount_Original);
                                        }
                                        attribute_costAmount_TextBox.Text = String.Format("{0:0.00}", _Item.ItemAmount);
                                    }
                                }
                            }
                        }

                        if (_CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE)
                        {
                            linkLabelCurrentRetailPrice.Text = _Item.RetailPrice.ToString("c");
                            attribute_RetailPrice_TextBox.Text = String.Format("{0:0.00}", 0M);
                        }
                        else if (_Item.RetailPrice > 0)
                        {
                            //Fix for BZ # 239
                            attribute_RetailPrice_TextBox.Text = String.Format("{0:0.00}", _Item.RetailPrice);
                        }

                        // FTN 3.3.7.a Add Fixed Features List if exist
                        if (_Item.SelectedProKnowMatch != null)
                        {
                            if (_Item.SelectedProKnowMatch.fixedFeaturesList != null)
                            {
                                grpFeatures.Visible = _Item.SelectedProKnowMatch.fixedFeaturesList.Count > 0;

                                foreach (FixedFeature ffFeature in _Item.SelectedProKnowMatch.fixedFeaturesList)
                                {
                                    txtFeatures_List.Text += ffFeature.AnswerText + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException(ex.Message, new ApplicationException());
            }

            // Select first Attribute control for initial focus
            if (categoryAttributeTableLayoutPanel.Controls.Count > 0)
            {
                GenerateDynamicTicketDescription();
                categoryAttributeTableLayoutPanel.Controls[1].Select();
            }

            descriptonTextBox.Top = pnlAttributes.Location.Y + pnlAttributes.Height + 5;
            loanAmountLabel.Top = descriptonTextBox.Location.Y + descriptonTextBox.Height + 5;
            attribute_loanAmount_TextBox.Top = descriptonTextBox.Location.Y + descriptonTextBox.Height + 5;

            retailPriceLabel.Top = descriptonTextBox.Location.Y + descriptonTextBox.Height + 5;
            attribute_RetailPrice_TextBox.Top = descriptonTextBox.Location.Y + descriptonTextBox.Height + 5;

            if (_CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE)
            {
                costAmountLabel.Top = descriptonTextBox.Location.Y + descriptonTextBox.Height + 5;
                attribute_costAmount_TextBox.Top = descriptonTextBox.Location.Y + descriptonTextBox.Height + 5;
                attribute_RetailPrice_TextBox.Left = attribute_RetailPrice_TextBox.Location.X + 50;
            }

            // Business Rule [2,3,1]:  Hide button if not in Jewelry section  
            if (_CategoryMask.ToString().Substring(0, 1) == "1")
                editStonesButton.Visible = true;
            else
                editStonesButton.Visible = false;

            categoryAttributeTableLayoutPanel.Visible = true;
        }

        // Build the properties of the Attribute CheckBox Control
        private void BuildOutAttributeCheckBox(ref CheckBox attributeControl, bool bIsEnable)
        {
            attributeControl.Enabled = bIsEnable;
        }

        // Build the properties of the Attributed ComboBox
        private void BuildOutAttributeComboBox(ref ComboBox attributeControl, List<string> answerList, string sValidationType, int iOptionIdx, bool bIsReadOnly, bool bIsRestricted, bool bIsRequired)
        {
            //Add the answerList to the ComboBox Items Collection        
            foreach (string sAnswer in answerList)
            {
                attributeControl.Items.Add(sAnswer);
            }
            // Apply ComboBox Style based upon restriction flag
            attributeControl.DropDownStyle = bIsRestricted == true ? ComboBoxStyle.DropDownList : ComboBoxStyle.DropDown;
            // Apply the default selected index
            attributeControl.SelectedIndex = iOptionIdx;
            attributeControl.Enabled = !bIsReadOnly;
            // Apply appropriate event handlers to capture both types of ComboBoxes
            if (bIsRestricted)
            {
                if (bIsRequired)
                    attributeControl.SelectedIndexChanged += new EventHandler(controlDisableSubmit_TextChanged);
                else
                    attributeControl.SelectedIndexChanged += new EventHandler(control_TextChanged);
            }
            else
            {
                if (bIsRequired)
                    attributeControl.TextChanged += new EventHandler(controlDisableSubmit_TextChanged);
                else
                    attributeControl.TextChanged += new EventHandler(control_TextChanged);
            }
        }

        // Build the properties of the Attribute TextBox Control
        private void BuildOutAttributeTextBox(ref TextBox attributeControl, List<string> answerList, string sValidationType, bool bIsReadOnly, bool bIsDescriptive, bool bIsRequired)
        {
            attributeControl.Size = new Size(200, attributeControl.Size.Height);
            attributeControl.Text = answerList[0];
            attributeControl.ReadOnly = bIsReadOnly;
            if (bIsDescriptive)
            {
                // Apply event handler to capture text changes to validate user entries
                if (bIsRequired)
                    attributeControl.TextChanged += new EventHandler(controlDisableSubmit_TextChanged);
                else
                    attributeControl.TextChanged += new EventHandler(control_TextChanged);
            }
        }

        // FTN 3.3.a.ii.2 Format the answer to correct Validation Type format
        private string BuildOutValidationTypeFormat(string sAnswer, string sValidationType)
        {
            if (sAnswer == string.Empty)
                return "";

            switch (sValidationType)
            {
                case "":
                case "S":
                    return sAnswer;
                case "A":
                    return String.Format("{0:C}", Convert.ToDecimal(sAnswer));
                case "F":
                    return String.Format("{0:0.00}", Convert.ToDecimal(sAnswer));
                case "N":
                    return String.Format("{0:0}", Convert.ToInt32(sAnswer));
                case "D":
                    return String.Format("{0:d}", Convert.ToDecimal(sAnswer));
                default:
                    return sAnswer;
            }
        }

        // Add the Attribute Control to the Form
        private void AddAttributeToForm(string sDescriptionLabel, Control attributeControl, bool bIsRequired)
        {
            Padding pad = new Padding(0);
            // FTN 3.3.a.ii.1.a Create Description Label Control using Item Attribute Description and ":"
            Label descriptionLabel = new Label
            {
                Padding = pad,
                Text = sDescriptionLabel + ":",
                AutoSize = true
            };

            attributeControl.Padding = pad;

            // Add Event Handlers for mouse overs and outs
            if (attributeControl.GetType() != typeof(CheckBox))
            {
                attributeControl.Enter += new EventHandler(control_Enter);
                attributeControl.Leave += new EventHandler(control_Leave);
            }

            // If required, add some cosmetics to control background, add to Required Attributes List
            Label asteriskLabel = new Label();
            asteriskLabel.ForeColor = Color.Red;
            asteriskLabel.Text = string.Empty;
            asteriskLabel.TextAlign = ContentAlignment.BottomCenter;
            asteriskLabel.Width = 10;

            if (bIsRequired)
            {
                asteriskLabel.Font = new Font(this.Font, FontStyle.Bold);
                asteriskLabel.Text = "*";
                //       attributeControl.BackColor  = Color.FromArgb(255, 255, 128);
                attributeControl.Tag = attributeControl.BackColor;
                _RequiredAttributes.Add(attributeControl.Name);
            }
            else
            {
                attributeControl.Tag = Color.LightGray;
            }

            // Add Controls to Form
            categoryAttributeTableLayoutPanel.Controls.Add(descriptionLabel);
            categoryAttributeTableLayoutPanel.Controls.Add(attributeControl);
            categoryAttributeTableLayoutPanel.Controls.Add(asteriskLabel);
            // Add a default border color to initiate control focus draw object
            Utilities.BorderColor(categoryAttributeTableLayoutPanel, attributeControl, Color.White);
        }

        // Common handler to assign not to use Hot Number Keys when in certain Controls
        private void control_Enter(object sender, EventArgs e)
        {
            Control tmpControl = (Control)sender;
            Utilities.BorderColor(tmpControl.Parent, tmpControl, Color.Blue);
            HighlightIfZero(tmpControl);
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            Control tmpControl = (Control)sender;
            HighlightIfZero(tmpControl);
        }

        private void HighlightIfZero(Control tmpControl)
        {
            if (tmpControl.Name == "attribute_loanAmount_TextBox")
            {
                attribute_loanAmount_TextBox.SelectionStart = 0;
                attribute_loanAmount_TextBox.SelectionLength = attribute_loanAmount_TextBox.Text.Length;
            }
            else if (tmpControl.Name == "attribute_costAmount_TextBox")
            {
                attribute_costAmount_TextBox.SelectionStart = 0;
                attribute_costAmount_TextBox.SelectionLength = attribute_costAmount_TextBox.Text.Length;

            }
            else if (tmpControl.Name == "attribute_RetailPrice_TextBox")
            {
                attribute_RetailPrice_TextBox.SelectionStart = 0;
                attribute_RetailPrice_TextBox.SelectionLength = attribute_RetailPrice_TextBox.Text.Length;
            }
        }
        //Madhu Veldanda 01/17/2011 fix to address BZ defect 78
        //to disable/enable the expense textbox based on the user selection
        private void expenseCheckBox_Checked(object sender, EventArgs e)
        {
            Control tmpControl = (Control)sender;
            if (((CheckBox)tmpControl).Checked)
            {
                attribute_RetailPrice_TextBox.Text = String.Format("{0:0.00}", 0);
                attribute_RetailPrice_TextBox.Enabled = false;
            }
            else
                attribute_RetailPrice_TextBox.Enabled = true;
        }

        // Common handler to reset to use Hot Number Keys when exiting certain Controls
        private void control_Leave(object sender, EventArgs e)
        {
            Control tmpControl = (Control)sender;
            Utilities.BorderColor(tmpControl.Parent, tmpControl, Color.White);

            if (ValidateDataType(tmpControl, true))
            {
                GenerateDynamicTicketDescription();

                if (tmpControl.Name == "attribute_loanAmount_TextBox"
                    || tmpControl.Name == "attribute_costAmount_TextBox")
                {
                    if (!string.IsNullOrEmpty(tmpControl.Text))
                    {
                        tmpControl.Text = String.Format("{0:0.00}", Convert.ToDecimal(tmpControl.Text));
                    }
                }
                if (tmpControl.Name == "attribute_RetailPrice_TextBox")
                {
                    if (!string.IsNullOrEmpty(tmpControl.Text))
                    {
                        tmpControl.Text = String.Format("{0:0.00}", Convert.ToDecimal(tmpControl.Text));
                    }
                }
            }
            else
            {
                //Madhu Veldanda 01/17/2011 fix to address BZ defect 78
                if (tmpControl.Name == "attribute_loanAmount_TextBox"
                    || tmpControl.Name == "attribute_costAmount_TextBox"
                    || tmpControl.Name == "attribute_RetailPrice_TextBox")
                {
                    //tmpControl.Text = string.Empty;
                    tmpControl.Text = tmpControl.Text = String.Format("{0:0.00}", 0);
                }
                //
            }

            bool bEnable = _RequiredAttributes.Count == 0 || RequiredAttributesCollected();
            if (_Item.CaccLevel == 0)
                submitButton.Enabled = bEnable;
            else
                getValuesButton.Enabled = bEnable;
            editStonesButton.Enabled = bEnable;
        }

        private void control_TextChanged(object sender, EventArgs e)
        {
            Control tmpControl = (Control)sender;

            if (tmpControl.GetType().Name == "TextBox")
            {
                int selStart = ((TextBox)tmpControl).SelectionStart;
                tmpControl.Text = tmpControl.Text.ToUpper();
                ((TextBox)tmpControl).SelectionStart = selStart; //code put in place by Drew to check textbox cursor issues
            }

            //if (ValidateDataType(tmpControl, true))
            if (ValidateDataType(tmpControl, false))
            {
                GenerateDynamicTicketDescription();
            }
        }

        private void controlDisableSubmit_TextChanged(object sender, EventArgs e)
        {
            Control tmpControl = (Control)sender;
            submitButton.Enabled = false;

            if (tmpControl.GetType().Name == "TextBox")
            {
                int selStart = ((TextBox)tmpControl).SelectionStart;
                tmpControl.Text = tmpControl.Text.ToUpper();
                ((TextBox)tmpControl).SelectionStart = selStart; //code put in place by Drew to check textbox cursor issues
            }

            if (ValidateDataType(tmpControl, false))
            {
                GenerateDynamicTicketDescription();
            }
        }

        private bool RequiredAttributesCollected()
        {
            bool bReady = false;
            foreach (string sAttributeControlName in _RequiredAttributes)
            {
                Control attributeControl = categoryAttributeTableLayoutPanel.Controls[sAttributeControlName];
                if (attributeControl != null)
                {
                    if (attributeControl.GetType() == typeof(TextBox) || attributeControl.GetType() == typeof(ComboBox))
                    {
                        bReady = ValidateDataType(attributeControl, false);
                        if (!bReady)
                            break;
                    }
                }
            }
            return bReady;
        }

        // FTN 3.3.a.ii.2 Validate data in Control is what was expected
        private bool ValidateDataType(Control validateControl, bool bProvideValidationAlert)
        {
            bool bValidated = false;
            bool bPerformUpdate = true;
            Type typeControl = validateControl.GetType();
            string sValueOfControl = string.Empty;
            string[] sControlData = validateControl.Name.Split('_');

            switch (typeControl.Name)
            {
                case "CheckBox":
                    sValueOfControl = ((CheckBox)validateControl).Checked.ToString();
                    bPerformUpdate = ((CheckBox)validateControl).Enabled;
                    break;
                case "ComboBox":
                    ComboBox tmpComboBox = ((ComboBox)validateControl);
                    sValueOfControl = tmpComboBox.DropDownStyle == ComboBoxStyle.DropDownList ? tmpComboBox.SelectedItem.ToString().ToUpper() : tmpComboBox.Text.ToUpper();
                    bPerformUpdate = ((ComboBox)validateControl).Enabled;
                    break;
                case "MaskedTextBox":
                    sValueOfControl = ((MaskedTextBox)validateControl).Text.ToUpper();
                    bPerformUpdate = !((MaskedTextBox)validateControl).ReadOnly;
                    break;
                case "TextBox":
                    sValueOfControl = ((TextBox)validateControl).Text.ToUpper();
                    bPerformUpdate = !((TextBox)validateControl).ReadOnly;
                    break;
            }

            if (!bPerformUpdate)
                return true;

            if (_Item != null)
            {
                if (sControlData[1] == "Comment")
                {
                    _Item.Comment = sValueOfControl;
                    return true;
                }
                if (sControlData[1] == "loanAmount")
                {
                    if (StringUtilities.IsDecimal(sValueOfControl))
                    {
                        decimal dLoanAmount = Convert.ToDecimal(sValueOfControl);
                        _Item.ItemAmount = dLoanAmount;
                        return true;
                    }
                    else
                    {
                        if (StringUtilities.isNotEmpty(sValueOfControl))
                            attribute_loanAmount_TextBox.Text = sValueOfControl.Substring(0, sValueOfControl.Length - 1);
                        int selStart = attribute_loanAmount_TextBox.SelectionStart;
                        attribute_loanAmount_TextBox.SelectionStart = selStart; //code put in place by Drew to check textbox cursor issues
                        return false;
                    }
                }
                if (sControlData[1] == "costAmount")
                {
                    if (StringUtilities.IsDecimal(sValueOfControl))
                    {
                        decimal dLoanAmount = Convert.ToDecimal(sValueOfControl);
                        _Item.ItemAmount = dLoanAmount;
                        return true;
                    }
                    else
                    {
                        if (StringUtilities.isNotEmpty(sValueOfControl))
                            attribute_costAmount_TextBox.Text = sValueOfControl.Substring(0, sValueOfControl.Length - 1);
                        int selStart = attribute_costAmount_TextBox.SelectionStart;
                        attribute_costAmount_TextBox.SelectionStart = selStart;//code put in place by Drew to check textbox cursor issues
                        return false;
                    }
                }
                if (sControlData[1] == "RetailPrice")
                {
                    if (StringUtilities.IsDecimal(sValueOfControl))
                    {
                        decimal dRetailPrice = Convert.ToDecimal(sValueOfControl);
                        _Item.RetailPrice = dRetailPrice;
                        return true;
                    }
                    else
                    {
                        if (StringUtilities.isNotEmpty(sValueOfControl))
                            attribute_RetailPrice_TextBox.Text = sValueOfControl.Substring(0, sValueOfControl.Length - 1);
                        int selStart = attribute_RetailPrice_TextBox.SelectionStart;
                        attribute_RetailPrice_TextBox.SelectionStart = selStart;//code put in place by Drew to check textbox cursor issues
                        return false;
                    }
                }
                if (sControlData[1] == "PfiTags")
                {
                    int iValue = Utilities.GetIntegerValue(sValueOfControl, 1);
                    _Item.PfiTags = iValue;
                    return true;
                }

                if (sControlData[1] == "GunLock")
                {
                    _Item.HasGunLock = Convert.ToBoolean(sValueOfControl);
                    return true;
                }
                if (sControlData[1] == "8")
                {
                    ComboBox karatComboBox = (ComboBox)categoryAttributeTableLayoutPanel.Controls["attribute_9_ComboBox"];
                    if (karatComboBox != null)
                    {
                        if (sValueOfControl == "PLATINUM")
                        {
                            karatComboBox.SelectedItem = "18";
                            karatComboBox.Enabled = false;
                        }
                        else
                        {
                            karatComboBox.Enabled = true;
                        }
                    }
                }
                //If its the quantity field added for vendor purchases
                if (sControlData[1] == "qnty")
                {
                    int qty = Utilities.GetIntegerValue(((TextBox)categoryAttributeTableLayoutPanel.Controls["attribute_qnty_TextBox"]).Text, 0);
                    if (qty == 0)
                    {
                        MessageBox.Show("Quantity cannot be 0.", "Data Type validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        validateControl.Focus();
                        return false;
                    }
                    else
                        _Item.Quantity = qty;
                    return true;
                }

                ItemAttribute attributeFind = _Item.Attributes.Find(
                    att => (att.AttributeCode == Convert.ToInt32(sControlData[1])));

                string sValidationDataType = attributeFind.ValidationDataType.ToString();
                sValidationDataType = string.IsNullOrEmpty(sValidationDataType) ? attributeFind.InputType : sValidationDataType;

                if (attributeFind.IsRequired && !ValidateDataTypeValue(sValidationDataType, sValueOfControl))
                {
                    // FTN 3.3.b.ii.2.a
                    // FTN 3.3.b.iii.1.a
                    if (bProvideValidationAlert && !string.IsNullOrEmpty(sValueOfControl))
                    {
                        MessageBox.Show("Value entered is invalid data type.  Re-enter.", "Data Type validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        validateControl.Focus();
                        bProvideValidationAlert = false;
                    }
                    return bValidated;
                }

                int iAnswerIndex = _Item.Attributes.FindIndex(delegate(ItemAttribute att)
                {
                    return (att.AttributeCode == Convert.ToInt32(sControlData[1]));
                });

                Answer updatedAnswer = _Item.Attributes[iAnswerIndex].Answer;

                // FTN 3.3.b.ii.1 Set ItemAttribute.Answer to answerList object
                switch (typeControl.Name)
                {
                    case "ComboBox":
                        ComboBox tmpComboBox = ((ComboBox)validateControl);
                        if (tmpComboBox.DropDownStyle == ComboBoxStyle.DropDownList)  // COMBOBOX_ONLY
                        {
                            Answer findAnswer = attributeFind.AnswerList.Find(delegate(Answer ans)
                            {
                                return (ans.AnswerText == sValueOfControl);
                            });
                            // FTN 3.3.b.i.1
                            updatedAnswer = findAnswer;
                        }
                        else
                        {
                            // FTN 3.3.b.ii.2.b.i
                            int iComboBoxValue_Idx = attributeFind.AnswerList.FindIndex(delegate(Answer ans)
                            {
                                return (ans.AnswerText == sValueOfControl);
                            });
                            // FTN 3.3.b.ii.1 && FTN 3.3.b.ii.2.b.ii.1
                            if (iComboBoxValue_Idx >= 0)
                            {
                                updatedAnswer = attributeFind.AnswerList.ElementAt(iComboBoxValue_Idx);
                            }
                            else
                            {
                                // FTN 3.3.b.ii.2.b.iii
                                Answer newAnswer = _Item.Attributes[iAnswerIndex].Answer;
                                newAnswer.AnswerCode = string.IsNullOrEmpty(sValueOfControl) ? 0 : 999;
                                newAnswer.AnswerText = string.IsNullOrEmpty(sValueOfControl) ? string.Empty : sValueOfControl;
                                newAnswer.InputKey = null;
                                newAnswer.OutputKey = null;
                                updatedAnswer = newAnswer;
                            }
                        }
                        break;
                    case "MaskedTextBox":
                    case "TextBox":
                        // FTN 3.3.b.iii.b.i
                        int iTextBoxValue_Idx = attributeFind.AnswerList.FindIndex(delegate(Answer ans)
                        {
                            return (ans.AnswerText == sValueOfControl);
                        });
                        // FTN 3.3.b.iii.b.ii.1
                        if (iTextBoxValue_Idx >= 0)
                        {
                            attributeFind.Answer = attributeFind.AnswerList.ElementAt(iTextBoxValue_Idx);
                        }
                        else
                        {
                            // FTN 3.3.b.iii
                            Answer newAnswer = new Answer();
                            newAnswer.AnswerCode = string.IsNullOrEmpty(sValueOfControl) ? 0 : 999;
                            newAnswer.AnswerText = string.IsNullOrEmpty(sValueOfControl) ? string.Empty : sValueOfControl;
                            newAnswer.InputKey = null;
                            newAnswer.OutputKey = null;
                            updatedAnswer = newAnswer;
                        }
                        break;
                }

                // Update the change to the List<ItemAttribute>
                attributeFind.Answer = updatedAnswer;
                _Item.Attributes.RemoveAt(iAnswerIndex);
                if (_Item.Attributes.Count == 0)
                    _Item.Attributes.Add(attributeFind);
                else
                    _Item.Attributes.Insert(iAnswerIndex, attributeFind);

                bValidated = true;
            }
            return bValidated;
        }

        private void textValue_FormatCurrency(object sender, ConvertEventArgs e)
        {
            // The method converts only to string type. Test this using the DesiredType. 
            if (e.DesiredType != typeof(string))
                return;

            // Use the ToString method to format the value as currency ("c"). 
            e.Value = ((double)e.Value).ToString("c");
        }

        // Method to validate the data type entered by customer to what was expected
        private bool ValidateDataTypeValue(string sValidationType, string sValueControl)
        {
            switch (sValidationType)
            {
                case "":
                case "S":
                    return StringUtilities.isNotEmpty(sValueControl);
                case "A":
                    try
                    {
                        string tmpValueControl = sValueControl.Replace(" ", "");
                        if (tmpValueControl.IndexOf(@"/") >= 0)
                        {
                            if (tmpValueControl.IndexOf("-") >= 0)
                            {
                                string[] tokens = tmpValueControl.Split('-');
                                tmpValueControl = tokens[1];
                            }
                            string[] sFractional = tmpValueControl.Split('/');
                            tmpValueControl = String.Format("{0:0.00}", (Convert.ToDecimal(sFractional[0]) / Convert.ToDecimal(sFractional[1])));
                        }
                        // Since we are dealing with Amounts, check length after decimal is 2 placeholders
                        decimal dValueControl = decimal.Parse(tmpValueControl);
                        return dValueControl >= 0 ? true : false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                case "F":
                    return StringUtilities.IsFloat(sValueControl);
                case "N":
                    return StringUtilities.IsInteger(sValueControl);
                case "D":
                    return StringUtilities.IsDecimal(sValueControl);
                default:
                    return false;
            }
        }

        // FTN 3.3.a.ii.4 Dynamic Ticket Description updated when control loses focus
        public int GenerateDynamicTicketDescriptionLength()
        {
            GenerateDynamicTicketDescription();
            return descriptonTextBox.Text.Length;
        }

        public void GenerateDynamicTicketDescription()
        {
            this.ShowJewelryDisplay();
            foreach (Control attributeControl in categoryAttributeTableLayoutPanel.Controls)
            {
                if (attributeControl.GetType() == typeof(TextBox) || attributeControl.GetType() == typeof(ComboBox))
                {
                    ValidateDataType(attributeControl, false);
                }
            }

            // FTN 3.3.a.ii.4.a.i
            _Item.Attributes.Sort(delegate(ItemAttribute IA_1, ItemAttribute IA_2)
            {
                return IA_1.DescriptionOrder.CompareTo(IA_2.DescriptionOrder);
            });

            string sItemNumberPrefix = string.Empty;
            string sDescriptionLabel = string.Empty;

            Item.GenerateTicketDescription(ref _Item, out sItemNumberPrefix, out sDescriptionLabel);

            if (!string.IsNullOrEmpty(sDescriptionLabel))// && !_ReadOnly)
            {
                descriptonTextBox.Text = sItemNumberPrefix + sDescriptionLabel;
            }
            else //if (string.IsNullOrEmpty(sDescriptionLabel) && _ReadOnly)
            {
                descriptonTextBox.Text = _Item.TicketDescription;
            }
        }

        private void getValuesButton_Click(object sender, EventArgs e)
        {
            //getValuesToShow();
            getValuePrep();

            if (_Item.IsJewelry)
                getJewelryValuesToShow();
            else
                getValuesToShow();
        }

        private void GetProknow()
        {
            ProKnowDetails proKnowDetails = new ProKnowDetails(DesktopSession);

            ProKnowDetails.ProKnowLookupAction proKnowLookupAction;
            proKnowLookupAction = ProKnowDetails.ProKnowLookupAction.DESCRIBE_ITEM_DOUBLE;

            manModelReplyType returnedManModelReplyType;

            proKnowDetails.GetProKnowDetails(_Item.Attributes[1].Answer.AnswerText
                                             , _Item.Attributes[2].Answer.AnswerText
                                             , out returnedManModelReplyType
                                             , out proKnowLookupAction);

            if (returnedManModelReplyType.serviceData != null && returnedManModelReplyType.serviceData.Items[0].GetType().Name != "businessExceptionType")
            {
                string level = string.Empty;
                ProKnowMatch proknowMatch = _Item.SelectedProKnowMatch;

                manModelMatchType foundManModelMatchType = (manModelMatchType)returnedManModelReplyType.serviceData.Items[0];
                proKnowDetails.ParseProKnowDetails(ref proknowMatch, ref level, foundManModelMatchType, ActiveManufacturer.PRIMARY, out proKnowLookupAction);

                _Item.SelectedProKnowMatch = proknowMatch;
            }
        }

        private void getValuePrep()
        {
            string sProKnowPhoneNumber = "1-800-645-3808";
            string sComponentValue = string.Empty;

            BusinessRuleVO brPHONE_NUMBER = GetBusinessRule("PWN_BR-000");
            if (brPHONE_NUMBER == null)
            {
                BasicExceptionHandler.Instance.AddException("Cannot find PWN_BR-000", new ApplicationException("DescribeItem.getValuesButton_Click: Cannot find PWN_BR-000"));
                return;
            }

            if (brPHONE_NUMBER.getComponentValue("PK_INFO_PHONE", ref sComponentValue))
                sProKnowPhoneNumber = sComponentValue.ToString();

            submitButton.Enabled = true;

            HideGroups();

            grpAdditional.Visible = true;
            lblAdditional_Phone.Text = String.Format("For more information call Pro-Know at {0}", sProKnowPhoneNumber);

            _Item.TotalLoanGoldValue = 0;
            _Item.TotalLoanStoneValue = 0;
        }

        private void getJewelryValuesToShow()
        {
            decimal dTotalLoanValue = 0;
            decimal dTotalRetailValue = 0;
            List<string> EXCLUDED_WATCHES;

            //Show jewelry display
            //this.ShowJewelryDisplay();

            // FTN 3.3.e.ii.1.a Format the answer to correct Validation Type format
            if (_Item.CategoryCode == 1520 || _Item.CategoryCode == 1540)
            {
                // Call PWN_BR-64 to retrieve Excluded_Watches
                BusinessRuleVO brEXLUDED_WATCHES = GetBusinessRule("PWN_BR-064");
                int iExcludedWatchIdx = -1;
                string sComponentValue = string.Empty;

                bool bFoundBusinessRule = brEXLUDED_WATCHES.getComponentValue("EXCLUDED_WATCHES", ref sComponentValue);

                if (bFoundBusinessRule)
                {
                    // What logic to convert sComponentValue to List?
                    EXCLUDED_WATCHES = sComponentValue.Split('|').ToList();
                    //foreach (string sTemp in sComponentValue.Split('|'))
                    //{
                    //    EXCLUDED_WATCHES.Add(sTemp);
                    //}
                    //iExcludedWatchIdx = EXCLUDED_WATCHES.FindIndex(delegate(string s)
                    //{
                    //    return s == _Item.SelectedProKnowMatch.manufacturerModelInfo[0].AnswerText;
                    //});
                    if (this._Item.SelectedProKnowMatch != null)
                    {
                        iExcludedWatchIdx = EXCLUDED_WATCHES.FindIndex(s => s == this._Item.SelectedProKnowMatch.manufacturerModelInfo[0].AnswerText);
                    }
                }

                if (iExcludedWatchIdx >= 0)
                {
                    if (!_ReadOnly)
                    {
                        grpAdditional.Visible = true;
                    }
                }
                else
                {
                    MetalFlow(ref dTotalLoanValue, ref dTotalRetailValue);
                    StonesFlow(ref dTotalLoanValue, ref dTotalRetailValue);
                }
            }
            else
            {
                // FTN 3.3.e.ii.2 Determine if Jewelry is Metal
                //int iMetalIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
                //{
                //    return ia.Description == "Type Of Metal";
                //});
                int iMetalIdx = _Item.Attributes.FindIndex(ia => ia.Description == "Type Of Metal");

                if (iMetalIdx >= 0)
                {
                    MetalFlow(ref dTotalLoanValue, ref dTotalRetailValue);
                    StonesFlow(ref dTotalLoanValue, ref dTotalRetailValue);
                }
                else
                    StonesFlow(ref dTotalLoanValue, ref dTotalRetailValue);
            }

            // Perform Variance Lookups
            //CDS.VarianceRates.Sort(delegate(Variance variance_1, Variance variance_2)
            //{
            //    return variance_1.MaxAmount.CompareTo(variance_2.MaxAmount);
            //});
            DesktopSession.VarianceRates.Sort((Variance variance1, Variance variance2) => variance1.MaxAmount.CompareTo(variance2.MaxAmount));

            int iVarianceIdx = DesktopSession.VarianceRates.FindIndex(v => v.DocType == 'R' && v.MaxAmount > dTotalRetailValue);

            if (iVarianceIdx >= 0 && dTotalRetailValue > 0)
            {
                decimal dVarianceMinAmount = DesktopSession.VarianceRates[iVarianceIdx].MinAmount;
                decimal dVariancePercent = DesktopSession.VarianceRates[iVarianceIdx].Percent;

                decimal dTempDiv = Math.Floor(dTotalRetailValue / dVarianceMinAmount);
                int dTempVarInt = Convert.ToInt32(dTempDiv);
                decimal dTempVar = dTempVarInt * dVarianceMinAmount;

                dTotalRetailValue = dTempVar + dVariancePercent;
            }

            if (dTotalLoanValue > 0 || dTotalRetailValue > 0)
            {
                if (dTotalLoanValue > 0)
                {
                    grpLoanValues.Visible = true;
                    lblLoanValues_SuggestedLoanValue.Text = String.Format("{0:C}", Utilities.GetDoubleValue(dTotalLoanValue));
                    _ProKnow_SuggestedLoanAmount = dTotalLoanValue;
                }
                if (dTotalRetailValue > 0)
                {
                    grpRetailValue.Visible = true;
                    lblRetail_SuggestedRetailValue.Text = String.Format("{0:C}", Utilities.GetDoubleValue(dTotalRetailValue));
                }

                ProKnowData jewelryProKnow = new ProKnowData();
                jewelryProKnow.LoanAmount = dTotalLoanValue;
                jewelryProKnow.RetailAmount = dTotalRetailValue;
                if (_Item.SelectedProKnowMatch != null)
                {
                    _Item.SelectedProKnowMatch.selectedPKData = jewelryProKnow;
                }
            }
            else
            {
                grpAdditional.Visible = true;
            }

            //Madhu to address BZ # 27
            /*
            if (grpLoanValues.Visible)
            {
            string sLoanValueToolTip = "Total Loan Gold Value:  " + String.Format("{0:C}", _Item.TotalLoanGoldValue);
            sLoanValueToolTip += Environment.NewLine;
            sLoanValueToolTip += "Total Loan Stone Value:  " + String.Format("{0:C}", _Item.TotalLoanStoneValue);
            toolTip1.SetToolTip(grpLoanValues, sLoanValueToolTip);
            }*/

            GroupLocation();
        }

        private void getValuesToShow()
        {
            //if read only 
            if ((_ReadOnly) && _Item.SelectedProKnowMatch != null)
            {
                grpLoanValues.Visible = true;

                lblLoanValues_SuggestedLoanValue.Text = _Item.SelectedProKnowMatch.selectedPKData.RetailAmount == 0 ?
                                                        "N/A" : String.Format("{0:C}", Utilities.GetDoubleValue(_Item.SelectedProKnowMatch.selectedPKData.RetailAmount));

                grpAdditional.Visible = true;
                lblAdditional_LastUpdatedValue.Text = string.IsNullOrEmpty(_Item.SelectedProKnowMatch.proCallData.LastUpdateDate) ?
                                                      "N/A" : _Item.SelectedProKnowMatch.proCallData.LastUpdateDate;

                lblAdditional_IntroducedValue.Text = string.IsNullOrEmpty(_Item.SelectedProKnowMatch.proCallData.YearIntroduced) ?
                                                     "N/A" : _Item.SelectedProKnowMatch.proCallData.YearIntroduced;
            }
            else if (_Item.SelectedProKnowMatch != null)
            {
                if (_Item.SelectedProKnowMatch.transitionStatus == TransitionStatus.MAN_MODEL_PROKNOW_COMBO || _Item.SelectedProKnowMatch.transitionStatus == TransitionStatus.MAN_MODEL_PROKNOW)
                {
                    if (_CurrentContext == CurrentContext.PFI_REDESCRIBE && _Item.Attributes != null)
                    {
                        GetProknow();
                    }

                    int iOrderNumber = 0;
                    foreach (Control formControl in categoryAttributeTableLayoutPanel.Controls)
                    {
                        if (formControl.GetType().Name == "ComboBox")
                        {
                            if (formControl.Name.StartsWith("attribute_1023"))
                            {
                                iOrderNumber = ((ComboBox)formControl).SelectedIndex + 1;
                                break;
                            }
                        }
                    }

                    if (_Item.SelectedProKnowMatch.proKnowData != null && iOrderNumber > 0)
                    {
                        iOrderNumber = _Item.SelectedProKnowMatch.proKnowData.FindIndex(pkdSearch => pkdSearch.ConditionLevel == iOrderNumber);

                        if (iOrderNumber >= 0)
                        {
                            _Item.SelectedProKnowMatch.selectedPKData = _Item.SelectedProKnowMatch.proKnowData[iOrderNumber];

                            if (_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].LoanAmount != 0)
                            {
                                grpLoanValues.Visible = true;
                                lblLoanValues_SuggestedLoanValue.Text = String.Format("{0:C}", Utilities.GetDoubleValue(_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].LoanAmount));

                                _ProKnow_SuggestedLoanAmount_NonJewelry = _Item.SelectedProKnowMatch.proKnowData[iOrderNumber].LoanAmount;
                            }
                            if (_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].RetailAmount != 0)
                            {
                                grpRetailValue.Visible = true;
                                lblRetail_SuggestedRetailValue.Text = String.Format("{0:C}", Utilities.GetDoubleValue(_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].RetailAmount));
                            }
                            if (_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].LoanVarHighAmount != 0)
                            {
                                grpLoanValues.Visible = true;
                                lblLoanValues_LoanValueHighValue.Text = String.Format("{0:C}", Utilities.GetDoubleValue(_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].LoanVarHighAmount));
                            }
                            if (_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].LoanVarLowAmount != 0)
                            {
                                grpLoanValues.Visible = true;
                                lblLoanValues_LoanValueLowValue.Text = String.Format("{0:C}", Utilities.GetDoubleValue(_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].LoanVarLowAmount));
                            }
                            if (_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].PurchaseAmount != 0)
                            {
                                grpAdditional.Visible = true;
                                lblAdditional_PurchaseValueValue.Text = String.Format("{0:C}", Utilities.GetDoubleValue(_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].PurchaseAmount));
                            }

                            if (!string.IsNullOrEmpty(_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].RetailVarHighRetailer))
                            {
                                grpRetailValue.Visible = true;
                                lblRetail_Retailer1Label.Text = _Item.SelectedProKnowMatch.proKnowData[iOrderNumber].RetailVarHighRetailer;
                                lblRetail_Retailer1Value.Text = String.Format("{0:C}", Utilities.GetDoubleValue(_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].RetailVarHighAmount));
                            }
                            if (!string.IsNullOrEmpty(_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].RetailVarLowRetailer))
                            {
                                grpRetailValue.Visible = true;
                                lblRetail_Retailer2Label.Text = _Item.SelectedProKnowMatch.proKnowData[iOrderNumber].RetailVarLowRetailer;
                                lblRetail_Retailer2Value.Text = String.Format("{0:C}", Utilities.GetDoubleValue(_Item.SelectedProKnowMatch.proKnowData[iOrderNumber].RetailVarLowAmount));
                            }
                        }


                    }
                    grpRetailValue.Visible = true;
                    lblRetail_TodaysRetailValue.Text = String.Format("{0:C}", _Item.SelectedProKnowMatch.proCallData.NewRetail);

                    if (!string.IsNullOrEmpty(_Item.SelectedProKnowMatch.proCallData.LastUpdateDate))
                    {
                        grpAdditional.Visible = true;
                        lblAdditional_LastUpdatedValue.Text = _Item.SelectedProKnowMatch.proCallData.LastUpdateDate;
                    }
                    if (!string.IsNullOrEmpty(_Item.SelectedProKnowMatch.proCallData.YearIntroduced))
                    {
                        grpAdditional.Visible = true;
                        lblAdditional_IntroducedValue.Text = _Item.SelectedProKnowMatch.proCallData.YearIntroduced;
                    }

                }
            }

            GroupLocation();
        }

        private void MetalFlow(ref decimal dTotalLoanValue, ref decimal dTotalRetailValue)
        {
            bool bPawnValue = false;
            decimal dAdjustedGrams = 0;
            decimal dApproximateKarat = 0;
            decimal dApproximateGrams = 0;
            decimal dLoanAmountPerGram = 0;
            decimal dRetailAmountPerGram = 0;
            string sClass = string.Empty;
            string sAnswerId = String.Empty;
            string sComponentValue = string.Empty;
            string sMetalType = string.Empty;

            //this.ShowJewelryDisplay();

            int iMetalIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
            {
                return ia.Description == "Type Of Metal";
            });
            if (iMetalIdx >= 0)
            {
                ItemAttribute iaClass = _Item.Attributes.Find(delegate(ItemAttribute ia)
                {
                    return ia.Description == "Class";
                });

                // Databases have variance on Metal Class.  Manipulation time to synch up
                // *** BEGIN ***
                sClass = iaClass.Answer.AnswerText.ToUpper();
                sAnswerId = iaClass.Answer.AnswerCode.ToString();
                int iClassHypenIdx = sClass.IndexOf("-");
                if (iClassHypenIdx >= 0)
                {
                    sClass = sClass.Substring(0, iClassHypenIdx);
                    sClass = sClass.Replace(" ", "");
                }
                // ***  END  ***

                ItemAttribute iaApproximateKarat = _Item.Attributes.Find(delegate(ItemAttribute ia)
                {
                    return ia.Description == "Approx. Karat";
                });

                dApproximateKarat = Convert.ToDecimal(iaApproximateKarat.Answer.AnswerText);

                ItemAttribute iaApproximateGrams = _Item.Attributes.Find(delegate(ItemAttribute ia)
                {
                    return ia.Description == "Approx. Grams";
                });

                dApproximateGrams = Utilities.GetDecimalValue(iaApproximateGrams.Answer.AnswerText, 0);

                // Call PWN_BR-61 for Metal Type value
                BusinessRuleVO brMETAL_TYPES = GetBusinessRule("PWN_BR-061");
                bPawnValue = brMETAL_TYPES.getComponentValue("GOLD_TYPES", ref sComponentValue);
                if (bPawnValue)
                {
                    List<string> listGoldTypes = new List<string>();
                    listGoldTypes.AddRange(sComponentValue.Split('|'));

                    if (listGoldTypes.FindIndex(delegate(string s)
                    {
                        return s == _Item.Attributes[iMetalIdx].Answer.AnswerText;
                    }) >= 0)
                    {
                        sMetalType = "GOLD";
                    }
                }

                if (sMetalType == string.Empty)
                {
                    bPawnValue = brMETAL_TYPES.getComponentValue("OTHER_TYPES", ref sComponentValue);
                    if (bPawnValue)
                    {
                        List<string> listOtherTypes = new List<string>();
                        listOtherTypes.AddRange(sComponentValue.Split('|'));

                        var index = listOtherTypes.FindIndex(
                            s => s == _Item.Attributes[iMetalIdx].Answer.AnswerText);
                        if (index >= 0)
                        {
                            sMetalType = listOtherTypes[index];
                        }
                    }
                }

                if (!bPawnValue || sMetalType == string.Empty)
                {
                    dTotalLoanValue = 0.00M;
                    dTotalRetailValue = 0.00M;
                    return;
                }

                // check to see if we are dealing with a POOR|SCRAP|COMMIDITY class
                if (sAnswerId.Equals(DesktopSession.ScrapAnswerId))
                {
                    foreach (string s in DesktopSession.ScrapTypes)
                    {
                        if (sClass.Equals(s))
                        {
                            sClass = SCRAP_STRING;
                            break;
                        }
                    }
                }

                int iMetalInfoIdx = DesktopSession.PMetalData.FindIndex(delegate(PMetalInfo pm)
                {
                    if (!sMetalType.Equals("GOLD"))
                    {
                        return pm.Class == sClass
                               && pm.Metal == sMetalType
                               && pm.Category == _Item.CategoryCode
                               && dApproximateGrams >= pm.Gram_Low
                               && dApproximateGrams <= pm.Gram_High;
                    }
                    else
                    {
                        return pm.Class == sClass
                               && pm.Metal == sMetalType
                               && pm.Category == _Item.CategoryCode
                               && pm.Karats == dApproximateKarat
                               && dApproximateGrams >= pm.Gram_Low
                               && dApproximateGrams <= pm.Gram_High;
                    }
                });

                if (iMetalInfoIdx < 0)
                {
                    iMetalInfoIdx = DesktopSession.PMetalData.FindIndex(delegate(PMetalInfo pm)
                    {
                        if (!sMetalType.Equals("GOLD"))
                        {
                            return pm.Class == sClass
                                   && pm.Metal == sMetalType
                                   && dApproximateGrams >= pm.Gram_Low
                                   && dApproximateGrams <= pm.Gram_High;
                        }
                        else
                        {
                            return pm.Class == sClass
                                   && pm.Metal == sMetalType
                                   && pm.Karats == dApproximateKarat
                                   && dApproximateGrams >= pm.Gram_Low
                                   && dApproximateGrams <= pm.Gram_High;
                        }
                    });
                }

                if (iMetalInfoIdx >= 0)
                {
                    dLoanAmountPerGram = DesktopSession.PMetalData[iMetalInfoIdx].Loan_Buy_Per_Gram;
                    dRetailAmountPerGram = DesktopSession.PMetalData[iMetalInfoIdx].Retail_Per_Gram;

                    // Determine if Jewelry is a Gold Watch
                    if (_Item.CategoryCode == 1520 || _Item.CategoryCode == 1540)
                    {
                        // *** COMMENTED OUT PER TOM
                        // Call PWN_BR-65 Business Rule for Watch Movement
                        //BusinessRuleVO brWATCH_MOVEMENT = GetBusinessRule("PWN_BR-065");
                        //bPawnValue                      = brWATCH_MOVEMENT.getComponentValue("WATCH_MOVEMENT", ref sComponentValue);
                        //if (bPawnValue)
                        //{
                        if (dApproximateGrams > Convert.ToDecimal(10 * 1.555))
                            dAdjustedGrams = (dApproximateGrams - Convert.ToDecimal(4 * 1.555));
                        else
                            dAdjustedGrams = (dApproximateGrams - Convert.ToDecimal(dApproximateGrams * 0.20M));
                        MessageBox.Show("Value have been adjusted for the weight of the watch movement.", "Watch Movement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // }
                    }
                    else
                        dAdjustedGrams = dApproximateGrams;

                    // *** COMMENTED OUT PER TOM
                    // Call PWN_BR-62
                    //BusinessRuleVO brLOAN_METAL_VALUE   = GetBusinessRule("PWN_BR-062");
                    //bPawnValue                          = brLOAN_METAL_VALUE.getComponentValue("LOAN_METAL_VALUE", ref sComponentValue);
                    //if (bPawnValue)
                    //{
                    //    dLoanAmountPerGram  = Convert.ToDecimal(sComponentValue);
                    dTotalLoanValue = (dLoanAmountPerGram * dAdjustedGrams);
                    labelGramValueHeading.Visible = true;
                    labelGramValue.Visible = true;
                    labelGramValueHeading.Text = dAdjustedGrams + "GRM";
                    labelGramValue.Text = String.Format("{0:C}", dTotalLoanValue);
                    //}

                    // *** COMMENTED OUT PER TOM
                    // Call PWN_BR-63
                    //BusinessRuleVO brRETAIL_METAL_VALUE = GetBusinessRule("PWN_BR-063");
                    //bPawnValue                          = brRETAIL_METAL_VALUE.getComponentValue("RETAIL_METAL_VALU", ref sComponentValue);
                    //if (bPawnValue)
                    //{
                    //    fTotalRetailValue = Convert.ToDecimal(sComponentValue);
                    dTotalRetailValue = (dRetailAmountPerGram * dAdjustedGrams);

                    //}
                    if (sMetalType == "GOLD")
                        _Item.TotalLoanGoldValue = dTotalLoanValue;
                    else
                        _Item.TotalLoanGoldValue = 0;
                }
            }
        }

        private void ShowJewelryDisplay()
        {
            //Safe lock to keep non jewelry items from being processed
            if (!_Item.IsJewelry ||
                this.JewelryDisplayForm.Visible)
            {
                return;
            }
            this.displayJewelry();
        }

        private void HideJewelryDisplay()
        {
            if (this.JewelryDisplayForm == null ||
                !this.JewelryDisplayForm.Visible)
            {
                return;
            }
            this.JewelryDisplayForm.SendToBack();
            this.JewelryDisplayForm.HideJewelry();
        }

        /// <summary>
        /// Display hard coded jewelry images (for build 3.0.4 only!!)
        /// </summary>
        private void displayJewelry()
        {
            //Display jewelry type if available
            if (!_Item.IsJewelry)
            {
                this.HideJewelryDisplay();
            }

            //Get the description of the jewelry item
            string catDesc = this._Item.CategoryDescription;
            if (string.IsNullOrEmpty(catDesc))
                return;

            //Check for jewelry types from category description
            string displayKey = string.Empty;
            if (catDesc.IndexOf("HB CHAIN", StringComparison.OrdinalIgnoreCase) >= 0 ||
                catDesc.IndexOf("HERRINGBONE", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                displayKey = "Herring Bone Necklace";
            }
            else if (catDesc.IndexOf("PEARL NECKLACE", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                displayKey = "Pearl Necklace";
            }
            else if (catDesc.IndexOf("PEND/CHARM W/STNS", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                displayKey = "Heart Diamond Pendant";
            }
            else if (catDesc.IndexOf("CHAIN W/DIA PEND", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                displayKey = "Diamond Pendant";
            }

            //If we did not find a type, do not display the form
            if (displayKey == string.Empty)
            {
                this.HideJewelryDisplay();
                return;
            }
            if (this.Visible)
            {
                this.JewelryDisplayForm.ShowJewelry((Form)this, displayKey);
                this.JewelryDisplayForm.BringToFront();
            }
        }

        private void StonesFlow(ref decimal dTotalLoanValue, ref decimal dTotalRetailValue)
        {
            bool bPawnValue = false;
            decimal dNumberOfPoints = 0;
            decimal dStoneLoanAmtPerPoint = 0;
            decimal dStoneRetailAmtPerPoint = 0;
            decimal dSuggestedLoanStoneValue = 0;
            decimal dSuggestedRetailStoneValue = 0;
            int iNumberOfStones = 0;
            string sComponentValue = string.Empty;
            string sStoneShape = string.Empty;
            totStoneValue = 0;

            // FTN 3.3.e.ii.3 Total up the Jewelry Set values using Stones Business Flow
            foreach (JewelrySet jewelrySet in _Item.Jewelry)
            {
                List<ItemAttribute> foundItemAttributes = jewelrySet.ItemAttributeList.FindAll(delegate(ItemAttribute ia)
                {
                    return ia.Description == "Type Of Stone(S)";
                });

                jewelrySet.TotalStoneValue = 0;

                foreach (ItemAttribute foundItemAttribute in foundItemAttributes)
                {
                    if (foundItemAttribute.Answer.AnswerText == "DIA-TEST POS")
                    {
                        ItemAttribute iaNumberOfStones = jewelrySet.ItemAttributeList.Find(delegate(ItemAttribute ia)
                        {
                            return ia.Description == "Number Of Stones";
                        });

                        iNumberOfStones = Convert.ToInt16(iaNumberOfStones.Answer.AnswerText);

                        ItemAttribute iaShapeOfStones = jewelrySet.ItemAttributeList.Find(delegate(ItemAttribute ia)
                        {
                            return ia.Description == "Shape Of Stones";
                        });

                        sStoneShape = iaShapeOfStones.Answer.AnswerText;

                        ItemAttribute iaNumberOfPoints = jewelrySet.ItemAttributeList.Find(delegate(ItemAttribute ia)
                        {
                            return ia.Description == "Number Of Points";
                        });

                        dNumberOfPoints = Convert.ToDecimal(iaNumberOfPoints.Answer.AnswerText);

                        ItemAttribute iaDiamondClarity = jewelrySet.ItemAttributeList.Find(delegate(ItemAttribute ia)
                        {
                            return ia.Description == "Diamond Clarity";
                        });

                        ItemAttribute iaDiamondColor = jewelrySet.ItemAttributeList.Find(delegate(ItemAttribute ia)
                        {
                            return ia.Description == "Diamond Color";
                        });

                        ItemAttribute iaClass = jewelrySet.ItemAttributeList.Find(delegate(ItemAttribute ia)
                        {
                            return ia.Description == "Class";
                        });

                        int iStoneInfoIdx = DesktopSession.StonesData.FindIndex(delegate(StoneInfo si)
                        {
                            return dNumberOfPoints >= si.Min_Points
                                   && dNumberOfPoints <= si.Max_Points
                                   && si.Clarity == iaDiamondClarity.Answer.AnswerCode
                                   && si.Color == iaDiamondColor.Answer.AnswerCode;
                        });

                        if (iStoneInfoIdx >= 0)
                        {
                            dStoneLoanAmtPerPoint = DesktopSession.StonesData[iStoneInfoIdx].Loan;
                            dStoneRetailAmtPerPoint = DesktopSession.StonesData[iStoneInfoIdx].Retail;
                        }

                        // COMMENTED OUT PER TOM
                        // Calculation of individual Loan and Retail Stone Values
                        // Call PWN_BR-66 & PWN_BR-67 Business Rules
                        //BusinessRuleVO brLOAN_VALUE_STONES  = GetBusinessRule("PWN_BR-066");
                        //bPawnValue                          = brLOAN_VALUE_STONES.getComponentValue("LOAN_VALUE_STONES", ref sComponentValue);
                        //if (bPawnValue)
                        //{
                        //    dStoneLoanAmtPerPoint       = Convert.ToDecimal(sComponentValue);
                        dSuggestedLoanStoneValue = (iNumberOfStones * (dStoneLoanAmtPerPoint * dNumberOfPoints));
                        //}

                        // COMMENTED OUT PER TOM
                        //BusinessRuleVO brRETAIL_VALUE_STONES    = GetBusinessRule("PWN_BR-067");
                        //bPawnValue                              = brRETAIL_VALUE_STONES.getComponentValue("RETAIL_VALUE_STONES", ref sComponentValue);
                        //if (bPawnValue)
                        //{
                        //    dStoneRetailAmtPerPoint     = Convert.ToDecimal(sComponentValue);
                        dSuggestedRetailStoneValue = (iNumberOfStones * (dStoneRetailAmtPerPoint * dNumberOfPoints));
                        //}

                        // Call PWN_BR-68 Business Rule to retrieve Scrap_List
                        BusinessRuleVO brSCRAP_VALUE = GetBusinessRule("PWN_BR-068");
                        bPawnValue = brSCRAP_VALUE.getComponentValue("SCRAP_LIST", ref sComponentValue);
                        if (bPawnValue)
                        {
                            List<string> listScrapList = new List<string>();
                            listScrapList.AddRange(sComponentValue.Split('|'));

                            if (listScrapList.FindIndex(delegate(string s)
                            {
                                return s == iaClass.Answer.AnswerText;
                            }) < 0)
                            {
                                decimal dAdjSS1 = 0;
                                decimal dAdjSS2 = 0;

                                // Call PWN_BR-69 Business Rule for Diamond Shape Adjustment
                                BusinessRuleVO brDIAMOND_SHAPE = GetBusinessRule("PWN_BR-069");
                                bPawnValue = brDIAMOND_SHAPE.getComponentValue("PK_SS1_ADJ", ref sComponentValue);
                                if (bPawnValue)
                                {
                                    dAdjSS1 = Convert.ToDecimal(sComponentValue);
                                }

                                bPawnValue = brDIAMOND_SHAPE.getComponentValue("PK_SS2_ADJ", ref sComponentValue);
                                if (bPawnValue)
                                {
                                    dAdjSS2 = Convert.ToDecimal(sComponentValue);
                                }

                                if (dAdjSS1 == 0 || dAdjSS1.Equals(null))
                                    dAdjSS1 = 1.15M;
                                if (dAdjSS2 == 0 || dAdjSS2.Equals(null))
                                    dAdjSS2 = 1.20M;

                                bPawnValue = brDIAMOND_SHAPE.getComponentValue("CL_PWN_0179_LVADSMOP", ref sComponentValue);
                                if (sComponentValue.IndexOf(sStoneShape) >= 0)
                                {
                                    dSuggestedLoanStoneValue = dSuggestedLoanStoneValue * dAdjSS1;
                                    dSuggestedRetailStoneValue = dSuggestedRetailStoneValue * dAdjSS1;
                                }

                                bPawnValue = brDIAMOND_SHAPE.getComponentValue("CL_PWN_0180_LVADSHEBF", ref sComponentValue);
                                if (sComponentValue.IndexOf(sStoneShape) >= 0)
                                {
                                    dSuggestedLoanStoneValue = dSuggestedLoanStoneValue * dAdjSS2;
                                    dSuggestedRetailStoneValue = dSuggestedRetailStoneValue * dAdjSS2;
                                }
                                jewelrySet.TotalStoneValue = dSuggestedLoanStoneValue;
                                _Item.TotalLoanStoneValue += dSuggestedLoanStoneValue;
                            }
                            else
                                jewelrySet.TotalStoneValue = 0;
                        }

                        // Summing up the calculated Stone Values
                        dTotalLoanValue += dSuggestedLoanStoneValue;
                        labelStoneValueHeading.Visible = true;
                        labelStoneValue.Visible = true;
                        totStoneValue += dSuggestedLoanStoneValue;
                        dTotalRetailValue += dSuggestedRetailStoneValue;
                    }
                    else
                        jewelrySet.TotalStoneValue = 0;
                }
                labelStoneValue.Text = totStoneValue > 0 ? String.Format("{0:C}", totStoneValue) : String.Format("{0:C}", 0);
            }
        }

        private string BuildProDataTable(string sDescription, string sValue)
        {
            if (sDescription != string.Empty)
            {
                sValue = String.Format("{0:C}", Convert.ToDouble(sValue));
                return sDescription = sDescription.PadRight(25, ' ') + "\t" + (sValue).PadLeft(10, ' ') + Environment.NewLine;
            }
            else
                return "";
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            // FTN 3.3.f.i
            submitButton.Enabled = false;
            this.HideJewelryDisplay();

            if (_CurrentContext == CurrentContext.AUDITCHARGEON)
            {
                if (!CostValueIsValid())
                {
                    MessageBox.Show("Invalid Cost Amount.  Please re-enter.", "Loan Cost Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    attribute_costAmount_TextBox.Focus();
                    submitButton.Enabled = true;
                    //this.ShowJewelryDisplay();
                    return;
                }
            }
            if (_CurrentContext != CurrentContext.CHANGE_RETAIL_PRICE && _CurrentContext != CurrentContext.AUDITCHARGEON && _CurrentContext != CurrentContext.GUNEDIT && _CurrentContext != CurrentContext.GUNREPLACE) 
            {
                if (!StringUtilities.IsDecimal(attribute_loanAmount_TextBox.Text))
                {
                    MessageBox.Show("Invalid Purchase Amount.  Please re-enter.", "Amount Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    attribute_loanAmount_TextBox.Focus();
                    submitButton.Enabled = true;
                    //this.ShowJewelryDisplay();
                    return;
                }

                if (!CostValueIsValid())
                {
                    MessageBox.Show("Invalid Cost Amount.  Please re-enter.", "Loan Cost Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    attribute_costAmount_TextBox.Focus();
                    submitButton.Enabled = true;
                    //this.ShowJewelryDisplay();
                    return;
                }

                //Madhu fix for BZ defect 78
                bool expenseItem = false;
                if (purchaseFlow || vendorPurchaseFlow)
                {
                    if (categoryAttributeTableLayoutPanel.Controls["attribute_expense_chkbox"] != null)
                        expenseItem = Utilities.GetBooleanValue(((CheckBox)categoryAttributeTableLayoutPanel.Controls["attribute_expense_chkbox"]).Checked, false);
                }

                //Madhu fix for BZ defect 78 added "!expenseItem" condition
                if (!expenseItem)
                {
                    if (!RetailValueIsValid())
                    {
                        MessageBox.Show("Invalid Retail Amount.  Please re-enter.", "Retail Amount Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        attribute_RetailPrice_TextBox.Focus();
                        submitButton.Enabled = true;
                        //this.ShowJewelryDisplay();
                        return;
                    }
                }

                if (descriptonTextBox.Text.Length > 200)
                {
                    int length = descriptonTextBox.Text.Length;
                    MessageBox.Show("Description length must be less than 200, current length = " +
                                    length.ToString(), "Description Length Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    submitButton.Enabled = true;
                    return;
                }

                // Loop all AttributeControls that were dynamically added to TableLayout Panel
                foreach (Control attributeControl in categoryAttributeTableLayoutPanel.Controls)
                {
                    // We are only interested in User-Entered Form Controls
                    if (attributeControl.GetType() == typeof(TextBox) || attributeControl.GetType() == typeof(ComboBox))
                    {
                        string sValue = string.Empty;

                        switch (attributeControl.GetType().Name)
                        {
                            case "MaskedTextBox":
                            case "TextBox":
                                sValue = ((TextBox)attributeControl).Text;
                                break;
                            case "ComboBox":
                                if (((ComboBox)attributeControl).SelectedItem != null)
                                    sValue = ((ComboBox)attributeControl).SelectedItem.ToString();
                                break;
                        }
                    }
                }

                string loanAmount = attribute_loanAmount_TextBox.Text;
                if (loanAmount.StartsWith("$"))
                    loanAmount = loanAmount.Substring(1);

                decimal dLoanAmount = Utilities.GetDecimalValue(loanAmount);
                decimal newDLoanAmount = 0.0m;
                bool dloanAmountReassigned = false;
                if (_CurrentContext == CurrentContext.PFI_REDESCRIBE ||
                    _CurrentContext == CurrentContext.PFI_ADD)
                {
                    if (string.IsNullOrEmpty(attribute_costAmount_TextBox.Text))
                        attribute_costAmount_TextBox.Text = attribute_loanAmount_TextBox.Text;
                    string costAmount = attribute_costAmount_TextBox.Text;
                    if (costAmount.StartsWith("$"))
                        dLoanAmount = Utilities.GetDecimalValue(costAmount.Substring(1));
                    else
                        dLoanAmount = Utilities.GetDecimalValue(costAmount);

                    //not concrete of this logic, done to get past "if (dLoanAmount == 0" in the next segment of code
                    if (dLoanAmount == 0)
                    {
                        newDLoanAmount = dLoanAmount;
                        dLoanAmount = 1;
                        dloanAmountReassigned = true;
                    }
                }

                // FTN 3.3.f.ii
                if (!attribute_loanAmount_TextBox.ReadOnly && dLoanAmount == 0 && merchandiseAvailableCheckbox.Checked && !_Item.IsChargedOff() && _CurrentContext != CurrentContext.CHANGE_RETAIL_PRICE)
                {
                    if (purchaseFlow || vendorPurchaseFlow)
                    {
                        MessageBox.Show(
                            "Invalid Purchase Amount.  Please re-enter.",
                            "Purchase Amount Validation",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else if (saleFlow)
                    {
                        MessageBox.Show(
                            "Invalid Retail Amount.  Please re-enter.",
                            "Retail Amount Validation",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Invalid Loan Amount.  Please re-enter.",
                            "Loan Amount Validation",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    attribute_loanAmount_TextBox.Focus();
                    submitButton.Enabled = true;
                    //this.ShowJewelryDisplay();
                    return;
                }

                if (dLoanAmount < 0 && merchandiseAvailableCheckbox.Checked)
                {
                    if (purchaseFlow || vendorPurchaseFlow)
                    {
                        MessageBox.Show("Purchase Amount cannot be negative.  Please re-enter.", "Purchase Amount Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Loan Amount cannot be negative.  Please re-enter.", "Loan Amount Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    attribute_loanAmount_TextBox.Focus();
                    submitButton.Enabled = true;
                    //this.ShowJewelryDisplay();
                    return;
                }
                if (dloanAmountReassigned)
                    dLoanAmount = newDLoanAmount;
                // FTN 3.3.f.iii:  Check previous context was "PFI Merge".  If so, trigger PFI Merge

                // FTN 3.3.f.iv:  Check to make sure Loan Amount does not exceed Max Loan Amount
                string sComponentValue = string.Empty;
                bool bPawnValue;
                if (!purchaseFlow && _Item.mDocType != "8")
                {
                    decimal CL_PWN_0001_MaxLoanAmount = 0;

                    bPawnValue = GetComponentValue("PWN_BR-035", "CL_PWN_0001_MAXLOANAMT", ref sComponentValue);
                    if (bPawnValue)
                    {
                        CL_PWN_0001_MaxLoanAmount = Convert.ToDecimal(sComponentValue);
                    }

                    if (CL_PWN_0001_MaxLoanAmount < dLoanAmount)
                    {
                        string sLoanMessage = String.Format("You have entered a loan amount greater than the maximum loan amount of {0}", CL_PWN_0001_MaxLoanAmount.ToString());
                        MessageBox.Show(sLoanMessage, "Loan Amount Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        attribute_loanAmount_TextBox.Focus();
                        submitButton.Enabled = true;
                        //this.ShowJewelryDisplay();
                        return;
                    }
                }

                // FTN 3.3.f.v:  Retrieve Shop Parameter PK_APPROVE

                bPawnValue = GetComponentValue("PWN_BR-004", "CL_PWN_0052_PK_APPROVE", ref sComponentValue);
                string CL_PWN_0052_PkApprove = string.Empty;

                if (bPawnValue)
                {
                    CL_PWN_0052_PkApprove = sComponentValue.ToString();
                }

                if ((CL_PWN_0052_PkApprove != null && CL_PWN_0052_PkApprove != "N") || _ProKnow_SuggestedLoanAmount_NonJewelry != dLoanAmount)
                {
                    // Perform Variance Lookups
                    int iVarianceIdx = DesktopSession.VarianceRates.FindIndex(delegate(Variance v)
                    {
                        return v.DocType == 'K'
                               && v.MinAmount <= dLoanAmount
                               && v.MaxAmount >= dLoanAmount;
                    });

                    if (iVarianceIdx >= 0)
                    {
                        decimal dVarianceAmount = 0;

                        if (_Item.IsJewelry)
                            dVarianceAmount = DesktopSession.VarianceRates[iVarianceIdx].Percent / 100 * _ProKnow_SuggestedLoanAmount;
                        else
                            dVarianceAmount = DesktopSession.VarianceRates[iVarianceIdx].Percent / 100 * _ProKnow_SuggestedLoanAmount_NonJewelry;

                        decimal dSuggestedPKValue = _Item.IsJewelry ? _ProKnow_SuggestedLoanAmount : _ProKnow_SuggestedLoanAmount_NonJewelry;

                        // Call PWN_BR-004 for Manager Approval for Loan Variance
                        bool bAcceptableLoanAmount = false;

                        /*bPawnValue = GetComponentValue("PWN_BR-004", "CL_PWN_0052_PK_APPROVE", ref sComponentValue);
                        if (bPawnValue)
                        {
                        CL_PWN_0052_PkApprove = sComponentValue;
                        }*/

                        switch (CL_PWN_0052_PkApprove)
                        {
                            case "Y":
                                bAcceptableLoanAmount = dLoanAmount >= (dSuggestedPKValue - dVarianceAmount);
                                bAcceptableLoanAmount = (bAcceptableLoanAmount && dLoanAmount <= (dSuggestedPKValue + dVarianceAmount));
                                break;
                            case "A":
                                bAcceptableLoanAmount = dLoanAmount >= dSuggestedPKValue;
                                bAcceptableLoanAmount = (bAcceptableLoanAmount && dLoanAmount <= (dSuggestedPKValue + dVarianceAmount));
                                break;
                            case "B":
                                bAcceptableLoanAmount = dLoanAmount >= dSuggestedPKValue;
                                bAcceptableLoanAmount = (bAcceptableLoanAmount && dLoanAmount >= (dSuggestedPKValue - dVarianceAmount));
                                break;
                        }
                        // if !bAcceptableLoanAmount, Invoke Manage Overrides
                        if (!bAcceptableLoanAmount && dSuggestedPKValue > 0)
                        {
                            //Do not call manager override for pro know value variation
                            //if we are doing PFI or we are creating a temporary ICN during sale
                            if (_CurrentContext == CurrentContext.PFI_ADD
                                || _CurrentContext == CurrentContext.PFI_MERGE
                                || _CurrentContext == CurrentContext.PFI_REPLACE
                                || _CurrentContext == CurrentContext.PFI_REDESCRIBE
                                || _Item.mDocType == "8"
                            )
                            {
                            }
                            else
                            {
                                int ticketNumber = 0;
                                ManagerOverrideTransactionType managerOverrideTransactionType;
                                if (!purchaseFlow)
                                {
                                    managerOverrideTransactionType = ManagerOverrideTransactionType.NL;
                                    ticketNumber = DesktopSession.ActivePawnLoan.TicketNumber;
                                }
                                else
                                {
                                    managerOverrideTransactionType = ManagerOverrideTransactionType.PUR;
                                    ticketNumber = DesktopSession.ActivePurchase.TicketNumber;
                                }

                                //Madhu 11/18/2010 fix for the defect PWNU00001456
                                ManageOverrides manageOverrides;

                                if (purchaseFlow || vendorPurchaseFlow)
                                {
                                    manageOverrides = new ManageOverrides(DesktopSession, "Describe Item"
                                                                          , ticketNumber
                                                                          , dLoanAmount
                                                                          , dSuggestedPKValue
                                                                          , managerOverrideTransactionType
                                                                          , ManagerOverrideType.PURO);
                                }
                                else
                                {
                                    manageOverrides = new ManageOverrides(DesktopSession, "Describe Item"
                                                                          , ticketNumber
                                                                          , dLoanAmount
                                                                          , dSuggestedPKValue
                                                                          , managerOverrideTransactionType
                                                                          , ManagerOverrideType.PKV);
                                }
                                manageOverrides.ShowDialog(this.Owner);
                                if (manageOverrides.OverrideAllowed)
                                {
                                    submitButton.Enabled = true;
                                }
                                else
                                {
                                    submitButton.Enabled = false;
                                    return;
                                }
                            }
                        }
                    }
                }


                _Item.ItemAmount = dLoanAmount;
                _Item.PfiAmount = _Item.ItemAmount;
                if (_CurrentContext == CurrentContext.NEW
                    || _CurrentContext == CurrentContext.PFI_ADD
                    || _CurrentContext == CurrentContext.PFI_MERGE
                    || _CurrentContext == CurrentContext.PFI_REPLACE
                )
                {
                    _Item.mDocType = !purchaseFlow ? "1" : "2";
                    _Item.mItemOrder = _CurrentContext == CurrentContext.PFI_REPLACE ? ItemOrder : _CurrentContext == CurrentContext.PFI_ADD ? _Item.mItemOrder : _CurrentContext == CurrentContext.PFI_MERGE ? DesktopSession.SelectedPFIMergeItemIndex[0] + 1 : _CurrentPawnIndex;
                    _Item.mStore = Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
                    _Item.mYear = _CurrentContext != CurrentContext.NEW ? _Item.mYear : Convert.ToInt16(ShopDateTime.Instance.ShopDate.Year.ToString().Substring(ShopDateTime.Instance.ShopDate.Year.ToString().Length - 1));
                    _Item.Icn = Utilities.IcnGenerator(_Item.mStore, _Item.mYear, _Item.mDocNumber, _Item.mDocType, _Item.mItemOrder, 0);
                }

                if (!merchandiseAvailableCheckbox.Checked)
                    _Item.ItemReason = ItemReason.NOMD;
                else if (_CurrentContext == CurrentContext.PFI_REPLACE)
                    _Item.ItemReason = ItemReason.BLNK;

                _Item.QuickInformation = GetQuickInformation();

                if (vendorPurchaseFlow)
                {
                    if (categoryAttributeTableLayoutPanel.Controls["attribute_expense_chkbox"] != null)
                    {
                        expenseItem = Utilities.GetBooleanValue(((CheckBox)categoryAttributeTableLayoutPanel.Controls["attribute_expense_chkbox"]).Checked, false);
                        _Item.IsExpenseItem = expenseItem;
                    }
                    //Add the retail price
                    decimal dRetailPrice = Utilities.GetDecimalValue(attribute_RetailPrice_TextBox.Text.ToString());
                    _Item.RetailPrice = dRetailPrice;
                    //If serial number is an attribute of this item
                    if (_Item.PfiDate == DateTime.MinValue)
                        _Item.PfiDate = ShopDateTime.Instance.ShopDate;
                    ItemAttribute itemAttribSerialNumber = _Item.Attributes.Find(
                        ia => ia.Description == "Serial Number");
                    if (_Item.Quantity > 1)
                    {
                        if (!string.IsNullOrEmpty(itemAttribSerialNumber.Description))
                        {
                            if (_Item.SerialNumber == null)
                                _Item.SerialNumber = new List<string>();
                            AddSerialNumbers serialNoForm = new AddSerialNumbers(DesktopSession)
                            {
                                ItemQuantity = _Item.Quantity,
                                ItemOrder = _Item.mItemOrder,
                                FirstSerialNumber = itemAttribSerialNumber.Answer.AnswerText

                            };
                            serialNoForm.ShowDialog();
                        }
                    }
                    else
                    {
                        _Item.Quantity = 1;
                        DesktopSession.ActivePurchase.Items[_Item.mItemOrder].SerialNumber = new List<string>(1) { itemAttribSerialNumber.Answer.AnswerText };
                    }
                }
                if (_CurrentContext == CurrentContext.PFI_REDESCRIBE
                    || _CurrentContext == CurrentContext.PFI_REPLACE
                    || _CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE
                    || _CurrentContext == CurrentContext.PFI_ADD)
                {
                    //if (customerPurchasePFI || !purchaseFlow)
                    //{
                        //SR 10/05/2010 Do not print tags during PFI if its cacc item
                        if (_Item.CaccLevel != 0
                            && _Item.ItemReason != ItemReason.COFFNXT
                            && _Item.ItemReason != ItemReason.COFFSTRU
                            && _Item.ItemReason != ItemReason.COFFBRKN
                            && _Item.PfiAssignmentType != PfiAssignment.Refurb)
                            DesktopSession.PrintTags(_Item, _CurrentContext);
                    //}
                }
                // FTN 3.3.f.viii & 3.3.f.ix
                if (_CurrentContext == CurrentContext.PFI_ADD
                    || _CurrentContext == CurrentContext.PFI_MERGE
                    || _CurrentContext == CurrentContext.PFI_REDESCRIBE
                    || _CurrentContext == CurrentContext.PFI_REPLACE
                )
                {
                    if (_CurrentContext == CurrentContext.PFI_ADD)
                    {
                        if (!purchaseFlow)
                        {
                            DesktopSession.ActivePawnLoan.Items.Add(_Item);
                        }
                        else
                        {
                            DesktopSession.ActivePurchase.Items.Add(_Item);
                        }
                    }
                    else
                    {
                        if (!purchaseFlow)
                        {
                            DesktopSession.ActivePawnLoan.Items.RemoveAt(_CurrentPawnIndex);
                            DesktopSession.ActivePawnLoan.Items.Insert(_CurrentPawnIndex, _Item);
                        }
                        else
                        {
                            DesktopSession.ActivePurchase.Items.RemoveAt(_CurrentPawnIndex);
                            DesktopSession.ActivePurchase.Items.Insert(_CurrentPawnIndex, _Item);
                        }

                    }

                    DesktopSession.HistorySession.VisibleForm("PFI_Verify");
                    DesktopSession.HistorySession.Back();
                    this.Close();
                }
                else if (_CurrentContext == CurrentContext.GUNEDIT)
                {
                    DesktopSession.ActivePawnLoan.Items.RemoveAt(_CurrentPawnIndex);
                    DesktopSession.ActivePawnLoan.Items.Insert(_CurrentPawnIndex, _Item);
                    NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                }
                else
                {
                    //If it is a vendor purchase check if the serial numbers entered matches the quantity
                    if (vendorPurchaseFlow)
                    {
                        ItemAttribute itemAttribSerialNumber = _Item.Attributes.Find(
                            ia => ia.Description == "Serial Number");
                        if (_Item.Quantity > 1 && !string.IsNullOrEmpty(itemAttribSerialNumber.Description) && _Item.SerialNumber.Count != _Item.Quantity)
                        {
                            MessageBox.Show("Please enter serial numbers for all the items before submitting");
                            return;
                        }
                    }
                    else
                    {
                        _Item.Quantity = 1;
                    }
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "ManageMultiplePawnItems";
                    NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                }
            }
            else if (_CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE)
            {

                decimal dRetailPrice = Utilities.GetDecimalValue(attribute_RetailPrice_TextBox.Text.ToString());
                _Item.RetailPrice = dRetailPrice;
                // _Item.PfiAmount = _Item.ItemAmount;
                if (_Item.CaccLevel != 0)
                {
                    ReprintTagVerify tagVerify = new ReprintTagVerify(DesktopSession, _Item, ReprintVerifySender.ChangeRetailPrice);
                    tagVerify.ShowDialog(this);
                }
                //CDS.PrintTags(_Item);

                string errorText = null;
                string errorCode = null;
                if (_Item.mDocType != "7")
                    RetailProcedures.LockItem(DesktopSession, _Item.Icn, out errorCode, out errorText, "N");

                MerchandiseProcedures.UpdateMerchandiseRetailPrice(DesktopSession,
                                                                   new Icn(_Item.Icn), dRetailPrice, out errorCode, out errorText);

                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else if (_CurrentContext == CurrentContext.GUNEDIT || _CurrentContext == CurrentContext.GUNREPLACE)
            {
                _Item.QuickInformation = GetQuickInformation();
                DesktopSession.ActivePawnLoan.Items.RemoveAt(_CurrentPawnIndex);
                DesktopSession.ActivePawnLoan.Items.Insert(_CurrentPawnIndex, _Item);
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                decimal dRetailPrice = Utilities.GetDecimalValue(attribute_RetailPrice_TextBox.Text.ToString());
                _Item.RetailPrice = dRetailPrice;
                _Item.PfiAmount = _Item.ItemAmount;
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
        }

        private bool CostValueIsValid()
        {
            //if (attribute_costAmount_TextBox.Visible && !string.IsNullOrEmpty(attribute_costAmount_TextBox.Text)
            //        && !StringUtilities.IsDecimal(attribute_costAmount_TextBox.Text))
            decimal costValue = Utilities.GetDecimalValue(attribute_costAmount_TextBox.Text, 0);

            if (costValue > 0)
            {
                return true;
            }

            if (!attribute_costAmount_TextBox.Visible)
            {
                return true;
            }

            if (!StringUtilities.IsDecimal(attribute_costAmount_TextBox.Text))
            {
                return false;
            }

            if (_CurrentContext == CurrentContext.AUDITCHARGEON && costValue <= 0)
            {
                return false;
            }

            return true;
        }

        private bool RetailValueIsValid()
        {
            decimal retailValue = Utilities.GetDecimalValue(attribute_RetailPrice_TextBox.Text);

            if (retailValue > 0)
            {
                return true;
            }

            if (!attribute_RetailPrice_TextBox.Visible)
            {
                return true;
            }

            if (!vendorPurchaseFlow && _Item.PfiAssignmentType == PfiAssignment.Scrap && _Item.IsJewelry)
            {
                return true;
            }

            if (_Item.IsChargedOff())
            {
                return true;
            }

            return false;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.HideJewelryDisplay();
            if (_CurrentContext == CurrentContext.NEW && _CurrentPawnIndex >= 0)
            {
                if (purchaseFlow)
                {
                    if (DesktopSession.ActivePurchase.Items.Count > 0 && DesktopSession.ActivePurchase.Items[_CurrentPawnIndex] != null)
                    {
                        DesktopSession.ActivePurchase.Items.RemoveAt(_CurrentPawnIndex);
                    }
                }
                else if (saleFlow)
                {
                    if (DesktopSession.ActiveRetail.Items != null && DesktopSession.ActiveRetail.Items.Count > 0 && DesktopSession.ActiveRetail.Items[_CurrentPawnIndex] != null)
                    {
                        DesktopSession.ActiveRetail.Items.RemoveAt(_CurrentPawnIndex);
                    }
                }
                else
                {
                    if (DesktopSession.ActivePawnLoan.Items.Count > 0 && DesktopSession.ActivePawnLoan.Items[_CurrentPawnIndex] != null)
                    {
                        DesktopSession.ActivePawnLoan.Items.RemoveAt(_CurrentPawnIndex);
                    }
                }
            }
            if (_CurrentContext == CurrentContext.EDIT_MMP && _CurrentPawnIndex >= 0)
            {
                if (purchaseFlow)
                {
                    if (DesktopSession.ActivePurchase.Items.Count > 0 && DesktopSession.ActivePurchase.Items[_CurrentPawnIndex] != null)
                    {
                        DesktopSession.ActivePurchase.Items.RemoveAt(_CurrentPawnIndex);
                        DesktopSession.ActivePurchase.Items.Insert(_CurrentPawnIndex, _OriginalItem);
                    }

                }
                else
                {
                    if (DesktopSession.ActivePawnLoan != null)
                    {
                        if (DesktopSession.ActivePawnLoan.Items.Count > 0 && DesktopSession.ActivePawnLoan.Items[_CurrentPawnIndex] != null)
                        {
                            DesktopSession.ActivePawnLoan.Items.RemoveAt(_CurrentPawnIndex);
                            DesktopSession.ActivePawnLoan.Items.Insert(_CurrentPawnIndex, _OriginalItem);
                        }
                    }
                }

            }
            if (_CurrentContext == CurrentContext.PFI_ADD
                || _CurrentContext == CurrentContext.PFI_MERGE
                || _CurrentContext == CurrentContext.PFI_REDESCRIBE
                || _CurrentContext == CurrentContext.PFI_REPLACE
            )
            {
                // Blank out the Active Pawn Loan since we do not want to save any changes
                // back into PFI
                if (!purchaseFlow)
                    DesktopSession.ActivePawnLoan.TicketNumber = -1;
                else
                    DesktopSession.ActivePurchase.TicketNumber = -1;

                if (_CurrentContext == CurrentContext.PFI_REDESCRIBE)
                {
                    DesktopSession.HistorySession.Back();
                    this.Close();
                }
                else
                {
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = _CurrentContext == CurrentContext.PFI_ADD ? "BackPFIAdd" : "Back";
                    NavControlBox.Action = NavBox.NavAction.BACK;
                }
                //NavControlBox.Action = NavBox.NavAction.BACK;
                this.Close();
            }
            else if (_CurrentContext == CurrentContext.READ_ONLY)
            {
                this.Close();
            }
            else if (_CurrentContext == CurrentContext.GUNEDIT || _CurrentContext == CurrentContext.GUNREPLACE)
            {
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "GUNEDIT";
                NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else
                //FindCurrentLevel();
                ExitOutToCallingPage(0);
        }

        private void closeOutToDesktop()
        {
            this.HideJewelryDisplay();
            if (_CurrentContext != CurrentContext.CHANGE_RETAIL_PRICE
                && DesktopSession.ActiveCustomer != null
                && DesktopSession.ActiveCustomer.CustomerNumber != string.Empty)
            {
                DialogResult dR = MessageBox.Show("Do you want to continue processing this customer?", "Describe Item Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dR == DialogResult.No)
                {
                    DesktopSession.ClearCustomerList();
                }
            }
            DesktopSession.ClearPawnLoan();
            if (purchaseFlow)
            {
                DesktopSession.Purchases.Add(new PurchaseVO());
            }
            else if (!saleFlow && !changeRetailPriceFlow)
            {
                if (this.DesktopSession.PawnLoans != null)
                {
                    this.DesktopSession.PawnLoans.Add(new PawnLoan());
                }
            }
            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.closeOutToDesktop();
        }

        private void editStonesButton_Click(object sender, EventArgs e)
        {
            this.HideJewelryDisplay();
            DescribeStones myForm = new DescribeStones(DesktopSession, _CurrentContext)
            {
                Jewelry = _Item.Jewelry,
                ReturnForm = this
            };
            myForm.ShowDialog(this);
        }

        public void AddDescribeStonesAttributes(List<JewelrySet> updatedJewelrySet)
        {
            _Item.Jewelry = updatedJewelrySet;
        }

        /// <summary>
        /// Exits out to CategorizeMerchandise form
        /// </summary>
        /// <param name="iCategoryCode"></param>
        private void ExitOutToCallingPage(int iCategoryCode)
        {
            this.HideJewelryDisplay();
            if (_CurrentContext == CurrentContext.EDIT_MMP ||
                _CurrentContext == CurrentContext.NEW)
            {
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "Back";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else if (_CurrentContext == CurrentContext.CHANGE_RETAIL_PRICE ||
                _CurrentContext == CurrentContext.AUDITCHARGEON)
            {
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else
            {
                Form previousForm = DesktopSession.HistorySession.Back();
                if (typeof(DescribeMerchandise) == previousForm.GetType())
                {
                    ((DescribeMerchandise)previousForm).GetCategoryNode(iCategoryCode);
                }
            }
        }

        private void DescribeItem_Enter(object sender, EventArgs e)
        {
            if (_Item.Attributes.Count > 0)
                GenerateDynamicTicketDescription();
            this.ShowJewelryDisplay();
        }

        private BusinessRuleVO GetBusinessRule(string sBusinessRule)
        {
            BusinessRuleVO _BusinessRule = null;
            if (this.DesktopSession.PawnBusinessRuleVO != null)
            {
                if (this.DesktopSession.PawnBusinessRuleVO.ContainsKey(sBusinessRule))
                {
                    _BusinessRule = this.DesktopSession.PawnBusinessRuleVO[sBusinessRule];
                }
            }

            return _BusinessRule;
        }

        private bool GetComponentValue(string sBusinessRule, string sComponentKey, ref string refComponentValue)
        {
            BusinessRuleVO bRuleVO = null;
            bool rt = false;
            if (DesktopSession.PawnBusinessRuleVO.ContainsKey(sBusinessRule))
            {
                bRuleVO = DesktopSession.PawnBusinessRuleVO[sBusinessRule];
            }
            if (bRuleVO != null)
            {
                rt = bRuleVO.getComponentValue(sComponentKey, ref refComponentValue);
            }
            return (rt);
        }

        private QuickCheck GetQuickInformation()
        {
            QuickCheck quickInfo = new QuickCheck();
            quickInfo.IsGun = false;
            quickInfo.Caliber = null;
            quickInfo.Gauge = null;
            quickInfo.GunType = null;
            quickInfo.Importer = null;
            quickInfo.Manufacturer = null;
            quickInfo.Model = null;
            quickInfo.SerialNumber = null;

            try
            {
                if (_Item.IsGun)
                {
                    quickInfo.IsGun = true;
                    quickInfo.GunType = _Item.CategoryDescription;
                }

                int iManufacturerIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
                {
                    //                    return ia.AttributeCode == 1;
                    return ia.MdseField == 'M';
                });
                int iModelIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
                {
                    //                    return ia.AttributeCode == 3;
                    return ia.MdseField == 'D';
                });
                int iImporterIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
                {
                    return ia.AttributeCode == 1703;
                });
                int iQuantityIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
                {
                    return ia.Description == "Quantity" && _Item.CaccLevel == 0;
                });
                int iSerialNumberIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
                {
                    //                    return ia.AttributeCode == 2;
                    return ia.MdseField == 'S';
                });

                int iCaliberIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
                {
                    return ia.AttributeCode == 7;
                });
                int iGaugeIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
                {
                    return ia.AttributeCode == 17;
                });
                int iWeightIdx = _Item.Attributes.FindIndex(delegate(ItemAttribute ia)
                {
                    return ia.MdseField == 'W';
                });

                //quickInfo.Manufacturer = iManufacturerIdx >= 0 ? ParseAnswerText(_Item.Attributes[iManufacturerIdx].Answer) : null;
                quickInfo.Manufacturer = iManufacturerIdx >= 0 ? _Item.Attributes[iManufacturerIdx].Answer.AnswerText : null;
                if (quickInfo.Manufacturer != null)
                    quickInfo.Manufacturer = quickInfo.Manufacturer.Trim() == string.Empty ? "N/A" : quickInfo.Manufacturer.Trim();
                //quickInfo.Model = iModelIdx >= 0 ? ParseAnswerText(_Item.Attributes[iModelIdx].Answer) : null;
                quickInfo.Model = iModelIdx >= 0 ? _Item.Attributes[iModelIdx].Answer.AnswerText : null;
                if (quickInfo.Model != null)
                {
                    quickInfo.Model = quickInfo.Model.Trim() == string.Empty ? "N/A" : quickInfo.Model.Trim();
                }
                quickInfo.Importer = iImporterIdx >= 0 ? ParseAnswerText(_Item.Attributes[iImporterIdx].Answer) : null;
                quickInfo.Quantity = iQuantityIdx >= 0 ? ParseAnswerText2(_Item.Attributes[iQuantityIdx].Answer, 1) : 1;
                quickInfo.SerialNumber = iSerialNumberIdx >= 0 ? ParseAnswerText(_Item.Attributes[iSerialNumberIdx].Answer) : null;
                quickInfo.Caliber = iCaliberIdx >= 0 ? ParseAnswerText(_Item.Attributes[iCaliberIdx].Answer) : null;
                quickInfo.Gauge = iGaugeIdx >= 0 ? ParseAnswerText(_Item.Attributes[iGaugeIdx].Answer) : null;
                quickInfo.Weight = iWeightIdx >= 0 ? ParseAnswerText2(_Item.Attributes[iWeightIdx].Answer, 0) : 0;

                if (quickInfo.Caliber == null && quickInfo.Gauge != null)
                    quickInfo.Caliber = quickInfo.Gauge;
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error in describe item while parsing item to get information " + ex.Message);
            }

            return quickInfo;
        }

        private string ParseAnswerText(Answer answer)
        {
            string sAnswerText = null;
            sAnswerText = answer.AnswerText;

            if (sAnswerText == "N/A" || sAnswerText == string.Empty)
                sAnswerText = null;

            return sAnswerText;
        }

        private int ParseAnswerText2(Answer answer, int iDefaultValue)
        {
            int iAnswerText = iDefaultValue;

            try
            {
                iAnswerText = Convert.ToInt32(answer.AnswerText);

                if (iAnswerText == 0)
                    iAnswerText = iDefaultValue;
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Exception thrown: {0}", eX);
                }
                BasicExceptionHandler.Instance.AddException("DescribeItem", eX);
            }

            return iAnswerText;
        }

        private void merchandiseAvailableCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (!merchandiseAvailableCheckbox.Checked)
            {
                if (_TempItem != null)
                    _Item = Utilities.CloneObject(_TempItem);
                attribute_loanAmount_TextBox.ReadOnly = true;
                attribute_costAmount_TextBox.ReadOnly = true;
                attribute_RetailPrice_TextBox.Text = "0.00";
                attribute_RetailPrice_TextBox.ReadOnly = true;
                submitButton.Enabled = true;
            }
            else
            {
                attribute_loanAmount_TextBox.ReadOnly = attribute_costAmount_TextBox.Visible;
                attribute_costAmount_TextBox.ReadOnly = false;
                attribute_RetailPrice_TextBox.ReadOnly = false;
                submitButton.Enabled = false;
            }

            categoryAttributeTableLayoutPanel.Controls.Clear();
            AddCategoryAttributes();
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            if (_CurrentContext != CurrentContext.GUNEDIT)
            {
                _CurrentContext = CurrentContext.PFI_REPLACE;
                DesktopSession.ReplaceDocNumber = _Item.mDocNumber;
                DesktopSession.ReplaceICN = _Item.Icn;
                DesktopSession.ReplaceDocType = _Item.mDocType;
                DesktopSession.ReplaceNumberOfTags = _Item.PfiTags;
                DesktopSession.ReplaceGunNumber = _Item.GunNumber;
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "DescribeMerchandisePFIReplace";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                _CurrentContext = CurrentContext.GUNREPLACE;
                DesktopSession.ReplaceDocNumber = _Item.mDocNumber;
                DesktopSession.ReplaceICN = _Item.Icn;
                DesktopSession.ReplaceDocType = _Item.mDocType;
                DesktopSession.ReplaceNumberOfTags = _Item.PfiTags;
                DesktopSession.ReplaceGunNumber = _Item.GunNumber;
                DesktopSession.DescribeItemContext = CurrentContext.GUNREPLACE;
                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "DescribeMerchandise";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

            }
            //this.Close();
        }

        private void chargeOffButton_Click(object sender, EventArgs e)
        {
            PFI_ChargeOff pfiChargeOff = new PFI_ChargeOff(DesktopSession);
            pfiChargeOff.ChargeOff += new PFI_ChargeOff.ChargeOffHandler(pfiChargeOff_ChargeOff);
            pfiChargeOff.ShowDialog(this.Owner);
        }

        void pfiChargeOff_ChargeOff(ItemReason pirChargeOffCode)
        {
            // Indicator of charge off
            _Item.ItemReason = pirChargeOffCode;
            _Item.RetailPrice = 0;
            _Item.ItemAmount = 0;
            //Form tmpForm = CDS.HistorySession.Back();
            //NavControlBox.Action = NavBox.NavAction.BACK;
        }

        private void HideGroups()
        {
            grpLoanValues.Visible = false;
            grpRetailValue.Visible = false;
            grpAdditional.Visible = false;

            lblRetail_TodaysRetailValue.Text = "N/A";
            lblRetail_SuggestedRetailValue.Text = "N/A";
            lblRetail_Retailer1Label.Text = string.Empty;
            lblRetail_Retailer1Value.Text = string.Empty;
            lblRetail_Retailer2Label.Text = string.Empty;
            lblRetail_Retailer2Value.Text = string.Empty;

            lblLoanValues_SuggestedLoanValue.Text = "N/A";
            lblLoanValues_LoanValueHighValue.Text = "N/A";
            lblLoanValues_LoanValueLowValue.Text = "N/A";

            lblAdditional_PurchaseValueValue.Text = "N/A";
            lblAdditional_IntroducedValue.Text = "N/A";
            lblAdditional_LastUpdatedValue.Text = "N/A";
        }

        private void GroupLocation()
        {
            int iGroupsHeight = 99;

            if (grpFeatures.Visible)
            {
                grpFeatures.Top = iGroupsHeight;
                iGroupsHeight += grpFeatures.Height + 5;
            }
            if (grpRetailValue.Visible)
            {
                grpRetailValue.Top = iGroupsHeight;
                iGroupsHeight += grpRetailValue.Height + 5;
            }
            if (grpLoanValues.Visible)
            {
                grpLoanValues.Top = iGroupsHeight;
                iGroupsHeight += grpLoanValues.Height + 5;
            }

            if (grpAdditional.Visible)
            {
                grpAdditional.Top = iGroupsHeight;
            }
        }

        private void linkLabelCurrentRetailPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RetailItem item = DesktopSession.ActiveRetail.RetailItems[0];
            RetailItemPricingHistory pricingHistory = new RetailItemPricingHistory(DesktopSession, item, GetQuickInformation());
            pricingHistory.ShowDialog();
        }


    }
}
