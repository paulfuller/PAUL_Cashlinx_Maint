using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Logic.DesktopProcedures;

namespace Pawn.Forms.Pawn.Services.PFI
{
    public partial class PFI_RefurbList : Form
    {
        public List<PFI_TransitionData> TransitionDatas {get; set;}
        public string AsOf {get; set;}
        private DataTable _GridTable = null;

        public PFI_RefurbList()
        {
            InitializeComponent();
        }

        public static DataTable setupDataTable(List<PFI_TransitionData> TransitionDatas, ref int iTagCount, ref decimal dCosts)
        {
            iTagCount = 0;
            dCosts = 0.00M;

            var myTable = new DataTable();

            myTable.Columns.Add("colRefurb");
            myTable.Columns.Add("colAssignmentType");
            myTable.Columns.Add("colNumber");
            myTable.Columns.Add("colDescription");
            myTable.Columns.Add("colTags");
            myTable.Columns.Add("colCost");
            myTable.Columns.Add("colRetail");
            myTable.Columns.Add("colReason");

            foreach (PFI_TransitionData transitionData in TransitionDatas)
            {
                PFI_ProductData pawnLoan = transitionData.pfiLoan;

                foreach (Item pawnItem in pawnLoan.UpdatedObject.Items)
                {
                    if (pawnItem.RefurbNumber > 0)
                    {
                        DataRow myRow = myTable.NewRow();
                        myRow["colRefurb"] = pawnItem.RefurbNumber;
                        myRow["colAssignmentType"] = Commons.GetPfiAssignmentAbbreviation(pawnItem.PfiAssignmentType);
                        myRow["colNumber"] = pawnLoan.UpdatedObject.TicketNumber;
                        myRow["colDescription"] = pawnItem.TicketDescription;
                        myRow["colTags"] = pawnItem.PfiTags;
                        myRow["colCost"] = String.Format("{0:C}", pawnItem.ItemAmount);
                        myRow["colRetail"] = String.Format("{0:C}", pawnItem.RetailPrice);

                        ItemReasonCode reasonCode = ItemReasonFactory.Instance.FindByReason(pawnItem.ItemReason);

                        if (reasonCode != null)
                        {
                            myRow["colReason"] = reasonCode.Description;
                        }
                        myTable.Rows.Add(myRow);

                        iTagCount += Utilities.GetIntegerValue(pawnItem.PfiTags, 0);
                        dCosts += Utilities.GetDecimalValue(pawnItem.ItemAmount, 0.00M);
                    }
                }
            }

            return myTable;
        }
            
        private void Setup()
        {
            int iTagCount = 0;
            decimal dCosts = 0.00M;

            _GridTable = setupDataTable(TransitionDatas, ref iTagCount, ref dCosts);
                
            asOfLabel.Text = "as of " + AsOf;
            totalTagsLabel.Text = iTagCount.ToString();
            totalCostLabel.Text = String.Format("{0:C}", dCosts);

            gvPostings.DataSource = _GridTable;
        }

        private void PFI_RefurbList_Load(object sender, EventArgs e)
        {
            Setup();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Printing

        private void printButton_Click(object sender, EventArgs e)
        {
            PrintPFIUtilities printPFIUtilities = new PrintPFIUtilities(_GridTable, this, "pfirefurb", "PFI Refurb List");
            printPFIUtilities.Print(totalCostLabel.Text, totalTagsLabel.Text);
        }
        #endregion

    }
}
