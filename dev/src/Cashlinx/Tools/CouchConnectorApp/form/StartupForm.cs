using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.util;
using CouchConsoleApp.Properties;
using System.Windows.Forms;
using CouchConsoleApp;
using CouchConsoleApp.couch;
using CouchConsoleApp.file;
using CouchConsoleApp.form;
using file;
using CouchConsoleApp.vo;

namespace form.CouchConsoleApp
{
    public partial class CouchConnector : Form
    {
        public string path = null;
        /*public CouchConnector()
        {
            InitializeComponent();
        }*/
        private Form1 mainForm = null;
        private bool isTargetCouchForm = false;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CouchConnector));

        public CouchConnector(string serverName, string port, string dbName)
        {
            InitializeComponent();
            this.cServerNameTxt.Text = serverName;
            this.cServerPortTxt.Text = port;
            this.cDBNameTxt.Text = dbName;
        }

        public CouchConnector(Form1 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
            this.couchLabel.Visible = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.loginB.Visible = false;
            this.exitButton.Visible = false;
            this.adminUserTxt.Visible = true;
            this.adminPwdTxt.Visible = true;
            this.adminUserLbl.Visible = true;
            this.adminPwdLbl.Visible = true;
            this.connectButton.Visible = true;
            this.connect_cancel.Visible = true;
            isTargetCouchForm = true;
            this.cServerNameTxt.Text = Settings.Default.TargetCouchServerName;
            this.cServerPortTxt.Text = Settings.Default.TargetCouchServerPort;
            this.cDBNameTxt.Text = Settings.Default.TargetCouchDBName;
            if (Settings.Default.TempFetch)
            {
                this.uname.Text = "mydbuser";
                this.pwd.Text = "mydbuser";
                this.adminUserTxt.Text = "admin";
                this.adminPwdTxt.Text = "adminadmin";
            }
        }

        private void StartupForm_Load(object sender, EventArgs e)
        {
            if (Settings.Default.TempFetch)
            {
                this.uname.Text = "clxuser1";
                //this.pwd.Text = "pa55w0rd1";
                this.pwd.Text = "password";
            }

        }

        /*private void registerB_Click(object sender, EventArgs e)
        {
            string userNameS = this.uname.Text;
            string pwdS = this.pwd.Text;
            file.FileHandler.init();
            file.FileHandler.register(userNameS, pwdS);
            //Console.WriteLine(SimpleHash.convertToSHA1("hello"));
        }*/

        private void loginB_Click(object sender, EventArgs e)
        {
            CouchVo vo = new CouchVo();
            if (connectAction(ref vo))
            {
                this.Visible = false;
                Form1 form1 = Form1.Instance();
                form1.init(vo);
                //form1.FormBorderStyle = FormBorderStyle.FixedDialog;
                form1.MaximizeBox = false;
                //form1.MinimizeBox = true;
                form1.AutoSize = true;
                form1.StartPosition = FormStartPosition.CenterScreen;
                //form1.WindowState = FormWindowState.Maximized;
                //form1.Size = new Size(865, 1000);
                form1.Show();
            }
        }

        private bool connectAction(ref CouchVo vo)
        {
            if (!ValidateText())
            {
                MessageBox.Show("Please check input");
                return false;
            }

            /*Properties.Settings.Default.CouchServerName = this.cServerNameTxt.Text;
            Properties.Settings.Default.CouchPort = this.cServerPortTxt.Text;
            Properties.Settings.Default.DBName = this.cDBNameTxt.Text;*/
            string userNameS = this.uname.Text;
            string pwdS = this.pwd.Text;

            if (string.IsNullOrEmpty(userNameS) || string.IsNullOrEmpty(pwdS))
            {
                MessageBox.Show("Please enter valid user name and password");
                return false;
            }

            //CouchVo vo = new CouchVo();
            vo.userName = this.uname.Text;
            vo.pwd = this.pwd.Text;
            vo.serverName = this.cServerNameTxt.Text;
            vo.serverport = this.cServerPortTxt.Text;
            vo.dbName = this.cDBNameTxt.Text;
            vo.adminUserName = this.adminUserTxt.Text;
            vo.adminPwd = this.adminPwdTxt.Text;

            CouchUtil util = new CouchUtil(vo);
            this.Enabled = false;
            if (isTargetCouchForm)
            {
                vo = util.couchDualLogin(vo);
            }
            else
            {
                vo = util.couchLogin(vo);
            }
            this.Enabled = true;
            String msg = "Failed to Connect";
            msg += "\n";
            msg += vo.errorMSG;
            if (vo.isError)
            {
                //this.mainForm.appendConsole(msg);
                log.Error(msg);
                MessageBox.Show(msg);
                return false;
            }
            else
            {
                log.Info("Couch Connection Success!!" + vo.serverName+":"+vo.dbName);
                return true;
            }
        }

        private bool ValidateText()
        {
            if (isTargetCouchForm)
            {
                if (string.IsNullOrEmpty(this.uname.Text) || string.IsNullOrEmpty(this.pwd.Text) ||
                    string.IsNullOrEmpty(this.cServerNameTxt.Text) || string.IsNullOrEmpty(this.cServerPortTxt.Text) ||
                    string.IsNullOrEmpty(this.cDBNameTxt.Text) || string.IsNullOrEmpty(this.adminUserTxt.Text) ||
                    string.IsNullOrEmpty(this.adminPwdTxt.Text))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.uname.Text) || string.IsNullOrEmpty(this.pwd.Text) ||
                    string.IsNullOrEmpty(this.cServerNameTxt.Text) || string.IsNullOrEmpty(this.cServerPortTxt.Text) ||
                    string.IsNullOrEmpty(this.cDBNameTxt.Text))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void pwd_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            CouchVo vo = new CouchVo();
            if (connectAction(ref vo))
            {
                this.Visible = false;
                this.mainForm.setTargetCouchVO(vo);
            }else
            {
                vo.serverName = "";
                this.mainForm.setTargetCouchVO(vo);  
            }
        }

        private void connect_cancel_Click(object sender, EventArgs e)
        {
            //CouchVo vo = new CouchVo();
            //this.mainForm.setTargetCouchVO(vo);
            this.Visible = false;
        }
    }
}