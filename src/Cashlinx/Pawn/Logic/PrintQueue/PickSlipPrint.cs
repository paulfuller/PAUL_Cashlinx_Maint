/*******************************************************************
* History:
*  PWNU00000509 5/27/2010 SMurphy - formatting changes
* *****************************************************************/

using System.Linq;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.UserControls;
using Common.Controllers.Database.Procedures;

namespace Pawn.Logic.PrintQueue
{
    public partial class PickSlipPrint : Form
    {
        string userName;
        int itemCount = 0;
        string transactionDate = "";
        int pageNo;
        private Bitmap bitMap;

        public string PickSlipType { get; set; }
        
        public PickSlipPrint()
        {
            InitializeComponent();
        }

        private void Print()
        {
            pageNo++;
 
            bitMap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            this.DrawToBitmap(bitMap, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            PrintingUtilities.PrintBitmapDocument(bitMap, GlobalDataAccessor.Instance.DesktopSession);
        }

        private void PickSlipPrint_Load(object sender, EventArgs e)
        {
            //Get all the data to print from the desktop session
            CustomerVO currentCust = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
            List<HoldData> custHoldsData = GlobalDataAccessor.Instance.DesktopSession.HoldsData;
            userName = GlobalDataAccessor.Instance.DesktopSession.UserName.ToLowerInvariant();
            labelTransactionType.Text = PickSlipType + @" Picking Slip";
            labelEmployeeNo.Text = userName;
            //set up the data
            if (currentCust != null && custHoldsData.Count > 0)
            {
                foreach (HoldData holdTransaction in custHoldsData)
                {
                    pageNo = 1;
                    itemCount = 0;
                    transactionDate = DateTime.Now.ToString("g");//holdTransaction.TransactionDate.ToShortDateString();
                    labelPageNo.Text = "Page " + pageNo.ToString();
                    labelDate.Text = transactionDate;
                    if (!string.IsNullOrEmpty(currentCust.MiddleInitial))
                        labelCustName.Text = currentCust.LastName + "," + currentCust.FirstName + "," + currentCust.MiddleInitial;
                    else
                        labelCustName.Text = currentCust.LastName + "," + currentCust.FirstName;

                    labelCurrentLoanNo.Text = holdTransaction.TicketNumber.ToString();
                    labelLoanAmt.Text = String.Format("{0:C}", holdTransaction.Amount);

                    if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsPartialPaymentAllowed(GlobalDataAccessor.Instance.CurrentSiteId))
                    {
                        labelCurrentPrincipal.Text = String.Format("{0:C}", holdTransaction.CurrentPrincipalAmount);
                    }
                    else
                    {
                        labelCurrentPrincipalHeading.Visible = false;
                        labelCurrentPrincipal.Visible = false;
                    }

                    labelorigLoanNo.Text = holdTransaction.OrigTicketNumber.ToString();
                    labelPreviousLoanNo.Text = holdTransaction.PrevTicketNumber.ToString();

                    //item description of all items for this ticket
                    int rowNum = 0;
                    HoldData transaction = holdTransaction;
                    var items =
                    holdTransaction.Items.Where(
                        itemdata => itemdata.mDocNumber == transaction.OrigTicketNumber);
                    int pageNumberCount = 0;
                    if (items.Count() > 0)
                    {
                        tableLayoutPanelItemData.Controls.Clear();
                        tableLayoutPanelItemData.Height = 0;
                        
                        foreach (Item transactionItem in items)
                        {
                            PickSlipPrintControl slipControl = new PickSlipPrintControl();
                            itemCount = itemCount + 1;
                            slipControl.ItemNumber = itemCount;
                            slipControl.ItemDescription = transactionItem.TicketDescription.ToString();
                            if (transactionItem.GunNumber > 0)
                                slipControl.GunNumber = transactionItem.GunNumber.ToString();
                            if (transactionItem.Location_Aisle != string.Empty)
                                slipControl.ItemLocationAisle = transactionItem.Location_Aisle.ToString();
                            if (transactionItem.Location_Shelf != string.Empty)
                                slipControl.ItemLocationShelf = transactionItem.Location_Shelf.ToString();
                            if (transactionItem.Location != string.Empty)
                                slipControl.ItemLocationOther = transactionItem.Location.ToString();
                      
                            //tableLayoutPanelItemData.Controls.Add(slipControl, 0, rowNum);
                            int gapDifference = pageFooterMarkerLabel.Top - tableLayoutPanelItemData.Bottom;
                            if (gapDifference < slipControl.Height)
                            {
                                //tableLayoutPanelItemData.Controls.Remove(slipControl);
                                rowNum = 0;
                                pageNumberCount++;
                                labelPageNo.Text = "Page " + pageNumberCount;
                                Print();
                                tableLayoutPanelItemData.Controls.Clear();
                                tableLayoutPanelItemData.Controls.Add(slipControl, 0, rowNum);
                                rowNum++;
                            }
                            else
                            {
                                tableLayoutPanelItemData.Controls.Add(slipControl, 0, rowNum);
                                rowNum++;
                            }
                        }
                    }
                    if (rowNum > 0)
                    {
                        pageNumberCount++;
                        labelPageNo.Text = "Page " + pageNumberCount;
                        Print();
                        tableLayoutPanelItemData.Controls.Clear();
                    }
                }
                Application.DoEvents();
            }
            this.Close();
        }

        private void CreateNewColumn(string text, int colNo, int rowNo)
        {
            Label newLabel = new Label();
            newLabel.AutoSize = false;
            newLabel.Font = this.labelCurrentLoanNo.Font;
            newLabel.Text = text;
            tableLayoutPanelItemData.Controls.Add(newLabel, colNo, rowNo);
        }
    }
}
