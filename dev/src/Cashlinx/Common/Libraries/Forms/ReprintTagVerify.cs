using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Retail;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;

namespace Common.Libraries.Forms
{
    public partial class ReprintTagVerify : CustomBaseForm
    {
        public ReprintTagVerify(DesktopSession desktopSession, Item item, ReprintVerifySender senderobject)
        {
            InitializeComponent();
            DesktopSession = desktopSession;
            Item = item;
            SenderObject = senderobject;
            txtTagsToReprint.Text = "1";
        }

        public ReprintVerifySender SenderObject {get;set;}

        public DesktopSession DesktopSession { get; private set; }
        public Item Item { get; set; }

        private void ReprintTag_Load(object sender, EventArgs e)
        {
            if (Item == null)
            {
                btnComplete.Enabled = false;
                return;
            }

            Icn icn = new Icn(Item.Icn);
            lblDocType.Text = ((int)icn.DocumentType).ToString();
            lblIcn.Text = icn.GetFullIcn();
            txtItemDescription.Text = Item.TicketDescription;
            lblRetailPrice.Text = Item.RetailPrice.ToString("c");
            lblStatus.Text = Item.ItemStatus.ToString();
            lblTicketNumber.Text = icn.DocumentNumber.ToString();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            bool printFeatures = false;
            if (chkPrintFeaturesTag.Checked)
                printFeatures = true;
            Item.PfiTags = Utilities.GetIntegerValue(txtTagsToReprint.Text);
            try
            {
                DesktopSession.PrintTags(Item, printFeatures);
                this.Hide();
                MessageBox.Show("Tag(s) printed successfully.");
                if (SenderObject == ReprintVerifySender.ChangeRetailPrice)
                {
                    ChangeRetailPriceSearch itemSearchFrm = new ChangeRetailPriceSearch(DesktopSession);
                    Form currForm = DesktopSession.HistorySession.Lookup(itemSearchFrm);
                    if (currForm.GetType() == typeof(ChangeRetailPriceSearch))
                    {
                        currForm.Show();
                    }
                }
                this.Close();
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Cannot re-print tag.  Exception thrown: {0} {1}", eX, eX.Message);
                }
                MessageBox.Show("Failed to print tag.  Please try again.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        
        private void ReprintTagVerify_Shown(object sender, EventArgs e)
        {
            txtTagsToReprint.Focus();
        }

        private void txtTagsToReprint_Leave(object sender, EventArgs e)
        {
            int tags;

            if (!int.TryParse(txtTagsToReprint.Text, out tags))
            {
                MessageBox.Show("Inavlid number of tags entry.");
                txtTagsToReprint.RevertValue();
                txtTagsToReprint.Focus();
            }
        }
    }
}
