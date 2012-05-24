/********************************************************************
* Namespace: CashlinxDesktop.DesktopForms.Pawn.ShopAdministration
* FileName: AddCashDrawer
* This form is shown to add a new cash drawer to the system
* Sreelatha Rengarajan 2/19/2010 Initial version
*******************************************************************/

using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class AddCashDrawer : CustomBaseForm
    {
        public NavBox NavControlBox;
        public AddCashDrawer()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void customButtonAdd_Click(object sender, EventArgs e)
        {
            this.customLabelInvalidUserMessage.Visible = false;
            if (customTextBoxUserName.isValid)
            {
                try
                {
                    string errorCode;
                    string errorMesg;
                    //Check if the cash drawer name is valid
                    //The cash drawer name should have the format storenumber_username
                    string storeNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                    var userName = string.Empty;
                    if (customTextBoxUserName.isValid)
                        userName = customTextBoxUserName.Text;
                    var cdName = string.Format("{0}_{1}", storeNumber, userName);
                    if (ShopCashProcedures.AddCashDrawer(cdName, string.Empty,
                        customTextBoxUserName.Text, "",
                        "", GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        ShopDateTime.Instance.ShopDate.ToShortDateString(), GlobalDataAccessor.Instance.DesktopSession.FullUserName,
                        GlobalDataAccessor.Instance.DesktopSession,out errorCode, out errorMesg))
                    {
                        MessageBox.Show("Cash drawer " + cdName + " successfully added to the system");
                        if (string.Equals(GlobalDataAccessor.Instance.DesktopSession.FullUserName, userName, StringComparison.Ordinal))
                        {
                            GlobalDataAccessor.Instance.DesktopSession.CashDrawerName = cdName;
                        }
                        if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger != null &&
                            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(
                                Commons.TriggerTypes.SHOPCASHMANAGEMENT, StringComparison.OrdinalIgnoreCase))
                        {
                            this.NavControlBox.IsCustom = true;
                            this.NavControlBox.CustomDetail = "ShopCashMgr";
                            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                        }
                        else
                            this.Close();
                    }
                    else
                    {
                        this.customLabelInvalidUserMessage.Visible = true;
                        this.customLabelInvalidUserMessage.Text = errorCode.Contains("2") ? Commons.GetMessageString("UniqueCashDrawer") : errorMesg;
                            return;


                    }
                }
                catch (Exception ex)
                {
                    BasicExceptionHandler.Instance.AddException("Error when trying to add cash drawer", new ApplicationException(ex.Message));
                }
            }
            else
            {
                MessageBox.Show("Please enter all required fields");
                return;
            }
        }

        private void AddCashDrawer_Load(object sender, EventArgs e)
        {
            //Set nav box owner
            this.NavControlBox.Owner = this;



        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger != null &&
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(
                    Commons.TriggerTypes.SHOPCASHMANAGEMENT, StringComparison.OrdinalIgnoreCase))
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            else
                this.Close();
        }
    }
}
