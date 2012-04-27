using System;
using System.Data;
using System.Windows.Forms;
//Odd file lock

namespace Pawn.Forms.Inquiry
{
    public partial class SearchResults : Form
    {
        protected DataSet _theData;
        protected Inquiry _theCriteria;
        //private int _keyDataGridColumn;
        //private Type _detailForm;


        public SearchResults () {}

        public SearchResults(DataSet s, Inquiry criteria, string resultSetName, string resultType = "Loans")
        {
            _theData = s;
            _theCriteria = criteria;

            InitializeComponent();

            String criteriaData = criteria.ToString();
            if (criteriaData.Contains("\n") && !criteriaData.Contains(Environment.NewLine))
            {
                // need to convert the \n to Environment.NewLine (\r\n)
                criteriaData = criteriaData.Replace("\n", Environment.NewLine);
            }
            criteriaSummary_txt.Text = criteriaData;

            resultsGrid_dg.AutoGenerateColumns = false;

            resultsGrid_dg.DataSource = s.Tables[resultSetName];

            if (s.Tables[resultSetName].Rows.Count > 0)
            {
                NrLoans_tb.Text = string.Format(" of {0} {1}", s.Tables[resultSetName].Rows.Count, resultType);
                
                LoanCtr_tb.Text = string.Format("{0}", 1);
            }
        }

        private void Back_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void resultsGrid_dg_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LoanCtr_tb.Text = string.Format("{0,3}", e.RowIndex + 1);
        }
    }
}
