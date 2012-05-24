/**********************************************************************************
 * Namespace:       CommonUI.DesktopForms.Pawn.Products.DescribeMerchandise
 * Class:           AlternateManufacturer
 * 
 * Description      Popup Dialog Form to allow End User to select correct 
 *                  Manufacturer
 * 
 * History
 * David D Wise, Initial Development
 *  PWNU00000602 SMurphy 4/2/2010 - problem with no secondary manufacturer or model when 
 *      alternate manufacturer is possible
 * no ticket Smurphy 4/21/2010 deleted VScrollbar - turned panel AutoScroll on and AutoSize off
 *      and removed unused "usings"
 ************************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Shared;
using Common.ProKnowService;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class AlternateManufacturer : Form
    {
        private ActiveManufacturer _ActiveManufacturerModel;               // Stores which Manufacturer is Active
        public string SelAltManu;
        public List<manModType> ManModTypes
        {
            get;
            set;
        }
        public ProKnowMatch SelectedProKnowMatch
        {
            get;
            set;
        }

        public DesktopSession DesktopSession { get; private set; }

        public AlternateManufacturer(DesktopSession desktopSession, ref ActiveManufacturer aManufacturerModel)
        {
            DesktopSession = desktopSession;
            InitializeComponent();
            this.cancelButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.continueButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.BackgroundImage = DesktopSession.ResourceProperties.newDialog_400_BlueScale;
            _ActiveManufacturerModel = aManufacturerModel;
        }

        private void AlternateManufacturer_Load(object sender, EventArgs e)
        {
            Setup();
        }

        private void Setup()
        {
            int iRowCount = 0;

            RadioButton selectedModTypeRadioButton = new RadioButton
            {
                AutoSize = true,
                ForeColor = Color.Black,
                Text = SelectedProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText,
                Tag = SelectedProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerCode,
                TabIndex = 1,
                Checked = true
            };
            optionsTableLayoutPanel.Controls.Add(selectedModTypeRadioButton, 0, iRowCount);

            foreach (manModType manModType in ManModTypes)
            {
                iRowCount++;
                RadioButton modTypeRadioButton = new RadioButton
                {
                    AutoSize = true,
                    ForeColor = Color.Black,
                    Text = manModType.description,
                    Tag = manModType.answerNumber,
                    TabIndex = 1
                };
                optionsTableLayoutPanel.Controls.Add(modTypeRadioButton, 0, iRowCount);
            }
            //Smurphy - deleted VScrollbar - turned panel AutoScroll on and AutoSize off
            //verticalScrollBar.Minimum = 0;
            //verticalScrollBar.Maximum = optionsTableLayoutPanel.Height - optionPanel.Height;

            string sTitleText = "Alternate Manufacturers were found.  Please choose a manufacturer ";
            sTitleText += "to continue";
            lblTitleText.Text = sTitleText;

            if (optionsTableLayoutPanel.Controls.Count > 0)
            {
                optionsTableLayoutPanel.Controls[0].Focus();
            }
        }
        //Smurphy - deleted VScrollbar - turned panel AutoScroll on and AutoSize off
        //private void verticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        //{
        //    optionsTableLayoutPanel.Location = new System.Drawing.Point(0, (0 - verticalScrollBar.Value));
        //    optionsTableLayoutPanel.Refresh();
        //}

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Instantiate Answer object and populate with 999 answer Code with current Manufacturer
            Answer manufacturerAnswer = new Answer();
            manufacturerAnswer.AnswerCode = 0;
            manufacturerAnswer.AnswerText = SelectedProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText;
            //PWNU00000602 SMurphy secondary manufacturer & model were being lost if from this screen because the DescribeItem screen looks for 
            //non-null in the SelectedProKnowMatch.manufacturerModelInfo[3].InputKey - it was not repopulated by ProKnow call since this 
            //value is populated for the primary manufacturer - not the secondary...
            manufacturerAnswer.InputKey = SelectedProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].InputKey;
            manufacturerAnswer.OutputKey = SelectedProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].OutputKey;
            SelectedProKnowMatch.manufacturerModelInfo.RemoveAt((int)_ActiveManufacturerModel);
            SelectedProKnowMatch.manufacturerModelInfo.Insert((int)_ActiveManufacturerModel, manufacturerAnswer);

            exitPage(false);
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            // Instantiate Answer object and populate with current information
            Answer manufacturerAnswer = new Answer();
            manufacturerAnswer.AnswerCode = SelectedProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerCode;
            manufacturerAnswer.AnswerText = SelectedProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].AnswerText;
            //PWNU00000602 SMurphy secondary manufacturer & model were being lost if from this screen because the DescribeItem screen looks for 
            //non-null in the SelectedProKnowMatch.manufacturerModelInfo[3].InputKey - it was not repopulated by ProKnow call since this 
            //value is populated for the primary manufacturer - not the secondary...
            manufacturerAnswer.InputKey = SelectedProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].InputKey;
            manufacturerAnswer.OutputKey = SelectedProKnowMatch.manufacturerModelInfo[(int)_ActiveManufacturerModel].OutputKey;

            // Loop through the RadioButtons that were dynamically added to the Panel
            foreach (Control myControl in optionsTableLayoutPanel.Controls)
            {
                if (myControl.GetType() == typeof(RadioButton))
                {
                    // Find the selected RadioButton and escape the loop
                    if (((RadioButton)myControl).Checked)
                    {
                        manufacturerAnswer.AnswerText = myControl.Text;
                        manufacturerAnswer.AnswerCode = Convert.ToInt32(myControl.Tag);
                        break;
                    }
                }
            }

            SelectedProKnowMatch.manufacturerModelInfo.RemoveAt((int)_ActiveManufacturerModel);
            SelectedProKnowMatch.manufacturerModelInfo.Insert((int)_ActiveManufacturerModel, manufacturerAnswer);

            exitPage(true);
        }

        private void exitPage(bool bSuccess)
        {
           /* Form previousForm = CashlinxDesktopSession.Instance.HistorySession.Back();
            if (typeof(DescribeMerchandise) == previousForm.GetType())
            {
                ((DescribeMerchandise)previousForm).ReturnFromAlternateMerchandise(bSuccess);
            }*/
            //Since this form is opened as show dialog we need only to close the form
            this.Close();
        }
    }
}
