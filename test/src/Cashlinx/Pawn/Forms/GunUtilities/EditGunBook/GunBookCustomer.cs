using System;
using System.Data;
using System.Windows.Forms;
using CashlinxDesktop.UserControls;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Pawn.Logic;

namespace Pawn.Forms.GunUtilities.EditGunBook
{
    public partial class GunBookCustomer : CustomBaseForm
    {

        public NavBox NavControlBox;
        private CustomerVO gunBookCustomerData;
        private string gunNumber;
        private CustomerType customerEditType;
        private CustomerVO newCustomer;
 
        public GunBookCustomer()
        {
            InitializeComponent();
            NavControlBox = new NavBox
            {
                Owner = this
            };
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void GunBookCustomer_Load(object sender, EventArgs e)
        {
            gunBookCustomerData = GlobalDataAccessor.Instance.DesktopSession.GunBookCustomerData;
            gunNumber = GlobalDataAccessor.Instance.DesktopSession.GunNumber;
            customerEditType = GlobalDataAccessor.Instance.DesktopSession.CustomerEditType;
            if (gunBookCustomerData != null)
            {
                currentName.Text = string.Format("{0} {1}", gunBookCustomerData.FirstName, gunBookCustomerData.LastName);
                customerName.Text = currentName.Text;
                AddressVO custAddr = gunBookCustomerData.getAddress(0);
                if (custAddr != null)
                {
                    address1.Text = custAddr.Address1;
                    address2.Text = string.Format("{0},{1} {2}", custAddr.City, custAddr.State_Code, custAddr.ZipCode);
                    customTextBoxaddr1.Text = custAddr.Address1;
                    customTextBoxAddr2.Text = custAddr.Address2;
                    customTextBoxCity.Text = custAddr.City;
                    ComboBox custstate = (ComboBox)state1.Controls[0];
                    foreach (USState currstate in custstate.Items)
                        if (currstate.ShortName == custAddr.State_Code)
                        {
                            custstate.SelectedIndex = custstate.Items.IndexOf(currstate);
                            break;
                        }
                    zipcode1.Text = custAddr.ZipCode;
                }
                customerNumber.Text = gunBookCustomerData.CustomerNumber;
                labelCustNumber.Text = gunBookCustomerData.CustomerNumber;
                IdentificationVO custId = gunBookCustomerData.getFirstIdentity();
                if (custId != null)
                {
                    currentID.Text = custId.IdType + " " + custId.IdIssuerCode + " " + custId.IdValue;
                    id.Text = currentID.Text;

                }
                newCustomer = Utilities.CloneObject(gunBookCustomerData);
                GlobalDataAccessor.Instance.DesktopSession.GunAcquireCustomer = this.customerEditType == CustomerType.RECEIPT;

            }
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            string errorCode;
            string errorText;
            var dSession = GlobalDataAccessor.Instance.DesktopSession;
            MerchandiseProcedures.UpdateGunCustomerData(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
             gunNumber,
             dSession.GunAcquireCustomer ? "A" : "D",
             newCustomer.LastName,
             newCustomer.FirstName,
             !string.IsNullOrEmpty(newCustomer.MiddleInitial) ? newCustomer.MiddleInitial.Substring(0, 1) : "",
             string.Format("{0} {1}", customTextBoxaddr1.Text, customTextBoxAddr2.Text),
             customTextBoxCity.Text,
             state1.selectedValue,
             zipcode1.Text,
             newCustomer.getFirstIdentity().IdType,
             newCustomer.getFirstIdentity().IdIssuer,
             newCustomer.getFirstIdentity().IdValue,
             dSession.UserName,
             out errorCode,
             out errorText);
            if (errorCode != "0")
                MessageBox.Show("Customer data could not be updated");
            else
            {
                MessageBox.Show("Customer data updated successfully");
                DataTable gunTableData;
                Item gunItem;
                MerchandiseProcedures.GetGunData(GlobalDataAccessor.Instance.OracleDA,
                                    GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                                    Utilities.GetIntegerValue(gunNumber),
                                    out gunTableData,
                                    out gunItem,
                                    out errorCode,
                                    out errorText);
                dSession.GunData = gunTableData;
                dSession.GunItemData = gunItem;


            }
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;

        }

        private void customTextBoxaddr1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(customTextBoxaddr1.Text))
            {
                MessageBox.Show("Address cannot be empty");
            }
        }


        private void customTextBoxCity_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(customTextBoxCity.Text))
            {
                MessageBox.Show("City cannot be empty");
            }

        }

        private void zipcode1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(zipcode1.Text))
            {
                ComboBox state = (ComboBox)this.state1.Controls[0];
                if (zipcode1.isValid)
                {
                    if (zipcode1.City.Length > 0)
                    {
                        customTextBoxCity.Text = zipcode1.City;
                    }
                    else
                    {
                        this.customTextBoxCity.Text = "";
                    }
                    if (zipcode1.State.Length > 0)
                    {
                        foreach (USState currstate in state.Items)
                            if (currstate.ShortName == zipcode1.State)
                            {
                                state.SelectedIndex = state.Items.IndexOf(currstate);
                                break;
                            }
                    }
                    else
                    {
                        state.SelectedIndex = -1;
                    }

                }
            }
            else
            {
                MessageBox.Show("Zipcode cannot be empty");
                zipcode1.Focus();
            }

        }

        private void state1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(state1.selectedValue))
                MessageBox.Show("Please select a valid state");
        }


 
  

    

   
    }
}
