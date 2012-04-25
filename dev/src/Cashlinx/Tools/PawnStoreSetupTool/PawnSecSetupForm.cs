using System;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Database.DataAccessLayer;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;

namespace PawnStoreSetupTool
{
    public partial class PawnSecSetupForm : Form
    {
        public const string GHOSTSCRIPT_EXE_FILENAME = "gswin32.exe";
        public const string ACROREAD_EXE_FILENAME = "AcroRd32.exe";
        public const string EXE_FILE_FILTER = "EXE files (*.exe)|*.exe";
        public const string CASHAM_DOMAIN = "casham.com";
        public const string FILEPATH_SLASH = @"\";
        public const decimal MIN_TERMINAL_NUMBER = 1m;
        public const decimal MAX_TERMINAL_NUMBER = 99m;
        public const decimal MIN_TRACE_LEVEL = 0m;
        public const decimal MAX_TRACE_LEVEL = 5m;
        private LogLevel logLevel;


        public LogLevel LogLevelAttrib
        {
            set
            {
                this.logLevel = value;
            }
            get
            {
                return (this.logLevel);
            }
        }

        public bool GlobalChangesMade
        {
            private set; get;
        }

        public bool StoreChangesMade
        {
            private set; get;
        }

        public bool MachineChangesMade
        {
            private set; get;
        }

        private bool initialCreation;
        public bool InitialCreation
        {
            get
            {
                return (this.initialCreation);
            }
        }

        private EncryptedConfigContainer pSec;
        public EncryptedConfigContainer EncryptedConfig
        {
            get
            {
                return (this.pSec);
            }
        }

        private string machineName;
        public string MachineName
        {
            get
            {
                return (this.machineName);
            }
        }

        private string ipAddress;
        public string IPAddress
        {
            get
            {
                return (this.ipAddress);
            }
        }

        private string macAddress;
        public string MACAddress
        {
            get
            {
                return (this.macAddress);
            }
        }

        /*public bool CreatingWorkstation
        {
            private set; get;
        }*/

        private StoreSetupVO storeData;
        private SecurityAccessor pawnSecAccessor;
        private PawnSecVO pawnSecVo;
        private string storeNumber;
        private DataAccessTools dAPawnSec;

        public PawnSecSetupForm(
            DataAccessTools dA,
            SecurityAccessor pSecAccess,
            StoreSetupVO sConfig,
            PawnSecVO pSecVo,
            EncryptedConfigContainer pEnc,            
            string clientKey,
            string storeNum, 
            bool initCreate)
        {
            InitializeComponent();
            this.logLevel = LogLevel.DEBUG;
            this.storeNumber = storeNum;
            GlobalChangesMade = false;
            StoreChangesMade = false;
            MachineChangesMade = false;
            initialCreation = initCreate;            
            this.storeData = sConfig;
            pawnSecAccessor = pSecAccess;
            pawnSecVo = pSecVo;
            //CreatingWorkstation = false;
            this.dAPawnSec = dA;

            ResourceProperties resourceProperties = new ResourceProperties();
            resourceProperties.PrivateKey = clientKey;

            if (initCreate)
            {
                if (!pawnSecAccessor.InitializeSecurityData(resourceProperties, clientKey, storeNum, pSecVo,
                        out this.machineName,
                        out this.ipAddress,
                        out this.macAddress))
                {
                    throw new ApplicationException(
                            "Cannot initialize encryption portion of PAWNSEC™.");
                }
                this.pSec = pawnSecAccessor.EncryptConfig;
            }
            else
            {
                this.pSec = pEnc;
            }
        }        

        private void PawnSecSetupForm_Load(object sender, EventArgs e)
        {
            string[] arr = Enum.GetNames(logLevel.GetType());
            this.logLevelComboBox.BeginUpdate();
            this.logLevelComboBox.Items.Clear();
            foreach(var logLev in arr)
            {
                this.logLevelComboBox.Items.Add(logLev);
            }
            this.logLevelComboBox.EndUpdate();
            //this.storeStateTextBox.Text = this.storeData.StoreInfo.State;
            //this.pawnsecStoreNumberTextBox.Text = this.storeData.StoreInfo.StoreNumber;
            this.storeLabel.Text = this.storeData.StoreInfo.StoreNumber;
            this.store2Label.Text = this.storeData.StoreInfo.StoreNumber;
            this.stateLabel.Text = this.storeData.StoreInfo.State;
            this.aliasLabel.Text = this.storeData.StoreInfo.Alias;
            this.timeZoneLabel.Text = this.storeData.StoreInfo.LocalTimeZone;

            //Update store configuration
            this.storeData.PawnSecData.StoreNumber = this.storeData.StoreInfo.StoreNumber;
            var pStore = this.storeData.PawnSecData.GetStore();

            if (!this.initialCreation)
            {
                syncMachineNames();
                updateFormBasedOnGlobalConfig();
            }
            //else
            //{
            //    CreatingWorkstation = true;                
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PawnSecVO.PawnSecStoreVO getStoreInformation()
        {
            //Update store configuration
            var pStore = this.storeData.PawnSecData.GetStore();
            if (pStore == null)
            {
                //ulong nextAppId = 1;
                //this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOAPPVER_ID, ref nextAppId);
                pStore = new PawnSecVO.PawnSecStoreVO(this.appVersionTextBox.Text ?? "1.0.0", "0")
                {
                     StoreSite = this.storeData.StoreInfo
                };
                //this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOCFG_ID, ref nextAppId);
                //pStore.StoreConfiguration.Id = nextAppId;
                //this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOSITINF_ID, ref nextAppId);
                //pStore.StoreSiteId = nextAppId;
                this.storeData.PawnSecData.Stores.Add(pStore);
            }
            return (pStore);
        }

        private void syncMachineNames()
        {
            if (CollectionUtilities.isNotEmpty(this.storeData.PawnSecData.ClientMachines))
            {
                this.machineNameComboBox.BeginUpdate();
                this.machineNameComboBox.Items.Clear();
                foreach (var mac in this.storeData.PawnSecData.ClientMachines)
                {
                    if (mac == null) continue;
                    this.machineNameComboBox.Items.Add(mac.Machine.MachineName);
                }
                this.machineNameComboBox.EndUpdate();
            }
        }

        private void logLevelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(logLevelComboBox.SelectedText)) return;
            this.logLevel = (LogLevel)Enum.Parse(this.logLevel.GetType(), logLevelComboBox.SelectedText, false);
        }

