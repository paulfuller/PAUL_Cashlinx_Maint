using System;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms.Retail
{
    public partial class ItemSearchResults : CustomBaseForm
    {
        public delegate void ShowDescribeMdse();

        public string Message
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }

        public event ShowDescribeMdse ShowDescMerchandise;
        public ItemSearchResultsMode ResultsMode { get; set; }

        public DesktopSession DesktopSession { get; private set; }

        public ItemSearchResults(DesktopSession desktopSession, ItemSearchResultsMode mode)
        {
            DesktopSession = desktopSession;
            InitializeComponent();

            ResultsMode = mode;

            btnTempICN.Visible = ResultsMode != ItemSearchResultsMode.CHANGE_RETAIL_PRICE && ResultsMode != ItemSearchResultsMode.REPRINT_TAG && ResultsMode != ItemSearchResultsMode.CHARGEOFF;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTempICN_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowDescMerchandise();
        }

    }
}
