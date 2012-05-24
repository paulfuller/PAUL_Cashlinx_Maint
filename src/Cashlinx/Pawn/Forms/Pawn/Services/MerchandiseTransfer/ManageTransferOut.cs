/**************************************************************************
* PFI_Verify.cs 
* 
* History:
*  no ticket SMurphy 5/6/2010 issues with PFI Merge, PFI Add and navigation
* DAndrews 2/7/2011 Refactored to work with Refurb and Excess items as well as Scrap.
* Jkings 05/03/2011 Fix for 561
* EWaltmon 10/12/2011 BZ#1307
**************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CashlinxDesktop.UserControls;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic;
using Reports;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    /// <summary>
    /// Used to transfer Catco, Refurb, Scrap merchandise, etc.
    /// </summary>
    public partial class ManageTransferOut : Form
    {
        #region Private Members
        private const string _NO_SELECTION = "Select";
        private const string _CATCO = "Catco";
        private const string _SHOP_NO = "Shop No.";
        private const string _GUN_ROOM = "Gun Room";
        private const string _OUT_OF_SHOP = "Out of Shop";

        private const string _SCRAP = "Scrap";
        private const string _REFURB = "Refurb";
        private const string _EXCESS = "Excess";
        private const string _APPRAISAL = "Appraisal";
        private const string _WHOLESALE = "Wholesale";

        private const int AppraisalTransFacNumber = 73;
        private const string AppraisalTransFacName = "Appraisal";
        private const string AppraisalTransferManagerName = "Carolyn Blanchard";
        private const string AppraisalTransferStorePhone = "817-927-8098";
        private const string AppraisalTransferFax = "817-654-7001";

        private const int WholesaleTransFacNumber = 72;
        private const string WholesaleTransFacName = "Wholesale";
        private const string WholesaleTransferManagerName = "Carolyn Blanchard";
        private const string WholesaleTransferStorePhone = "817-927-8098";
        private const string WholesaleTransferFax = "817-654-7001";

        private const int ScrapTransFacNumber = 64;
        private const string ScrapTransFacName = "Jewelry Support";
        private const string ScrapTransferManagerName = "Carolyn Blanchard";
        private const string ScrapTransferStorePhone = "817-927-8098";
        //private const int ExcessTransferFacilityNumber = 71;
        //Fix for 687
        private const int ExcessTransferFacilityNumber = 71;
        private const string ExcessTransferFacilityName = "Jewelry Excess";
        private const string ExcessTransferManagerName = "Carolyn Blanchard";
        private const string ExcessTransferStorePhone = "817-927-8098";

        private const int RefurbTransferFacilityNumber = 68;
        private const string RefurbTransferFacilityName = "Jewelry Refurb";
        private const string RefurbTransferManagerName = "Lori Dove";
        private const string RefurbTransferStorePhone = "817-927-8098";

        private string _transferTypeDeptName = string.Empty;
        private string _transferTypeDeptManagerName = string.Empty;
        private string _transferTypeDeptManagerPhone = string.Empty;
        private int TransferTypeNumber = 0;

        private string _transferType = "";
        private string _carrier = "";
        private string _catcoType = "";
        private TransferType _transferTypeList;
        private TransferOutCount _transferOutCount;
        private Carrier _carrierList = null;

        private List<IItem> _availableItems = null;
        private List<IItem> _itemsToTransfer = null;
        private int _maxItemsPerTransfer = 0;
        private string _shopNoToTransfer = null;
        private bool isClxToClx = false;

        private ShopNumber _shopNumberComp = null;
        private GunRoomType _gunRooomComp = null;
        private Dictionary<string, string> topsShopInfoDict = null;
        private Dictionary<string, string> clxShopInfoDict = null;

        private AutoCompleteStringCollection autoComp = null;

        private bool isATFStore = false;
        private string _toShopName = null;
        #endregion Private Members


        #region Private Properties

        /// <summary>
        /// Returns true if all conditions are met to allow items to be transferred, including at least 1 item selected for transfer.
        /// </summary>
        private bool CanContinue
        {
            get
            {
                bool retValue = true; //assume the best; set to false if condition exists to prevent transfer

                // Carrier list has a selection?
                if (_carrierList != null //is null if not used/displayed
                    && _carrierList.CarrierList.SelectedIndex > -1
                    && (
                    String.IsNullOrEmpty(_carrier)
                    || _carrier.Equals(_NO_SELECTION, StringComparison.CurrentCultureIgnoreCase)
                    )
                )
                {
                    retValue = false;
                }

                // Transfer To list has a selection?
                if (String.IsNullOrEmpty(_transferType)
                    || _transferType.Equals(_NO_SELECTION, StringComparison.CurrentCultureIgnoreCase))
                {
                    retValue = false;
                }
                else if (_transferType.Equals(_CATCO, StringComparison.CurrentCultureIgnoreCase))
                {
                    // Transfer Type list has a selection?
                    if (String.IsNullOrEmpty(_catcoType)
                        || _catcoType.Equals(_NO_SELECTION, StringComparison.CurrentCultureIgnoreCase))
                    {
                        retValue = false;
                    }
                }
                else if (_transferType.Equals(_SHOP_NO, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (String.IsNullOrEmpty(_shopNoToTransfer))
                    {
                        retValue = false;
                    }
                    else if (GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber.Equals(_shopNoToTransfer)) //transfer to same shop disable continue
                    {
                        retValue = false;
                    }

                    /*if (gunTransferValidation() == false)
                        retValue = false;*/
                }
                else if (_transferType.Equals(_GUN_ROOM, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (String.IsNullOrEmpty(this._gunRooomComp.getSelectedFacility()) || "Select".Equals(this._gunRooomComp.getSelectedFacility()))
                    {
                        retValue = false;
                    }
                }
                else
                {
                    //TODO: implementation for non-CATCO selections
                    // Transfer Type list has a selection?
                    retValue = false; //until implementation completed for non-CATCO types
                }

                // At least one item is selected for transfer?
                if (_itemsToTransfer == null || _itemsToTransfer.Count == 0)
                {
                    retValue = false;
                }

                return retValue;
            }
        }

        // Allow Gun transfer only to ATF stores

        private bool gunTransferValidation()
        {
            if (_toShopName == null) return true;

            if (_itemsToTransfer != null && _itemsToTransfer.Count > 0)
            {
                foreach (IItem item in _itemsToTransfer)
                {
                    if (item.IsGun)
                    {
                        if (isATFStore) return true;
                        else return false;
                    }
                }
            }
            return true;
        }

        private bool ShowCommentColumn
        {
            get
            {
                bool retValue = false;
                if (_itemsToTransfer != null && _itemsToTransfer.Count > 0)
                {
                    foreach (IItem item in _itemsToTransfer)
                    {
                        if (String.IsNullOrEmpty(item.Comment) == false)
                        {
                            retValue = true;
                            break; //exit loop if a comment is found with any item
                        }
                    }
                }
                return retValue;
            }
        }

        /// <summary>
        /// Determines whether Scrap items are being transferred.
        /// </summary>
        private bool IsScrapTransfer
        {
            get
            {
                return (
                String.IsNullOrEmpty(_transferType) == false
                && _transferType.Equals(_CATCO, StringComparison.CurrentCultureIgnoreCase)
                && String.IsNullOrEmpty(_catcoType) == false
                && _catcoType.Equals(_SCRAP, StringComparison.CurrentCultureIgnoreCase)
                );
            }
        }

        #endregion Private Properties

        #region Constructor

        public ManageTransferOut()
        {
            InitializeComponent();

            Setup();

            PrepareShopTextForAutoComplete();
        }

        #endregion Constructor

        #region Private Methods

        #region TestMethods
        private void CreateFakeScrapItems()
        {
            _availableItems = new List<IItem>();

            IItem i = new ScrapItem();
            i.Icn = "111111111111111";
            i.TicketDescription = "Desc1";
            i.ItemAmount = 11;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "020309002516100100";
            i.TicketDescription = "02030DESC";
            i.ItemAmount = 21;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "22222222222111111";
            i.TicketDescription = "Desc21";
            i.ItemAmount = 21;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "22222222222222";
            i.TicketDescription = "Desc2";
            i.ItemAmount = 22;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "333333333333333";
            i.TicketDescription = "Desc3";
            i.ItemAmount = 33;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "444444444444444";
            i.TicketDescription = "Desc4";
            i.ItemAmount = 44;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "555555555555555";
            i.TicketDescription = "Desc5";
            i.ItemAmount = 55;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "66666666666666";
            i.TicketDescription = "Desc6";
            i.ItemAmount = 66;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "7777777777777777";
            i.TicketDescription = "Desc7";
            i.ItemAmount = 77;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "88888888888888888";
            i.TicketDescription = "Desc8";
            i.ItemAmount = 88;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            i = new ScrapItem();
            i.Icn = "9999999999999999999";
            i.TicketDescription = "Desc9";
            i.ItemAmount = 99;
            i.ItemStatus = ProductStatus.PFI;
            _availableItems.Add(i);

            _itemsToTransfer = new List<IItem>();
        }

        #endregion TestMethods

        private void Init()
        {
            try
            {
                this.cmdICNSubmit.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                _itemsToTransfer = new List<IItem>();
                _availableItems = new List<IItem>();
                isClxToClx = false;
                this.icnLabel.Text = "Enter or Scan ICN#: ";

                if (!String.IsNullOrEmpty(_transferType))
                {
                    if (_transferType.Equals(_CATCO, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!string.IsNullOrEmpty(_catcoType) && !_catcoType.Equals(_NO_SELECTION, StringComparison.CurrentCultureIgnoreCase))
                        {
                            //Change label for ICN search to Refurb search incase of Refurb
                            if (_catcoType.Equals(_REFURB))
                            {
                                this.icnLabel.Text = "Refurb#:";
                            }

                            _transferOutCount.TransferTypeLabel = _catcoType;
                            string errorMessage = string.Empty;

                            if (!IsAppraisalOrWholesale())
                            {
                                //Retrieve CATCO items
                                _availableItems =
                                TransferProcedures.GetJsupItemsByStore(GlobalDataAccessor.Instance.DesktopSession,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    _catcoType,
                                    out errorMessage);
                            }

                            //Check for load error
                            if (String.IsNullOrEmpty(errorMessage) == false)
                            {
                                MessageBox.Show(errorMessage, "Load Items Available for Transfer");
                            }
                        }
                        else //no item (or "Select") selected
                        {
                            // Set TransferOut control's label (empty string forces default text.
                            _transferOutCount.TransferTypeLabel = String.Empty;
                        }
                        _transferOutCount.SetCounts(_availableItems.Count, _itemsToTransfer.Count);
                        this.cmdSearch.Visible = !IsAppraisalOrWholesale();
                    }
                    else
                    {
                        // Reset CATCO-related stuff.
                        if (_transferTypeList.TransferTypeList.Items.Count > 0)
                        {
                            //0 = "Select" = no selection
                            _transferTypeList.TransferTypeList.SelectedIndex = 0;
                        }
                        _catcoType = String.Empty;

                        // Retrieve non-Catco items based on selected type of merchandise to be transferred.
                        if (_transferType == _GUN_ROOM)
                        {
                            //TODO: implement GUN ROOM available items retrieval
                            this.Cursor = Cursors.WaitCursor;
                            _availableItems = this.getPFIItemForShopToGunRoom();
                            this.Cursor = Cursors.Default;
                            this.cmdSearch.Visible = true;
                        }
                        else if (_transferType == _SHOP_NO)
                        {
                            this.toTransLabel.Text = "Shop";
                            // initialize to shop when drop down changed
                            if (string.IsNullOrEmpty(_shopNoToTransfer))
                            {
                                resetToFields();
                            }
                            this.cmdSearch.Visible = false;
                        }
                        else if (_transferType == _OUT_OF_SHOP)
                        {
                            //TODO: implement OUT OF SHOP available items retrieval
                        }
                        else if (_transferType == "Select")
                        {
                            resetToFields();
                            this.cmdSearch.Visible = false;
                            CleanupLayoutTable();
                        }
                    }
                }//end if (String.IsNullOrEmpty(_transferType) == false)
            }
            catch (Exception exInit)
            {
                BasicExceptionHandler.Instance.AddException("ManageTransferOut Initialization Error",
                                                            new ApplicationException("ManageTransferOut.Init() Error", exInit));
            }
            finally
            {
                // Reset the cursor to the default.
                Cursor.Current = Cursors.Default;
            }
        }

        // This method gets the list of shops from SP and keeps the text ready to auto complete
        private void PrepareShopTextForAutoComplete()
        {
            if (topsShopInfoDict != null)
            {
                //Console.WriteLine("Value present returning");
                return;
            }
            string errorCode = null;
            string errorText = null;
            ShopProcedures.ExecuteGetStoreInfoWithShortName(GlobalDataAccessor.Instance.OracleDA,
                                                            out topsShopInfoDict, out clxShopInfoDict, out errorCode, out errorText);
            autoComp = new AutoCompleteStringCollection();

            List<string> keylist = new List<string>(topsShopInfoDict.Keys);
            // Loop through list and add all TOPS stores
            foreach (string key in keylist)
            {
                autoComp.Add(key);
                //Console.WriteLine(key + "," + shopInfoDict[key]);
            }

            // Loop through list and add all Cashlinx stores
            keylist = new List<string>(clxShopInfoDict.Keys);
            foreach (string key in keylist)
            {
                autoComp.Add(key);
            }
            //this._shopNumberComp.getShopNoTextObj().AutoCompleteCustomSource = autoComp;
        }

        private void Setup()
        {
            setToShopNameForCATCO(0, string.Empty, string.Empty, string.Empty, string.Empty);

            //Create class objects
            _transferTypeList = new TransferType();
            _transferOutCount = new TransferOutCount();
            _itemsToTransfer = new List<IItem>();

            //Hook into events
            this.transferToList1.TransferToList.SelectedIndexChanged += new EventHandler(TransferToList_SelectedIndexChanged);

            Init();

            //Get the business rule for max items per transfer. 
            _maxItemsPerTransfer =
            new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxItemsForTransfer(
                GlobalDataAccessor.Instance.CurrentSiteId);

            #region TestMethodCall
            //string methodToCall = BusinessRulesProcedures.GetTestMethod(GlobalDataAccessor.Instance.CurrentSiteId);

            //Type t = this.GetType();

            //MethodInfo info =  t.GetMethod(methodToCall);
            //if (info != null)
            //{
            //    int i = (int)info.Invoke(this, null);
            //}
            #endregion TestMethodCall

            GetStoreAddressInfo();
        }

        private void setToShopNameForCATCO(int storeNumber, string facilityName, string managerName, string phoneNumber, string faxNumber)
        {
            //Per Munta this is hard coded for now and we'll come back to it.
            //TLR 10/7/2010
            if (string.IsNullOrWhiteSpace(facilityName))
            {
                this.toTransLabel.Text = "Catco";
            }
            else
            {
                this.toTransLabel.Text = string.Format("Catco - {0} {1}", facilityName, storeNumber);
            }
            this.storeAddrLine1.Text = "1200 W BERRY";
            this.storeAddrLine2.Text = "FORT WORTH, TX 76118";
            this.storePhNo.Text = (string.IsNullOrEmpty(phoneNumber) ? "817-207-6019" : phoneNumber);
            this.storeMgrName.Text = managerName;
            this.lblStoreFax.Text = (string.IsNullOrEmpty(faxNumber) ? "817-207-6020" :  faxNumber);
        }

        private void setToShopNameForGunRoom()
        {
            //Per Munta this is hard coded for now and we'll come back to it.
            //TLR 10/7/2010
            this.toTransLabel.Text = "Gun Room";
            this.storeAddrLine1.Text = "555 Donut Ave.";
            this.storeAddrLine2.Text = "FORT WORTH, TX 76110";
            this.storePhNo.Text = "817-555-3330";
            this.storeMgrName.Text = "Kobe Bryant";
            this.lblStoreFax.Text = "817-555-3330";
        }

        private void resetToFields()
        {
            this.toTransLabel.Text = "";
            this.storeAddrLine1.Text = "";
            this.storeAddrLine2.Text = "";
            this.storeMgrName.Text = "";
            this.storePhNo.Text = "";
            this.lblStoreFax.Text = "";
        }

        #region TestMethodCall Method
        //public Int32 MethodCallTest()
        //{
        //    Console.WriteLine("Test method call called and tested!");
        //    return 42;
        //}
        #endregion  TestMethodCall Method

        private void GetStoreAddressInfo()
        {
            this.lblFrom1.Text = CashlinxDesktopSession.Instance.CurrentSiteId.StoreName;
            this.lblFrom2.Text = CashlinxDesktopSession.Instance.CurrentSiteId.StoreAddress1;
            this.lblFrom3.Text = CashlinxDesktopSession.Instance.CurrentSiteId.StoreCityName
                                 + ", " + CashlinxDesktopSession.Instance.CurrentSiteId.State
                                 + " " + CashlinxDesktopSession.Instance.CurrentSiteId.StoreZipCode;
            this.lblFromTelephone.Text = Commons.Format10And11CharacterPhoneNumberForUI(CashlinxDesktopSession.Instance.CurrentSiteId.StorePhoneNo);
            this.lblFromFax.Text = Commons.Format10And11CharacterPhoneNumberForUI(CashlinxDesktopSession.Instance.CurrentSiteId.StoreFaxNo);
        }

        /// <summary>
        /// Remove all controls from tblLayout2 and remove all controls from tblLayout except transferToList1.
        /// </summary>
        private void CleanupLayoutTable()
        {
            // Unhook events of controls being removed from tblLayout2.
            resetToFields();
            if (this.tblLayout2.Controls.Contains(_transferTypeList))
            {
                _transferTypeList.TransferTypeList.SelectedIndexChanged -= CatcoTypeList_SelectedIndexChanged;
            }
            //Clean out layout 2's -- all controls.
            this.tblLayout2.Controls.Clear();

            int ctrlCount = this.tblLayout.Controls.Count - 1;
            for (int i = 1; i <= ctrlCount; i++)
            {
                this.tblLayout.Controls.RemoveAt(1);
            }
        }

        private bool setClxToClxFlag(string shopNo)
        {
            bool returval = true;

            if (topsShopInfoDict.ContainsValue(shopNo))
            {
                isClxToClx = false;
            }
            else if (clxShopInfoDict.ContainsValue(shopNo))
            {
                isClxToClx = true;
            }
            else
            {
                return false;
            }
            return returval;
        }

        private void ShopNameChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string sChangedText = textBox.Text.ToUpper();
            int selStart = textBox.SelectionStart;

            textBox.SelectionStart = selStart;

            // To Do
            if (sChangedText != "" && topsShopInfoDict != null && topsShopInfoDict.Keys.Count > 0 && sChangedText.Length > 3)
            {
                if (topsShopInfoDict.ContainsKey(sChangedText))
                {
                    isClxToClx = false;
                    _shopNoToTransfer = topsShopInfoDict[sChangedText];
                    ShopNoEntered(sender, e);
                }
                else if (clxShopInfoDict.ContainsKey(sChangedText))
                {
                    _shopNoToTransfer = clxShopInfoDict[sChangedText];
                    isClxToClx = true;
                    ShopNoEntered(sender, e);
                }
                else
                {
                    _shopNoToTransfer = "";
                    this.continueButton.Enabled = this.CanContinue;
                }
            }
            else
            {
                _shopNoToTransfer = "";
                this.continueButton.Enabled = this.CanContinue;
            }
        }

        // This method will be used for in shop transfer to search a store
        private void ShopNoEntered(object sender, EventArgs e)
        {
            if (_shopNoToTransfer == null || _shopNoToTransfer.Length < 3) return;

            SiteId storeInfo = new SiteId();
            string errorCode = null;
            string errorText = null;
            ShopProcedures.ExecuteGetStoreInfo(GlobalDataAccessor.Instance.OracleDA,
                                               _shopNoToTransfer, ref storeInfo, out errorCode, out errorText);

            if (storeInfo != null && !(storeInfo.StoreAddress1.Equals("")))
            {
                //The Shop number entered does not exist.  Please try again.
                this._toShopName = storeInfo.StoreName;
                this.storeAddrLine1.Text = storeInfo.StoreAddress1;
                this.storeAddrLine2.Text = storeInfo.StoreCityName + "," + storeInfo.State + " " + storeInfo.StoreZipCode;
                this.storeMgrName.Text = storeInfo.StoreManager;
                this.storePhNo.Text = storeInfo.StorePhoneNo;
                this.lblStoreFax.Text = storeInfo.StoreFaxNo;
                if (!string.IsNullOrEmpty(storeInfo.FireArmLicenceNo))
                    isATFStore = true;
                else
                    isATFStore = false;
                // Enable continue on Shop selection and items to transfer >0
                //if (gunTransferValidation() == false) MessageBox.Show("Fire Arm cannot be transferred to selected store");
                this.continueButton.Enabled = this.CanContinue;
            }
            else
            {
                //MessageBox.Show("The Shop number entered does not exist.  Please try again");
                resetToFields();
            }
        }

        /// <summary>
        /// Place controls on panels based on the selection type. Also hooks/unhooks Carrier list events.
        /// </summary>
        private void SetupLayoutTable()
        {
            if (_carrierList != null)
            {
                // Un-hook events
                _carrierList.CarrierList.SelectedIndexChanged -= CarrierList_SelectedIndexChanged;
            }
            _carrierList = new Carrier(); //assume we'll add it; if not, reset to null in final else block

            if (_transferType == _CATCO)
            {
                CleanupLayoutTable();
                tblLayout.Controls.Add(_carrierList, 0, 1);
                tblLayout2.Controls.Add(_transferTypeList, 0, 0);
                tblLayout2.Controls.Add(_transferOutCount, 0, 1);

                // Hook into Catco list's SelectedIndexChanged event.
                _transferTypeList.TransferTypeList.SelectedIndexChanged += new EventHandler(CatcoTypeList_SelectedIndexChanged);
            }
            else if (_transferType == _SHOP_NO)
            {
                CleanupLayoutTable();
                _shopNumberComp = new ShopNumber();
                _shopNumberComp.getShopNoTextObj().Leave += new System.EventHandler(this.ShopNoEntered);
                _shopNumberComp.getShopNoTextObj().TextChanged += new System.EventHandler(this.ShopNameChanged);

                tblLayout.Controls.Add(_carrierList, 0, 2);
                tblLayout2.Controls.Add(_shopNumberComp, 0, 0);
                _shopNumberComp.getShopNoTextObj().AutoCompleteCustomSource = autoComp;
            }
            else if (_transferType == _GUN_ROOM)
            {
                CleanupLayoutTable();
                this._gunRooomComp = new GunRoomType();
                this._gunRooomComp.getGunFacilityList().SelectedIndexChanged += new EventHandler(FacilityList_SelectedIndexChanged);
                tblLayout.Controls.Add(_carrierList, 0, 1);
                tblLayout2.Controls.Add(this._gunRooomComp, 0, 0);
                setToShopNameForGunRoom();
            }
            else if (_transferType == _OUT_OF_SHOP)
            {
                CleanupLayoutTable();
                tblLayout.Controls.Add(new JewleryCase(), 1, 0);
                tblLayout.Controls.Add(_carrierList, 0, 2);
                tblLayout2.Controls.Add(new OutOfShop(), 0, 0);
            }
            else
            {
                _carrierList = null; //when not null, value selection is checked to allow Continue
            }

            if (_carrierList != null)
            {
                // Hook up events
                _carrierList.CarrierList.SelectedIndexChanged += new EventHandler(CarrierList_SelectedIndexChanged);
                // Default selection
                if (_carrierList.CarrierList.Items.Count > 0)
                {
                    _carrierList.Required = true;
                    _carrierList.CarrierList.SelectedIndex = 0; //0 = "Select" = no selecton
                    _carrier = _carrierList.CarrierList.Text;
                }
            }
        }

        private bool IsAppraisalOrWholesale()
        {
            return _transferType.Equals(_CATCO) && (_catcoType.Equals(_APPRAISAL) || _catcoType.Equals(_WHOLESALE));
        }

        private void RefreshGridView()
        {
            // Clear grid rows.
            gvMerchandise.Rows.Clear();
            // Show or hide column(s) dynamically based on type of items being selected for transfer.
            //colJewelryCaseNumber.Visible = this.IsScrapTransfer;
            colComments.Visible = this.ShowCommentColumn;
            if (RefurbCol != null)
            {
                if (isRefurbType())
                {
                    this.RefurbCol.Visible = true;
                }
                else
                {
                    this.RefurbCol.Visible = false;
                }
            }

            gvMerchandise.Refresh();
            // Fill grid with selected items for transfer.
            if (_itemsToTransfer.Count > 0)
            {
                int serial = 1;
                foreach (IItem i in _itemsToTransfer)
                {
                    int gvIdx = gvMerchandise.Rows.Add();
                    DataGridViewRow myRow = gvMerchandise.Rows[gvIdx];

                    myRow.Tag = i;
                    myRow.Cells[RefurbCol.Name].Value = i.RefurbNumber;
                    myRow.Cells[colNumber.Name].Value = serial;
                    myRow.Cells[colICN.Name].Value = i.Icn;
                    myRow.Cells[colMerchandiseDescription.Name].Value = i.TicketDescription;
                    myRow.Cells[colCost.Name].Value = String.Format("{0:C}", i.ItemAmount);
                    myRow.Cells[colQuantity.Name].Value = i.Quantity;

                    if (colComments.Visible == true)
                    {
                        myRow.Cells[colComments.Name].Value = i.Comment;
                    }
                    if (colJewelryCaseNumber.Visible == true)
                    {
                        myRow.Cells[colJewelryCaseNumber.Name].Value = ((ScrapItem)i).JewelryCaseNumber;
                    }
                    serial++;
                }
            }

            this.txtIcn.Text = "";
            // Reset progress counts.
            if (_availableItems != null)
            {
                _transferOutCount.SetCounts(_availableItems.Count, _itemsToTransfer.Count);
            }

            if (_itemsToTransfer.Count() >= _maxItemsPerTransfer)
            {
                MessageBox.Show(
                    String.Format(
                        "The maximum item limit of {0} has been reached. Please complete transfer."
                        , _maxItemsPerTransfer));
                this.txtIcn.Enabled = false;
                this.cmdSearch.Enabled = false;
            }
            else
            {
                this.txtIcn.Enabled = true;
                this.cmdSearch.Enabled = true;
            }

            this.continueButton.Enabled = this.CanContinue;
        }

        private List<TransferItemVO> BuildTransferItemVOList(out decimal TransferAmountSum)
        {
            List<TransferItemVO> transferOutItems = new List<TransferItemVO>();
            TransferAmountSum = 0.00m;

            // Build List of TransferItemVO items.
            foreach (IItem item in _itemsToTransfer)
            {
                TransferItemVO transferData = new TransferItemVO();
                transferData.ICN = item.Icn;
                transferData.StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                transferData.ICNQty = item.Quantity > 0
                                      ? item.Quantity.ToString()
                                      : "1";
                transferData.TransactionDate = ShopDateTime.Instance.ShopDate;
                transferData.MdseRecordDate = ShopDateTime.Instance.ShopDate;
                transferData.MdseRecordTime = ShopDateTime.Instance.ShopTransactionTime;
                transferData.MdseRecordUser = GlobalDataAccessor.Instance.DesktopSession.UserName;
                transferData.MdseRecordDesc = "";
                transferData.MdseRecordChange = 0;
                transferData.MdseRecordType = "";
                transferData.ClassCode = "";
                transferData.AcctNumber = "";
                transferData.CreatedBy = GlobalDataAccessor.Instance.DesktopSession.UserName;
                transferData.GunNumber = item.GunNumber > 0 ? item.GunNumber.ToString() : null;
                transferData.GunType = item.QuickInformation.GunType;
                transferData.ItemDescription = item.TicketDescription;

                // BZ# 1307 - 10/12/2011
                transferData.RefurbNumber = item.RefurbNumber;

                transferData.ItemCost = item.ItemAmount;
                transferData.RefurbNumber = item.RefurbNumber;
                TransferAmountSum += item.ItemAmount * ((item.Quantity > 0) ? item.Quantity : 1);
                transferOutItems.Add(transferData);
            }
            return transferOutItems;
        }

        private bool CreateAndSaveReceiptDetails(
            int TransferNumber, decimal SumOfTransferredItemAmounts,
            out string ErrorCode, out string ErrorText)
        {
            bool retValue = false;
            ErrorCode = String.Empty;
            ErrorText = String.Empty;

            //List<ReceiptDetailsVO> receiptDetails = new List<ReceiptDetailsVO>();
            List<string> refNumbers = new List<string>();
            List<string> refAmounts = new List<string>();
            List<string> refEvents = new List<string>();
            List<string> refStore = new List<string>();
            List<string> refTypes = new List<string>();
            List<string> refDates = new List<string>();
            List<string> refTimes = new List<string>();

            refNumbers.Add(TransferNumber.ToString());
            refAmounts.Add(SumOfTransferredItemAmounts.ToString());
            refEvents.Add("TO");
            refStore.Add(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);
            refTypes.Add("3");
            refDates.Add(ShopDateTime.Instance.ShopDate.ToShortDateString());
            refTimes.Add(String.Format("{0} {1}",
                                       ShopDateTime.Instance.ShopDate.ToShortDateString(),
                                       ShopDateTime.Instance.ShopTime.ToString()));

            ReceiptDetailsVO transferReceiptDetailsVO = new ReceiptDetailsVO();
            transferReceiptDetailsVO.UserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
            transferReceiptDetailsVO.ReceiptDate = ShopDateTime.Instance.ShopDate;
            transferReceiptDetailsVO.RefNumbers = refNumbers;
            transferReceiptDetailsVO.RefAmounts = refAmounts;
            transferReceiptDetailsVO.RefEvents = refEvents;
            transferReceiptDetailsVO.RefStores = refStore;
            transferReceiptDetailsVO.RefTypes = refTypes;
            transferReceiptDetailsVO.RefDates = refDates;
            transferReceiptDetailsVO.RefTimes = refTimes;

            retValue = ProcessTenderProcedures.ExecuteInsertReceiptDetails(
                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                GlobalDataAccessor.Instance.DesktopSession.UserName,
                ShopDateTime.Instance.ShopDate.ToShortDateString(),
                GlobalDataAccessor.Instance.DesktopSession.UserName,
                ref transferReceiptDetailsVO,
                out ErrorCode,
                out ErrorText);

            return retValue;
        }

        private bool TranferOutItems(out List<TransferItemVO> TransferOutItems,
                                     out decimal TotalTransferAmount,
                                     out int TransferNumber,
                                     out string ErrorMessage)
        {
            bool retValue = false;
            TransferOutItems = new List<TransferItemVO>();
            TransferNumber = 0;
            ErrorMessage = String.Empty;
            TotalTransferAmount = 0.0m;

            if (_itemsToTransfer != null && _itemsToTransfer.Count > 0)
            {
                TransferOutItems = BuildTransferItemVOList(out TotalTransferAmount);

                if (TransferOutItems != null && TransferOutItems.Count > 0) //&& TotalTransferAmount > 0.0m
                {
                    if (!GlobalDataAccessor.Instance.beginTransactionBlock())
                    {
                        string errMsg = string.Format("Cannot start transaction block in TransferOutItems");
                        if (FileLogger.Instance.IsLogFatal)
                        {
                            FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                        }
                        ErrorMessage = errMsg;
                        BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                        return (false);
                    }

                    retValue = TransferProcedures.TransferScrap(
                        TransferOutItems,
                        ScrapTransFacNumber,
                        ExcessTransferFacilityNumber,
                        RefurbTransferFacilityNumber,
                        this._carrierList.CarrierList.Text,
                        out TransferNumber,
                        out ErrorMessage);
                    if (retValue && TransferNumber > 0 && (string.IsNullOrEmpty(ErrorMessage) || ErrorMessage.Equals("Success", StringComparison.OrdinalIgnoreCase)))
                    {
                        string errorCode;
                        retValue = CreateAndSaveReceiptDetails(
                            TransferNumber,
                            TotalTransferAmount,
                            out errorCode,
                            out ErrorMessage);
                        // NOTE 1: errorCode and ErrorMessage are set to String.Empty at start of CreateAndSaveReceiptDetails.
                        // NOTE 2: errorCode and ErrorMessage are set to "0" and "Success" respectively on successful CreateAndSaveReceiptDetails.
                        if (!string.IsNullOrEmpty(errorCode))
                        {
                            if (errorCode == "0") //ASSUME: retValue == true and ErrorMessage = "Success"
                            {
                                if (!GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT))
                                {
                                    //Failed to commit, must rollback
                                    string errMsg = string.Format("Cannot commit transaction block in TransferOutItems");
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    ErrorMessage = errMsg;
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                    return (false);
                                }
                                ErrorMessage = String.Empty;
                            }
                            else
                            {
                                if (!GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK))
                                {
                                    string errMsg = string.Format("Cannot rollback transaction block in TransferOutItems: (Error {0}) {1}", errorCode, ErrorMessage);
                                    if (FileLogger.Instance.IsLogFatal)
                                    {
                                        FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                    }
                                    ErrorMessage = errMsg;
                                    BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                    return (false);
                                }
                            }
                        }
                        else if (retValue == false)
                        {
                            if (!GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK))
                            {
                                string errMsg = string.Format("Cannot rollback transaction block in TransferOutItems");
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                ErrorMessage = errMsg;
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return (false);
                                //When retValue == false, ErrorMessage should not be empty/null so this is double-check of success.
                            }
                            if (String.IsNullOrEmpty(ErrorMessage))
                            {
                                ErrorMessage = "TranferOutItems failed to CreateAndSaveReceiptDetails";
                            }
                            return (false);
                        }
                        else //shouldn't get here unless change to CreateAndSaveReceiptDetails's out parameter logic
                        {
                            //IF HERE, WE KNOW: retValue == true and String.IsNullOrEmpty(errorCode) == true
                            if (!GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.COMMIT))
                            {
                                string errMsg = string.Format("Cannot commit transaction block in TransferOutItems");
                                if (FileLogger.Instance.IsLogFatal)
                                {
                                    FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                                }
                                ErrorMessage = errMsg;
                                BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                                return (false);
                            }
                            ErrorMessage = String.Empty;
                        }
                    }
                    else //TransferProcedures.TransferScrap(...) failed
                    {
                        if (!GlobalDataAccessor.Instance.DesktopSession.endTransactionBlock(EndTransactionType.ROLLBACK))
                        {
                            string errMsg = string.Format("Cannot rollback transaction block in TransferOutItems");
                            if (FileLogger.Instance.IsLogFatal)
                            {
                                FileLogger.Instance.logMessage(LogLevel.FATAL, this, errMsg);
                            }
                            ErrorMessage = errMsg;
                            BasicExceptionHandler.Instance.AddException(errMsg, new ApplicationException(errMsg));
                            return (false);
                        }
                        // Since several conditions could cause us to be here, ensure out values are set.
                        if (retValue)
                        {
                            retValue = false;
                        }
                        if (String.IsNullOrEmpty(ErrorMessage))
                        {
                            ErrorMessage = "TranferOutItems failed to TransferScrap";
                        }
                    }
                }
            }
            return retValue;
        }

        private void PerformGunTransfer()
        {
            bool retValue = false;
            List<TransferItemVO> TransferOutItems = new List<TransferItemVO>();
            string ErrorMessage = String.Empty;
            int transferNumber = 0;
            decimal TotalTransferAmount = 0.0m;
            try
            {
                if (_itemsToTransfer == null || _itemsToTransfer.Count == 0)
                {
                    MessageBox.Show("No items of the selected type are available for transfer.", "Available Transfer Items");
                    return;
                }
                TransferOutItems = BuildTransferItemVOList(out TotalTransferAmount);
                if (TransferOutItems != null && TransferOutItems.Count > 0)
                {
                    retValue = true;
                    //to be implemented
                    string gunFacility = "86";
                    retValue = TransferProcedures.TransferToGunRoom(TransferOutItems, out transferNumber, this._carrierList.CarrierList.Text, out ErrorMessage, gunFacility);
                }
                if (!retValue)
                {
                    MessageBox.Show("Error:" + ErrorMessage);
                    BasicExceptionHandler.Instance.AddException("Gun Room Transfer was not created", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Gun Room Transfer was not created");
                    return;
                }
                foreach (TransferItemVO transfer in TransferOutItems)
                {
                    transfer.TransferNumber = transferNumber;
                }
                SaveTransferReceipts(transferNumber, TotalTransferAmount);

                //Generate the transfer report
                try
                {
                    TransferOutReport trnsfrRpt = PrintShopAndGunTransReport(TransferOutItems, transferNumber, TotalTransferAmount, this._gunRooomComp.getSelectedFacility());

                    if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                    {
                        printDocument(trnsfrRpt);
                        printBarCodeTag(transferNumber, this._gunRooomComp.getSelectedFacility());
                    }
                }
                catch (Exception ee)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Gun transfer out document failed: " + ee.StackTrace);
                    MessageBox.Show("Gun transfer document Failed");
                    return;
                }

                MessageBox.Show("Gun Transfer Out data successfully inserted");
            }
            catch (Exception e)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Gun Transfer failed: " + e.StackTrace);
            }
        }

        // Function added for transfering Shop to Shop item transfer will be called with "continue" click
        private void TransferOutItemForShop()
        {
            bool retValue = false;
            List<TransferItemVO> TransferOutItems = new List<TransferItemVO>();
            string ErrorMessage = String.Empty;
            int transferNumber = 0;
            decimal TotalTransferAmount = 0.00m;
            if (_itemsToTransfer == null || _itemsToTransfer.Count == 0)
            {
                MessageBox.Show("No items of the selected type are available for transfer.", "Available Transfer Items");
                return;
            }
            try
            {
                TransferOutItems = BuildTransferItemVOList(out TotalTransferAmount);

                if (TransferOutItems != null && TransferOutItems.Count > 0)
                {
                    retValue = TransferProcedures.TransferItemsOutOfStore(TransferOutItems, out transferNumber, this._carrierList.CarrierList.Text, out ErrorMessage, isClxToClx, this._shopNoToTransfer);
                }
                if (!retValue)
                {
                    MessageBox.Show("Error:" + ErrorMessage);
                    BasicExceptionHandler.Instance.AddException("Transfer out of store was not created", new ApplicationException());
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer out of store was not created");
                    return;
                }

                foreach (TransferItemVO transfer in TransferOutItems)
                {
                    transfer.TransferNumber = transferNumber;
                }

                SaveTransferReceipts(transferNumber, TotalTransferAmount);

                //Generate the transfer report
                try
                {
                    if (IsAppraisalOrWholesale())
                    {
                        PrintTransferReport(TransferOutItems);
                    }
                    else
                    {
                        TransferOutReport trnsfrRpt = PrintShopAndGunTransReport(TransferOutItems, transferNumber, TotalTransferAmount, this._toShopName);

                        //TODO: Store report in couch db
                        if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled)
                        {
                            printDocument(trnsfrRpt);
                            printBarCodeTag(transferNumber, this._shopNoToTransfer);
                        }
                    }
                }
                catch (Exception e)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer out document failed: " + e.StackTrace);
                    MessageBox.Show("Shop transfer out document failed");
                    return;
                }
                MessageBox.Show("Shop Transfer Out data successfully inserted");
            }
            catch (Exception exc)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer out failed: " + exc.StackTrace);
            }
        }

        private static void printDocument(TransferOutReport trnsfrRpt)
        {
            if (!SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled ||
                !GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
            {
                MessageBox.Show("No laser printer configured or printing is disabled. Cannot print transfer out document");
                return;
            }
            string laserPrinterIp = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress;
            int laserPrinterPort = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port;
            PrintingUtilities.printDocument(trnsfrRpt.getReportWithPath(), laserPrinterIp, laserPrinterPort, 2);
        }

        private TransferOutReport PrintShopAndGunTransReport(List<TransferItemVO> TransferOutItems, int transferNumber, decimal TotalTransferAmount, string toName)
        {
            string logPath =
            SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration
            .BaseLogPath;
            ReportObject.TransferReport reportObj = new ReportObject.TransferReport();
            reportObj.FromStoreName = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
            reportObj.FromStoreAddrLine1 = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;
            reportObj.FromStoreAddrLine2 = GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName
                                           + ", " + GlobalDataAccessor.Instance.CurrentSiteId.State
                                           + " " + GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
            
            reportObj.FromStoreAddrLine3 = " ";
            reportObj.FromTelephone = lblFromTelephone.Text;
            reportObj.FromFax = lblFromFax.Text;
            reportObj.ToStoreName = toName;
            reportObj.ToStoreAddrLine1 = this.storeAddrLine1.Text;
            reportObj.ToStoreAddrLine2 = this.storeAddrLine2.Text;
            reportObj.storeMgrName = this.storeMgrName.Text;
            reportObj.storeMgrPhone = this.storePhNo.Text;
            reportObj.transferAmount = Convert.ToString(TotalTransferAmount);
            reportObj.storeFax = this.lblStoreFax.Text;
            TransferOutReport trnsfrRpt;
            trnsfrRpt = new TransferOutReport(TransferOutItems, ShopDateTime.Instance.ShopDateCurTime, GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                                                               Convert.ToString(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber), GlobalDataAccessor.Instance.DesktopSession.FullUserName, Convert.ToString(transferNumber), logPath, _transferType, reportObj, PdfLauncher.Instance);
            trnsfrRpt.CreateReport();
            return trnsfrRpt;
        }

        private void SaveTransferReceipts(int transferNumber, decimal TotalTransferAmount)
        {
            #region receipt
            var errorCode = String.Empty;
            var errorText = String.Empty;
            var hasErrors = false;
            ReceiptDetailsVO transferReceiptDetailsVO = new ReceiptDetailsVO();
            transferReceiptDetailsVO.ReceiptDate = ShopDateTime.Instance.ShopDate;
            transferReceiptDetailsVO.RefDates = new List<string>() { ShopDateTime.Instance.ShopDate.ToShortDateString() };
            transferReceiptDetailsVO.RefTimes = new List<string>() { ShopDateTime.Instance.ShopDate.ToShortDateString() + " " + ShopDateTime.Instance.ShopTime.ToString() };
            transferReceiptDetailsVO.UserId = GlobalDataAccessor.Instance.DesktopSession.UserName;
            transferReceiptDetailsVO.RefNumbers = new List<string>() { Convert.ToString(transferNumber) };
            transferReceiptDetailsVO.RefTypes = new List<string>() { "3" };
            transferReceiptDetailsVO.RefStores = new List<string>() { GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber };
            transferReceiptDetailsVO.RefEvents = new List<string>() { "TO" };
            transferReceiptDetailsVO.RefAmounts = new List<string>() { Convert.ToString(TotalTransferAmount) };

            ProcessTenderProcedures.ExecuteInsertReceiptDetails(
                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                GlobalDataAccessor.Instance.DesktopSession.UserName,
                ShopDateTime.Instance.ShopDate.ToShortDateString(),
                GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                ref transferReceiptDetailsVO,
                out errorCode,
                out errorText);

            if (errorCode != "0")
            {
                hasErrors = true;
            }

            errorCode = String.Empty;
            errorText = String.Empty;

            if (hasErrors)
            {
                BasicExceptionHandler.Instance.AddException("Transfer receipts were not created", new ApplicationException());
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Transfer receipts were not created");
            }
            #endregion receipt
        }

        private void PrintTransferReport(List<TransferItemVO> ItemsTransferred)
        {
            if (ItemsTransferred != null && ItemsTransferred.Count > 0)
            {
                int transferNum = ItemsTransferred[0].TransferNumber;
                BusinessRuleVO ruleScrap = null;
                // if (IsScrapTransfer)
                //{
                ruleScrap = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO["PWN_BR-061"];
                //}
                string logPath =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration
                .BaseLogPath;

                ReportObject.TransferReport reportObj = new ReportObject.TransferReport();
                reportObj.FromStoreName = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;
                reportObj.FromStoreAddrLine1 = GlobalDataAccessor.Instance.CurrentSiteId.StoreAddress1;
                reportObj.FromStoreAddrLine2 = GlobalDataAccessor.Instance.CurrentSiteId.StoreCityName
                                               + ", " + GlobalDataAccessor.Instance.CurrentSiteId.State
                                               + " " + GlobalDataAccessor.Instance.CurrentSiteId.StoreZipCode;
                reportObj.FromStoreAddrLine3 = " ";

                reportObj.ToStoreName = _CATCO;
                reportObj.ToStoreAddrLine1 = this.storeAddrLine1.Text;
                reportObj.ToStoreAddrLine2 = this.storeAddrLine2.Text;
                reportObj.storeMgrName = this.storeMgrName.Text;
                reportObj.storeMgrPhone = this.storePhNo.Text;
                reportObj.storeFax = this.lblStoreFax.Text;
                reportObj.TransferTypeDepartmentName = _transferTypeDeptName;
                reportObj.TransferTypeFacilityManagerName = _transferTypeDeptManagerName;
                reportObj.TransferTypeFacilityPhone = _transferTypeDeptManagerPhone;
                if (TransferTypeNumber != 0)
                    reportObj.TransferTypeFacilityNumber = TransferTypeNumber.ToString();
                //reportObj.transferAmount = Convert.ToString(TotalTransferAmount);

                TransferOutScrapsReport trnsfrRpt =
                new TransferOutScrapsReport(
                    ItemsTransferred,
                    _itemsToTransfer,
                    ruleScrap,
                    ShopDateTime.Instance.ShopDate,
                    GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                    transferNum.ToString(),
                    _catcoType,
                    logPath,
                    reportObj, PdfLauncher.Instance);

                if (trnsfrRpt.CreateReport() == false)
                {
                    BasicExceptionHandler.Instance.AddException("ManageTransferOut Transfer Report Printing Error",
                                                                new ApplicationException("ManageTransferOut.PrintTransferReport() Error"));
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not generate Transfer out report");
                    }
                    return;
                }
                //TODO: Store report in couch db
                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                {
                    string laserPrinterIp = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress;
                    int laserPrinterPort = GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port;
                    PrintingUtilities.printDocument(trnsfrRpt.getReportWithPath(),
                                                    laserPrinterIp,
                                                    laserPrinterPort,
                                                    2);

                    printBarCodeTag(transferNum, Convert.ToString(TransferTypeNumber));
                }//end if (PrintEnabled)
            }//end if (ItemsTransferred)
        }

        private void printBarCodeTag(int transferNum, string toName)
        {
            if (!SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled ||
                !GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "No barcode printer configured to print tags");
                }
                return;
            }
            IntermecBarcodeTagPrint intermecBarcodeTagPrint =
            new IntermecBarcodeTagPrint("",
                                        Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber),
                                        IntermecBarcodeTagPrint.PrinterModel.Intermec_PM4i,
                                        GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.IPAddress,
                                        (uint)GlobalDataAccessor.Instance.DesktopSession.BarcodePrinter.Port, GlobalDataAccessor.Instance.DesktopSession);
            decimal amount = 0;
            int noOfItems = 0;
  
            var fromStore = string.Empty;
            //fromStore = _itemsToTransfer[0].mStore.ToString();
            fromStore = Convert.ToInt32(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber).ToString();
            foreach (Item item in _itemsToTransfer)
            {
                amount += item.ItemAmount * ((item.Quantity > 0)? item.Quantity : 1);
                noOfItems += ((item.Quantity > 0) ? item.Quantity : 1);
            }
            intermecBarcodeTagPrint.PrintBagTagForTransfer(fromStore, transferNum, amount, noOfItems, _catcoType, toName);
        }

        private void ShowMessageOnListSelectionChange()
        {
            if (_itemsToTransfer != null && _itemsToTransfer.Count > 0)
            {
                MessageBox.Show(
                    "Changing the Transfer To or Catco Transfer Type option will remove the merchandise you have selected to transfer.",
                    "Selection(s) Changed");
            }
        }

        #endregion Private Methods

        #region Events

        private void ManageTransferOut_Load(object sender, EventArgs e)
        {
            try
            {
                /* !!!!!!!!!! REMOVE once other choices are implemented !!!!!!!!!! */
                if (this.transferToList1.TransferToList.Items.Count > 0)
                {
                    //select Catco
                    this.transferToList1.TransferToList.SelectedIndex = 0;
                }
                //this.transferToList1.Enabled = false;
                if (String.IsNullOrEmpty(_transferType))
                {
                    _transferType = this.transferToList1.TransferToList.Text;
                }
            }
            catch (Exception exLoad)
            {
                BasicExceptionHandler.Instance.AddException("ManageTransferOut Load Error",
                                                            new ApplicationException("ManageTransferOut.Load() Error", exLoad));
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Unhook from events
                this.transferToList1.TransferToList.SelectedIndexChanged -= TransferToList_SelectedIndexChanged;
                if (_transferTypeList != null)
                {
                    _transferTypeList.TransferTypeList.SelectedIndexChanged -= CatcoTypeList_SelectedIndexChanged;
                }
                if (_carrierList != null)
                {
                    _carrierList.CarrierList.SelectedIndexChanged -= CarrierList_SelectedIndexChanged;
                }
            }
            catch (Exception)
            {
                //ignore errors while unhooking events before closing form
                return;
            }

            this.Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            var mdseStatus = this._itemsToTransfer.ToDictionary(item => item.Icn, item => string.Empty);
            string errorCode, errorText = null;
            var retVal = TransfersDBProcedures.GetCurrentMdseStatus(CDS.CurrentSiteId.StoreNumber, mdseStatus, out errorCode, out errorText);

            if (retVal && mdseStatus.Any(item => !item.Value.Equals(ProductStatus.PFI.ToString())))
            {
                if (MessageBox.Show("One or more items identified for transfer have been sold. These items will be removed from the transfer.", "Transfer Out", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                var nonPFIItems = (from item in mdseStatus
                                   where !item.Value.Equals(ProductStatus.PFI.ToString())
                                   select item.Key).ToList();

                _itemsToTransfer.RemoveAll(item => nonPFIItems.Contains(item.Icn));
                RefreshGridView();
            }

            if (this.CanContinue == false)
            {
                MessageBox.Show("Cannot transfer item(s). " +
                                "Please be sure all selections have been made and at least one item for transfer is selected.",
                                "Continue");
                return;
            }


            if (IsAppraisalOrWholesale())
            {
                isClxToClx = false;
                TransferOutItemForShop();
                //CATCOTransfer();
            }
            else if (_transferType.Equals(_CATCO, StringComparison.CurrentCultureIgnoreCase))
            {
                CATCOTransfer();
                //No webservice after catco transfer will be done during shop close
                /*if (returnVal)
                callWS_Click();*/
            }
            else if (_transferType.Equals(_SHOP_NO, StringComparison.CurrentCultureIgnoreCase))
            {
                if (gunTransferValidation() == false)
                {
                    MessageBox.Show("Shop or vendor does not have a firearm license");
                    return;
                }

                if (setClxToClxFlag(_shopNoToTransfer))
                {
                    TransferOutItemForShop();
                }
                else
                {
                    MessageBox.Show("Error with target store selected");
                    return;
                }
            }
            else if (_transferType.Equals(_GUN_ROOM, StringComparison.CurrentCultureIgnoreCase))
            {
                PerformGunTransfer();
            }

            Init();
            RefreshGridView();
        }

        private void cmdDisplayAll_Click(object sender, EventArgs e)
        {
            #region Validate Transfer Type Selections
            if (String.IsNullOrEmpty(_transferType)
                || _transferType.Equals(_NO_SELECTION, StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show("Please select the type of items to be transferred.", "Transfer Type Selection");
                return;
            }
            else if (_transferType.Equals(_CATCO, StringComparison.CurrentCultureIgnoreCase)
                     && (String.IsNullOrEmpty(_catcoType)
                         || _catcoType.Equals(_NO_SELECTION, StringComparison.CurrentCultureIgnoreCase))
            )
            {
                MessageBox.Show(String.Format("Please select the type of {0} items to be transferred.", _CATCO),
                                String.Format("{0} Type Selection", _CATCO));
                return;
            }
            else if (_transferType.Equals(_SHOP_NO, StringComparison.CurrentCultureIgnoreCase))
            // && (String.IsNullOrEmpty(this._shopNumberComp.getShopNo())))
            {
                // This is moved to ICN based search
                /*this.Cursor = Cursors.WaitCursor;
                if (!_shopToShopLoaded)
                _availableItems = this.getPFIItemForShopToShop();
                this.Cursor = Cursors.Default;
                _shopToShopLoaded = true;*/
            }
            else if (_transferType.Equals(_GUN_ROOM, StringComparison.CurrentCultureIgnoreCase))
            {
                if (_availableItems.Count == 0)
                {
                    _availableItems = this.getPFIItemForShopToGunRoom();
                }
            }

            #endregion Validate Transfer Type Selections

            List<IItem> availableItems = new List<IItem>();

            #region Build list of available items for SelectTransferItems dialog
            try
            {
                this.Cursor = Cursors.WaitCursor;
                foreach (IItem item in _availableItems)
                {
                    //Only show available items not already selected to transfer
                    if (!(from i in _itemsToTransfer
                          where i.Icn == item.Icn
                          select i).Any())
                    {
                        availableItems.Add(item);
                    }
                }
            }
            catch (Exception exBuildAvailableList)
            {
                BasicExceptionHandler.Instance.AddException("ManageTransferOut: Item Selection List Initialization Error",
                                                            new ApplicationException("ManageTransferOut.cmdDisplayAll() Error", exBuildAvailableList));
                return;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            #endregion Build list of available items for SelectTransferItems dialog

            if (availableItems.Count == 0)
            {
                if (_availableItems.Count > 0)
                {
                    if (_itemsToTransfer.Count > 0)
                    {
                        MessageBox.Show(
                            "All items of the selected type have been selected for transfer.",
                            "Available Transfer Items");
                    }
                    else
                    {
                        MessageBox.Show("No items of the selected type are available for transferring.",
                                        "Available Transfer Items");
                    }
                }
                else
                {
                    MessageBox.Show("No items of the selected type are available for transferring.",
                                    "Available Transfer Items");
                }
                //return;
            }
            else
            {
                // Display all option with Refurb No
                SelectTransferItems s = null;
                bool duplicateMode = false;
                s = showDisplayAllDialog(availableItems, s, duplicateMode);
                s.ShowDialog();

                #region DEPRECATED Select JewelryCaseNumber
                //if (s.SelectedItems.Count > 0)
                //{
                //    JewelryCaseDetails d = new JewelryCaseDetails();
                //    d.ShowDialog();
                //    if (d.ShouldContinue)
                //    {
                //        s.SelectedItems[0].JewelryCaseNumber = d.JewelryCaseNumber;
                //    }
                //}
                #endregion DEPRECATED Select JewelryCaseNumber

                #region Add Selected Items to List of Items to Transfer
                if (s.SelectedItems != null & s.SelectedItems.Count > 0)
                {
                    if (this.IsScrapTransfer)
                    {
                        foreach (IItem item in s.SelectedItems)
                        {
                            _itemsToTransfer.Add(item as ScrapItem);
                        }
                    }
                    else
                    {
                        _itemsToTransfer.AddRange(s.SelectedItems);
                    }
                }
                #endregion Add Selected Items to List of Items to Transfer

                RefreshGridView();
            }
        }

        private void validateAndAdd(IItem itemToAdd)
        {
            if (_itemsToTransfer.Count == 0)
            {
                _itemsToTransfer.Add(itemToAdd);
                return;
            }

            for (int j = 0; j < _itemsToTransfer.Count; j++)
            {
                if (_itemsToTransfer[j].Icn == itemToAdd.Icn)
                {
                    MessageBox.Show("Item cannot be found in the available items or has already been added.", "Item Not Available or Already Selected");
                    return;
                }
            }
            _itemsToTransfer.Add(itemToAdd);
        }

        private void cmdICNSubmit_Click(object sender, EventArgs e)
        {
            string icn = txtIcn.Text.Trim();
            if (String.IsNullOrEmpty(txtIcn.Text) || String.IsNullOrEmpty(txtIcn.Text.Trim()))
                {
                    MessageBox.Show("Please enter or scan the ICN# to lookup.", "Submmit ICN#");
                    return;
                }
            string reasonType = string.Empty;
            if (_transferType == _SHOP_NO || IsAppraisalOrWholesale())
            {
                //new impl jak
                ICNSearchForInShopTransfer(icn);
                return;
            }
            if (_transferType == _GUN_ROOM)
            { // This will be invoked if display all not selectd gun room
                _availableItems=icnBasedSearchForGun(icn, out reasonType);
            
                if (_availableItems == null || _availableItems.Count == 0)
                {
                    if (reasonType != null && "Not a Fire Arm".Equals(reasonType))
                    { //this will identify if non gun selected for icn srch
                        MessageBox.Show("Only Firearm item can be transferred to CAF Facility or SP (SuperPawn Gun Room) Facility");
                        return;
                    }
 
                }
                _itemsToTransfer = _availableItems;
            }
            else // CATCO OR GUN available list srch
            {
                //No need to continue if nothing was entered or no available items.
                if (_availableItems == null || _availableItems.Count == 0)
                {
                    MessageBox.Show("No items of the selected type are available for transfer.", "Available Transfer Items");
                    return;
                }


                var ticketNumber = string.Empty;
                var itemNumber = string.Empty;

                if (!_catcoType.Equals(_REFURB))
                {
                    string[] icnParsed = icn.Split('.');
                    ticketNumber = icnParsed[0];
                    if (icnParsed.Length > 1)
                    {
                        itemNumber = icnParsed[1];
                    }
                }
                else
                {
                    //incase of Refurb take the refurb no as is inplace of icn
                    ticketNumber = icn;
                }

                ScrapICNSearch(ticketNumber, itemNumber);
            }

            RefreshGridView();
        }

        private void ICNSearchForInShopTransfer(string icn)
        {
            string reasonType = string.Empty;

            _availableItems = this.getPFIItemForShopToShop(icn, out reasonType);

            if (_availableItems == null || _availableItems.Count == 0)
            {
                if (reasonType.Equals(string.Empty))
                {
                    MessageBox.Show("No Records Found");
                }
                else if (IsAppraisalOrWholesale())
                {
                    MessageBox.Show(reasonType);
                }
                else
                {
                    MessageBox.Show("The ICN# cannot be transferred because it's marked as \"" + reasonType + "\"");
                }
                //return;
            }
            else if (_availableItems.Count() == 1)
            {
                validateAndAdd(_availableItems[0]);
                //return;
            }
            else
            {

                var distinctItems = from sItem in _availableItems
                                    where ((sItem.IsJewelry && sItem.Jewelry.Any(j => j.SubItemNumber == 0)) ||
                                    !sItem.IsJewelry)
                                    select sItem;

                if (distinctItems.Count() > 0)
                {
                    SelectTransferItems selectItems = new SelectTransferItems(_availableItems, true, false);
                    selectItems.ShowDialog();
                    if (selectItems.SelectedItems != null && selectItems.SelectedItems.Count() > 0)
                    {
                        IItem item = selectItems.SelectedItems.First();
                        //_itemsToTransfer.Add(item);
                        validateAndAdd(item);
                    }
                }
                else
                {
                    validateAndAdd(_availableItems[0]);
                }
            }

            RefreshGridView();
        }

        // Search for Jewellery related items
        private void ScrapICNSearch(string ticketNumber, string itemNumber)
        {
            List<IItem> results = null;

            // incase of refurb search based on refurb no
            if (_catcoType.Equals(_REFURB))
            {
                results = (from i in _availableItems
                           where i.RefurbNumber.ToString().Equals(ticketNumber)
                           select i).ToList();
                foreach (IItem item in _availableItems)
                {
                    Console.WriteLine(item.RefurbNumber + " Contains " + ticketNumber + " Boolean " + item.RefurbNumber.ToString().Equals(ticketNumber));
                }
            }
            else
            {
                results = (from i in _availableItems
                           where i.Icn.Substring(6, 6).Contains(ticketNumber)
                                 && i.Icn.Substring(13, 3).Contains(itemNumber)
                           select i).ToList();
            }
            //For debugging icn search
            /*foreach (IItem item in _availableItems)
            {
            Console.WriteLine(item.Icn.Substring(6, 6) + " Contains " + ticketNumber + " Boolean " + item.Icn.Substring(6, 6).Contains(ticketNumber));
            Console.WriteLine(item.Icn.Substring(13, 3) + " Contains " + itemNumber + " Boolean " + item.Icn.Substring(13, 3).Contains(itemNumber));
            }*/

            //Removes items that are already in the transfer list. 

            foreach (IItem item in _itemsToTransfer)
            {
                results.RemoveAll(i => i.Icn == item.Icn);
            }

            if (results.Count() < 1)
            {
                MessageBox.Show("Item cannot be found in the available items or has already been added.", "Item Not Available or Already Selected");
                //return;
            }
            else if (results.Count() == 1)
            {
                if (this.IsScrapTransfer)
                {
                    _itemsToTransfer.Add(results[0] as ScrapItem);
                }
                else
                {
                    _itemsToTransfer.Add(results[0]);
                }
            }
            else //if (results.Count() >= 1)
            {
                //SelectTransferItems selectItems = new SelectTransferItems(results, true);
                //selectItems.ShowDialog();
                SelectTransferItems selectItems = null;
                bool duplicateMode = true;
                selectItems = showDisplayAllDialog(results, selectItems, duplicateMode);

                selectItems.ShowDialog();

                if (selectItems.SelectedItems != null && selectItems.SelectedItems.Count() > 0)
                {
                    IItem item = selectItems.SelectedItems.First();
                    if (this.IsScrapTransfer)
                    {
                        _itemsToTransfer.Add(item as ScrapItem);
                    }
                    else
                    {
                        _itemsToTransfer.Add(item);
                    }
                }
            }
        }

        private bool isRefurbType()
        {
            if (_transferType.Equals(_CATCO, StringComparison.CurrentCultureIgnoreCase))
            {
                if (!String.IsNullOrEmpty(_catcoType) && _catcoType.Equals(_REFURB, StringComparison.CurrentCultureIgnoreCase))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private SelectTransferItems showDisplayAllDialog(List<IItem> results, SelectTransferItems selectItems, bool duplicateMode)
        {
            if (isRefurbType())
            {
                selectItems = new SelectTransferItems(results, duplicateMode, true);
            }
            else
            {
                selectItems = new SelectTransferItems(results, duplicateMode, false);
            }
            return selectItems;
        }

        private void addCommentButton_Click(object sender, EventArgs e)
        {
            if (gvMerchandise.SelectedRows.Count > 0)
            {
                string icn = gvMerchandise.SelectedRows[0].Cells[colICN.Name].Value.ToString();
                IItem item = (from i in _itemsToTransfer
                              where i.Icn == icn
                              select i).First();

                MerchandiseComment c;
                if (this.IsScrapTransfer)
                {
                    c = new MerchandiseComment(item as ScrapItem);
                }
                else
                {
                    c = new MerchandiseComment(item);
                }

                DialogResult dlgRes = c.ShowDialog();
                if (dlgRes != DialogResult.Cancel)
                {
                    item.Comment = c.Comment;
                    RefreshGridView();
                }
            }
            else
            {
                MessageBox.Show("Please select the item to add a comment to.", "Item Comment");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (gvMerchandise.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in gvMerchandise.SelectedRows)
                {
                    string icn = row.Cells[colICN.Name].Value.ToString();
                    _itemsToTransfer.RemoveAll(i => i.Icn == icn);
                }

                RefreshGridView();
            }
            else
            {
                MessageBox.Show("Please select the item to delete.", "Delete Item to Transfer");
            }
        }

        private void CarrierList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_carrierList.CarrierList.SelectedIndex > -1)
            {
                _carrier = _carrierList.CarrierList.Text;
            }
            else
            {
                _carrier = "";
            }

            // Enable Continue button if all selections made, including this one
            this.continueButton.Enabled = this.CanContinue;
        }

        private void FacilityList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.continueButton.Enabled = this.CanContinue;
        }

        private void CatcoTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowMessageOnListSelectionChange();

            _catcoType = _transferTypeList.TransferTypeList.Text;
            if (_catcoType == "Excess")
            {
                TransferTypeNumber = ExcessTransferFacilityNumber;
                _transferTypeDeptName = ExcessTransferFacilityName;
                _transferTypeDeptManagerPhone = ExcessTransferStorePhone;
                _transferTypeDeptManagerName = ExcessTransferManagerName;
                setToShopNameForCATCO(ExcessTransferFacilityNumber, ExcessTransferFacilityName, ExcessTransferManagerName, ExcessTransferStorePhone, string.Empty);
                _transferOutCount.Visible = true;
                cmdSearch.Visible = true;
            }
            else if (_catcoType == "Refurb")
            {
                TransferTypeNumber = RefurbTransferFacilityNumber;
                _transferTypeDeptName = RefurbTransferFacilityName;
                _transferTypeDeptManagerPhone = RefurbTransferStorePhone;
                _transferTypeDeptManagerName = RefurbTransferManagerName;
                setToShopNameForCATCO(RefurbTransferFacilityNumber, RefurbTransferFacilityName, RefurbTransferManagerName, RefurbTransferStorePhone, string.Empty);
                _transferOutCount.Visible = true;
                cmdSearch.Visible = true;
            }
            else if (_catcoType == "Scrap")
            {
                TransferTypeNumber = ScrapTransFacNumber;
                _transferTypeDeptName = ScrapTransFacName;
                _transferTypeDeptManagerPhone = ScrapTransferStorePhone;
                _transferTypeDeptManagerName = ScrapTransferManagerName;
                setToShopNameForCATCO(ScrapTransFacNumber, ScrapTransFacName, ScrapTransferManagerName, ScrapTransferStorePhone, string.Empty);
                _transferOutCount.Visible = true;
                cmdSearch.Visible = true;
            }
            else if (_catcoType == _APPRAISAL)
            {
                _shopNoToTransfer = AppraisalTransFacNumber.ToString("00000");
                TransferTypeNumber = AppraisalTransFacNumber;
                _transferTypeDeptName = AppraisalTransFacName;
                _transferTypeDeptManagerPhone = AppraisalTransferStorePhone;
                _transferTypeDeptManagerName = AppraisalTransferManagerName;
                setToShopNameForCATCO(AppraisalTransFacNumber, AppraisalTransFacName, AppraisalTransferManagerName, AppraisalTransferStorePhone, AppraisalTransferFax);
                _transferOutCount.Visible = false;
                cmdSearch.Visible = false;
            }
            else if (_catcoType == _WHOLESALE)
            {
                _shopNoToTransfer = WholesaleTransFacNumber.ToString("00000");
                TransferTypeNumber = WholesaleTransFacNumber;
                _transferTypeDeptName = WholesaleTransFacName;
                _transferTypeDeptManagerPhone = WholesaleTransferStorePhone;
                _transferTypeDeptManagerName = WholesaleTransferManagerName;
                setToShopNameForCATCO(WholesaleTransFacNumber, WholesaleTransFacName, WholesaleTransferManagerName, WholesaleTransferStorePhone, WholesaleTransferFax);
                _transferOutCount.Visible = false;
                cmdSearch.Visible = false;
            }
            else
            {
                _transferTypeDeptName = string.Empty;
                _transferTypeDeptManagerPhone = string.Empty;
                _transferTypeDeptManagerName = string.Empty;
                setToShopNameForCATCO(0, string.Empty, string.Empty, string.Empty, string.Empty);
                _transferOutCount.Visible = false;
                cmdSearch.Visible = false;
            }
            _transferOutCount.TransferTypeLabel = _catcoType;
            // Load available items for new selection.
            Init();
            RefreshGridView();
        }

        private void ticketNumber_TextChanged(object sender, EventArgs e)
        {
            string icn = String.IsNullOrEmpty(this.txtIcn.Text) ? "" : this.txtIcn.Text.Trim();

            if (icn.Length == Icn.ICN_LENGTH)
            {
                // For shop to shop transfer
                if (_transferType == _SHOP_NO)
                {
                    //new impl jak
                    ICNSearchForInShopTransfer(icn);
                    return;
                }
                this.cmdICNSubmit.Enabled = true;

                if ((from i in _itemsToTransfer
                     where i.Icn == icn
                     select i).Any())
                {
                    MessageBox.Show("This item has already been scanned for transfer.", "Duplicate Item");
                    return;
                }
                else if (!(from i in _availableItems
                           where i.Icn == icn
                           select i).Any())
                {
                    MessageBox.Show("This item cannot be found in the available items.", "Item Not Available");
                    return;
                }
                else
                {
                    List<IItem> items = (from i in _availableItems
                                         where i.Icn == icn
                                         select i).ToList();

                    //SelectTransferItems selectItems = new SelectTransferItems(items, false);
                    //selectItems.ShowDialog();
                    //JewelryCaseDetails d = new JewelryCaseDetails();
                    //d.ShowDialog();

                    //if (d.ShouldContinue)
                    //{
                    //    items[0].JewelryCaseNumber = d.JewelryCaseNumber;
                    //}

                    if (this.IsScrapTransfer)
                        _itemsToTransfer.Add(items.First() as ScrapItem);
                    else
                        _itemsToTransfer.Add(items.First());

                    RefreshGridView();
                }
            }
            else
            {
                if (_catcoType.Equals(_REFURB))
                {
                    this.cmdICNSubmit.Enabled = true;
                }
                else
                {
                    if (icn.IndexOf(".") != -1)
                        this.cmdICNSubmit.Enabled = true;
                    else
                        this.cmdICNSubmit.Enabled = false;
                }
            }
        }

        private void TransferToList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowMessageOnListSelectionChange();

            //Build screens dynamically since this screen will be reused for different
            //merchandise transfer out types.
            _transferType = this.transferToList1.TransferToList.Text;

            // Place controls on panels based on selection.
            SetupLayoutTable();

            // Load available items for new selection.
            Init();
            RefreshGridView();

            CatcoTypeList_SelectedIndexChanged(sender, e);
        }

        #endregion Events

        private bool CATCOTransfer()
        {
            List<TransferItemVO> transferOutItems = null;
            int transferNumber = 0;
            string errorMessage = String.Empty;
            decimal totTransferAmount = 0.0m;

            bool retValue = TranferOutItems(
                out transferOutItems,
                out totTransferAmount,
                out transferNumber,
                out errorMessage);

            if (retValue
                && transferOutItems != null
                && transferOutItems.Count > 0
                //&& totTransferAmount > 0.0m
                && transferNumber > 0
                && (String.IsNullOrEmpty(errorMessage)
                    || errorMessage.Equals("Success", StringComparison.CurrentCultureIgnoreCase)
                )
            )
            {
                // Set TransferNumber property using value returned in the out parameter.
                foreach (TransferItemVO transfer in transferOutItems)
                {
                    transfer.TransferNumber = transferNumber;
                }
                //TODO: logic no longer needed?
                if (this.IsScrapTransfer)
                {
                    foreach (ScrapItem item in _itemsToTransfer)
                    {
                        item.TransferNumber = transferNumber;
                    }
                }
                try
                {
                    PrintTransferReport(transferOutItems);
                }
                catch (Exception e)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Manager Transfer out" + e.Message);
                    MessageBox.Show("Catco transfer document creation failed");
                }
                return true;
                //MessageBox.Show("Catco Transfer Out data successfully inserted");
            }
            else
            {
                if (String.IsNullOrEmpty(errorMessage) == false)
                {
                    MessageBox.Show(errorMessage, "Transfer Item(s)");
                    return false;
                }
            }

            return true;
        }

        private DesktopSession CDS
        {
            get
            {
                return GlobalDataAccessor.Instance.DesktopSession;
            }
        }

        #region Data Population based on Transfer type selection
        private List<IItem> getPFIItemForShopToGunRoom()
        {
            List<string> searchFor = new List<string>() { "MD_DESC" };
            List<string> searchValues = new List<string>() { "" };
            string reasonType = string.Empty;
            string errorText = null;
            string errorCode = null;
            List<RetailItem> searchRItems;
            List<IItem> items = new List<IItem>();
            string searchFlag = "EXPAND";
            //string searchFlag = "NORMAL";

            //RetailProcedures.SearchForItem(
            RetailProcedures.SearchForItem(searchFor, searchValues, CDS, searchFlag, true, out searchRItems, out errorCode, out errorText);

            addResultsToList(searchRItems, items, true, IsAppraisalOrWholesale(), out reasonType);
            return items;
        }

        private List<IItem> getPFIItemForShopToShop(String icn, out string reasonType)
        {
            List<string> searchFor = new List<string>() { "MD_DESC" };
            List<string> searchValues = new List<string>() { icn };

            string errorText = null;
            string errorCode = null;
            List<RetailItem> searchRItems;
            List<IItem> items = new List<IItem>();
            //string searchFlag="EXPAND";
            string searchFlag = "NORMAL";

            //RetailProcedures.SearchForItem(
            RetailProcedures.SearchForItem(searchFor, searchValues, CDS, searchFlag, true, out searchRItems, out errorCode, out errorText);

            var distinctItems = (from sItem in searchRItems
                                 where ((sItem.IsJewelry && sItem.Jewelry.Any(j => j.SubItemNumber == 0)) ||
                                 !sItem.IsJewelry)
                                 select sItem).ToList();

            if (searchRItems.Count > 0 && distinctItems.Count == 0)
            {
                if (searchRItems.First().Icn.Substring(17, 1) == "0")
                {
                    addResultsToList(new List<RetailItem> { searchRItems.First() }, items, false, IsAppraisalOrWholesale(), out reasonType);
                }
                else
                    reasonType = "Sub item";
            }
            else
            {
                addResultsToList(searchRItems, items, false, IsAppraisalOrWholesale(), out reasonType);
            }

            return items;
        }

        private List<IItem> icnBasedSearchForGun(String icn, out string reasonType)
        {
            List<string> searchFor = new List<string>() { "MD_DESC" };
            List<string> searchValues = new List<string>() { icn };

            string errorText = null;
            string errorCode = null;
            List<RetailItem> searchRItems;
            List<IItem> items = new List<IItem>();
            //string searchFlag="EXPAND";
            string searchFlag = "NORMAL";

            //RetailProcedures.SearchForItem(
            RetailProcedures.SearchForItem(searchFor, searchValues, CDS, searchFlag, true, out searchRItems, out errorCode, out errorText);

            addResultsToList(searchRItems, items, true, IsAppraisalOrWholesale(), out reasonType);

            return items;
        }

        private static void addResultsToList(List<RetailItem> searchRItems, List<IItem> items, bool forGunSrch, bool forAppraisalOrWholesale, out string reasonType)
        {
            IItem item = null;
            reasonType = string.Empty;
            //searchItems = new List<Item>();
            foreach (RetailItem anItem in searchRItems)
            {
                if (anItem.TempStatus == StateStatus.BLNK)
                {
                    if (forAppraisalOrWholesale)
                    {
                        if (!anItem.IsJewelry)
                        {
                            reasonType = "Non-jewelry items are not eligible for transfer.";
                            continue;
                        }
                        if (!string.IsNullOrWhiteSpace(anItem.TranType) && !anItem.TranType.Equals("N"))
                        {
                            reasonType = "The ICN# cannot be transferred because it's marked as \"" + anItem.TranType + "\"";
                            continue;
                        }
                    }

                    // during gun search if item is not gun , skip adding

                    if (forGunSrch && !anItem.IsGun)
                    {
                        reasonType = "Not a Fire Arm";
                        continue;
                    }

                    //for gun search if TRAN TYPE is not CAF skip
                    if (forGunSrch && anItem.IsGun)
                    {
                        if (anItem.TranType != null && (!anItem.TranType.Equals("C"))) // Guns can be added if they are marked "N"
                        {
                            reasonType = "Not Marked for CAF";
                            continue;
                        }
                    }

                    //if srch is not for gun room and gun is found then skip adding
                    if (!forGunSrch && anItem.IsGun)
                    {
                        if (anItem.TranType == null || anItem.TranType.Equals(string.Empty))
                        {
                            //consider adding the record
                        }
                        else if (anItem.TranType != null && (anItem.TranType.Equals("C"))) // Guns can be added if they are marked "N"
                        {
                            reasonType = "CAF";
                            break;
                        }
                    }

                    // if catco item searched for shop to shop skip adding
                    if (!forGunSrch) //incase of gun srch dont
                    {
                        if (anItem.TranType != null)
                        {
                            /*if (anItem.TranType.Equals("SB"))
                            {
                            reasonType = "SB";
                            break;
                            }*/
                            if (!anItem.TranType.Equals(string.Empty)) // if empty allow it to add
                            {
                                if (!anItem.TranType.Equals("N")) //any thing other Normal break
                                { // Filtered items are SB,S,R,E. Allowed are N and Empty
                                    reasonType = anItem.TranType;
                                    break;
                                }
                            }
                        }
                    }

                    item = new Item();
                    item.RefurbNumber = anItem.RefurbNumber;
                    item.Icn = anItem.Icn;
                    item.TicketDescription = anItem.TicketDescription;
                    item.ItemAmount = anItem.ItemAmount;
                    item.ItemStatus = anItem.ItemStatus;
                    item.mDocType = anItem.mDocType;
                    item.mStore = anItem.mStore;
                    item.CategoryCode = anItem.CategoryCode;
                    item.CategoryDescription = anItem.CategoryDescription;
                    item.mItemOrder = anItem.mItemOrder;
                    item.CaccLevel = anItem.CaccLevel;
                    item.Attributes = anItem.Attributes;
                    item.Quantity = anItem.Quantity;
                    item.IsGun = anItem.IsGun;
                    item.GunNumber = anItem.GunNumber;
                    item.IsJewelry = anItem.IsJewelry;
                    if (item.IsJewelry)
                    {
                        item.Jewelry = anItem.Jewelry;
                    }
                    if (new Icn(item.Icn).DocumentType == DocumentType.CaccItem)
                    {
                        item.ItemAmount = Math.Round(anItem.PfiAmount / anItem.Quantity, 2);
                        item.Quantity = 1;
                    }
                    //item.QuickInformation = anItem.QuickInformation;
                    items.Add(item);
                }

            }
        }

        #endregion Data Population based on Transfer type selection

        // This Method for will invoke transfer WS
        private void callWS_Click()
        {
            bool errorbool = false;
            var errorCode = string.Empty;
            var errorMessage = string.Empty;
            string pStoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            string transferDate = ShopDateTime.Instance.ShopDate.ToShortDateString();
            DataTable transferItems = new DataTable();

            if (_catcoType.Equals(_SCRAP, StringComparison.CurrentCultureIgnoreCase))
            {
                TransfersDBProcedures.ExecuteGetTOTickets(
                    pStoreNumber, transferDate, out transferItems, out errorCode, out errorMessage, TransferProcedures._TRAN_TYPE_SCRAP);

                errorbool = TransferProcedures.InvokeWebServiceForTransfer(pStoreNumber, transferItems, out errorMessage, TransferProcedures._TRAN_TYPE_SCRAP);
            }
            else if (_catcoType.Equals(_EXCESS, StringComparison.CurrentCultureIgnoreCase))
            {
                //Transfer Excess
                TransfersDBProcedures.ExecuteGetTOTickets(
                    pStoreNumber, transferDate, out transferItems, out errorCode, out errorMessage, TransferProcedures._TRAN_TYPE_EXCESS);
                errorbool = TransferProcedures.InvokeWebServiceForTransfer(pStoreNumber, transferItems, out errorMessage, TransferProcedures._TRAN_TYPE_EXCESS);
            }
            else if (_catcoType.Equals(_REFURB, StringComparison.CurrentCultureIgnoreCase))
            {
                //Transfer Refurb
                TransfersDBProcedures.ExecuteGetTOTickets(
                    pStoreNumber, transferDate, out transferItems, out errorCode, out errorMessage, TransferProcedures._TRAN_TYPE_REFURB);

                errorbool = TransferProcedures.InvokeWebServiceForTransfer(pStoreNumber, transferItems, out errorMessage, TransferProcedures._TRAN_TYPE_REFURB);
            }

            if (!errorbool)
            {
                if (errorMessage != null)
                    MessageBox.Show(errorMessage);
            }
            else
            {
                MessageBox.Show("Catco Transfer Out data successfully inserted");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || (this.ActiveControl.Equals(cancelButton) && keyData == Keys.Enter))
            {
                this.cancelButton_Click(null, new EventArgs());
                return true;
            }

            if (keyData == Keys.Enter && (this.ActiveControl.Equals(cmdICNSubmit) || this.ActiveControl.Equals(txtIcn)))
            {
                if (this.cmdICNSubmit.Enabled)
                {
                    this.cmdICNSubmit_Click(null, new EventArgs());
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void gvMerchandise_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colQuantity.Index)
            {
                return;
            }

            DataGridViewCell qtyCell = gvMerchandise[e.ColumnIndex, e.RowIndex];

            gvMerchandise.CommitEdit(DataGridViewDataErrorContexts.Commit);


            qtyCell.ReadOnly = false;
            qtyCell.Style.BackColor = SystemColors.Highlight;
            qtyCell.Style.ForeColor = Color.White;
            gvMerchandise.BeginEdit(false);
        }

        private void gvMerchandise_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colQuantity.Index)
            {
                return;
            }

            DataGridViewCell qtyCell = gvMerchandise[e.ColumnIndex, e.RowIndex];
            IItem item = gvMerchandise.Rows[e.RowIndex].Tag as IItem;
            int currentValue = Utilities.GetIntegerValue(qtyCell.EditedFormattedValue);

            if (currentValue <= 0)
            {
                MessageBox.Show("Invalid quantity.");
                gvMerchandise.CancelEdit();
                return;
            }

            item.Quantity = currentValue;
            gvMerchandise.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void gvMerchandise_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colQuantity.Index)
            {
                return;
            }

            var item = gvMerchandise.Rows[e.RowIndex].Tag as IItem;
            DataGridViewCell qtyCell = gvMerchandise[e.ColumnIndex, e.RowIndex];
            DataGridViewCell costCell = gvMerchandise[colCost.Index, e.RowIndex];

            if (item == null || qtyCell == null || costCell == null)
            {
                return;
            }

            int count = Utilities.GetIntegerValue(qtyCell.Value);
            costCell.Value = count * item.ItemAmount;

            gvMerchandise.Refresh();
        }
    }
}
