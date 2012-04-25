using System;
using System.Windows.Forms;
using System.Drawing;

namespace Audit.Logic
{
    public class FormHelper
    {
        public FormHelper(Form form)
        {
            Form = form;
        }

        public Form Form { get; private set; }

        public void Center(Control c)
        {
            CenterHorizontally(c);
            CenterVertically(c);
        }

        public void CenterHorizontally(Control c)
        {
            var monitorSize = GetMonitorSize();
            float halfScreenWidth = monitorSize.Width / 2.0f;
            float halfWidth = c.Width / 2.0f;
            c.Location = new Point((int)(Math.Floor(halfScreenWidth - halfWidth)), c.Location.Y);
        }

        public void CenterVertically(Control c)
        {
            var monitorSize = GetMonitorSize();
            float halfScreenHeight = monitorSize.Height / 2.0f;
            float halfHeight = c.Height / 2.0f;
            c.Location = new Point(c.Location.X, (int)(Math.Floor(halfScreenHeight - halfHeight)));
        }

        public Size GetMonitorSize()
        {
            return SystemInformation.PrimaryMonitorSize;
        }
    }
}
