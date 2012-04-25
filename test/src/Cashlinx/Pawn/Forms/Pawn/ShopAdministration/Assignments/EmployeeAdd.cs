/**********************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.ShopAdministration.Assignments
 * Class:           EmployeeAdd
 * 
 * Description      Form used to add Visiting Employee to a Manager's Shop.
 * 
 * History
 * David D Wise, Initial Development
 * SR 6/2/2010 Added logic to process the data entered
 **********************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Utility.Logger;

namespace Pawn.Forms.Pawn.ShopAdministration.Assignments
{
    public partial class EmployeeAdd : Form
    {
        public string EmployeeNumber;
        public NavBox NavControlBox;

        public EmployeeAdd()
        {
            InitializeComponent();
            EmployeeNumber = "";
            this.NavControlBox = new NavBox();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            var errorCode = string.Empty;
            var errorText = string.Empty;
            //Get the shop roles that are valid from PWN_BR-097
            List<string> validShopRoles = new List<string>();
            bool retValue = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidShopRoles(GlobalDataAccessor.Instance.CurrentSiteId, out validShopRoles);
            if (retValue)
            {
                if (SecurityProfileProcedures.AddVisitingEmployee(customTextBoxEmployeeNo.Text, "", GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserName,
                    validShopRoles, GlobalDataAccessor.Instance.DesktopSession, out errorCode, out errorText))
                {
                    MessageBox.Show(@"Visiting employee successfully added");
                    Close();
                }
                else
                {
                    //Process the different error codes
                    if (errorCode == "1")
                    {
                        errorText = "The employee number entered is invalid. Please try again.";
                    }
                    if (errorCode == "2")
                    {
                        errorText = "No Valid store id found";
                    }
                    if (errorCode == "4")
                    {
                        errorText = "The user does not have a shop operations role. Please enter another employee number.";
                    }
                    if (errorCode == "6")
                    {
                        errorText = "Employee is already active in the store";
                    }
                    if (errorCode == "7")
                    {
                        errorText = "The employee number entered is not active. Please enter another employee number";
                    }

                    lblMessage.Text = errorText;
                    return;
                }
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Business rule to load valid shop roles failed");
                lblMessage.Text = @"Add Visiting employee failed";
                return;
            }

        }

        private void EmployeeAdd_Load(object sender, EventArgs e)
        {
            this.NavControlBox.Owner = this;
            lblMessage.Text = "";
            
        }
    }
}
