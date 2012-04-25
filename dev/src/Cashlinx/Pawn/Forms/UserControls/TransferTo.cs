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
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Pawn.Forms.UserControls
{
    public partial class TransferTo : UserControl
    {
        private bool _required;
        private bool _isValid;
        //TODO: Not hard code these values.
        private const string _CATCO = "Catco";
        private const string _SHOP_NO = "Shop No.";
        private const string _GUN_ROOM = "Gun Room";
        private const string _OUT_OF_SHOP = "Out of Shop";

        [Category("Type")]
        [Description("Sets whether the Control is required to be entered in the form")]
        [DefaultValue(false)]
        public bool Required
        {
            get { return _required; }
            set { _required = value; }
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

        public ComboBox TransferToList
        {
            get { return this.transferToList; }            
        }

        public TransferTo()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            this.transferToList.SelectedIndex = -1;
        }

        protected override void OnLoad(EventArgs e)
        {

            try
            {
                if (!this.DesignMode)
                {
                    //DataTable raceTable = CashlinxDesktop.Desktop.CashlinxDesktopSession.Instance.RaceTable;
                    ArrayList TransferToTypes = new ArrayList();
                    TransferToTypes.Insert(0, new ComboBoxData("", "Select"));
                    TransferToTypes.Insert(1, new ComboBoxData(_CATCO, _CATCO));
                    TransferToTypes.Insert(2, new ComboBoxData(_SHOP_NO, _SHOP_NO));
                    TransferToTypes.Insert(3, new ComboBoxData(_GUN_ROOM, _GUN_ROOM));
                    //TransferToTypes.Insert(4, new ComboBoxData(_OUT_OF_SHOP, _OUT_OF_SHOP));
                    this.transferToList.DataSource = TransferToTypes;
                    this.transferToList.DisplayMember = "Description";
                    this.transferToList.ValueMember = "Code";
                    this.transferToList.SelectedIndex = 0;
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
                if (this.transferToList.SelectedItem != null && this.transferToList.Text != "Select")
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
            //try
            //{
            //    Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            //    Common.CustomPaint(this, rect);
            //}
            //catch (SystemException ex)
            //{
            //    BasicExceptionHandler.Instance.AddException("Error calling the paint method to draw border", ex);
            //}
        }


        protected override void OnLeave(EventArgs e)
        {
            //if (_required)
            //    if (this.transferList.SelectedItem != null && this.transferList.Text != "Select")
            //    {
            //        _isValid = true;
            //    }
            //    else
            //    {
            //        _isValid = false;
            //    }

            //Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 3, this.Bounds.Height + 3);
            //Common.RemoveBorder(this, rect);
            
            base.OnLeave(e);


        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.transferToList.BackColor = this.BackColor;
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
                ComboBoxData eIdType = (ComboBoxData)transferToList.Items[e.Index];
                string strToShow = eIdType.Description.ToString();
                if ((e.State & DrawItemState.Selected) > 0)
                {
                    textBrush = SystemBrushes.HighlightText;
                }

                e.Graphics.DrawString(strToShow, drawFont, textBrush, e.Bounds);
                e.DrawFocusRectangle();
            }

        }

        private void TransferToList_Load(object sender, EventArgs e)
        {

        }

    }

}
