using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Libraries.Forms.Components
{
    public partial class CustomComboBox : UserControl
    {
 
        private int x;
        private int y;
        private int w;
        private int h;
        private Rectangle rect;

        public CustomComboBox()
        {
            InitializeComponent();
            this.x = this.ClientRectangle.X;
            this.y = this.ClientRectangle.Y;
            this.w = this.ClientRectangle.Width;
            this.h = this.ClientRectangle.Height;
            rect = new Rectangle(x, y, w, h);

        }

        private void comboBox1_Enter_1(object sender, System.EventArgs e)
        {
            var hwnd = new IntPtr();
            hwnd = this.Handle;
            Graphics g = Graphics.FromHwnd(hwnd);
            var p = new Pen(Color.Blue, 2);
            g.DrawRectangle(p, rect);
            
        }

        private void comboBox1_Leave_1(object sender, System.EventArgs e)
        {
            this.Refresh();
        }

        

    
    }
}
