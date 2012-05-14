/**********************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Pawn.ShopAdministration.Assignments
 * Class:           SelectEmployee
 * 
 * Description      Form used to select/add an Employee and edit their Shop Profile.
 * 
 * History
 * David D Wise, Initial Development
 * SR 5/27/2010 Added logic to perform actions on the loaded data
 **********************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.ShopAdministration.Assignments
{
    public partial class SelectEmployee : Form
    {
        private string _StoreNumber;
        private bool _EnableDeleteVisitingEmployee;
        private bool _EnableAddEmployeeButton;
        private UserVO _UserVO;
        private DataTable _ShopVisitingEmployees;
        public NavBox NavControlBox;

        public SelectEmployee()
        {
            InitializeComponent();
            _StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            this.NavControlBox = new NavBox();

        }

 
        private void PopulateShopEmployees()
        {
            gvShopEmployees.Rows.Clear();
            string sFilter = "homestore = '" + _StoreNumber + "'";

            DataRow[] dataRows = _ShopVisitingEmployees.Select(sFilter);
            foreach (DataRow dataRow in dataRows)
            {
                string strUserId=Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.USERID], "");
                if (strUserId == GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID)
                    continue;
                int gvNewRowIdx = gvShopEmployees.Rows.Add();
                DataGridViewRow myRow = gvShopEmployees.Rows[gvNewRowIdx];
                myRow.Cells[colShopEmployeeNumber.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.EMPLOYEENUMBER]);
                myRow.Cells[colShopEmpName.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.NAME]);
                myRow.Cells[colShopLastName.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.LASTNAME]);
                myRow.Cells[colShopFirstName.Name].Value =  Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.FIRSTNAME]);
                myRow.Cells[colShopEmployeeRole.Name].Value =  Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.EMPLOYEEROLE]);
                myRow.Cells[colShopHomeShop.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.HOMESTORE]);
                myRow.Cells[colShopProfile.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.PROFILES]);
                myRow.Cells[colShopProfiles.Name].Value = Utilities.GetIntegerValue(dataRow[(int)EmployeeProfileRecord.NOOFPROFILES], 0);
                myRow.Cells[colShopUserId.Name].Value = strUserId;
            }
        }

        private void PopulateVisitingEmployees()
        {
            gvVisitingEmployees.Columns[colVisitingDelete.Name].Visible = _EnableDeleteVisitingEmployee;

            gvVisitingEmployees.Rows.Clear();
            string sFilter = "homestore <> '" + _StoreNumber + "'";

            DataRow[] dataRows = _ShopVisitingEmployees.Select(sFilter);
            foreach (DataRow dataRow in dataRows)
            {
                string strUserId = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.USERID], "");
                if (strUserId == GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile.UserID)
                    continue;

                int gvNewRowIdx = gvVisitingEmployees.Rows.Add();

                DataGridViewRow myRow = gvVisitingEmployees.Rows[gvNewRowIdx];
                myRow.Cells[colVisitingEmployeeNumber.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.EMPLOYEENUMBER]);
                myRow.Cells[colVisitingEmpName.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.NAME]);
                myRow.Cells[colVisitingLastName.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.LASTNAME]);
                myRow.Cells[colVisitingFirstName.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.FIRSTNAME]);
                myRow.Cells[colVisitingEmployeeRole.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.EMPLOYEEROLE]);
                myRow.Cells[colVisitingHomeShop.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.HOMESTORE]);
                myRow.Cells[colVisitingProfile.Name].Value = Utilities.GetStringValue(dataRow[(int)EmployeeProfileRecord.PROFILES]);
                myRow.Cells[colVisitingProfiles.Name].Value = Utilities.GetIntegerValue(dataRow[(int)EmployeeProfileRecord.NOOFPROFILES], 0);
                myRow.Cells[colVisitingUserId.Name].Value = strUserId;
                
            }
        }

        private void gvShopEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            errorLabel.Text = "";
            if (gvShopEmployees != null)
            {
                if (e.ColumnIndex == gvShopEmployees.Columns[colShopEmployeeNumber.Name].Index)
                {
                    int iRowIdx = e.RowIndex;
                    //int iEmployeeNumber = Utilities.GetIntegerValue(gvShopEmployees.Rows[iRowIdx].Cells[colShopEmployeeNumber.Name].Value);
                    string strEmpName = Utilities.GetStringValue(gvShopEmployees.Rows[iRowIdx].Cells[colShopEmpName.Name].Value);
                    string strEmpRole=Utilities.GetStringValue(gvShopEmployees.Rows[iRowIdx].Cells[colShopEmployeeRole.Name].Value);
                    ROLEHIERARCHYLEVEL roleLevel=_UserVO.UserRole.CheckRoleHierarchy(strEmpRole);
                    if (roleLevel == ROLEHIERARCHYLEVEL.GREATER)
                        GoToSecurityProfile(_StoreNumber, strEmpName);
                    else
                        MessageBox.Show(@"You cannot change the security profile for a user in an equal or higher role");

                }
            }
        }

        private void gvVisitingEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            errorLabel.Text = "";
            if (e.ColumnIndex == gvVisitingEmployees.Columns[colVisitingDelete.Name].Index
                ||e.ColumnIndex == gvVisitingEmployees.Columns[colVisitingEmployeeNumber.Name].Index)
            {
                int iRowIdx = e.RowIndex;
                string strUserId = Utilities.GetStringValue(gvVisitingEmployees.Rows[iRowIdx].Cells[colVisitingUserId.Name].Value);
                string strEmpName = Utilities.GetStringValue(gvVisitingEmployees.Rows[iRowIdx].Cells[colVisitingEmpName.Name].Value);
                string strEmpRole = Utilities.GetStringValue(gvVisitingEmployees.Rows[iRowIdx].Cells[colVisitingEmployeeRole.Name].Value);
                if (e.ColumnIndex == gvVisitingEmployees.Columns[colVisitingDelete.Name].Index)
                {
                    if (MessageBox.Show(@"Are you sure you want to delete this employee profile?", "User Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var sErrorCode = string.Empty;
                        var sErrorText = string.Empty;
                        bool retVal = SecurityProfileProcedures.DeleteEmployeeProfile(strUserId, "", _StoreNumber, _UserVO.UserName, GlobalDataAccessor.Instance.DesktopSession, out sErrorCode, out sErrorText);
                        if (!retVal)
                        {
                            errorLabel.Text = sErrorText;
                        }
                        else
                        {
                            //Delete the employee row
                            DataRow dr=_ShopVisitingEmployees.Rows.Find(strUserId);
                            _ShopVisitingEmployees.Rows.Remove(dr);
                            PopulateVisitingEmployees();

                        }
                    }
                    else
                        return;
                }
                else
                {
                    ROLEHIERARCHYLEVEL roleLevel = _UserVO.UserRole.CheckRoleHierarchy(strEmpRole);
                    if (roleLevel == ROLEHIERARCHYLEVEL.GREATER)
                        GoToSecurityProfile(_StoreNumber, strEmpName);
                    else
                        MessageBox.Show(@"You cannot change the security profile for a user in an equal or higher role");
                   
                }
            }
        }

        private void GoToSecurityProfile(string sShopID, string sEmpName)
        {
            errorLabel.Text = string.Empty;
            string errorCode;
            string errorMesg;
            UserVO selectedEmployeeData;
            try
            {

                if (SecurityProfileProcedures.GetUserSecurityProfile(sEmpName, sShopID, "", "Y", GlobalDataAccessor.Instance.DesktopSession, out selectedEmployeeData, out errorCode, out errorMesg))
                {
                    GlobalDataAccessor.Instance.DesktopSession.SelectedUserProfile = selectedEmployeeData;
                    NavControlBox.IsCustom = true;
                    NavControlBox.CustomDetail = "EmployeeDetails";
                    NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
                }
                else
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error in loading security profile for the selected employee " + errorCode + " " + errorMesg);
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error in loading security profile for the selected employee " + ex.Message);
                BasicExceptionHandler.Instance.AddException("Security profile could not be loaded for " + sEmpName, new ApplicationException());
            }

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            this.errorLabel.Text = string.Empty;
            var myForm = new EmployeeAdd();
            myForm.ShowDialog();
            GetEmployeeProfiles();

        }

        private void SelectEmployee_Load(object sender, EventArgs e)
        {
            this.errorLabel.Text = string.Empty;
            this.NavControlBox.Owner = this;
            _UserVO = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
            //If logged in user is a shop user allow add and delete else disable add and delete
            if (_UserVO.ShopLevelUser)
            {
                _EnableDeleteVisitingEmployee = true;
                _EnableAddEmployeeButton = true;

            }
            else
            {
                _EnableDeleteVisitingEmployee = false;
                _EnableAddEmployeeButton = false;

            }

            shopNumberLabel.Text = _StoreNumber;
            GetEmployeeProfiles();

            customButtonAddEmployee.Visible = _EnableAddEmployeeButton;
        }

        private bool RemoveDuplicateEmployees(string colName)
        {
            if (string.IsNullOrEmpty(colName))
            {
                //Must assume that duplicates exist
                return (false);
            }
            var rowsToRemove = new List<DataRow>();
            var existingEmps = new Dictionary<string, int>();

            try
            {

                //Construct count bucket and duplicate list
                foreach (DataRow dr in _ShopVisitingEmployees.Rows)
                {
                    if (dr == null) continue;
                    var usId = dr["userid"];
                    if (usId == null) continue;
                    var usIdStr = usId.ToString();
                    if (CollectionUtilities.isNotEmptyContainsKey(existingEmps, usIdStr))
                    {
                        existingEmps[usIdStr]++;

                    }
                    else
                    {
                        existingEmps.Add(usIdStr, 1);
                    }

                    //See what the updated count looks like
                    if (existingEmps[usIdStr] > 1)
                    {
                        rowsToRemove.Add(dr);
                    }
                }

                //Now that we've created the removal list, we need to execute the removals
                if (CollectionUtilities.isNotEmpty(rowsToRemove))
                {
                    foreach (var dr in rowsToRemove)
                    {
                        if (dr == null) continue;
                        _ShopVisitingEmployees.Rows.Remove(dr);
                    }
                }
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Could not filter employee list. Exception: {0}", eX.Message);
                }
                //Must assume at this point that some duplicates exist and could not be removed
                return (false);
            }
            //Can safely assume here that any duplicates were removed successfully
            return (true);
        }

        private void GetEmployeeProfiles()
        {
            string sErrorCode;
            string sErrorText;
            try
            {
                if (SecurityProfileProcedures.ExecuteGetEmployeeProfileHeader(_UserVO.UserName
                                            , null
                                            , _StoreNumber
                                            , out _ShopVisitingEmployees
                                            , out sErrorCode
                                            , out sErrorText))
                {
                    if (sErrorCode == "0")
                    {
                        _ShopVisitingEmployees.DefaultView.Sort = "employeenumber";
                        //If we have filtered the duplicates, assign the primary key
                        if (RemoveDuplicateEmployees("userid"))
                        {
                            var key = new DataColumn[1];
                            key[0] = _ShopVisitingEmployees.Columns["userid"];
                            _ShopVisitingEmployees.PrimaryKey = key;
                        }
                        else
                        {
                            var res = MessageBox.Show(
                                "There are duplicate employee entries in this shop.  Would you still like to view the employee list?",
                                "Employee Profile Request Warning", MessageBoxButtons.YesNo);
                            if (res == DialogResult.No)
                            {
                                NavControlBox.Action = NavBox.NavAction.CANCEL;
                                return;
                            }
                        }
                        //Allow the shop employees to be shown
                        PopulateShopEmployees();
                        PopulateVisitingEmployees();
                    }
                    else
                    {
                        errorLabel.Text = sErrorText;
                    }
                }
                else
                {
                    errorLabel.Text = sErrorText;
                }
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Error getting employee profile header" + ex.Message);
                BasicExceptionHandler.Instance.AddException("Error getting employee profile header", new ApplicationException());

            }

        }
    }
}
