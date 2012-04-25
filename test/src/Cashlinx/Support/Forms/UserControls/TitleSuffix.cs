/********************************************************************
* CashlinxDesktop.UserControls
* Eyecolor
* This user control can be used in a form to show list of valid titlesuffixes
* (ex: I, II, Jr ) etc
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
    public partial class TitleSuffix : UserControl
    {
        public TitleSuffix()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    DataTable custTitleSuffixTable = GlobalDataAccessor.Instance.DesktopSession.TitleSuffixTable;
                    ArrayList titleSuffixTypes = new ArrayList();

                    if (custTitleSuffixTable != null && custTitleSuffixTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in custTitleSuffixTable.Rows)
                        {
                            titleSuffixTypes.Add(new ComboBoxData(dr["code"].ToString(), dr["codedesc"].ToString()));
                        }

                    }
                    if (titleSuffixTypes.Count == 0)
                    {
                        //The call to the database failed or did not yield rows
                        //populate the titles using static data
                        titleSuffixTypes.Add(new ComboBoxData("I", "I"));
                        titleSuffixTypes.Add(new ComboBoxData("II", "II"));
                        titleSuffixTypes.Add(new ComboBoxData("III", "III"));
                        titleSuffixTypes.Add(new ComboBoxData("Jr.", "Jr."));
                        titleSuffixTypes.Add(new ComboBoxData("Sr.", "Sr."));
                        titleSuffixTypes.Add(new ComboBoxData("Other", "Other"));
                    }
                    titleSuffixTypes.Insert(0, (new ComboBoxData("", "Select")));
                    this.custTitleSuffixList.DataSource = titleSuffixTypes;
                    this.custTitleSuffixList.DisplayMember = "Description";
                    this.custTitleSuffixList.ValueMember = "Code";
                }
            }
            catch (SystemException Ex)
            {
                BasicExceptionHandler.Instance.AddException("Could not load Title Suffix data", Ex);
            }

            base.OnLoad(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            try
            {
                Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y-1, this.Bounds.Width+2, this.Bounds.Height + 3);
                Commons.CustomPaint(this, rect);
            }
            catch (SystemException ex)
            {
                BasicExceptionHandler.Instance.AddException("Error calling the paint method to draw border", ex);
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            Rectangle rect = new Rectangle(this.Bounds.X - 2, this.Bounds.Y-1, this.Bounds.Width+2, this.Bounds.Height + 3);
            Commons.RemoveBorder(this, rect);
            base.OnLeave(e);
        }


    }

}
