/*********************************************************************************
 * Namespace:       CommonUI.DesktopForms.Pawn.Products.DescribeMerchandise
 * Class:           DescribeStones_Images
 * 
 * Description      Popup Form to show images of different stones.
 * 
 * History
 * David D Wise, Initial Development
 * 
 *********************************************************************************/

using System;
using System.Windows.Forms;
using Common.Controllers.Application;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class DescribeStones_Images : Form
    {
        private bool _Setup;
        public delegate void ClarityHandler(int iClarityIndex);
        public event ClarityHandler UpdateClarity;

        public DesktopSession DesktopSession { get; private set; }

        public DescribeStones_Images(DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
            _Setup = true;
            InitializeComponent();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            Setup();
        }

        private void DescribeStones_Images_Load(object sender, EventArgs e)
        {

        }

        private void Setup()
        {
            this.closeButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.radioButton1.Image = DesktopSession.ResourceProperties.cl1;
            this.radioButton2.Image = DesktopSession.ResourceProperties.cl2;
            this.radioButton3.Image = DesktopSession.ResourceProperties.cl3;
            this.radioButton4.Image = DesktopSession.ResourceProperties.cl4;
            this.radioButton5.Image = DesktopSession.ResourceProperties.cl5;
            this.BackgroundImage = DesktopSession.ResourceProperties.newDialog_400_BlueScale;

            //int iRowCount = 0;
            _Setup = false;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            exitPage();
        }

        private void exitPage()
        {
            this.Close();
        }

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            if(_Setup) return;

            UpdateClarity(((RadioButton)sender).TabIndex);
            exitPage();
        }
    }
}
