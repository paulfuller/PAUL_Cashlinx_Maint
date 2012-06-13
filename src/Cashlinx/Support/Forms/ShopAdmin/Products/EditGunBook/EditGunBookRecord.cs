using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Support.Libraries.Objects.Customer;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Support.Logic;

namespace Support.Forms.ShopAdmin.EditGunBook
{
    public partial class EditGunBookRecord : CustomBaseForm
    {
        private DataTable gunBookData;
        private Item gunItemData;
        private CustomerVOForSupportApp gunBookCustomer;
        private string acquireCustFirstName;
        private string acquireCustLastName;
        private string acquireCustMiddleName;
        private string dispositionCustFirstName;
        private string dispositionCustLastName;
        private string dispositionCustMiddleName;
        private string acquireCustomerAddress1;
        private string acquireCustomerCity;
        private string acquireCustomerState;
        private string acquireCustomerZipcode;
        private string acquireCustIDType;
        private string acquireCustIDNumber;
        private string acquireCustIDAgency;
        private string acquireCustNumber;
        private string dispositionCustomerAddress1;
        private string dispositionCustomerCity;
        private string dispositionCustomerState;
        private string dispositionCustomerZipcode;
        private string dispositionCustIDType;
        private string dispositionCustIDNumber;
        private string dispositionCustIDAgency;
        private string dispositionCustNumber;
        private string acquireTransactionType;
        private string dispTransactionType;
        private string gunCACCCode;

        public NavBox NavControlBox;

        private string icnDocType;

        public EditGunBookRecord()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
            //{
            //    Owner = this
            //};

        }

