using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.Type;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class ShopCashMgr : Form
    {
        public const string PRIMARY = "Primary";
        public const string AUXILIARY = "Auxiliary";
        public NavBox NavControlBox;
        private List<CashDrawerVO> cashDrawers;
        private List<CashDrawerUserVO> availableUsers;
        private List<CashDrawerUserVO> assignedPrimaryUsers;
        private List<CashDrawerUserVO> assignedAuxiliaryUsers;
        //Dictionary with key of cash drawer object
        //  value of PairType<primary cash drawer user object, 
        //    list of auxiliary cash drawer user objects>
        private Dictionary<CashDrawerVO, PairType<CashDrawerUserVO, List<CashDrawerUserVO>>> cashDrawerUserMap;
        private Dictionary<CashDrawerVO, List<CashDrawerUserVO>> cashDrawerDeleteMap;
        private CashDrawerUserVO assignedUserSelection;
        private CashDrawerUserVO availableUserSelection;
        private CashDrawerVO cashDrawerSelection;
        private bool pendingChanges;
        private string selectedCashDrawerID;
        private List<CashDrawerUserVO> deleteMapping;
        private List<string> updatedCashDrawerId;

        public ShopCashMgr()
        {
            InitializeComponent();
            this.NavControlBox = new NavBox();
            this.assignedUserSelection = null;
            this.availableUserSelection = null;
            this.cashDrawerSelection = null;
            this.cashDrawers = new List<CashDrawerVO>();
            this.availableUsers = new List<CashDrawerUserVO>();
            this.assignedPrimaryUsers = new List<CashDrawerUserVO>();
            this.assignedAuxiliaryUsers = new List<CashDrawerUserVO>();
            this.cashDrawerUserMap = new Dictionary<CashDrawerVO, PairType<CashDrawerUserVO, List<CashDrawerUserVO>>>();
            this.cashDrawerDeleteMap = new Dictionary<CashDrawerVO, List<CashDrawerUserVO>>();
            deleteMapping = new List<CashDrawerUserVO>();
            updatedCashDrawerId = new List<string>();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            var dR = MessageBox.Show("Are you sure you want to commit the cash drawer assignment changes?", "Shop Cash Warning Message", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (dR == DialogResult.Yes)
            {
                this.insertCashDrawerAssignmentChanges();
                this.cancelButton.Text = "Close";
            }
            else
            {
                return;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger != null &&
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(
                    Commons.TriggerTypes.SHOPCASHMANAGEMENT, StringComparison.OrdinalIgnoreCase))
                this.NavControlBox.Action = NavBox.NavAction.CANCEL;
            else
            {
                if (string.IsNullOrEmpty(GlobalDataAccessor.Instance.DesktopSession.CashDrawerName))
                {
                    MessageBox.Show("No Cashdrawer assigned. Exiting the application");
                    
                }
                this.Close();
            }
        }

        private void navLeftButton_Click(object sender, EventArgs e)
        {
            this.navButtonSelectionChange(true);
        }

        private void navRightButton_Click(object sender, EventArgs e)
        {
            this.navButtonSelectionChange(false);
        }

        private void insertCashDrawerAssignmentChanges()
        {
            bool success = false;
            List<string> successfulUpdates = new List<string>();
            if (pendingChanges)
            {
                var userId = GlobalDataAccessor.Instance.DesktopSession.FullUserName;
                var transactionDate = ShopDateTime.Instance.ShopDate.ToShortDateString();

                var workStationId = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;
                if (CollectionUtilities.isEmpty(this.cashDrawers))
                    return;
                foreach (var cvo in this.cashDrawers)
                {
                    var mapping = this.cashDrawerUserMap[cvo];
                    if (mapping == null)
                        continue;

                    if (mapping.Left == null)
                        continue;
                    //Check if anything changed with this cashdrawer
                    var cvo1 = cvo;
                    var idChanged = (from id in updatedCashDrawerId
                                     where id == cvo1.Id
                                     select id).FirstOrDefault();
                    if (idChanged == null)
                        continue;
                    string primaryUserId = mapping.Left.Id;
                    string[] auxUsers = null;
                    if (CollectionUtilities.isNotEmpty(mapping.Right))
                    {
                        auxUsers = new string[mapping.Right.Count];
                        int cnt = 0;
                        foreach (var cdUsrVo in mapping.Right)
                        {
                            auxUsers[cnt] = cdUsrVo.Id;
                            cnt++;
                        }
                    }
                    var insRes = DialogResult.Retry;
                    while (insRes == DialogResult.Retry)
                    {
                        string errorCode;
                        string errorText;
                        if (!ShopProcedures.ExecuteUpdateCashDrawerDetails(
                            null, primaryUserId, auxUsers,
                            cvo.Id, cvo.BranchId, workStationId, userId, transactionDate,
                            out errorCode, out errorText))
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                           "ShopProcedures.ExecuteUpdateCashDrawerDetails failed: {0}, {1}", errorCode, errorText);
                            insRes = MessageBox.Show("Could not submit shop cash drawer changes for drawer " + cvo.Name +
                                                     "\nWould you like to retry?",
                                                     "Shop Cash Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        }
                        else
                        {
                            successfulUpdates.Add(cvo.Name);
                            success = true;
                            break;
                        }
                    }
                }
                if (success)
                {
                    StringBuilder cdId = new StringBuilder();
                    foreach (string s in successfulUpdates)
                        cdId.Append(s + ",");
                    MessageBox.Show("Cashdrawer assignment was successful for cashdrawers " + cdId);
                }
                else
                if (updatedCashDrawerId.Count > 0)
                    MessageBox.Show("There were errors assigning users to cash drawers");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isLeft"></param>
        private void navButtonSelectionChange(bool isLeft)
        {
            if (this.cashDrawers.Count <= 1 && this.cashDrawerSelection != null)
            {
                MessageBox.Show("The only drawer available is already selected.", "Shop Cash Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (this.cashDrawers.Count <= 1 && this.cashDrawerSelection == null)
            {
                this.updateCashDrawerSelection(0);
            }
            else if (this.cashDrawers.Count > 1 && this.cashDrawerSelection != null)
            {
                int cdIdx = this.cashDrawers.FindIndex(cvo => (this.cashDrawerSelection == cvo));

                if (cdIdx >= 0)
                {
                    if (isLeft)
                    {
                        --cdIdx;
                        if (cdIdx < 0)
                        {
                            cdIdx = this.cashDrawers.Count - 1;
                        }
                    }
                    else
                    {
                        ++cdIdx;
                        if (cdIdx >= this.cashDrawers.Count)
                        {
                            cdIdx = 0;
                        }
                    }
                    this.updateCashDrawerSelection(cdIdx);
                }
            }
            else if (this.cashDrawers.Count > 1 && this.cashDrawerSelection == null)
            {
                if (isLeft)
                {
                    this.updateCashDrawerSelection(this.cashDrawers.Count - 1);
                }
                else
                {
                    this.updateCashDrawerSelection(0);
                }
            }
            else
            {
                MessageBox.Show("No cash drawers to select.", "Shop Cash Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cdIdx"></param>
        private void updateCashDrawerSelection(int cdIdx)
        {
            if (cdIdx < 0 || cdIdx >= this.cashDrawers.Count)
            {
                return;
            }
            this.cashDrawerSelection = this.cashDrawers[cdIdx];
            int cnt = 0;
            foreach (ListViewItem lvi in this.cashDrawerListView.Items)
            {
                if (cashDrawerSelection != null && lvi.Text == this.cashDrawerSelection.Name)
                {
                    lvi.Selected = true;
                    this.cashDrawerListView_ItemSelectionChanged(this, new ListViewItemSelectionChangedEventArgs(lvi, cnt, true));
                    break;
                }
                /*else if (oldSelection != null && lvi.Text == oldSelection.Name)
                {
                lvi.Selected = false;
                //this.cashDrawerListView_ItemSelectionChanged(this, new ListViewItemSelectionChangedEventArgs(lvi, cnt, false));
                }*/
                cnt++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addUserButton_Click(object sender, EventArgs e)
        {
            if (this.availableUserSelection == null ||
                this.cashDrawerSelection == null)
            {
                if (this.availableUserSelection == null && this.cashDrawerSelection != null)
                {
                    MessageBox.Show("Please select a user from the available user list.", "Shop Cash Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (this.cashDrawerSelection == null && this.assignedUserSelection != null)
                {
                    MessageBox.Show("Please select a cash drawer.", "Shop Cash Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Please select a cash drawer and a user from the available user list.", "Shop Cash Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            //Get the cash drawer user map
            PairType<CashDrawerUserVO, List<CashDrawerUserVO>> mapping =
            this.cashDrawerUserMap[this.cashDrawerSelection];

            if (mapping.Left == null)
            {
                mapping.Left = this.availableUserSelection;
            }
            else
            {
                mapping.Right.Add(this.availableUserSelection);
            }

            updatedCashDrawerId.Add(this.cashDrawerSelection.Id);
            this.cashDrawerUserMap[this.cashDrawerSelection] = mapping;
            this.availableUsers.Remove(this.availableUserSelection);
            this.updateAvailableUserList();
            this.updateAssignedUserList(this.cashDrawerSelection);

            //Clear selection
            this.availableUserSelection = null;
            this.assignedUserSelection = null;

            //Set pending changes           
            this.pendingChanges = true;
            if (this.submitButton.Enabled == false)
            {
                this.submitButton.Enabled = true;
                this.submitButton.Update();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeUserButton_Click(object sender, EventArgs e)
        {
            bool primaryDeleted = false;
            if (this.assignedUserSelection == null ||
                this.cashDrawerSelection == null)
            {
                if (this.assignedUserSelection == null && this.cashDrawerSelection != null)
                {
                    MessageBox.Show("Please select a user from the assigned user list.", "Shop Cash Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (this.cashDrawerSelection == null && this.assignedUserSelection != null)
                {
                    MessageBox.Show("Please select a cash drawer.", "Shop Cash Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Please select a cash drawer and a user from the assigned user list.", "Shop Cash Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            //Get the cash drawer user map
            PairType<CashDrawerUserVO, List<CashDrawerUserVO>> mapping =
            this.cashDrawerUserMap[this.cashDrawerSelection];

            //Get the cash drawer delete map entry
            deleteMapping = this.cashDrawerDeleteMap[this.cashDrawerSelection];

            //Remove the user in either the primary or auxiliary position
            if (mapping != null)
            {

      
                    if (mapping.Left != null && mapping.Left == this.assignedUserSelection)
                    {
                        if (mapping.Right.Count >= 1)
                        {
                            mapping.Left = mapping.Right[0];
                            mapping.Right.Remove(mapping.Right[0]);
                        }
                        else
                        {
                            primaryDeleted = true;
                            mapping.Left.RegisterId = null;
                            mapping.Left.UserName = null;
                            mapping.Left.UserId = 0;
                        }
                    }
                    else
                    {
                        mapping.Right.Remove(this.assignedUserSelection);
                    }
                    if (!deleteMapping.Contains(this.assignedUserSelection) &&
                        !string.IsNullOrEmpty(this.assignedUserSelection.ConnectedId))
                    {
                        deleteMapping.Add(this.assignedUserSelection);
                    }
                    //Update available users
                    this.availableUsers.Add(this.assignedUserSelection);
                    //Update user map
                    this.cashDrawerUserMap[this.cashDrawerSelection] = mapping;
                    if (!updatedCashDrawerId.Any(s => s == this.cashDrawerSelection.Id))
                    {
                        updatedCashDrawerId.Add(this.cashDrawerSelection.Id);
                        
                    }
                    
                    
                    //Update views
                    this.updateAssignedUserList(this.cashDrawerSelection);
                    this.updateAvailableUserList();

                    //Clear selections
                    this.assignedUserSelection = null;
                    this.availableUserSelection = null;
                    if (primaryDeleted)
                    {
                        if (assignedUserListView.Items.Count == 0)
                            this.submitButton.Enabled = false;
                    }
 
                        //Set pending changes           
                        this.pendingChanges = true;
                        if (this.submitButton.Enabled == false)
                        {
                            this.submitButton.Enabled = true;
                            this.submitButton.Update();
                        }

                    
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShopCashMgr_Load(object sender, EventArgs e)
        {
            //Set nav box owner
            this.NavControlBox.Owner = this;

            //Make call stored proc to get current data
            this.retrieveCashDrawerDetails();
            //Call Sp to retrieve safe users list
            this.retrieveSafeUsersList();
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger != null && 
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.SHOPCASHMANAGEMENT, StringComparison.OrdinalIgnoreCase))
            {

                this.buttonDeleteDrawer.Visible = true;
                //Madhu BZ # 247
                //this.buttonWokstationManagement.Visible = true;
            }
            else
            {

                this.buttonDeleteDrawer.Visible = false;
                //Madhu BZ # 247
                //this.buttonWokstationManagement.Visible = false;
            }
            this.buttonDeleteDrawer.Enabled = false;
        }

        private void retrieveSafeUsersList()
        {
            DataTable safeUsersList;
            string errorCode;
            string errorText;
            bool retValue = ShopCashProcedures.GetSafeUsersList(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                GlobalDataAccessor.Instance.DesktopSession,
                out safeUsersList,
                out errorCode,
                out errorText);
            if (!retValue)
                MessageBox.Show("Cannot get safe users list for the store");
            if (safeUsersList != null && safeUsersList.Rows.Count > 0)
            {
                foreach (DataRow dr in safeUsersList.Rows)
                {
                    var fname=Utilities.GetStringValue(dr["fname"]).Trim();
                    var lname=Utilities.GetStringValue(dr["lname"]).Trim();
                    if (!string.IsNullOrEmpty(fname) || !string.IsNullOrEmpty(lname))
                    {
                        this.listViewSafeUsers.Items.Add(string.Format("{0} {1}", fname, lname));
                    }
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool retrieveCashDrawerDetails()
        {
            DesktopSession cds = GlobalDataAccessor.Instance.DesktopSession;
            this.cashDrawers = new List<CashDrawerVO>();
            string storeNumber = cds.CurrentSiteId.StoreNumber;
            DataTable storeCashDrawerList;
            DataTable availCashDrawerUsersList;
            DataTable assignCashDrawerUsersList;
            DataTable auxCashDrawerUsersList;
            string errorCode;
            string errorText;

            if (!ShopProcedures.ExecuteGetCashDrawerDetails(
                GlobalDataAccessor.Instance.OracleDA,
                storeNumber,
                out storeCashDrawerList,
                out availCashDrawerUsersList,
                out assignCashDrawerUsersList,
                out auxCashDrawerUsersList,
                out errorCode,
                out errorText))
            {
                MessageBox.Show("Cannot retrieve cash drawer details for this store");
                return (false);
            }

            //Load cash drawers
            if (storeCashDrawerList != null && storeCashDrawerList.IsInitialized &&
                storeCashDrawerList.Rows != null && storeCashDrawerList.Rows.Count > 0)
            {
                foreach (DataRow dR in storeCashDrawerList.Rows)
                {
                    if (Utilities.GetStringValue(dR["name"]).Contains("SAFE") || Utilities.GetStringValue(dR["name"]).Contains("safe"))
                        continue;
                    CashDrawerVO cDrawer = new CashDrawerVO();

                    cDrawer.Id = Utilities.GetStringValue(dR["id"]);
                    cDrawer.Name = Utilities.GetStringValue(dR["name"]);
                    cDrawer.OpenFlag = Utilities.GetStringValue(dR["openflag"]);
                    cDrawer.RegisterUserId = Utilities.GetStringValue(dR["registeruserid"]);
                    cDrawer.NetName = Utilities.GetStringValue(dR["netname"]);
                    cDrawer.BranchId = Utilities.GetStringValue(dR["branchid"]);

                    this.cashDrawers.Add(cDrawer);
                }
            }

            //Load available cash drawer users
            this.availableUsers=new List<CashDrawerUserVO>();
            if (availCashDrawerUsersList != null && availCashDrawerUsersList.IsInitialized &&
                availCashDrawerUsersList.Rows != null && availCashDrawerUsersList.Rows.Count > 0)
            {
                foreach (DataRow dR in availCashDrawerUsersList.Rows)
                {
                    CashDrawerUserVO cDrawUsr = getCashDrawerUser(dR);
                    this.availableUsers.Add(cDrawUsr);
                }
            }

            //Load assigned cash drawer users
            if (assignCashDrawerUsersList != null && assignCashDrawerUsersList.IsInitialized &&
                assignCashDrawerUsersList.Rows != null && assignCashDrawerUsersList.Rows.Count > 0)
            {
                foreach (DataRow dR in assignCashDrawerUsersList.Rows)
                {
                    CashDrawerUserVO cDrawUsr = getCashDrawerUser(dR);
                    this.assignedPrimaryUsers.Add(cDrawUsr);
                }
            }

            
            //Load auxiliary assigned cash drawer users
            if (auxCashDrawerUsersList != null && auxCashDrawerUsersList.IsInitialized &&
            auxCashDrawerUsersList.Rows != null && auxCashDrawerUsersList.Rows.Count > 0)
            {
            foreach (DataRow dR in auxCashDrawerUsersList.Rows)
            {
            CashDrawerUserVO cDrawUsr = getCashDrawerUser(dR);
            cDrawUsr.ConnectedId = Utilities.GetStringValue(dR["ccduid"]);
            cDrawUsr.RegisterId = Utilities.GetStringValue(dR["cashdrawerid"]);
            this.assignedAuxiliaryUsers.Add(cDrawUsr);
            }
            }

            //Build user map
            this.buildInitalAssignedUserMap();

            //Load data into shop cash mgmt form
            this.loadDataIntoForm();

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        private void buildInitalAssignedUserMap()
        {
            //Clear for initial construction
            this.cashDrawerUserMap.Clear();
            bool lookForPrimaryUsers = true;
            if (assignedPrimaryUsers.Count <= 0)
            {
                //Cannot populate the value components of the map now
                lookForPrimaryUsers = false;
            }
            //Iterate through cash drawers
            foreach (CashDrawerVO curCd in this.cashDrawers)
            {
                if (curCd == null)
                {
                    continue;
                }

                PairType<CashDrawerUserVO,
                List<CashDrawerUserVO>> usrEntry =
                new PairType<CashDrawerUserVO, List<CashDrawerUserVO>>(null, new List<CashDrawerUserVO>());

                List<CashDrawerUserVO> assignedUser = new List<CashDrawerUserVO>();
                if (lookForPrimaryUsers)
                {
                    foreach (CashDrawerUserVO cdUsr in this.assignedPrimaryUsers)
                    {
                        if (cdUsr == null)
                        {
                            continue;
                        }

                        if (curCd.RegisterUserId == cdUsr.Id)
                        {
                            usrEntry.Left = cdUsr;
                            assignedUser.Add(cdUsr);
                            break;
                        }
                    }
                    
                    this.cashDrawerDeleteMap.Add(curCd, assignedUser);
                    foreach (CashDrawerUserVO cdAuxUsr in this.assignedAuxiliaryUsers)
                    {
                        if (cdAuxUsr == null)
                        {
                            continue;
                        }

                        if (cdAuxUsr.RegisterId == curCd.Id)
                        {
                            List<CashDrawerUserVO> usrList = usrEntry.Right;
                            if (usrList == null)
                            {
                                usrList = new List<CashDrawerUserVO>();
                            }
                            usrList.Add(cdAuxUsr);
                            usrEntry.Right = usrList;
                        }
                    }
                    this.cashDrawerUserMap.Add(curCd, usrEntry);
                }
                
                

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void loadDataIntoForm()
        {
            //Add cash drawers
            foreach (CashDrawerVO cdvo in this.cashDrawers)
            {
                this.cashDrawerListView.Items.Add(new ListViewItem(cdvo.Name, 0));
            }

            //Add cash drawer available users
            this.availableUsersListView.View = View.Details;
            this.updateAvailableUserList();
            this.Update();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dR"></param>
        /// <returns></returns>
        private CashDrawerUserVO getCashDrawerUser(DataRow dR)
        {
            if (dR == null)
                return (null);

            CashDrawerUserVO cDrawUsr = new CashDrawerUserVO();

            cDrawUsr.Id = Utilities.GetStringValue(dR["id"]);
            cDrawUsr.UserId = Utilities.GetIntegerValue(dR["userid"]);
            cDrawUsr.UserName = Utilities.GetStringValue(dR["username"]);
            cDrawUsr.BranchId = Utilities.GetStringValue(dR["branchid"]);
            cDrawUsr.NetName = Utilities.GetStringValue(dR["netname"]);

            return (cDrawUsr);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cashDrawerListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //Get the item that has its selection changed
            if (e == null)
                return;

            if (e.IsSelected)
            {
                //Get item
                ListViewItem selectedDrawer = e.Item;

                //Get item text
                string selectedDrawerText = selectedDrawer.Text;

                //Find the cash drawer
                int selectedCashDrawerIdx = this.cashDrawers.FindIndex(
                    cvo =>
                    (cvo.Name.Equals(selectedDrawerText, StringComparison.OrdinalIgnoreCase)));

                if (selectedCashDrawerIdx >= 0)
                {
                    CashDrawerVO selectedCashDrawer = this.cashDrawers[selectedCashDrawerIdx];
                    this.cashDrawerSelection = selectedCashDrawer;
                    this.selectedCashDrawerID = selectedCashDrawer.Id;
                    if (this.cashDrawerUserMap.ContainsKey(selectedCashDrawer))
                    {
                        this.updateAssignedUserList(selectedCashDrawer);
                        this.updateAvailableUserList();
                    }
                    this.buttonDeleteDrawer.Enabled = true;
                }
            }
  
        }

        /// <summary>
        /// 
        /// </summary>
        private void updateAvailableUserList()
        {
            //Add cash drawer available users
            this.availableUsersListView.Items.Clear();
            
            foreach (CashDrawerUserVO cdusrvo in this.availableUsers)
            {
                ListViewItem viewItem = new ListViewItem(cdusrvo.UserName);
                //Madhu - fix for BZ # 124
                //viewItem.SubItems.Add(cdusrvo.UserId.ToString());
                this.availableUsersListView.Items.Add(viewItem);
            }

            this.availableUsersListView.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cdVo"></param>
        private void updateAssignedUserList(CashDrawerVO cdVo)
        {
            if (cdVo == null)
                return;

            this.assignedUserListView.Items.Clear();
            PairType<CashDrawerUserVO, List<CashDrawerUserVO>> mapping =
            this.cashDrawerUserMap[cdVo];
            bool clearExistingList = true;
            if (mapping != null)
            {
                if (mapping.Left != null && mapping.Left.UserId != 0)
                {
                    this.addUserToAssignedUserListView(mapping.Left, true);
                }
                    if (CollectionUtilities.isNotEmpty(mapping.Right))
                    {
                        foreach (CashDrawerUserVO cvo in mapping.Right)
                        {
                            if (cvo == null)
                                continue;
                            if (mapping.Left != null && mapping.Left.UserId == 0)
                                this.addUserToAssignedUserListView(cvo, true);
                            else
                            this.addUserToAssignedUserListView(cvo, false);
                        }
                    }
                
            }
  
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cvo"></param>
        /// <param name="clearFirst"></param>
        private void addUserToAssignedUserListView(CashDrawerUserVO cvo, bool clearFirst)
        {
            if (clearFirst)
            {
                this.assignedUserListView.Items.Clear();
                this.assignedUserListView.Update();
            }

            ListViewItem viewItem = new ListViewItem(cvo.UserName);
            viewItem.SubItems.Add(((clearFirst) ? PRIMARY : AUXILIARY));
            this.assignedUserListView.Items.Add(viewItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void assignedUserListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e == null)
                return;

            if (e.IsSelected && e.Item != null && this.cashDrawerSelection != null)
            {
                ListViewItem assignedLVItem = e.Item;
                AssignUserSelection(assignedLVItem);
                return;
            }
            this.assignedUserSelection = null;
        }

        private void AssignUserSelection(ListViewItem assignedLVItem)
        {
            string userName = assignedLVItem.Text;
            if (!string.IsNullOrEmpty(userName) && assignedLVItem.SubItems != null && assignedLVItem.SubItems.Count > 0)
            {
                string typeName = assignedLVItem.SubItems[1].Text;
                if (!string.IsNullOrEmpty(typeName))
                {
                    PairType<CashDrawerUserVO, List<CashDrawerUserVO>> cDrawerEntry =
                    this.cashDrawerUserMap[this.cashDrawerSelection];
                    if (typeName.Equals(PRIMARY, StringComparison.OrdinalIgnoreCase))
                    {
                        this.assignedUserSelection = cDrawerEntry.Left;
                        return;
                    }
                    if (CollectionUtilities.isNotEmpty(cDrawerEntry.Right))
                    {
                        foreach (CashDrawerUserVO cdUsr in cDrawerEntry.Right)
                        {
                            if (cdUsr == null)
                                continue;
                            if (cdUsr.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                            {
                                this.assignedUserSelection = cdUsr;
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void availableUsersListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e == null)
                return;

            if (e.IsSelected && e.Item != null && this.cashDrawerSelection != null)
            {
                ListViewItem assignedLVItem = e.Item;
                string userName = assignedLVItem.Text;
                if (!string.IsNullOrEmpty(userName) && assignedLVItem.SubItems != null && assignedLVItem.SubItems.Count > 0)
                {
                    string userId = assignedLVItem.SubItems[0].Text;
                    if (!string.IsNullOrEmpty(userId))
                    {
                        int availUserIdx = this.availableUsers.FindIndex(delegate(CashDrawerUserVO cdu)
                                                                         {
                                                                             return (cdu.UserName.Equals(userId, StringComparison.OrdinalIgnoreCase));
                                                                         });

                        if (availUserIdx >= 0)
                        {
                            this.availableUserSelection = this.availableUsers[availUserIdx];
                            return;
                        }
                    }
                }
            }
            this.availableUserSelection = null;
        }

        private void assignedUserListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (e == null)
                return;

            if (this.cashDrawerSelection != null)
            {
                //Madhu - fix for BZ # 124
                if (assignedUserListView.SelectedItems.Count > 0)
                {
                    ListViewItem assignedLVItem = assignedUserListView.SelectedItems[0];
                    AssignUserSelection(assignedLVItem);
                    return;
                }
            }
            this.assignedUserSelection = null;
        }

        private void buttonAddDrawer_Click(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger != null && 
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.SHOPCASHMANAGEMENT, StringComparison.OrdinalIgnoreCase))
            {
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "AddCashDrawer";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                var addCashDrawerFrm = new AddCashDrawer();
                addCashDrawerFrm.ShowDialog(this);
           //Make call stored proc to get current data
            this.retrieveCashDrawerDetails();
                loadDataIntoForm();

            }
        }

        private void buttonDeleteDrawer_Click(object sender, EventArgs e)
        {
            if (selectedCashDrawerID.Length == 0)
            {
                MessageBox.Show("Please select a cash drawer to delete");
                return;
            }
            if (selectedCashDrawerID == GlobalDataAccessor.Instance.DesktopSession.StoreSafeID)
            {
                MessageBox.Show("You cannot delete the safe");
                return;
            }
            try
            {
                decimal deleteStatus;
                string errorCode;
                string errorMesg;
                bool retval = ShopCashProcedures.DeleteCashDrawer(selectedCashDrawerID, GlobalDataAccessor.Instance.DesktopSession, out deleteStatus,
                                                                  out errorCode, out errorMesg);
                if (retval)
                {
                    if (deleteStatus == 1)
                        MessageBox.Show("This drawer cannot be deleted until it is balanced with no cash.");
                    else if (deleteStatus == 2)
                        MessageBox.Show("This drawer must have a balance of zero to delete it from the shop.");
                    else if (deleteStatus == 0)
                    {
                        MessageBox.Show("Successfully deleted cash drawer");
                        //reload the form
                        this.NavControlBox.IsCustom = true;
                        this.NavControlBox.CustomDetail = "Reload";
                        this.NavControlBox.Action = NavBox.NavAction.SUBMIT;
                    }
                }
                else
                    MessageBox.Show("Error when deleting cash drawer " + errorMesg);
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Error when deleting cash drawer", new ApplicationException(ex.Message));
            }
        }

        //Madhu BZ # 247
        /*        private void buttonWokstationManagement_Click(object sender, EventArgs e)
        {
        ManageWorkstations workstationForm = new ManageWorkstations();
        workstationForm.ShowDialog(this);
        }
        */ 

        private void ShopCashMgr_Shown(object sender, EventArgs e)
        {
        }

        private void labelBacktoAssignments_Click(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger != null && 
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.SHOPCASHMANAGEMENT, StringComparison.OrdinalIgnoreCase))
            {
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "VIEWASSIGNMENTS";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                var viewFrm = new ViewCashDrawerAssignments();
                viewFrm.ShowDialog();
                
                
            }

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger != null && 
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger.Equals(Commons.TriggerTypes.SHOPCASHMANAGEMENT, StringComparison.OrdinalIgnoreCase))
            {
                this.NavControlBox.IsCustom = true;
                this.NavControlBox.CustomDetail = "EditCashDrawer";
                this.NavControlBox.Action = NavBox.NavAction.BACKANDSUBMIT;
            }
            else
            {
                AddCashDrawer addCashDrawerFrm = new AddCashDrawer();
                addCashDrawerFrm.ShowDialog(this);
            }

        }
    }
}
