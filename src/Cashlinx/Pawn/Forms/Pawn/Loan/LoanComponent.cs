using System;
using System.Windows.Forms;

namespace Pawn.Forms.Pawn.Loan
{
    public partial class LoanComponent : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public LoanComponent()
        {
            InitializeComponent();
        }

        public void AddComponent(string component)
        {
            if (component != null)
            {
                this.componentListBox.Items.Add(component);
            }
        }

        public void ClearComponents()
        {
            this.componentListBox.Items.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
