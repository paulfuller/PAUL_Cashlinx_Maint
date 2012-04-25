using System;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Security;

namespace Common.Controllers.Application.ApplicationFlow
{
    /// <summary>
    /// General Image Button Controller
    /// - Utilizes naming conventions of graphical images
    ///   to properly show the different states of hoverable,
    ///   depressable, and normal buttons.
    /// </summary>
    public class ImageButtonController : IDisposable
    {
        #region Constants
        public static readonly string BUTTONNAME = "button";
        public static readonly string[] BUTTONSTATES =
        {
            "disabled", "normal", "hover", "depressed"
        };
        public static readonly string BUTTONSEP = "_";
        public static readonly int DEPRESSED_DELAY = 200;
        #endregion


        #region Enums
        /// <summary>
        /// The possible button image states
        /// </summary>
        public enum ImageButtonState
        {
            DISABLED        = 0,
            NORMAL          = 1,
            HOVER           = 2,
            DEPRESSED       = 3
        }

        /// <summary>
        /// The possible actions that can be performed
        /// on a button that could impact the button state
        /// </summary>
        public enum ImageButtonAction
        {
            CLICK = 0,
            HOVER = 1,
            ENTER = 2,
            LEAVE = 3
        }
        #endregion

        #region Private Fields
        private Button buttonControl;
        private string buttonName;
        private ImageButtonState originalButtonState;
        private ImageButtonState buttonState;
        private bool initialized;
        private object mutexObj;
        #endregion

        #region Accessors
        public ImageButtonState ButtonState
        {
            get { return (this.buttonState); }
        }
        public DesktopSession DesktopSession { get; private set; }
        public bool Initialized
        {
            get { return (this.initialized); }
        }
        #endregion

        /// <summary>
        /// For comparing button controls with the internal
        /// button control to this class
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool isSameButton(Button b)
        {
            if (b == null || this.buttonControl == null)
                return (false);
            if (b == this.buttonControl)
                return (true);
            return (false);
        }

        /// <summary>
        /// Computes the resource name of the button image
        /// based on the button state
        /// </summary>
        /// <returns>Resource name</returns>
        private string computeButtonStateImageName()
        {
            if (!this.initialized)
                return(string.Empty);
            StringBuilder sb = new StringBuilder();
            sb.Append(this.buttonName);
            sb.Append(BUTTONSEP);
            sb.Append(BUTTONNAME);
            sb.Append(BUTTONSEP);
            sb.Append(BUTTONSTATES[(int)this.buttonState]);
            return (sb.ToString());
        }

