using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CouchConsoleApp.couch;
using CouchConsoleApp.vo;


namespace CouchConsoleApp.form
{
    public partial class CouchWebTestForm : Form
    {
        public CouchWebTestForm()
        {
            InitializeComponent();
            this.Show();
        }

        private void GetDocButton_Click(object sender, EventArgs e)
        {
            CouchVo vo = new CouchVo();
            vo.serverName = "localhost";
            vo.serverport = "5984";
            vo.userName = "clxuser1";
            vo.pwd = "pa55w0rd1";
            vo.serverName = Properties.Settings.Default.CouchServerName;
            vo.serverport = Properties.Settings.Default.CouchPort;
            vo.dbName = "clx_cust_docs_dev";
            vo.adminUserName = "admin";
            vo.adminPwd = "adminadmin";
            PawnDocRegVO regvo=new PawnDocRegVO();
            if (string.IsNullOrEmpty(this.doctext.Text))
                return;
            regvo.StorageID = this.doctext.Text;
            string errorText = "";
            bool isError = false;
           /* Document doc=CouchArchiverGetHelper.getInstance().GetDocument(out errorText,out isError, vo, regvo);
            MessageBox.Show("Is Error " + isError + " Text " + errorText);*/

            string rdoc = CouchArchiverGetHelper.getInstance().GetRawDocument(out errorText, out isError, vo, regvo);
            MessageBox.Show("Is Error " + isError + " Text " + errorText);

            bool addDocError = false;
            string addError = "";
            vo.dbName = "clx_11_2010";
            vo.userName = "clxuser1";
            vo.pwd = "pa55w0rd1";
            
            bool added=CouchArchiverAddHelper.getInstance().Document_Add(rdoc,out addDocError,out addError,vo, regvo.StorageID);

            if(added)
            {
                MessageBox.Show(" Added ,,,,,Is Error " + isError + " Text " + addError); 
            }else
            {
                MessageBox.Show("Not Added ,,,,,Is Error " + isError + " Text " + addError); 
            }
            
            
        }
    }
}
