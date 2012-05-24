/**************************************************************************************************************
* CashlinxDesktop
* CreateVendor
* This form is used to create a vendor in the manage vendor use case
* Tracy McConnell 8/10/2010 Initial version
* Madhu 11/16/2010 fix for the defect PWNU00001448
**************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;
using Utilities = Common.Libraries.Utility.Utilities;

namespace Pawn.Forms.Pawn.Customer
{
    public partial class CreateVendor : Form
    {
        private Form ownerFrm;
        private LookupVendorSearchData currentSearchData;
        string strUserId = "";
        string strStoreNumber = "";

        bool _isFormValid;

        bool retValue = false;
        ProcessingMessage procMsg;
        public NavBox NavControlBox;// { get; set; }
        string vendorID = "";
        VendorVO vendor;

        public CreateVendor()
        {
            ownerFrm = this.Owner;
            vendor = new VendorVO();
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        private void CreateVendor_Load(object sender, EventArgs e)
        {
            this.NavControlBox.Owner = this;
            var gSess = GlobalDataAccessor.Instance;
            var dSession = gSess.DesktopSession;
            strStoreNumber = gSess.CurrentSiteId.StoreNumber;
            strUserId = dSession.UserName;
            currentSearchData = dSession.LookupCriteria;

            if (dSession.ActiveVendor != null &&
                !dSession.ActiveVendor.NewVendor)
            {
                vendor = GlobalDataAccessor.Instance.DesktopSession.ActiveVendor;
                PopulateVendor(vendor);

                if (!vendor.CreationStore.Equals(gSess.CurrentSiteId.StoreNumber))
                {
                    vendInfoPanel.Enabled = false;
                    if (GlobalDataAccessor.Instance.DesktopSession.VenderFFLRequired)
                    {
                        corporateFFLLabel.Visible = true;
                        customButtonSubmit.Enabled = false;
                    }
                    else
                    {
                        corporateFFLLabel.Visible = false;
                    }
                }
                else
                {
                    const string resourceName = "EDITVENDOR";
                    var currUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                    if (!(SecurityProfileProcedures.CanUserViewResource(resourceName, currUser, dSession)))
                        vendInfoPanel.Enabled = false;
                }

                if (!(vendInfoPanel.Enabled))
                {
                    customButtonSubmit.Text = "Continue";

                    if (GlobalDataAccessor.Instance.DesktopSession.VenderFFLRequired && !Utilities.IsValidFFL(ffl.Text))
                    {
                        customButtonSubmit.Enabled = false;
                    }
                }
            }
            else if (GlobalDataAccessor.Instance.DesktopSession.VenderFFLRequired && GlobalDataAccessor.Instance.DesktopSession.ActiveVendor != null && CashlinxDesktopSession.Instance.ActiveVendor.NewVendor)
            {
                vendor = GlobalDataAccessor.Instance.DesktopSession.ActiveVendor;
                PopulateVendor(vendor);
            }
            else if (currentSearchData != null)
            {
                this.name.Text = currentSearchData.VendName;
                //this.taxID.Text = currentSearchData.TaxID;
                this.taxID.Enabled = false;
            }

            this.zipcode.stateChanging += this.state.dependentTextChanged;
            this.zipcode.cityChanging += city.dependentTextChanged;

            this.name.Focus();
        }

        private void PopulateVendor(VendorVO vendor)
        {
            name.Text = vendor.Name;
            taxID.Text = vendor.TaxID;
            ffl.Text = vendor.Ffl;
            address1.Text = vendor.Address1;
            address2.Text = vendor.Address2;
            zipcode.Text = vendor.ZipCode;
            zip4.Text = vendor.Zip4;
            city.Text = vendor.City;
            state.selectedValue = vendor.State;
            phone.Text = vendor.ContactPhone;
            phone2.Text = vendor.ContactPhone2;
            fax.Text = vendor.faxPhone;
            contact.Text = vendor.ContactName;
            comment.Text = vendor.Comment;

            if (CashlinxDesktopSession.Instance.VenderFFLRequired)
            {
                ffl.Required = true;
                customLabel8.Required = true;
                errorLabel.Text = "A valid FFL# is required for all firearm transactions";
                errorLabel.Visible = true;
            }
        }

        private void createVendorCancelButton_Click(object sender, EventArgs e)
        {
            //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
            var dgr = MessageBox.Show(Commons.GetMessageString("CancelConfirmMessage"), "Warning", MessageBoxButtons.YesNo);
            if (dgr == DialogResult.Yes)
            {
                CashlinxDesktopSession.Instance.VenderFFLRequired = false;
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
            else
            {
                return;
            }
        }

        private void checkFormFields()
        {
            // _isFormValid = this.name.isValid && this.taxID.isValid;
            IEnumerable<Control> requiredFields = from Control c in new AllControls(this)
                                                  where (c.GetType().GetProperty("Required") != null &&
                                                         c.GetType().GetProperty("isValid") != null)
                                                  select c;

            _isFormValid = true;

            foreach (Control c in requiredFields)
            {
                PropertyInfo p = c.GetType().GetProperty("Required");

                if ((bool)p.GetValue(c, null))
                { // if field value isRequired
                    p = c.GetType().GetProperty("isValid");
                    _isFormValid = _isFormValid && (bool)p.GetValue(c, null);

                    if (!_isFormValid)
                        break;
                }
            }
        }

        // INSTEAD -- register required fields when they are indicated as required, and the validator can "check required fields" without explicit knowledge of which they are.
        //    AND without having to touch every control on the form -- it would then ONLY touch the required fields
        // would be nice to only touch the control once, and instead of accumulating it into a list, then reprocessing the list
        // perhaps using an enumerator
        class AllControls : IEnumerable<Control>
        {
            Form theForm;

            public AllControls(Form aForm)
            {
                theForm = aForm;
            }

            IEnumerator<Control> IEnumerable<Control>.GetEnumerator()
            {
                return new ControlEnumerator(theForm.Controls);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new ControlEnumerator(theForm.Controls);
            }
        }

        class ControlEnumerator : IEnumerator<Control>
        {
            class EnumerationNode
            {
                public int position;
                public Control.ControlCollection theControls;

                public EnumerationNode(int pos, Control.ControlCollection c)
                {
                    position = pos;
                    theControls = c;
                }
            };

            private Stack<EnumerationNode> controlTree = new Stack<EnumerationNode>();

            //private Stack<int> position = new Stack<int>();
            //private Stack<Control.ControlCollection> theControls = new Stack<Control.ControlCollection>();

            public ControlEnumerator(Control.ControlCollection controls)
            {
                controlTree.Push(new EnumerationNode(0, controls));
            }

            Object IEnumerator.Current
            {
                get
                {
                    EnumerationNode n = controlTree.First();

                    return (Object)n.theControls[n.position];
                }
            }

            Control IEnumerator<Control>.Current
            {
                get
                {
                    EnumerationNode n = controlTree.First();

                    return n.theControls[n.position];
                }
            }

            public bool MoveNext()
            {
                EnumerationNode n = controlTree.Peek();

                n.position += 1;
                int i = n.position;
                Control.ControlCollection c = n.theControls;

                if (!(i < c.Count && i > 0))
                    controlTree.Pop();
                else if (c[i].HasChildren)
                    controlTree.Push(new EnumerationNode(0, c[i].Controls));

                return controlTree.Count > 0;
            }

            public void Reset()
            {
                EnumerationNode n = controlTree.Last();
                n.position = 0;
                controlTree.Clear();
                controlTree.Push(n);
            }

            void IDisposable.Dispose()
            {
            }
        }

        private bool saveData()
        {
            procMsg = new ProcessingMessage("Saving Vendor Data");
            SetButtonState(false);
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
            procMsg.ShowDialog(this);
            if (retValue)
            {
                return true;
            }
            return false;
        }
        private void SetButtonState(bool enable)
        {
            customButtonSubmit.Enabled = enable;
            customButtonCancel.Enabled = enable;
        }

        //Madhu 11/16/2010 fix for the defect PWNU00001448
        private void phone_Leave(object sender, EventArgs e)
        {
            if (phone.Text.Length < 12)
            {
                MessageBox.Show(Commons.GetMessageString("LookupPhoneNumberError"));
                phone.Focus();
            }
        }

        //Madhu 11/16/2010 fix for the defect PWNU00001448
        private void phone2_Leave(object sender, EventArgs e)
        {
            if (phone2.Text.Length > 0 && phone2.Text.Length < 12)
            {
                MessageBox.Show(Commons.GetMessageString("LookupPhoneNumberError"));
                phone2.Focus();
            }
        }

        //Madhu 11/16/2010 fix for the defect PWNU00001448
        private void fax_Leave(object sender, EventArgs e)
        {
            if (fax.Text.Length > 0 && fax.Text.Length < 12)
            {
                MessageBox.Show(Commons.GetMessageString("LookupPhoneNumberError"));
                fax.Focus();
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var errorDesc = string.Empty;
            DialogResult dgr = DialogResult.Retry;
            //Make a call to save in the database

            do
            {
                vendorID = vendor.ID;
                retValue = VendorProcedures.AddVendor(vendor, strUserId, strStoreNumber, out vendorID, out errorDesc);
                if (retValue)
                    break;
                else
                {
                    dgr = MessageBox.Show(Commons.GetMessageString("ProcessingError"), "Error", MessageBoxButtons.RetryCancel);

                    if (dgr == DialogResult.OK)
                        break;
                }
            }while (dgr == DialogResult.Retry);
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            procMsg.Update();
            procMsg.Close();
            procMsg.Dispose();
            SetButtonState(true);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (customButtonSubmit.Text == "S&ubmit")
            {
                try
                {
                    checkFormFields();
                    if (_isFormValid)
                    {
                        createVendorObject();
                        if (saveData())
                        {
                            if (!string.IsNullOrEmpty(vendorID))
                                vendor.ID = vendorID;

                            GlobalDataAccessor.Instance.DesktopSession.ActiveVendor = vendor;
                            this.NavControlBox.IsCustom = true;
                            this.NavControlBox.CustomDetail = "AddVendorComplete";
                            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                        }
                        else
                        {
                            this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                        }
                    }
                    else
                    {
                        MessageBox.Show(Commons.GetMessageString("FormRequiredFieldsFilledError"));
                        return;
                    }
                }
                catch (Exception)
                {
                    //CashlinxDesktopSession.Instance.FormState = CashlinxDesktopSession.CustomerFormStates.CANCEL;
                    //CustomerController.NavigateUser(ownerFrm);
                    this.NavControlBox.Action = NavBox.NavAction.CANCEL;
                }
            }
            else
            {
                createVendorObject();
                GlobalDataAccessor.Instance.DesktopSession.ActiveVendor = vendor;
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "AddVendorComplete";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
        }

        private void createVendorObject()
        {
            VendorProcedures.setVendorDataInObject(ref vendor,
                                                   name.Text,
                                                   taxID.Text,
                                                   ffl.Text,
                                                   address1.Text,
                                                   address2.Text,
                                                   zipcode.Text,
                                                   zip4.Text,
                                                   city.Text,
                                                   state.selectedValue,
                                                   phone.Text,
                                                   phone2.Text,
                                                   fax.Text,
                                                   contact.Text,
                                                   comment.Text);
            if (!string.IsNullOrEmpty(vendorID))
                vendor.ID = vendorID;
        }

        private void ffl_Leave(object sender, EventArgs e)
        {
            if (ffl.Text.Length > 0 && !Utilities.IsValidFFL(ffl.Text))
            {
                MessageBox.Show("FFL should be 15 characters.");
                ffl.Focus();
                _isFormValid = false;
            }
 
        }
    }
}
