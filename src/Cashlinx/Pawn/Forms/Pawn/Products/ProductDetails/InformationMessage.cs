using System;
using System.Windows.Forms;

namespace Pawn.Forms.Pawn.Products.ProductDetails
{
    public partial class InformationMessage : Form
    {
        public bool ShowRefresh
        {
            get;
            set;
        }
 

        public InformationMessage(string strMessage)
        { 
            InitializeComponent();
            if (strMessage.Length != 0)
                customLabelMessage.Text = strMessage;
            else
                this.Close();
        }


        private void customButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InformationMessage_Load(object sender, EventArgs e)
        {
            if (ShowRefresh)
                this.customButtonRefresh.Visible = true;
        }

        private void customButtonRefresh_Click(object sender, EventArgs e)
        {

        }
    }
}