        private void EditGunBookRecord_Load(object sender, EventArgs e)
        {
            this.NavControlBox.Owner = this;
            gunBookData = GlobalDataAccessor.Instance.DesktopSession.GunData;
            gunItemData = GlobalDataAccessor.Instance.DesktopSession.GunItemData;
            if (gunBookData != null && gunBookData.Rows.Count > 0)
            {
                gunCACCCode = Utilities.GetStringValue(gunBookData.Rows[0]["cat_code"]);
                currentGunNo.Text = Utilities.GetStringValue(gunBookData.Rows[0]["gun_number"]);
                originalGunNo.Text = Utilities.GetStringValue(gunBookData.Rows[0]["original_gun_number"]);
                newGunNo.Text = Utilities.GetStringValue(gunBookData.Rows[0]["new_gun_number"]);
                status.Text = Utilities.GetStringValue(gunBookData.Rows[0]["status_cd"]);
                statusDate.Text = Utilities.GetDateTimeValue(gunBookData.Rows[0]["status_date"]).ToString("d", DateTimeFormatInfo.InvariantInfo);
                gunBound.Text = Utilities.GetStringValue(gunBookData.Rows[0]["gun_bound"]);
                pageRecord.Text = Utilities.GetStringValue(gunBookData.Rows[0]["gun_page"]) + "/" + Utilities.GetStringValue(gunBookData.Rows[0]["record_number"]);
                manufacturer.Text = Utilities.GetStringValue(gunBookData.Rows[0]["manufacturer"]);
                model.Text = Utilities.GetStringValue(gunBookData.Rows[0]["model"]);
                serialNumber.Text = Utilities.GetStringValue(gunBookData.Rows[0]["serial_number"]);
                caliber.Text = Utilities.GetStringValue(gunBookData.Rows[0]["caliber"]);
                type.Text = Utilities.GetStringValue(gunBookData.Rows[0]["gun_type"]);
                importer.Text = Utilities.GetStringValue(gunBookData.Rows[0]["importer"]);
                icnDocType = Utilities.GetStringValue(gunBookData.Rows[0]["icn_doc_type"]);
                icn.Text = Utilities.IcnGenerator(Utilities.GetIntegerValue(gunBookData.Rows[0]["icn_store"]),
                    Utilities.GetIntegerValue(gunBookData.Rows[0]["icn_year"]),
                    Utilities.GetIntegerValue(gunBookData.Rows[0]["icn_doc"]),
                    Utilities.GetStringValue(gunBookData.Rows[0]["icn_doc_type"]),
                    Utilities.GetIntegerValue(gunBookData.Rows[0]["icn_item"]),
                    Utilities.GetIntegerValue(gunBookData.Rows[0]["icn_sub_item"]));
                //acquisition data
                acquireCustNumber = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_customer_number"]);
                acquisitionCustomerNo.Text = acquireCustNumber;
                acquireTransactionType = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_transaction_type"]);
                acquisitionType.Text = acquireTransactionType;
                acquireCustFirstName = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_first_name"]);
                acquireCustLastName = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_last_name"]);
                acquireCustMiddleName = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_middle_initial"]);
                acquisitionName.Text = acquireCustFirstName + " " + acquireCustMiddleName + " " + acquireCustLastName;
                acquisitionTicket.Text = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_document_number"]);
                acquireCustomerAddress1 = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_address"]);
                acquisitionAddress1.Text = acquireCustomerAddress1;
                acquireCustomerCity = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_city"]);
                acquireCustomerState = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_state"]);
                acquireCustomerZipcode = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_postal_code"]);
                acquisitionAddress2.Text = acquireCustomerCity + "," + acquireCustomerState + " " + acquireCustomerZipcode;
                acquisitionDate.Text = Utilities.GetDateTimeValue(gunBookData.Rows[0]["acquire_date"]).ToShortDateString();
                acquireCustIDType = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_id_type"]);
                acquireCustIDNumber = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_id_number"]);
                acquireCustIDAgency = Utilities.GetStringValue(gunBookData.Rows[0]["acquire_id_agency"]);
                acquisitionID.Text = acquireCustIDType + " " + acquireCustIDAgency + " " + acquireCustIDNumber;
                //disposition data
                dispositionCustNumber = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_customer_number"]);
                dispositionCustomerNo.Text = dispositionCustNumber;
                dispTransactionType = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_transaction_type"]);
                dispositionType.Text = dispTransactionType;
                dispositionCustLastName = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_last_name"]);
                dispositionCustFirstName = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_first_name"]);
                dispositionCustMiddleName = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_middle_initial"]);
                dispositionName.Text = dispositionCustFirstName + " " + dispositionCustMiddleName + " " + dispositionCustLastName;
                dispositionTicket.Text = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_document_number"]);
                dispositionCustomerAddress1 = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_address"]);
                dispositionAddress1.Text = dispositionCustomerAddress1;
                dispositionCustomerCity = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_city"]);
                dispositionCustomerState = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_state"]);
                dispositionCustomerZipcode = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_postal_code"]);
                dispositionAddress2.Text = dispositionCustomerCity + "," + dispositionCustomerState + " " + dispositionCustomerZipcode;
                dispositionDate.Text = Utilities.GetDateTimeValue(gunBookData.Rows[0]["disposition_date"]).ToShortDateString();
                dispositionCustIDType = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_id_type"]);
                dispositionCustIDAgency = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_id_agency"]);
                dispositionCustIDNumber = Utilities.GetStringValue(gunBookData.Rows[0]["disposition_id_number"]);
                dispositionID.Text = dispositionCustIDType + " " + dispositionCustIDAgency + " " + dispositionCustIDNumber;
                string gunStatus = Utilities.GetStringValue((gunBookData.Rows[0]["status_cd"]));
                if (gunStatus == "VO" || gunStatus == "PS")
                {
                    labelErrMessage.Text = Commons.GetMessageString("GunEditError");
                    DisableActions();

                }
                if (string.IsNullOrEmpty(dispositionCustNumber))
                {
                    DispositionReplace.Enabled = false;
                    DispositionEdit.Enabled = false;
                }
                if (!SecurityProfileProcedures.CanUserModifyResource("EDIT GUN BOOK", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, CashlinxPawnSupportSession.Instance) &&
                    !SecurityProfileProcedures.CanUserModifyResource("EDIT RESTRICTED GUN BOOK FIELDS", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, CashlinxPawnSupportSession.Instance))
                {
                    //firearmDescEdit.Enabled = false;
                    //AcquisitionEdit.Enabled = false;
                    //AcquisitionReplace.Enabled = false;
                    //DispositionEdit.Enabled = false;
                    //DispositionReplace.Enabled = false;
                }
                if (string.IsNullOrEmpty(acquireCustNumber))
                {
                    AcquisitionEdit.Enabled = false;
                    AcquisitionReplace.Enabled = false;
                }
                if (acquireTransactionType == "T" || acquireTransactionType == "C")
                {
                    AcquisitionEdit.Enabled = false;
                    AcquisitionReplace.Enabled = false;

                }
                if (dispTransactionType == "T" || dispTransactionType == "C")
                {
                    DispositionReplace.Enabled = false;
                    DispositionEdit.Enabled = false;

                }



            }
            else
            {
                labelErrMessage.Text = "Gun Book data not found";
                DisableActions();
            }


        }

