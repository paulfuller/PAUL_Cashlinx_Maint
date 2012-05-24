using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Pawn.Logic;

namespace Pawn.Forms.Pawn.Customer.Holds
{
    public partial class MultiJewelryCaseNumber : Form
    {
        #region Private Fields
        private List<GridColumns> _gridColumnsList;
        private BindingSource _bindingSource1;
        #endregion

        #region Constructors
        public MultiJewelryCaseNumber()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private void LoadGrid()
        {
            _gridColumnsList = new List<GridColumns>();
            foreach (var holdData in GlobalDataAccessor.Instance.DesktopSession.HoldsData)
            {
                var transItems = holdData.Items;
                foreach (var item in transItems)
                {
                    if (item.IsJewelry)
                    {
                        var gridColumn = new GridColumns();
                        gridColumn.ICN = item.Icn;
                        gridColumn.Description = item.TicketDescription;
                        _gridColumnsList.Add(gridColumn);
                    }
                }
            }
            _bindingSource1 = new BindingSource();
            _bindingSource1.DataSource = _gridColumnsList;
            dataGridViewJewelry.AutoGenerateColumns = false;
            dataGridViewJewelry.DataSource = _bindingSource1;
            dataGridViewJewelry.Columns[0].DataPropertyName = "ICN";
            dataGridViewJewelry.Columns[0].Width = 120;

            dataGridViewJewelry.Columns[1].DataPropertyName = "Description";
            dataGridViewJewelry.Columns[1].Width = 240;

            dataGridViewJewelry.Columns[2].DataPropertyName = "Jewelry";
            dataGridViewJewelry.Columns[2].Width = 120;
            dataGridViewJewelry.Columns[2].HeaderText = "Jewelry Case #";
            
            dataGridViewJewelry.AllowUserToAddRows = false;
        }
        #endregion

        #region Private Events Methods
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MultiJewelryCaseNumber_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            //loop through all rows in data grid, get the ICN, use the icn to loop through holddata.items and update the jewelryCase number
            try
            {
                foreach (DataGridViewRow dgvr in dataGridViewJewelry.Rows)
                {
                    string gridIcn = dgvr.Cells[0].Value.ToString();
                    string jewelryCase = Convert.ToString(dgvr.Cells[2].Value);
                    foreach (var holdData in GlobalDataAccessor.Instance.DesktopSession.HoldsData)
                    {
                        var transItems = holdData.Items;
                        foreach (var item in transItems)
                        {
                            if (item.Icn.Equals(gridIcn))
                            {
                                item.JeweleryCaseNumber = jewelryCase;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        #region Helper Class
        public class GridColumns
        {
            public string ICN { get; set; }
            public string Description { get; set; }
        }
        #endregion  
    }
}
