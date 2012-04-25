/**********************************************************************************
* Namespace:       CommonUI.DesktopForms.Pawn.Products.Describe Merchandise
* Class:           DescribeMerchandise
* 
* Description      Form to allow End User to describe an item on Pawn Loan before
*                  View/Edit the actual information.
* 
* History
* David D Wise, Initial Development
*  PWNU00000103 SMurphy 3/17/2010 Problem when hitting the Cancel button - it was activating GetAlternateManufacturer() 
*  PWNU00000103 SMurphy 3/29/2010 Same as above - Switched to PreviewKeyDown event
*  PWNU00000103 SMurphy 3/25/2010 Form looses functionality after selecting alternate merchant
*  PWNU00000103 SMurphy 3/28/2010 Selected alt manufacturer was not being populated in Describe Merchandise when form closed (removed global var)
*  PWNU00000103 SMurphy 3/29/2010 Problem with model textbox & 1st Cancel button being visible/enabled when they should not be
*  PWNU00000103 SMurphy refactored code - (PWNU00000602 & PWNU00000632) added check for proknow answer - corrected navigation and data passing issues
*  no ticket SMurphy 4/21/2010 changes on form navigaton and from DescribeMerchandise -> DescribeItem
*  no ticket SMurphy 5/6/2010 navigation issues when Cancelling out of PFI process
*  no ticket SMurphy 6/10/2010 added checks for spaces in model & manu. put in "N/A" if <var>.trim() is null
*  SR 12/07/2010 Added logic to handle retail flow
*  Madhu 02/07/2011 fix for BZ # 140 to remove the Merchandise Count control
*************************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Common.ProKnowService;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class DescribeMerchandise : Form
    {
        private ActiveManufacturer _ActiveManufacturerModel;           // Stores which manufacturer has focus
        private bool bypassAltManu = false;

        //PWNU00000103 3/29/2010 Smurphy
        private CurrentContext _CurrentContext { get; set; }
        public DesktopSession DesktopSession { get; private set; }

        // Variable holding desktop flow current context
        //private string _CurrentControlName;                // Stores current control name for Hot Enter Key
        private int _FoundCategoryCode;                 // Used further in Describe Merchandise
        private int _FoundCategoryMask;                 // Used further in Describe Merchandise
        private string _LevelNest;                         // Store Level Nest as property to push to secondary forms
        private DataTable _ManufacturerData;
        //private bool _ManufacturerModelMisMatch;
        private manModelReplyType _ReturnedManModelReplyType;         // Stores ProKnowDetails manModelMatchTypes returned from 
        private List<PairType<int, string>> _Models;                            // List PairType to store _Models from GetModels Lookup
        private ProKnowMatch _ProKnowMatch;                      // Stores ProKnow WebService call results
        private bool _UseHotNumberKeysBoolean = true;    // Flag indicating if to use Hot Keys for number digits entered
        private Form _OwnerForm;
        public NavBox NavControlBox;
        private Control controlFocus;
        private bool DoubleItem;
        private bool purchaseFlow;
        private bool vendorPurchaseFlow;
        private bool saleFlow;
        private bool gunEditFlow;

        public int CategoryCode { get; set; }

        // Calling Application passed Category Code
        //form events
        public DescribeMerchandise(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
            InitializeComponent();

            SetResourceProperties();

            ResetFormFields();
            this.NavControlBox = new NavBox();
            this.NavControlBox.Owner = this;
            _CurrentContext = CurrentContext.NEW;

            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.CUSTOMERPURCHASE.ToString() ||
                DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.DESCRIBEITEMCUSTOMERPURCHASE ||
                DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.VENDORPURCHASE ||
                DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.CUSTOMERPURCHASEPFI ||
                DesktopSession.PurchasePFIAddItem)
            {
                purchaseFlow = true;
            }
            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.VENDORPURCHASE)
                vendorPurchaseFlow = true;
            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.RETAIL)
                saleFlow = true;
            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.GUNBOOKEDIT)
                gunEditFlow = true;
            //Show the back button only if you come to this form
            //from manage multiple pawn items
            if (!purchaseFlow && (DesktopSession.ActivePawnLoan != null &&
                                  DesktopSession.ActivePawnLoan.Items != null &&
                                  DesktopSession.ActivePawnLoan.Items.Count > 0))
                backButton.Visible = true;
            else if (purchaseFlow && (DesktopSession.ActivePurchase != null &&
                                      DesktopSession.ActivePurchase.Items != null &&
                                      DesktopSession.ActivePurchase.Items.Count > 0))
                backButton.Visible = true;
            DoubleItem = false;
            //this.TopMost = true;
        }

        private void SetResourceProperties()
        {
            this.secondaryContinueButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.continueButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.cancelFormButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.findCategoryCodeButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.backButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.BackgroundImage = DesktopSession.ResourceProperties.newDialog_512_BlueScale;
        }

        public DescribeMerchandise(DesktopSession desktopSession, CurrentContext currentContext)
        {
            DesktopSession = desktopSession;
            InitializeComponent();
            SetResourceProperties();
            ResetFormFields();
            this.NavControlBox = new NavBox();
            this.NavControlBox.Owner = this;
            _CurrentContext = currentContext;
            backButton.Visible = false;
            DoubleItem = false;
            if (this.DesktopSession.HistorySession != null && ! string.IsNullOrEmpty(DesktopSession.HistorySession.Trigger))
            {
                if (this.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.CUSTOMERPURCHASE.ToString() ||
                    this.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.DESCRIBEITEMCUSTOMERPURCHASE ||
                    this.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.VENDORPURCHASE ||
                    this.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.CUSTOMERPURCHASEPFI ||
                    this.DesktopSession.PurchasePFIAddItem)
                {
                    this.purchaseFlow = true;
                }
                if (this.DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.RETAIL)
                    this.saleFlow = true;
            }
            if (DesktopSession.HistorySession.Trigger == Commons.TriggerTypes.GUNBOOKEDIT)
                gunEditFlow = true;

        }

        private void DescribeMerchandise_Load(object sender, EventArgs e)
        {
            _OwnerForm = this.Owner;

            _Models = new List<PairType<int, string>>();

            // Check to see if calling form set Category Code.  If so, proceed to Category Code depth level
            if (CategoryCode > 0)
            {
                GetCategoryNode(CategoryCode);
            }
            else
            {
                if (!DesktopSession.CategoryXML.Error)
                {
                    // If no error during setup, populate Merchandise Category Nodes
                    if (DesktopSession.CategoryXML.MerchandiseCount > 0)
                    {
                        GetMerchandiseButtons(DesktopSession.CategoryXML.NextNode, "");
                    }
                    //Madhu to fix for BZ # 140
                    /*
                    #if DEBUG
                    merchandiseCountLabel.Visible = true;
                    merchandiseCountValueLabel.Visible = true;
                    merchandiseCountValueLabel.Text = DesktopSession.CategoryXML.MerchandiseCount.ToString();
                    #else
                    merchandiseCountLabel.Visible       = false;
                    merchandiseCountValueLabel.Visible  = false;
                    #endif
                    */ 
                }
                else
                {
                    MessageBox.Show(DesktopSession.CategoryXML.ErrorMessage, "Category Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Get Merchandise Manufacturers from MainDesktop WebService Pull
            if (DesktopSession.MerchandiseManufacturers != null &&
                DesktopSession.MerchandiseManufacturers.IsInitialized)
            {
                _ManufacturerData = DesktopSession.MerchandiseManufacturers;
                if (_ManufacturerData.Rows.Count > 0)
                {
                    AutoCompleteStringCollection manufacturerDataCollection = new AutoCompleteStringCollection();
                    foreach (DataRow myRow in _ManufacturerData.Rows)
                    {
                        manufacturerDataCollection.Add(myRow[0].ToString());
                    }
                    manufacturerTextBox.AutoCompleteCustomSource = manufacturerDataCollection;
                    secondaryManufacturerTextBox.AutoCompleteCustomSource = manufacturerDataCollection;
                }
            }
            //textboxFocus = manufacturerTextBox;
        }

        private void manufacturerTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //tab to next control or go to buttons to select
            if (e.KeyValue == 9 || e.KeyValue == 13)
            {
                if (string.IsNullOrEmpty(this.manufacturerTextBox.Text))
                {
                    CategoryCodeTableLayoutPanel.Enabled = true;
                    controlFocus = null;
                    CategoryCodeTableLayoutPanel.Focus();
                }
                else
                {
                    _ProKnowMatch = null;
                    controlFocus = manufacturerTextBox;
                    visibileModelControls(false);
                    visibileSecondaryModelControls(false, false);

                    _ActiveManufacturerModel = ActiveManufacturer.PRIMARY;
                    ManufacturerPreviewKeyDown(manufacturerTextBox, modelTextBox);
                }
            }
        }

        private void modelTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 9 || e.KeyValue == 13)
            {
                ModelPreviewKeyDown(modelTextBox);
            }
        }

        private void secondaryManufacturerTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 9 || e.KeyValue == 13)
            {
                if (string.IsNullOrEmpty(this.secondaryManufacturerTextBox.Text))
                {
                    this.CategoryCodeTableLayoutPanel.Enabled = false;
                    MessageBox.Show("Please enter the Manufacturer.");
                    Refresh();
                    secondaryManufacturerTextBox.Focus();
                }
                else
                {
                    visibileSecondaryModelControls(true, false);

                    _ActiveManufacturerModel = ActiveManufacturer.SECONDARY;
                    ManufacturerPreviewKeyDown(this.secondaryManufacturerTextBox, this.secondaryModelTextBox);
                }
            }
        }

        private void secondaryModelTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyValue == 9 || e.KeyValue == 13)
            {
                secondaryContinueButton.Enabled = true;
            }
        }

        private void manufacturerTextBox_TextChanged(object sender, EventArgs e)
        {
            _ActiveManufacturerModel = ActiveManufacturer.PRIMARY;

            visibileModelControls(false);
            visibileSecondaryModelControls(false, false);

            ManufacturerTextChanged(this.manufacturerTextBox, this.modelTextBox);
        }

        private void secondaryManufacturerTextBox_TextChanged(object sender, EventArgs e)
        {
            _ActiveManufacturerModel = ActiveManufacturer.SECONDARY;

            visibileSecondaryModelControls(true, false);

            ManufacturerTextChanged(this.secondaryManufacturerTextBox, this.secondaryModelTextBox);
        }

        private void modelTextBox_TextChanged(object sender, EventArgs e)
        {
            _ActiveManufacturerModel = ActiveManufacturer.PRIMARY;
            ModelTextChanged(modelTextBox, continueButton);
        }

        private void secondaryModelTextBox_TextChanged(object sender, EventArgs e)
        {
            _ActiveManufacturerModel = ActiveManufacturer.SECONDARY;
            ModelTextChanged(this.secondaryModelTextBox, this.secondaryContinueButton);
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            //clear out any existing secondary manu/model info - should not be here
            if (_ProKnowMatch != null && _ProKnowMatch.manufacturerModelInfo.Count > 2 || _ProKnowMatch.transitionStatus == TransitionStatus.MAN_MODEL_PROKNOW_COMBO)
            {
                _ProKnowMatch.manufacturerModelInfo.RemoveAt(2);
                _ProKnowMatch.manufacturerModelInfo.RemoveAt(2);
                _ProKnowMatch.preAnsweredQuestions = null;
                _ProKnowMatch.primaryCategoryCode = 0;
                _ProKnowMatch.primaryCategoryCodeDescription = null;
                _ProKnowMatch.primaryMaskPointer = 0;
                _ProKnowMatch.proKnowData = null;
                _ProKnowMatch.transitionStatus = TransitionStatus.NO_PROKNOW;
            }

            GetProKnowDetails();
            //moved here so that the continue button causes the move to Describe Item
            if (_ProKnowMatch.transitionStatus == TransitionStatus.MAN_MODEL_PROKNOW_COMBO)
            {
                secondaryManufacturerTextBox.Text = string.Empty;
                visibileSecondaryModelControls(true, false);
                Refresh();
                secondaryManufacturerTextBox.Focus();
            }
            else
            {
                //moved here so that the continue button causes the move to Describe Item
                if (_ReturnedManModelReplyType.serviceData.Items[0].GetType().Name != "businessExceptionType")
                {
                    manModelMatchType foundManModelMatchType = (manModelMatchType)_ReturnedManModelReplyType.serviceData.Items[0];
                    //do not go thru ParseProKnowDetails if there has been no secondary item added
                    if (!DoubleItem)
                        ParseProKnowDetails(foundManModelMatchType, _ActiveManufacturerModel);
                }
            }
        }

        private void secondaryContinueButton_Click(object sender, EventArgs e)
        {
            GetProKnowDetails();
            //moved here so that the continue button causes the move to Describe Item
            _ReturnedManModelReplyType.serviceData.Items[0].GetType();
            if (_ReturnedManModelReplyType.serviceData.Items[0].GetType().Name != "businessExceptionType")
            {
                manModelMatchType foundManModelMatchType = (manModelMatchType)_ReturnedManModelReplyType.serviceData.Items[0];
                if (!DoubleItem)
                    ParseProKnowDetails(foundManModelMatchType, _ActiveManufacturerModel);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "Back";
            NavControlBox.Action = NavBox.NavAction.BACK;
        }

        /// <summary>
        /// Exit Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelFormButton_Click(object sender, EventArgs e)
        {
            if (_CurrentContext == CurrentContext.PFI_MERGE)
            {
                if (DesktopSession.ActivePawnLoan != null)
                    DesktopSession.ActivePawnLoan.TicketNumber = -1;
                else if (DesktopSession.ActivePurchase != null)
                    DesktopSession.ActivePurchase.TicketNumber = -1;
                DesktopSession.HistorySession.Back();
                this.Close();
                //NavControlBox.CustomDetail = "PFIAddOrMerge";
                //NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else if (_CurrentContext == CurrentContext.PFI_ADD)
            {
                if (DesktopSession.ActivePawnLoan != null)
                    DesktopSession.ActivePawnLoan.TicketNumber = -1;
                else if (DesktopSession.ActivePurchase != null)
                    DesktopSession.ActivePurchase.TicketNumber = -1;
                NavControlBox.IsCustom = true;
                NavControlBox.Action = NavBox.NavAction.BACK;
                

                //NavControlBox.CustomDetail = "PFIAddOrMerge";
                //NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else if (_CurrentContext == CurrentContext.PFI_REPLACE)
            {
                if (DesktopSession.ActivePawnLoan != null)
                    DesktopSession.ActivePawnLoan.TicketNumber = -1;
                else if (DesktopSession.ActivePurchase != null)
                    DesktopSession.ActivePurchase.TicketNumber = -1;
                NavControlBox.IsCustom = true;
                DesktopSession.HistorySession.Back();
                this.Close();
                //NavControlBox.CustomDetail = "PFIReplace";
                //NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else if (_CurrentContext == CurrentContext.AUDITCHARGEON)
            {
                NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
            else
            {
                if (DesktopSession.ActiveCustomer != null && !string.IsNullOrEmpty(DesktopSession.ActiveCustomer.CustomerNumber))
                {
                    DialogResult dR = MessageBox.Show("Do you want to continue processing this customer?", "Describe Item Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dR == DialogResult.No)
                    {
                        DesktopSession.ClearCustomerList();
                    }
                }

                DesktopSession.ClearPawnLoan();
                if (!saleFlow)
                {
                    if (!purchaseFlow)
                        DesktopSession.PawnLoans.Add(new PawnLoan());
                    else
                        DesktopSession.Purchases.Add(new PurchaseVO());
                }
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        //
        //public method
        //
        //used in ManufacturerModelMatches
        public void ReturnFromManufacturerModelMerchandise(bool bSuccess, int iIdx)
        {
            if (bSuccess)
            {
                //hit when 2nd is available and 2nd manu/model is selected
                //hit when 2nd is available and 2nd manu/model is not selected
                manModelMatchType foundManModelMatchType = (manModelMatchType)_ReturnedManModelReplyType.serviceData.Items[iIdx];
                ParseProKnowDetails(foundManModelMatchType, _ActiveManufacturerModel);
                CategoryCodeTableLayoutPanel.Enabled = false;
            }
        }

        //
        //private methods
        //
        //controls manufacturer textboxes
        private void ManufacturerPreviewKeyDown(TextBox manuTextbox, TextBox mdlTextbox)
        {
            if (nestFlowLayoutPanel.Controls.Count > 5)
            {
                GetMerchandiseButtons(DesktopSession.CategoryXML.NextNode, "");
            }

            mdlTextbox.Enabled = true;
            mdlTextbox.Text = string.Empty;
            controlFocus = mdlTextbox;
            //this.SelectNextControl(manuTextbox, true, true, true, true);
            GetAlternateManufacturer(manuTextbox);
        }

        private void ManufacturerTextChanged(TextBox manuTextbox, TextBox mdlTextbox)
        {
            int selStart = manuTextbox.SelectionStart;
            manuTextbox.Text = manuTextbox.Text.ToUpper();
            manuTextbox.SelectionStart = selStart;//code put in place by Drew to check textbox cursor issues
            mdlTextbox.Text = "";
        }

        //controls model textboxes
        private void ModelPreviewKeyDown(TextBox mdlTextbox)
        {
            if (_ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerCode == 999)
            {
                if (string.IsNullOrEmpty(mdlTextbox.Text))
                {
                    controlFocus = null;
                    CategoryCodeTableLayoutPanel.Focus();
                    visibileModelControls(false);
                }
                else
                {
                    controlFocus = mdlTextbox;
                    GetProKnowDetails();
                }
            }
            else
            {
                controlFocus = continueButton;
                GetProKnowDetails();
            }
        }

        private void ModelTextChanged(TextBox mdlTextbox, Button contButton)
        {
            string sChangedText = mdlTextbox.Text.ToUpper();
            int selStart = mdlTextbox.SelectionStart;
            if (sChangedText != "" && modelListBox.Items.Count > 0)
            {
                PairType<int, string> myPair = (PairType<int, string>)this.modelListBox.SelectedItem;
                if (myPair.Left != 999)
                    ManufacturerModelUpdate(_ActiveManufacturerModel, sChangedText, myPair.Left);

                contButton.Enabled = true;
            }
            else
                contButton.Enabled = false;
            mdlTextbox.Text = sChangedText;
            mdlTextbox.SelectionStart = selStart;//code put in place by Drew to check textbox cursor issues
            populateManufacturerModels(sChangedText);
        }

        //breadcrumbs & buttons
        /// <summary>
        /// Dynamic Button Generator from the passed Category Node array
        /// </summary>
        /// <param name="cnSearchCategoryNodes"></param>
        private void GetMerchandiseButtons(List<CategoryNode> cnSearchCategoryNodes, string sLevelNest)
        {
            // Add the Level Nest Navigation to Flow Layout Panel
            CreateLevelNestNavigation(sLevelNest);

            // Always clear the TableLayout Panel before adding any new dynamic buttons
            CategoryCodeTableLayoutPanel.Controls.Clear();

            // Loop the List<CategoryNode> to create the Button
            cnSearchCategoryNodes.ForEach(
                delegate(CategoryNode cnCategoryNode)
                {
                    Button cnDynamicButton = CreateButton(cnCategoryNode);
                    cnDynamicButton.Text = cnCategoryNode.CategoryCode.ToString().Substring(cnCategoryNode.Level - 1, 1) + " " + cnDynamicButton.Text;
                    CategoryCodeTableLayoutPanel.Controls.Add(cnDynamicButton);
                });
        }

        /// <summary>
        /// Build out the Level Nest Navigation
        /// </summary>
        /// <param name="sLevelNest"></param>
        private void CreateLevelNestNavigation(string sLevelNest)
        {
            // Set form property for use in secondary forms
            _LevelNest = sLevelNest;
            // Clear all navigation controls before populating
            nestFlowLayoutPanel.Controls.Clear();
            // If the Level is at Parent, do not display anything
            if (sLevelNest == "")
                return;

            // Loop and create the individual level navigation links
            string[] sLevelLinkInfo = sLevelNest.Split('|');
            if (sLevelLinkInfo.Length > 0)
            {
                for (int i = 0; i < sLevelLinkInfo.Length; i++)
                {
                    // Separator Symbol for form
                    Label NavigationSeparatorLabel = new Label
                    {
                        TextAlign = ContentAlignment.TopLeft,
                        Text = "> ",
                        Height = 22,
                        Width = 8,
                    };

                    string[] sLevelLink = sLevelLinkInfo[i].Split('#');

                    LinkLabel llLevelLink = new LinkLabel
                    {
                        Text = sLevelLink[1].ToString(),
                        Tag = sLevelLink[0].ToString(),
                        TextAlign = ContentAlignment.TopLeft,
                        Height = 22,
                        AutoSize = true
                    };
                    llLevelLink.Click += new System.EventHandler(controlClickEvent);

                    nestFlowLayoutPanel.Controls.Add(llLevelLink);
                    // Only add Link separator if not at end of For-Loop
                    if (i + 1 < sLevelLinkInfo.Length)
                        nestFlowLayoutPanel.Controls.Add(NavigationSeparatorLabel);
                }
            }
        }

        /// <summary>
        /// Get a specific Category Node by the passed Category Code
        /// </summary>
        /// <param name="scategoryCode"></param>
        /// <returns></returns>
        public void GetCategoryNode(int iCategoryCode)
        {
            //ResetFormFields();
            if (iCategoryCode == 0)
            {
                GetMerchandiseButtons(DesktopSession.CategoryXML.NextNode, "");
                return;
            }

            _LevelNest = "";
            CategoryNode cnCategoryNodeWalker = DesktopSession.CategoryXML.GetMerchandiseCategory(iCategoryCode.ToString(), ref _LevelNest);

            if (!DesktopSession.CategoryXML.Error)
            {
                if (DesktopSession.CategoryXML.Exists)
                {
                    // Check to see if current node has sub nodes.  If so generate Dynamic Buttons on form
                    if (cnCategoryNodeWalker.NextNode != null)
                    {
                        GetMerchandiseButtons(cnCategoryNodeWalker.NextNode, _LevelNest);
                    }
                    else
                    {
                        _FoundCategoryCode = cnCategoryNodeWalker.CategoryCode;
                        _FoundCategoryMask = cnCategoryNodeWalker.Masks;

                        // Create new instance of ProKnowMatch.
                        // Create Answer objects for Manufacturer and Model 
                        //if (!_ManufacturerModelMisMatch)
                        //{
                        Answer manufacturerAnswer = new Answer();
                        Answer modelAnswer = new Answer();

                        string tmpManufacturer = string.Empty;
                        string tmpModel = string.Empty;

                        if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                        {
                            tmpManufacturer = manufacturerTextBox.Text;
                            tmpModel = modelTextBox.Text;
                        }
                        else
                        {
                            tmpManufacturer = secondaryManufacturerTextBox.Text;
                            tmpModel = secondaryModelTextBox.Text;
                        }

                        manufacturerAnswer.AnswerText = string.IsNullOrEmpty(tmpManufacturer.Trim()) ? "N/A" : tmpManufacturer.Trim();
                        manufacturerAnswer.AnswerCode = _ProKnowMatch == null ? 999 : _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerCode;

                        modelAnswer.AnswerText = string.IsNullOrEmpty(tmpModel.Trim()) ? "N/A" : tmpModel;
                        modelAnswer.AnswerCode = _ProKnowMatch == null ? 999 : _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerCode;
                        //}
                        //SMurphy
                        if (_ProKnowMatch == null)
                        {
                            _ProKnowMatch = new ProKnowMatch();
                        }
                        _ProKnowMatch.manufacturerModelInfo = new List<Answer>();
                        _ProKnowMatch.manufacturerModelInfo.Add(manufacturerAnswer);
                        _ProKnowMatch.manufacturerModelInfo.Add(modelAnswer);

                        if (_ProKnowMatch.proKnowData != null)
                            _ProKnowMatch.transitionStatus = TransitionStatus.MAN_MODEL_PROKNOW;
                        else
                            _ProKnowMatch.transitionStatus = TransitionStatus.NO_PROKNOW;

                        if (_ActiveManufacturerModel == ActiveManufacturer.SECONDARY || VerifyAge(_FoundCategoryMask, _FoundCategoryCode, cnCategoryNodeWalker.Description))
                        {
                            CopyItemAmountsWhenReplacing();
                            if (!saleFlow)
                            {
                                if (purchaseFlow)
                                    DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePurchase.Items.Count - 1;
                                else
                                    DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePawnLoan.Items.Count - 1;

                                //DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePawnLoan.PawnItems.Count - 1;
                                DesktopSession.DescribeItemContext = _CurrentContext;
                                DesktopSession.DescribeItemSelectedProKnowMatch = _ProKnowMatch;
                                if (_CurrentContext == CurrentContext.PFI_MERGE ||
                                    _CurrentContext == CurrentContext.PFI_ADD ||
                                    _CurrentContext == CurrentContext.PFI_REPLACE)
                                    NavControlBox.CustomDetail = "DescribeItemPFI";
                                else
                                    NavControlBox.CustomDetail = "DescribeItem";

                                NavControlBox.IsCustom = true;
                            }
                            else
                            {
                                DesktopSession.DescribeItemPawnItemIndex = ((CustomerProductDataVO)DesktopSession.ActiveRetail).Items.Count - 1;
                                DesktopSession.DescribeItemContext = _CurrentContext;
                                DesktopSession.DescribeItemSelectedProKnowMatch = _ProKnowMatch;
                            }
                            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                        }
                        else
                            return;
                    }
                }
                else
                {
                    CategoryCodeTableLayoutPanel.Controls.Clear();
                    MessageBox.Show("Merchandise not found.", "Merchandise Lookup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (DesktopSession.CategoryXML.MerchandiseCount > 0)
                    {
                        GetMerchandiseButtons(DesktopSession.CategoryXML.NextNode, "");
                    }
                }
            }
            else
            {
                MessageBox.Show(DesktopSession.CategoryXML.ErrorMessage, "Category Load Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Dynamic Button Generator
        /// </summary>
        /// <param name="cnCategoryNode"></param>
        /// <returns></returns>
        private Button CreateButton(CategoryNode cnCategoryNode)
        {
            Button btnNewButton = new Button
            {
                Size = new Size(250, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = cnCategoryNode.Description,
                BackgroundImage = DesktopSession.ResourceProperties.oldvistabutton_blue,
                BackgroundImageLayout = ImageLayout.Stretch,
                Enabled = cnCategoryNode.CatAllowed,
                ForeColor = cancelFormButton.ForeColor,
                Font = cancelFormButton.Font,
                FlatStyle = FlatStyle.Flat,
                Name = "btn" + cnCategoryNode.CategoryCode,
                Tag = cnCategoryNode.CategoryCode
            };
            btnNewButton.Click += new System.EventHandler(controlClickEvent);
            return (btnNewButton);
        }

        /// <summary>
        /// Standard Event for dynamic controls to retrieve Category Code from Tag
        /// in retrieving associated Category Node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void controlClickEvent(object sender, System.EventArgs e)
        {
            // Create instance of Button
            Control lxButton = (Control)sender;
            GetCategoryNode(Convert.ToInt32(lxButton.Tag));
        }

        /// <summary>
        /// Retrieve the Category Node by Category Code Provided from Category Code Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findCategoryCodeButton_Click(object sender, EventArgs e)
        {
            if (categoryCodeTextBox.Text != "")
                GetCategoryNode(Convert.ToInt32(categoryCodeTextBox.Text));
        }

        /// <summary>
        /// Retrieves the next higher up Category Node level
        /// </summary>
        private void FindPreviousLevel()
        {
            visibileModelControls(false);
            if (nestFlowLayoutPanel.Controls.Count > 0)
            {
                int iCtrlIdx = nestFlowLayoutPanel.Controls.Count - 3 <= 1 ? 0 : nestFlowLayoutPanel.Controls.Count - 3;
                LinkLabel llLink = (LinkLabel)nestFlowLayoutPanel.Controls[iCtrlIdx];

                GetCategoryNode(Convert.ToInt32(llLink.Tag));
            }
        }

        /// <summary>
        /// Since the Category Buttons are dynamically created and passed Category Level Number on Button,
        /// select which button was "hot keyed" and invoke a simulated button click
        /// </summary>
        /// <param name="sKeyValue"></param>
        private void GetKeyedCategoryButton(string sKeyValue)
        {
            foreach (Control tmpControl in CategoryCodeTableLayoutPanel.Controls)
            {
                if (tmpControl.Text.Substring(0, 1) == sKeyValue)
                {
                    ((Button)tmpControl).PerformClick();
                    break;
                }
            }
        }

        /// <summary>
        /// Based upon User Key Press, determine what Category Node to retrieve
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DescribeMerchandise_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_UseHotNumberKeysBoolean)
                return;

            string sKeyValue = e.KeyChar.ToString();
            switch (sKeyValue)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    GetKeyedCategoryButton(sKeyValue);
                    break;
                case "=":
                    FindPreviousLevel();
                    break;
                case "-":
                    if (DesktopSession.CategoryXML.MerchandiseCount > 0)
                    {
                        GetMerchandiseButtons(DesktopSession.CategoryXML.NextNode, "");
                    }
                    break;
            }
        }

        private void DescribeMerchandise_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.Enter:
            //        if (_CurrentControlName != "" && this.Controls[_CurrentControlName].GetType().ToString() == "Button")
            //        {
            //            Button ctrlButton = (Button)this.Controls[_CurrentControlName];
            //            ctrlButton.PerformClick();
            //        }
            //        break;
            //    case Keys.Home:
            //        if (CategoryCodeTableLayoutPanel.Controls.Count > 0)
            //            CategoryCodeTableLayoutPanel.Controls[0].Focus();
            //        else
            //            CategoryCodeTableLayoutPanel.Focus();
            //        break;
            //    case Keys.End:
            //        cancelFormButton.Focus();
            //        break;
            //}
        }

        // Common handler to assign not to use Hot Number Keys when in certain Controls
        private void controlEnter(object sender, EventArgs e)
        {
            //double refresh resolves issue with blue outline (Utilities.BorderColor(this, textbox, Color.Blue);) not consistently working
            Refresh();
            Refresh();
            _UseHotNumberKeysBoolean = false;

            TextBox textbox = (TextBox)sender;
            Utilities.BorderColor(this, textbox, Color.Blue);
        }

        private void visibileModelControls(bool bTurnOn)
        {
            continueButton.Visible = bTurnOn;
            enterModelLabel.Visible = bTurnOn;
            modelListBox.Visible = bTurnOn;
            modelTextBox.Visible = bTurnOn;

            //PWNU00000103 SMurphy 3/29/2010 Problem with model textbox & 1st Cancel button being visible/enabled when they should not be
            if (!bTurnOn)
            {
                modelTextBox.Text = "";
                continueButton.Enabled = false;
                //modelListBox.Items.Clear();
            }
            else
            {
                if (String.IsNullOrEmpty(modelTextBox.Text))
                    continueButton.Enabled = false;
                else
                    continueButton.Enabled = true;
            }

            if (!bTurnOn)
                modelListBox.DataSource = null;
        }

        private void visibileSecondaryModelControls(bool bTurnManufactureOn, bool bTurnModelOn)
        {
            secondaryContinueButton.Visible = bTurnModelOn;
            secondaryManufacturerLabel.Visible = bTurnManufactureOn;
            secondaryManufacturerTextBox.Visible = bTurnManufactureOn;
            secondaryModelLabel.Visible = bTurnModelOn;
            modelListBox.Visible = bTurnModelOn;
            secondaryModelTextBox.Visible = bTurnModelOn;
            //SMurphy repeated code
            //modelListBox.Visible = bTurnModelOn;

            if (!String.IsNullOrEmpty(secondaryModelTextBox.Text))
                secondaryContinueButton.Enabled = true;
            else
                secondaryContinueButton.Enabled = false;

            if (!bTurnModelOn)
                modelListBox.DataSource = null;
        }

        private void PopulateSecondaryManufacturer(int iAttributeID)
        {
            string sErrorCode;
            string sErrorText;
            DataTable manufacturerTable;

            if (MerchandiseProcedures.ExecuteGetManufacturers(iAttributeID, out manufacturerTable, out sErrorCode, out sErrorText))
            {
                if (manufacturerTable.Rows.Count > 0)
                {
                    AutoCompleteStringCollection manufacturerDataCollection = new AutoCompleteStringCollection();
                    foreach (DataRow myRow in manufacturerTable.Rows)
                    {
                        manufacturerDataCollection.Add(myRow[0].ToString());
                    }
                    secondaryManufacturerTextBox.AutoCompleteCustomSource = manufacturerDataCollection;
                }
            }
        }

        private void enterModelListBox_Click(object sender, EventArgs e)
        {
            try
            {
                PairType<int, string> myItem = (PairType<int, string>)modelListBox.SelectedItem;
                if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                {
                    modelTextBox.Text = myItem.Right;
                    controlFocus = continueButton;
                }
                else
                {
                    secondaryModelTextBox.Text = myItem.Right;
                    controlFocus = secondaryContinueButton;
                }

                CategoryCodeTableLayoutPanel.Enabled = false;
                controlFocus.Focus();
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Exception thrown: {0}", eX);
                }
                BasicExceptionHandler.Instance.AddException("DescribeMerchandise", eX);
            }
        }

        private void ResetFormFields()
        {
            manufacturerTextBox.Text = "";
            secondaryManufacturerTextBox.Text = "";
            modelTextBox.Text = "";
            secondaryModelTextBox.Text = "";
            modelListBox.DataSource = null;
            this.CategoryCodeTableLayoutPanel.Enabled = true;

            visibileModelControls(false);
            visibileSecondaryModelControls(false, false);
        }

        //ProKnow methods
        private void GetProKnowDetails()
        {
            ProKnowDetails proKnowDetails = new ProKnowDetails(DesktopSession);

            ProKnowDetails.ProKnowLookupAction proKnowLookupAction;

            proKnowDetails.GetProKnowDetails(_ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText
                                             , _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerText
                                             , out _ReturnedManModelReplyType
                                             , out proKnowLookupAction);

            if (proKnowDetails.Error)
            {
                MessageBox.Show(proKnowDetails.ErrorMessage, "Continue Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (proKnowLookupAction)
            {
                case ProKnowDetails.ProKnowLookupAction.NO_MATCH_FOUND:
                    DialogResult resultDialogResult = MessageBox.Show("No category was found for this manufacturer and model.  Do you want to continue using these values?", "No Match Model", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (resultDialogResult == DialogResult.Yes)
                    {
                        //SMurphy
                        _UseHotNumberKeysBoolean = true;

                        if (_ActiveManufacturerModel == ActiveManufacturer.SECONDARY)
                        {
                            if (VerifyAge(0, 0, ""))
                            {
                                //SMurphy
                                //ManufacturerUpdate(_ActiveManufacturerModel, _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText, _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerCode);
                                //ManufacturerModelUpdate(_ActiveManufacturerModel, _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerText, 999);
                                ManufacturerUpdate(_ActiveManufacturerModel, string.IsNullOrEmpty(secondaryManufacturerTextBox.Text.Trim()) ? "N/A" : secondaryManufacturerTextBox.Text, 999);
                                ManufacturerModelUpdate(_ActiveManufacturerModel, string.IsNullOrEmpty(secondaryModelTextBox.Text.Trim()) ? "N/A" : secondaryModelTextBox.Text, 999);
                                //hit when hitting 2nd Continue and no Pronknow for 2nd Manu/Model
                                //_ProKnowMatch.transitionStatus = TransitionStatus.MAN_MODEL_NO_PROKNOW;
                                _ProKnowMatch.transitionStatus = TransitionStatus.MAN_MODEL_PROKNOW;
                                if (!purchaseFlow)
                                    DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePawnLoan.Items.Count - 1;
                                else
                                    DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePurchase.Items.Count - 1;
                                DesktopSession.DescribeItemContext = _CurrentContext;
                                DesktopSession.DescribeItemSelectedProKnowMatch = _ProKnowMatch;
                                NavControlBox.CustomDetail = "DescribeItem";
                                NavControlBox.IsCustom = true;
                                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                            }
                        }
                        else
                        {
                            ManufacturerUpdate(_ActiveManufacturerModel, _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText, _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerCode);
                            ManufacturerModelUpdate(_ActiveManufacturerModel, string.IsNullOrEmpty(_ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerText.Trim()) ? "N/A" :
                                                                              _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerText.Trim(), 999);
                            _ProKnowMatch.transitionStatus = TransitionStatus.MAN_MODEL_NO_PROKNOW;

                            MessageBox.Show("Use the Category buttons below to proceed.", "Manual Categorize Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //Refresh();
                            controlFocus = null;
                            CategoryCodeTableLayoutPanel.Enabled = true;
                            CategoryCodeTableLayoutPanel.Focus();
                        }
                    }
                    else
                    {
                        if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                        {
                            //SMurphy PWNU00000103 3/29/2010 
                            modelTextBox.Text = string.Empty;
                            manufacturerTextBox.Text = string.Empty;
                            visibileModelControls(false);
                            controlFocus = manufacturerTextBox;
                        }
                        else
                        {
                            //SMurphy PWNU00000103 3/29/2010 
                            secondaryModelTextBox.Text = string.Empty;
                            secondaryManufacturerTextBox.Text = string.Empty;
                            visibileSecondaryModelControls(true, false);
                            controlFocus = secondaryManufacturerTextBox;
                        }
                        controlFocus.Focus();
                    }
                    break;

                case ProKnowDetails.ProKnowLookupAction.MATCH_FOUND:
                    //moved to continue and secondarycontinue button click event 
                    //manModelMatchType foundManModelMatchType = (manModelMatchType)_ReturnedManModelReplyType.serviceData.Items[0];
                    //ParseProKnowDetails(foundManModelMatchType, _ActiveManufacturerModel);

                    break;

                case ProKnowDetails.ProKnowLookupAction.MATCH_MULTI_FOUND:
                    List<manModelMatchType> manModelMatchTypes = new List<manModelMatchType>();
                    foreach (object oManModelMatchType in _ReturnedManModelReplyType.serviceData.Items)
                    {
                        manModelMatchType objectManModelMatchType = (manModelMatchType)oManModelMatchType;
                        manModelMatchTypes.Add(objectManModelMatchType);
                    }
                    if (_ProKnowMatch.transitionStatus != TransitionStatus.MAN_MODEL_PROKNOW_COMBO)
                    {
                        ManufacturerModelMatches myForm = new ManufacturerModelMatches(DesktopSession, ref _ActiveManufacturerModel)
                        {
                            ReturnForm = this,
                            SelectedManModelMatchTypes = manModelMatchTypes,
                            SelectedProKnowMatch = _ProKnowMatch
                        };

                        myForm.ShowDialog(_OwnerForm);
                    }
                    //never hit
                    //else
                    //{
                    //    //ParseProKnowDetails(manModelMatchTypes[0], _ActiveManufacturerModel);
                    //}
                    break;
            }
        }

        private void ParseProKnowDetails(manModelMatchType selectedManModelMatchType, ActiveManufacturer _ActiveManufacturerModel)
        {
            ProKnowDetails proKnowDetails = new ProKnowDetails(DesktopSession);
            ProKnowDetails.ProKnowLookupAction proKnowLookupAction;
            proKnowDetails.ParseProKnowDetails(ref _ProKnowMatch, ref _LevelNest, selectedManModelMatchType, _ActiveManufacturerModel, out proKnowLookupAction);

            switch (proKnowLookupAction)
            {
                case ProKnowDetails.ProKnowLookupAction.SECONDARY:

                    manufacturerTextBox.Enabled = true;
                    modelTextBox.Enabled = false;
                    continueButton.Enabled = false;
                    _Models.Clear();

                    // Make the Secondary Manufacturer / Model / Continue Controls visible
                    int iAttributeID = _ProKnowMatch.manufacturerModelInfo[2].AnswerCode;

                    // Get Merchandise Manufacturers from MainDesktop WebService Pull
                    PopulateSecondaryManufacturer(iAttributeID);

                    visibileSecondaryModelControls(true, false);
                    secondaryManufacturerLabel.Text = _ProKnowMatch.manufacturerModelInfo[2].AnswerText;
                    secondaryModelLabel.Text = _ProKnowMatch.manufacturerModelInfo[3].AnswerText;
                    break;

                case ProKnowDetails.ProKnowLookupAction.DESCRIBE_ITEM_DOUBLE:
                    //SMurphy problem when proknow answer id not in db PWNU00000602 & PWNU00000632
                    DoubleItem = true;
                    string errorAnswerCode = string.Empty;
                    string errorAnswerText = string.Empty;
                    int answerCodeToCheck = _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerCode;
                    if (!MerchandiseProcedures.IsAnswerIDValid(_ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerCode,
                                                               out errorAnswerCode, out errorAnswerText))
                    {
                        //write to log
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, errorAnswerText + " " + errorAnswerCode);
                        //replace answer code
                        ManufacturerModelUpdate(_ActiveManufacturerModel,
                                                string.IsNullOrEmpty(_ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerText.Trim()) ? "N/A" :
                                                _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel + 1].AnswerText.Trim(), 999);
                        int idx = _ProKnowMatch.preAnsweredQuestions.FindIndex(pk => pk.AnswerCode == answerCodeToCheck);
                        if (idx >= 0)
                            _ProKnowMatch.preAnsweredQuestions.RemoveAt(idx);
                        _ProKnowMatch.transitionStatus = TransitionStatus.NO_PROKNOW;
                    }

                    if (_ProKnowMatch.transitionStatus ==
                        TransitionStatus.MAN_MODEL_PROKNOW_COMBO)
                    {
                        if (VerifyAge(0, 0, ""))
                        {
                            //hit when 2nd continue button pressed
                            if (!saleFlow)
                            {
                                if (!purchaseFlow)
                                    DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePawnLoan.Items.Count - 1;
                                else
                                    DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePurchase.Items.Count - 1;
                            }
                            else
                                DesktopSession.DescribeItemPawnItemIndex = 0;
                            DesktopSession.DescribeItemContext = _CurrentContext;
                            DesktopSession.DescribeItemSelectedProKnowMatch = _ProKnowMatch;
                            if (_CurrentContext == CurrentContext.PFI_MERGE ||
                                _CurrentContext == CurrentContext.PFI_ADD ||
                                _CurrentContext == CurrentContext.PFI_REPLACE)
                                NavControlBox.CustomDetail = "DescribeItemPFI";
                            else
                                NavControlBox.CustomDetail = "DescribeItem";
                            NavControlBox.IsCustom = true;
                            if (DesktopSession.GenerateTemporaryICN)
                            {
                                NavControlBox.IsCustom = true;
                                NavControlBox.CustomDetail = "TemporaryICN";
                            }

                            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                        }
                    }
                    else
                    {
                        if (VerifyAge(_ProKnowMatch.primaryMaskPointer, _ProKnowMatch.primaryCategoryCode, _ProKnowMatch.primaryCategoryCodeDescription))
                        {
                            //hit twice when possible 2nd category and no 2nd manu/model is selected
                            //hit when tabbing out of 1st model
                            //hit when pushing 1st Continue
                            CopyItemAmountsWhenReplacing();
                            if (!saleFlow)
                            {
                                if (!purchaseFlow)
                                    DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePawnLoan.Items.Count - 1;
                                else
                                    DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePurchase.Items.Count - 1;
                            }
                            else
                                DesktopSession.DescribeItemPawnItemIndex = 0;

                            //DesktopSession.DescribeItemPawnItemIndex = DesktopSession.ActivePawnLoan.PawnItems.Count - 1;
                            DesktopSession.DescribeItemContext = _CurrentContext;
                            DesktopSession.DescribeItemSelectedProKnowMatch = _ProKnowMatch;
                            if (_CurrentContext == CurrentContext.PFI_MERGE ||
                                _CurrentContext == CurrentContext.PFI_ADD ||
                                _CurrentContext == CurrentContext.PFI_REPLACE)
                                NavControlBox.CustomDetail = "DescribeItemPFI";
                            else
                                NavControlBox.CustomDetail = "DescribeItem";
                            NavControlBox.IsCustom = true;
                            if (DesktopSession.GenerateTemporaryICN)
                            {
                                NavControlBox.IsCustom = true;
                                NavControlBox.CustomDetail = "TemporaryICN";
                            }

                            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                        }
                    }
                    break;
            }
        }

        private void CopyItemAmountsWhenReplacing()
        {
            if (_CurrentContext == CurrentContext.PFI_REPLACE || _CurrentContext == CurrentContext.GUNREPLACE)
            {
                if (!purchaseFlow)
                {
                    Item originalPawnItem = DesktopSession.ActivePawnLoan.Items[DesktopSession.DescribeItemPawnItemIndex];
                    Item replacementPawnItem = DesktopSession.ActivePawnLoan.Items[DesktopSession.ActivePawnLoan.Items.Count - 1];
                    replacementPawnItem.mYear = originalPawnItem.mYear;
                    replacementPawnItem.mStore = originalPawnItem.mStore;
                    replacementPawnItem.ItemAmount = originalPawnItem.ItemAmount;
                    replacementPawnItem.ItemAmount_Original = originalPawnItem.ItemAmount_Original;
                }
                else
                {
                    Item originalPawnItem = DesktopSession.ActivePurchase.Items[DesktopSession.DescribeItemPawnItemIndex];
                    Item replacementPawnItem = DesktopSession.ActivePurchase.Items[DesktopSession.ActivePurchase.Items.Count - 1];
                    replacementPawnItem.mYear = originalPawnItem.mYear;
                    replacementPawnItem.mStore = originalPawnItem.mStore;
                    replacementPawnItem.ItemAmount = originalPawnItem.ItemAmount;
                    replacementPawnItem.ItemAmount_Original = originalPawnItem.ItemAmount_Original;
                }
            }
        }

        // Standard Method to update the ProKnow Manufacturer information
        private void ManufacturerUpdate(ActiveManufacturer currentManufacturer, string sAnswerText, int iAnswerCode)
        {
            Answer updatedAnswer = _ProKnowMatch.manufacturerModelInfo[(int)currentManufacturer];
            updatedAnswer.AnswerText = sAnswerText;
            updatedAnswer.AnswerCode = iAnswerCode;

            _ProKnowMatch.manufacturerModelInfo.RemoveAt((int)currentManufacturer);
            _ProKnowMatch.manufacturerModelInfo.Insert((int)currentManufacturer, updatedAnswer);
        }

        // Standard Method to update the ProKnow Manufacturer's Model information
        private void ManufacturerModelUpdate(ActiveManufacturer currentManufacturer, string sAnswerText, int iAnswerCode)
        {
            Answer updatedAnswer = _ProKnowMatch.manufacturerModelInfo[(int)currentManufacturer + 1];
            updatedAnswer.AnswerText = sAnswerText;
            updatedAnswer.AnswerCode = iAnswerCode;

            _ProKnowMatch.manufacturerModelInfo.RemoveAt((int)currentManufacturer + 1);
            _ProKnowMatch.manufacturerModelInfo.Insert((int)currentManufacturer + 1, updatedAnswer);
        }

        private void GetAlternateManufacturer(TextBox manuTextbox)
        {
            if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                visibileModelControls(false);
            else
                visibileSecondaryModelControls(true, false);

            if (_ProKnowMatch == null)
            {
                _ProKnowMatch = new ProKnowMatch();

                Answer manufacturerAnswer = new Answer();
                manufacturerAnswer.AnswerText = string.IsNullOrEmpty(manufacturerTextBox.Text.Trim()) ? "N/A" : manufacturerTextBox.Text.Trim();
                manufacturerAnswer.AnswerCode = 999;

                Answer modelAnswer = new Answer();
                modelAnswer.AnswerText = string.IsNullOrEmpty(this.modelTextBox.Text.Trim()) ? "N/A" : this.modelTextBox.Text;
                modelAnswer.AnswerCode = 999;

                _ProKnowMatch.transitionStatus = TransitionStatus.NO_PROKNOW;

                if (_ProKnowMatch.manufacturerModelInfo == null)
                    _ProKnowMatch.manufacturerModelInfo = new List<Answer>();

                _ProKnowMatch.manufacturerModelInfo.Add(manufacturerAnswer);
                _ProKnowMatch.manufacturerModelInfo.Add(modelAnswer);
            }
            else
            {
                ManufacturerUpdate(_ActiveManufacturerModel, _ActiveManufacturerModel == ActiveManufacturer.PRIMARY ? manufacturerTextBox.Text : secondaryManufacturerTextBox.Text, 0);
            }

            alternameManufacturerReplyType myReply = DesktopSession.CallProKnow.GetAlternateManufacturer(_ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText);

            if (DesktopSession.CallProKnow.Error || myReply.serviceData == null)
            {
                MessageBox.Show("Pro-Know is experiencing technical difficulties.  Please manually categorize items", "ProKnow Lookup Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                ManufacturerUpdate(_ActiveManufacturerModel, string.IsNullOrEmpty(_ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText.Trim()) ? "N/A" :
                                                             _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText.Trim(), 0);
                ManufacturerModelUpdate(_ActiveManufacturerModel, "N/A", 999);
                _ProKnowMatch.transitionStatus = TransitionStatus.MANUFACTURER_ONLY;
                //return;
            }
            else
            {
                if (myReply.serviceData.Items.Length == 1)
                {
                    if (myReply.serviceData.Items[0].GetType() == typeof(businessExceptionType))
                    {
                        businessExceptionType statusBusinessExceptionType = (businessExceptionType)myReply.serviceData.Items[0];
                        if (statusBusinessExceptionType.responseCode == "NO_ALTERNATES_FOUND")
                        {
                            DataRow[] myDataRows = _ManufacturerData.Select("ANS_DESC='" + _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText.Replace("'", "") + "'");
                            if (myDataRows.Length == 0)
                            {
                                ManufacturerUpdate(_ActiveManufacturerModel,
                                                   string.IsNullOrEmpty(_ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText.Trim()) ? "N/A" :
                                                   _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText.Trim(), 999);
                            }
                            GetModels();
                        }
                        return;
                    }
                }

                if (myReply.serviceData.Items.Length >= 1 && !bypassAltManu)
                {
                    if (myReply.serviceData.Items[0].GetType() == typeof(manModType))
                    {
                        List<manModType> manModels = new List<manModType>();
                        foreach (object oManModel in myReply.serviceData.Items)
                        {
                            manModType objectManModel = (manModType)oManModel;
                            manModels.Add(objectManModel);
                        }

                        AlternateManufacturer myForm = new AlternateManufacturer(DesktopSession, ref _ActiveManufacturerModel)
                        {
                            ManModTypes = manModels,
                            SelectedProKnowMatch = _ProKnowMatch
                        };

                        myForm.ShowDialog(_OwnerForm);
                        manuTextbox.Text = _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText;

                        GetModels();
                    }
                }
            }
        }

        private void GetModels()
        {
            getModelsReplyType myReply = DesktopSession.CallProKnow.GetModels(_ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText);

            //PWNU00000103 3/29/2010 Smurphy last ditch attempt to bypass alt when prompt to continue = No
            bypassAltManu = false;

            _Models.Clear();

            if (DesktopSession.CallProKnow.Error || myReply.serviceData == null)
            {
                MessageBox.Show("Pro-Know is experiencing technical difficulties.  Please manually categorize items", "ProKnow Lookup Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                ManufacturerUpdate(_ActiveManufacturerModel, _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText, _ProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerCode);
                ManufacturerModelUpdate(_ActiveManufacturerModel, "N/A", 999);
                _ProKnowMatch.transitionStatus = TransitionStatus.MANUFACTURER_ONLY;
            }
            else
            {
                if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                    visibileModelControls(true);
                else
                    visibileSecondaryModelControls(true, true);

                if (myReply.serviceData.Items.Length == 1)
                {
                    if (myReply.serviceData.Items[0].GetType() == typeof(businessExceptionType))
                    {
                        businessExceptionType statusBusinessExceptionType = (businessExceptionType)myReply.serviceData.Items[0];
                        if (statusBusinessExceptionType.responseCode == "NO_MODELS_FOUND")
                        {
                            modelListBox.Visible = false;

                            DialogResult resultDialogResult = MessageBox.Show("No Models were found for this manufacturer.  Do you want to continue using this value?  Yes/No?", "No Match Model", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (resultDialogResult == DialogResult.Yes)
                            {
                                if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                                {
                                    continueButton.Enabled = true;
                                    controlFocus = continueButton;
                                }
                                else
                                {
                                    secondaryContinueButton.Enabled = true;
                                    controlFocus = secondaryContinueButton;
                                }
                            }
                            else
                            {
                                if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                                {
                                    visibileModelControls(false);
                                    bypassAltManu = true;
                                    manufacturerTextBox.Text = string.Empty;
                                    controlFocus = manufacturerTextBox;
                                }
                                else
                                {
                                    secondaryContinueButton.Enabled = false;
                                    secondaryManufacturerTextBox.Text = string.Empty;
                                    controlFocus = secondaryManufacturerTextBox;
                                }
                            }
                        }
                        return;
                    }
                }
                if (myReply.serviceData.Items.Length >= 1)
                {
                    if (myReply.serviceData.Items[0].GetType() == typeof(manModType))
                    {
                        foreach (object oManModel in myReply.serviceData.Items)
                        {
                            manModType objectManModel = (manModType)oManModel;
                            PairType<int, string> myItem = new PairType<int, string>(objectManModel.answerNumber, objectManModel.description);
                            _Models.Add(myItem);
                        }
                        populateManufacturerModels("");
                    }
                    else
                    {
                        // Make invisible the Model ListBox since no Items
                        if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                            visibileModelControls(false);
                        else
                            visibileSecondaryModelControls(false, false);
                    }
                }
            }
        }

        private void populateManufacturerModels(string sFilterString)
        {
            modelListBox.DataSource = null;

            if (_Models.Count > 0)
            {
                List<PairType<int, string>> filteredListPairType = new List<PairType<int, string>>();

                foreach (PairType<int, string> myPair in _Models)
                {
                    if (string.IsNullOrEmpty(sFilterString))
                    {
                        filteredListPairType.Add(myPair);
                    }
                    else
                    {
                        string sFilterString2 = sFilterString;
                        sFilterString2 = Regex.Replace(myPair.Right, @"[\W]", "");
                        if (myPair.Right.StartsWith(sFilterString.ToUpper()) || sFilterString2.StartsWith(sFilterString.ToUpper()))
                        {
                            filteredListPairType.Add(myPair);
                        }
                    }
                }

                if (filteredListPairType.Count > 0)
                {
                    // Add Manufacturer _Models to ListBox
                    modelListBox.BeginUpdate();
                    modelListBox.DataSource = filteredListPairType;
                    modelListBox.DisplayMember = "Right";
                    modelListBox.ValueMember = "Left";
                    modelListBox.EndUpdate();

                    if (_ActiveManufacturerModel == ActiveManufacturer.PRIMARY)
                        visibileModelControls(true);
                    else
                        visibileSecondaryModelControls(true, true);
                }
            }
            else
            {
                modelListBox.Visible = false;
            }
        }

        private bool VerifyAge(int iCategoryMask, int iCategoryCode, string sDescription)
        {
            bool bAddedToLoan = false;
            if (DesktopSession.GenerateTemporaryICN)
            {
                if (iCategoryCode >= 4000 && iCategoryCode <= 5000)
                {
                    // DAndrews 1/25/2011: Bugzilla Bug #114
                    //MessageBox.Show("Cannot create temporary ICN for firearm");
                    MessageBox.Show("Temporary ICN not allowed for Firearms");
                    return false;
                }
            }
            if (gunEditFlow)
            {
                if (iCategoryCode < 4000 || iCategoryCode > 5000)
                {
                    MessageBox.Show("Cannot replace firearm with other merchandise");
                    return false;
                }
            }

            Item _PawnItem = new Item();
            

            if (iCategoryMask > 0)
            {
                DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);
                if (sDescription != "")
                    dmPawnItem.SelectedPawnItem.CategoryDescription = sDescription;
                _PawnItem = dmPawnItem.SelectedPawnItem;

                if (saleFlow)
                {
                    if (DesktopSession.GenerateTemporaryICN)
                    {
                        _PawnItem.mItemOrder = DesktopSession.ActiveRetail.Items.Count + 1;
                        NavControlBox.IsCustom = true;
                        NavControlBox.CustomDetail = "TemporaryICN";
                    }
                    else
                    {
                        ItemSearchCriteria currentItemSearch = new ItemSearchCriteria();
                        currentItemSearch.CategoryDescription = sDescription;
                        currentItemSearch.CategoryID = iCategoryCode;
                        if (_ProKnowMatch.manufacturerModelInfo != null)
                        {
                            currentItemSearch.Manufacturer = _ProKnowMatch.manufacturerModelInfo[0].AnswerText;
                            currentItemSearch.Model = _ProKnowMatch.manufacturerModelInfo[1].AnswerText;
                        }
                        DesktopSession.ActiveItemSearchData = currentItemSearch;
                    }
                }
                else if (purchaseFlow)
                    _PawnItem.mItemOrder = DesktopSession.ActivePurchase.Items.Count + 1;
                else if (!gunEditFlow)
                    _PawnItem.mItemOrder = DesktopSession.ActivePawnLoan.Items.Count + 1;
                if (iCategoryCode > 0)
                    _PawnItem.CategoryCode = iCategoryCode;
                if (purchaseFlow)
                    DesktopSession.ActivePurchase.Items.Add(_PawnItem);
                else if (saleFlow && DesktopSession.GenerateTemporaryICN)
                {
                    _PawnItem.mDocType = "8";
                    ((CustomerProductDataVO)DesktopSession.ActiveRetail).Items.Add(_PawnItem);
                }
                else if (!saleFlow && !purchaseFlow && !gunEditFlow)
                    DesktopSession.ActivePawnLoan.Items.Add(_PawnItem);
                else if (gunEditFlow)
                {
                    _PawnItem.Icn = DesktopSession.ActivePawnLoan.Items[0].Icn;
                    _PawnItem.GunNumber = DesktopSession.ActivePawnLoan.Items[0].GunNumber;
                    _PawnItem.mDocNumber = DesktopSession.ActivePawnLoan.Items[0].mDocNumber;
                    _PawnItem.mItemOrder = DesktopSession.ActivePawnLoan.Items[0].mItemOrder;
                    _PawnItem.mStore = DesktopSession.ActivePawnLoan.Items[0].mStore;
                    _PawnItem.mDocType = DesktopSession.ActivePawnLoan.Items[0].mDocType;
                    _PawnItem.mYear = DesktopSession.ActivePawnLoan.Items[0].mYear;

                    DesktopSession.ActivePawnLoan.Items.RemoveAt(0);
                    DesktopSession.ActivePawnLoan.Items.Add(_PawnItem);
                }
                bAddedToLoan = true;
            }
            else
            {
                if (!purchaseFlow && !saleFlow)
                    _PawnItem = DesktopSession.ActivePawnLoan.Items[DesktopSession.ActivePawnLoan.Items.Count - 1];
                else if (!saleFlow)
                    _PawnItem = DesktopSession.ActivePurchase.Items[DesktopSession.ActivePurchase.Items.Count - 1];
            }

            _PawnItem.IsGun = Utilities.IsGun(_PawnItem.GunNumber, _PawnItem.CategoryCode, _PawnItem.IsJewelry, _PawnItem.MerchandiseType);
            //SR 11/11/2009
            //When the context is PFI we do not need to check for age
            //since this is not a customer based process
            if (_CurrentContext == CurrentContext.PFI_MERGE ||
                _CurrentContext == CurrentContext.PFI_ADD ||
                _CurrentContext == CurrentContext.PFI_REPLACE ||
                _CurrentContext == CurrentContext.GUNREPLACE)
            {
                _PawnItem.mYear = this.purchaseFlow ? DesktopSession.ActivePurchase.Items[0].mYear : DesktopSession.ActivePawnLoan.Items[0].mYear;
                return true;
            }
            //No need to check for age if this is a vendor purchase flow
            //or if its a sale flow
            if (vendorPurchaseFlow || saleFlow || gunEditFlow)
                return true;

            if (_PawnItem.IsGun
                || _PawnItem.MerchandiseType == "H"
                || _PawnItem.MerchandiseType == "L"
            )
            {
                if (this.DesktopSession.ActiveCustomer != null)
                {
                    if (string.IsNullOrEmpty(this.DesktopSession.ActiveCustomer.CustomerNumber))
                    {
                        //If customer is not identified show warning message
                        if (_PawnItem.MerchandiseType == "H")
                        {
                            MessageBox.Show(
                                            String.Format("The age limit for pawning a handgun is {0}",
                                                          CustomerProcedures.getHandGunValidAge(this.DesktopSession)), "Age Verification",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //return false;
                        }
                        if (_PawnItem.MerchandiseType == "L")
                        {
                            MessageBox.Show(
                                            String.Format("The age limit for pawning a long gun is {0}",
                                                          CustomerProcedures.getLongGunValidAge(this.DesktopSession)), "Age Verification",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //return false;
                        }
                    }
                    else
                    {
                        if (_PawnItem.MerchandiseType == "H")
                        {
                            if (this.DesktopSession.ActiveCustomer.Age < Convert.ToInt16(CustomerProcedures.getHandGunValidAge(this.DesktopSession)))
                            {
                                MessageBox.Show(
                                                this.purchaseFlow
                                                        ? "This customer does not meet the age requirements for this buy"
                                                        : "This customer does not meet the age requirements to pawn or pickup this item",
                                                "Age Verification",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                if (bAddedToLoan)
                                {
                                    if (this.purchaseFlow)
                                        this.DesktopSession.ActivePurchase.Items.RemoveAt(this.DesktopSession.ActivePurchase.Items.Count - 1);
                                    else
                                        this.DesktopSession.ActivePawnLoan.Items.RemoveAt(this.DesktopSession.ActivePawnLoan.Items.Count - 1);
                                }
                                return false;
                            }
                        }
                        else if (_PawnItem.MerchandiseType == "L")
                        {
                            if (this.DesktopSession.ActiveCustomer.Age < Convert.ToInt16(CustomerProcedures.getLongGunValidAge(this.DesktopSession)))
                            {
                                MessageBox.Show(
                                                this.purchaseFlow
                                                        ? "This customer does not meet the age requirements for this buy"
                                                        : "This customer does not meet the age requirements to pawn or pickup this item",
                                                "Age Verification",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                if (bAddedToLoan)
                                {
                                    if (this.purchaseFlow)
                                        this.DesktopSession.ActivePurchase.Items.RemoveAt(this.DesktopSession.ActivePurchase.Items.Count - 1);
                                    else
                                        this.DesktopSession.ActivePawnLoan.Items.RemoveAt(this.DesktopSession.ActivePawnLoan.Items.Count - 1);
                                }

                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void CategoryCodeTableLayoutPanel_Enter(object sender, EventArgs e)
        {
            if (controlFocus == null)
            {
                _UseHotNumberKeysBoolean = true;
                Refresh();
            }
            else
            {
                controlFocus.Focus();
            }
        }
    }
}
