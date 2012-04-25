using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Objects;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class JewelryDisplay : Form
    {
        public static Dictionary<string, Bitmap> JEWELRY { get; private set; }

        public JewelryDisplay(ResourceProperties resourceProperties)
        {
            JEWELRY = new Dictionary<string, Bitmap>()
        {
            {"Pearl Necklace", resourceProperties.Pearl},
            {"Herring Bone Necklace", resourceProperties.HB},
            {"Heart Diamond Pendant", resourceProperties.HP},
            {"Diamond Pendant", resourceProperties.DP}
        };

            InitializeComponent();
            this.Hide();
        }

        public bool ShowJewelry(Form parent, string nm)
        {
            if (parent == null || string.IsNullOrEmpty(nm))
                return (false);

            if (!JEWELRY.ContainsKey(nm))
                return (false);

            //Ensure we have the entry
            Bitmap entry = JEWELRY[nm];

            if (entry == null)
                return (false);

            int parentX = parent.Location.X;
            int parentY = parent.Location.Y;

            int parentH = parent.Height;
            int parentW = parent.Width;

            int thisFormH = this.Height;
            int formHDiff = parentH - thisFormH;
            if (formHDiff > 0)
            {
                formHDiff = formHDiff / 2;
            }
            else
            {
                formHDiff = 0;
            }

            this.Location = new Point((parentX + parentW), (parentY + formHDiff));

            this.jewelryNameLabel.Text = nm;
            this.jewelryPictureBox.Image = entry;
            this.Update();
            this.Show();

            return (true);
        }

        public void HideJewelry()
        {
            this.SendToBack();
            this.Hide();
            this.Visible = false;
        }
    }
}