        private void DisableActions()
        {
            labelErrMessage.Visible = true;
            firearmDescEdit.Enabled = false;
            AcquisitionReplace.Enabled = false;
            AcquisitionEdit.Enabled = false;
            DispositionReplace.Enabled = false;
            DispositionEdit.Enabled = false;

        }

        private void customButtonClose_Click(object sender, EventArgs e)
        {
            string errorCode;
            string errorText;
            RetailProcedures.LockItem(CashlinxPawnSupportSession.Instance, icn.Text, out errorCode, out errorText, "N");
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void AcquisitionEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadItemData();

            gunBookCustomer = new CustomerVOForSupportApp();
            gunBookCustomer.FirstName = acquireCustFirstName;
            gunBookCustomer.LastName = acquireCustLastName;
            gunBookCustomer.MiddleInitial = acquireCustMiddleName;
            AddressVO addr1 = new AddressVO();

            addr1.Address1 = acquireCustomerAddress1;
            addr1.City = acquireCustomerCity;
            addr1.State_Code = acquireCustomerState;
            addr1.ZipCode = acquireCustomerZipcode;
            addr1.ContactTypeCode = CustomerAddressTypes.HOME_ADDRESS;
            addr1.ContMethodTypeCode = "POSTALADDR";

            gunBookCustomer.addAddress(addr1);
            gunBookCustomer.CustomerNumber = acquireCustNumber;
            IdentificationVO id = new IdentificationVO();
            id.IdType = acquireCustIDType;
            id.IdValue = acquireCustIDNumber;
            id.IdIssuerCode = acquireCustIDAgency;
            id.IsLatest = true;
            gunBookCustomer.addIdentity(id);
            CashlinxPawnSupportSession.Instance.ActiveCustomer = gunBookCustomer;
            CashlinxPawnSupportSession.Instance.GunAcquireCustomer = true;
            CashlinxPawnSupportSession.Instance.CustomerEditType = CustomerType.RECEIPT;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "EditCustomer";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;


        }

