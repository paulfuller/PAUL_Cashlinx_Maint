using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Inquiry;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    public partial class InventoryInquirySearch : Form
    {
        public NavBox NavControlBox;

        public string[] catCodes, catNames;
        CategorySelect categoryForm;

        public InventoryInquirySearch()
        {
            //string errText;
            //int errNo;

            InitializeComponent();

            NavControlBox = new NavBox();
            NavControlBox.Owner = this;

            dateCalendarStart.PositionPopupCalendarOverTextbox = true;
            dateCalendarStart.BringToFront();
            dateCalendarEnd.PositionPopupCalendarOverTextbox = true;
            dateCalendarEnd.BringToFront();
            //dateCalendarStart.SelectedDate = "";
            //dateCalendarEnd.SelectedDate = "";
            dateCalendarStart.SelectedDate = "mm/dd/yyyy";
            dateCalendarEnd.SelectedDate = "mm/dd/yyyy";
            //dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
            //dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");


            sortBy_cb.DataSource = StringDBMap_Enum<sortField_enum>.displayValues();
            status_cb.DataSource = StringDBMap_Enum<statusField_enum>.displayValues();
            //sortDir_cb.DataSource = StringDBMap_Enum<Inquiry.sortDir_enum>.displayValues();
            sortBy_cb.SelectedIndex = 0;
            sortDir_cb.SelectedIndex = 0;

            DataSet data = InventoryInquiries.getCategories();


            categoryForm = new CategorySelect(data);
        }

        private void Clear_btn_Click(object sender, EventArgs e)
        {
            clear_fields();
        }

        private void clear_fields()
        {

            IEnumerable<TextBox> textBoxes = from Control c in this.Controls
                                             where c.GetType() == ICN_shop_tb.GetType()
                                             select (TextBox)c;

            foreach (TextBox t in textBoxes)
            {
                t.Text = "";
            }

            dateCalendarStart.SelectedDate = "";
            dateCalendarEnd.SelectedDate = "";
            sortBy_cb.SelectedIndex = 0;
            sortDir_cb.SelectedIndex = 0;
            status_cb.SelectedIndex = 0;
            this.catCodes = null;
            categoryForm.clear();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.categoryForm.Dispose();
            this.Close();
            //this.NavControlBox.Action = NavBox.NavAction.BACK;
            //CashlinxDesktopSession.Instance.HistorySession.Desktop();
        }


        private int getIntVal(TextBox tb, int defaultValue)
        {
            int retval = defaultValue;

            if (tb.Text.Length > 0)
            {
                int parsedValue;
                if (int.TryParse(tb.Text, out parsedValue))
                    retval = parsedValue;
                else
                    retval = -1;
            }

            return retval;
        }


        private void Find_btn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            var criteria = new InventoryInquiries();
            criteria.Mdse = new MDSE();

            criteria.Mdse.Icn.ShopNumber = getIntVal(ICN_shop_tb, -1);
            if (ICN_year_tb.Text.Length > 0)
            {
                criteria.Mdse.Icn.LastDigitOfYear = getIntVal(ICN_year_tb, -1);
                criteria.icnYear_IsNull = false;
            }

            criteria.Mdse.Icn.DocumentNumber = getIntVal(ICN_doc_tb, -1);

            int doc_type = getIntVal(ICN_doc_type_tb, -1);
            if (doc_type >= 0 && doc_type < 10)
            {
                criteria.Mdse.Icn.DocumentType = (DocumentType)doc_type;
                criteria.icnDocType_IsNull = false;
            }

            // need to add exception management -- parse is not fail safe
            criteria.Mdse.Icn.ItemNumber = getIntVal(ICN_item_tb, -1);
            criteria.Mdse.Icn.SubItemNumber = getIntVal(ICN_sub_item_tb, -1);
            criteria.RfbNr = getIntVal(rfb_tb, -1);
            criteria.GunNumber = getIntVal(gunNbr_tb, -1);
            if (status_cb.SelectedItem != null)
                criteria.status = StringDBMap_Enum<statusField_enum>.fromString(status_cb.SelectedItem.ToString());

            if (dateCalendarStart.SelectedDate != "mm/dd/yyyy" && dateCalendarStart.SelectedDate.Length > 0)
                criteria.lowStatusDate = DateTime.Parse(dateCalendarStart.SelectedDate);
            else
                criteria.lowStatusDate = DateTime.MinValue;

            if (dateCalendarEnd.SelectedDate != "mm/dd/yyyy" && dateCalendarStart.SelectedDate.Length > 0)
                criteria.highStatusDate = DateTime.Parse(dateCalendarEnd.SelectedDate);
            else
                criteria.highStatusDate = DateTime.MaxValue;

            criteria.age = getIntVal(invAge_tb, -1);
            bool converted = decimal.TryParse(lowRetailAmt_tb.Text, out criteria.lowRetail);
            if (!converted)
                criteria.lowRetail = -1;
            converted = decimal.TryParse(highRetailAmt_tb.Text, out criteria.highRetail);
            if (!converted)
                criteria.highRetail = -1;
            converted = decimal.TryParse(lowCost_tb.Text, out criteria.lowCost);
            if (!converted)
                criteria.lowCost = -1;
            converted = decimal.TryParse(highCost_tb.Text, out criteria.highCost);
            if (!converted)
                criteria.highCost = -1;

            converted = decimal.TryParse(fieldCost_tb.Text, out criteria.fieldCost);
            if (!converted)
                criteria.fieldCost = -1;

            criteria.descr = descr_tb.Text;
            criteria.manufacturer = manuf_tb.Text;
            criteria.model = model_tb.Text;
            criteria.serialNr = serialNr_tb.Text;
            criteria.aisle = aisle_tb.Text;
            criteria.shelf = shelf_tb.Text;
            criteria.location = loctn_tb.Text;

            criteria.sortBy = StringDBMap_Enum<sortField_enum>.fromString(sortBy_cb.SelectedItem.ToString());
            criteria.sortDir = StringDBMap_Enum<Inquiry.sortDir_enum>.fromString(sortDir_cb.SelectedItem.ToString());


            if (this.catCodes != null && this.catCodes.GetLength(0) > 0)
            {
                criteria.cat_code = new List<string>(this.catCodes);
                criteria.cat_names = new List<string>(this.catNames);
            }
            else
            {
                criteria.cat_code = new List<string>();
                criteria.cat_code.Add("-1"); //empty array
            }

            DataSet s = null;
            try
            {
                s = criteria.getData();

                if (s.IsNullOrEmpty())
                {
                    throw new BusinessLogicException(ReportConstants.NODATA);
                }
            }
            catch (BusinessLogicException blex)
            {
                MessageBox.Show(blex.Message);
                return;
            }

            //this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
            Cursor.Current = Cursors.Default;

            this.Visible = false;
            var resultsDisplay = new InventorySearchResults(s, criteria, "MDSE");

            resultsDisplay.ShowDialog();

            if (resultsDisplay.DialogResult == DialogResult.Cancel)
                this.Close();

            else
                this.Visible = true;

        }

        private void ICN_doc_tb_Leave(object sender, EventArgs e)
        {
            Doc_nr_tb.Text = this.ICN_doc_tb.Text;
        }

        private void Doc_nr_tb_Leave(object sender, EventArgs e)
        {
            this.ICN_doc_tb.Text = Doc_nr_tb.Text;
        }

        private bool categoryShown = false;

        private void CategoriesBtn_Click(object sender, EventArgs e)
        {
            categoryShown = true;
            categoryForm.ShowDialog();

            if (categoryForm.DialogResult == DialogResult.OK)
            {
                this.catCodes = categoryForm.getCatCodes();
                this.catNames = categoryForm.getCatNames();
            }
        }



        private void InventoryInquirySearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (categoryShown)
            {
                categoryShown = false;
                e.Cancel = true;
            }

        }
    }
}
