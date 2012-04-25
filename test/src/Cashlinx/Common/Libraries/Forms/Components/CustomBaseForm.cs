using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Common.Libraries.Forms.Components.Behaviors;

namespace Common.Libraries.Forms.Components
{
    public partial class CustomBaseForm : Form
    {
        public CustomBaseForm()
        {
            InitializeComponent();
        }

        protected void HighlightControl(Control control)
        {
            HighlightControl(control, Color.LightPink);
        }

        protected void HighlightControl(Control control, Color color)
        {
            var highlighter = new ControlHighlighter(control, color);
            highlighter.Execute();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Debugger.IsAttached)
            {
                if (keyData == (Keys.OemQuestion | Keys.Control))
                {
                    MessageBox.Show(this.Name);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
