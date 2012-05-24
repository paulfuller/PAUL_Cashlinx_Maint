using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CouchConsoleApp.db;
using form.CouchConsoleApp;

namespace CouchConsoleApp.form
{
    public partial class DBParamsForm : Form
    {
        private Form1 mainForm = null;

        public DBParamsForm()
        {

            InitializeComponent();
            popDBVal();
        }

        public DBParamsForm(Form1 mainForm)
        {

            this.mainForm = mainForm;
            InitializeComponent();
            popDBVal();
        }

        private void popDBVal()
        {

            /*this.dbuname.Text = "ccsowner";
            this.dbpwd.Text = "prime98s";
            this.dbSIDTxt.Text = "CLXD3";
            this.dbServerNameTxt.Text = "clxdbdev";
            this.dbServerPortTxt.Text = "1521";*/

            if (Properties.Settings.Default.TempFetch)
            {
                this.dbuname.Text = "ccsowner";
                this.dbpwd.Text = "prime98s";
            }

            this.dbSIDTxt.Text = Properties.Settings.Default.DBSID;
            this.dbServerNameTxt.Text = Properties.Settings.Default.DBServerName;
            this.dbServerPortTxt.Text = Properties.Settings.Default.DBPort;


            /*this.dbuname.Text = "ccsowner";
            this.dbpwd.Text = "prime98s";
            this.dbSIDTxt.Text = "CLXPP";
            this.dbServerNameTxt.Text = "clxppreprod.vip.casham.com";
            this.dbServerPortTxt.Text = "1524";*/


        }

        private void dbcancelbutton_Click(object sender, EventArgs e)
        {
            mainForm.setTargetDB(DBConnector.getInstance());
            this.Close();
        }

        private void dbconnectButton_Click(object sender, EventArgs e)
        {
            DBConnector dbConn = DBConnector.getInstance();

            if ((String.IsNullOrEmpty(this.dbuname.Text) ||
                String.IsNullOrEmpty(this.dbpwd.Text) ||
                String.IsNullOrEmpty(this.dbServerNameTxt.Text) ||
                String.IsNullOrEmpty(this.dbServerPortTxt.Text) ||
                String.IsNullOrEmpty(this.dbSIDTxt.Text)))
            {
                MessageBox.Show("Please enter values for all parameters");
            }

            string msg = null;

            bool useConnectionPool = true;
            string minConnection = "100";
            string maxConnection = "500";
            string connLife = "120";
            string connTimeout = "60";

            
            useConnectionPool = Properties.Settings.Default.DB_Conn_Use_Pool;
            
            if(!string.IsNullOrEmpty( Properties.Settings.Default.DB_Min_PoolSize))
            {
                minConnection = Properties.Settings.Default.DB_Min_PoolSize;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.DB_Max_PoolSize))
            {
                maxConnection = Properties.Settings.Default.DB_Max_PoolSize;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.DB_Conn_Life_min))
            {
                connLife = Properties.Settings.Default.DB_Conn_Life_min;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.DB_Conn_Timeout_min))
            {
                connTimeout = Properties.Settings.Default.DB_Conn_Timeout_min;
            }

            bool initRet = dbConn.initialize(this.dbuname.Text, this.dbpwd.Text, this.dbServerNameTxt.Text,
            this.dbServerPortTxt.Text, this.dbSIDTxt.Text, "CCSOWNER", useConnectionPool,minConnection, maxConnection,connLife , connTimeout, "10", "10", out msg);

            {

                if (initRet)
                {

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Connection Failed :" + msg);
                    //Form1.appendConsole("Db connection Failed" + msg);
                }
                mainForm.setTargetDB(dbConn);

            }
        }
    }
}
