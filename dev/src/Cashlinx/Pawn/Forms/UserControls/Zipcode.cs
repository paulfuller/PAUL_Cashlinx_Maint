/********************************************************************
* CashlinxDesktop.Usercontrols
* Zipcode
* This user control lets the user enter a zipcode and when they leave
 * the control to call the USPS address validation service to get city and state
* Sreelatha Rengarajan 3/23/2009 Initial version
 * Sreelatha Rengarajan 6/19/09  Added a property in configuration file called USPSValidationEnabled
 *                               which, if false, the USPS validation service is not called
 * SR 6/1/2010 Added logic for changing back color*                               
**********************************************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Libraries.Forms;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls
{
    public partial class Zipcode : UserControl
    {

        private bool _required;
        private bool _isValid;
        private string _city;
        private string _state;
        ProcessingMessage procMessage;
        AddressData checkedAddress;
        private bool nonNumberEntered = false;
        private Random _rndObj = new Random();
        public event RoutedPropertyChangedEventHandler<String> stateChanging;
        public event RoutedPropertyChangedEventHandler<String> cityChanging;

        public new string Text
        {
            set
            {
                this.zipcodeTextBox.Text = value;
            }
   
            get
            {
                return this.zipcodeTextBox.Text;
            }
        }

        [Category("Type")]
        [Description("Sets whether the Control is required to be entered in the form")]
        [DefaultValue(false)]
        public bool Required
        {
            get
            {
                return _required;
            }
            set
            {
                _required = value;
            }
        }

        [Category("Validation")]
        [Description("Sets if the control is valid")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool isValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
            }
        }

        [Category("Data")]
        [Description("Sets the city data after web service call")]
        [Browsable(false)]
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }

        [Category("Data")]
        [Description("Sets the state data after web service call")]
        [Browsable(false)]
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public Zipcode()
        {
            InitializeComponent();
        }

        protected override void OnEnter(EventArgs e)
        {
            Rectangle rect = new Rectangle(this.Bounds.X-1, this.Bounds.Y - 1, this.Bounds.Width + 1, this.Bounds.Height + 1);
            Commons.CustomPaint(this, rect);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {
                if (this.zipcodeTextBox.isValid)
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }
            }

            //base.OnLayout(e);
        }


        protected override void OnLeave(EventArgs e)
        {
            checkData();

            Rectangle rect = new Rectangle(this.Bounds.X-1, this.Bounds.Y - 1, this.Bounds.Width + 1, this.Bounds.Height + 1);
            Commons.RemoveBorder(this, rect);
            base.OnLeave(e);


        }



        private void checkData()
        {
            if (_required)
            {
                if (this.zipcodeTextBox.isValid)
                {
                    checkZipCode();
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }
            }
            else
                if (this.zipcodeTextBox.isValid)
                {
                    _isValid = true;
                    checkZipCode();
                }
        }

        // Handle the KeyDown event to determine the type of character entered into the textbox.
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
            //If shift key was pressed, it's not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }

        // This event occurs after the KeyDown event and can be used to prevent
        // characters from entering the control.
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        //When the control's back color is changed change the
        //backcolor of the textbox
        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
            this.zipcodeTextBox.BackColor = this.BackColor;
        }

        public void checkValid()
        {
            checkData();
            
        }

        /// <summary>
        /// Method that does the zipcode validation against a web service in the
        /// background while showing a processing message to the user
        /// </summary>
        private void checkZipCode()
        {
            if (string.Equals(Properties.Resources.USPSValidationEnabled, Boolean.TrueString, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    procMessage = new ProcessingMessage("USPS Address Verification is in progress. Please Wait.");
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                    bw.RunWorkerAsync();
                    procMessage.ShowDialog(this.Parent);
                }
                catch (SystemException ex)
                {
                    BasicExceptionHandler.Instance.AddException("Error calling the web service to check zip code", ex);
                }
            }
            else
            {
                this.City = "";
                this.State = GlobalDataAccessor.Instance.CurrentSiteId.State;

            }

        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            procMessage.Close();
            if (checkedAddress != null)
            {
                if (checkedAddress.City != null)
                {
                    String oldCity = this.City;
                    this.City = checkedAddress.City;

                    if (!checkedAddress.City.Equals(oldCity) && cityChanging != null)
                        cityChanging(checkedAddress.City, new RoutedPropertyChangedEventArgs<string>(oldCity, this.City));


                }
                if (checkedAddress.State != null)
                {
                    String oldState = this.State;
                    this.State = checkedAddress.State;

                    if (!checkedAddress.State.Equals(oldState) && stateChanging != null)
                        stateChanging(checkedAddress.State, new RoutedPropertyChangedEventArgs<string>(oldState, this.State));
                }
            }
            else
            {
                this.City = "";
                this.State = GlobalDataAccessor.Instance.CurrentSiteId.State;
            }

        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            AddressWebService addrWebservice;
            try
            {
                addrWebservice = new AddressWebService();
                addrWebservice.CurrentDateTime = ShopDateTime.Instance.ShopDate;
                addrWebservice.ShopNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                addrWebservice.TerminalID = GlobalDataAccessor.Instance.CurrentSiteId.TerminalId;
                if (GlobalDataAccessor.Instance.DesktopSession.UserName.Equals(string.Empty))
                    addrWebservice.UserID = "1";
                else
                    addrWebservice.UserID = GlobalDataAccessor.Instance.DesktopSession.UserName;
                addrWebservice.TransactionID = _rndObj.Next().ToString();
                checkedAddress = addrWebservice.LookupCityState(this.zipcodeTextBox.Text, "");
                //call to web service was a success

            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Call to web service to get city and state by zipcode failed", ex);
            }
            finally
            {
                addrWebservice = null;
            }


        }


    }
}