        /// <summary>
        /// Update the control to display the proper image based
        /// on the button state
        /// </summary>
        private void updateButton()
        {
            if (!this.initialized)
                return;
            string buttonImageName = this.computeButtonStateImageName();
            //Determine if the button image exists, otherwise we will not it
            if (DesktopSession == null ||
                DesktopSession.ButtonResourceManagerHelper == null ||
                string.IsNullOrEmpty(buttonImageName) ||
                string.IsNullOrWhiteSpace(buttonImageName))
            {
                return;
            }

            var buttonResMgr = DesktopSession.ButtonResourceManagerHelper.GetResourceManager();
            if (buttonResMgr == null)
            {
                return;
            }
            var buttonImg = buttonResMgr.GetObject(buttonImageName);
            if (buttonImg == null)
            {
                return;
            }
            this.buttonControl.BackgroundImage = (System.Drawing.Image)buttonImg;
            this.buttonControl.Visible = true;
            this.buttonControl.Update();

            //Check if delay is required for depressed
            /*if (this.buttonState == ImageButtonState.DEPRESSED)
            {
                DateTime origTime = DateTime.Now;
                while (true)
                {
                    DateTime nextTime = DateTime.Now;

                    TimeSpan curSpan = nextTime - origTime;
                    if (curSpan.TotalMilliseconds > (double)DEPRESSED_DELAY)
                    {
                        break;
                    }
                }
            }*/
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="btnCtrl">Button control</param>
        /// <param name="btnName">Button name context contained within the resource</param>
        /// <param name="startDisabled">Whether or not the button should be disabled</param>
        public ImageButtonController(DesktopSession desktopSession, Button btnCtrl, string btnName, bool startDisabled)
        {
            this.DesktopSession = desktopSession;
            this.initialized = false;
            this.mutexObj = null;
            if (btnCtrl == null || string.IsNullOrEmpty(btnName))
                return;
            this.mutexObj = new object();
            lock (this.mutexObj)
            {
                this.buttonControl = btnCtrl;
                this.buttonName = btnName;
                this.buttonState = (startDisabled) ? ImageButtonState.DISABLED : ImageButtonState.NORMAL;
                if (this.buttonState == ImageButtonState.DISABLED)
                {
                    this.buttonControl.Enabled = false;
                }
                this.initialized = true;
                this.originalButtonState = this.buttonState;
                this.updateButton();
            }
        }

        /// <summary>
        /// Reset the button state to its original value and
        /// update the graphical view of the button appropriately
        /// </summary>
        public void resetToOriginalState()
        {
            lock (this.mutexObj)
            {
                this.buttonState = this.originalButtonState;
                if (this.buttonState == ImageButtonState.DISABLED)
                {
                    this.buttonControl.Enabled = false;
                }
                else
                {
                    this.buttonControl.Enabled = true;
                    if (this.buttonState == ImageButtonState.DEPRESSED)
                    {
                        this.buttonState = ImageButtonState.NORMAL;
                    }
                }
                this.updateButton();
            }
        }

        /// <summary>
        /// Compute the new button state based on the input action and the
        /// current button state
        /// </summary>
        /// <param name="action">Action to perform against the current button state</param>
        public void performAction(ImageButtonAction action)
        {
            if (!this.initialized || this.buttonState == ImageButtonState.DISABLED)
                return;

            //Lock the class as the asynchronous nature of the UI could
            //and will cause data race conditions otherwise
            lock (this.mutexObj)
            {

                //Perform the button state change based on previous button state
                //and the input button action
                switch (this.buttonState)
                {
                    //Apply the action against the NORMAL button state
                    case ImageButtonState.NORMAL:
                        if (action == ImageButtonAction.CLICK)
                        {
                            this.buttonState = ImageButtonState.DEPRESSED;
                        }
                        else if (action == ImageButtonAction.ENTER)
                        {
                            this.buttonState = ImageButtonState.HOVER;
                        }
                        else if (action == ImageButtonAction.HOVER)
                        {
                            this.buttonState = ImageButtonState.HOVER;
                        }
                        else if (action == ImageButtonAction.LEAVE)
                        {
                            this.buttonState = ImageButtonState.NORMAL;
                        }
                        break;
                    //Apply the action against the HOVER button state
                    case ImageButtonState.HOVER:
                        if (action == ImageButtonAction.CLICK)
                        {
                            this.buttonState = ImageButtonState.DEPRESSED;
                        }
                        else if (action == ImageButtonAction.ENTER)
                        {
                            this.buttonState = ImageButtonState.HOVER;
                        }
                        else if (action == ImageButtonAction.HOVER)
                        {
                            this.buttonState = ImageButtonState.HOVER;
                        }
                        else if (action == ImageButtonAction.LEAVE)
                        {
                            this.buttonState = ImageButtonState.NORMAL;
                        }
                        break;
                    //Apply the action against the DEPRESSED button state
                    case ImageButtonState.DEPRESSED:
                        if (action == ImageButtonAction.CLICK)
                        {
                            this.buttonState = ImageButtonState.HOVER;
                        }
                        else if (action == ImageButtonAction.ENTER)
                        {
                            this.buttonState = ImageButtonState.DEPRESSED;
                        }
                        else if (action == ImageButtonAction.HOVER)
                        {
                            this.buttonState = ImageButtonState.DEPRESSED;
                        }
                        else if (action == ImageButtonAction.LEAVE)
                        {
                            this.buttonState = ImageButtonState.DEPRESSED;
                        }
                        break;
                    default:
                        break;
                }

                //Update button
                this.updateButton();
            }// End lock
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.initialized == false)
                return;
            lock (this.mutexObj)
            {
                //Null out fields
                this.initialized = false;
                this.buttonControl = null;
                this.buttonName = null;
            }
            //Null out mutex
            this.mutexObj = null;
        }

        #endregion
    }
}