        private void primaryPawnSecSetupDoneButton_Click(object sender, EventArgs e)
        {
            //Update store configuration
            var pStore = this.getStoreInformation();
            pStore.StoreConfiguration.DayOffset = (int)this.dayOffsetNumericUpDown.Value;
            pStore.StoreConfiguration.MonthOffset = (int)this.monthOffsetNumericUpDown.Value;
            pStore.StoreConfiguration.FetchSizeMultiplier = (ulong)this.dataFetchSizeNumericUpDown.Value;
            pStore.StoreConfiguration.MetalsFile = pmetalFileTextBox.Text;
            pStore.StoreConfiguration.StonesFile = stonesFileTextBox.Text;

            //pStore.

            //if (!CreatingWorkstation)
            //{
            if (string.IsNullOrEmpty(machineNameComboBox.Text))
            {
                return;
            }
            //Use workstation object matching selected from combo box
            int firstPerIdx = machineNameComboBox.Text.IndexOf(".");
            string trimmedMachineName;
            if (firstPerIdx >= 1)
            {
                trimmedMachineName = machineNameComboBox.Text.Substring(0, firstPerIdx);
            }
            else
            {
                trimmedMachineName = machineNameComboBox.Text;
            }
            bool newMach = false,
                 newMap = false;

            var mach =
                    this.storeData.PawnSecData.ClientMachines.Find(
                            x => x.Machine.MachineName.Equals(machineNameComboBox.Text, StringComparison.OrdinalIgnoreCase));
            if (mach == null)
            {
                MessageBox.Show("Could not find the machine selected in the combo box.",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
            var machMapping =
                    this.storeData.PawnSecData.ClientStoreMapList.Find(
                            x => x.ClientRegistryId == mach.Machine.ClientId);
            if (machMapping == null)
            {
                MessageBox.Show("Could not find the mapping belonging to the machine selected in the combo box.",
                                PawnStoreSetupForm.SETUPALERT_TXT);
                return;
            }
                

            if (!string.IsNullOrEmpty(this.ipAddressTextBox.Text))
            {
                mach.Machine.IPAddress = this.ipAddressTextBox.Text;
                if (!StringUtilities.ISIPv4Address(mach.Machine.IPAddress))
                {
                    MessageBox.Show(
                            "The IPv4 address entered is not properly formatted.  Must be of the form: XXX.XXX.XXX.XXX " +
                            Environment.NewLine +
                            " (NOTE: The individual IPv4 components need to be between 0 and 255 and can be 1 to 3 digits");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(this.macAddressTextBox.Text))
            {
                mach.Machine.MACAddress = this.macAddressTextBox.Text;
                if (!StringUtilities.IsMACAddress(mach.Machine.MACAddress))
                {
                    MessageBox.Show(
                            "The MAC address entered is not properly formatted. Must be of the form: XX:XX:XX:XX:XX:XX",
                            PawnStoreSetupForm.SETUPALERT_TXT);
                    return;
                }
            }

            if (this.adobeOverrideCheckBox.Checked &&
                !string.IsNullOrEmpty(this.adobeReaderOverrideTextBox.Text))
            {
                mach.Machine.AdobeOverride = this.adobeReaderOverrideTextBox.Text;
            }

            if (this.ghostScriptOverrideCheckBox.Checked &&
                !string.IsNullOrEmpty(this.ghostScriptOverrideTextBox.Text))
            {
                mach.Machine.GhostOverride = this.ghostScriptOverrideTextBox.Text;
            }


                /*ulong uNexId = 0;
                bool updatedMapping = false;
                if (mach.Machine.ClientId <= 0)
                {
                    this.storeData.PawnSecData.NextIdSet.GetNextIds(
                            PawnSecVO.PawnSecNextIdVO.SELECTOR.CLIREG_ID, ref uNexId);
                    updatedMapping = true;
                }*/
                //mach.Machine.ClientId = (mach.Machine.ClientId <= 0) ? uNexId : mach.Machine.ClientId;
            mach.Machine.IsAllowed = this.isAllowedCheckBox.Checked;
            mach.StoreMachine.PrintEnabled = this.printingAllowedCheckBox.Checked;
            mach.StoreMachine.LogLevel = this.logLevelComboBox.Text;
            mach.StoreMachine.TraceLevel = (int)this.traceLevelNumericUpDown.Value;
            mach.StoreMachine.TerminalNumber = (int)this.terminalNumberNumericUpDown.Value;
            mach.StoreMachine.WorkstationId = trimmedMachineName;
            mach.Machine.WorkstationName = trimmedMachineName;

                #region NOT NEEDED
                /*if (mach.StoreMachine.Id <= 0)
                {
                    this.storeData.PawnSecData.NextIdSet.GetNextIds(
                            PawnSecVO.PawnSecNextIdVO.SELECTOR.STOCLICFG_ID, ref uNexId);
                    updatedMapping = true;
                }
                mach.StoreMachine.Id = (mach.StoreMachine.Id <= 0) ? uNexId : mach.StoreMachine.Id;
                if (newMach)
                {
                    this.storeData.PawnSecData.ClientMachines.Add(mach);
                }
                if (updatedMapping)
                {
                    var cMapVo = machMapping;
                    if (newMap)
                    {
                        this.storeData.PawnSecData.NextIdSet.GetNextIds(
                                PawnSecVO.PawnSecNextIdVO.SELECTOR.CLISTOMAP_ID, ref uNexId);
                        cMapVo.Id = uNexId;
                    }
                    cMapVo.StoreNumber = storeNumber;
                    cMapVo.StoreConfigId = pStore.StoreConfiguration.Id;
                    cMapVo.StoreClientConfigId = pStore.StoreConfiguration.Id;
                    cMapVo.StoreSiteId = pStore.StoreSiteId;
                    cMapVo.ClientRegistryId = mach.Machine.ClientId;

                    if (newMap)
                    {
                        this.storeData.PawnSecData.ClientStoreMapList.Add(cMapVo);    
                    }                    
                    this.storeData.PawnSecData.GenerateMaps();
                }*/
            /*}
            else
            {
                if (CollectionUtilities.isEmpty(this.storeData.PawnSecData.ClientMachines))
                {
                    MessageBox.Show("Did not properly add machine.  Please click the add button next to the machine name.",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                    return;
                }
                //Get last workstation object
                var lastMach = this.storeData.PawnSecData.ClientMachines[this.storeData.PawnSecData.ClientMachines.Count - 1];
                //Ensure this machine matches the test in the combo box
                if (lastMach != null && lastMach.Machine.MachineName.Equals(machineNameComboBox.Text))
                {
                    //Use workstation object matching selected from combo box
                    int firstPerIdx = machineNameComboBox.Text.IndexOf(".");
                    string trimmedMachineName;
                    if (firstPerIdx >= 1)
                    {
                        trimmedMachineName = machineNameComboBox.Text.Substring(0, firstPerIdx);
                    }
                    else
                    {
                        trimmedMachineName = machineNameComboBox.Text;
                    }
                    if (!string.IsNullOrEmpty(this.ipAddressTextBox.Text))
                    {
                        lastMach.Machine.IPAddress = this.ipAddressTextBox.Text;
                        if (!StringUtilities.ISIPv4Address(lastMach.Machine.IPAddress))
                        {
                            MessageBox.Show(
                                    "The IPv4 address entered is not properly formatted.  Must be of the form: XXX.XXX.XXX.XXX " +
                                    Environment.NewLine +
                                    " (NOTE: The individual IPv4 components need to be between 0 and 255 and can be 1 to 3 digits");
                            return;
                        }
                    }

                    if (!string.IsNullOrEmpty(this.macAddressTextBox.Text))
                    {
                        lastMach.Machine.MACAddress = this.macAddressTextBox.Text;
                        if (!StringUtilities.IsMACAddress(lastMach.Machine.MACAddress))
                        {
                            MessageBox.Show(
                                    "The MAC address entered is not properly formatted. Must be of the form: XX:XX:XX:XX:XX:XX",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                            return;
                        }
                    }

                    if (this.adobeOverrideCheckBox.Checked &&
                        !string.IsNullOrEmpty(this.adobeReaderOverrideTextBox.Text))
                    {
                        lastMach.Machine.AdobeOverride = this.adobeReaderOverrideTextBox.Text;
                    }

                    if (this.ghostScriptOverrideCheckBox.Checked &&
                        !string.IsNullOrEmpty(this.ghostScriptOverrideTextBox.Text))
                    {
                        lastMach.Machine.GhostOverride = this.ghostScriptOverrideTextBox.Text;
                    }*/

                    /*bool newMach = false,
                         newMap = false;

                    var mach = this.storeData.PawnSecData.ClientMachines.Find(
                                    x => x.Machine.MachineName.Equals(trimmedMachineName));
                    if (mach == null)
                    {
                        mach = new PawnSecVO.ClientPawnSecMachineVO();
                        newMach = true;
                    }

                    var machMapping =
                            this.storeData.PawnSecData.ClientStoreMapList.Find(
                                    x => x.ClientRegistryId == mach.Machine.ClientId);
                    if (machMapping == null)
                    {
                        machMapping = new PawnSecVO.ClientStoreMapVO();
                        newMap = true;
                    }*/

                    /*lastMach.Machine.IsAllowed = this.isAllowedCheckBox.Checked;
                    lastMach.StoreMachine.PrintEnabled = this.printingAllowedCheckBox.Checked;
                    lastMach.StoreMachine.TerminalNumber = (int)this.terminalNumberNumericUpDown.Value;
                    lastMach.StoreMachine.LogLevel = this.logLevelComboBox.Text;
                    lastMach.StoreMachine.TraceLevel = (int)this.traceLevelNumericUpDown.Value;
                    lastMach.StoreMachine.WorkstationId = trimmedMachineName;
                    lastMach.Machine.WorkstationName = trimmedMachineName;*/
                    //ulong uNexId = 1;
                    /*if (newMach)
                    {
                        if (lastMach.Machine.ClientId <= 0)
                        {
                            this.storeData.PawnSecData.NextIdSet.GetNextIds(
                                    PawnSecVO.PawnSecNextIdVO.SELECTOR.CLIREG_ID, ref uNexId);
                            lastMach.Machine.ClientId = uNexId;
                        }
                        if (lastMach.StoreMachine.Id <= 0)
                        {
                            this.storeData.PawnSecData.NextIdSet.GetNextIds(
                                    PawnSecVO.PawnSecNextIdVO.SELECTOR.STOCLICFG_ID, ref uNexId);
                            lastMach.StoreMachine.Id = uNexId;
                        }
                    }
                    if (newMap)
                    {
                        var cMapVo = new PawnSecVO.ClientStoreMapVO();
                        this.storeData.PawnSecData.NextIdSet.GetNextIds(
                                PawnSecVO.PawnSecNextIdVO.SELECTOR.CLISTOMAP_ID, ref uNexId);
                        cMapVo.Id = uNexId;
                        cMapVo.StoreNumber = storeNumber;
                        cMapVo.StoreConfigId = pStore.StoreConfiguration.Id;
                        cMapVo.StoreClientConfigId = pStore.StoreConfiguration.Id;
                        cMapVo.StoreSiteId = pStore.StoreSiteId;
                        cMapVo.ClientRegistryId = lastMach.Machine.ClientId;
                        this.storeData.PawnSecData.ClientStoreMapList.Add(cMapVo);
                    }*/
                    //this.storeData.PawnSecData.GenerateMaps();
                //}
                //}
                #endregion

                //Update global configuration
            GlobalConfigVO gCfg = this.storeData.PawnSecData.GlobalConfiguration;
            if (!string.IsNullOrEmpty(this.adobeReaderPathTextBox.Text))
            {
                gCfg.AdobeReaderPath = this.adobeReaderPathTextBox.Text;
            }

            if (!string.IsNullOrEmpty(this.ghostScriptPathTextBox.Text))
            {
                gCfg.GhostScriptPath = this.ghostScriptPathTextBox.Text;
            }

            if (!string.IsNullOrEmpty(this.globalBasePathTextBox.Text))
            {
                //No need for this field
            }

            if (!string.IsNullOrEmpty(this.globalLogFolder.Text))
            {
                gCfg.BaseLogPath = this.globalLogFolder.Text;
            }

            if (!string.IsNullOrEmpty(this.globalMediaFolder.Text))
            {
                gCfg.BaseMediaPath = this.globalMediaFolder.Text;
            }

            if (!string.IsNullOrEmpty(this.globalTemplateFolder.Text))
            {
                gCfg.BaseTemplatePath = this.globalTemplateFolder.Text;
            }

            if (!string.IsNullOrEmpty(this.appVersionTextBox.Text))
            {
                pStore.AppVersion.AppVersion = this.appVersionTextBox.Text;
                gCfg.Version = pStore.AppVersion.AppVersionId;
            }

            Close();
        }

        private DialogResult createOpenFileDialog(string defaultFileName, string fileFilter, 
            out OpenFileDialog oDiag)
        {
            //Show open file dialog
            oDiag = new OpenFileDialog
            {
                CheckFileExists = true,
                InitialDirectory = @"c:\",
                FileName = (defaultFileName ?? string.Empty),
                Filter = (fileFilter ?? string.Empty) + "|All files (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false
            };

            return (oDiag.ShowDialog(this));
        }

        private DialogResult createOpenFolderDialog(string desc, out FolderBrowserDialog oDiag)
        {
            //Show open file dialog
            oDiag = new FolderBrowserDialog();
            oDiag.ShowNewFolderButton = false;
            oDiag.Description = desc ?? "Select a folder";
            oDiag.RootFolder = Environment.SpecialFolder.MyComputer;
            return (oDiag.ShowDialog(this));
        }

        private bool extractPathAndFileName(DialogResult res, OpenFileDialog diag, out string safePathFile)
        {
            safePathFile = string.Empty;
            if (diag == null || res != DialogResult.OK) return (false);
            var safeFileName = diag.SafeFileName;
            var safeFileDir = diag.FileName;
            if (string.IsNullOrEmpty(safeFileDir)) return (false);
            int idxLastSlash = safeFileDir.LastIndexOf(FILEPATH_SLASH);
            safeFileDir = safeFileDir.Substring(0, idxLastSlash + 1);
            safePathFile = safeFileDir + safeFileName;
            return (!string.IsNullOrEmpty(safePathFile));
        }

        private void browseAdobeReaderPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDiag;
            var res = createOpenFileDialog(ACROREAD_EXE_FILENAME, EXE_FILE_FILTER, out oDiag);
            string safePathFile;
            if (extractPathAndFileName(res, oDiag, out safePathFile))
            {
                this.adobeReaderPathTextBox.Text = safePathFile;
            }
        }

        private void browseGhostScriptPathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDiag;
            var res = createOpenFileDialog(GHOSTSCRIPT_EXE_FILENAME, EXE_FILE_FILTER, out oDiag);
            string safePathFile;
            if (extractPathAndFileName(res, oDiag, out safePathFile))
            {
                this.ghostScriptPathTextBox.Text = safePathFile;
            }
        }

        private void browseAdobeReaderPathOverrideButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDiag;
            var res = createOpenFileDialog(ACROREAD_EXE_FILENAME, EXE_FILE_FILTER, out oDiag);
            string safePathFile;
            if (extractPathAndFileName(res, oDiag, out safePathFile))
            {
                this.adobeReaderOverrideTextBox.Text = safePathFile;
            }
        }

