//  no ticket SMurphy 4/28/2010 commented unused reports pieces

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Network;
using Common.Libraries.Forms;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;
using Support.Forms.HardwareConfig;
using Support.Forms.ShopAdmin.EditGunBook;
using Support.Logic;

namespace Support.Forms
{
    public sealed partial class SupportAppDesktop : Form, IDisposable
    {
        public DesktopSession cdSession { get; set; } // Requires public access for Describe Merchandise upcalls for Global PawnItem
        private bool resetFlag;
        private object lastActiveMenuPanel;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// This function will reset the menu back to its initial state
        /// </summary>
        private void resetMenu()
        {
            if (this.resetFlag)
                return;
            this.resetFlag = true;
            //Will allow user to reset the menu back to the parent
            if (this.mainMenuPanel.Enabled == false)
            {
                if (this.CustomerServiceMenuPanel.Enabled)
                {
                    this.CustomerServiceMenuPanel.Enabled = false;
                    this.CustomerServiceMenuPanel.Visible = false;
                    this.CustomerServiceMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.CustomerServiceMenuPanel.SendToBack();
                }

                if (this.userAdminMenuPanel.Enabled)
                {
                    this.userAdminMenuPanel.Enabled = false;
                    this.userAdminMenuPanel.Visible = false;
                    this.userAdminMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.userAdminMenuPanel.SendToBack();
                }

                if (this.shopAdminMenuPanel.Enabled)
                {
                    this.shopAdminMenuPanel.Enabled = false;
                    this.shopAdminMenuPanel.Visible = false;
                    this.shopAdminMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.shopAdminMenuPanel.SendToBack();
                }

                if (this.systemAdminMenuPanel.Enabled)
                {
                    this.systemAdminMenuPanel.Enabled = false;
                    this.systemAdminMenuPanel.Visible = false;
                    this.systemAdminMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.systemAdminMenuPanel.SendToBack();
                }

                if (this.configMenuPanel2.Enabled)
                {
                    this.configMenuPanel2.Enabled = false;
                    this.configMenuPanel2.Visible = false;
                    this.configMenuPanel2.ButtonControllers.resetGroupInitialState();
                    this.configMenuPanel2.SendToBack();
                }
                
                if (this.cashlinxAdminPanel2.Enabled)
                {
                    this.cashlinxAdminPanel2.Enabled = false;
                    this.cashlinxAdminPanel2.Visible = false;
                    this.cashlinxAdminPanel2.ButtonControllers.resetGroupInitialState();
                    this.cashlinxAdminPanel2.SendToBack();
                }

                if (this.gbUtilitiesPanel.Enabled)
                {
                    this.gbUtilitiesPanel.Enabled = false;
                    this.gbUtilitiesPanel.Visible = false;
                    this.gbUtilitiesPanel.ButtonControllers.resetGroupInitialState();
                    this.gbUtilitiesPanel.SendToBack();
                }

                //CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile = new UserVO();
                //CashlinxPawnSupportSession.Instance.ClearLoggedInUser();


                this.mainMenuPanel.Enabled = true;
                this.mainMenuPanel.Visible = true;
                this.mainMenuPanel.ButtonControllers.resetGroupInitialState();
                this.mainMenuPanel.BringToFront();
                this.mainMenuPanel.Update();
                this.mainMenuPanel.setExitButtonHandler(this.exitButton_Click);
            }
            this.resetFlag = false;
        }

        /// <summary>
        /// Based on the button clicked and the current panel shown, 
        /// invoke the proper functionality or show the proper sub panel
        /// </summary>
        /// <param name="menuCtrl"></param>
        /// <returns></returns>
        private bool triggerNextEvent(MenuLevelController menuCtrl)
        {
            bool rt = false;
            if (menuCtrl != null)
            {
                Button triggerBtn = menuCtrl.TriggerButton;
                if (triggerBtn != null)
                {
                    if (triggerBtn != menuCtrl.BackButton)
                    {
                        string targetPanelName = menuCtrl[triggerBtn];


                        if (!string.IsNullOrEmpty(targetPanelName) &&
                            !targetPanelName.Equals("null"))
                        {
                              rt = this.initializeProperMenu(targetPanelName);
                        }
                        else
                        {
                            rt = this.initializeFunctionality(
                                StringUtilities.removeFromString(triggerBtn.Name.ToLowerInvariant(), "button"));
                        }
                    }
                    else
                    {
                        //We have the back button - reset the menu
                        this.resetMenu();
                        return (true);
                    }
                }
            }
            return (rt);
        }

