/**************************************************************************************************************
* CashlinxDesktop
* ReprintTagSelect
* This form is used to show the result of the tag look up process in the case where there is more than one 
* ICN found
* Dee Bailey 8/31/2009 Initial version
**************************************************************************************************************/

using System;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services
{
    public partial class TagReprintSelect : Form
    {
        public TagReprint frm1;
        DataTable tagInformation = new DataTable();
        public NavBox NavControlBox;
        Form ownerfrm;
        public TagReprintSelect()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
            this.Select(); 
        }

        public void TagReprintSelect_Load(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
            errorLabel.Text = "";
            ownerfrm = this.Owner;
            NavControlBox.Owner = this;
            tagInformation = ((TagReprint)frm1).tagInformation;
            DataRow dr = tagInformation.Rows[0];
            labelBarCodeNumberICN.Text = dr["ICN"].ToString();
            labelDesc.Text = dr["MD_DESC"].ToString();
            labelStatus.Text = dr["status_cd"].ToString();
            labelRetailPrice.Text = dr["RETAIL_PRICE"].ToString();
            labelTicketNumber.Text = dr["ICN_DOC"].ToString();
            labelDocType.Text = dr["ICN_DOC_TYPE"].ToString();
             
                //this.NavControlBox.Action = NavBox.NavAction.CANCEL;
    
        }
        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            TagReprint frm = new TagReprint();
            frm.frm2 = this;
            this.Hide();
            frm.ShowDialog();
            frm.Select();

        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            VerifyInput();
            if (errorLabel.Text.Length > 0)
            {
                errorLabel.Visible = true;
            }
            else
            {
                //go to print engine
            }
            }
        private void VerifyInput()
        {
            errorLabel.Text = "";
            if (textBoxFeaturesPrint.Text.ToUpper().Trim() == "Y" ||
              textBoxFeaturesPrint.Text.ToUpper().Trim() == "N")
            { }
            else
            {
               if (errorLabel.Text.Length > 0)
                    errorLabel.Text += System.Environment.NewLine + Commons.GetMessageString("TagReprintFeatureTagMsg");
                else
                    errorLabel.Text = Commons.GetMessageString("TagReprintFeatureTagMsg");
            }
            if (customTextBoxNumberToPrint.Text.Length < 1)
            {
                if (errorLabel.Text.Length > 0)
                    errorLabel.Text += System.Environment.NewLine + Commons.GetMessageString("TagReprintBarCodePrintNumber");
                else
                    errorLabel.Text = Commons.GetMessageString("TagReprintBarCodePrintNumber");
            }       
        }


   }
}
