/**********************************************************************************
* Namespace:       CashlinxDesktop.DesktopForms.Pawn.ShopAdministration.Assignments
* Class:           SecurityProfile
* 
* Description      Form used to manage Limits and Resources to a Employee's Profile
*                  for a given Shop he is assigned (visiting or home) to
* 
* History
* David D Wise, Initial Development(Form layout)
* SR 6/1/2010 Added logic for loading data and processing data in the form
* 
**********************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.ShopAdministration.Assignments
{
    public partial class SecurityProfile : Form
    {
        public NavBox NavControlBox;

        private List<string> loggedinUserOnlyResources;
        private List<string> selectedUserResources;
        private List<string> loggedinUserAllResources;
        private bool _Change_Resources;
        private bool _Change_Limits;
        private string _ShopID;
        private UserVO _UserVO;
        private List<string> _userStores;
        private List<LimitsVO> loggedinUserLimits;
        private List<LimitsVO> selectedUserLimits;
        private UserVO loggedInUser;
        private List<string> selectedUserOnlyResources;
        private List<string> addedResource;
        private List<string> removedResource;
        private bool isFormValid;
        private StringBuilder errorMessages;
        private decimal maxLoanStateLimit = 0.0m;
        private Timer timer;

        private const string LIMITEXCEEDEDMESSAGE = "You cannot set a limit that exceeds your own. Please try again";

        public SecurityProfile()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
            timer = new Timer();
            timer.Tick += timer_Tick;

            timer.Interval = 2000;

            //Start the timer
            timer.Start();
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (!isFormValid)
            {
                MessageBox.Show(@"Fix the errors in the form and submit. " + System.Environment.NewLine + errorMessages.ToString());
                return;
            }
            //Process added resources
            List<string> resourcesToAdd = new List<string>();
            foreach (string resName in addedResource)
            {
                string name = resName;
                var resData = (from resource in loggedInUser.UserResources
                               where resource.ResourceName == name
                               select resource).FirstOrDefault();
                if (resData != null)
                {
                    resourcesToAdd.Add(resData.ResourceID);
                    resourcesToAdd.Add("Y");
                    resourcesToAdd.Add(resData.ResourceMask.ToString());
                }
            }
            List<string> resourcesToRemove = new List<string>();
            foreach (string resName in removedResource)
            {
                string name = resName;
                var resData = (from resource in _UserVO.UserResources
                               where resource.ResourceName == name
                               select resource).FirstOrDefault();
                if (resData != null)
                {
                    resourcesToRemove.Add(resData.ResourceID);
                    resourcesToRemove.Add("N");
                    resourcesToRemove.Add(resData.ResourceMask.ToString());
                }
            }

            List<string> resourcesToModify = new List<string>();
            resourcesToModify.AddRange(resourcesToAdd);
            resourcesToModify.AddRange(resourcesToRemove);
            List<string> limitsToModify = new List<string>();
            foreach (DataGridViewRow dgvr in gvLimits.Rows)
            {
                string editedValue = dgvr.Cells[2].EditedFormattedValue.ToString();
                if (editedValue.Contains("$"))
                    editedValue = editedValue.Remove(0, 1);
                decimal limitData = Utilities.GetDecimalValue(editedValue, 0);

                int prodoffid = Utilities.GetIntegerValue(dgvr.Cells[0].Value, 0);
                int roleLimitId = Utilities.GetIntegerValue(dgvr.Cells[3].Value, 0);
                
                var userLimitData = (from limit in selectedUserLimits
                                     where limit.ProdOfferingId == prodoffid &&
                                           string.IsNullOrEmpty(limit.StoreID)
                                     select limit).FirstOrDefault();
                if (userLimitData != null)
                {
                    if (userLimitData.Limit != limitData)
                    {
                        limitsToModify.Add(userLimitData.ProdOfferingId.ToString());
                        limitsToModify.Add(limitData.ToString());
                    }
                }
                else
                {
                    limitsToModify.Add(prodoffid.ToString());
                    if (roleLimitId == 0)
                        limitsToModify.Add(limitData.ToString());
                    else
                        limitsToModify.Add(limitData == maxLoanStateLimit ? "-1" : limitData.ToString());
                }
            }

            string errorCode;
            string errorText;
            //Call update employee profile SP
            GlobalDataAccessor.Instance.beginTransactionBlock();
            bool retValue = SecurityProfileProcedures.UpdateEmployeeProfile(_UserVO.UserID, "", _ShopID, loggedInUser.UserName, resourcesToModify, limitsToModify, GlobalDataAccessor.Instance.DesktopSession, out errorCode, out errorText);
            if (retValue)
            {
                MessageBox.Show(@"Successfully updated user profile");
                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.COMMIT);
            }
            else
            {
                MessageBox.Show(errorText);
                GlobalDataAccessor.Instance.endTransactionBlock(EndTransactionType.ROLLBACK);
            }
            NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            //reset the assigned and available resources
            //and the limits
            _Change_Limits = false;
            _Change_Resources = false;
            updateButtons(_Change_Resources, _Change_Limits);
            GetDataForSelectedUserInStore();
        }

        #region Resources
        private void resourcesAssignButton_Click(object sender, EventArgs e)
        {
            if (resourcesAvailableListBox.SelectedItems.Count > 0)
            {
                for (int i = 0; i < resourcesAvailableListBox.Items.Count; i++)
                {
                    if (resourcesAvailableListBox.SelectedItems.Contains(resourcesAvailableListBox.Items[i]))
                    {
                        listBoxAssigned.Items.Add(resourcesAvailableListBox.Items[i]);
                        //If the resource that was added has a limit pertaining to it then
                        //show that limit in the limits area
                        string selectedResource = resourcesAvailableListBox.Items[i].ToString();
                        var limitData = (from limit in selectedUserLimits
                                         where limit.ResourceName == selectedResource &&
                                               string.IsNullOrEmpty(limit.StoreID)
                                         select limit).FirstOrDefault();
                        if (limitData == null)
                        {
                            limitData = (from limit2 in loggedinUserLimits
                                         where limit2.ResourceName == selectedResource &&
                                               (string.IsNullOrEmpty(limit2.StoreID) || limit2.StoreNumber == _ShopID)
                                         select limit2).FirstOrDefault();
                        }
                        if (limitData != null)
                        {
                            var dgvr = new DataGridViewRow();
                            var cell1 = new DataGridViewTextBoxCell
                                        {
                                            Value = limitData.ProdOfferingId.ToString()
                                        };
                            dgvr.Cells.Insert(0, cell1);
                            var cell2 = new DataGridViewTextBoxCell
                                        {
                                            Value = limitData.ResourceName.ToString()
                                        };
                            dgvr.Cells.Insert(1, cell2);
                            var cell3 = new DataGridViewTextBoxCell
                                        {
                                            Value = limitData.Limit
                                        };
                            dgvr.Cells.Insert(2, cell3);
                            var cell4 = new DataGridViewTextBoxCell
                                        {
                                            Value = limitData.RoleLimitId
                                        };
                            dgvr.Cells.Insert(3, cell4);
                            gvLimits.Rows.Add(dgvr);
                        }
                        //TO DO change based on limit type
                        gvLimits.Columns[2].DefaultCellStyle.Format = "C";
                        addedResource.Add(selectedResource);
                        resourcesAvailableListBox.Items.Remove(resourcesAvailableListBox.Items[i]);
                        i--;
                    }
                }

                _Change_Resources = true;
                
                updateButtons(_Change_Resources, _Change_Limits);
            }
        }

        private void resourcesUnAssignButton_Click(object sender, EventArgs e)
        {
            if (listBoxAssigned.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listBoxAssigned.Items.Count; i++)
                {
                    if (listBoxAssigned.SelectedItems.Contains(listBoxAssigned.Items[i]))
                    {
                        resourcesAvailableListBox.Items.Add(listBoxAssigned.Items[i]);
                        string selectedResource = listBoxAssigned.Items[i].ToString();
                        removedResource.Add(selectedResource);

                        //If the resource that was added has a limit pertaining to it then
                        //remove that limit in the limits area
                        if (gvLimits.Rows.Count > 0)
                        {
                            int idx = -1;
                            foreach (DataGridViewRow dgvr in gvLimits.Rows)
                            {
                                if (dgvr.Cells[1].Value.ToString() == selectedResource)
                                {
                                    idx = dgvr.Index;
                                    break;
                                }
                            }
                            if (idx > -1)
                                gvLimits.Rows.RemoveAt(idx);
                        }

                        listBoxAssigned.Items.Remove(listBoxAssigned.Items[i]);
                        i--;
                    }
                }

                _Change_Resources = true;
                updateButtons(_Change_Resources, _Change_Limits);
            }
        }

        #endregion

        private void updateButtons(bool bResourcesChange, bool limitsChanged)
        {
            limitsErrorLabel.Text = "";
            resourcesErrorLabel.Text = "";

            customButtonReset.Visible = bResourcesChange || limitsChanged;
            customButtonSubmit.Visible = bResourcesChange || limitsChanged;

            resourcesAvailableListBox.Enabled = resourcesAvailableListBox.Items.Count > 0;
            resourcesAssignButton.Enabled = resourcesAvailableListBox.Items.Count > 0;
            listBoxAssigned.Enabled = listBoxAssigned.Items.Count > 0;
            resourcesUnAssignButton.Enabled = listBoxAssigned.Items.Count > 0;
        }

        private void SecurityProfile_Load(object sender, EventArgs e)
        {
            try
            {
                this.NavControlBox.Owner = this;
                limitsErrorLabel.Text = "";
                resourcesErrorLabel.Text = "";
                addedResource = new List<string>();
                removedResource = new List<string>();
                errorMessages = new StringBuilder();
                isFormValid = true;

                _UserVO = GlobalDataAccessor.Instance.DesktopSession.SelectedUserProfile;

                employeeNumberLabel.Text = !string.IsNullOrEmpty(_UserVO.EmployeeNumber) ? _UserVO.EmployeeNumber : "";
                employeeRoleLabel.Text = _UserVO.UserFirstName
                                         + " "
                                         + _UserVO.UserLastName
                                         + " "
                                         + _UserVO.UserRole.RoleName;
                homeShopIDLabel.Text = _UserVO.FacNumber ?? "";
                limitsLastUpdated.Text = _UserVO.LastUpdatedDate.ToShortDateString();

                _userStores = _UserVO.ProfileStores;
                _ShopID = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                if (_userStores.Count > 0 && _userStores[0] != null)
                {
                    foreach (string s in _userStores)
                    {
                        shopIDComboBox.Items.Add(s);
                    }
                }
                //TODO: Fix THIS!!!! Should be a static method call!!!!!!!!!
                new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxLoanLimit(CashlinxDesktopSession.Instance.CurrentSiteId, out maxLoanStateLimit);

                loggedInUser = GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile;
                loggedinUserLimits = new List<LimitsVO>();
                //Populate the limits list
                //Get the limits of the logged in user
                loggedinUserLimits = SecurityProfileProcedures.GetListOfLimits(loggedInUser, _ShopID);
                List<ServiceOffering> serviceOfferings = GlobalDataAccessor.Instance.DesktopSession.ServiceOfferings;
                foreach (ResourceVO rVo in loggedInUser.UserResources)
                {
                    ResourceVO vo = rVo;
                    var sOffering = (from servOffering in serviceOfferings
                                     where servOffering.ServiceOfferingID == vo.ResourceID
                                     select servOffering).FirstOrDefault();
                    if (sOffering.ServiceOfferingID != null)
                    {
                        //Check if that limit is there in loggedinuserlimits
                        var limitData = (from limit in loggedinUserLimits
                                         where limit.ServiceOffering == sOffering.ServiceOfferingID
                                         select limit).FirstOrDefault();
                        if (limitData == null)
                        {
                            LimitsVO newLimit = new LimitsVO();
                            newLimit.ServiceOffering = sOffering.ServiceOfferingID;
                            newLimit.ProdOfferingId = sOffering.ProdOffering;
                            newLimit.ResourceName = vo.ResourceName;
                            newLimit.RoleLimitId = 0;
                            newLimit.StoreID = string.Empty;

                            if (vo.ResourceName == Commons.GetResourceName("NEWPAWNLOAN"))
                            {
                                //Get the limit from business rule
                                decimal maxLoanLimit = 0.0m;
                                if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxLoanLimit(GlobalDataAccessor.Instance.CurrentSiteId, out maxLoanLimit))
                                    newLimit.Limit = maxLoanLimit;
                            }
                            if (vo.ResourceName == Commons.GetResourceName("CUSTOMERBUY"))
                            {
                                //Set the limit to the max
                                newLimit.Limit = 99999;
                            }
                            loggedinUserLimits.Add(newLimit);
                        }
                    }
                }

                shopIDComboBox.SelectedIndex = _userStores.IndexOf(_ShopID);
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot load security profile");
                BasicExceptionHandler.Instance.AddException("Security profile could not be loaded " + ex.Message, new ApplicationException(ex.ToString()));
            }
        }

        private void GetDataForSelectedUserInStore()
        {
            gvLimits.Rows.Clear();
            loggedinUserOnlyResources = new List<string>();
            
            try
            {
                GetResourcesDataForSelectedUserInStore();
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Resource data could not be loaded for the user in selected store" + ex.Message);
            }
            try
            {
                GetLimitsDataForSelectedUserInStore();
            }
            catch (Exception ex)
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Limits data could not be loaded for the user in selected store" + ex.Message);
            }
        }

        private void GetLimitsDataForSelectedUserInStore()
        {
            selectedUserLimits = new List<LimitsVO>();
 
            //Get the limits of the selected user
            selectedUserLimits = SecurityProfileProcedures.GetListOfLimits(_UserVO, _ShopID);

            //check if all the resources which belong to the selected user has
            //associated limit and if yes check if its there in selecteduserlimits
            //and if not get the limit from the business rule
            List<ServiceOffering> serviceOfferings = GlobalDataAccessor.Instance.DesktopSession.ServiceOfferings;
            foreach (ResourceVO rVo in _UserVO.UserResources)
            {
                ResourceVO vo = rVo;
                if (vo.Assigned == "N")
                    continue;
                var sOffering = (from servOffering in serviceOfferings
                                 where servOffering.ServiceOfferingID == vo.ResourceID 
                                 select servOffering).FirstOrDefault();
                if (sOffering.ServiceOfferingID != null)
                {
                    //Check if that limit is there in selecteduserlimits
                    var limitData = (from limit in loggedinUserLimits
                                     where limit.ServiceOffering == sOffering.ServiceOfferingID
                                     select limit).FirstOrDefault();
                    if (limitData == null)
                    {
                        LimitsVO newLimit = new LimitsVO();
                        newLimit.ServiceOffering = sOffering.ServiceOfferingID;
                        newLimit.ProdOfferingId = sOffering.ProdOffering;
                        newLimit.ResourceName = vo.ResourceName;
                        newLimit.RoleLimitId = 0;
                        newLimit.StoreID = string.Empty;

                        if (vo.ResourceName == Commons.GetResourceName("NEWPAWNLOAN"))
                        {
                            //Get the limit from business rule
                            decimal maxLoanLimit = 0.0m;
                            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetMaxLoanLimit(GlobalDataAccessor.Instance.CurrentSiteId, out maxLoanLimit))
                                newLimit.Limit = maxLoanLimit;
                        }
                        if (vo.ResourceName == Commons.GetResourceName("CUSTOMERBUY"))
                        {
                            //Set the limit to the max
                            newLimit.Limit = 99999;
                        }
                        selectedUserLimits.Add(newLimit);
                    }
                }
            }

            this.gvLimits.AutoGenerateColumns = false;
            foreach (LimitsVO limitData in selectedUserLimits)
            {
                if (limitData.StoreNumber == string.Empty)
                {
                    //If the limit being shown is the default limit
                    //Make sure the corresponding resource is still part
                    //of the user's resources list
                    var resName = (from resource in selectedUserResources
                                   where resource == limitData.ResourceName
                                   select resource).FirstOrDefault();
                    if (resName == null)
                        continue;
                }
                DataGridViewRow dgvr = new DataGridViewRow();
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                cell1.Value = limitData.ProdOfferingId.ToString();
                dgvr.Cells.Insert(0, cell1);
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = limitData.ResourceName.ToString();
                dgvr.Cells.Insert(1, cell2);
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                cell3.Value = limitData.Limit;
                dgvr.Cells.Insert(2, cell3);
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                cell4.Value = limitData.RoleLimitId;
                dgvr.Cells.Insert(3, cell4);
                gvLimits.Rows.Add(dgvr);
            }

            //Set the format for the limit field to currency
            //TO DO - when limit type attribute is added the format will change accordingly
            gvLimits.Columns[2].DefaultCellStyle.Format = "C";
            populateLimitsDataGrid();
        }

        private void GetResourcesDataForSelectedUserInStore()
        {
           listBoxAssigned.Items.Clear();
            resourcesAvailableListBox.Items.Clear();
            selectedUserResources = SecurityProfileProcedures.GetListOfResources(_UserVO, _ShopID);
            loggedinUserAllResources = SecurityProfileProcedures.GetListOfResources(loggedInUser, _ShopID);
            selectedUserOnlyResources = new List<string>();
            List<string> userResources = new List<string>();
            //Populate the assigned resources list
            foreach (string s in selectedUserResources)
                userResources.Add(s);

            foreach (string s in userResources)
            {
                string s1 = s;
                string resourceName = (from actorResource in loggedinUserAllResources
                                       where actorResource == s1
                                       select actorResource).FirstOrDefault();
                if (string.IsNullOrEmpty(resourceName))
                    selectedUserOnlyResources.Add(s1);
            }
            if (loggedinUserAllResources.Count > selectedUserResources.Count)
            {
                foreach (string s in loggedinUserAllResources)
                {
                    string s1 = s;
                    string resourceName = (from actorResource in userResources
                                           where actorResource == s1
                                           select actorResource).FirstOrDefault();
                    if (string.IsNullOrEmpty(resourceName))
                        loggedinUserOnlyResources.Add(s1);
                }
            }

            userResources.Sort();
            loggedinUserOnlyResources.Sort();
            selectedUserOnlyResources.Sort();

            listBoxAssigned.Items.AddRange(userResources.ToArray());
            //customListBoxAssigned.Items.AddRange(selectedUserOnlyResources.ToArray());

            //populate the available resources list
            List<string> availableResources = loggedinUserOnlyResources;
            resourcesAvailableListBox.Items.AddRange(availableResources.ToArray());
            updateButtons(_Change_Resources, _Change_Limits);
            if (_ShopID != GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber)
            {
                resourcesAvailableListBox.Enabled = false;
                listBoxAssigned.Enabled = false;
            }
            else
            {
                resourcesAvailableListBox.Enabled = true;
                listBoxAssigned.Enabled = true;
            }
        }

        private void populateLimitsDataGrid()
        {
            foreach (DataGridViewRow dgvr in gvLimits.Rows)
            {
                string prodLimitName = dgvr.Cells[1].Value.ToString();
                var actorLimitName = string.Empty;
                if (loggedinUserLimits != null)
                {
                    actorLimitName = (from res in loggedinUserAllResources
                                      where res == prodLimitName
                                      select res).FirstOrDefault();
                }

                //If the limit belongs to the selected user but not the logged in user
                //then show the row in read only mode

                if (string.IsNullOrEmpty(actorLimitName))
                {
                    dgvr.DefaultCellStyle.BackColor = Color.Gray;
                    dgvr.DefaultCellStyle.SelectionBackColor = Color.Gray;
                    dgvr.ReadOnly = true;
                }
            }
            if (_ShopID != GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber)
            {
                gvLimits.Enabled = false;
            }
            else
                gvLimits.Enabled = true;
        }

        private void assignedListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            
            e.DrawBackground();
            Brush textBrush = SystemBrushes.ControlText;

            string strToShow = listBoxAssigned.Items[e.Index].ToString();
            var resourcename = from s in selectedUserOnlyResources where s == strToShow select s;
            if (resourcename.Count() > 0)
            {
                textBrush = Brushes.Gray;
            }

            Brush backColorBrush = new SolidBrush(listBoxAssigned.BackColor);
            
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds);
            }
            else
                e.Graphics.FillRectangle(backColorBrush, e.Bounds);

            e.Graphics.DrawString(strToShow, listBoxAssigned.Font, textBrush, e.Bounds);
            
        }

        private void assignedListBox1_Click(object sender, EventArgs e)
        {
            //If the selected resource is a resource assigned only to the
            //selected user then the actor cannot act on it so disable selection.
            string selectedResource = listBoxAssigned.Items[listBoxAssigned.SelectedIndex].ToString();
            var resourcename = from s in selectedUserOnlyResources where s == selectedResource select s;
            if (resourcename.Count() > 0)
            {
                listBoxAssigned.SetSelected(listBoxAssigned.SelectedIndex, false);
            }
            else
            {
                listBoxAssigned.SetSelected(listBoxAssigned.SelectedIndex, true);
            }
        }

 
        private void shopIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ShopID = shopIDComboBox.Items[shopIDComboBox.SelectedIndex].ToString();
            GetDataForSelectedUserInStore();
        }

        private void gvLimits_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //Call business rules procedures to see if the limit needs to be checked against the
            //logged in user's limit
            if (gvLimits.Rows.Count > 0)
            {
                errorMessages = new StringBuilder();
                isFormValid = true;
                if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetValidateUserLimit(GlobalDataAccessor.Instance.CurrentSiteId))
                {
                    this.customButtonReset.Visible = true;
                    string editedValue = gvLimits.Rows[e.RowIndex].Cells[2].EditedFormattedValue.ToString();
                    if (editedValue.Contains("$"))
                        editedValue = editedValue.Remove(0, 1);
                    decimal limitValue = Utilities.GetDecimalValue(editedValue, 0);
                    if (limitValue == 0)
                    {
                        MessageBox.Show(@"Invalid amount entered");
                        isFormValid = false;
                        return;
                    }
                    decimal prodLimit = Utilities.GetDecimalValue(editedValue);
                    int prodOffId = Utilities.GetIntegerValue(gvLimits.Rows[e.RowIndex].Cells[0].Value.ToString());
                    decimal loggedinUserProdLimit = 0.0M;
                    //Get the limit for the logged in user for the same product
                    var loggedinUserLimit = (from limit in loggedinUserLimits
                                             where limit.ProdOfferingId == prodOffId 
                                             select limit).FirstOrDefault();
                    if (loggedinUserLimit != null)
                    {
                        loggedinUserProdLimit = loggedinUserLimit.Limit;
                        if (loggedinUserProdLimit < prodLimit)
                        {
                            errorMessages.Append(LIMITEXCEEDEDMESSAGE);
                            MessageBox.Show(LIMITEXCEEDEDMESSAGE);
                            isFormValid = false;
                            return;
                        }
                    }
                    _Change_Limits = true;
                    updateButtons(_Change_Resources, _Change_Limits);

                    gvLimits.Rows[e.RowIndex].Cells[2].Value = string.Format("{0:c}", Utilities.GetDecimalValue(editedValue, 0));
                }
            }
        }

        protected override System.Boolean ProcessDialogKey(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.ActiveControl.Tag.ToString() == "Limits")
                {
                    _Change_Limits = true;
                    updateButtons(_Change_Resources, _Change_Limits);
                }
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }


        void timer_Tick(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            timer = sender as Timer;
            if (timer == null)
                return;
            listBoxAssigned.Refresh();
        }

 


    }
}
