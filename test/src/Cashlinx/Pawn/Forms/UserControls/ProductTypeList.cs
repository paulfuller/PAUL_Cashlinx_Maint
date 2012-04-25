/********************************************************************
* CashlinxDesktop.UserControls
* Race
* This user control can be used in a form to show list of valid values for race
* from the database and allow selection
* Sreelatha Rengarajan 4/5/2009 Initial version
* SR 6/1/2010 Added logic for changing back color
*******************************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Common.Controllers.Database.Couch;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls
{
    public partial class ProductTypeList : UserControl
    {
        private bool _required;
        private bool _isValid;
        //TODO: Not hard code these values.
        //private const string _CUSTOMER_BUY = "Buy";
        //private const string _VENDOR_BUY = "Vendor Buy";
        //private const string _PAWN_LAON = "Pawn Loan";
        //private const string _PURCHASE_RETURN = "Purchase Return";
        //private const string _RECIEPT = "Receipt";

        [Category("Type")]
        [Description("Sets whether the Control is required to be entered in the form")]
        [DefaultValue(false)]
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
        }

        public ComboBox ComboBox
        {
            get { return productList; }
        }

        [Category("Validation")]
        [Description("Sets if the control is valid")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool isValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        public ProductTypeList()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            this.productList.SelectedIndex = -1;
        }

        public event EventHandler SelectedIndexChanged;

        protected override void OnLoad(EventArgs e)
        {

            try
            {
                if (!this.DesignMode)
                {
                    //DataTable raceTable = CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.RaceTable;
                    ArrayList productTypes = new ArrayList();
                    productTypes.Add(new ComboBoxData("", "Select"));
                    productTypes.Add(new ComboBoxData(DocumentHelper.CUSTOMER_BUY, DocumentHelper.CUSTOMER_BUY));
                    //productTypes.Insert(2, new ComboBoxData(_VENDOR_BUY, _VENDOR_BUY));
                    productTypes.Add(new ComboBoxData(DocumentHelper.PAWN_LOAN, DocumentHelper.PAWN_LOAN));
                    productTypes.Add(new ComboBoxData(DocumentHelper.PURCHASE_RETURN, DocumentHelper.PURCHASE_RETURN));
                    productTypes.Add(new ComboBoxData(DocumentHelper.RECIEPT, DocumentHelper.RECIEPT));
                    productTypes.Add(new ComboBoxData(DocumentHelper.REPRINT_TAGS, DocumentHelper.REPRINT_TAGS));
                    this.productList.DataSource = productTypes;
                    this.productList.DisplayMember = "Description";
                    this.productList.ValueMember = "Code";
                    this.productList.SelectedIndex = 0;
                    //}
                }

                //TO DO: The call to the database failed or did not yield rows
                //then populate the titles using static data

            }
            catch (SystemException Ex)
            {
                BasicExceptionHandler.Instance.AddException("Could not load product data", Ex);
                
            }

            base.OnLoad(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {
                if (this.productList.SelectedItem != null && this.productList.Text != "Select")
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }
            }
            //base.OnLayout(e);
        }


        protected override void OnEnter(EventArgs e)
        {
            try
            {
                Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
                Commons.CustomPaint(this, rect);
            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Error calling the paint method to draw border", ex);
            }
        }


        protected override void OnLeave(EventArgs e)
        {
            if (_required)
                if (this.productList.SelectedItem != null && this.productList.Text != "Select")
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }

            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            Commons.RemoveBorder(this, rect);
            
            base.OnLeave(e);


        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.productList.BackColor = this.BackColor;
        }

        private void productList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            else
            {
                e.DrawBackground();
                Brush textBrush = SystemBrushes.ControlText;
                Font drawFont = e.Font;
                ComboBoxData eIdType = (ComboBoxData)productList.Items[e.Index];
                string strToShow = eIdType.Description;
                if ((e.State & DrawItemState.Selected) > 0)
                {
                    textBrush = SystemBrushes.HighlightText;
                }

                e.Graphics.DrawString(strToShow, drawFont, textBrush, e.Bounds);
                e.DrawFocusRectangle();
            }

        }

        private void productList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged == null)
            {
                return;
            }

            SelectedIndexChanged(sender, e);
        }

    }

}
