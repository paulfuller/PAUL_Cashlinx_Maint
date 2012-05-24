using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System.Windows;
using System.Windows.Input;

namespace PawnRulesManagerWPF.Views
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
            
            this.txtUser.Focus();
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }

        private void cmdLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; 
            this.Hide();
        }

        private void txtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void Login()
        {
            if (string.IsNullOrEmpty(this.txtPassword.Password) ||
                string.IsNullOrEmpty(this.txtUser.Text.Trim()))
            {
                System.Windows.Forms.MessageBox.Show("You must enter a username and password.");
            }
            else
            {

                UserName = this.txtUser.Text;
                Password = this.txtPassword.Password;
                this.DialogResult = true;                
                this.Hide();
            }

        }

        private void txtUser_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }


        }
    }
}
