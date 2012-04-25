/**************************************************************************************************************
* CashlinxDesktop
* TagReprint
* This form is used to lookup a pawn tag for reprinting. The CSR may enter the Bar Code number (ICN) or short
* code to retrieve tag for reprintting
* Dee Bailey 8/31/2009 Initial version
**************************************************************************************************************/

using System;
using System.Data;
using System.ComponentModel;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Pawn.Services
{
    public partial class TagReprint : Form
    {
        Form ownerfrm;
        public TagReprintSelect frm2;
        private string StoreNumber;
        public string IcnNumber;
        public NavBox NavControlBox;
        ProcessingMessage procMsg;
        bool retValue = false;
        string errorCode;
        string errorText;
        string strItemWasSelected = "N";
        bool isshort;
        public DataTable tagInformation = new DataTable();
        public TagReprint()
        {
            InitializeComponent();
            dataGridViewTagData.Visible = false;
            textBoxIcnNumber.Visible = true;
            labelIcnNumber.Visible = true;
            NavControlBox = new NavBox();
            StoreNumber = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
            this.labelHeading.Text = "Bar Code Tag Reprint";
            this.Select(); 
        }
        private void buttonComplete_Click(object sender, EventArgs e)
        {
            if (dataGridViewTagData.Visible)
            {
                if (strItemWasSelected == "Y")
                {
                    LookUpICN();
                }
            }
            else
            {
                int idx;
                errorLabel.Text = "";
                IcnNumber = textBoxIcnNumber.Text;
                idx = (IcnNumber).IndexOf('.');
                if (idx > 0)
                {
                    isshort = true;
                }
                validateEntry(idx);
                if (errorLabel.Text.Length > 0)
                {
                    errorLabel.Visible = true;
                    return;
                }
                LookUpICN();
            }
        }
     
        public void LookUpICN()
        {
            try
            {
                errorLabel.Visible = false;
                procMsg = new ProcessingMessage("Retrieving Tag Data");
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                bw.RunWorkerAsync();
                procMsg.ShowDialog(this);
                if (retValue)
                {
                     if (tagInformation != null)
                     {
                         if (tagInformation.Rows.Count > 1)
                         {
                             textBoxIcnNumber.Visible = false;
                             labelIcnNumber.Visible = false;
                             dataGridViewTagData.Visible = true; 
                             loadGridWithTheData();
                             if (errorLabel.Text.Length > 0)
                                 errorLabel.Text += System.Environment.NewLine + Commons.GetMessageString("TagReprintDuplicateShortCodeMsg");
                             else
                                 errorLabel.Text = Commons.GetMessageString("TagReprintDuplicateShortCodeMsg");
                                 errorLabel.Visible = true;
                         }
                         else
                         {
                             dataGridViewTagData.Visible = false;
                             textBoxIcnNumber.Visible = true;
                             labelIcnNumber.Visible = true;
                             TagReprintSelect frm = new TagReprintSelect();
                             frm.frm1 = this;
                             this.Hide();
                             frm.ShowDialog();
                             frm.Select();
                         }

                     }
                     else
                     {
                            MessageBox.Show(Commons.GetMessageString("TagReprintIcnNumberMsg"), "Prompt", MessageBoxButtons.OK);
                            errorLabel.Text = Commons.GetMessageString("TagReprintIcnNumberMsg");
                            errorLabel.Visible = true;
                            return;
                     }
                }
                else
                {
                    MessageBox.Show(Commons.GetMessageString("TagReprintIcnNumberMsg"), "Prompt", MessageBoxButtons.OK);
                    errorLabel.Text = Commons.GetMessageString("TagReprintIcnNumberMsg");
                    errorLabel.Visible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Failed to get the tag data for " + IcnNumber.ToString(), ex);
                NavControlBox.Action = NavBox.NavAction.CANCEL;
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            if (IcnNumber.Length == 18)
            {
                //ExecuteGetTagLongCode
                retValue = GenerateDocumentsProcedures.ExecuteGetTagLongCode(IcnNumber, StoreNumber, out tagInformation,
                out errorCode, out errorText);
            }
            else
            {
                //ExecuteGetTagShortCode
                retValue = GenerateDocumentsProcedures.ExecuteGetTagShortCode(IcnNumber, StoreNumber, out tagInformation,
                out errorCode, out errorText);
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            procMsg.Update();
            procMsg.Close();
            procMsg.Dispose();
        }
     

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            NavControlBox.Action = NavBox.NavAction.CANCEL;
        }

        private void loadGridWithTheData()
        {
 
            DataTable tg = new DataTable();
            tg = tagInformation;
            tg.Columns.Remove("ICN_DOC");
            tg.Columns.Remove("ICN_DOC_TYPE");
            this.labelHeading.Text = "Duplicate Short Code ICN#";
            BindingSource bs = default(BindingSource);
            bs = new BindingSource();
            bs.DataSource = tg;

            dataGridViewTagData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewTagData.MultiSelect = false;
            dataGridViewTagData.DataSource = bs;
            dataGridViewTagData.ClearSelection();  
            dataGridViewTagData.Refresh();
        }

        private void dataGridViewTagData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IcnNumber = dataGridViewTagData.Rows[e.RowIndex].Cells["ICN"].Value.ToString();
            textBoxIcnNumber.Text = IcnNumber;
            strItemWasSelected = "Y";
   
        }
        private void validateEntry(int idx)
        {
           
            errorLabel.Text = "";
            if (isshort)
                editShortCode(idx);

            if (isshort && IcnNumber.Length < 10)
            {
                
                MessageBox.Show(Commons.GetMessageString("TagReprintIcnNumberInvalid"), "Prompt", MessageBoxButtons.OK);
                return;
                if (errorLabel.Text.Length > 0)
                    errorLabel.Text += System.Environment.NewLine + Commons.GetMessageString("TagReprintIcnNumberInvalid");
                else
                    errorLabel.Text = Commons.GetMessageString("TagReprintIcnNumberInvalid");
            }

            if (!isshort && textBoxIcnNumber.Text.Length != 18)
            {
                MessageBox.Show(Commons.GetMessageString("TagReprintIcnNumberInvalid"), "Prompt", MessageBoxButtons.OK);
                if (errorLabel.Text.Length > 0)
                    errorLabel.Text += System.Environment.NewLine + Commons.GetMessageString("TagReprintIcnNumberInvalid");
                else
                    errorLabel.Text = Commons.GetMessageString("TagReprintIcnNumberInvalid");
            }
    
        }
        private void editShortCode(int idx)
        {
            try
            {
            string strStore =  IcnNumber.Substring(0, (idx));
            string strItem = IcnNumber.Substring(idx + 1,(IcnNumber.Length - (strStore.Length + 1)));
            long iItem = Convert.ToInt64(strItem);
            long iStore =  Convert.ToInt64(strStore);
            IcnNumber  = iStore.ToString("000000") + "." + iItem.ToString("000");
            }
            catch (Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("Failed to get the tag data for " + IcnNumber.ToString(), ex);
                IcnNumber = " ";
            }

        }
     
    }
}
