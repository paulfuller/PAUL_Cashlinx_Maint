using System.Drawing;
using System.Windows.Forms;

namespace Common.Libraries.Forms.Components
{
    public partial class CustomButton : Button
    {
        public CustomButton()
        {
            InitializeComponent();
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            BackColor = System.Drawing.Color.Transparent;
            BackgroundImageLayout = ImageLayout.None;
            Cursor = Cursors.Hand;
            FlatAppearance.BorderColor = Color.White;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Margin = new Padding(0);
            MaximumSize = new Size(100, 50);
            MinimumSize = new Size(100, 50);
            Size = new Size(100, 50);


        }


  

 

    }
}
