using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic;
using System.Collections.Generic;

namespace Pawn.Forms.Pawn.Services.Pickup
{
    public partial class FirearmsBackgroundCheck : CustomBaseForm
    {
        private bool _idCwpExists;
        private CustomerVO _currentCustomer;
        private List<RetailItem> _firearmItems;
        private bool _retailFirearmCheck;
        private bool _layawayFirearmCheck;

        public FirearmsBackgroundCheck()
        {
            InitializeComponent();
        }

        private Boolean IsBackgroundCheckRequired
        {
            get;
            set;
        }

        private void SetupDisplayForList()
        {
            if (_retailFirearmCheck)
            {
                foreach (RetailItem item in _firearmItems)
                {
                    int gvIdx = gvMerchandise.Rows.Add();
                    DataGridViewRow myRow = gvMerchandise.Rows[gvIdx];
                    //string icn = i.Icn;
                    myRow.Cells[colICN.Name].Value = item.Icn;
                    myRow.Cells[colMerchandiseDescription.Name].Value = item.TicketDescription;
                    myRow.Cells[colStatus.Name].Value = item.ItemStatus;
                    myRow.Cells[colSalePrice.Name].Value = item.NegotiatedPrice.ToString("c");

                    myRow.Tag = item;
                }
            }
            else if (_layawayFirearmCheck)
            {
                customLabelBackGroundCheckFee.Visible = true;
                customTextBoxBackgroundCheckFee.Visible = true;

                foreach (RetailItem item in _firearmItems)
                {
                    int gvIdx = gvMerchandise.Rows.Add();
                    DataGridViewRow myRow = gvMerchandise.Rows[gvIdx];
                    //string icn = i.Icn;
                    myRow.Cells[colICN.Name].Value = item.Icn;
                    myRow.Cells[colMerchandiseDescription.Name].Value = item.TicketDescription;
                    myRow.Cells[colStatus.Name].Value = item.ItemStatus;
                    myRow.Cells[colSalePrice.Name].Value = item.RetailPrice.ToString("c");

                    myRow.Tag = item;
                }
            }
            else
            {
                this.Height -= gvMerchandise.Height;
                gvMerchandise.Visible = false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CashlinxDesktopSession.Instance.BackgroundCheckFeeValue = 0M;
            CashlinxDesktopSession.Instance.BackgroundCheckCompleted = false;
            Close();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            string strIdentNumber = customTextBoxCWPNumber.Text;
            Boolean cancelSubmit = false;

            //BZ # 753
            //if (!_retailFirearmCheck && !customTextBoxRefNumber.isValid)
            if (!_retailFirearmCheck && !customTextBoxRefNumber.isValid && string.IsNullOrEmpty(strIdentNumber))
            {
                MessageBox.Show(@"Background reference number must be entered");
                return;
            }

            if (!checkExpDate())
            {
                this.dateCWP.Focus();
                return;
            }

            //store the background ref check number in the customer object
            _currentCustomer.BackgroundCheckRefNumber = customTextBoxRefNumber.Text;
            //store the CWP data against the customer if he did not already
            //have a cwp and if they entered CWP data in the form
            ComboBox statelist = (ComboBox)stateCWP.Controls[0];
            string cwpIssuerState = statelist.GetItemText(statelist.SelectedItem);
            string cwpIssuerStateName = statelist.SelectedValue.ToString();
            string strIdentExpiryDate = dateCWP.Controls[0].Text;
            if (strIdentExpiryDate.Equals("mm/dd/yyyy"))
            {
                strIdentExpiryDate = string.Empty;
            }
            string strIdentTypeCode = CustomerIdTypes.CW.ToString();

            IdentificationVO custCWPID = new IdentificationVO();
            string storeState = CashlinxDesktopSession.Instance.CurrentSiteId.State;
            if (_currentCustomer.HasValidConcealedWeaponsPermitInState(storeState, ShopDateTime.Instance.ShopDate))
            {
                custCWPID = _currentCustomer.getIdByTypeandIssuer(CustomerIdTypes.CW.ToString(), storeState);
            }

            if (!string.IsNullOrEmpty(custCWPID.IdValue.Trim()))
            {
                _idCwpExists = true;

                //idExpDate = custCWPID.IdExpiryData.Date != DateTime.MaxValue.Date ? custCWPID.IdExpiryData.FormatDate() : string.Empty;
                //idIssuer = custCWPID.IdIssuerCode;
            }

            decimal backgroundCheckFee;

            if (_retailFirearmCheck || _layawayFirearmCheck)
            {
                //Check if ident number and issuer and expiry date is entered if reference number was not entered
                if (!customTextBoxRefNumber.isValid && (strIdentNumber.Equals(string.Empty) || cwpIssuerState.Equals("Select One") || strIdentExpiryDate == string.Empty || strIdentExpiryDate == string.Empty))
                {
                    if (!IsBackgroundCheckRequired)
                    {
                        MessageBox.Show("Either Background reference number or all CWP data should be entered");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Background reference number should be entered");
                        return;
                    }
                }
                else if (_layawayFirearmCheck)
                {
                    //if (!decimal.TryParse(customTextBoxBackgroundCheckFee.Text, out backgroundCheckFee) || backgroundCheckFee < 0)
                    bool formatSuccess = Commons.FormatStringAsDecimal(customTextBoxBackgroundCheckFee.Text, out backgroundCheckFee);

                    //BZ # 503                    
                    if (formatSuccess)
                    {
                        if (backgroundCheckFee <= 0.0m)
                        {
                            string msg = "Back ground Check Fee amount is less or equal to zero. Is that okay?";
                            DialogResult dgr = MessageBox.Show(msg, "Back ground Check Fee equals zero", MessageBoxButtons.YesNo);
                            if (dgr == DialogResult.No)
                            {
                                return;
                            }
                        }
                        else
                        {
                            CashlinxDesktopSession.Instance.BackgroundCheckFeeValue = backgroundCheckFee;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid background check fee");
                        return;
                    }
                    //BZ # 503 end
                }
                else
                {
                    CashlinxDesktopSession.Instance.BackgroundCheckFeeValue = 0M;
                }
            }
            else
            {
                if ((!customTextBoxRefNumber.isValid) && (strIdentNumber.Equals(string.Empty) || cwpIssuerState.Equals("Select One")))
                {
                    if (!IsBackgroundCheckRequired)
                    {
                        MessageBox.Show("Either Background reference number or all CWP data should be entered");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Background reference number should be entered");
                        return;
                    }
                }

                CashlinxDesktopSession.Instance.BackgroundCheckFeeValue = 0M;
            }

            if ((!customTextBoxRefNumber.isValid) && !IsBackgroundCheckRequired && _currentCustomer.HasConcealedWeaponsPermit())
            {
                //check to see if the object has been populated with data.
                if (custCWPID.IdExpiryData == DateTime.MaxValue)
                {
                    custCWPID = _currentCustomer.getAllIdentifications().Where(i => i.IdType == CustomerIdTypes.CW.ToString()).FirstOrDefault();
                }

                if (strIdentNumber.Trim() != custCWPID.IdValue.Trim() || cwpIssuerState != custCWPID.IdIssuerCode)
                {
                    DialogResult dgr = MessageBox.Show("This customer has the following CWP on file: " + custCWPID.IdIssuerCode + "-" + custCWPID.IdValue + ". If you continue, the CWP you entered will replace the one on file.", "Concealed Weapons Permit Already Exists", MessageBoxButtons.YesNo);
                    if (dgr == DialogResult.No)
                        return;
                }
            }

            //Check if the ID is a new id entered in this form or it already exists
            //for the customer
            //Add to the database only if its a new id
            if (!string.IsNullOrEmpty(strIdentNumber) && !cwpIssuerState.Equals("Select One"))
            {
                IdentificationVO custIdInfo = _currentCustomer.getIdentity(CustomerIdTypes.CW.ToString(), strIdentNumber, cwpIssuerState, strIdentExpiryDate);
                if (custIdInfo == null)
                {
                    DialogResult dgr;
                    do
                    {
                        string strUserId = CashlinxDesktopSession.Instance.UserName;
                        string errorMsg = string.Empty;
                        string errorCode = string.Empty;
                        bool updateIdData = new CustomerDBProcedures(CashlinxDesktopSession.Instance).UpdateCustPersonalIdentification(_currentCustomer.PartyId, strIdentNumber, strIdentExpiryDate, strIdentTypeCode, cwpIssuerState, strUserId, out errorCode, out errorMsg);
                        if (updateIdData)
                        {
                            IdentificationVO newCustId = new IdentificationVO
                            {
                                IdType = strIdentTypeCode,
                                IdValue = strIdentNumber,
                                IdIssuerCode = cwpIssuerState,
                                IdIssuer = cwpIssuerStateName,
                                DatedIdentDesc = StateIdTypeDescription.CW.ToString(),
                                IdExpiryData =
                                        Utilities.GetDateTimeValue(strIdentExpiryDate, DateTime.MaxValue)
                            };
                            _currentCustomer.addIdentity(newCustId);

                            break;
                        }
                        dgr = MessageBox.Show(Commons.GetMessageString("CustIdentUpdateFailure"), "Error", MessageBoxButtons.RetryCancel);
                    } while (dgr == DialogResult.Retry);
                }
            }
            //If the user cancelled retrying to update the db when it failed
            //continue since the CWP can always be added later for the customer and do
            //not have to stop the pickup flow for it
            GlobalDataAccessor.Instance.DesktopSession.BackgroundCheckCompleted = true;
            GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer = _currentCustomer;
            Close();

        }

        private void FirearmsBackgroundCheck_Load(object sender, EventArgs e)
        {
            IsBackgroundCheckRequired = CustomerProcedures.IsBackgroundCheckRequired(CashlinxDesktopSession.Instance);
            if (!IsBackgroundCheckRequired)
            {
                customLabelCWPNumber.Visible = true;
                customTextBoxCWPNumber.Visible = true;
                customLabelCWPExpDate.Visible = true;
                dateCWP.Visible = true;
                customLabelState.Visible = true;
                stateCWP.Visible = true;
            }
            else
            {
                customLabelCWPNumber.Visible = false;
                customTextBoxCWPNumber.Visible = false;
                customLabelCWPExpDate.Visible = false;
                dateCWP.Visible = false;
                customLabelState.Visible = false;
                stateCWP.Visible = false;
            }

            if (CashlinxDesktopSession.Instance.ActiveRetail != null)
            {
                _firearmItems = (from itemData in CashlinxDesktopSession.Instance.ActiveRetail.RetailItems
                                 where itemData.IsGun
                                 select itemData).ToList();
                _retailFirearmCheck = true;
                _layawayFirearmCheck = false;
            }
            else if (CashlinxDesktopSession.Instance.ServiceLayaways != null && CashlinxDesktopSession.Instance.ServiceLayaways.Count > 0)
            {
                List<LayawayVO> pickupLayaways = CashlinxDesktopSession.Instance.ServiceLayaways.FindAll(l => l.LoanStatus == ProductStatus.PU).ToList();
                if (pickupLayaways.Count > 0)
                {
                    _firearmItems = pickupLayaways.SelectMany(l => l.RetailItems).Where(i => i.IsGun).ToList();
                    _retailFirearmCheck = false;
                    _layawayFirearmCheck = true;
                }
            }
            SetupDisplayForList();
            _currentCustomer = CashlinxDesktopSession.Instance.ActiveCustomer;
            string strStoreState = CashlinxDesktopSession.Instance.CurrentSiteId.State;
            IdentificationVO custCWPID = new IdentificationVO();
            string idNumber = string.Empty;
            string idExpDate = string.Empty;
            string idIssuer = string.Empty;
            bool dateExpired = false;
            if (!(strStoreState.Equals(string.Empty)))
            {
                List<IdentificationVO> identifications = _currentCustomer.getAllIdentifications();
                foreach (IdentificationVO id in identifications)
                {
                    if (id.IdType != null && id.IdType.Equals(CustomerIdTypes.CW.ToString()))
                    {
                        DateTime expiryDate = Utilities.GetDateTimeValue(id.IdExpiryData, DateTime.MaxValue);
                        if (expiryDate.Date < ShopDateTime.Instance.ShopDate.Date)
                        {
                            dateExpired = true;
                            break;
                        }
                    }
                }

                if (_currentCustomer.HasValidConcealedWeaponsPermitInState(strStoreState, ShopDateTime.Instance.ShopDate) || dateExpired)
                {
                    custCWPID = _currentCustomer.getIdByTypeandIssuer(CustomerIdTypes.CW.ToString(), strStoreState);
                }
            }

            if (custCWPID != null && custCWPID.IdValue != string.Empty)
            {
                _idCwpExists = true;
                idNumber = custCWPID.IdValue;
                idExpDate = custCWPID.IdExpiryData.Date != DateTime.MaxValue.Date ? custCWPID.IdExpiryData.FormatDate() : string.Empty;
                idIssuer = custCWPID.IdIssuerCode;
            }

            string CWPNum = this.customTextBoxCWPNumber.Text;
            if (_idCwpExists)
            {
                ComboBox statelist = (ComboBox)this.stateCWP.Controls[0];
                statelist.SelectedIndex = 0;

                if (dateExpired)
                    this.dateExpireWarning.Visible = true;
                else
                    this.dateExpireWarning.Visible = false;
            }
            else if (string.Empty.Equals(CWPNum))
            {
                dateCWP.Enabled = false;
                stateCWP.Enabled = false;
            }
            else
            {
                customTextBoxCWPNumber.Enabled = true;
                dateCWP.Enabled = true; ;
                stateCWP.Enabled = true;
            }
            this.dateCWP.ErrorMessage = Commons.GetMessageString("InvalidDate");

            //BZ # 503
            if (customTextBoxBackgroundCheckFee.TextLength == 0)
                customTextBoxBackgroundCheckFee.Text = "0.00";
            //BZ # 503 end
        }

        private void dateCWP_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!checkExpDate()) return;
                string enteredDate = dateCWP.Controls[0].Text;
                DateTime expiryDate = Utilities.GetDateTimeValue(enteredDate, DateTime.MaxValue);

                if (expiryDate != DateTime.MaxValue && expiryDate.Date < ShopDateTime.Instance.ShopDate.Date)
                    MessageBox.Show(Commons.GetMessageString("InvalidDate"));

            }
            catch (Exception)
            {
                MessageBox.Show(Commons.GetMessageString("InvalidDate"));
                this.dateCWP.Focus();
            }

        }

