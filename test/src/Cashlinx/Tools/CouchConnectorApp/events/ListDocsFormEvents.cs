using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CouchConsoleApp.db;
using CouchConsoleApp.form;

namespace CouchConsoleApp.events
{
    public class ListDocsFormEvents
    {
        private Form1 mainForm = null;
        private ListDocsForm docForm = null;
        private DataTable resultTable;
        private BindingSource resultBindingSource;


        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ListDocsFormEvents));



        public ListDocsFormEvents(Form1 mainForm, ListDocsForm listDocsForm)
        {
            this.mainForm = mainForm;
            this.docForm = docForm;
        }


        public void BackgroundWorkerForGetDocsProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.log.Debug(e.UserState as string);
        }


        public void BackgroundWorkerForGetDocsDoWork(object sender, DoWorkEventArgs e)
        {
            prepateDataToDisplay();

        }

        public void BgWork_GetDocs_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.log.Debug("Canceled!");
                mainForm.enableGo();
            }
            else if (!(e.Error == null))
            {
                this.log.Debug("Error: " + e.Error.Message);
                mainForm.enableGo();
            }
            else
            {
                this.log.Debug("Done!");
                //mainForm.docListShowBeginCntrls();
                showDocList();
                //this.tbProgress.Text = "Done!";
            }

        }


        public void prepateDataToDisplay()
        {

        }


        private void showDocList()
        {
           


        }

        public void ListDocForm_FormClosing(object sender, EventArgs e)
        {
            this.mainForm.constrollSet1Enable();
            mainForm.docListShowDisposeCntrls();
        }
    }
}
