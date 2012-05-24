using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;

namespace Support.Forms.Customer
{
    public partial class ViewPersonalInformationHistory : Form
    {
        private string ParentFormName;
        private string ThisFormName = "ViewPersonalInformationHistory";
        private Form ownerFrm;
        public NavBox NavControlBox;
        /*__________________________________________________________________________________________*/
        public ViewPersonalInformationHistory()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
            NavControlBox.CustomDetail = ThisFormName;
        }
        /*__________________________________________________________________________________________*/
        private void ViewPersonalInformationHistory_Load(object sender, EventArgs e)
        {
            ownerFrm = this.Owner;
            NavControlBox.Owner = this;

        }
        /*__________________________________________________________________________________________*/
        private void BtnBack_Click(object sender, EventArgs e)
        {
            // TriggerName is assigned at the LooupCustomerFlowExecutor.executorFxn level
            string ParentFormName = GlobalDataAccessor.Instance.DesktopSession.HistorySession.TriggerName;
            
            if (ParentFormName == "ViewCustomerInformationReadOnly")
                this.NavControlBox.CustomDetail = "ViewCustomerInformationReadOnly"; 
            else if (ParentFormName == "UpdateCustomerDetails")
               this.NavControlBox.CustomDetail = "UpdateCustomerDetails"; 

            this.NavControlBox.IsCustom = true;
            this.NavControlBox.Action = NavBox.NavAction.BACK;
        }
    }
}
