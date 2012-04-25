/**********************************************************************************
* Namespace:       CashlinxDesktop.DesktopForms.Pawn.ShopAdministration.Assignments
* Class:           EmployeeSearch
* 
* Description      Initial form used during Assign User Limits and Resources.
* 
* History
* David D Wise, Initial Development
* 
**********************************************************************************/
using System;
using System.Windows.Forms;

namespace Pawn.Forms.Pawn.ShopAdministration.Assignments
{
    public partial class EmployeeSearch : Form
    {
        public EmployeeSearch()
        {
            InitializeComponent();
        }

        /*  private void searchButton_Click(object sender, EventArgs e)
        {
        lblMessage.Text = "";

        if(employeeNumberTextBox.Text != "")
        {
        int iEmployeeNumber = Convert.ToInt32(employeeNumberTextBox.Text);
        string _ShopID = "";

        if (iEmployeeNumber == 1)
        {
        lblMessage.Text = "You have entered an invalid Employee ID.  Please try again.";
        }
        else if (iEmployeeNumber == 2)
        {
        lblMessage.Text = "The Employee ID you entered is not active.  Please enter another employee number.";
        }
        else if (iEmployeeNumber == 3)
        {
        lblMessage.Text = "This user does not have a shop operations role.  Please enter another employee number.";
        }
        else
        {
        SecurityProfile myForm = new SecurityProfile(_ShopID, iEmployeeNumber);
        myForm.ShowDialog();
        }
        }
        else if(shopNumberComboBox.SelectedItem.ToString() != "---")
        {
        SelectEmployee myForm = new SelectEmployee();
        myForm.ShowDialog();

        }
        else
        {
        MessageBox.Show(
        "Need to either select a Shop ID or enter Employee Number.",
        "User Action",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning);
        }
        }*/

        private void shopIDComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void EmployeeSearch_Load(object sender, EventArgs e)
        {
            //Get the list of shops that the logged in user has access to
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void customButtonSearch_Click(object sender, EventArgs e)
        {
            if (shopNumberComboBox.SelectedIndex < 0 && employeeNumberTextBox.Text.Length == 0)
            {
                lblMessage.Text = @"Select a shop number or enter employee number to search";
                return;
            }
        }
    }
}