        /// <summary>
        /// Convenience method to show a menu panel
        /// </summary>
        /// <param name="targetPanelName"></param>
        /// <returns></returns>
        private bool initializeProperMenu(string targetPanelName)
        {
            bool rt = false;
            if (!string.IsNullOrEmpty(targetPanelName))
            {
                Control c = this.Controls[targetPanelName];
                if (c != null)
                {
                    c.Enabled = true;
                    c.Visible = true;
                    c.BringToFront();
                    c.Update();
                    this.lastActiveMenuPanel = c;
                    rt = true;
                }
            }

            if (rt == false)
            {
                MessageBox.Show(string.Format("Could not navigate to child menu {{ {0} }}", targetPanelName),
                    "MenuNavigationError", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        public object handleEndFlow(object noParam)
        {
            try
            {
                this.resetMenu();
            }
            //Catch all
            catch
            {
                //Ensure that the main menu is visible if the reset menu fails
                this.mainMenuPanel.Enabled = true;
                this.mainMenuPanel.Visible = true;
                this.mainMenuPanel.ButtonControllers.resetGroupInitialState();
                this.mainMenuPanel.BringToFront();
                this.mainMenuPanel.Update();
                this.mainMenuPanel.setExitButtonHandler(this.exitButton_Click);
                this.resetFlag = false;
            }

            Show();
            BringToFront();
            CashlinxPawnSupportSession.Instance.ApplicationExit = true;

            return (null);
        }

        /// <summary>
        /// Convenience method to trigger application functionality
        /// based on the "leaf" button pressed.  A leaf button is one
        /// that triggers functions, rather than new sub menus
        /// </summary>
        /// <param name="functionalityName"></param>
        /// <returns></returns>
        private bool initializeFunctionality(string functionalityName)
        {
            bool rt = false;
            if (!string.IsNullOrEmpty(functionalityName))
            {
                CashlinxPawnSupportSession.Instance.HistorySession.TriggerName = functionalityName;
                //Check functions here
                if (functionalityName.Equals("changepassword", StringComparison.OrdinalIgnoreCase))
                {
                    var password = string.Empty;

                    var chngPwdForm = new UserChangePassword(PawnLDAPAccessor.Instance.PasswordPolicy,
                                                             CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserCurrentPassword);
                    DialogResult pwdResult = chngPwdForm.ShowDialog();
                    if (pwdResult == DialogResult.OK)
                    {
                        password = chngPwdForm.EnteredNewPassword;
                        CashlinxPawnSupportSession.Instance.LoggedInUserSecurityProfile.UserCurrentPassword = password;
                    }

                    this.handleEndFlow(null);

                    rt = true;

                }
                else if (functionalityName.Equals("resetldap", StringComparison.OrdinalIgnoreCase))
                {

                    var frm_reset_pwd = new FrmResetPwd();
                    frm_reset_pwd.ShowDialog();
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("cleartemp", StringComparison.OrdinalIgnoreCase))
                {
                    var frm_clear_tmp_status = new ClearTempStatus.FrmClearTempStatus1();
                    frm_clear_tmp_status.ShowDialog();
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("recautotrans", StringComparison.OrdinalIgnoreCase))
                {
                    //Need FrmAutoTransfer.cs
                    AutoTransfer.FrmAutoTransfer frm_auto_transfer = new AutoTransfer.FrmAutoTransfer();
                    frm_auto_transfer.ShowDialog();
                    this.handleEndFlow(null);
                    rt = true;
                }
                else if (functionalityName.Equals("custmaint", StringComparison.OrdinalIgnoreCase))
                {
                    //var frm_lookup_customer = new Support.Forms.Customer.LookupCustomer();
                    //frm_lookup_customer.ShowDialog();
                    //this.handleEndFlow(null);
                    //rt = true;

                    GlobalDataAccessor.Instance.DesktopSession.ClearSessionData();
                    GlobalDataAccessor.Instance.DesktopSession.HistorySession.Trigger = functionalityName;
                    GlobalDataAccessor.Instance.DesktopSession.AppController.invokeWorkflow(
                        functionalityName, this, this.endStateNotifier);


                    rt = true;
                }
                else if (functionalityName.Equals("hardware", StringComparison.OrdinalIgnoreCase))
                {
                    GunBookSearch search = new GunBookSearch();
                    search.ShowDialog();
                    //Need Hardware_Config.cs
                    //var myHardware = new HardwareConfig.Hardware_Config();
                    //var frm_get_shop = new FrmGetShop(CashlinxPawnSupportSession.Instance);
                    //DialogResult shopResult = frm_get_shop.ShowDialog();
                    //if (shopResult == DialogResult.OK)
                    //{
                    //    Hardware_Config.Instance.StoreID = frm_get_shop.StoreGuid;
                    //    Hardware_Config.Instance.StoreNumber = frm_get_shop.StoreNumber;
                    //    var frm_hardware_config = new HardwareConfig.FrmConfig();       // myHardware);
                    //    frm_hardware_config.ShowDialog();
                        
                    //}                    
                    //this.handleEndFlow(null);
                    rt = true;
                }
                else
                {
                    MessageBox.Show("Functionality Not Enabled with this release");
                    this.handleEndFlow(null);
                    rt = true;
                }
            }

            if (rt == false)
            {
                MessageBox.Show("Could not invoke child functionality { " + functionalityName + " }",
                    "MenuFunctionalityError", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        public SupportAppDesktop()
        {
            InitializeComponent();

            this.resetFlag = false;
            this.BackgroundImage = global::Support.Properties.Resources.Red_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            var monitorSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;
            this.ClientSize = new System.Drawing.Size(monitorSize.Width, monitorSize.Height);
            this.MaximumSize = new System.Drawing.Size(monitorSize.Width, monitorSize.Height);
            this.MinimumSize = new System.Drawing.Size(monitorSize.Width, monitorSize.Height);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);

            //Compute center screen coords
            float halfScreenWidth = monitorSize.Width / 2.0f;
            float halfScreenHeight = monitorSize.Height / 2.0f;

            //Setup user control array
            var ucArray =
                    new UserControl[]
                    {
                            this.mainMenuPanel,
                            this.CustomerServiceMenuPanel,
                            this.userAdminMenuPanel,
                            this.shopAdminMenuPanel,
                            this.systemAdminMenuPanel,
                            this.configMenuPanel2,
                            this.cashlinxAdminPanel2,
                            this.gbUtilitiesPanel
                    };

            //Center all panels
            foreach (var curPanel in ucArray)
            {
                this.centerMenuPanel(curPanel, halfScreenWidth, halfScreenHeight);
            }

            // try to find out what database we are in
            //string CurSystem = Properties.Settings.Default.PawnSystem;
            //string CurSystem = Properties.Settings.Default.PawnSecDBService;

            string CurSystem = string.Empty;
            try
            {
                string key = Properties.Resources.PrivateKey;
                string dbService = StringUtilities.Decrypt(
                Properties.Settings.Default.PawnSecDBService, key, true);

                if (string.IsNullOrEmpty(dbService))
                    dbService = string.Empty;
                else
                    dbService = dbService.ToUpperInvariant();

                if (dbService.Contains("CLXP"))
                    CurSystem = "Production";
                else if (dbService.Contains("CLXT"))
                    CurSystem = "QA";
                else if (dbService.Contains("CLXD"))
                    CurSystem = "Development";

                if(!string.IsNullOrEmpty(CurSystem))
                    CurSystem = " - " + CurSystem;
            }
            catch (Exception)
            {
                CurSystem = string.Empty;
            } //just to supress the error and continue further

            //lblVersion.Text = lblVersion.Text + " - " + CurSystem;
            lblVersion.Text = lblVersion.Text + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + CurSystem;
            

            //Make the version label appear in the bottom left
            //float verLabelHeight = this.versionLabel.Height;
            //this.versionLabel.Location = new Point(
            //        0, (int)(System.Math.Floor(monitorSize.Height - verLabelHeight - 10)));
            float verLabelHeight = this.lblVersion.Height;
            this.lblVersion.Location = new Point(
                    5, (int)(System.Math.Floor(monitorSize.Height - verLabelHeight - 50)));
            
            //Make the date label appear in the bottom right
            float dateLabelWidth = this.shopDateLabel.Width;
            float dateLabelHeight = this.shopDateLabel.Height;
            this.shopDateLabel.Location = new Point(
                    (int)(System.Math.Floor(monitorSize.Width - dateLabelWidth - 10)),
                    (int)(System.Math.Floor(monitorSize.Height - dateLabelHeight - 10)));

        }


        /// <summary>
        /// Centers a user control on the given screen based on resolution
        /// </summary>
        /// <param name="uC"></param>
        /// <param name="halfScreenWidth"></param>
        /// <param name="halfScreenHeight"></param>
        private void centerMenuPanel(UserControl uC,
            float halfScreenWidth, float halfScreenHeight)
        {
            if (uC == null)
                return;
            //Center control
            float halfWidth = uC.Width / 2.0f;
            float halfHeight = uC.Height / 2.0f;
            uC.Location = new Point(
                (int)(System.Math.Floor(halfScreenWidth - halfWidth)),
                (int)(System.Math.Floor(halfScreenHeight - halfHeight)));
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
        }

        #endregion


        private void exitButton_Click(object sender, EventArgs e)
        {
            //06/17/2010 GL
            //If no security profile has been loaded, exit will cause object not set reference exception

            this.KillApplication(null);
        }

        /// <summary>
        /// Kill the application
        /// </summary>
        /// <param name="reason">Log a reason for app exit - optional</param>
        public void KillApplication(string reason)
        {
            string resStr = reason ?? "";
            if (FileLogger.Instance != null)
            {
                if (FileLogger.Instance.IsEnabled && FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "Exiting application " + resStr);
                }
                FileLogger.Instance.Dispose();
            }
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>



        /// <summary>
        /// Event handler for the main menu panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MainMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            //we only care about this event if the panel is being disabled
            if (this.mainMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.mainMenuPanel.Visible = false;
                this.mainMenuPanel.SendToBack();
                this.mainMenuPanel.Update();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.mainMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.mainMenuPanel.Visible = true;
                    this.mainMenuPanel.Enabled = true;
                    this.mainMenuPanel.BringToFront();
                    this.mainMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.mainMenuPanel.Update();
                }
            }
        }


        public void gbUtilitiesMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            //we only care about this event if the panel is being disabled
            if (this.gbUtilitiesPanel.Enabled == false)
            {
                //Disable panel visibility
                this.gbUtilitiesPanel.Visible = false;
                this.gbUtilitiesPanel.SendToBack();
                this.gbUtilitiesPanel.Update();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.gbUtilitiesPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.gbUtilitiesPanel.Visible = true;
                    this.gbUtilitiesPanel.Enabled = true;
                    this.gbUtilitiesPanel.BringToFront();
                    this.gbUtilitiesPanel.ButtonControllers.resetGroupInitialState();
                    this.gbUtilitiesPanel.Update();
                }
            }
        }

        public void CustomerServiceMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            //we only care about this event if the panel is being disabled
            if (this.CustomerServiceMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.CustomerServiceMenuPanel.Visible = false;
                this.CustomerServiceMenuPanel.SendToBack();
                this.CustomerServiceMenuPanel.Update();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.CustomerServiceMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.CustomerServiceMenuPanel.Visible = true;
                    this.CustomerServiceMenuPanel.Enabled = true;
                    this.CustomerServiceMenuPanel.BringToFront();
                    this.CustomerServiceMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.CustomerServiceMenuPanel.Update();
                }
            }
        }

