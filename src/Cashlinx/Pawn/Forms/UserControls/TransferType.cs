/********************************************************************
* CashlinxDesktop.UserControls
* TransferType
* This user control can be used in a form to show list of valid values for CATCO Transfer Types 
* from the database and allow selection
* Sreelatha Rengarajan 4/5/2009 Initial version
* SR 6/1/2010 Added logic for changing back color
* DAndrews 1/27/2011 Code and comment clean up to match control's real name and purpose.
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
    public partial class TransferType : UserControl
    {
        #region Private Members
        private bool _required;
        private bool _isValid;

        //TODO: Not hard code these values.
        private const string _APPRAISAL = "Appraisal";
        private const string _EXCESS = "Excess";
        private const string _REFURB = "Refurb";
        private const string _SCRAP = "Scrap";
        private const string _WHOLESALE = "Wholesale";
        #endregion

        #region Public Members
        public ComboBox TransferTypeList
        {
            get { return this.transferList; }
        }

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
        #endregion

        #region Constructor
        public TransferType()
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
            this.transferList.SelectedIndex = -1;
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
                    TransferToTypes.Add(new ComboBoxData("", "Select"));
                    TransferToTypes.Add(new ComboBoxData(_EXCESS, _EXCESS));
                    TransferToTypes.Add(new ComboBoxData(_REFURB, _REFURB));
                    TransferToTypes.Add(new ComboBoxData(_SCRAP, _SCRAP));
                    TransferToTypes.Add(new ComboBoxData(_APPRAISAL, _APPRAISAL));
                    TransferToTypes.Add(new ComboBoxData(_WHOLESALE, _WHOLESALE));

                    //TransferToTypes.Insert(4, new ComboBoxData(_APPRAISAL, _APPRAISAL));
                    //TransferToTypes.Insert(5, new ComboBoxData(_REPAIR, _REPAIR));
                    this.transferList.DataSource = TransferToTypes;
                    this.transferList.DisplayMember = "Description";
                    this.transferList.ValueMember = "Code";
                    this.transferList.SelectedIndex = 0;
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
                if (this.transferList.SelectedItem != null && this.transferList.Text != "Select")
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
            base.OnLeave(e);

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
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.transferList.BackColor = this.BackColor;
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
                ComboBoxData eIdType = (ComboBoxData)transferList.Items[e.Index];
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

        private void transferList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
