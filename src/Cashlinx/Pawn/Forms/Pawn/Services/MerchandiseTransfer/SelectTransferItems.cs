/**************************************************************************
 * PFI_Verify.cs 
 * 
 * History:
 *  no ticket SMurphy 5/6/2010 issues with PFI Merge, PFI Add and navigation
 * 
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Business;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    public partial class SelectTransferItems : Form
    {
        #region Private Members

        private List<IItem> _availableItems = null;
        private List<IItem> _selectedItems = new List<IItem>();
        private bool _isDuplicateMode = false;
        private bool _isRefurbTrans = false;
        #endregion

        #region Public Members

        public NavBox NavControlBox;
        public List<IItem> SelectedItems
        {
            get { return _selectedItems; }
        }

        #endregion

        #region Constructor

        public SelectTransferItems(List<IItem> availableItems, bool isDuplicateMode, bool isRefurbTrans)
        {
            InitializeComponent();
            _isDuplicateMode = isDuplicateMode;
            _availableItems = availableItems;
            _isRefurbTrans = isRefurbTrans;


            Setup();
        }

        #endregion

        #region Private Methods

        private void Setup()
        {
            //Slight control changes for the duplicate screen to give the user more consistent feedback.
            if(_isDuplicateMode)
            {
                cancelButton.Visible = false;
                this.lblError.Visible = true;
                this.titleLabel.Text = "Duplicate Item List";
                this.continueButton.Text = "Ok";
            }
            if (_isRefurbTrans)
            {
                this.Refurb.Visible = true;
            }
            else
            {
                this.Refurb.Visible = false;
            }


            int serialNo = _availableItems.Count;

            foreach (IItem i in _availableItems)
            {
                int gvIdx = gvMerchandise.Rows.Add();
                DataGridViewRow myRow = gvMerchandise.Rows[gvIdx];
                //string icn = i.Icn;
                myRow.Cells[colICN.Name].Value = i.Icn;
                myRow.Cells[colMerchandiseDescription.Name].Value = i.TicketDescription;
                if (_isRefurbTrans)
                {
                   
                    myRow.Cells[Refurb.Name].Value = i.RefurbNumber;
                }

                myRow.Cells[colNumber.Name].Value = serialNo;
                myRow.Cells[colStatus.Name].Value = i.ItemStatus;
                serialNo--;
            }
        }

        #endregion

        #region Events

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //TODO: this.DialogResult = DialogResult.Cancel;
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            //bool shouldContinue = false;
            //int caseNumber = -1;
            
            //JewelryCaseDetails d = new JewelryCaseDetails();
            //d.ShowDialog();

            //if (!d.ShouldContinue) return;

            foreach(DataGridViewRow r in gvMerchandise.SelectedRows)
            {
                string icn = r.Cells[colICN.Name].Value.ToString();
                IItem item = (from a in _availableItems
                             where a.Icn == icn
                             select a).First();

                //item.JewelryCaseNumber = d.JewelryCaseNumber;
                _selectedItems.Add(item);
            }

            //TODO: this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

    }
}
