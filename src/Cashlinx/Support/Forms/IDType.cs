/********************************************************************
* CashlinxDesktop.UserControls
* IDType
* This user control will show a list of accepted ID Types from the database
* Sreelatha Rengarajan 3/13/2009 Initial version
* SR 6/1/2010 Added logic to change the backcolor
*******************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Common.Controllers.Application;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;
//using Pawn.Logic;

namespace Support.Forms
{
    public partial class IDType : UserControl
    {
        private bool _required;
        private bool _isValid;
        List<string> custIdTypes=new List<string>();

        [Category("Type")]
        [Description("Sets whether the Control is required to be entered in the form")]
        [DefaultValue(false)]
        public bool Required
        {
            get
            {
                return _required;
            }
            set
            {
                _required = value;
            }
        }

        [Category("Validation")]
        [Description("Sets if the control is valid")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool isValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if (!DesignMode && value != Color.Transparent)
                {
                    base.BackColor = value;
                    this.IdTypeList.BackColor = value;
                }
            }
        }

        public IDType()
        {
            InitializeComponent();
            this.IdTypeList.DrawMode = DrawMode.OwnerDrawFixed;

        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    //DataTable idTypeTable = GlobalDataAccessor.Instance.DesktopSession.IdTypeTable;
                    DataTable idTypeTable = Support.Logic.CashlinxPawnSupportSession.Instance.IdTypeTable;

                    if (idTypeTable.Rows.Count > 0)
                    {
                        ArrayList idTypes = new ArrayList(idTypeTable.Rows.Count+1);
                        for (int i=0;i<idTypes.Capacity;i++)
                        {
                            idTypes.Add("");
                        }
                        int index = 0;
                        foreach (DataRow dr in idTypeTable.Rows)
                        {
                            string code = dr["code"].ToString();
                            if (code == CustomerIdTypes.DRIVERLIC.ToString())
                                index = (int)CustomerIdTypes.DRIVERLIC;
                            if (code == CustomerIdTypes.SI.ToString())
                                index = (int)CustomerIdTypes.SI;
                            if (code == CustomerIdTypes.RI.ToString())
                                index = (int)CustomerIdTypes.RI;
                            if (code == CustomerIdTypes.MC.ToString())
                                index = (int)CustomerIdTypes.MC;
                            if (code == CustomerIdTypes.MI.ToString())
                                index = (int)CustomerIdTypes.MI;
                            if (code == CustomerIdTypes.PASSPORT.ToString())
                                index = (int)CustomerIdTypes.PASSPORT;
                            if (code == CustomerIdTypes.GI.ToString())
                                index = (int)CustomerIdTypes.GI;
                            if (code == CustomerIdTypes.II.ToString())
                                index = (int)CustomerIdTypes.II;
                            if (code == CustomerIdTypes.CW.ToString())
                                index = (int)CustomerIdTypes.CW;
                            //if (code == CustomerIdTypes.FFL.ToString())
                            //    index = (int)CustomerIdTypes.FFL;
                            if (code == CustomerIdTypes.OT.ToString())
                                index = (int)CustomerIdTypes.OT;

                            idTypes.RemoveAt(index);
                            idTypes.Insert(index,new ComboBoxData(dr["code"].ToString(), dr["codedesc"].ToString()));
                        }
                        idTypes.RemoveAt(0);
                        idTypes.Insert(0, new ComboBoxData("", "Select One"));
                        this.IdTypeList.DataSource = idTypes;
                        this.IdTypeList.DisplayMember = "Description";
                        this.IdTypeList.ValueMember = "Code";
 
                    }
                }


            }
            catch (SystemException Ex)
            {
                BasicExceptionHandler.Instance.AddException("ID Type data could not be fetched ", Ex);
            }


            base.OnLoad(e);
        }

        private void idTypeList_DrawItem(object sender, DrawItemEventArgs e)
        {
            //if (e.Index == -1)
            //    return;
            //e.DrawBackground();
            //Brush textBrush = SystemBrushes.ControlText;
            //Font drawFont = e.Font;
            //ComboBoxData eIdType = (ComboBoxData)IdTypeList.Items[e.Index];
            //string strToShow = eIdType.Description.ToString();
            //e.Graphics.DrawString(strToShow, drawFont, textBrush, e.Bounds);
            //e.DrawFocusRectangle();

            if (e.Index == -1)
                return;
            else
            {
                e.DrawBackground();
                Brush textBrush = SystemBrushes.ControlText;
                Font drawFont = e.Font;
                ComboBoxData eIdType = (ComboBoxData)IdTypeList.Items[e.Index];
                string strToShow = eIdType.Description.ToString();
                if ((e.State & DrawItemState.Selected) > 0)
                {
                    textBrush = SystemBrushes.HighlightText;
                }

                e.Graphics.DrawString(strToShow, drawFont, textBrush, e.Bounds);
                e.DrawFocusRectangle();
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (_required)
            {
                
                if (this.IdTypeList.SelectedItem != null && this.IdTypeList.SelectedIndex > 0)
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
                Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 1, this.Bounds.Width + 2, this.Bounds.Height + 1);
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
            {
                
                if (this.IdTypeList.SelectedItem != null && this.IdTypeList.SelectedIndex > 0)
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }
            }

            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y - 2, this.Bounds.Width + 2, this.Bounds.Height + 2);
            Commons.RemoveBorder(this, rect);
            base.OnLeave(e);


        }

        private void IdTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_required)
            {
                if (this.IdTypeList.SelectedItem != null && this.IdTypeList.SelectedIndex > 0)
                {
                    _isValid = true;
                }
                else
                {
                    _isValid = false;
                }
            }
        }

        private void IDType_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == false)
            {
                this.IdTypeList.BackColor = Color.LightGray;
                
            }
            else
            {
                this.IdTypeList.BackColor = Color.White;
                
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            if (!DesignMode && this.BackColor != Color.Transparent)
                this.IdTypeList.BackColor = this.BackColor;
        }


    }




}
