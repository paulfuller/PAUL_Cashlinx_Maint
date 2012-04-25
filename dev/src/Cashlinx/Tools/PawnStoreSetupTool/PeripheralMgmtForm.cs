using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.String;
using Common.Libraries.Utility.Type;

namespace PawnStoreSetupTool
{
    public partial class PeripheralMgmtForm : Form
    {
        private const string COMMIT = "Commit";
        private const string EDIT_TEXT = "Edit";
        private const string REMOVE_TEXT = "Remove";
        private const string ADD_TEXT = "Add";

        public enum PeripheralMgmtMode : uint
        {
            INITIAL = 0,
            VIEW    = 1,
            EDIT    = 2,
            ADD     = 3,
            DELETE  = 4
        }

        public PeripheralTypeVO IncomingType
        {
            set; get;
        }
        public PeripheralVO AddPeripheral
        {
            private set; get;
        }
        public PeripheralVO EditPeripheral
        {
            private set; get;
        }
        public PeripheralVO ViewPeripheral
        {
            private set; get;
        }
        public PeripheralVO DeletePeripheral
        {
            private set; get;
        }

        public WorkstationVO SelectedWorkstation
        {
            private set; get;
        }

        private bool pendingChanges;
        private PeripheralMgmtMode mode;
        private int peripheralSelectedIndex;
        private StoreSetupVO storeData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        public PeripheralMgmtForm(
            ref StoreSetupVO store)
        {
            this.mode = PeripheralMgmtMode.INITIAL;
            this.AddPeripheral = new PeripheralVO();
            this.EditPeripheral = new PeripheralVO();
            this.ViewPeripheral = new PeripheralVO();
            this.DeletePeripheral = new PeripheralVO();
            this.pendingChanges = false;
            this.storeData = null;
            InitializeComponent();
            if (store == null) return;
            this.storeData = store;
            this.peripheralSelectedIndex = -1;
        }


