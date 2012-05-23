/********************************************************************
* CashlinxDesktop.UserControls
* Eyecolor
* This user control can be used in a form to show list of valid titles
* (ex: Mr,Mrs) etc
* from the database and allow selection
* Sreelatha Rengarajan 4/3/2009 Initial version
*******************************************************************/

using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Common.Controllers.Application;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Shared;

namespace Support.Forms.UserControls
{
    public partial class Title : UserControl
    {
        public Title()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    DataTable custTitleTable = GlobalDataAccessor.Instance.DesktopSession.TitleTable;
                    ArrayList titleTypes = new ArrayList();
                    if (custTitleTable != null && custTitleTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in custTitleTable.Rows)
                        {
                            titleTypes.Add(new ComboBoxData(dr["code"].ToString(), dr["codedesc"].ToString()));
                        }
                    }
                    else
                    {
                        //The call to the database failed or did not yield rows
                        //populate the titles using static data

                        titleTypes.Add(new ComboBoxData("Mr.", "Mr."));
                        titleTypes.Add(new ComboBoxData("Ms.", "Ms."));
                        titleTypes.Add(new ComboBoxData("Miss.", "Miss."));
                        titleTypes.Add(new ComboBoxData("Mrs.", "Mrs."));
                        titleTypes.Add(new ComboBoxData("Dr.", "Dr."));
                    }
                    titleTypes.Insert(0, (new ComboBoxData("", "Select")));
                    this.custTitleList.DataSource = titleTypes;
                    this.custTitleList.DisplayMember = "Description";
                    this.custTitleList.ValueMember = "Code";
                }

            }
            catch (SystemException Ex)
            {
                BasicExceptionHandler.Instance.AddException("Could not retrieve Titles data ", Ex);
            }

            base.OnLoad(e);
        }


        protected override void OnEnter(EventArgs e)
        {
            try
            {
                Rectangle rect = new Rectangle(this.Bounds.X-1, this.Bounds.Y - 1, this.Bounds.Width + 2, this.Bounds.Height+2);
                Commons.CustomPaint(this, rect);
            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Error calling the paint method to draw border ", ex);
            }
            //base.OnEnter(e);
        }

 
        protected override void OnLeave(EventArgs e)
        {
            Rectangle rect = new Rectangle(this.Bounds.X - 1, this.Bounds.Y - 1, this.Bounds.Width + 2, this.Bounds.Height+2);
            Commons.RemoveBorder(this, rect);
            //base.OnLeave(e);
        }

   

    }

}
