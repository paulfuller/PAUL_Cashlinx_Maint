/********************************************************************
* Namespace: CashlinxDesktop.DesktopForms.Pawn.ShopAdministration
* FileName: ViewCashDrawerAssignments
* This form shows the list of all cash drawers for the store and 
* the user assignment to the cash drawer.
* Sreelatha Rengarajan 2/2/2010 Initial version
*******************************************************************/

using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class ViewCashDrawerAssignments : CustomBaseForm
    {
        BindingSource _bindingSource1;
        public NavBox NavControlBox;
        public bool ChangeAssignments
        {
            get;
            set;
        }
        DataTable cashDrawerAssignments = GlobalDataAccessor.Instance.DesktopSession.StoreCashDrawerAssignments;
        public ViewCashDrawerAssignments()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
        }

        private void customButtonClose_Click(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger != null && GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.SHOPCASHMANAGEMENT, StringComparison.OrdinalIgnoreCase))
            {

                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
                this.Close();
        }

 

        private void ViewCashDrawerAssignments_Load(object sender, EventArgs e)
        {
            _bindingSource1 = new BindingSource();
            this.NavControlBox.Owner = this;
            DataTable CDAssignmentsTable = cashDrawerAssignments.Clone();
            //The call to database returns address and phone number in
            //several fields. They have to be merged in order to show on the form
            CDAssignmentsTable.Columns.Add("EmpName");
            CDAssignmentsTable.Columns.Add("safename");
            foreach (DataRow dr in cashDrawerAssignments.Rows)
            {
                CDAssignmentsTable.ImportRow(dr);

            }
            foreach (DataRow drow in CDAssignmentsTable.Rows)
            {
                string empName = drow["FNAME"] + " " + drow["LNAME"];
                drow.SetField("EmpName", empName);
                drow.SetField("safename", GlobalDataAccessor.Instance.DesktopSession.StoreSafeName);
            }
            _bindingSource1.DataSource = CDAssignmentsTable;

            this.customDataGridViewCDAssignments.AutoGenerateColumns = false;
            this.customDataGridViewCDAssignments.DataSource = _bindingSource1;

            this.customDataGridViewCDAssignments.Columns[0].DataPropertyName = "username";
            this.customDataGridViewCDAssignments.Columns[1].DataPropertyName = "EmpName";
            this.customDataGridViewCDAssignments.Columns[2].DataPropertyName = "name";
            //this.customDataGridViewCDAssignments.Columns[3].DataPropertyName = "safename";

            this.customDataGridViewCDAssignments.Focus();
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger != null && GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.SHOPCASHMANAGEMENT, StringComparison.OrdinalIgnoreCase))
                customButtonManageCashDrawers.Visible = true;
            else
                customButtonManageCashDrawers.Visible = false;

        }

        private void customButtonManageCashDrawers_Click(object sender, EventArgs e)
        {
            this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }
    }
}