        private void customTextBoxBackgroundCheckFee_Leave(object sender, EventArgs e)
        {
            //BZ # 503
            if (customTextBoxBackgroundCheckFee.TextLength == 0)
                customTextBoxBackgroundCheckFee.Text = "0.00";
            else
            {
                decimal backgroundCheckFee;
                bool formatSuccess = Commons.FormatStringAsDecimal(customTextBoxBackgroundCheckFee.Text, out backgroundCheckFee);
                if (formatSuccess)
                {

                    if (backgroundCheckFee <= 0.0m)
                        customTextBoxBackgroundCheckFee.Text = "0.00";
                    else
                        customTextBoxBackgroundCheckFee.Text = backgroundCheckFee.ToString();
                }
            }
            //BZ # 503 end

            decimal value;

            if (!decimal.TryParse(customTextBoxBackgroundCheckFee.Text, out value))
            {
                MessageBox.Show("Invalid background check fee");
                customTextBoxBackgroundCheckFee.RevertValue();
                return;
            }

            if (value < 0)
            {
                MessageBox.Show("Background check fee cannot be negative");
                customTextBoxBackgroundCheckFee.RevertValue();
                return;
            }
        }

        private bool checkExpDate()
        {
            string CWPNum = this.customTextBoxCWPNumber.Text;

            if (CWPNum != null && !CWPNum.Equals(string.Empty))
            {
                string enteredDate = dateCWP.Controls[0].Text;
                DateTime expiryDate = Utilities.GetDateTimeValue(enteredDate, DateTime.MaxValue);

                if (enteredDate.Equals(string.Empty) || 
                    enteredDate.Equals("mm/dd/yyyy") ||
                    string.Empty.Equals(stateCWP)    ||
                    string.Empty.Equals(CWPNum.Trim()))
                {
                    MessageBox.Show("Please enter CWP, State and Expiry Date.");
                    return false;
                }
                else if (expiryDate != DateTime.MaxValue && expiryDate.Date < ShopDateTime.Instance.ShopDate.Date)
                {
                    MessageBox.Show("Concealed Weapon Permit has expired.");
                    return false;
                }
            }
            return true;
        }
        private void customTextBoxCWPNumber_Leave(object sender, EventArgs e)
        {
            disableControls();
        }

        private void customTextBoxCWPNumber_TextChanged(object sender, EventArgs e)
        {
            disableControls();
        }

        private void disableControls()
        {
            string CWPNum = this.customTextBoxCWPNumber.Text;
            if (string.Empty.Equals(CWPNum))
            {
                this.dateCWP.Text = "mm/dd/yyyy";
                stateCWP.selectedValue = "Select One";
                dateCWP.Enabled = false;
                stateCWP.Enabled = false;
            }
            else
            {
                dateCWP.Enabled = true;
                stateCWP.Enabled = true;
            }
        }

    }
}
