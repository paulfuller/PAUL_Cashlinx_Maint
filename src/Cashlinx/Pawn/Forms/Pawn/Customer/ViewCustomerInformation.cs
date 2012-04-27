/*****************************************************************************************************
* CashlinxDesktop
* ViewCustomerInformation
* This form is shown when a customer is selected in lookup results and the View
* button is clicked
* Sreelatha Rengarajan 5/14/2009 Initial version
*
* 2-mar-2010 rjm   changed width of panels tableLayoutPanel1,
*               tableLayoutPanel2, tableLayoutPanel3, and tableLayoutPanel4
*               to allow resizing of all address1 labels. This fixed
*               a problem with the app truncating data that would
*               not fit in the label when exceeding 38 characters in length
* 5/24/2010 SR Changed the font and increased the max width of the address lines so that it can show
 * the entire text if its 40 characters and it was updated in this session
*******************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CashlinxDesktop.DesktopForms.Pawn.Customer;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer
{
    public partial class ViewCustomerInformation : Form
    {
        private CustomerVO _customerToView;
        private CustomerVO _updatedCustomer;

        private bool _loadUpdatedData = false;


        ContactVO _custHomeNumber;
        ContactVO _custCellNumber;
        ContactVO _custWorkNumber;
        ContactVO _customerPager;
        ContactVO _customerFax;
        ContactVO _custPrimaryNumber;

        AddressVO _custPhysicalAddress;
        AddressVO _custMailingAddress;
        AddressVO _custWorkAddress;


        List<CustLoanLostTicketFee> customerLoans = new List<CustLoanLostTicketFee>();
        Form ownerfrm;
        public NavBox NavControlBox;


        //After the individual updates(personal info, address, comments, ids and contact info
        //this customer object is set with the new values and not the original customer object
        //_customerToView which is set in the lookup results form
        //This allows us to compare the values in these 2 objects in order to show the 
        //updated values in bold.
        public CustomerVO UpdatedCustomerToView
        {
            set
            {
                _updatedCustomer = value;
            }
        }

        public bool ShowUpdates
        {
            set
            {
                _loadUpdatedData = value;
            }
        }

        private bool _showReadOnly;
        public bool ShowReadOnly
        {
            get
            {
                return _showReadOnly;
            }
            set
            {
                _showReadOnly = value;
                UpdateLinks();
            }
        }


        public ViewCustomerInformation()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();

            this.custContactPermission.Text = "";
            this.custDOB.Text = "";
            this.custEmail.Text = "";
            this.custFax.Text = "";
            this.custHeardAboutUsFrom.Text = "";
            this.custHomePhone.Text = "";
            this.custMailingAddr.Text = "";
            this.custMailingAddr2.Text = "";
            this.custMailingAddrCountry.Text = "";
            this.custMailingAddrNotes.Text = "";
            this.custMobilePhone.Text = "";
            this.custName.Text = "";
            this.custNumber.Text = "";
            this.custPager.Text = "";
            this.custPhysicalAddr.Text = "";
            this.custPhysicalAddr2.Text = "";
            this.custPhysicalAddrCountry.Text = "";
            this.custPhysicalAddrNotes.Text = "";
            this.custPreferredCallTime.Text = "";
            this.custPreferredContact.Text = "";
            this.custPrimaryPhone.Text = "";
            this.custReceivePromotions.Text = "";
            this.custReminderContact.Text = "";
            this.custSince.Text = "";
            this.custSSN.Text = "";
            this.custWorkAddr.Text = "";
            this.custWorkAddr2.Text = "";
            this.custWorkAddrCountry.Text = "";
            this.custWorkAddrNotes.Text = "";
            this.custWorkPhone.Text = "";
            this.tabControl2.DrawMode = TabDrawMode.OwnerDrawFixed;
            ShowReadOnly = false;


        }

        //SR 01/08/2009
        //Since we have the cancel button in this form we do not need to process escape key anymore
        /*protected override System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.NavControlBox.Action = NavBox.NavAction.BACK;
                //return true to indicate that the key has been handled
                return true;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }*/









        private void linkLabelPersonalInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _loadUpdatedData = false;
            UpdateCustomerDetails custDetailsForm =
                new UpdateCustomerDetails
                {
                    CustToEdit = _customerToView
                };
            //set the data in the form from the customer object being viewed
            //Show the form as a modal
            custDetailsForm.ShowDialog(this);
            if (_loadUpdatedData)
                LoadCustPersonalInfo();



        }





        private void linkLabelPhysicalAddr_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _loadUpdatedData = false;
            //Create a new customer object with just the addresses from the
            //customer being viewed
            CustomerVO customerToUpdateAddr = new CustomerVO();
            customerToUpdateAddr.addAddress(_customerToView.getHomeAddress());
            customerToUpdateAddr.addAddress(_customerToView.getMailingAddress());
            customerToUpdateAddr.addAddress(_customerToView.getWorkAddress());
            customerToUpdateAddr.addAddress(_customerToView.getAdditionalAddress());
            customerToUpdateAddr.CustomerNumber = _customerToView.CustomerNumber;
            customerToUpdateAddr.PartyId = _customerToView.PartyId;

            UpdateAddress addrForm = new UpdateAddress
                                         {
                                             MailingAddress = false,
                                             WorkAddress = false,
                                             PhysicalAddress = true,
                                             CustAddrToView = customerToUpdateAddr
                                         };
            addrForm.ShowDialog(this);
            if (_loadUpdatedData)
                LoadPhysicalAddr();

        }

        private void linkLabelMailingAddr_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _loadUpdatedData = false;
            //create a new customer object with just the addresses from the customer being viewed
            CustomerVO customerToUpdateAddr = new CustomerVO();
            customerToUpdateAddr.addAddress(_customerToView.getHomeAddress());
            customerToUpdateAddr.addAddress(_customerToView.getMailingAddress());
            customerToUpdateAddr.addAddress(_customerToView.getWorkAddress());
            customerToUpdateAddr.addAddress(_customerToView.getAdditionalAddress());
            customerToUpdateAddr.CustomerNumber = _customerToView.CustomerNumber;
            customerToUpdateAddr.PartyId = _customerToView.PartyId;

            UpdateAddress addrForm = new UpdateAddress
                                         {
                                             MailingAddress = true,
                                             WorkAddress = false,
                                             PhysicalAddress = false,
                                             CustAddrToView = customerToUpdateAddr
                                         };
            addrForm.ShowDialog(this);
            if (_loadUpdatedData)
                LoadMailingAddr();

        }

        private void linkLabelWorkAddr_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _loadUpdatedData = false;
            //create a new customer object with just the addresses from the customer being viewed
            var customerToUpdateAddr = new CustomerVO();
            customerToUpdateAddr.addAddress(_customerToView.getHomeAddress());
            customerToUpdateAddr.addAddress(_customerToView.getMailingAddress());
            customerToUpdateAddr.addAddress(_customerToView.getWorkAddress());
            customerToUpdateAddr.addAddress(_customerToView.getAdditionalAddress());
            customerToUpdateAddr.CustomerNumber = _customerToView.CustomerNumber;
            customerToUpdateAddr.PartyId = _customerToView.PartyId;

            var addrForm = new UpdateAddress
                                         {
                                             MailingAddress = false,
                                             WorkAddress = true,
                                              PhysicalAddress = false,
                                             CustAddrToView = customerToUpdateAddr
                                         };
            addrForm.ShowDialog(this);
            if (_loadUpdatedData)
                LoadWorkAddr();

        }

        private void linkLabelCommentsEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _loadUpdatedData = false;
            UpdateCommentsandNotes commentsForm = new UpdateCommentsandNotes
                                                      {
                                                          ViewCommentsAndNotes = false,
                                                          CustToEdit = _customerToView
                                                      };
            commentsForm.ShowDialog(this);
            if (_loadUpdatedData)
            {
                this.tableLayoutPanel9.Controls.Clear();
                LoadLatestComment();
            }
        }

        private void linkLabelCommentsView_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            UpdateCommentsandNotes commentsForm = new UpdateCommentsandNotes
                                                      {
                                                          ViewCommentsAndNotes = true,
                                                          CustToEdit = _customerToView
                                                      };
            commentsForm.ShowDialog(this);

        }

        private void linkLabelContactInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _loadUpdatedData = false;
            UpdateCustomerContactDetails contactsForm = new UpdateCustomerContactDetails
                                                            {
                                                                CustomerToEdit = _customerToView
                                                            };
            contactsForm.ShowDialog(this);
            if (_loadUpdatedData)
                LoadCustContactInfo();

        }

        private void linkLabelIDEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _loadUpdatedData = false;
            UpdateCustomerIdentification custIdForm = new UpdateCustomerIdentification
            {
                CustToEdit = _customerToView
            };
            custIdForm.ShowDialog(this);
            if (_loadUpdatedData)
            {
                this.tableLayoutPanel6.Controls.Clear();
                LoadIdentityData();
            }

        }

        private void linkLabelCustDoc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Customer Documents edit form will be shown");
        }


        private void tabControl2_DrawItem(object sender, DrawItemEventArgs e)
        {

            Graphics g = e.Graphics;
            TabPage tp = tabControl2.TabPages[e.Index];
            Brush brBack;
            Brush brFore;
            StringFormat sf = new StringFormat();
            Font xFont;
            RectangleF rBorder = new RectangleF(tabControl2.Left - 3, tabControl2.Top - 12, tabControl2.Width + 1, tabControl2.Height + 1);
            RectangleF r = new RectangleF(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height - 1);
            sf.Alignment = StringAlignment.Center;
            String strTitle = tp.Text;
            brBack = new SolidBrush(tp.BackColor);

            g.FillRectangle(brBack, e.Bounds);     //tab page itself
            if (e.State == DrawItemState.Selected)
            {
                brFore = new SolidBrush(Color.Black);
                xFont = new Font(tabControl2.Font, FontStyle.Bold);
                g.DrawString(strTitle, xFont, brFore, r, sf);    //Draw the labels.
            }
            else
            {
                brFore = new SolidBrush(tp.ForeColor);
                xFont = new Font(tabControl2.Font, FontStyle.Regular);
                g.DrawString(strTitle, xFont, brFore, r, sf);    //Draw the labels.
            }


            g.Dispose();
            brFore.Dispose();
            brBack.Dispose();

        }

        private void ViewCustomerInformation_Load(object sender, EventArgs e)
        {
            ownerfrm = this.Owner;
            this.NavControlBox.Owner = this;
            LoadCustomerInfoToView();
            //SR 2/16/2010 Roles and resources check added
            //check the privileges of the logged in user to determine
            //if the user can edit any information
            if (!(SecurityProfileProcedures.CanUserModifyResource("EDITCUSTINFO", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, GlobalDataAccessor.Instance.DesktopSession)))
                ShowReadOnly=true;


        }

        private void UpdateLinks()
        {
            //Disable the edit buttons if its a read only view
            this.linkLabelCommentsEdit.Enabled = !ShowReadOnly;
            this.linkLabelContactInfo.Enabled = !ShowReadOnly;
            this.linkLabelIDEdit.Enabled = !ShowReadOnly;
            this.linkLabelMailingAddr.Enabled = !ShowReadOnly;
            this.linkLabelPersonalInfo.Enabled = !ShowReadOnly;
            this.linkLabelPhysicalAddr.Enabled = !ShowReadOnly;
            this.linkLabelPhysicalDesc.Enabled = !ShowReadOnly;
            this.linkLabelWorkAddr.Enabled = !ShowReadOnly;
            this.linkLabelCustDoc.Enabled = !ShowReadOnly;
            if (ShowReadOnly)
                this.customButtonCancel.Text = "Close";
            else
                this.customButtonCancel.Text = "Cancel";
        }

        private void LoadCustomerInfoToView()
        {
            try
            {
                _customerToView = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                if (_customerToView != null)
                {
                    //Load Contact Information
                    LoadCustContactInfo();

                    //Load Personal Information
                    LoadCustPersonalInfo();

                    //Load Mailing Address
                    LoadMailingAddr();

                    //Load Physical Address
                    LoadPhysicalAddr();

                    //Load work address
                    LoadWorkAddr();

                    //Load Customer Identities
                    LoadIdentityData();

                    //Load Physical Description data
                    LoadPhysicalDescData();
                    //Show the latest comment
                    LoadLatestComment();


                }
            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Error in View customer Information load " + ex.Message, new ApplicationException("Error in View Customer Information", ex));
                //CashlinxDesktopSession.Instance.HistorySession.Back();
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }


        }





        private void LoadCustContactInfo()
        {
            ContactVO custUpdatedHomeNumber = null;
            ContactVO custUpdatedCellNumber = null;
            ContactVO custUpdatedWorkNumber = null;
            ContactVO custUpdatedPager = null;
            ContactVO custUpdatedFax = null;
            ContactVO custUpdatedPrimaryNumber = null;


            _custHomeNumber = _customerToView.getContact(CustomerPhoneTypes.HOME_NUMBER);
            _custCellNumber = _customerToView.getContact(CustomerPhoneTypes.MOBILE_NUMBER);
            _custWorkNumber = _customerToView.getContact(CustomerPhoneTypes.WORK_NUMBER);
            _customerPager = _customerToView.getContact(CustomerPhoneTypes.PAGER_NUMBER);
            _customerFax = _customerToView.getContact(CustomerPhoneTypes.FAX_NUMBER);
            _custPrimaryNumber = _customerToView.getPrimaryContact();

            if (_updatedCustomer != null)
            {
                custUpdatedHomeNumber = _updatedCustomer.getContact(CustomerPhoneTypes.HOME_NUMBER);
                custUpdatedCellNumber = _updatedCustomer.getContact(CustomerPhoneTypes.MOBILE_NUMBER);
                custUpdatedWorkNumber = _updatedCustomer.getContact(CustomerPhoneTypes.WORK_NUMBER);
                custUpdatedPager = _updatedCustomer.getContact(CustomerPhoneTypes.PAGER_NUMBER);
                custUpdatedFax = _updatedCustomer.getContact(CustomerPhoneTypes.FAX_NUMBER);
                custUpdatedPrimaryNumber = _updatedCustomer.getPrimaryContact();
            }


            StringBuilder strContactPermission = new StringBuilder();
            if (_updatedCustomer != null && _updatedCustomer.NoCallFlag != _customerToView.NoCallFlag)
            {
                this.custContactPermission.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                if (_updatedCustomer.NoCallFlag == "Y")
                    strContactPermission.Append(CustomerContactPermissions.NOCALLS);
            }
            else
            {
                if (_customerToView.NoCallFlag == "Y")
                    strContactPermission.Append(CustomerContactPermissions.NOCALLS);
            }
            if (_updatedCustomer != null && _updatedCustomer.NoEmailFlag != _customerToView.NoEmailFlag)
            {
                this.custContactPermission.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                if (_updatedCustomer.NoEmailFlag == "Y")
                {
                    if (strContactPermission.Length > 0)
                    {
                        strContactPermission.Append(",");
                    }
                    strContactPermission.Append(CustomerContactPermissions.NOEMAILS);

                }
            }
            else
            {
                if (_customerToView.NoEmailFlag == "Y")
                {
                    if (strContactPermission.Length > 0)
                    {
                        strContactPermission.Append(",");
                    }
                    strContactPermission.Append(CustomerContactPermissions.NOEMAILS);

                }
            }

            if (_updatedCustomer != null && _customerToView.NoFaxFlag != _updatedCustomer.NoFaxFlag)
            {
                this.custContactPermission.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                if (_updatedCustomer.NoFaxFlag == "Y")
                {
                    if (strContactPermission.Length > 0)
                    {
                        strContactPermission.Append(",");
                    }
                    strContactPermission.Append(CustomerContactPermissions.NOFAXES);

                }
            }
            else
            {
                if (_customerToView.NoFaxFlag == "Y")
                {
                    if (strContactPermission.Length > 0)
                    {
                        strContactPermission.Append(",");
                    }
                    strContactPermission.Append(CustomerContactPermissions.NOFAXES);

                }
            }
            if (_updatedCustomer != null && _customerToView.NoMailFlag != _updatedCustomer.NoMailFlag)
            {
                this.custContactPermission.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                if (_updatedCustomer.NoMailFlag == "Y")
                {
                    if (strContactPermission.Length > 0)
                    {
                        strContactPermission.Append(",");
                    }
                    strContactPermission.Append(CustomerContactPermissions.NOMAILS);

                }
            }
            else
            {
                if (_customerToView.NoMailFlag == "Y")
                {
                    if (strContactPermission.Length > 0)
                    {
                        strContactPermission.Append(",");
                    }
                    strContactPermission.Append(CustomerContactPermissions.NOMAILS);

                }
            }
            if (_updatedCustomer != null && _customerToView.OptOutFlag != _updatedCustomer.OptOutFlag)
            {
                this.custContactPermission.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                if (_updatedCustomer.OptOutFlag == "Y")
                {
                    if (strContactPermission.Length > 0)
                    {
                        strContactPermission.Append(",");
                    }
                    strContactPermission.Append(CustomerContactPermissions.OPTOUT);

                }
            }
            else
            {
                if (_customerToView.OptOutFlag == "Y")
                {
                    if (strContactPermission.Length > 0)
                    {
                        strContactPermission.Append(",");
                    }
                    strContactPermission.Append(CustomerContactPermissions.OPTOUT);

                }
            }

            if (strContactPermission.Length == 0)
            {
                strContactPermission.Append(CustomerContactPermissions.NOPERMISSION);
            }

            this.custContactPermission.Text = strContactPermission.ToString();
            if (_updatedCustomer != null && _customerToView.HearAboutUs != _updatedCustomer.HearAboutUs)
            {
                this.custHeardAboutUsFrom.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custHeardAboutUsFrom.Text = Commons.GetHearAboutUsData(_updatedCustomer.HearAboutUs);
            }
            else
                this.custHeardAboutUsFrom.Text = Commons.GetHearAboutUsData(_customerToView.HearAboutUs);
            //Get primary email address

            CustomerEmailVO primaryEmailObj = _customerToView.getPrimaryEmail();
            CustomerEmailVO alternateEmailObj = _customerToView.getAlternateEmail();
            string custPrimaryEmail;
            string custAlternateEmail;
            CustomerEmailVO primaryUpdatedEmail = new CustomerEmailVO();
            CustomerEmailVO alternateUpdatedEmail = new CustomerEmailVO();
            if (_updatedCustomer != null)
            {
                primaryUpdatedEmail = _updatedCustomer.getPrimaryEmail();
                alternateUpdatedEmail = _updatedCustomer.getAlternateEmail();
            }


            //primary email already exists for the customer
            if (primaryEmailObj.EmailAddress != null)
            {
                //primary email address has been updated
                if (primaryUpdatedEmail.EmailAddress != null && primaryEmailObj.EmailAddress != primaryUpdatedEmail.EmailAddress)
                //primary email has been updated show the new one
                {
                    this.custEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    custPrimaryEmail = primaryUpdatedEmail.EmailAddress.ToString();
                }
                else
                {
                    //primary email exists for the customer and has been deleted
                    if (_loadUpdatedData && primaryUpdatedEmail.EmailAddress == null)
                        custPrimaryEmail = "";
                    else
                        //primary email exists for the customer and has not been updated
                        custPrimaryEmail = primaryEmailObj.EmailAddress;
                }
            }
            else
            {
                //primary email does not exist for the customer to begin with
                if (primaryUpdatedEmail.EmailAddress != null)
                {
                    //It has been added in the update contact details form
                    this.custEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    custPrimaryEmail = primaryUpdatedEmail.EmailAddress.ToString();
                }
                else
                {
                    //primary email has not been added in the update contact details form either
                    custPrimaryEmail = string.Empty;
                }
            }
            if (custPrimaryEmail != string.Empty)
                this.custEmail.Text = custPrimaryEmail;
            else
                this.custEmail.Text = "";

            //Alternate email already exists for the customer
            if (alternateEmailObj.EmailAddress != null)
            {
                //Alternate email has been updated
                if (alternateUpdatedEmail.EmailAddress != null && alternateEmailObj.EmailAddress != alternateUpdatedEmail.EmailAddress)
                {
                    this.custEmail2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    custAlternateEmail = alternateUpdatedEmail.EmailAddress;
                }
                else
                {
                    //alternate email has not been deleted
                    if (_loadUpdatedData && alternateUpdatedEmail.EmailAddress == null)
                        custAlternateEmail = "";
                    else
                        //alternate email has not been updated
                        custAlternateEmail = alternateEmailObj.EmailAddress;
                }
            }
            else
            {
                //Alternate email does not already exist for the customer
                if (alternateUpdatedEmail.EmailAddress != null)
                {
                    //alternate email has been added in the update contact details form
                    this.custEmail2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    custAlternateEmail = alternateUpdatedEmail.EmailAddress;
                }
                else
                {
                    //No alternate email for the customer and has not been added in the update
                    //contact details form either
                    custAlternateEmail = string.Empty;
                }
            }
            if (custAlternateEmail != string.Empty)
            {
                this.custEmail2.Text = custAlternateEmail;
                this.custEmail2.Visible = true;
            }
            else
                this.custEmail2.Visible = false;


            //Home  number already exists for the customer
            if (_custHomeNumber != null)
            {
                //And it has been updated
                if (custUpdatedHomeNumber != null && (custUpdatedHomeNumber.ContactAreaCode != _custHomeNumber.ContactAreaCode || _custHomeNumber.ContactPhoneNumber != custUpdatedHomeNumber.ContactPhoneNumber))
                {
                    this.custHomePhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custHomePhone.Text = "(" + custUpdatedHomeNumber.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(custUpdatedHomeNumber.ContactPhoneNumber);
                }
                else
                    //it has been deleted if _loadUpdates is true
                    if (_loadUpdatedData && custUpdatedHomeNumber == null)
                    {
                        this.custHomePhone.Text = "";
                    }
                    else
                        //it has not been updated
                        this.custHomePhone.Text = "(" + _custHomeNumber.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(_custHomeNumber.ContactPhoneNumber);
            }
            else
            {
                //Home number does not already exist for the customer..
                //check if it has been added in update contact details form
                if (custUpdatedHomeNumber != null)
                {
                    this.custHomePhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custHomePhone.Text = "(" + custUpdatedHomeNumber.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(custUpdatedHomeNumber.ContactPhoneNumber);
                }
                else
                    //No existing home phone number nor was it added in update contact details form
                    this.custHomePhone.Text = "";
            }

            //cell  number already exists for the customer
            if (_custCellNumber != null)
            {
                //And it has been updated
                if (custUpdatedCellNumber != null && (custUpdatedCellNumber.ContactAreaCode != _custCellNumber.ContactAreaCode || _custCellNumber.ContactPhoneNumber != custUpdatedCellNumber.ContactPhoneNumber))
                {
                    this.custMobilePhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custMobilePhone.Text = "(" + custUpdatedCellNumber.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(custUpdatedCellNumber.ContactPhoneNumber);
                }
                else
                    //it has been deleted
                    if (_loadUpdatedData && custUpdatedCellNumber == null)
                    {
                        this.custMobilePhone.Text = "";
                    }
                    else
                        //it has been updated
                        this.custMobilePhone.Text = "(" + _custCellNumber.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(_custCellNumber.ContactPhoneNumber);
            }
            else
            {
                //Cell number does not already exist for the customer..
                //check if it has been added in update contact details form
                if (custUpdatedCellNumber != null)
                {
                    this.custMobilePhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custMobilePhone.Text = "(" + custUpdatedCellNumber.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(custUpdatedCellNumber.ContactPhoneNumber);
                }
                else
                    //No existing cell phone number nor was it added in update contact details form
                    this.custMobilePhone.Text = "";
            }

            //Pager already exists for the customer
            if (_customerPager != null)
            {
                //And it has been updated
                if (custUpdatedPager != null && (custUpdatedPager.ContactAreaCode != _customerPager.ContactAreaCode || _customerPager.ContactPhoneNumber != custUpdatedPager.ContactPhoneNumber))
                {
                    this.custPager.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custPager.Text = string.Format("({0}) {1}", custUpdatedPager.ContactAreaCode, Commons.FormatPhoneNumberForUI(custUpdatedPager.ContactPhoneNumber));
                }
                else
                    //it has been deleted
                    if (_loadUpdatedData && custUpdatedPager == null)
                    {
                        this.custPager.Text = "";
                    }
                    else
                        //it has not been updated
                        this.custPager.Text = string.Format("({0}) {1}", _customerPager.ContactAreaCode, Commons.FormatPhoneNumberForUI(_customerPager.ContactPhoneNumber));
            }
            else
            {
                //pager number does not already exist for the customer..
                //check if it has been added in update contact details form
                if (custUpdatedPager != null)
                {
                    this.custPager.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custPager.Text = string.Format("({0}) {1}", custUpdatedPager.ContactAreaCode, Commons.FormatPhoneNumberForUI(custUpdatedPager.ContactPhoneNumber));
                }
                else
                    //No existing pager number nor was it added in update contact details form
                    this.custPager.Text = "";
            }

            //Work  number already exists for the customer
            if (_custWorkNumber != null)
            {
                //And it has been updated
                if (custUpdatedWorkNumber != null && (custUpdatedWorkNumber.ContactAreaCode != _custWorkNumber.ContactAreaCode || _custWorkNumber.ContactPhoneNumber != custUpdatedWorkNumber.ContactPhoneNumber))
                {
                    this.custWorkPhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custWorkPhone.Text = string.Format("({0}) {1}", custUpdatedWorkNumber.ContactAreaCode, Commons.FormatPhoneNumberForUI(custUpdatedWorkNumber.ContactPhoneNumber));
                }
                else
                    if (_loadUpdatedData && custUpdatedWorkNumber == null)
                    {
                        //it has been deleted
                        this.custWorkPhone.Text = "";
                    }
                    else
                        //it has not been updated
                        this.custWorkPhone.Text = string.Format("({0}) {1}", _custWorkNumber.ContactAreaCode, Commons.FormatPhoneNumberForUI(_custWorkNumber.ContactPhoneNumber));
            }
            else
            {
                //Work number does not already exist for the customer..
                //check if it has been added in update contact details form
                if (custUpdatedWorkNumber != null)
                {
                    this.custWorkPhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custWorkPhone.Text = "(" + custUpdatedWorkNumber.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(custUpdatedWorkNumber.ContactPhoneNumber);
                }
                else
                    //No existing work phone number nor was it added in update contact details form
                    this.custWorkPhone.Text = "";
            }

            //Fax already exists for the customer
            if (_customerFax != null)
            {
                //And it has been updated
                if (custUpdatedFax != null && (custUpdatedFax.ContactAreaCode != _customerFax.ContactAreaCode || _customerFax.ContactPhoneNumber != custUpdatedFax.ContactPhoneNumber))
                {
                    this.custFax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custFax.Text = "(" + custUpdatedFax.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(custUpdatedFax.ContactPhoneNumber);
                }
                else
                    //it has been deleted
                    if (_loadUpdatedData && custUpdatedFax == null)
                    {
                        this.custFax.Text = "";
                    }
                    else
                        //it has not been updated
                        this.custFax.Text = "(" + _customerFax.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(_customerFax.ContactPhoneNumber);
            }
            else
            {
                //fax number does not already exist for the customer..
                //check if it has been added in update contact details form
                if (custUpdatedFax != null)
                {
                    this.custFax.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custFax.Text = "(" + custUpdatedFax.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(custUpdatedFax.ContactPhoneNumber);
                }
                else
                    //No existing fax number nor was it added in update contact details form
                    this.custFax.Text = "";
            }
            //Primary number already exists for the customer
            if (_custPrimaryNumber != null)
            {
                //And it has been updated
                if (custUpdatedPrimaryNumber != null && (custUpdatedPrimaryNumber.ContactAreaCode != _custPrimaryNumber.ContactAreaCode || _custPrimaryNumber.ContactPhoneNumber != custUpdatedPrimaryNumber.ContactPhoneNumber))
                {
                    this.custPrimaryPhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custPrimaryPhone.Text = "(" + custUpdatedPrimaryNumber.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(custUpdatedPrimaryNumber.ContactPhoneNumber);
                }
                else
                    //it has been deleted
                    if (_loadUpdatedData && custUpdatedPrimaryNumber == null)
                    {
                        this.custPrimaryPhone.Text = string.Empty;
                    }
                    else
                        //it has not been updated
                        this.custPrimaryPhone.Text = string.Format("({0}) {1}", _custPrimaryNumber.ContactAreaCode, Commons.FormatPhoneNumberForUI(_custPrimaryNumber.ContactPhoneNumber));
            }
            else
            {
                //Primary number does not already exist for the customer..
                //check if it has been added in update contact details form
                if (custUpdatedPrimaryNumber != null)
                {
                    this.custPrimaryPhone.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custPrimaryPhone.Text = "(" + custUpdatedPrimaryNumber.ContactAreaCode + ") " + Commons.FormatPhoneNumberForUI(custUpdatedPrimaryNumber.ContactPhoneNumber);
                }
                else
                    //No existing primary phone number nor was it added in update contact details form
                    this.custPrimaryPhone.Text = "";
            }


            if (_updatedCustomer != null && _updatedCustomer.PreferredCallTime != _customerToView.PreferredCallTime)
            {
                this.custPreferredCallTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custPreferredCallTime.Text = Commons.GetPrefCallTime(_updatedCustomer.PreferredCallTime);
            }
            else
                this.custPreferredCallTime.Text = Commons.GetPrefCallTime(_customerToView.PreferredCallTime);

            if (_updatedCustomer != null && _updatedCustomer.PreferredContactMethod != _customerToView.PreferredContactMethod)
            {
                this.custPreferredContact.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custPreferredContact.Text = Commons.GetPreferredContactData(_updatedCustomer.PreferredContactMethod);
            }
            else
                this.custPreferredContact.Text = Commons.GetPreferredContactData(_customerToView.PreferredContactMethod);

            if (_updatedCustomer != null && _updatedCustomer.ReceivePromotionOffers != _customerToView.ReceivePromotionOffers)
            {
                this.custReceivePromotions.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custReceivePromotions.Text = Commons.GetCodeDesc(_updatedCustomer.ReceivePromotionOffers);
            }
            else
                this.custReceivePromotions.Text = Commons.GetCodeDesc(_customerToView.ReceivePromotionOffers);
            if (_updatedCustomer != null && _updatedCustomer.ReminderContact != _customerToView.ReminderContact)
            {
                this.custReminderContact.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custReminderContact.Text = Commons.GetCodeDesc(_updatedCustomer.ReminderContact);
            }
            else
                this.custReminderContact.Text = Commons.GetCodeDesc(_customerToView.ReminderContact);
            _custWorkNumber = null;
            _custCellNumber = null;
            _custHomeNumber = null;
            _custPrimaryNumber = null;

            //If the updated customer is not null some info for the customer changed and hence
            //_customerToView object should be updated to reflect the new information in case the user
            //clicks on Edit link since at that time the _customerToView object is used to populate the form
            if (_updatedCustomer != null)
            {
                _customerToView.NoCallFlag = _updatedCustomer.NoCallFlag;
                _customerToView.NoMailFlag = _updatedCustomer.NoMailFlag;
                _customerToView.NoFaxFlag = _updatedCustomer.NoFaxFlag;
                _customerToView.NoEmailFlag = _updatedCustomer.NoEmailFlag;
                _customerToView.PreferredCallTime = _updatedCustomer.PreferredCallTime;
                _customerToView.PreferredContactMethod = _updatedCustomer.PreferredContactMethod;
                _customerToView.ReceivePromotionOffers = _updatedCustomer.ReceivePromotionOffers;
                _customerToView.ReminderContact = _updatedCustomer.ReminderContact;
                _customerToView.OptOutFlag = _updatedCustomer.OptOutFlag;
                _customerToView.HearAboutUs = _updatedCustomer.HearAboutUs;
                //update phone numbers
                _customerToView.removePhoneNumbers();
                if (custUpdatedHomeNumber != null)
                    _customerToView.updatePhoneNumber(
                        CustomerPhoneTypes.HOME_NUMBER, custUpdatedHomeNumber.ContactAreaCode,
                        custUpdatedHomeNumber.ContactPhoneNumber, custUpdatedHomeNumber.ContactExtension,
                        custUpdatedHomeNumber.CountryDialNumCode, CustomerPhoneTypes.VOICE_NUMBER, custUpdatedHomeNumber.TeleusrDefText);
                if (custUpdatedWorkNumber != null)
                    _customerToView.updatePhoneNumber(
                        CustomerPhoneTypes.WORK_NUMBER, custUpdatedWorkNumber.ContactAreaCode,
                        custUpdatedWorkNumber.ContactPhoneNumber, custUpdatedWorkNumber.ContactExtension,
                        custUpdatedWorkNumber.CountryDialNumCode, CustomerPhoneTypes.VOICE_NUMBER, custUpdatedWorkNumber.TeleusrDefText);
                if (custUpdatedCellNumber != null)
                    _customerToView.updatePhoneNumber(
                        CustomerPhoneTypes.HOME_NUMBER, custUpdatedCellNumber.ContactAreaCode,
                        custUpdatedCellNumber.ContactPhoneNumber, custUpdatedCellNumber.ContactExtension,
                        custUpdatedCellNumber.CountryDialNumCode, CustomerPhoneTypes.MOBILE_NUMBER, custUpdatedCellNumber.TeleusrDefText);
                if (custUpdatedPager != null)
                    _customerToView.updatePhoneNumber(
                        CustomerPhoneTypes.HOME_NUMBER, custUpdatedPager.ContactAreaCode,
                        custUpdatedPager.ContactPhoneNumber, custUpdatedPager.ContactExtension,
                        custUpdatedPager.CountryDialNumCode, CustomerPhoneTypes.PAGER_NUMBER, custUpdatedPager.TeleusrDefText);
                if (custUpdatedFax != null)
                    _customerToView.updatePhoneNumber(
                        CustomerPhoneTypes.HOME_NUMBER, custUpdatedFax.ContactAreaCode,
                        custUpdatedFax.ContactPhoneNumber, custUpdatedFax.ContactExtension,
                        custUpdatedFax.CountryDialNumCode, CustomerPhoneTypes.FAX_NUMBER, custUpdatedFax.TeleusrDefText);
                //update emails

                if (primaryUpdatedEmail.EmailAddress != null && alternateUpdatedEmail.EmailAddress != null)
                {
                    _customerToView.removeEmails();
                    _customerToView.addEmail(primaryUpdatedEmail);
                    _customerToView.addEmail(alternateUpdatedEmail);
                }
                else if (primaryUpdatedEmail.EmailAddress != null)
                {
                    _customerToView.removeEmails();
                    _customerToView.addEmail(primaryUpdatedEmail);
                }
                else if (alternateUpdatedEmail.EmailAddress != null)
                {
                    _customerToView.removeEmails();
                    _customerToView.addEmail(alternateUpdatedEmail);
                }


            }

        }

        private void LoadPhysicalDescData()
        {
            //Set Race data
            string custRace;

            if (_updatedCustomer != null && _customerToView.Race != _updatedCustomer.Race)
            {
                this.labelRaceData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                custRace = _updatedCustomer.Race;
            }
            else
                custRace = _customerToView.Race;
            this.labelRaceData.Text = string.Empty;
            if (GlobalDataAccessor.Instance.DesktopSession.RaceTable != null)
            {
                foreach (DataRow dr in GlobalDataAccessor.Instance.DesktopSession.RaceTable.Rows)
                {
                    if (dr["code"].ToString() == custRace)
                        this.labelRaceData.Text = dr["codedesc"].ToString();
                }
            }


            //Set Gender data
            string gender;
            if (_updatedCustomer != null && _customerToView.Gender != _updatedCustomer.Gender)
            {
                this.labelGenderData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                gender = _updatedCustomer.Gender;
            }
            else
                gender = _customerToView.Gender;

            this.labelGenderData.Text = Commons.GetGenderData(gender);

            //Set Haircolor data
            this.labelHairColorData.Text = string.Empty;
            string haircolor;
            if (_updatedCustomer != null && _customerToView.HairColor != _updatedCustomer.HairColor)
            {
                this.labelHairColorData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                haircolor = _updatedCustomer.HairColor;
            }
            else
                haircolor = _customerToView.HairColor;

            if (GlobalDataAccessor.Instance.DesktopSession.HairColorTable != null)
            {
                foreach (DataRow dr in GlobalDataAccessor.Instance.DesktopSession.HairColorTable.Rows)
                {
                    if (dr["hair_color_code"].ToString() == haircolor)
                        this.labelHairColorData.Text = dr["codedesc"].ToString();
                }
            }


            //Set eye color data
            string eyecolor;
            this.labelEyeColorData.Text = string.Empty;
            if (_updatedCustomer != null && _customerToView.EyeColor != _updatedCustomer.EyeColor)
            {
                this.labelEyeColorData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                eyecolor = _updatedCustomer.EyeColor;
            }
            else
                eyecolor = _customerToView.EyeColor;
            if (GlobalDataAccessor.Instance.DesktopSession.EyeColorTable != null)
            {
                foreach (DataRow dr in GlobalDataAccessor.Instance.DesktopSession.EyeColorTable.Rows)
                {
                    if (dr["eye_color_code"].ToString() == eyecolor)
                        this.labelEyeColorData.Text = dr["codedesc"].ToString();
                }

            }

            if (_updatedCustomer != null && _customerToView.Height != _updatedCustomer.Height)
            {
                this.labelHeightData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.labelHeightData.Text = _updatedCustomer.Height;
            }
            else
                this.labelHeightData.Text = _customerToView.Height;

            int weight;
            if (_updatedCustomer != null && _customerToView.Weight != _updatedCustomer.Weight)
            {
                this.labelWeightData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                weight = _updatedCustomer.Weight;
            }
            else
                weight = _customerToView.Weight;

            if (weight != 0)
                this.labelWeightData.Text = weight.ToString();
            else
                this.labelWeightData.Text = string.Empty;

            CustomerNotesVO custUpdatedPhysicalNote = new CustomerNotesVO();
            CustomerNotesVO custPhysicalNote = _customerToView.getPhysicalDescNote();
            string note;
            if (_updatedCustomer != null)
            {
                custUpdatedPhysicalNote = _updatedCustomer.getPhysicalDescNote();
                if (custUpdatedPhysicalNote.ContactNote != null && custUpdatedPhysicalNote.ContactNote != custPhysicalNote.ContactNote)
                {
                    this.labelOthersData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    note = custUpdatedPhysicalNote.ContactNote;
                }
                else
                    note = custPhysicalNote.ContactNote;
            }
            else
                note = custPhysicalNote.ContactNote;

            if (note != null)
                this.labelOthersData.Text = note;
            else
                this.labelOthersData.Text = string.Empty;

            //If the updated customer is not null some info for the customer changed and hence
            //_customerToView object should be updated to reflect the new information in case the user
            //clicks on Edit link since at that time the _customerToView object is used to populate the form
            if (_updatedCustomer != null)
            {
                _customerToView.Race = _updatedCustomer.Race;
                _customerToView.Gender = _updatedCustomer.Gender;
                _customerToView.HairColor = _updatedCustomer.HairColor;
                _customerToView.EyeColor = _updatedCustomer.EyeColor;
                _customerToView.Height = _updatedCustomer.Height;
                _customerToView.Weight = _updatedCustomer.Weight;
                if (custUpdatedPhysicalNote.ContactNote != null)
                {
                    _customerToView.updatePhysicalDescNote(
                        custUpdatedPhysicalNote.ContactNote,
                        custUpdatedPhysicalNote.StoreNumber,
                        custUpdatedPhysicalNote.CreationDate,
                        custUpdatedPhysicalNote.CreatedBy,
                        custUpdatedPhysicalNote.CustomerProductNoteId);
                }
            }

        }

        private void LoadCustPersonalInfo()
        {
            //If the values in the _updatedcustomer object is not the same as the _customertoview customer object
            //the updated value needs to be shown in bold.
            if (_updatedCustomer != null && _customerToView.DateOfBirth.ToShortDateString() != _updatedCustomer.DateOfBirth.ToShortDateString())
            {
                this.custDOB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custDOB.Text = _updatedCustomer.DateOfBirth == DateTime.MaxValue ? "" : (_updatedCustomer.DateOfBirth).FormatDate();
            }
            else
            {
                this.custDOB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custDOB.Text = _customerToView.DateOfBirth == DateTime.MaxValue ? "" : (_customerToView.DateOfBirth).FormatDate();
            }

            if (_updatedCustomer != null && (_customerToView.FirstName != _updatedCustomer.FirstName || _customerToView.MiddleInitial != _updatedCustomer.MiddleInitial || _customerToView.LastName != _updatedCustomer.LastName))
            {
                this.custName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custName.Text = _updatedCustomer.FirstName + " " + _updatedCustomer.MiddleInitial + " " + _updatedCustomer.LastName;
            }
            else
            {
                this.custName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custName.Text = _customerToView.FirstName + " " + _customerToView.MiddleInitial + " " + _customerToView.LastName;
            }

            if (_updatedCustomer != null && _customerToView.SocialSecurityNumber != _updatedCustomer.SocialSecurityNumber)
            {
                this.custSSN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custSSN.Text = Commons.FormatSSN(_updatedCustomer.SocialSecurityNumber);
            }
            else
            {
                this.custSSN.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.custSSN.Text = Commons.FormatSSN(_customerToView.SocialSecurityNumber);
            }

            //The following 2 values cannot be updated
            this.custNumber.Text = _customerToView.CustomerNumber;
            this.custSince.Text = (_customerToView.CustomerSince).FormatDate();
            //If the updated customer object is not null it means that some info
            //was updated so _customerToView object should be updated to reflect these new values
            //in case the user clicks on the edit link again since at that time the info to edit
            //is populated from the _customerToView object and hence must reflect the latest info
            if (_updatedCustomer != null)
            {
                _customerToView.DateOfBirth = _updatedCustomer.DateOfBirth;
                _customerToView.SocialSecurityNumber = _updatedCustomer.SocialSecurityNumber;
                _customerToView.FirstName = _updatedCustomer.FirstName;
                _customerToView.MiddleInitial = _updatedCustomer.MiddleInitial;
                _customerToView.LastName = _updatedCustomer.LastName;
                _customerToView.DateOfBirth = _updatedCustomer.DateOfBirth;
            }


        }

        private void LoadWorkAddr()
        {
            //When the customer data is loaded when no updates are done
            //either when the form is loaded the first time or after coming back from the
            //update address form with no updates done
            if (_updatedCustomer == null)
            {
                _custWorkAddress = _customerToView.getWorkAddress();
                if (_custWorkAddress != null)
                {
                    if (_custWorkAddress.Address2 != string.Empty)
                        this.custWorkAddr.Text = _custWorkAddress.Address1 + "," + _custWorkAddress.Address2;
                    else
                        this.custWorkAddr.Text = _custWorkAddress.Address1;
                    if (_custWorkAddress.UnitNum != string.Empty)
                        this.custWorkAddr.Text = this.custWorkAddr.Text + "," + _custWorkAddress.UnitNum;
                    this.custWorkAddr2.Text = _custWorkAddress.City + "," + _custWorkAddress.State_Code + "," + _custWorkAddress.ZipCode;
                    this.custWorkAddrCountry.Text = _custWorkAddress.Country_Name;
                    this.custWorkAddrNotes.Text = _custWorkAddress.Notes;
                }
            }
            else
            {
                //Some updates on the work address was done
                _custWorkAddress = _customerToView.getWorkAddress();
                AddressVO custUpdatedWorkAddress = _updatedCustomer.getWorkAddress();
                if (_custWorkAddress != null)
                {
                    if (_custWorkAddress.Address1 != custUpdatedWorkAddress.Address1 || _custWorkAddress.Address2 != custUpdatedWorkAddress.Address2)
                    {
                        this.custWorkAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        if (custUpdatedWorkAddress.Address2 != string.Empty)
                            this.custWorkAddr.Text = custUpdatedWorkAddress.Address1 + "," + custUpdatedWorkAddress.Address2;
                        else
                            this.custWorkAddr.Text = custUpdatedWorkAddress.Address1;
                    }
                    else
                    {
                        if (_custWorkAddress.Address2 != string.Empty)
                            this.custWorkAddr.Text = _custWorkAddress.Address1 + "," + _custWorkAddress.Address2;
                        else
                            this.custWorkAddr.Text = _custWorkAddress.Address1;
                    }
                    if (_custWorkAddress.UnitNum != custUpdatedWorkAddress.UnitNum)
                    {
                        this.custWorkAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custWorkAddr.Text = this.custWorkAddr.Text + "," + custUpdatedWorkAddress.UnitNum;
                    }
                    else
                        if (custUpdatedWorkAddress.UnitNum != string.Empty)
                            this.custWorkAddr.Text = this.custWorkAddr.Text + "," + custUpdatedWorkAddress.UnitNum;

                    if (_custWorkAddress.City != custUpdatedWorkAddress.City || _custWorkAddress.State_Code != custUpdatedWorkAddress.State_Code || _custWorkAddress.ZipCode != custUpdatedWorkAddress.ZipCode)
                    {
                        this.custWorkAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custWorkAddr2.Text = custUpdatedWorkAddress.City + "," + custUpdatedWorkAddress.State_Code + " " + custUpdatedWorkAddress.ZipCode;
                    }
                    else
                        this.custWorkAddr2.Text = _custWorkAddress.City + "," + _custWorkAddress.State_Code + " " + _custWorkAddress.ZipCode;
                    if (_custWorkAddress.Country_Name != custUpdatedWorkAddress.Country_Name)
                    {
                        this.custWorkAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custWorkAddrCountry.Text = custUpdatedWorkAddress.Country_Name;
                    }
                    else
                        this.custWorkAddrCountry.Text = _custWorkAddress.Country_Name;
                    if (_custWorkAddress.Notes != custUpdatedWorkAddress.Notes)
                    {
                        this.custWorkAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custWorkAddrNotes.Text = custUpdatedWorkAddress.Notes;
                    }
                    else
                        this.custWorkAddrNotes.Text = _custWorkAddress.Notes;


                }
                else
                {
                    //show everything in bold since if you are here that means the customer did not have
                    //mailing address before and now he does
                    this.custWorkAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    if (custUpdatedWorkAddress.Address2 != string.Empty)
                        this.custWorkAddr.Text = custUpdatedWorkAddress.Address1 + "," + custUpdatedWorkAddress.Address2;
                    else
                        this.custWorkAddr.Text = custUpdatedWorkAddress.Address1;
                    if (custUpdatedWorkAddress.UnitNum != string.Empty)
                        this.custWorkAddr.Text = this.custWorkAddr.Text + "," + custUpdatedWorkAddress.UnitNum;

                    this.custWorkAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custWorkAddr2.Text = custUpdatedWorkAddress.City + "," + custUpdatedWorkAddress.State_Code + " " + custUpdatedWorkAddress.ZipCode;
                    this.custWorkAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custWorkAddrCountry.Text = custUpdatedWorkAddress.Country_Name;
                    this.custWorkAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custWorkAddrNotes.Text = custUpdatedWorkAddress.Notes;

                }
                _customerToView.updateWorkAddress(
                    custUpdatedWorkAddress.Address1, custUpdatedWorkAddress.Address2,
                    custUpdatedWorkAddress.UnitNum, custUpdatedWorkAddress.City,
                    custUpdatedWorkAddress.ZipCode, custUpdatedWorkAddress.Country_Name,
                    custUpdatedWorkAddress.State_Code, custUpdatedWorkAddress.Notes);

            }

        }

        private void LoadMailingAddr()
        {
            //When the customer data is loaded when no updates are done
            //either when the form is loaded the first time or after coming back from the
            //update address form with no updates done
            if (_updatedCustomer == null)
            {
                _custMailingAddress = _customerToView.getMailingAddress();
                if (_custMailingAddress != null)
                {
                    if (_custMailingAddress.Address2 != string.Empty)
                        this.custMailingAddr.Text = _custMailingAddress.Address1 + "," + _custMailingAddress.Address2;
                    else
                        this.custMailingAddr.Text = _custMailingAddress.Address1;
                    if (_custMailingAddress.UnitNum != string.Empty)
                        this.custMailingAddr.Text = this.custMailingAddr.Text + "," + _custMailingAddress.UnitNum;

                    this.custMailingAddr2.Text = _custMailingAddress.City + "," + _custMailingAddress.State_Code + " " + _custMailingAddress.ZipCode;
                    this.custMailingAddrCountry.Text = _custMailingAddress.Country_Name;
                    this.custMailingAddrNotes.Text = _custMailingAddress.Notes;
                }
            }
            else
            {
                //Some updates on the mailing address was done
                _custMailingAddress = _customerToView.getMailingAddress();
                AddressVO custUpdatedMailingAddress = _updatedCustomer.getMailingAddress();
                if (_custMailingAddress != null)
                {
                    if (_custMailingAddress.Address1 != custUpdatedMailingAddress.Address1 || _custMailingAddress.Address2 != custUpdatedMailingAddress.Address2)
                    {
                        this.custMailingAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        if (custUpdatedMailingAddress.Address2 != string.Empty)
                            this.custMailingAddr.Text = custUpdatedMailingAddress.Address1 + "," + custUpdatedMailingAddress.Address2;
                        else
                            this.custMailingAddr.Text = custUpdatedMailingAddress.Address1;
                    }
                    else
                    {
                        if (_custMailingAddress.Address2 != string.Empty)
                            this.custMailingAddr.Text = _custMailingAddress.Address1 + "," + _custMailingAddress.Address2;
                        else
                            this.custMailingAddr.Text = _custMailingAddress.Address1;
                    }
                    if (_custMailingAddress.UnitNum != custUpdatedMailingAddress.UnitNum)
                    {
                        this.custMailingAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custMailingAddr.Text = this.custMailingAddr.Text + "," + custUpdatedMailingAddress.UnitNum;
                    }
                    else
                        if (custUpdatedMailingAddress.UnitNum != string.Empty)
                            this.custMailingAddr.Text = this.custMailingAddr.Text + "," + custUpdatedMailingAddress.UnitNum;

                    if (_custMailingAddress.City != custUpdatedMailingAddress.City || _custMailingAddress.State_Code != custUpdatedMailingAddress.State_Code || _custMailingAddress.ZipCode != custUpdatedMailingAddress.ZipCode)
                    {
                        this.custMailingAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custMailingAddr2.Text = custUpdatedMailingAddress.City + "," + custUpdatedMailingAddress.State_Code + " " + custUpdatedMailingAddress.ZipCode;
                    }
                    else
                        this.custMailingAddr2.Text = _custMailingAddress.City + "," + _custMailingAddress.State_Code + " " + _custMailingAddress.ZipCode;
                    if (_custMailingAddress.Country_Name != custUpdatedMailingAddress.Country_Name)
                    {
                        this.custMailingAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custMailingAddrCountry.Text = custUpdatedMailingAddress.Country_Name;
                    }
                    else
                        this.custMailingAddrCountry.Text = _custMailingAddress.Country_Name;
                    if (_custMailingAddress.Notes != custUpdatedMailingAddress.Notes)
                    {
                        this.custMailingAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custMailingAddrNotes.Text = custUpdatedMailingAddress.Notes;
                    }
                    else
                        this.custMailingAddrNotes.Text = _custMailingAddress.Notes;


                }
                else
                {
                    //show everything in bold since if you are here that means the customer did not have
                    //mailing address before and now he does
                    this.custMailingAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    if (custUpdatedMailingAddress.Address2 != string.Empty)
                        this.custMailingAddr.Text = custUpdatedMailingAddress.Address1 + "," + custUpdatedMailingAddress.Address2;
                    else
                        this.custMailingAddr.Text = custUpdatedMailingAddress.Address1;
                    if (custUpdatedMailingAddress.UnitNum != string.Empty)
                        this.custMailingAddr.Text = this.custMailingAddr.Text + "," + custUpdatedMailingAddress.UnitNum;
                    this.custMailingAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custMailingAddr2.Text = custUpdatedMailingAddress.City + "," + custUpdatedMailingAddress.State_Code + " " + custUpdatedMailingAddress.ZipCode;
                    this.custMailingAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custMailingAddrCountry.Text = custUpdatedMailingAddress.Country_Name;
                    this.custMailingAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custMailingAddrNotes.Text = custUpdatedMailingAddress.Notes;

                }
                _customerToView.updateMailingAddress(
                    custUpdatedMailingAddress.Address1, custUpdatedMailingAddress.Address2,
                    custUpdatedMailingAddress.UnitNum, custUpdatedMailingAddress.City,
                    custUpdatedMailingAddress.ZipCode, custUpdatedMailingAddress.Country_Name,
                    custUpdatedMailingAddress.State_Code, custUpdatedMailingAddress.Notes);

            }

        }

        private void LoadPhysicalAddr()
        {
            //When the customer data is loaded without any updates being done
            if (_updatedCustomer == null)
            {
                _custPhysicalAddress = _customerToView.getHomeAddress();
                if (_custPhysicalAddress != null)
                {
                    if (_custPhysicalAddress.Address2 != string.Empty)
                        this.custPhysicalAddr.Text = _custPhysicalAddress.Address1 + "," + _custPhysicalAddress.Address2;
                    else
                        this.custPhysicalAddr.Text = _custPhysicalAddress.Address1;
                    if (_custPhysicalAddress.UnitNum != string.Empty)
                        this.custPhysicalAddr.Text = this.custPhysicalAddr.Text + "," + _custPhysicalAddress.UnitNum;

                    this.custPhysicalAddr2.Text = _custPhysicalAddress.City + "," + _custPhysicalAddress.State_Code + " " + _custPhysicalAddress.ZipCode;
                    this.custPhysicalAddrCountry.Text = _custPhysicalAddress.Country_Name;
                    this.custPhysicalAddrNotes.Text = _custPhysicalAddress.Notes;
                }
            }
            else
            {
                //Some updates on the physical address was done
                _custPhysicalAddress = _customerToView.getHomeAddress();
                AddressVO custUpdatedPhysicalAddress = _updatedCustomer.getHomeAddress();
                if (_custPhysicalAddress != null)
                {

                    if (_custPhysicalAddress.Address1 != custUpdatedPhysicalAddress.Address1 || _custPhysicalAddress.Address2 != custUpdatedPhysicalAddress.Address2)
                    {
                        this.custPhysicalAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        if (custUpdatedPhysicalAddress.Address2 != string.Empty)
                            this.custPhysicalAddr.Text = custUpdatedPhysicalAddress.Address1 + "," + custUpdatedPhysicalAddress.Address2;
                        else
                            this.custPhysicalAddr.Text = custUpdatedPhysicalAddress.Address1;
                    }
                    else
                    {
                        if (_custPhysicalAddress.Address2 != string.Empty)
                            this.custPhysicalAddr.Text = _custPhysicalAddress.Address1 + "," + _custPhysicalAddress.Address2;
                        else
                            this.custPhysicalAddr.Text = _custPhysicalAddress.Address1;
                    }
                    if (_custPhysicalAddress.UnitNum != custUpdatedPhysicalAddress.UnitNum)
                    {
                        this.custPhysicalAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custPhysicalAddr.Text = this.custPhysicalAddr.Text + "," + custUpdatedPhysicalAddress.UnitNum;
                    }
                    else
                        if (custUpdatedPhysicalAddress.UnitNum != string.Empty)
                            this.custPhysicalAddr.Text = this.custPhysicalAddr.Text + "," + custUpdatedPhysicalAddress.UnitNum;

                    if (_custPhysicalAddress.City != custUpdatedPhysicalAddress.City || _custPhysicalAddress.State_Code != custUpdatedPhysicalAddress.State_Code || _custPhysicalAddress.ZipCode != custUpdatedPhysicalAddress.ZipCode)
                    {
                        this.custPhysicalAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custPhysicalAddr2.Text = custUpdatedPhysicalAddress.City + "," + custUpdatedPhysicalAddress.State_Code + " " + custUpdatedPhysicalAddress.ZipCode;
                    }
                    else
                        this.custPhysicalAddr2.Text = _custPhysicalAddress.City + "," + _custPhysicalAddress.State_Code + " " + _custPhysicalAddress.ZipCode;
                    if (_custPhysicalAddress.Country_Name != custUpdatedPhysicalAddress.Country_Name)
                    {
                        this.custPhysicalAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custPhysicalAddrCountry.Text = custUpdatedPhysicalAddress.Country_Name;
                    }
                    else
                        this.custPhysicalAddrCountry.Text = _custPhysicalAddress.Country_Name;
                    if (_custPhysicalAddress.Notes != custUpdatedPhysicalAddress.Notes)
                    {
                        this.custPhysicalAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.custPhysicalAddrNotes.Text = custUpdatedPhysicalAddress.Notes;
                    }
                    else
                        this.custPhysicalAddrNotes.Text = _custPhysicalAddress.Notes;


                }
                else
                {
                    //show everything in bold since if you are here that means the customer did not have
                    //physical address before and now he does
                    this.custPhysicalAddr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    if (custUpdatedPhysicalAddress.Address2 != string.Empty)
                        this.custPhysicalAddr.Text = custUpdatedPhysicalAddress.Address1 + "," + custUpdatedPhysicalAddress.Address2;
                    else
                        this.custPhysicalAddr.Text = custUpdatedPhysicalAddress.Address1;
                    if (custUpdatedPhysicalAddress.UnitNum != string.Empty)
                        this.custPhysicalAddr.Text = this.custPhysicalAddr.Text + "," + custUpdatedPhysicalAddress.UnitNum;

                    this.custPhysicalAddr2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custPhysicalAddr2.Text = custUpdatedPhysicalAddress.City + "," + custUpdatedPhysicalAddress.State_Code + " " + custUpdatedPhysicalAddress.ZipCode;
                    this.custPhysicalAddrCountry.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custPhysicalAddrCountry.Text = custUpdatedPhysicalAddress.Country_Name;
                    this.custPhysicalAddrNotes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.custPhysicalAddrNotes.Text = custUpdatedPhysicalAddress.Notes;

                }
                _customerToView.updateHomeAddress(
                    custUpdatedPhysicalAddress.Address1, custUpdatedPhysicalAddress.Address2,
                    custUpdatedPhysicalAddress.UnitNum, custUpdatedPhysicalAddress.City,
                    custUpdatedPhysicalAddress.ZipCode, custUpdatedPhysicalAddress.Country_Name,
                    custUpdatedPhysicalAddress.State_Code, custUpdatedPhysicalAddress.Notes);


            }

        }

        private void LoadLatestComment()
        {
            CustomerNotesVO custlatestnote;
            if (_loadUpdatedData && _updatedCustomer != null)
                custlatestnote = _updatedCustomer.getLatestNote();
            else
                custlatestnote = _customerToView.getLatestNote();

            if (custlatestnote.ContactNote != null)
            {
                Label labelShopHead = new Label();
                Label labelDateHead = new Label();
                Label labelCommentHead = new Label();
                Label labelEnteredByHead = new Label();
                labelShopHead.Text = "Shop";
                labelDateHead.Text = "Date / Time";
                labelCommentHead.Text = "Comments/Notes";
                labelEnteredByHead.Text = "User Id";
                labelShopHead.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelDateHead.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelCommentHead.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                labelEnteredByHead.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.tableLayoutPanel9.Controls.Add(labelShopHead, 0, 0);
                this.tableLayoutPanel9.Controls.Add(labelDateHead, 1, 0);
                this.tableLayoutPanel9.Controls.Add(labelCommentHead, 2, 0);
                this.tableLayoutPanel9.Controls.Add(labelEnteredByHead, 3, 0);


                Label labelShop = new Label();
                Label labelDate = new Label();
                Label labelComment = new Label();
                Label labelEnteredBy = new Label();
                labelShop.Name = "labelCommentShop";
                labelDate.Name = "labelCommentDate";
                labelComment.Name = "labelCommentValue";
                labelEnteredBy.Name = "labelCommentUser";
                labelShop.AutoSize = true;
                labelDate.AutoSize = true;
                labelComment.AutoSize = true;
                labelEnteredBy.AutoSize = true;
                if (_loadUpdatedData && _updatedCustomer != null)
                {
                    labelShop.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    labelDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    labelComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    labelEnteredBy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                else
                {
                    labelShop.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    labelDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    labelComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    labelEnteredBy.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                }

                labelShop.Text = custlatestnote.StoreNumber;
                labelDate.Text = custlatestnote.UpdatedDate.ToString();
                labelComment.Text = custlatestnote.ContactNote;
                labelEnteredBy.Text = custlatestnote.CreatedBy;

                this.tableLayoutPanel9.Controls.Add(labelShop, 0, 1);
                this.tableLayoutPanel9.Controls.Add(labelDate, 1, 1);
                this.tableLayoutPanel9.Controls.Add(labelComment, 2, 1);
                this.tableLayoutPanel9.Controls.Add(labelEnteredBy, 3, 1);
                this.linkLabelCommentsView.Visible = true;

            }
            else
                this.linkLabelCommentsView.Visible = false;

            //update the customer object _customerToView with the values
            //from the updatedcustomer object which would have values 
            //set in the updatecommentsandnotes form
            if (_loadUpdatedData && _updatedCustomer != null)
            {
                _customerToView.removeNotes();
                foreach (CustomerNotesVO note in _updatedCustomer.getAllCommentNotes())
                {
                    _customerToView.addNotes(note);
                }
            }


        }

        private void LoadIdentityData()
        {
            Label labelIdentName;
            Label labelIdentValue;
            List<IdentificationVO> custIdentities;
            List<IdentificationVO> custUpdatedIdentities;
            custIdentities = _customerToView.getAllIdentifications();
            if (_loadUpdatedData && _updatedCustomer != null)
                custUpdatedIdentities = _updatedCustomer.getAllIdentifications();
            else
                custUpdatedIdentities = null;

            if (custUpdatedIdentities != null)
            {
                for (int i = 0; i < custUpdatedIdentities.Count; i++)
                {
                    IdentificationVO customerId =
                        _customerToView.getIdentity(custUpdatedIdentities[i].IdType,
                        custUpdatedIdentities[i].IdValue, custUpdatedIdentities[i].IdIssuerCode,
                        (custUpdatedIdentities[i].IdExpiryData).FormatDate());

                    labelIdentName = new Label();
                    labelIdentValue = new Label();
                    labelIdentName.AutoSize = true;
                    labelIdentValue.AutoSize = true;

                    if (customerId == null)
                    {
                        labelIdentName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        labelIdentValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    }
                    else
                    {
                        labelIdentName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        labelIdentValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    }
                    if (custUpdatedIdentities[i].DatedIdentDesc != null)
                    {
                        //If its the first ID print the Id type. If not, check to see if the ID type
                        //is the same as the previous one then do not print it
                        if (i != 0)
                        {
                            labelIdentName.Text = custUpdatedIdentities[i].IsSameIDDesc(custUpdatedIdentities[i - 1]) ? "" : custUpdatedIdentities[i].DatedIdentDesc.ToString();
                        }
                        else
                            labelIdentName.Text = custUpdatedIdentities[i].DatedIdentDesc.ToString();
                    }
                    else
                    {
                        //If its the first ID print the Id type. If not, check to see if the ID type
                        //is the same as the previous one then do not print it
                        if (i != 0)
                        {
                            labelIdentName.Text = custUpdatedIdentities[i].IsSameIDType(custUpdatedIdentities[i - 1]) ? "" : custUpdatedIdentities[i].IdType.ToString();
                        }
                        else
                            labelIdentName.Text = custUpdatedIdentities[i].IdType.ToString();
                    }
                    labelIdentValue.Text = custUpdatedIdentities[i].IdIssuerCode.ToString() + "," + custUpdatedIdentities[i].IdValue;
                    this.tableLayoutPanel6.Controls.Add(labelIdentName, 0, i);
                    this.tableLayoutPanel6.Controls.Add(labelIdentValue, 2, i);
                    labelIdentName.Anchor = AnchorStyles.Right;
                    labelIdentValue.Anchor = AnchorStyles.Left;


                }
                _customerToView.removeIdentities();
                foreach (IdentificationVO id in _updatedCustomer.getAllIdentifications())
                {
                    _customerToView.addIdentity(id);
                }


            }
            else
            {
                //The updated Ids is null..check whether there were any original Ids
                if (custIdentities != null)
                {
                    for (int i = 0; i < custIdentities.Count; i++)
                    {

                        labelIdentName = new Label();
                        labelIdentValue = new Label();
                        labelIdentName.AutoSize = true;
                        labelIdentValue.AutoSize = true;
                        labelIdentName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        labelIdentValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        if (custIdentities[i].DatedIdentDesc != null)
                        {
                            //If its the first ID print the Id type. If not, check to see if the ID type
                            //is the same as the previous one then do not print it
                            if (i != 0)
                            {
                                labelIdentName.Text = custIdentities[i].IsSameIDDesc(custIdentities[i - 1]) ? "" : custIdentities[i].DatedIdentDesc.ToString();
                            }
                            else
                                labelIdentName.Text = custIdentities[i].DatedIdentDesc.ToString();
                        }
                        else
                        {
                            //If its the first ID print the Id type. If not, check to see if the ID type
                            //is the same as the previous one then do not print it
                            if (i != 0)
                            {
                                labelIdentName.Text = custIdentities[i].IsSameIDType(custIdentities[i - 1]) ? "" : custIdentities[i].IdType.ToString();
                            }
                            else
                                labelIdentName.Text = custIdentities[i].IdType.ToString();
                        }
                        labelIdentValue.Text = custIdentities[i].IdIssuerCode.ToString() + "," + custIdentities[i].IdValue;
                        this.tableLayoutPanel6.Controls.Add(labelIdentName, 0, i);
                        this.tableLayoutPanel6.Controls.Add(labelIdentValue, 2, i);
                        labelIdentName.Anchor = AnchorStyles.Right;
                        labelIdentValue.Anchor = AnchorStyles.Left;
                    }

                }
            }



        }

        private void linkLabelPhysicalDesc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _loadUpdatedData = false;
            UpdatePhysicalDesc physicalDescForm = new UpdatePhysicalDesc
            {
                CustToView = _customerToView
            };
            physicalDescForm.ShowDialog(this);
            if (_loadUpdatedData)
                LoadPhysicalDescData();
        }

  

        private void tabControl2_Selecting(object sender, TabControlCancelEventArgs e)
        {
            /*if (e.TabPageIndex == 1 || e.TabPageIndex == 2 || e.TabPageIndex == 3 || e.TabPageIndex == 4)
                e.Cancel = true;*/
            if (e.TabPage.Name.Equals("PhysicalDescription",StringComparison.OrdinalIgnoreCase))
            {
                //Its the view physical desc tab that was clicked check that the user has access
                UserVO currUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                bool usercanview = SecurityProfileProcedures.CanUserViewResource(e.TabPage.Name.ToUpper(), currUser, GlobalDataAccessor.Instance.DesktopSession);
                bool usercanModify = SecurityProfileProcedures.CanUserModifyResource("EDIT" + e.TabPage.Name.ToUpper(), currUser, GlobalDataAccessor.Instance.DesktopSession);
                if ( usercanview || usercanModify)
                {
                    //user can either view or modify so allow the tab to be clicked
                    e.Cancel = false;
                    if (usercanview && !usercanModify)
                    {
                        foreach (Control ctrl in PhysicalDescription.Controls)
                            ctrl.Enabled = false;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {

            if (customButtonCancel.Text == "Close")
            {
                GlobalDataAccessor.Instance.DesktopSession.TabStateClicked = FlowTabController.State.None;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "Close";
                this.NavControlBox.Action = NavBox.NavAction.BACK;
            }
            else
            {
                DialogResult dR = MessageBox.Show("Do you want to continue processing this customer?", "View Customer Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dR == DialogResult.No)
                {
                    GlobalDataAccessor.Instance.DesktopSession.ClearCustomerList();

                }

                //1/29/2010 According to QA requirement Cancel should take you to ring menu!
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "Menu";
                this.NavControlBox.Action = NavBox.NavAction.BACK;

                
            }
        }

  



    }
}
