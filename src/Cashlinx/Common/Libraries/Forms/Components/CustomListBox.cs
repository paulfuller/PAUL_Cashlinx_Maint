/********************************************************************
* CustomFormElements
* CustomListBox
* Custom ListBox derived from ListBox which handles scrollbar events
* Sreelatha Rengarajan 6/07/2010 Initial version
*******************************************************************/

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Common.Libraries.Forms.Components
{
    public partial class CustomListBox : ListBox
    {

        private const int WM_VSCROLL = 0x115;
        private const int SB_ENDSCROLL = 8;

  

        private struct ScrollInfoStruct
        {
            public int nPos;
        }

        [Category("Action")]
        public event ScrollEventHandler Scrolled = null;

        public CustomListBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void listBox1_BackColorChanged(object sender, System.EventArgs e)
        {
            base.BackColor = this.BackColor;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetScrollInfo(
            IntPtr hWnd, int n, ref ScrollInfoStruct lpScrollInfo);

        protected override void WndProc(ref System.Windows.Forms.Message msg)
        {
            if (msg.Msg == WM_VSCROLL)
            {
                if (Scrolled != null)
                {
                    ScrollInfoStruct si = new ScrollInfoStruct();
                    Marshal.SizeOf(si);
                    GetScrollInfo(msg.HWnd, 0, ref si);

                    if (msg.WParam.ToInt32() == SB_ENDSCROLL)
                    {
                        ScrollEventArgs sargs = new ScrollEventArgs(
                            ScrollEventType.EndScroll,
                            si.nPos);
                        Scrolled(this, sargs);
                    }
                }
            }
            base.WndProc(ref msg);
        }

 
    }
}
