using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace Pawn.Forms.Inquiry.CashTransferInquiry
{
    public partial class CashTransferInquirySearch : Form
    {
        public NavBox NavControlBox;

        private AutoCompleteStringCollection autoComp = new AutoCompleteStringCollection();
        private Dictionary<string, string> topsShopInfoDict = null;
        private Dictionary<string, string> clxShopInfoDict = null;

        private DataSet dsCashDrawersSource = null;
        private DataSet dsCashDrawersDestination = null;

        private DataSet dsBanksSource = null;
        private DataSet dsBanksDestination = null;

        public CashTransferInquirySearch()
        {
            InitializeComponent();

            NavControlBox = new NavBox();
            NavControlBox.Owner = this;

            dateCalendarStart.PositionPopupCalendarOverTextbox = true;
            dateCalendarEnd.PositionPopupCalendarOverTextbox = true;
            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");

            sortBy_cb.DataSource = StringDBMap_Enum<global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.sortField_enum>.displayValues();
            sortBy_cb.SelectedIndex = 0;

            sortDir_cb.SelectedIndex = 0;

            label_source_secondary.Visible = false;
            label_dest_secondary.Visible = false;

            label_source_primary.Visible = true;
            label_dest_primary.Visible = true;
        }
             
        private void dateOption_rb_CheckedChanged(object sender, EventArgs e)
        {
            dateCalendarStart.Enabled = dateOption_rb.Checked;
            dateCalendarEnd.Enabled   = dateOption_rb.Checked;

            fromTicket_tb.Enabled = !dateOption_rb.Checked;
            toTicket_tb.Enabled   = !dateOption_rb.Checked;

            clear_fields();
        }

        private void TicketOption_rb_CheckedChanged(object sender, EventArgs e)
        {
            fromTicket_tb.Enabled = TicketOption_rb.Checked;
            toTicket_tb.Enabled   = TicketOption_rb.Checked;

            if (TicketOption_rb.Checked)
            {
                clear_fields();
            }
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            clear_fields();
        }

        private void clear_fields()
        {
            transfer_type_cb.SelectedIndex = 0;

            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            fromTicket_tb.Text = "";
            toTicket_tb.Text = "";

            transfer_source_primary_cb.SelectedIndex = -1;
            transfer_destination_secondary_cb.SelectedIndex = -1;
            transfer_destination_secondary_cb.SelectedIndex = -1;
            transfer_destination_primary_cb.SelectedIndex = -1;

            lowLoanAmt_tb.Text = "";
            highLoanAmt_tb.Text = "";
            userID_tb.Text = "";

            transfer_destination_secondary_txtBox.Visible = false;

            transfer_source_secondary_txtBox.Visible = false;
        }

        private void Find_btn_Click(object sender, EventArgs e)
        {
            if (dateOption_rb.Checked)
            {
                if (dateCalendarStart.SelectedDate == null ||
                    dateCalendarStart.SelectedDate.Length == 0 ||
                    dateCalendarStart.SelectedDate == "mm/dd/yyyy")
                {
                    dateCalendarStart.SelectedDate = dateCalendarEnd.SelectedDate;
                }
                    
                if (dateCalendarEnd.SelectedDate == null ||
                    dateCalendarEnd.SelectedDate.Length == 0 ||
                    dateCalendarEnd.SelectedDate == "mm/dd/yyyy")
                {
                    dateCalendarEnd.SelectedDate = dateCalendarStart.SelectedDate;
                }

                if (dateCalendarStart.SelectedDate == "mm/dd/yyyy" && dateCalendarEnd.SelectedDate == "mm/dd/yyyy")
                {
                    dateCalendarStart.SelectedDate = DateTime.Now.ToShortDateString();
                    dateCalendarEnd.SelectedDate = DateTime.Now.ToShortDateString();
                }

                DateTime dtStart = DateTime.MaxValue;
                DateTime dtEnd = DateTime.MinValue;
                DateTime.TryParse(dateCalendarStart.SelectedDate, out dtStart);
                DateTime.TryParse(dateCalendarEnd.SelectedDate, out dtEnd);
                if (dtStart > dtEnd)
                {
                    MessageBox.Show("'To:' date is greater than the 'From:' date. Please adjust the date.");
                    return;
                }
            }
            Cursor.Current = Cursors.WaitCursor;

            var cashTransferData = new global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry()
            {
                byDate = dateOption_rb.Checked,
                startDate = dateCalendarStart.SelectedDate,
                endDate = dateCalendarEnd.SelectedDate,
                transferType = (global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.transferType_enum)transfer_type_cb.SelectedIndex,
                userID = userID_tb.Text,
                sortBy = (global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.sortField_enum)sortBy_cb.SelectedIndex,
                sortDir = (global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.sortDir_enum)sortDir_cb.SelectedIndex
            };

            if (status_cb.SelectedItem != null)
            {
                cashTransferData.status = ((KeyValuePair<String, String>)status_cb.SelectedItem).Value;
            }

            if (fromTicket_tb.Text.Length > 0)
            {
                int.TryParse(fromTicket_tb.Text, out cashTransferData.lowTransferNumber);
            }

            if (toTicket_tb.Text.Length > 0)
            {
                int.TryParse(toTicket_tb.Text, out cashTransferData.highTransferNumber);
            }

            if (lowLoanAmt_tb.Text.Length > 0)
            {
                double.TryParse(lowLoanAmt_tb.Text, out cashTransferData.lowAmount);
            }

            if (highLoanAmt_tb.Text.Length > 0)
            {
                double.TryParse(highLoanAmt_tb.Text, out cashTransferData.highAmount);
            }

            if (transfer_source_primary_cb.Visible && transfer_source_primary_cb.SelectedItem != null)
            {
                cashTransferData.sourcePrimary = transfer_source_primary_cb.SelectedItem.ToString().Trim().ToUpper();
            }
            else
            {
                cashTransferData.sourcePrimary = String.Empty;
            }

            if (transfer_source_primary_cb.Visible && transfer_source_primary_cb.SelectedItem != null)
            {
                cashTransferData.destinationPrimary = transfer_destination_primary_cb.SelectedItem.ToString().Trim().ToUpper();
            }
            else
            {
                cashTransferData.destinationPrimary = String.Empty;
            }

            if (transfer_source_secondary_cb.Visible)
            {
                cashTransferData.sourceSecondary = transfer_source_secondary_cb.SelectedValue.ToString().Trim();
            }
            else if (transfer_source_secondary_txtBox.Visible)
            {
                String lookupValue = transfer_source_secondary_txtBox.Text.Trim();
                Boolean foundValue = false;
                foreach (KeyValuePair<string, string> pair in clxShopInfoDict)
                {
                    if (pair.Key.ToUpper() == lookupValue.ToUpper())
                    {
                        lookupValue = pair.Value;
                        foundValue = true;
                        break;
                    }
                }
                
                if (!foundValue)
                {
                    foreach (KeyValuePair<string, string> pair in topsShopInfoDict)
                    {
                        if (pair.Key.ToUpper() == lookupValue.ToUpper())
                        {
                            lookupValue = pair.Value;
                            foundValue = true;
                            break;
                        }
                    }
                }

                if (!foundValue)
                {
                    System.Diagnostics.Debug.Assert(false, "Unexpected condition.");
                    cashTransferData.sourceSecondary = lookupValue;
                }

                cashTransferData.sourceSecondary = lookupValue;
            }
            else
            {
                cashTransferData.sourceSecondary = String.Empty;
            }

            if (transfer_destination_secondary_cb.Visible)
            {
                cashTransferData.destinationSecondary = transfer_destination_secondary_cb.SelectedValue.ToString().Trim();
            }
            else if (transfer_destination_secondary_txtBox.Visible)
            {
                String lookupValue = transfer_destination_secondary_txtBox.Text.Trim();
                Boolean foundValue = false;
                foreach (KeyValuePair<string, string> pair in clxShopInfoDict)
                {
                    if (pair.Key.ToUpper() == lookupValue.ToUpper())
                    {
                        lookupValue = pair.Value;
                        foundValue = true;
                        break;
                    }
                }

                if (!foundValue)
                {
                    foreach (KeyValuePair<string, string> pair in topsShopInfoDict)
                    {
                        if (pair.Key.ToUpper() == lookupValue.ToUpper())
                        {
                            lookupValue = pair.Value;
                            foundValue = true;
                            break;
                        }
                    }
                }

                if (!foundValue)
                {
                    System.Diagnostics.Debug.Assert(false, "Unexpected condition.");
                    cashTransferData.destinationSecondary = lookupValue;
                }

                cashTransferData.destinationSecondary = lookupValue;
            }
            else
            {
                cashTransferData.destinationSecondary = String.Empty;
            }

            DataSet dataSetReturned = null;
            try
            {
                dataSetReturned = cashTransferData.getData();

                if (dataSetReturned.IsNullOrEmpty())
                    throw new BusinessLogicException(ReportConstants.NODATA);
            }
            catch (BusinessLogicException blex)
            {
                MessageBox.Show(blex.Message);
                return;
            }

            Cursor.Current = Cursors.Default;

            this.Visible = false;

            String tableName = "";
            if (dataSetReturned.Tables.Contains("BANK_TRANSFERS"))
            {
                tableName = "BANK_TRANSFERS";
            }
            else if (dataSetReturned.Tables.Contains("STORE_TRANSFERS"))
            {
                tableName = "STORE_TRANSFERS";
            }
            else if (dataSetReturned.Tables.Contains("SHOP_TO_SHOP_TRANSFERS"))
            {
                tableName = "SHOP_TO_SHOP_TRANSFERS";
            }
            else
            {
                tableName = "BANK_TRANSFERS";
            }
            var resultsDisplay = new CashTransferSearchResults(dataSetReturned, cashTransferData, tableName);
            
            resultsDisplay.ShowDialog();

            if (resultsDisplay.DialogResult == DialogResult.Cancel)
            {
                this.Close();
            }
            else
            {
                this.Visible = true;
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void transfer_type_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSourceAndDestinationPrimaryComboBoxes();
        }

        private void PopulateSourceAndDestinationPrimaryComboBoxes()
        {
            switch (StringDBMap_Enum<global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.transferType_enum>.fromString(this.transfer_type_cb.SelectedItem.ToString()))
            {
                case (global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.transferType_enum.BANK):
                    // Transfer Source/Destination
                    transfer_source_primary_cb.Visible = true;
                    transfer_source_secondary_cb.Visible = true;
                    transfer_destination_primary_cb.Visible = true;
                    transfer_destination_secondary_cb.Visible = true;
                    label_source_primary.Visible = true;
                    label_dest_primary.Visible = true;

                    transfer_source_primary_cb.Items.Clear();
                    transfer_source_primary_cb.Items.Add(" ");
                    transfer_source_primary_cb.Items.Add("Safe");
                    transfer_source_primary_cb.Items.Add("Bank");

                    transfer_destination_primary_cb.Items.Clear();
                    transfer_destination_primary_cb.Items.Add(" ");
                    transfer_destination_primary_cb.Items.Add("Safe");
                    transfer_destination_primary_cb.Items.Add("Bank");

                    transfer_source_primary_cb.SelectedIndex = 0;                    
                    transfer_destination_primary_cb.SelectedIndex = 0;

                    // Status
                    status_cb.Visible = true;
                    status_cb.Items.Clear();
                    status_cb.Items.Add(new KeyValuePair<String, String>(" ", ""));
                    status_cb.Items.Add(new KeyValuePair<String, String>("Accepted", "ACCEPTED"));
                    status_cb.Items.Add(new KeyValuePair<String, String>("Void", "VOID"));
                    status_cb.ValueMember = "Value";
                    status_cb.DisplayMember = "Key";
                    status_cb.SelectedIndex = 0;
                    break;

                case (global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.transferType_enum.INTERNAL):
                    // Transfer Source/Destination
                    transfer_source_primary_cb.Visible = true;
                    transfer_source_secondary_cb.Visible = true;
                    transfer_destination_primary_cb.Visible = true;
                    transfer_destination_secondary_cb.Visible = true;
                    label_source_primary.Visible = true;
                    label_dest_primary.Visible = true;

                    transfer_source_primary_cb.Items.Clear();
                    transfer_source_primary_cb.Items.Add(" ");
                    transfer_source_primary_cb.Items.Add("Drawer");
                    transfer_source_primary_cb.Items.Add("Safe");

                    transfer_destination_primary_cb.Items.Clear();
                    transfer_destination_primary_cb.Items.Add(" ");
                    transfer_destination_primary_cb.Items.Add("Drawer");
                    transfer_destination_primary_cb.Items.Add("Safe");

                    transfer_source_primary_cb.SelectedIndex = 0;
                    transfer_destination_primary_cb.SelectedIndex = 0;

                    // Status
                    status_cb.Visible = true;
                    status_cb.Items.Clear();
                    status_cb.Items.Add(new KeyValuePair<String, String>(" ", ""));
                    status_cb.Items.Add(new KeyValuePair<String, String>("Accepted", "ACCEPTED"));
                    status_cb.Items.Add(new KeyValuePair<String, String>("Void", "VOID"));
                    status_cb.ValueMember = "Value";
                    status_cb.DisplayMember = "Key";
                    status_cb.SelectedIndex = 0;
                    break;

                case (global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.transferType_enum.SAFE):
                    // Transfer Source/Destination
                    transfer_source_primary_cb.Visible = false;
                    transfer_source_secondary_cb.Visible = false;
                    transfer_destination_primary_cb.Visible = false;
                    transfer_destination_secondary_cb.Visible = false;
                    label_source_primary.Visible = false;
                    label_dest_primary.Visible = false;
                    label_source_secondary.Visible = false;
                    label_dest_secondary.Visible = false;
                    transfer_source_secondary_txtBox.Visible = false;
                    transfer_destination_secondary_txtBox.Visible = false;

                    transfer_source_primary_cb.Items.Clear();
                    transfer_source_secondary_cb.DataSource = null;
                    transfer_source_secondary_cb.Items.Clear();

                    transfer_destination_primary_cb.Items.Clear();
                    transfer_destination_secondary_cb.DataSource = null;
                    transfer_destination_secondary_cb.Items.Clear();

                    // Status
                    status_cb.Visible = true;
                    status_cb.Items.Clear();
                    status_cb.Items.Add(new KeyValuePair<String, String>(" ", ""));
                    status_cb.Items.Add(new KeyValuePair<String, String>("Accepted", "ACCEPTED"));
                    status_cb.ValueMember = "Value";
                    status_cb.DisplayMember = "Key";
                    status_cb.SelectedIndex = 0;
                    break;

                case (global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.transferType_enum.SHOP_TO_SHOP):
                    // Transfer Source/Destination
                    transfer_source_primary_cb.Visible = true;
                    transfer_source_secondary_cb.Visible = true;
                    transfer_destination_primary_cb.Visible = true;
                    transfer_destination_secondary_cb.Visible = true;
                    label_source_primary.Visible = true;
                    label_dest_primary.Visible = true;

                    transfer_source_primary_cb.Items.Clear();
                    transfer_source_primary_cb.Items.Add(" ");
                    transfer_source_primary_cb.Items.Add("Safe");
                    transfer_source_primary_cb.Items.Add("Shop");

                    transfer_destination_primary_cb.Items.Clear();
                    transfer_destination_primary_cb.Items.Add(" ");
                    transfer_destination_primary_cb.Items.Add("Safe");
                    transfer_destination_primary_cb.Items.Add("Shop");

                    transfer_source_primary_cb.SelectedIndex = 0;
                    transfer_destination_primary_cb.SelectedIndex = 0;
                    
                    // Status
                    status_cb.Visible = true;
                    status_cb.Items.Clear();
                    status_cb.Items.Add(new KeyValuePair<String, String>(" ", ""));
                    status_cb.Items.Add(new KeyValuePair<String, String>("Pending", "PENDING"));
                    status_cb.Items.Add(new KeyValuePair<String, String>("Accepted", "ACCEPTED"));
                    status_cb.Items.Add(new KeyValuePair<String, String>("Void", "VOID"));
                    status_cb.ValueMember = "Value";
                    status_cb.DisplayMember = "Key";
                    status_cb.SelectedIndex = 0;
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false, "Unhandled condition. Forget to add handler?");
                    break;
            }
        }

        private void CashTransferInquirySearch_Load(object sender, EventArgs e)
        {
            // Load Cash Drawer Data from database
            try
            {
                dsCashDrawersSource = global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.getCashDrawersForStore(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber);

                if (dsCashDrawersSource.IsNullOrEmpty())
                {
                    throw new BusinessLogicException(ReportConstants.NODATA);
                }
                dsCashDrawersDestination = dsCashDrawersSource.Copy();

            }
            catch (BusinessLogicException blex)
            {
                MessageBox.Show(blex.Message);
                return;
            }

            // Load list of Banks from database
            try
            {
                dsBanksSource = global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.getAllBanksStore(CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber);

                if (dsBanksSource.IsNullOrEmpty())
                {
                    throw new BusinessLogicException(ReportConstants.NODATA);
                }
                dsBanksDestination = dsBanksSource.Copy();

            }
            catch (BusinessLogicException blex)
            {
                MessageBox.Show(blex.Message);
                return;
            }

            // Load all Store Numbers from database
            PrepareShopTextForAutoComplete();

            transfer_type_cb.DataSource = StringDBMap_Enum<global::Pawn.Forms.Inquiry.CashTransferInquiry.CashTransferInquiry.transferType_enum>.displayValues();
            transfer_type_cb.SelectedIndex = 0;
        }

        // This method gets the list of shops from SP and keeps the text ready to auto complete
        private void PrepareShopTextForAutoComplete()
        {
            string errorCode = null;
            string errorText = null;
            ShopProcedures.ExecuteGetStoreInfoWithShortName(GlobalDataAccessor.Instance.OracleDA,
                                                            out topsShopInfoDict, out clxShopInfoDict, out errorCode, out errorText);
            autoComp = new AutoCompleteStringCollection();

            List<string> keylist = new List<string>(topsShopInfoDict.Keys);
            // Loop through list and add all TOPS stores
            foreach (string key in keylist)
            {
                autoComp.Add(key);
                //Console.WriteLine(key + "," + shopInfoDict[key]);
            }

            // Loop through list and add all Cashlinx stores
            keylist = new List<string>(clxShopInfoDict.Keys);
            foreach (string key in keylist)
            {
                autoComp.Add(key);
            }


            transfer_destination_secondary_txtBox.AutoCompleteCustomSource = autoComp;
            transfer_source_secondary_txtBox.AutoCompleteCustomSource = autoComp;
        }

        private void transfer_source_primary_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.transfer_source_primary_cb.SelectedItem == null)
            {
                return;
            }

            switch (this.transfer_source_primary_cb.SelectedItem.ToString().Trim())
            {
                case ("Drawer"):
                    transfer_source_secondary_cb.Visible = true;
                    transfer_source_secondary_cb.DataSource = dsCashDrawersSource.Tables[0];
                    transfer_source_secondary_cb.DisplayMember = "drawer_name";
                    transfer_source_secondary_cb.ValueMember = "drawer_id";

                    transfer_source_secondary_txtBox.Visible = false;

                    label_source_secondary.Visible = true;
                    break;
                case ("Safe"):
                case (""):
                    transfer_source_secondary_cb.Visible = false;
                    transfer_source_secondary_cb.DataSource = null;

                    transfer_source_secondary_txtBox.Visible = false;
                    label_source_secondary.Visible = false;
                    break;
                case ("Bank"):
                    transfer_source_secondary_cb.Visible = true;
                    transfer_source_secondary_cb.DataSource = dsBanksSource.Tables[0];
                    transfer_source_secondary_cb.DisplayMember = "BANKNAME";
                    transfer_source_secondary_cb.ValueMember = "routingnumber";

                    transfer_source_secondary_txtBox.Visible = false;
                    label_source_secondary.Visible = true;
                    break;
                case ("Shop"):
                    // Show text box for secondary criteria
                    transfer_source_secondary_txtBox.Visible = true;
                    transfer_source_secondary_txtBox.BringToFront();
                    label_source_secondary.Visible = true;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "Unhandled case. Forget to add handler?");
                    break;
            }
        }

        private void transfer_destination_primary_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.transfer_destination_primary_cb.SelectedItem == null)
            {
                return;
            }

            switch (this.transfer_destination_primary_cb.SelectedItem.ToString().Trim())
            {
                case ("Drawer"):
                    transfer_destination_secondary_cb.Visible = true;
                    transfer_destination_secondary_cb.DataSource = dsCashDrawersDestination.Tables[0];
                    transfer_destination_secondary_cb.DisplayMember = "drawer_name";
                    transfer_destination_secondary_cb.ValueMember = "drawer_id";

                    transfer_destination_secondary_txtBox.Visible = false;
                    label_dest_secondary.Visible = true;
                    break;
                case ("Safe"):
                case (""):
                    transfer_destination_secondary_cb.Visible = false;
                    transfer_destination_secondary_cb.DataSource = null;

                    transfer_destination_secondary_txtBox.Visible = false;
                    label_dest_secondary.Visible = false;
                    break;
                case ("Bank"):
                    transfer_destination_secondary_cb.Visible = true;
                    transfer_destination_secondary_cb.DataSource = dsBanksDestination.Tables[0];
                    transfer_destination_secondary_cb.DisplayMember = "BANKNAME";
                    transfer_destination_secondary_cb.ValueMember = "routingnumber";

                    transfer_destination_secondary_txtBox.Visible = false;
                    label_dest_secondary.Visible = true;
                    break;
                case ("Shop"):
                    // Show text box for secondary criteria
                    transfer_destination_secondary_txtBox.Visible = true;
                    transfer_destination_secondary_txtBox.BringToFront();
                    label_dest_secondary.Visible = true;
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false, "Unhandled case. Forget to add handler?");
                    break;
            }
        }
    }
}
