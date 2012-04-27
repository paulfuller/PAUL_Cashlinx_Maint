using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using CouchConsoleApp.form;
using CouchConsoleApp.test;
using CouchConsoleApp.vo;

namespace CouchConsoleApp.events
{
    public class AddTempDocEventHandler
    {

        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AddTempDocEventHandler));

        private Form1 mainForm = null;


        public AddTempDocEventHandler(Form1 mainForm)
        {
            this.mainForm = mainForm;
        }

        public void BackgroundWorkerForGetDocsProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.log.Debug(e.UserState as string);
        }

        public void BackgroundWorkerForGetDocsDoWork(object sender, DoWorkEventArgs e)
        {
            CouchVo vo = new CouchVo();
            /*vo.userName = "clxuser1";
            vo.pwd = "pa55w0rd1";
            vo.serverName = Properties.Settings.Default.CouchServerName;
            vo.serverport = Properties.Settings.Default.CouchPort;
            vo.dbName = "clx_cust_docs_dev";
            vo.adminUserName = "admin";
            vo.adminPwd = "adminadmin";*/


            vo.userName = "clxuser1";
            //vo.pwd = "pa55w0rd1";
            vo.pwd = "password";
            vo.serverName = Properties.Settings.Default.CouchServerName;
            vo.serverport = Properties.Settings.Default.CouchPort;
            vo.dbName = Properties.Settings.Default.DBName;
            vo.adminUserName = "admin";
            vo.adminPwd = "adminadmin";
            TestPopulateData test = new TestPopulateData();
            test.populateData(vo,false,this.mainForm.getDocID());

            /*TestPopulateData2 test = new TestPopulateData2();
            test.populateData(vo, false, this.mainForm.getDocID());*/
        }


        public void BgWork_GetDocs_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.log.Debug("Canceled!");
            }
            else if (!(e.Error == null))
            {
                this.log.Debug("Error: " + e.Error.Message);
            }
            else
            {
                this.log.Debug("Done!");
            }
        }

    }
}