        private void PeripheralMgmtForm_Load(object sender, EventArgs e)
        {
            //this.peripheralPropertyGrid.SelectedObject = this.EditPeripheral;
            bool needUpdate = false;
            if (IncomingType != null)
            {
                //Setup add mode
                this.AddPeripheral.PeriphType = this.IncomingType;
                this.mode = PeripheralMgmtMode.ADD;
                needUpdate = true;
            }
            if (CollectionUtilities.isNotEmpty(storeData.AllPeripherals))
            {
                syncPeripherals();
            }
            if (CollectionUtilities.isNotEmpty(storeData.AllWorkstations))
            {
                syncWorkstations();
            }
            if (needUpdate)
            {
                this.updateButtonState();
                this.updatePropertyViewState();
            }
            else
            {
                this.updateButtonState();
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            DialogResult res = DialogResult.Yes;
            if (this.pendingChanges)
            {
                res = MessageBox.Show("You have changes which will be lost.  Do you want to continue?",
                                "Pawn Setup Alert",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
            }

            if (res == DialogResult.Yes)
            {
                Close();
            }
        }

        private void processPendingChange(bool changeSelection)
        {
            if (!this.pendingChanges) return;
            DialogResult res = DialogResult.Yes;
            if (changeSelection)
            {
                res = MessageBox.Show(
                          "You have changes which will be lost. " + System.Environment.NewLine +
                          "Do you want to save your changes?",
                          PawnStoreSetupForm.SETUPALERT_TXT,
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Exclamation);
            }
            //If mode is edit or add, submit changes
            if (res == DialogResult.Yes)
            {
                PeripheralVO perVo = (this.mode == PeripheralMgmtMode.ADD) ? this.AddPeripheral : this.EditPeripheral;
                bool hasId;
                int idx = getPeripheralIndex(perVo, this.storeData.AllPeripherals, out hasId);
                if (idx == -1)
                {
                    //Add
                    this.storeData.AllPeripherals.Add(perVo);
                    //this.storeData.PeripheralsInserts.Add(perVo);
                }
                else
                {
                    //Edit
                    this.storeData.AllPeripherals[idx] = perVo;
                    //this.storeData.PeripheralsUpdates.Add(perVo);
                }
            }
            this.pendingChanges = false;
        }

        private void peripheralListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkToEnableMapButton();
            //If we have pending changes to a peripheral and the selection is of
            //a different peripheral altogether, warn of a loss in work            
            this.processPendingChange(true);
            if (this.peripheralListView.SelectedIndices != null &&
                this.peripheralListView.SelectedIndices.Count > 0)
            {
                this.mode = PeripheralMgmtMode.VIEW;
                this.peripheralSelectedIndex = this.peripheralListView.SelectedIndices[0];
                this.updateButtonState();
                this.updatePropertyViewState();
            }
        }

        private void peripheralPropertyGrid_Click(object sender, EventArgs e)
        {
            //Do nothing for now
        }

        private void checkToEnableMapButton()
        {
            if (this.workstationListView.SelectedIndices != null &&
                this.peripheralListView.SelectedIndices != null &&
                this.workstationListView.SelectedIndices.Count > 0 &&
                this.peripheralListView.SelectedIndices.Count > 0)
            {
                this.mapPeripheralButton.Enabled = true;                
            }
            else
            {
                this.mapPeripheralButton.Enabled = false;
            }
        }

        private void workstationListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //checkToEnableMapButton();
            if (this.workstationListView.SelectedIndices != null)
            {

                if (this.workstationListView.SelectedItems != null &&
                    this.workstationListView.SelectedItems.Count > 0)
                {
                    var selectedItem = this.workstationListView.SelectedItems[0];
                    if (selectedItem != null && selectedItem.SubItems != null &&
                        selectedItem.SubItems.Count > 1)
                    {
                        var selectedSubItem = selectedItem.SubItems[0];
                        if (!string.IsNullOrEmpty(selectedSubItem.Text))
                        {
                            var fndWk =
                                    this.storeData.AllWorkstations.Find
                                    (
                                            x => x.Name.Equals(selectedSubItem.Text, StringComparison.OrdinalIgnoreCase)
                                    );
                            if (fndWk != null)
                            {
                                this.SelectedWorkstation = fndWk;
                                this.mapPeripheralButton.Enabled = true;
                                var perCntObj = selectedItem.SubItems[1];
                                var perCnt = 0;
                                if (perCntObj != null)
                                {
                                    var perCntStr = selectedItem.SubItems[1].Text;
                                    if (!string.IsNullOrEmpty(perCntStr))
                                    {
                                        Int32.TryParse(perCntStr, out perCnt);
                                    }                                    
                                }
                                this.deMapButton.Enabled = (perCnt > 0);
                            }
                            else
                            {
                                this.SelectedWorkstation = null;
                                this.mapPeripheralButton.Enabled = false;
                                this.deMapButton.Enabled = false;
                            }
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void syncPeripherals()
        {
            if (CollectionUtilities.isEmpty(storeData.AllPeripherals))
            {
                return;
            }

            this.peripheralListView.BeginUpdate();
            this.peripheralListView.Items.Clear();
            foreach(var p in storeData.AllPeripherals)
            {
                if (p == null) continue;

                var sV = new ListViewItem(p.Name);
                //p.UpdatePeripheral();
                if (p.PeriphType != null)
                {
                    sV.SubItems.Add(p.PeriphType.PeripheralTypeName);
                }
                else
                {
                    sV.SubItems.Add("UNKNOWN");
                }
                sV.SubItems.Add(p.IPAddress);
                sV.SubItems.Add(p.Port);

                this.peripheralListView.Items.Add(sV);
            }

            this.peripheralListView.EndUpdate();
        }

        private void syncWorkstations()
        {
            if (CollectionUtilities.isEmpty(this.storeData.AllWorkstations))
            {
                return;
            }

            this.workstationListView.BeginUpdate();
            this.workstationListView.Items.Clear();
            foreach(var w in storeData.AllWorkstations)
            {
                if (w == null || string.IsNullOrEmpty(w.Name)) continue;

                //Lookup workstation peripheral map
                int numPeriphs = 0;
                string name = w.Name;
                if (CollectionUtilities.isNotEmptyContainsKey(this.storeData.PawnWorkstationPeripheralMap, name))
                {
                    var listPer = this.storeData.PawnWorkstationPeripheralMap[name];
                    if (CollectionUtilities.isNotEmpty(listPer))
                    {
                        numPeriphs = listPer.Count;
                    }
                }

                //Create list item
                var wV = new ListViewItem(name);
                wV.SubItems.Add(numPeriphs.ToString());

                this.workstationListView.Items.Add(wV);
            }
            this.workstationListView.EndUpdate();
        }

        /// <summary>
        /// 
        /// </summary>
        private void updateButtonState()
        {
            switch(this.mode)
            {
                case PeripheralMgmtMode.INITIAL:
                    this.addButton.Enabled = true;
                    this.addButton.Text = ADD_TEXT;
                    this.editButton.Enabled = false;
                    this.editButton.Text = EDIT_TEXT;
                    this.removeButton.Enabled = false;
                    break;
                case PeripheralMgmtMode.VIEW:
                    this.addButton.Enabled = true;
                    this.addButton.Text = ADD_TEXT;
                    this.editButton.Enabled = true;
                    this.editButton.Text = EDIT_TEXT;
                    this.removeButton.Enabled = true;
                    this.removeButton.Text = REMOVE_TEXT;
                    break;
                case PeripheralMgmtMode.EDIT:
                    this.addButton.Enabled = false;
                    this.addButton.Text = ADD_TEXT;
                    this.editButton.Enabled = true;
                    this.editButton.Text = COMMIT;
                    this.removeButton.Enabled = true;
                    this.removeButton.Text = REMOVE_TEXT;
                    break;
                case PeripheralMgmtMode.ADD:
                    this.addButton.Enabled = true;
                    this.addButton.Text = COMMIT;
                    this.editButton.Enabled = false;
                    this.editButton.Text = EDIT_TEXT;
                    this.removeButton.Enabled = false;
                    this.removeButton.Text = REMOVE_TEXT;
                    break;
                case PeripheralMgmtMode.DELETE:
                    this.addButton.Enabled = false;
                    this.addButton.Text = ADD_TEXT;
                    this.editButton.Enabled = false;
                    this.editButton.Text = EDIT_TEXT;
                    this.removeButton.Enabled = true;
                    this.removeButton.Text = COMMIT;
                    break;
            }
            this.addButton.Update();
            this.editButton.Update();
            this.removeButton.Update();
        }

        /// <summary>
        /// Find a peripheral based on selected index of the peripheral list view
        /// </summary>
        /// <returns></returns>
        private PeripheralVO findSelectedPeripheral()
        {
            if (this.peripheralSelectedIndex == -1) return (null);
            if (this.peripheralListView.Items != null &&
                this.peripheralListView.Items.Count >= this.peripheralSelectedIndex)
            {
                var perSel = this.peripheralListView.Items[this.peripheralSelectedIndex];
                if (perSel != null && 
                    perSel.SubItems != null && 
                    perSel.SubItems.Count > 1 && 
                    perSel.SubItems[0] != null &&
                    perSel.SubItems[1] != null &&
                    !string.IsNullOrEmpty(perSel.SubItems[0].Text) &&
                    !string.IsNullOrEmpty(perSel.SubItems[1].Text))
                {
                    var fndPeriph = 
                        this.storeData.AllPeripherals.Find(
                            x => 
                                (x.Name.Equals(perSel.SubItems[0].Text, StringComparison.OrdinalIgnoreCase) &&
                                 x.PeriphType.PeripheralTypeName.Equals(perSel.SubItems[1].Text)));
                    return (fndPeriph);
                }
            }
            return (null);
        }

        /// <summary>
        /// 
        /// </summary>
        private void updatePropertyViewState()
        {
            switch(this.mode)
            {
                case PeripheralMgmtMode.INITIAL:
                    this.peripheralPropertyGrid.Enabled = false;
                    break;
                case PeripheralMgmtMode.VIEW:
                    var fndViewPeriph = this.findSelectedPeripheral();
                    if (fndViewPeriph != null)
                    {
                        this.ViewPeripheral = fndViewPeriph;
                        this.peripheralPropertyGrid.SelectedObject = this.ViewPeripheral;
                        //Turn off editing
                        this.peripheralPropertyGrid.Enabled = false; 
                    }
                    break;
                case PeripheralMgmtMode.EDIT:
                    var fndEditPeriph = this.findSelectedPeripheral();
                    if (fndEditPeriph != null)
                    {
                        this.EditPeripheral = fndEditPeriph;
                        this.peripheralPropertyGrid.SelectedObject = this.EditPeripheral;
                        this.peripheralPropertyGrid.Enabled = true;
                    }
                    break;
                case PeripheralMgmtMode.ADD:
                    this.AddPeripheral = new PeripheralVO();
                    this.AddPeripheral.StoreId = this.storeData.StoreInfo.StoreId;
                    this.peripheralPropertyGrid.SelectedObject = this.AddPeripheral;
                    this.peripheralPropertyGrid.Enabled = true;
                    break;
                case PeripheralMgmtMode.DELETE:
                    var fndPeripheral = this.findSelectedPeripheral();
                    if (fndPeripheral != null)
                    {
                        this.peripheralPropertyGrid.SelectedObject = fndPeripheral;
                        this.peripheralPropertyGrid.Enabled = false;
                        this.DeletePeripheral = fndPeripheral;
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pers"></param>
        /// <param name="hasId"></param>
        /// <returns></returns>
        private int getPeripheralIndex(PeripheralVO p, List<PeripheralVO> pers, out bool hasId)
        {
            hasId = false;
            if (p == null || CollectionUtilities.isEmpty(pers))
                return (-1);

            int fndIdIdx = pers.FindIndex(x => x.Id.Equals(p.Id, StringComparison.OrdinalIgnoreCase));
            //int fndIpIdx = (!string.IsNullOrEmpty(p.IPAddress)) 
            //                        ? pers.FindIndex(x => x.IPAddress.Equals(p.IPAddress))
            //                        : -1;
            int fndNmIdx = (!string.IsNullOrEmpty(p.Name)) 
                                    ? pers.FindIndex(x => x.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase)) 
                                    : -1;

            int fndTypeIdx = pers.FindIndex(
                x => x.PeriphType.PeripheralTypeId.Equals(
                    p.PeriphType.PeripheralTypeId, StringComparison.OrdinalIgnoreCase));

            if (fndIdIdx != -1 && fndTypeIdx != -1)
            {
                hasId = true;
                return (fndIdIdx);
            }

            return (-1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pers"></param>
        /// <param name="hasId"></param>
        /// <returns></returns>
        private int getPeripheralIndex(PeripheralVO p, List<PairType<string, PeripheralVO>> pers, out bool hasId)
        {
            hasId = false;
            if (p == null || CollectionUtilities.isEmpty(pers)) return (-1);

            int fndIdIdx = pers.FindIndex(x => x.Right.Id.Equals(p.Id, StringComparison.OrdinalIgnoreCase));
            //int fndIpIdx = (!string.IsNullOrEmpty(p.IPAddress))
            //                        ? pers.FindIndex(x => x.Right.IPAddress.Equals(p.IPAddress))
            //                        : -1;
            int fndNmIdx = (!string.IsNullOrEmpty(p.Name))
                                    ? pers.FindIndex(x => x.Right.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase))
                                    : -1;

            int fndTypeIdx = pers.FindIndex(
                x => x.Right.PeriphType.PeripheralTypeId.Equals(
                    p.PeriphType.PeripheralTypeId, StringComparison.OrdinalIgnoreCase));

            if (fndIdIdx != -1 && fndTypeIdx != -1)
            {
                hasId = true;
                return (fndIdIdx);
            }

            return (-1);
        }

        private bool isPeripheralValid(PeripheralVO pVo, out string errMsg)
        {
            errMsg = string.Empty;
            //Verify the peripheral being added prior to adding it
            bool fndErrors = false;
            var errMsgSB = new StringBuilder();
            if (pVo == null)
            {
                errMsgSB.AppendLine("Peripheral object must be valid");
                fndErrors = true;
            }
            else
            {
                if (string.IsNullOrEmpty(pVo.Name) || pVo.Name.Trim().Length == 0)
                {
                    errMsgSB.AppendLine("Peripheral Name is empty or null");
                    fndErrors = true;
                }
                

                if (pVo.LogicalType == PeripheralVO.PeripheralType.UNKNOWN)
                {
                    errMsgSB.AppendLine("Peripheral logical type must be specified");
                    fndErrors = true;
                }

                if (pVo.LogicalType != PeripheralVO.PeripheralType.INTERMEC)
                {
                    if (!StringUtilities.ISIPv4Address(pVo.IPAddress))
                    {
                        errMsgSB.AppendLine("Peripheral IP Address must be valid");
                        fndErrors = true;
                    }
                    int ptNum;
                    if (string.IsNullOrEmpty(pVo.Port) || !Int32.TryParse(pVo.Port, out ptNum))
                    {
                        errMsgSB.AppendLine("Peripheral Port number must be valid");
                        fndErrors = true;
                    }
                }
            }
            if (fndErrors)
            {
                errMsg = errMsgSB.ToString();
            }
            return (!fndErrors);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            if (addButton.Text == COMMIT)
            {
                //Verify the peripheral being added prior to adding it
                string errMsg;
                if (!this.isPeripheralValid(this.AddPeripheral, out errMsg))
                {
                    MessageBox.Show("Cannot add the peripheral due to the following errors:" + Environment.NewLine +
                                    errMsg, PawnStoreSetupForm.SETUPALERT_TXT);
                    this.AddPeripheral = null;
                    this.mode = PeripheralMgmtMode.INITIAL;
                    this.syncPeripherals();
                    this.updateButtonState();
                    this.updatePropertyViewState();
                    this.pendingChanges = false;
                    return;
                }
                //this.AddPeripheral.UpdatePeripheral();
                this.AddPeripheral.SetPeripheralTypeNames(this.storeData.AllPeripheralTypes);
                this.AddPeripheral.CalculatePeripheralTypeFromLogicalType(storeData.AllPeripheralTypes);
                bool hasId;
                int fndIdx = this.getPeripheralIndex(this.AddPeripheral,
                                                     this.storeData.AllPeripherals,
                                                     out hasId);
                if (fndIdx == -1)
                {
                    this.storeData.AllPeripherals.Add(this.AddPeripheral);
                    /*int fndInsIdx = this.getPeripheralIndex(this.AddPeripheral,
                                                            this.storeData.PeripheralsInserts,
                                                            out hasId);
                    if (fndInsIdx == -1)
                    {
                        this.storeData.PeripheralsInserts.Add(this.AddPeripheral);
                    }
                    else
                    {
                        MessageBox.Show(
                                "This peripheral already exists in the add list.  Please try again.",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                    }*/
                }
                else
                {
                    MessageBox.Show("This peripheral has already been added.",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                }

                //Clear view
                this.mode = PeripheralMgmtMode.INITIAL;
                this.syncPeripherals();
                this.updateButtonState();
                this.updatePropertyViewState();
                this.pendingChanges = false;
            }
            else
            {
                //Set mode to add
                processPendingChange(false);
                this.mode = PeripheralMgmtMode.ADD;
                this.updateButtonState();
                this.updatePropertyViewState();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (editButton.Text == COMMIT)
            {
                string errMsg;
                if (!this.isPeripheralValid(this.EditPeripheral, out errMsg))
                {
                    MessageBox.Show("Cannot edit the peripheral due to the following errors:" + Environment.NewLine +
                                    errMsg, PawnStoreSetupForm.SETUPALERT_TXT);
                    this.EditPeripheral = null;
                    this.mode = PeripheralMgmtMode.INITIAL;
                    this.syncPeripherals();
                    this.updateButtonState();
                    this.updatePropertyViewState();
                    this.pendingChanges = false;
                    return;
                }
                //this.EditPeripheral.UpdatePeripheral();
                this.EditPeripheral.CalculatePeripheralTypeFromTypeId(storeData.AllPeripheralTypes);
                bool hasId;
                int fndIdx = getPeripheralIndex(this.EditPeripheral,
                                                this.storeData.AllPeripherals,
                                                out hasId);
                //Update the peripheral in the all peripheral list
                if (fndIdx != -1)
                {
                    this.storeData.AllPeripherals[fndIdx] = this.EditPeripheral;
                    /*int fndUpdIdx = getPeripheralIndex(this.EditPeripheral,
                                                       this.storeData.PeripheralsUpdates,
                                                       out hasId);
                    if (fndUpdIdx == -1)
                    {
                        this.storeData.PeripheralsUpdates.Add(this.EditPeripheral);
                    }
                    else
                    {
                        MessageBox.Show("You have already made a change to this peripheral.",
                                        PawnStoreSetupForm.SETUPALERT_TXT);
                    }*/
                }
                else
                {
                    MessageBox.Show(
                            "Cannot find the original peripheral that you are editing now.  Please try again.",
                            PawnStoreSetupForm.SETUPALERT_TXT);
                }

                //Clear view
                this.mode = PeripheralMgmtMode.INITIAL;
                this.syncPeripherals();
                this.updateButtonState();
                this.updatePropertyViewState();
                this.pendingChanges = false;
            }
            else
            {
                //Set mode to edit
                processPendingChange(false);
                this.mode = PeripheralMgmtMode.EDIT;
                this.updateButtonState();
                this.updatePropertyViewState();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (removeButton.Text == COMMIT)
            {
                //Remove the peripheral (move it from the all list to the delete list)
                //this.storeData.PeripheralsDeletes.Add(this.DeletePeripheral);
                //Clear selection
                this.storeData.AllPeripherals.RemoveAll(x => x.Id.Equals(this.DeletePeripheral.Id, StringComparison.OrdinalIgnoreCase));
                if (CollectionUtilities.isNotEmpty(this.storeData.PawnWorkstationPeripheralMap))
                {
                    //Remove from any workstation mapped to this peripheral
                    foreach (var wkst in this.storeData.PawnWorkstationPeripheralMap.Keys)
                    {
                        if (wkst == null)
                            continue;
                        var perMapList = this.storeData.PawnWorkstationPeripheralMap[wkst];
                        if (CollectionUtilities.isNotEmpty(perMapList))
                        {
                            perMapList.RemoveAll(x => x.Right.Id.Equals(this.DeletePeripheral.Id));
                        }
                    }
                }


                //Reset list
                this.mode = PeripheralMgmtMode.INITIAL;
                this.syncPeripherals();
                this.syncWorkstations();
                this.updateButtonState();
                this.updatePropertyViewState();
                this.pendingChanges = false;
            }
            else
            {
                //Setup for removal
                processPendingChange(false);
                this.mode = PeripheralMgmtMode.DELETE;
                this.updateButtonState();
                this.updatePropertyViewState();
            }
        }

        private void mapButton_Click(object sender, EventArgs e)
        {
            //Check if a peripheral is selected
            if (this.peripheralSelectedIndex != -1 && 
                this.SelectedWorkstation != null &&
                !this.pendingChanges &&
                (this.mode == PeripheralMgmtMode.INITIAL ||
                 this.mode == PeripheralMgmtMode.VIEW ||
                 this.mode == PeripheralMgmtMode.EDIT))
            {
                List<PairType<string, PeripheralVO>> mappedPers;
                bool exists = false;
                string wkName = this.SelectedWorkstation.Name;
                if (CollectionUtilities.isNotEmptyContainsKey(
                    this.storeData.PawnWorkstationPeripheralMap,
                    wkName))
                {
                    mappedPers =
                            this.storeData.PawnWorkstationPeripheralMap[wkName];
                    exists = true;
                }
                else
                {
                    mappedPers =
                            new List<PairType<string, PeripheralVO>>();
                }
                PeripheralVO selPer = this.findSelectedPeripheral();
                if (selPer != null)
                {
                    bool hasId;
                    int perIdx = getPeripheralIndex(selPer,
                                                    mappedPers,
                                                    out hasId);
                    if (perIdx == -1)
                    {
                        mappedPers.Add(new PairType<string, PeripheralVO>("0", selPer));
                    }
                    else
                    {
                        MessageBox.Show(
                                "That peripheral <=> workstation mapping already exists.",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Cannot locate the selected peripheral.",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                    return;
                }

                if (exists)
                {
                    this.storeData.PawnWorkstationPeripheralMap[wkName] = mappedPers;
                }
                else
                {
                    this.storeData.PawnWorkstationPeripheralMap.Add(wkName,
                        mappedPers);
                }
                this.pendingChanges = false;
                //Resync workstations
                this.syncWorkstations();
            }
        }

        private void peripheralPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.pendingChanges = true;
        }

        private void deMapButton_Click(object sender, EventArgs e)
        {
            var wkst = this.SelectedWorkstation;
            if (wkst != null && !string.IsNullOrEmpty(wkst.Name) &&
                CollectionUtilities.isNotEmptyContainsKey(
                    this.storeData.PawnWorkstationPeripheralMap, wkst.Name))
            {
                //Create mapping entry data table
                var wkName = wkst.Name;
                var mapEntDt = new DataTable();
                mapEntDt.Columns.Add("Workstation Name");
                mapEntDt.Columns.Add("Peripheral Name");
                mapEntDt.Columns.Add("Peripheral Type");
                var perList = this.storeData.PawnWorkstationPeripheralMap[wkName];                
                if (CollectionUtilities.isNotEmpty(perList))
                {
                    var wksName = wkName;
                    foreach (var per in perList)
                    {
                        if (per == null)
                            continue;
                        var dR = mapEntDt.NewRow();
                        dR["Workstation Name"] = wksName;
                        dR["Peripheral Name"] = per.Right.Name;
                        dR["Peripheral Type"] = per.Right.PeriphType.PeripheralTypeName;
                        mapEntDt.Rows.Add(dR);
                    }
                }


                if (mapEntDt.Rows.Count > 0)
                {
                    var rowCount = mapEntDt.Rows.Count;
                    var sumFm = new SummaryForm(mapEntDt,
                                                "Please remove incorrect mappings (NOTE: Use the Delete key):",
                                                false);
                    sumFm.ShowDialog(this);
                    //Resolve any changes made
                    if (sumFm.SummaryData.Rows.Count < rowCount)
                    {                        
                        var sumDt = sumFm.SummaryData;
                        if (sumDt.Rows.Count > 0)
                        {
                            var removePeripherals = new List<PeripheralVO>();
                            foreach (var per in perList)
                            {
                                if (per == null) continue;
                                bool fndPer = false;
                                foreach (DataRow dR in sumDt.Rows)
                                {
                                    string perName = Utilities.GetStringValue(dR["Peripheral Name"]);
                                    if (perName.Equals(per.Right.Name, StringComparison.OrdinalIgnoreCase))
                                    {
                                        fndPer = true;
                                        break;
                                    }
                                }
                                if (!fndPer)
                                {
                                    removePeripherals.Add(per.Right);
                                }
                            }

                            if (CollectionUtilities.isNotEmpty(removePeripherals))
                            {
                                foreach(var remPer in removePeripherals)
                                {
                                    PeripheralVO per = remPer;
                                    perList.RemoveAll(x => x.Right.Name.Equals(per.Name, StringComparison.OrdinalIgnoreCase));
                                }
                            }
                        }
                        else
                        {
                            perList.Clear();
                        }
                    }
                    //Update it back to the peripheral map
                    this.pendingChanges = false;
                    //Resync workstations
                    this.syncWorkstations();
                }
            }
        }
    }
}
