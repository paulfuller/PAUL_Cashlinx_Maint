using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Libraries.Utility.String;

namespace Common.Controllers.Application.ApplicationFlow
{
    public class ImageButtonControllerGroup
    {
        public const string BUTTON_SUFFIX = "button";
        public const string BUTTON_TAGSEP = "|";
        public const string BUTTON_LEAF = "null";

        public DesktopSession DesktopSession { get; private set; }

        private Dictionary<string, ImageButtonController> imgControllers;
        private string currentDepressedButton;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btn"></param>
        /// <returns></returns>
        private string getButtonName(Button btn)
        {
            string rt = StringUtilities.removeFromString(btn.Name.ToLower(), BUTTON_SUFFIX);
            return (rt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="desktopSession"> </param>
        /// <param name="cntls"> </param>
        public ImageButtonControllerGroup(
            DesktopSession desktopSession,
            Control.ControlCollection cntls)            
        {
            if (cntls == null || cntls.Count <= 0)
            {
                return;
            }

            //Initialize data
            this.DesktopSession = desktopSession;
            this.imgControllers = new Dictionary<string,ImageButtonController>();
            this.currentDepressedButton = string.Empty;

            //Initialize buttons in the panel as the group
            foreach (Control c in cntls)
            {
                if (c == null)
                    continue;
                if (c is Button)
                {
                    Button curButton = (Button)c;                    
                    string btnName = getButtonName(curButton);
                    if (btnName.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    //SR 10/6/2010
                    //Added check to see if the button is available for the store
                    if (!(btnName.Equals("back", StringComparison.OrdinalIgnoreCase)))
                    {
                        curButton.Enabled = DesktopSession.IsButtonAvailable(curButton.Name, DesktopSession.CurrentSiteId);
                    }

                    bool disabledFlag = (curButton.Enabled == false);
                    imgControllers.Add(btnName, 
                        new ImageButtonController(DesktopSession, curButton, btnName, disabledFlag));
                    curButton.MouseClick += this.handleMouseClick;
                    curButton.Click += this.handleClick;
                    curButton.MouseHover += this.handleHover;
                    curButton.MouseEnter += this.handleEnter;
                    curButton.MouseLeave += this.handleLeave;
                }
            }
        }
        /// <summary>
        /// Reset all image buttons to their original state
        /// </summary>
        public void resetGroupInitialState()
        {
            this.currentDepressedButton = string.Empty;
            foreach (string s in this.imgControllers.Keys)
            {
                if (string.IsNullOrEmpty(s))
                {
                    continue;
                }
                if (!this.imgControllers.ContainsKey(s))
                {
                    continue;
                }
                ImageButtonController ibCntrl = this.imgControllers[s];
                ibCntrl.resetToOriginalState();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="action"></param>
        private void internalEventHandler(object sender, 
            ImageButtonController.ImageButtonAction action)
        {
            if ((sender != null) && (sender is Button))
            {
                var btn = (Button)sender;
                if (btn.Enabled)
                {
                    string buttonName = this.getButtonName(btn);
                    ImageButtonController imgBtnCntrl = this.imgControllers[buttonName];
                    if (imgBtnCntrl != null && imgBtnCntrl.Initialized)
                    {
                        bool performAction = true;
                        bool clearDepressed = false;
                        if (action == ImageButtonController.ImageButtonAction.CLICK)
                        {
                            if (!string.IsNullOrEmpty(this.currentDepressedButton))
                            {
                                ImageButtonController btnCntrl = 
                                    this.imgControllers[this.currentDepressedButton];
                                if (!btnCntrl.isSameButton(btn))
                                {
                                    performAction = false;
                                }
                                else
                                {
                                    clearDepressed = true;
                                }
                            }
                            else
                            {
                                this.currentDepressedButton = buttonName;
                            }
                        }
                        if (performAction)
                        {
                            imgBtnCntrl.performAction(action);
                        }
                        if (clearDepressed)
                        {
                            this.currentDepressedButton = string.Empty;
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
        private void handleHover(object sender, EventArgs e)
        {
            this.internalEventHandler(sender, 
                ImageButtonController.ImageButtonAction.HOVER);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleClick(object sender, EventArgs e)
        {
            /*this.internalEventHandler(sender,
                ImageButtonController.ImageButtonAction.CLICK);*/
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleMouseClick(object sender, MouseEventArgs e)
        {
            this.internalEventHandler(sender, 
                ImageButtonController.ImageButtonAction.CLICK);
            this.resetGroupInitialState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleEnter(object sender, EventArgs e)
        {
            this.internalEventHandler(sender,
                ImageButtonController.ImageButtonAction.ENTER);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleLeave(object sender, EventArgs e)
        {
            this.internalEventHandler(sender,
                ImageButtonController.ImageButtonAction.LEAVE);
        }
    }
}
