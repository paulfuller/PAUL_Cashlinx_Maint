/*********************************************************************************
 * Namespace:       CommonUI.DesktopForms.Pawn.Products.DescribeMerchandise
 * Class:           ManufacturerModelMatches
 * 
 * Description      Popup Form to select specific Manufacture Model of an Item.
 * 
 * History
 * David D Wise, Initial Development
 *  no ticket SMurphy 4/1/2010 app crashes if nothing has been selected 
 *  no ticket SMurphy 4/22/2010 added default option button selected and removed unused "usings"
 *********************************************************************************/

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
    public partial class ManufacturerModelMatches : Form
    {
        private ActiveManufacturer _ActiveManufacturerModel;               // Stores which Manufacturer is Active

        public DesktopSession DesktopSession { get; private set; }
        public Form ReturnForm {get;set;}
        public List<manModelMatchType> SelectedManModelMatchTypes { get; set; }
        public ProKnowMatch SelectedProKnowMatch { get; set; }

        public ManufacturerModelMatches(DesktopSession desktopSession, ref ActiveManufacturer aManufacturerModel)
        {
            DesktopSession = desktopSession;
            InitializeComponent();
            this.continueButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.cancelButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.BackgroundImage = DesktopSession.ResourceProperties.newDialog_400_BlueScale;
            _ActiveManufacturerModel = aManufacturerModel;
        }

        private void ManufacturerModelMatches_Load(object sender, EventArgs e)
        {
            Setup();
        }

        private void Setup()
        {
            int iRowCount = 0;

            foreach (manModelMatchType manModelMatchType in SelectedManModelMatchTypes)
            {
                CategoryNode cnCategoryNodeWalker = DesktopSession.CategoryXML.GetMerchandiseCategory(manModelMatchType.categoryCode.ToString());

                if (!cnCategoryNodeWalker.Error)
                {
                    RadioButton modTypeRadioButton = new RadioButton
                    {
                        AutoSize = true,
                        ForeColor = Color.Black,
                        Text = cnCategoryNodeWalker.Description,
                        Tag = cnCategoryNodeWalker.CategoryCode,
                        TabIndex = iRowCount 
                    };
                    modTypeRadioButton.Checked = iRowCount == 0 ? true : false;
                    optionsTableLayoutPanel.Controls.Add(modTypeRadioButton, 0, iRowCount);
                    iRowCount++;
                }
            }

            verticalScrollBar.Minimum = 0;
            verticalScrollBar.Maximum = optionsTableLayoutPanel.Height - optionPanel.Height;

            string sTitleText = "Several categories were found.  Please choose which category ";
            sTitleText += "you would like to continue with";
            lblTitleText.Text = sTitleText;

            if (optionsTableLayoutPanel.Controls.Count > 0)
            {
                optionsTableLayoutPanel.Controls[0].Focus();
            }
        }

        private void verticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            optionsTableLayoutPanel.Location = new System.Drawing.Point(0, (0 - verticalScrollBar.Value));
            optionsTableLayoutPanel.Refresh();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            exitPage(false, 0);
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            int iIdx = 0;
            // Loop through the RadioButtons that were dynamically added to the Panel
            foreach (Control myControl in optionsTableLayoutPanel.Controls)
            {
                if (myControl.GetType() == typeof(RadioButton))
                {
                    // Find the selected RadioButton and escape the loop
                    if (((RadioButton)myControl).Checked)
                    {
                        SelectedProKnowMatch.primaryCategoryCodeDescription = myControl.Text;
                        SelectedProKnowMatch.primaryCategoryCode = Convert.ToInt32(myControl.Tag);
                        break;
                    }
                    iIdx++;
                }
            }
            //no ticket SMurphy 4/1/2010 app crashes if nothing has been selected
            if (iIdx == optionsTableLayoutPanel.Controls.Count)
            {
                exitPage(false, iIdx);
            }
            else
            {
                exitPage(true, iIdx);
            }
        }

        private void exitPage(bool bSuccess, int iIdx)
        {
            this.Hide();
            if (typeof(Common.Libraries.Forms.Pawn.Products.DescribeMerchandise.DescribeMerchandise) == ReturnForm.GetType())
            {
                ((Common.Libraries.Forms.Pawn.Products.DescribeMerchandise.DescribeMerchandise)ReturnForm).ReturnFromManufacturerModelMerchandise(bSuccess, iIdx);
            }
            this.Close();
        }
    }
}