        public void userAdminMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            //we only care about this event if the panel is being disabled
            if (this.userAdminMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.userAdminMenuPanel.Visible = false;
                this.userAdminMenuPanel.SendToBack();
                this.userAdminMenuPanel.Update();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.userAdminMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.userAdminMenuPanel.Visible = true;
                    this.userAdminMenuPanel.Enabled = true;
                    this.userAdminMenuPanel.BringToFront();
                    this.userAdminMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.userAdminMenuPanel.Update();
                }
            }
        }

        public void shopAdminMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            //we only care about this event if the panel is being disabled
            if (this.shopAdminMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.shopAdminMenuPanel.Visible = false;
                this.shopAdminMenuPanel.SendToBack();
                this.shopAdminMenuPanel.Update();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.shopAdminMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.shopAdminMenuPanel.Visible = true;
                    this.shopAdminMenuPanel.Enabled = true;
                    this.shopAdminMenuPanel.BringToFront();
                    this.shopAdminMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.shopAdminMenuPanel.Update();
                }
            }
        }

        public void systemAdminMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            //we only care about this event if the panel is being disabled
            if (this.systemAdminMenuPanel.Enabled == false)
            {
                //Disable panel visibility
                this.systemAdminMenuPanel.Visible = false;
                this.systemAdminMenuPanel.SendToBack();
                this.systemAdminMenuPanel.Update();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.systemAdminMenuPanel.MenuController))
                {
                    //Failure occurred - restore menu
                    this.systemAdminMenuPanel.Visible = true;
                    this.systemAdminMenuPanel.Enabled = true;
                    this.systemAdminMenuPanel.BringToFront();
                    this.systemAdminMenuPanel.ButtonControllers.resetGroupInitialState();
                    this.systemAdminMenuPanel.Update();
                }
            }
        }

        public void configMenuPanel2_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            //we only care about this event if the panel is being disabled
            if (this.configMenuPanel2.Enabled == false)
            {
                //Disable panel visibility
                this.configMenuPanel2.Visible = false;
                this.configMenuPanel2.SendToBack();
                this.configMenuPanel2.Update();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.configMenuPanel2.MenuController))
                {
                    //Failure occurred - restore menu
                    this.configMenuPanel2.Visible = true;
                    this.configMenuPanel2.Enabled = true;
                    this.configMenuPanel2.BringToFront();
                    this.configMenuPanel2.ButtonControllers.resetGroupInitialState();
                    this.configMenuPanel2.Update();
                }
            }
        }

        public void cashlinxAdminPanel2_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag)
                return;
            //we only care about this event if the panel is being disabled
            if (this.cashlinxAdminPanel2.Enabled == false)
            {
                //Disable panel visibility
                this.cashlinxAdminPanel2.Visible = false;
                this.cashlinxAdminPanel2.SendToBack();
                this.cashlinxAdminPanel2.Update();

                //Get menu controller from sender
                if (!this.triggerNextEvent(this.cashlinxAdminPanel2.MenuController))
                {
                    //Failure occurred - restore menu
                    this.cashlinxAdminPanel2.Visible = true;
                    this.cashlinxAdminPanel2.Enabled = true;
                    this.cashlinxAdminPanel2.BringToFront();
                    this.cashlinxAdminPanel2.ButtonControllers.resetGroupInitialState();
                    this.cashlinxAdminPanel2.Update();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupportAppDesktop_Load(object sender, EventArgs e)
        {
            this.endStateNotifier = new FxnBlock();
            this.endStateNotifier.InputParameter = null;
            this.endStateNotifier.Function = this.handleEndFlow;

            this.mainMenuPanel.setExitButtonHandler(this.exitButton_Click);

            //Acquire desktop session object
            this.cdSession = CashlinxPawnSupportSession.Instance;
            this.cdSession.Setup(this);
        }


        /// <summary>
        /// Key down event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewDesktop_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.resetFlag)
                return;
            int formsInTreeCnt = CashlinxPawnSupportSession.Instance.HistorySession.FormsInTree();
            if (formsInTreeCnt > 1)
            {
                CashlinxPawnSupportSession.Instance.HistorySession.ResetFocus();
                return;
            }

            //Only use menu hot key logic when a portion of the menu
            //is actually in focus
            if ((this.mainMenuPanel.Visible && this.mainMenuPanel.Enabled) ||
                (this.CustomerServiceMenuPanel.Visible && this.CustomerServiceMenuPanel.Enabled) ||
                (this.userAdminMenuPanel.Visible && this.userAdminMenuPanel.Enabled))

            {
                MenuLevelController curMenuLevel = null;
                if (this.mainMenuPanel.Enabled)
                {
                    curMenuLevel = this.mainMenuPanel.MenuController;
                }
                else if (this.CustomerServiceMenuPanel.Enabled)
                {
                    curMenuLevel = this.CustomerServiceMenuPanel.MenuController;
                }
                else if (this.userAdminMenuPanel.Enabled)
                {
                    curMenuLevel = this.userAdminMenuPanel.MenuController;
                }

                if (curMenuLevel == null)
                    return;
                if (e.Shift)
                {
                    if (e.KeyCode == Keys.R)
                    {
                        //Execute resetting the menu
                        this.resetMenu();
                    }
                }
                else
                {
                    curMenuLevel.buttonTriggerHotKey(e.KeyCode);
                }
            }
        }

        private void mainMenuPanel_Load(object sender, EventArgs e)
        {

        }

    }
}
