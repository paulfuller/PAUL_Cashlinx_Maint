using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Libraries.Utility;

namespace Pawn.Forms.UserControls
{
    public partial class OtherTender : UserControl
    {
        public delegate void AddHandler(decimal currencyTotal,int qnty,List<decimal> otherTenderAmounts);
        public event AddHandler AmtContinue;
        private int qnty=0;
        public List<decimal> OtherTenderAmounts
        {
            get;
            set;
        }
        public OtherTender()
        {
            InitializeComponent();
            if (OtherTenderAmounts == null)
            OtherTenderAmounts = new List<decimal>();
        }

        private void customButtonContinue_Click(object sender, EventArgs e)
        {
            string totalAmt = textBoxTotal.Text;
            if (totalAmt.StartsWith("$"))
                totalAmt = totalAmt.Substring(1);
            decimal currencytotal=Utilities.GetDecimalValue(totalAmt,0);
            if (currencytotal == 0)
            {
                qnty = 0;
                OtherTenderAmounts = new List<decimal>();
            }
            AmtContinue(currencytotal,qnty,OtherTenderAmounts);
        }

        private void dataGridViewOtherTenders_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewOtherTenders.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1;
        }

        private void dataGridViewOtherTenders_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                OtherTenderAmounts = new List<decimal>();
                decimal totalAmt = 0.0m;
                qnty = 0;
                foreach (DataGridViewRow dgvr in dataGridViewOtherTenders.Rows)
                {
                    if (dgvr.Cells[1] != null && dgvr.Cells[1].EditedFormattedValue != null && !string.IsNullOrEmpty(dgvr.Cells[1].EditedFormattedValue.ToString()))
                    {
                         decimal rowAmount=Utilities.GetDecimalValue(dgvr.Cells[1].EditedFormattedValue.ToString(), 0);
                        totalAmt += rowAmount;
                        qnty++;
                        if (OtherTenderAmounts != null)
                        {
                            OtherTenderAmounts.Add(rowAmount);
                        }
                    }
                }
                
                textBoxTotal.Text = string.Format("{0:C}", totalAmt);
            }
        }

        private void customButtonCancel_Click(object sender, EventArgs e)
        {
            OtherTenderAmounts = new List<decimal>();
            AmtContinue(0,0,OtherTenderAmounts);
        }

        private void OtherTender_Load(object sender, EventArgs e)
        {
            int i=1;
            
            if (OtherTenderAmounts != null && OtherTenderAmounts.Count > 0)
            {
                decimal totAmount = 0.0m;
                qnty = 0;
                foreach (decimal d in OtherTenderAmounts)
                {
                    DataGridViewRow dgvr = new DataGridViewRow();
                    DataGridViewTextBoxCell numbernewcell = new DataGridViewTextBoxCell();
                    numbernewcell.Value = i.ToString();
                    dgvr.Cells.Insert(0,numbernewcell);
                    numbernewcell = new DataGridViewTextBoxCell();
                    numbernewcell.Value = d;
                    totAmount += d;
                    dgvr.Cells.Insert(1,numbernewcell);
                    dataGridViewOtherTenders.Rows.Add(dgvr);
                    i++;
                    qnty++;

                }
                textBoxTotal.Text = string.Format("{0:C}", totAmount);
            }
        }
    }
}
