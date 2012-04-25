using System;
using System.Drawing;
using System.Windows.Forms;
using Audit.Flows.Impl;
using Common.Controllers.Application.ApplicationFlow;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Utility.String;

namespace Audit
{
    public partial class MainDesktop : MainDesktopBase
    {
        public MainDesktop()
        {
            InitializeComponent();
            SetFormSize();
            CenterPanels();
        }

        private FxnBlock endStateNotifier;
        private object lastActiveMenuPanel;
        private bool resetFlag;

        private void CenterPanels()
        {
            //Setup user control array
            var ucArray =
                    new UserControl[]
                    {
                            this.mainMenuPanel
                    };

            //Center all panels
            foreach (var curPanel in ucArray)
            {
                this.Center(curPanel);
            }
        }

        private void SetFormSize()
        {
            var monitorSize = GetMonitorSize();
            this.ClientSize = new Size(monitorSize.Width, monitorSize.Height);
            this.MaximumSize = new Size(monitorSize.Width, monitorSize.Height);
            this.MinimumSize = new Size(monitorSize.Width, monitorSize.Height);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        private void MainDesktop_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.resetFlag == true)
                return;
            int formsInTreeCnt = ADS.HistorySession.FormsInTree();
            if (formsInTreeCnt > 1)
            {
                ADS.HistorySession.ResetFocus();
                return;
            }

            //Only use menu hot key logic when a portion of the menu
            //is actually in focus
            if ((this.mainMenuPanel.Visible && this.mainMenuPanel.Enabled))
            {
                MenuLevelController curMenuLevel = null;
                if (this.mainMenuPanel.Enabled)
                {
                    curMenuLevel = this.mainMenuPanel.MenuController;
                }

                if (curMenuLevel == null)
                {
                    return;
                }

                curMenuLevel.buttonTriggerHotKey(e.KeyCode);
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.KillApplication(null);
        }

        public void KillApplication(string reason)
        {
            Application.Exit();
        }

        private void MainDesktop_Load(object sender, EventArgs e)
        {
            this.endStateNotifier = new FxnBlock();
            this.endStateNotifier.InputParameter = null;
            this.endStateNotifier.Function = this.handleEndFlow;
            this.mainMenuPanel.setExitButtonHandler(this.exitButton_Click);

            ADS.Setup(this);

            ADS.ResourceProperties.DP = Properties.Resources.DP;
            ADS.ResourceProperties.HB = Properties.Resources.HB;
            ADS.ResourceProperties.HP = Properties.Resources.HP;
            ADS.ResourceProperties.Pearl = Properties.Resources.Pearl;

            ADS.ResourceProperties.oldvistabutton_blue = Properties.Resources.oldvistabutton_blue;
            ADS.ResourceProperties.vistabutton_blue = Properties.Resources.vistabutton_blue;
            ADS.ResourceProperties.cl1 = Properties.Resources.cl1;
            ADS.ResourceProperties.cl2 = Properties.Resources.cl2;
            ADS.ResourceProperties.cl3 = Properties.Resources.cl3;
            ADS.ResourceProperties.cl4 = Properties.Resources.cl4;
            ADS.ResourceProperties.cl5 = Properties.Resources.cl5;
            ADS.ResourceProperties.newDialog_400_BlueScale = Properties.Resources.newDialog_400_BlueScale;
            ADS.ResourceProperties.newDialog_512_BlueScale = Properties.Resources.newDialog_512_BlueScale;
            ADS.ResourceProperties.newDialog_600_BlueScale = Properties.Resources.newDialog_600_BlueScale;

            ADS.ResourceProperties.OverrideMachineName = Properties.Resources.OverrideMachineName;
        }

        private void mainMenuPanel_Load(object sender, EventArgs e)
        {
        }

        private bool InitializeFunctionality(string functionalityName)
        {
            bool rt = false;
            if (functionalityName.Equals("inventoryaudit", StringComparison.OrdinalIgnoreCase))
            {
                ADS.HistorySession.Trigger = functionalityName;
                ADS.AppController.invokeWorkflow(
                         MainFlowExecutor.INVENTORYAUDIT, this, this.endStateNotifier);
                rt = true;
            }

            if (rt == false)
            {
                MessageBox.Show("Could not invoke child functionality { " + functionalityName + " }",
                    "MenuFunctionalityError", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return rt;
        }

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

            ADS.HistorySession.Desktop();
            ADS.ApplicationExit = true;

            return (null);
        }

        private void resetMenu()
        {
            if (this.resetFlag)
                return;
            this.resetFlag = true;
            //Will allow user to reset the menu back to the parent
            if (this.mainMenuPanel.Enabled == false)
            {
                //Remove the connected cd user entry
                //string errorCode;
                //string errorMesg;
                //ShopCashProcedures.DeleteConnectedCdUser(CashlinxDesktopSession.Instance.CashDrawerId,
                //    PawnSecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.WorkstationId,
                //    CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber, CashlinxDesktopSession.Instance,
                //    out errorCode,
                //    out errorMesg);
                //CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile = new UserVO();
                ADS.ClearLoggedInUser();

                ADS.ClearSessionData();

                this.mainMenuPanel.Enabled = true;
                this.mainMenuPanel.Visible = true;
                this.mainMenuPanel.ButtonControllers.resetGroupInitialState();
                this.mainMenuPanel.BringToFront();
                this.mainMenuPanel.Update();
                this.mainMenuPanel.setExitButtonHandler(this.exitButton_Click);
            }
            this.resetFlag = false;
        }

        private void mainMenuPanel_EnabledChanged(object sender, EventArgs e)
        {
            if (this.resetFlag == true)
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
                            UserVO currUser = ADS.LoggedInUserSecurityProfile;
                            if (currUser == null || (string.IsNullOrEmpty(currUser.UserName)))
                            {
                                ADS.ClearLoggedInUser();
                                ADS.PerformAuthorization();
                            }
                            if (!string.IsNullOrEmpty(ADS.LoggedInUserSecurityProfile.UserName))
                                rt = this.initializeProperMenu(targetPanelName);
                            else
                            {
                                this.resetMenu();
                                return (true);
                            }
                        }
                        else
                        {
                            UserVO currUser = ADS.LoggedInUserSecurityProfile;
                            if (currUser == null || (string.IsNullOrEmpty(currUser.UserName)))
                            {
                                ADS.ClearLoggedInUser();
                                ADS.PerformAuthorization();
                            }
                            if (!string.IsNullOrEmpty(ADS.LoggedInUserSecurityProfile.UserName))
                            {

                                rt = this.InitializeFunctionality(
                                    StringUtilities.removeFromString(triggerBtn.Name.ToLowerInvariant(), "button"));


                            }
                            else
                            {
                                this.resetMenu();
                                return (true);
                            }
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
                MessageBox.Show("Could not navigate to child menu { " + targetPanelName + " }",
                    "MenuNavigationError", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (rt);
        }
    }
}
