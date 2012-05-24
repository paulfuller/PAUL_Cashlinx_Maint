using System.Drawing;
using System.Windows.Forms;

namespace Support.Libraries.Forms.Components
{
    public partial class SupportButton : Button
    {
        public SupportButton()
        {
            InitializeComponent();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.Stretch;
            Cursor = Cursors.Hand;
            FlatAppearance.BorderColor = Color.White;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Margin = new Padding(0);
            MaximumSize = new Size(90, 40);
            MinimumSize = new Size(90, 40);
            Size = new Size(90, 40);


        }
    }
}