        private void DispositionReplace_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadItemData();
            CashlinxPawnSupportSession.Instance.GunAcquireCustomer = false;
            CashlinxPawnSupportSession.Instance.CustomerEditType = CustomerType.NONE;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "LookupCustomer";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }

        private void AcquisitionReplace_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadItemData();
            CashlinxPawnSupportSession.Instance.GunAcquireCustomer = true;
            CashlinxPawnSupportSession.Instance.CustomerEditType = CustomerType.NONE;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "LookupCustomer";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }

        private void DispositionEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadItemData();

            gunBookCustomer = new CustomerVOForSupportApp();
            gunBookCustomer.FirstName = dispositionCustFirstName;
            gunBookCustomer.LastName = dispositionCustLastName;
            gunBookCustomer.MiddleInitial = dispositionCustMiddleName;
            AddressVO addr1 = new AddressVO();

            addr1.Address1 = dispositionCustomerAddress1;
            addr1.City = dispositionCustomerCity;
            addr1.State_Code = dispositionCustomerState;
            addr1.ZipCode = dispositionCustomerZipcode;
            addr1.ContactTypeCode = CustomerAddressTypes.HOME_ADDRESS;
            addr1.ContMethodTypeCode = "POSTALADDR";
            gunBookCustomer.addAddress(addr1);
            gunBookCustomer.CustomerNumber = dispositionCustNumber;
            IdentificationVO id = new IdentificationVO();
            id.IdType = dispositionCustIDType;
            id.IdValue = dispositionCustIDNumber;
            id.IdIssuerCode = dispositionCustIDAgency;
            id.IsLatest = true;
            gunBookCustomer.addIdentity(id);
            CashlinxPawnSupportSession.Instance.ActiveCustomer = gunBookCustomer;
            CashlinxPawnSupportSession.Instance.GunAcquireCustomer = false;
            CashlinxPawnSupportSession.Instance.CustomerEditType = CustomerType.DISPOSITION;
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "EditCustomer";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;


        }

        private void firearmDescEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (gunItemData != null)
            {
                LoadItemData();
                int iCategoryMask = Utilities.GetIntegerValue(gunCACCCode);
                DescribedMerchandise dmPawnItem = new DescribedMerchandise(iCategoryMask);
                Item pawnItem = CashlinxPawnSupportSession.Instance.ActivePawnLoan.Items[0];
                if (pawnItem.Jewelry == null || pawnItem.Jewelry.Count == 0)
                    dmPawnItem.SelectedPawnItem.Jewelry = null;
                // Due to holding and updated Item Amount, add it to Selected Pawn Item
                dmPawnItem.SelectedPawnItem.ItemAmount = pawnItem.ItemAmount;
                Item.PawnItemMerge(ref pawnItem, dmPawnItem.SelectedPawnItem, false);
                // Update local PFI Active Loan Pawn Item with Cat5 Info
                pawnItem.CategoryMask = iCategoryMask;
                pawnItem.ItemReason = ItemReason.BLNK;

                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.RemoveAt(0);
                GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan.Items.Insert(0, pawnItem);
                GlobalDataAccessor.Instance.DesktopSession.DescribeItemContext = CurrentContext.GUNEDIT;
                GlobalDataAccessor.Instance.DesktopSession.DescribeItemPawnItemIndex = 0;

                var cdSession = CashlinxPawnSupportSession.Instance;
                //cdSession.ResourceProperties.oldvistabutton_blue = Properties.Resources.oldvistabutton_blue;
                cdSession.ResourceProperties.vistabutton_blue = Properties.Resources.vistabutton_blue;
                cdSession.ResourceProperties.cl1 = Common.Properties.Resources.cl1;
                cdSession.ResourceProperties.cl2 = Common.Properties.Resources.cl2;
                cdSession.ResourceProperties.cl3 = Common.Properties.Resources.cl3;
                cdSession.ResourceProperties.cl4 = Common.Properties.Resources.cl4;
                cdSession.ResourceProperties.cl5 = Common.Properties.Resources.cl5;
                cdSession.ResourceProperties.newDialog_400_BlueScale = Common.Properties.Resources.newDialog_400_BlueScale;
                cdSession.ResourceProperties.newDialog_512_BlueScale = Common.Properties.Resources.newDialog_512_BlueScale;
                cdSession.ResourceProperties.newDialog_600_BlueScale = Common.Properties.Resources.newDialog_600_BlueScale;

                cdSession.ResourceProperties.OverrideMachineName = global::Support.Properties.Resources.OverrideMachineName;
                cdSession.HistorySession.Trigger = Commons.TriggerTypes.GUNBOOKEDIT;

                NavControlBox.IsCustom = true;
                NavControlBox.CustomDetail = "DescribeMerchandise";
                NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

                //DescribeMerchandise descMerchFrm = new DescribeMerchandise(GlobalDataAccessor.Instance.DesktopSession, CurrentContext.GUNEDIT);
                //descMerchFrm.ShowDialog();
            }
        }

        /*private void firearmDescEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadItemData();
            NavControlBox.IsCustom = true;
            NavControlBox.CustomDetail = "DescribeMerchandise";
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }*/


        private void LoadItemData()
        {
            GlobalDataAccessor.Instance.DesktopSession.PawnLoans = new List<PawnLoan>(1)
                                                                    {
                                                                        new PawnLoan()
                                                                    };
            if (GlobalDataAccessor.Instance.DesktopSession.ActivePawnLoan != null)
            {
                CashlinxPawnSupportSession.Instance.ActivePawnLoan.Items = new List<Item>(1);
                CashlinxPawnSupportSession.Instance.ActivePawnLoan.Items.Add(Utilities.CloneObject(gunItemData));



            }

        }

    }
}
