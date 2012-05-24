using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Common.Controllers.Application.ApplicationFlow
{
    public class MenuLevelController
    {
        private readonly UserControl usrPanel;
        private string panelTag;
        private readonly Dictionary<Button, string> buttonToChildPanelMap;

        public bool Initialized { get; private set; }

        public string PanelName { get; private set; }

        public string MenuParentName { get; private set; }

        public string this[Button b]
        {
            get
            {
                if (buttonToChildPanelMap != null)
                {
                    return buttonToChildPanelMap.ContainsKey(b) ? (buttonToChildPanelMap[b]) : (null);
                }
                return "";
            }
        }

        public UserControl Panel
        {
            get
            {
                return (this.usrPanel);
            }
        }

        public Button TriggerButton { get; private set; }

        public Button BackButton { get; private set; }

        private void initialize()
        {
            //Get panel tag to determine panel name
            //and menu parent name
            var panelTagObj = this.usrPanel.Tag;
            if (panelTagObj != null && panelTagObj is string)
            {
                this.panelTag = (string)panelTagObj;
                if (!string.IsNullOrEmpty(this.panelTag))
                {
                    var pipeIdx = this.panelTag.IndexOf('|');
                    if (pipeIdx > -1)
                    {
                        this.PanelName = this.panelTag.Substring(0, pipeIdx);
                        this.MenuParentName = this.panelTag.Substring(pipeIdx + 1);
                        //See if this is the parent top menu (menu parent name is string "null")
                        if (this.MenuParentName.Equals("null"))
                        {
                            this.MenuParentName = string.Empty;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                return;
            }
            //Scan buttons to determine valid child panels
            //and the buttons that trigger them
            var initOneButton = false;

            if (usrPanel.Controls.Count > 0)
            {
                foreach (Control c in this.usrPanel.Controls)
                {
                    if (c == null)
                        continue;
                    if (!(c is Button))
                    {
                        continue;
                    }
                    var btn = (Button)c;
                    var btnTag = btn.Tag;
                    if (btnTag == null || !(btnTag is string))
                    {
                        continue;
                    }
                    var btnTagStr = (string)btnTag;
                    if (string.IsNullOrEmpty(btnTagStr))
                    {
                        continue;
                    }
                    var pipeIdx = btnTagStr.IndexOf('|');
                    if (pipeIdx <= -1)
                    {
                        continue;
                    }
                    var btnName = btnTagStr.Substring(0, pipeIdx);
                    var btnTargetName = btnTagStr.Substring(pipeIdx + 1);
                    if (!btnName.Equals("BackButton", StringComparison.OrdinalIgnoreCase))
                    {
                        //Make entry in map
                        this.buttonToChildPanelMap.Add(btn, btnTargetName);
                        //Ensure that this class monitors the buttons
                        btn.Click += this.handleButtonClick;
                        btn.MouseClick += this.handleMouseButtonClick;
                        initOneButton = true;
                    }
                    else
                    {
                        btn.Click += this.handleButtonClick;
                        btn.MouseClick += this.handleMouseButtonClick;
                        this.BackButton = btn;
                    }
                }
            }
            else
            {
                return;
            }

            if (initOneButton)
                Initialized = true;
        }

        public MenuLevelController(UserControl panel)
        {
            this.Initialized = false;
            if (panel == null)
                return;
            this.BackButton = null;
            this.usrPanel = panel;
            this.buttonToChildPanelMap = new Dictionary<Button, string>();
            this.initialize();
        }

        public void buttonTriggerHotKey(Keys k)
        {
            if (!this.Initialized)
            {
                return;
            }

            if (k >= Keys.NumPad0 && k <= Keys.NumPad9)
            {
                int keyValue = (int)k;
                keyValue -= 48;
                k = (Keys)keyValue;
            }

            //Find triggering button that is tied to the hotkey
            //We are using the digits 1 through 9, 0 for 10, and
            //F1 through F12 (to replace 11 through 22)
            //The button's hotkey assignment is stored in it's 
            //tab index field
            //Loop through the buttons until the proper tab index
            //is found, if at all
            foreach (var b in this.buttonToChildPanelMap.Keys)
            {
                if (b == null || b.Enabled == false)
                    continue;

                var tabIdx = b.TabIndex;
                var kN = Keys.D0;
                if (tabIdx >= 1 && tabIdx <= 9)
                {
                    kN = Keys.D0 + tabIdx;
                }
                else switch (tabIdx)
                {
                    case 10:
                        kN = Keys.D0;
                        break;
                    case 11:
                        kN = Keys.F1;
                        break;
                    case 12:
                        kN = Keys.F2;
                        break;
                    default:
                        if (tabIdx > 12 && tabIdx <= 19)
                        {
                            kN = Keys.F3 + (tabIdx - 13);
                        }
                        else if (tabIdx > 19 && tabIdx <= 29)
                        {
                            kN = Keys.F10 + (tabIdx - 20);
                        }
                        else if (tabIdx > 29 && tabIdx <= 34)
                        {
                            kN = Keys.F20 + (tabIdx - 30);
                        }
                        else
                        {
                            //No key matches that can be used for
                            //a menu hotkey value
                            continue;
                        }
                        break;
                }

                //See if the button tab index matches the generated key number
                if (k != kN)
                {
                    continue;
                }
                //We found the button, set it as the trigger
                this.TriggerButton = b;

                //Fire the user panel Enabled event
                this.usrPanel.Enabled = false;

                //Exit this method
                return;
            }
        }

        public void handleButtonClick(object sender, EventArgs e)
        {
            //Do nothing for now
        }

        public void handleMouseButtonClick(object sender, MouseEventArgs e)
        {
            if (!this.Initialized)
            {
                return;
            }
            //Set triggering button
            
            this.TriggerButton = (Button)sender;

            //Fire event to disable the user panel
            this.usrPanel.Enabled = false;
        }
    }
}
