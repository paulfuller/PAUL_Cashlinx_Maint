using System.Windows.Forms;

namespace Common.Libraries.Forms
{
    public partial class ProcessingMessage : Form
    {
        public static readonly string PROCMSG = "Processing Data";
        private string msgString;
        private bool formLoaded;
        public string Message
        {
            set
            {
                msgString = value ?? PROCMSG;
                this.updateMessageLabel();
            }
        }

        private void updateMessageLabel()
        {
            if (!this.formLoaded) return;

            this.MessageLabel.Text = !string.IsNullOrEmpty(this.msgString)
                                        ? this.msgString : PROCMSG;
            this.BringToFront();
            this.Visible = true;
            this.Enabled = true;
            this.TopMost = true;
            if (this.Visible && this.timerToHide.Interval > 0)
            {
                this.timerToHide.Start();
            }
            this.Update();
        }

        public ProcessingMessage(string strMessage, int timeToHide=2000)
        {
            InitializeComponent();
            this.msgString = strMessage ?? PROCMSG;
            this.formLoaded = false;
            this.timerToHide.Interval = timeToHide;
        }

        private void ProcessingMessage_Load(object sender, System.EventArgs e)
        {
            this.formLoaded = true;
            this.updateMessageLabel();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            //If we reach here the message has been up for too long.
            if (this.Visible && this.formLoaded)
            {
                Hide();
                timerToHide.Stop();
            }
        }
    }
}