        private void ghostScriptOverrideButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDiag;
            var res = createOpenFileDialog(GHOSTSCRIPT_EXE_FILENAME, EXE_FILE_FILTER, out oDiag);
            string safePathFile;
            if (extractPathAndFileName(res, oDiag, out safePathFile))
            {
                this.ghostScriptOverrideTextBox.Text = safePathFile;
            }
        }

        private void terminalNumberNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            decimal termNum = terminalNumberNumericUpDown.Value;
            if (termNum < MIN_TERMINAL_NUMBER)
            {
                terminalNumberNumericUpDown.Value = MIN_TERMINAL_NUMBER;
            }
            else if (termNum > MAX_TERMINAL_NUMBER)
            {
                terminalNumberNumericUpDown.Value = MAX_TERMINAL_NUMBER;
            }
        }

        private void traceLevelNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            decimal tracNum = traceLevelNumericUpDown.Value;
            if (tracNum < MIN_TRACE_LEVEL)
            {
                traceLevelNumericUpDown.Value = MIN_TRACE_LEVEL;
            }
            else if (tracNum > MAX_TRACE_LEVEL)
            {
                traceLevelNumericUpDown.Value = MAX_TRACE_LEVEL;
            }
        }

        private void browseBasePathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDiag;
            var res = createOpenFolderDialog("Base path for important sub folders:", out fDiag);
            if (res == DialogResult.OK)
            {
                this.globalBasePathTextBox.Text = fDiag.SelectedPath ?? string.Empty;
            }
        }

        private void browseTemplateFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDiag;
            var res = createOpenFolderDialog("Select the folder for templates:", out fDiag);
            if (res == DialogResult.OK)
            {
                this.globalTemplateFolder.Text = fDiag.SelectedPath ?? string.Empty;
            }
        }

        private void browseLogFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDiag;
            var res = createOpenFolderDialog("Select the folder for log files:", out fDiag);
            if (res == DialogResult.OK)
            {
                this.globalLogFolder.Text = fDiag.SelectedPath ?? string.Empty;
            }
        }

        private void browseMediaFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fDiag;
            var res = createOpenFolderDialog("Select the folder for media files:", out fDiag);
            if (res == DialogResult.OK)
            {
                this.globalMediaFolder.Text = fDiag.SelectedPath ?? string.Empty;
            }
        }

        private void updateFormBasedOnMachine(PawnSecVO.ClientPawnSecMachineVO machine)
        {
            if (machine != null)
            {
                this.ipAddressTextBox.Enabled = true;
                this.macAddressTextBox.Enabled = true;
                this.isAllowedCheckBox.Enabled = true;
                this.terminalNumberNumericUpDown.Enabled = true;
                this.logLevelComboBox.Enabled = true;
                this.traceLevelNumericUpDown.Enabled = true;
                this.adobeOverrideCheckBox.Enabled = true;
                this.ghostScriptOverrideCheckBox.Enabled = true;
                this.removeButton.Enabled = true;
                this.printingAllowedCheckBox.Enabled = true;
                if (!string.IsNullOrEmpty(machine.Machine.IPAddress))
                {
                    this.ipAddressTextBox.Text = machine.Machine.IPAddress;
                }
                if (!string.IsNullOrEmpty(machine.Machine.MACAddress))
                {
                    this.macAddressTextBox.Text = machine.Machine.MACAddress;
                }
                this.isAllowedCheckBox.Checked = machine.Machine.IsAllowed;
                this.printingAllowedCheckBox.Checked = machine.StoreMachine.PrintEnabled;
                this.traceLevelNumericUpDown.Value = machine.StoreMachine.TraceLevel;
                this.terminalNumberNumericUpDown.Value = machine.StoreMachine.TerminalNumber;
                int fndIdx = -1, idx = 0;
                foreach(var it in this.logLevelComboBox.Items)
                {
                    if (it.Equals(machine.StoreMachine.LogLevel))
                    {
                        fndIdx = idx;
                        break;
                    }
                    idx++;
                }
                if (fndIdx != -1)
                {
                    this.logLevelComboBox.SelectedIndex = fndIdx;
                }
                else
                {
                    this.logLevelComboBox.SelectedText = LogLevel.DEBUG.ToString();
                }
                if (!string.IsNullOrEmpty(machine.Machine.AdobeOverride))
                {
                    this.adobeOverrideCheckBox.Checked = true;
                    this.adobeReaderOverrideTextBox.Text = machine.Machine.AdobeOverride;
                }

                if (!string.IsNullOrEmpty(machine.Machine.GhostOverride))
                {
                    this.ghostScriptOverrideCheckBox.Checked = true;
                    this.ghostScriptOverrideTextBox.Text = machine.Machine.GhostOverride;
                }
            }            
        }

        private void updateFormBasedOnGlobalConfig()
        {
            if (this.storeData.PawnSecData != null &&
                this.storeData.PawnSecData.GlobalConfiguration != null)
            {
                GlobalConfigVO g = this.storeData.PawnSecData.GlobalConfiguration;

                if (!string.IsNullOrEmpty(g.AdobeReaderPath))
                {
                    this.adobeReaderPathTextBox.Text = g.AdobeReaderPath;
                }

                if (!string.IsNullOrEmpty(g.GhostScriptPath))
                {
                    this.ghostScriptPathTextBox.Text = g.GhostScriptPath;
                }

                //Create const string default base path
                const string defaultBasePath = @"c:\Program Files\Phase2App";

                //Set to this default ALWAYS
                this.globalBasePathTextBox.Text = defaultBasePath;

                if (!string.IsNullOrEmpty(g.BaseLogPath))
                {
                    this.globalLogFolder.Text = g.BaseLogPath;
                }
                else
                {
                    this.globalLogFolder.Text = defaultBasePath + @"\logs";
                }

                if (!string.IsNullOrEmpty(g.BaseMediaPath))
                {
                    this.globalMediaFolder.Text = g.BaseMediaPath;
                }
                else
                {
                    this.globalMediaFolder.Text = defaultBasePath + @"\media";
                }

                if (!string.IsNullOrEmpty(g.BaseTemplatePath))
                {
                    this.globalTemplateFolder.Text = g.BaseTemplatePath;
                }
                else
                {
                    this.globalTemplateFolder.Text = defaultBasePath + @"\templates";
                }

                if (!string.IsNullOrEmpty(g.AdobeReaderPath))
                {
                    this.adobeReaderPathTextBox.Text = g.AdobeReaderPath;
                }

                if (!string.IsNullOrEmpty(g.GhostScriptPath))
                {
                    this.ghostScriptPathTextBox.Text = g.GhostScriptPath;
                }

                if (!string.IsNullOrEmpty(g.Version))
                {
                    var curStore = this.pawnSecVo.GetStore();
                    if (curStore != null)
                    {
                        this.appVersionTextBox.Text = curStore.AppVersion.AppVersion;
                    }                    
                }
            }
        }

        private void machineNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Must be editing a machine (check initial creation flag)
            if (!this.InitialCreation)// && !this.CreatingWorkstation)
            {
                //Sync machine boxes with name of machine selected
                string macName = this.machineNameComboBox.Text;
                if (!string.IsNullOrEmpty(macName))
                {
                    var mac = this.storeData.PawnSecData.ClientMachines.Find(x => x.Machine.MachineName.Equals(macName, StringComparison.OrdinalIgnoreCase));
                    if (mac != null)
                    {
                        updateFormBasedOnMachine(mac);
                        this.removeButton.Enabled = true;
                    }
                }
            }
            else if (
                this.machineNameComboBox.SelectedIndex >= 0 && 
                this.machineNameComboBox.Items != null && 
                this.machineNameComboBox.Items.Count > this.machineNameComboBox.SelectedIndex)
            {
                //Take a slightly different approach
                object macName = this.machineNameComboBox.Items[this.machineNameComboBox.SelectedIndex];
                if (macName != null)
                {
                    string macNameStr = macName.ToString();
                    if (!string.IsNullOrEmpty(macNameStr))
                    {
                        var mac =
                                this.storeData.PawnSecData.ClientMachines.Find(
                                        x => x.Machine.MachineName.Equals(macNameStr, StringComparison.OrdinalIgnoreCase));
                        if (mac != null)
                        {
                            updateFormBasedOnMachine(mac);
                            this.removeButton.Enabled = true;
                        }
                    }
                }
            }
        }

        private void machineNameComboBox_TextChanged(object sender, EventArgs e)
        {
            string macName = this.machineNameComboBox.Text;
            if (!string.IsNullOrEmpty(macName))
            {
                this.ipAddressTextBox.Enabled = true;
                this.macAddressTextBox.Enabled = true;
                this.isAllowedCheckBox.Enabled = true;
                this.terminalNumberNumericUpDown.Enabled = true;
                this.logLevelComboBox.Enabled = true;
                this.traceLevelNumericUpDown.Enabled = true;
                this.adobeOverrideCheckBox.Enabled = true;
                this.ghostScriptOverrideCheckBox.Enabled = true;
                this.addMachineButton.Enabled = true;
                //this.CreatingWorkstation = true;
                this.printingAllowedCheckBox.Enabled = true;
            }
            else
            {
                this.addMachineButton.Enabled = false;
                this.macAddressTextBox.Enabled = false;
                this.isAllowedCheckBox.Enabled = false;
                this.terminalNumberNumericUpDown.Enabled = false;
                this.logLevelComboBox.Enabled = false;
                this.traceLevelNumericUpDown.Enabled = false;
                this.adobeOverrideCheckBox.Enabled = false;
                this.ghostScriptOverrideCheckBox.Enabled = false;
                this.addMachineButton.Enabled = false;
                //this.CreatingWorkstation = false;
                this.printingAllowedCheckBox.Enabled = false;
            }
        }

        private void adobeOverrideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (adobeOverrideCheckBox.Checked)
            {
                this.adobeReaderOverrideTextBox.Enabled = true;
                this.browseAdobeReaderPathOverrideButton.Enabled = true;
            }
            else
            {
                this.adobeReaderOverrideTextBox.Enabled = false;
                this.browseAdobeReaderPathOverrideButton.Enabled = false;
            }
        }

        private void ghostScriptOverrideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ghostScriptOverrideCheckBox.Checked)
            {
                this.ghostScriptOverrideButton.Enabled = true;
                this.ghostScriptOverrideTextBox.Enabled = true;
            }
            else
            {
                this.ghostScriptOverrideButton.Enabled = false;
                this.ghostScriptOverrideTextBox.Enabled = false;
            }
        }

        private void addMachineButton_Click(object sender, EventArgs e)
        {
            string macName = this.machineNameComboBox.Text;
            if (!string.IsNullOrEmpty(macName) &&
                macName.ToLowerInvariant().Contains(CASHAM_DOMAIN))
            {
                macName = macName.Trim();
                var res = MessageBox.Show("Are you sure you want to add " + macName + " to PAWNSEC™ ?",
                                          PawnStoreSetupForm.SETUPALERT_TXT,
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
                if (res == DialogResult.No)
                {
                    return;
                }

                //Ensure that this machine does not exist in pawn sec already for this store or
                //any other store
                var fndMachine = false;
                var pgFm = new InProgressForm("* VERIFYING UNIQUENESS OF MACHINE *");
                if (CollectionUtilities.isNotEmpty(this.storeData.PawnSecData.ClientMachines))
                {
                    var fndMac = this.storeData.PawnSecData.ClientMachines.Find(
                        x => x.Machine.MachineName.Equals(macName, StringComparison.OrdinalIgnoreCase));
                    if (fndMac != null)
                    {
                        fndMachine = true;
                    }
                    if (fndMachine == false &&
                        !string.IsNullOrEmpty(ipAddressTextBox.Text))
                    {
                        var trimIp = ipAddressTextBox.Text.Trim();
                        if (!string.IsNullOrEmpty(trimIp))
                        {
                            //See if IP address is used, and if so, ensure uniqueness
                            var fndIp =
                                    this.storeData.PawnSecData.ClientMachines.Find(
                                            x => x.Machine.IPAddress.Equals(trimIp));
                            if (fndIp != null)
                            {
                                fndMachine = true;
                            }
                        }
                    }

                    if (fndMachine == false &&
                        !string.IsNullOrEmpty(macAddressTextBox.Text))
                    {
                        var trimMac = macAddressTextBox.Text.Trim();
                        if (!string.IsNullOrEmpty(trimMac))
                        {
                            var fndMacAddr =
                                    this.storeData.PawnSecData.ClientMachines.Find(
                                            x =>
                                            x.Machine.MACAddress.Equals(trimMac, StringComparison.OrdinalIgnoreCase));
                            if (fndMacAddr != null)
                            {
                                fndMachine = true;
                            }
                        }
                    }
                }
                
                //Now check globally against all entries
                if (!fndMachine)
                {
                    var whereClause = "machinename = '" + macName + "'";
                    if (!string.IsNullOrEmpty(ipAddressTextBox.Text))
                    {
                        var trimIp = ipAddressTextBox.Text.Trim();
                        if (!string.IsNullOrEmpty(trimIp))
                        {
                            whereClause += " or ipaddress = '" + trimIp + "'";
                        }
                    }
                    if (!string.IsNullOrEmpty(macAddressTextBox.Text))
                    {
                        var trimMac = macAddressTextBox.Text.Trim();
                        if (!string.IsNullOrEmpty(trimMac))
                        {
                            whereClause += " or macaddress = '" + trimMac + "'";
                        }
                    }
                    DataReturnSet dS;
                    if (DataAccessService.ExecuteQuery(false,
                                                       string.Format(
                                                               "select machinename from clientregistry where {0}",
                                                               whereClause),
                                                       "clientregistry",
                                                       PawnStoreSetupForm.PAWNSEC,
                                                       out dS,
                                                       ref this.dAPawnSec))
                    {
                        if (dS != null && dS.NumberRows > 0)
                        {
                            fndMachine = true;
                        }
                    }
                    pgFm.HideMessage();
                }

                if (fndMachine)
                {
                    pgFm.Dispose();
                    MessageBox.Show(
                            "This machine already exists in the pawn security database. " + Environment.NewLine +
                            "  If this machine has moved to a different store, please " + Environment.NewLine + 
                            "remove it from that store's security database first.",
                            PawnStoreSetupForm.SETUPALERT_TXT);
                    return;
                }
                pgFm.Dispose();    
                this.addMachineButton.Enabled = false;
                this.removeButton.Enabled = false;
                this.machineNameComboBox.Enabled = false;
                var newMac = new PawnSecVO.ClientPawnSecMachineVO();
                var newMapping = new PawnSecVO.ClientStoreMapVO();
                newMac.Machine.MachineName = macName;
                int perIdx = macName.IndexOf(".");
                if (perIdx != -1)
                {
                    newMac.Machine.WorkstationName = macName.Substring(0, perIdx);
                    
                }
                //Set terminal number
                if (CollectionUtilities.isNotEmpty(this.storeData.PawnSecData.ClientMachines))
                {
                    newMac.StoreMachine.TerminalNumber =
                            this.storeData.PawnSecData.ClientMachines.Max(
                                    x => x.StoreMachine.TerminalNumber) + 1;
                }
                else
                {
                    newMac.StoreMachine.TerminalNumber = 1;
                }
                //Set store client config id
                //ulong storeMachineId = 0;
                //this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.STOCLICFG_ID, ref storeMachineId);
                newMac.StoreMachine.Id = 0;//storeMachineId;

                //Set client id
                //ulong clientRegId = 0;
                //this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.CLIREG_ID, ref clientRegId);
                newMac.Machine.ClientId = 0;

                //Set mapping id
                //ulong newMappingId = 1;
                //this.storeData.PawnSecData.NextIdSet.GetNextIds(PawnSecVO.PawnSecNextIdVO.SELECTOR.CLISTOMAP_ID, ref newMappingId);
                newMapping.Id = 0;
                newMapping.ClientRegistryId = newMac.Machine.ClientId;

                //Set store config id
                newMapping.StoreClientConfigId = newMac.StoreMachine.Id;

                var curSto = this.getStoreInformation();
                if (curSto == null)
                {
                    MessageBox.Show("Cannot find store to add this client to for mapping purposes",
                                    PawnStoreSetupForm.SETUPALERT_TXT);
                    return;
                }
                newMapping.StoreSiteId = curSto.StoreSiteId;
                newMapping.StoreConfigId = curSto.StoreConfiguration.Id;
                newMapping.StoreNumber = this.storeNumber;
                this.storeData.PawnSecData.ClientMachines.Add(newMac);
                this.storeData.PawnSecData.ClientStoreMapList.Add(newMapping);
                this.storeData.PawnSecData.GenerateMaps();
            }
            else
            {
                MessageBox.Show(
                        "Please enter a valid machine name. It must contain the full domain name: " + CASHAM_DOMAIN,
                        PawnStoreSetupForm.SETUPALERT_TXT);
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            string macName = this.machineNameComboBox.Text;
            if (!string.IsNullOrEmpty(macName))
            {
                var res = MessageBox.Show("Are you sure you want to delete " + macName + " from PAWNSEC™ ?",
                                          PawnStoreSetupForm.SETUPALERT_TXT,
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
                if (res == DialogResult.No)
                {
                    return;
                }

                this.removeButton.Enabled = false;
                this.addMachineButton.Enabled = false;
                this.machineNameComboBox.Enabled = false;

                //Retrieve the machine from the client machines
                var fndMachine = this.storeData.PawnSecData.ClientMachines.Find(x => x.Machine.MachineName.Equals(macName, StringComparison.OrdinalIgnoreCase));
                if (fndMachine != null)
                {
                    this.storeData.PawnSecData.ClientMachines.Remove(fndMachine);
                    this.storeData.PawnSecData.ClientStoreMapList.RemoveAll(
                            x => x.ClientRegistryId == fndMachine.Machine.ClientId);
                    this.storeData.PawnSecData.GenerateMaps();
                }
                else
                {
                    MessageBox.Show("Could not delete " + macName + " from PAWNSEC™ !");
                }
            }
        }
    }
}
