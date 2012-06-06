using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Libraries.Forms.Components.Behaviors;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class NewFindMerchandise : Form
    {
        private Dictionary<string, string> manufacturerDictionary;
        private readonly AutoCompleter _autoCompleter;

        private const string ACTION_OK = "OK";
        private const string ACTION_MANUAL = "MANUAL";
        private const string ACTION_CANCEL = "CANCEL";

        private static string[] _manufacturers = new[]
                                                 {
                                                     "Bose",
                                                     "Sony",
                                                     "Nikon",
                                                     "Compaq",
                                                     "Glock",
                                                     "Brass Eagle",
                                                     "Citizen",
                                                     "Selmer",
                                                     "Huffy",
                                                     "Toshiba",
                                                     "Toro",
                                                     "RCA",
                                                     "Echo",
                                                     "Sylvania",
                                                     "Nintendo",
                                                     "Maestro",
                                                     "Motorola",
                                                     "Haeir",
                                                     "Bissell",
                                                     "Benelli",
                                                     "Stihl",
                                                     "Daewoo",
                                                     "Bostitch",
                                                     "Shimano",
                                                     "Roland",
                                                     "QEP",
                                                     "Mitchell",
                                                     "Bosch",
                                                     "Skil",
                                                     "Samsung",
                                                     "Apple",
                                                     "Emerson",
                                                     "Casio",
                                                     "Sanyo",
                                                     "Olympus",
                                                     "Browning",
                                                     "Aiwa",
                                                     "JVC",
                                                     "Crate",
                                                     "Peavey",
                                                     "Fender",
                                                     "Yamaha"
                                                 };

        public DesktopSession DesktopSession { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public NewFindMerchandise(DesktopSession desktopSession)
        {
            this.DesktopSession = desktopSession;
            InitializeComponent();

            if (desktopSession != null)
            {
                this.searchButton.BackgroundImage = this.DesktopSession.ResourceProperties.vistabutton_blue;
                this.cancelButton.BackgroundImage = this.DesktopSession.ResourceProperties.vistabutton_blue;
                this.manualEntryButton.BackgroundImage = this.DesktopSession.ResourceProperties.vistabutton_blue;
                this.BackgroundImage = this.DesktopSession.ResourceProperties.newDialog_400_BlueScale;
            }

            Action = ACTION_OK;

            _autoCompleter = new AutoCompleter(
                manufacturerTextBox, 
                suggestedManufacturerListBox, 
                _manufacturers);

            suggestedManufacturerListBox.VisibleChanged +=
                (sender, args) =>
                {
                    if (suggestedManufacturerListBox.Visible)
                        suggestedManufacturerLabel.Visible = true;
                };

        }

        /// <summary>
        /// 
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Manufacturer
        {
            get
            {
                return manufacturerTextBox.Text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Model
        {
            get
            {
                return modelTextBox.Text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ProKnowService.manModelReplyType Reply { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchButton_Click(object sender, EventArgs e)
        {
            //if (manufacturerTextBox.Text != BLANK_STRING && modelTextBox.Text != BLANK_STRING)
            //{
            //    if (textEntryErrorLabel.Visible == true)
            //    {
            //        textEntryErrorLabel.Text = BLANK_STRING;
            //        textEntryErrorLabel.Visible = false;
            //    }
            //    searchButton.Enabled = false;
            //    proKnowWebService.GetAlternateManufacturer(manufacturerTextBox.Text.Trim());
            //    errorFlag = proKnowWebService.InitializeRequest(
            //        new string[] { manufacturerTextBox.Text.Trim(), modelTextBox.Text.Trim() });
            //    if (!errorFlag)
            //    {
            //        errorFlag = proKnowWebService.SendRequest();
            //    }
            //    else
            //    {
            //        textEntryErrorLabel.Visible = true;
            //        textEntryErrorLabel.Text = "Error initializing web service request.";
            //        this.manualEntryButton.Visible = true;
            //    }
            //    if (!errorFlag)
            //    {
            //        errorFlag = proKnowWebService.ProcessResponse();
            //    }
            //    else
            //    {
            //        textEntryErrorLabel.Visible = true;
            //        textEntryErrorLabel.Text = "Error sending web service request.";
            //        this.manualEntryButton.Visible = true;
            //    }
            //    if (!errorFlag)
            //    {
            //        this.action = ACTION_OK;
            //        this.DialogResult = DialogResult.OK;
            //        this.reply = proKnowWebService.Reply;
            //    }
            //    else
            //    {
            //        textEntryErrorLabel.Visible = true;
            //        textEntryErrorLabel.Text =
            //            "No data was found for Manufacturer and Model.";
            //        this.manualEntryButton.Visible = true;
            //    }
            //    searchButton.Enabled = true;
            //}
            //else
            //{
            //    textEntryErrorLabel.Visible = true;
            //    textEntryErrorLabel.Text = "Please enter data for Manufacturer and Model";
            //    this.DialogResult = DialogResult.Retry;
            //    this.action = ACTION_RETRY;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Action = ACTION_CANCEL;
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manualEntryButton_Click(object sender, EventArgs e)
        {
            this.Action = ACTION_MANUAL;
            this.DialogResult = DialogResult.OK;
        }
    }
}