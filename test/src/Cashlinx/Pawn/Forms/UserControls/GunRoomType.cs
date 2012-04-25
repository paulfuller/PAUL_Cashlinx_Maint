/********************************************************************
* CashlinxDesktop.UserControls
* GunRoomType
* This user control can be used in a form to show list of valid values for GunRoomType
* from the database and allow selection
* Sreelatha Rengarajan 4/5/2009 Initial version
* SR 6/1/2010 Added logic for changing back color
* DAndrews 1/27/2011 Changed code and comments to be intended list of GunRoomType, not Carrier or product.
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
    public partial class GunRoomType : UserControl
    {
        #region Private Members
        private bool _required;
        private bool _isValid;
        //TODO: Not hard code these values.
        private const string _CAF = "CAF Facility";
        private const string _SP = "SP Facility";
        #endregion

        #region Public Members
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

        public System.Windows.Forms.ComboBox getGunFacilityList()
        {
            return this.gunRoomTypeList;
        }

        public String getSelectedFacility(){
           return this.gunRoomTypeList.Text;
        }

        #endregion

        #region Constructor
        public GunRoomType()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the list's SelectedIndex to -1 so no item is selected.
        /// </summary>
        public void Clear()
        {
            this.gunRoomTypeList.SelectedIndex = -1;
        }
        #endregion

        #region Protected Override Methods
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    ArrayList TransferToTypes = new ArrayList();
                    TransferToTypes.Insert(0, new ComboBoxData("", "Select"));
                    TransferToTypes.Insert(1, new ComboBoxData(_CAF, _CAF));
                    //TransferToTypes.Insert(2, new ComboBoxData(_SP, _SP));
                    this.gunRoomTypeList.DataSource = TransferToTypes;
                    this.gunRoomTypeList.DisplayMember = "Description";
                    this.gunRoomTypeList.ValueMember = "Code";
                    this.gunRoomTypeList.SelectedIndex = 1;
                    //}
                }
            }
            catch (SystemException Ex)
            {
                BasicExceptionHandler.Instance.AddException("Could not load list values", Ex);
            }

            base.OnLoad(e);
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {
                if (this.gunRoomTypeList.SelectedItem != null && this.gunRoomTypeList.Text != "Select")
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
                this.gunRoomTypeList.BackColor = this.BackColor;
        }
        #endregion

        #region Events
        private void list_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            else
            {
                e.DrawBackground();
                Brush textBrush = SystemBrushes.ControlText;
                Font drawFont = e.Font;
                ComboBoxData eIdType = (ComboBoxData)gunRoomTypeList.Items[e.Index];
                string strToShow = eIdType.Description.ToString();
                if ((e.State & DrawItemState.Selected) > 0)
                {
                    textBrush = SystemBrushes.HighlightText;
                }

                e.Graphics.DrawString(strToShow, drawFont, textBrush, e.Bounds);
                e.DrawFocusRectangle();
            }
        }
        #endregion

    }
}
