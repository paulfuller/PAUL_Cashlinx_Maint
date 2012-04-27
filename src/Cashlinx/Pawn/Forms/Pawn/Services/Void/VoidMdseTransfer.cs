using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.Void
{
    


    public partial class VoidMdseTransfer : CustomBaseForm
    {
        private const string TRAN_TYPE_SCRAP = "S";
        private const string TRAN_TYPE_REFURB = "R";
        private const string TRAN_TYPE_EXCESS = "E";
        private const string TRAN_TYPE_GUN_SHOP = "N";
        private const string TRAN_TYPE_STORE2STORE = "";
        private const string TRAN_TYPE_INVALID = "INVALID";
        private const string TRAN_TYPE_CAF="C";

        private const string PROC_FOR_SCRAP = "void_to_scrap";
        private const string PROC_FOR_EXCESS = "void_to_excess";
        private const string PROC_FOR_REFURB = "void_to_refurb";

        private bool isTranOut = false;
        private DataTable mdseTransfer;
        private DataTable xfrInfo;
        private const string INVALIDVOIDMESSAGE = "The number of days to void has passed.";
        private const string INVALIDSTATUSMESSAGE = "One or more merchandise is not in PFI status. Cannot void";

        public VoidMdseTransfer()
        {
            InitializeComponent();
        }

        private void VoidMdseTransfer_Load(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.MdseTransferData != null)
            {
                
                mdseTransfer = GlobalDataAccessor.Instance.DesktopSession.MdseTransferData.Tables["MDSEINFO"];
                xfrInfo = GlobalDataAccessor.Instance.DesktopSession.MdseTransferData.Tables["TRANSINFO"];
            }
            long maxVoidDays = 0L;
            if (!new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxMdseTransferVoidDays(GlobalDataAccessor.Instance.CurrentSiteId,
                out maxVoidDays))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                        "Cannot retrieve maximum void days. Defaulting to {0}", maxVoidDays);
                }
            }
            string fromStore = string.Empty;
            DateTime transferDate = DateTime.MinValue;
            if (xfrInfo != null && xfrInfo.Rows.Count > 0)
            {
                labelHeading.Text = "Void Transfer " + ((xfrInfo.Rows[0]["status"].Equals("TI")) ? "IN" : "OUT" );

                isTranOut=((xfrInfo.Rows[0]["status"].Equals("TO")) ? true:false);
                if (isTranOut)
                {
                    fromStore = xfrInfo.Rows[0]["entitynumber"].ToString();
                }
                else
                {
                    fromStore = xfrInfo.Rows[0]["from_store"].ToString();
                }

                switch (fromStore )
                {
                    case "00068":
                        fromStore = "Catco Facility # 68 Refurb";
                        break;

                    case "00086":
                        fromStore = "GunRoom Facility # 86 CAF";
                        break;
                    
                    default:
                        if (isTranOut)
                        {
                            fromStore = xfrInfo.Rows[0]["entitynumber"].ToString();
                        }
                        else
                        {
                            fromStore = xfrInfo.Rows[0]["from_store"].ToString();
                        }
                        break;
                }

                transferDate=Utilities.GetDateTimeValue(Utilities.GetDateTimeValue(xfrInfo.Rows[0]["transTime"]).ToShortDateString());
                labelDestination.Text = ((xfrInfo.Rows[0]["status"].Equals("TI")) ? "From - " : "Destination - ") + fromStore;
                labelTransferNo.Text = xfrInfo.Rows[0]["transTicketNum"].ToString();
            }

            if (mdseTransfer != null && mdseTransfer.Rows.Count > 0)
            {
               
                if (transferDate != ShopDateTime.Instance.ShopDate)
                {
                    labelMessage.Text = INVALIDVOIDMESSAGE;
                    labelMessage.Visible = true;
                    customButtonVoid.Enabled = false;
                }

                decimal amount = 0;
                //if(isTranOut)
                    amount = mdseTransfer.Rows.Cast<DataRow>().Sum(dr => Utilities.GetDecimalValue(dr["pfi_amount"], 0));
                //else
                    //amount= mdseTransfer.Rows.Cast<DataRow>().Sum(dr => Utilities.GetDecimalValue(dr["item_amt"], 0));
                bool otherStatus=false;
                if (!isTranOut)
                otherStatus= this.mdseTransfer.Rows.Cast<DataRow>().Any(
                    drow => drow["status_cd"].ToString() != MerchandiseStatus.PFI.ToString() && 
                    drow["status_cd"].ToString() != "TIC");
                

                labelUserID.Text = mdseTransfer.Rows[0]["CREATEDBY"].ToString();
                labelDate.Text = mdseTransfer.Rows[0]["lastupdatedate"].ToString();
                labelTotal.Text = amount.ToString("c");

                //labelTransferNo.Text = CashlinxDesktopSession.Instance.SelectedTransferNumber;
                BindingSource _bindingSource1 = new BindingSource { DataSource = mdseTransfer };
                //dataGridViewMdse.AutoGenerateColumns = false;
                this.dataGridViewMdse.DataSource = _bindingSource1;
                this.dataGridViewMdse.Columns[mdseDesc.Index].DataPropertyName = "md_desc";
                //if(isTranOut)
                    this.dataGridViewMdse.Columns[cost.Index].DataPropertyName = "pfi_amount";
                //else
                    //this.dataGridViewMdse.Columns[cost.Index].DataPropertyName = "item_amt";
                this.dataGridViewMdse.Columns[cost.Index].DefaultCellStyle.Format = "c";
                //this.dataGridViewMdse.Columns[colQuantity.Index].DataPropertyName = "icnqty";
                this.dataGridViewMdse.AutoGenerateColumns = false;
                
                if (otherStatus)
                {
                    labelMessage.Text = INVALIDSTATUSMESSAGE;
                    labelMessage.Visible = true;
                    customButtonVoid.Enabled = false;

                }

            }

        }

        private string typeOfTransfer()
        {
            string value=null;
            if (this.dataGridViewMdse.Rows[0].Cells["tran_type"] != null && this.dataGridViewMdse.Rows[0].Cells["tran_type"].Value!=null)
            {
               value  = this.dataGridViewMdse.Rows[0].Cells["tran_type"].Value.ToString();
            }
            if (value == null)
            {
                return TRAN_TYPE_INVALID;

            }
            else if ("N".Equals(value))
            {
                return TRAN_TYPE_GUN_SHOP;
            }
            else if ("C".Equals(value))
                return TRAN_TYPE_CAF;
            else if (TRAN_TYPE_SCRAP.Equals(value))
            {
                return TRAN_TYPE_SCRAP;
            }
            else if (TRAN_TYPE_EXCESS.Equals(value))
            {
                return TRAN_TYPE_EXCESS;
            }
            else if (TRAN_TYPE_REFURB.Equals(value))
            {
                return TRAN_TYPE_REFURB;
            }
            else if (TRAN_TYPE_STORE2STORE.Equals(value))
            {
                return TRAN_TYPE_STORE2STORE;
            }
            else
            {
                return TRAN_TYPE_INVALID;
            }
        }

        private void customButtonVoid_Click(object sender, EventArgs e)
        {

            if (comboBoxReason.SelectedIndex >= 0)
            {
                try
                {

                    string errorCode;
                    string errorText;
                    bool retValue = false;
                    string tranType = typeOfTransfer();

                    if (tranType == TRAN_TYPE_GUN_SHOP || tranType == TRAN_TYPE_STORE2STORE || tranType == TRAN_TYPE_CAF || (xfrInfo.Rows[0]["status"].Equals("TI")))
                    {
                        retValue = VoidProcedures.VoidShopAndGunTransfer(GlobalDataAccessor.Instance.OracleDA,
                        labelTransferNo.Text, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        ShopDateTime.Instance.ShopDate.FormatDate(),
                        ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                        GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                        comboBoxReason.Text,
                        customTextBoxComment.Text,
                        out errorCode,
                        out errorText);
                    }
                    else if (TRAN_TYPE_SCRAP.Equals(tranType))
                    {
                        retValue = VoidProcedures.VoidCatcoTransfer(GlobalDataAccessor.Instance.OracleDA,
                        labelTransferNo.Text, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        comboBoxReason.Text, customTextBoxComment.Text,
                        GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                        ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                        PROC_FOR_SCRAP,
                        out errorCode,
                        out errorText);
                    }
                    else if (TRAN_TYPE_REFURB.Equals(tranType))
                    {
                        retValue = VoidProcedures.VoidCatcoTransfer(GlobalDataAccessor.Instance.OracleDA,
                        labelTransferNo.Text, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        comboBoxReason.Text, customTextBoxComment.Text,
                        GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                        ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                        PROC_FOR_REFURB,
                        out errorCode,
                        out errorText);
                    }
                    else if (TRAN_TYPE_EXCESS.Equals(tranType))
                    {
                        retValue = VoidProcedures.VoidCatcoTransfer(GlobalDataAccessor.Instance.OracleDA,
                        labelTransferNo.Text, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        comboBoxReason.Text, customTextBoxComment.Text,
                        GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                        ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                        PROC_FOR_EXCESS,
                        out errorCode,
                        out errorText);
                    }
                    else if (TRAN_TYPE_EXCESS.Equals(tranType))
                    {
                        retValue = VoidProcedures.VoidCatcoTransfer(GlobalDataAccessor.Instance.OracleDA,
                        labelTransferNo.Text, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        comboBoxReason.Text, customTextBoxComment.Text,
                        GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID,
                        ShopDateTime.Instance.ShopDate.FormatDate() + " " + ShopDateTime.Instance.ShopTime.ToString(),
                        PROC_FOR_EXCESS,
                        out errorCode,
                        out errorText);
                    }
                    else
                    {
                        MessageBox.Show("Selected Transaction cannot be voided");
                        Close();
                        return;
                    }


                    if (retValue)
                    {
                        // Below Webservice call needs to be enalbed after BA confirmation
                       /* TransferWebService transferwebService = new TransferWebService();
                        retValue=transferwebService.MDSETransferVoidWS(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, labelTransferNo.Text, out errorText);
                        if (retValue)
                            this.Close();
                        else
                        {
                            MessageBox.Show("Void Web Service call failed.." + errorText);
                            Close();
                        }*/
                        MessageBox.Show("Void Transfer completed successfully");
                        Close();
                    }
                    else
                    {
                        DialogResult dgr;
                        dgr = MessageBox.Show("Void transaction failed. Do you want to retry?", "Void Error", MessageBoxButtons.OKCancel);
                        if (dgr == DialogResult.OK)
                            return;
                        Close();
                    }
                }
                catch (Exception ex)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error when voiding merchandise transfer " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Select the reason for void and submit");
                return;
            }


        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
