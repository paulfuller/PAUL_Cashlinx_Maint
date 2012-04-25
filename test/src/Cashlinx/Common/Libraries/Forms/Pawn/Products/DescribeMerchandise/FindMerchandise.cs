using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class FindMerchandise : Form
    {
        private Dictionary<string, string> manufacturerDictionary;
        private WebServiceProKnow proKnowWebService;
        private AutoCompleteStringCollection autoComplete;

        private const string ACTION_OK = "OK";
        private const string ACTION_MANUAL = "MANUAL";
        private const string ACTION_CANCEL = "CANCEL";

        public DesktopSession DesktopSession { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public FindMerchandise(DesktopSession desktopSession)
        {
            this.DesktopSession = desktopSession;
            InitializeComponent();

            this.searchButton.BackgroundImage = this.DesktopSession.ResourceProperties.vistabutton_blue;
            this.cancelButton.BackgroundImage = this.DesktopSession.ResourceProperties.vistabutton_blue;
            this.manualEntryButton.BackgroundImage = this.DesktopSession.ResourceProperties.vistabutton_blue;
            this.BackgroundImage = this.DesktopSession.ResourceProperties.newDialog_400_BlueScale;

            manufacturerDictionary = new Dictionary<string, string>();
            autoComplete = new AutoCompleteStringCollection();

            LoadManufacturerDictionary();

            proKnowWebService = new WebServiceProKnow(this.DesktopSession);

            Action = ACTION_OK;

            this.manufacturerTextBox.AutoCompleteCustomSource = this.autoComplete;
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
        private void FindMerchandise_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadManufacturerDictionary()
        {
            if (manufacturerDictionary != null)
            {
                manufacturerDictionary.Add("Bose", "Bose");
                autoComplete.Add("Bose");
                manufacturerDictionary.Add("Sony", "Sony");
                autoComplete.Add("Sony");
                manufacturerDictionary.Add("Nikon", "Nikon");
                autoComplete.Add("Nikon");
                manufacturerDictionary.Add("Compaq", "Compaq");
                autoComplete.Add("Compaq");
                manufacturerDictionary.Add("Glock", "Glock");
                autoComplete.Add("Glock");
                manufacturerDictionary.Add("Brass Eagle", "Brass Eagle");
                autoComplete.Add("Brass Eagle");
                manufacturerDictionary.Add("Citizen", "Citizen");
                autoComplete.Add("Citizen");
                manufacturerDictionary.Add("Selmer", "Selmer");
                autoComplete.Add("Selmer");
                manufacturerDictionary.Add("Huffy", "Huffy");
                autoComplete.Add("Huffy");
                manufacturerDictionary.Add("Toshiba", "Toshiba");
                autoComplete.Add("Toshiba");
                manufacturerDictionary.Add("Toro", "Toro");
                autoComplete.Add("Toro");
                manufacturerDictionary.Add("RCA", "RCA");
                autoComplete.Add("RCA");
                manufacturerDictionary.Add("Echo", "Echo");
                autoComplete.Add("Echo");
                manufacturerDictionary.Add("Sylvania", "Sylvania");
                autoComplete.Add("Sylvania");
                manufacturerDictionary.Add("Nintendo", "Nintendo");
                autoComplete.Add("Nintendo");
                manufacturerDictionary.Add("Maestro", "Maestro");
                autoComplete.Add("Maestro");
                manufacturerDictionary.Add("Motorola", "Motorola");
                autoComplete.Add("Motorola");
                manufacturerDictionary.Add("Haeir", "Haeir");
                autoComplete.Add("Haeir");
                manufacturerDictionary.Add("Bissell", "Bissell");
                autoComplete.Add("Bissell");
                manufacturerDictionary.Add("Benelli", "Benelli");
                autoComplete.Add("Benelli");
                manufacturerDictionary.Add("Stihl", "Stihl");
                autoComplete.Add("Stihl");
                manufacturerDictionary.Add("Daewoo", "Daewoo");
                autoComplete.Add("Daewoo");
                manufacturerDictionary.Add("Bostitch", "Bostitch");
                autoComplete.Add("Bostitch");
                manufacturerDictionary.Add("Shimano", "Shimano");
                autoComplete.Add("Shimano");
                manufacturerDictionary.Add("Roland", "Roland");
                autoComplete.Add("Roland");
                manufacturerDictionary.Add("QEP", "QEP");
                autoComplete.Add("QEP");
                manufacturerDictionary.Add("Mitchell", "Mitchell");
                autoComplete.Add("Mitchell");
                manufacturerDictionary.Add("Bosch", "Bosch");
                autoComplete.Add("Bosch");
                manufacturerDictionary.Add("Skil", "Skil");
                autoComplete.Add("Skil");
                manufacturerDictionary.Add("Samsung", "Samsung");
                autoComplete.Add("Samsung");
                manufacturerDictionary.Add("Apple", "Apple");
                autoComplete.Add("Apple");
                manufacturerDictionary.Add("Emerson", "Emerson");
                autoComplete.Add("Emerson");
                manufacturerDictionary.Add("Casio", "Casio");
                autoComplete.Add("Casio");
                manufacturerDictionary.Add("Sanyo", "Sanyo");
                autoComplete.Add("Sanyo");
                manufacturerDictionary.Add("Olympus", "Olympus");
                autoComplete.Add("Olympus");
                manufacturerDictionary.Add("Browning", "Browning");
                autoComplete.Add("Browning");
                manufacturerDictionary.Add("Aiwa", "Aiwa");
                autoComplete.Add("Aiwa");
                manufacturerDictionary.Add("JVC", "JVC");
                autoComplete.Add("JVC");
                manufacturerDictionary.Add("Crate", "Crate");
                autoComplete.Add("Crate");
                manufacturerDictionary.Add("Peavey", "Peavey");
                autoComplete.Add("Peavey");
                manufacturerDictionary.Add("Fender", "Fender");
                autoComplete.Add("Fender");
                manufacturerDictionary.Add("Yamaha", "Yamaha");
                autoComplete.Add("Yamaha");
            }
        }

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
        private void manufacturerTextBox_TextChanged(object sender, EventArgs e)
        {
            if (manufacturerTextBox.Text.Length >= 3)
            {
                if (!suggestedManufacturerLabel.Visible && !suggestedManufacturerListBox.Visible)
                {
                    suggestedManufacturerListBox.Visible = true;
                    suggestedManufacturerLabel.Visible = true;
                }

                foreach (string s in manufacturerDictionary.Keys)
                {
                    bool containsString = suggestedManufacturerListBox.Items.Contains(s);
                    bool startsWith = s.ToLower().StartsWith(
                        manufacturerTextBox.Text.ToString().ToLower());

                    if (startsWith)
                    {
                        if (!containsString)
                        {
                            suggestedManufacturerListBox.Items.Add(s);
                        }
                    }
                    else if (!startsWith && containsString)
                    {
                        suggestedManufacturerListBox.Items.Remove(s);
                    }
                }
            }
            else
            {
                suggestedManufacturerListBox.Items.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modelTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void suggestedManufacturerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            manufacturerTextBox.Text = suggestedManufacturerListBox.SelectedItem.ToString();
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