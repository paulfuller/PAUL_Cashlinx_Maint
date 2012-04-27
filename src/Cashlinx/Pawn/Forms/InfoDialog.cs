using System;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms
{
    public partial class InfoDialog : CustomBaseForm
    {
        public bool AlreadyClosed { get; private set; }
        private bool loadedAlready;
        private string messageToShow;
        public string MessageToShow
        {
            get { return (this.messageToShow); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.messageToShow = value;
                    if (!DesignMode && loadedAlready)
                    {
                        this.customLabelMessage.Text = this.messageToShow;
                        this.customLabelMessage.Update();
                    }
                }
            }
        }

        private string headerToShow;
        public string HeaderToShow
        {
            get { return (this.headerToShow); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.headerToShow = value;
                    if (!DesignMode && loadedAlready)
                    {
                        this.customHeaderLabel.Text = this.headerToShow;
                        this.customHeaderLabel.Update();
                    }
                }
            }
        }


        public InfoDialog()
        {
            InitializeComponent();
            loadedAlready = false;
            this.messageToShow = "Message";
            this.headerToShow = "Information Message";
            this.AlreadyClosed = false;
        }

        private void customButtonClose_Click(object sender, EventArgs e)
        {
            this.AlreadyClosed = true;
            this.Close();
        }

        private void InfoDialog_Load(object sender, EventArgs e)
        {
            loadedAlready = true;
            this.customLabelMessage.Text = this.messageToShow;
            this.customHeaderLabel.Text = this.headerToShow;
        }
    }
}
