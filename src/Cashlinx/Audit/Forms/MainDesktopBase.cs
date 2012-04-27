using System.Drawing;
using System.Windows.Forms;
using Audit.Logic;

namespace Audit
{
    public partial class MainDesktopBase : Form
    {
        public MainDesktopBase()
        {
            InitializeComponent();
            FormHelper = new FormHelper(this);
        }

        protected AuditDesktopSession ADS
        {
            get { return AuditDesktopSession.Instance; }
        }

        private FormHelper FormHelper { get; set; }

        protected void Center(Control c)
        {
            FormHelper.Center(c);
        }

        protected void CenterHorizontally(Control c)
        {
            FormHelper.CenterHorizontally(c);
        }

        protected void CenterVertically(Control c)
        {
            FormHelper.CenterVertically(c);
        }

        protected Size GetMonitorSize()
        {
            return FormHelper.GetMonitorSize();
        }
    }
}
