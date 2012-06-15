using Common.Libraries.Forms.Components;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;


namespace Pawn.Forms.Retail
{
    public partial class ItemSelect : Form
    {
        public delegate void ShowDescribeMdse();
        public event ShowDescribeMdse ShowDescMerchandise;

        public ItemSelect(List<ScrapItem> availableItems, bool isDuplicateMode)
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTempICN_Click(object sender, EventArgs e)
        {
            ShowDescMerchandise();
        }

    }
}
