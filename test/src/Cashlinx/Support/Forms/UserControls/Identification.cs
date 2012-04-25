/********************************************************************
* CashlinxDesktop.UserControls
* Identification
* This user control can be used in a form to allow the user to enter rows
* of identifications for the customer
* Sreelatha Rengarajan 7/8/2009 Initial version
*
* 1-mar-2010  rjm   Ticket PWNU00000201: Modified addRowToEnter() to 
*                  place Driver's License as the first item in the 
*                  dropdown list idtypecombocell. The correct 
*                  soltuion is to change the table we are pulling 
*                  the data from (picklist) to allow the ordering of 
*                  the items pulled. Because we do not know how this 
*                  might affect Phase 1, we are going with this less 
*                  elegant solution.
* 
* 2-mar-2010 rjm   Ticket PWNU00000257:                  
 * 1/13/2012 EDW   Additonal Error Handling and minor code refactoring.
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;
using Support.Logic;
using Support.Forms.Pawn.Customer;

namespace Support.Forms.UserControls
{
    public partial class Identification : UserControl
    {
        
        private DataTable idtypetable;
        private DataTable idissuerstatetable;
        private DataTable idissuercountrytable;
        private DataTable idissueragency;  // remove

        private bool dataValid = false;
        private int numRows = 0;

        #region FORMSTARTUP
        private List<IdentificationVO> customerIdentities;

        /*__________________________________________________________________________________________*/
        public Identification()
        {
            InitializeComponent();
            idTypeTable = Support.Logic.CashlinxPawnSupportSession.Instance.IdTypeTable;
            DataTable idIssuerStateTable = Support.Logic.CashlinxPawnSupportSession.Instance.StateTable;
        }
        /*__________________________________________________________________________________________*/
        public void Clear()
        {
            this.dataGridViewCustomerID.Rows.Clear();
        }
        #endregion
        #region MEMBER PROPERTIES
        /*__________________________________________________________________________________________*/
        private DataTable idTypeTable 
       {    
            get
            {
                if (idtypetable == null)
                    idtypetable = Support.Logic.CashlinxPawnSupportSession.Instance.IdTypeTable;

                return idtypetable;
            } 
               set
               {
                   idtypetable = value;
                   //if (idtypetable == null)
                   //    idtypetable = Support.Logic.CashlinxPawnSupportSession.Instance.IdTypeTable;
               } 
        }
        /*__________________________________________________________________________________________*/
        private DataTable idIssuerAgency 
            { get
                {
                    if (idissueragency == null)
                        idissueragency = Support.Logic.CashlinxPawnSupportSession.Instance.StateTable;

                    return idissueragency;
                } 
               set
               {
                   idissueragency = value;
                   //if (idissueragency == null)
                   //    idissueragency = Support.Logic.CashlinxPawnSupportSession.Instance.StateTable;
               }
            }
        /*__________________________________________________________________________________________*/
        private DataTable idIssuerStateTable
            { 
                get
                {
                    if (idissuerstatetable == null)
                        idissuerstatetable = Support.Logic.CashlinxPawnSupportSession.Instance.StateTable;

                    return idissuerstatetable;
                } 
               set
               {
                   idissuerstatetable = value;
                   //if (idissuerstatetable == null)
                   //    idissuerstatetable = Support.Logic.CashlinxPawnSupportSession.Instance.StateTable;
               }
            }
        /*__________________________________________________________________________________________*/
        private DataTable idIssuerCountryTable
        {
            get
            {
                if (idissuercountrytable == null)
                    idissuercountrytable = Support.Logic.CashlinxPawnSupportSession.Instance.CountryTable;

                return idissuercountrytable;
            }
            set
            {
                idissuercountrytable = value;  
                //if (idissuercountrytable == null)
                //    idissuercountrytable = Support.Logic.CashlinxPawnSupportSession.Instance.CountryTable;
            }
        }
        #endregion
        #region Boolean
        //WCM 4/9/12 This is not being called ???
        /*__________________________________________________________________________________________*/
        public bool isValid()
        {
            dataValid = true;
            numRows = 0;
            for (int j = 0; j < dataGridViewCustomerID.Rows.Count - 1; j++)
            {
                DataGridViewRow dgvr = dataGridViewCustomerID.Rows[j];

                if (dgvr.Cells[0].EditedFormattedValue.ToString().Equals("Select") && dgvr.Cells[1].EditedFormattedValue.ToString().Equals("Select"))
                    dgvr.ErrorText = "";
                else if (dgvr.Cells[0].EditedFormattedValue != null && dgvr.Cells[0].EditedFormattedValue.ToString() != "Select" && dgvr.Cells[0].EditedFormattedValue.ToString() != string.Empty)
                {
                    if (dgvr.Cells[1].EditedFormattedValue.ToString() == string.Empty || dgvr.Cells[1].EditedFormattedValue == null || dgvr.Cells[1].EditedFormattedValue.ToString().Equals("Select"))
                    {

                        dgvr.ErrorText = Commons.GetMessageString("IDIssuerRequired");
                        dataValid = false;
                    }
                    else if (dgvr.Cells[2].EditedFormattedValue.ToString() == string.Empty || dgvr.Cells[2].EditedFormattedValue == null)
                    {
                        dataValid = false;
                        dgvr.ErrorText = Commons.GetMessageString("IDNumberRequired");
                    }
                    else
                    {
                        dgvr.ErrorText = string.Empty;
                    }
                }
                else
                {

                    if (dgvr.Cells[1].EditedFormattedValue.ToString() == string.Empty || dgvr.Cells[1].EditedFormattedValue == null || dgvr.Cells[1].EditedFormattedValue.ToString().Equals("Select"))
                    {

                        dgvr.ErrorText = Commons.GetMessageString("IDTypeAndIssuerRequired");
                        dataValid = false;
                    }
                    else
                    {

                        dgvr.ErrorText = Commons.GetMessageString("IDTypeRequired");
                        dataValid = false;
                    }

                }
                checkExpiryDate(dgvr.Index);

                if (!dataValid)
                {
                    dataGridViewCustomerID.RowHeadersVisible = true;
                    return false;
                }
                numRows++;
            }
            dataGridViewCustomerID.RowHeadersVisible = false;
            return true;
        }
        #endregion
        #region GETDATA
        //WCM 4/5/12 Remove the assignment for the IDTypeTable & IdIssuerStateTable and use member properties
        /*__________________________________________________________________________________________*/
        private void addRowToEnter(DataGridViewCellEventArgs e)
        {
            if (dataGridViewCustomerID.Rows[e.RowIndex].IsNewRow)
            {
                //Add an empty row
                DataGridViewComboBoxCell idtypecombocell = new DataGridViewComboBoxCell();
                DataGridViewComboBoxCell issuercombocell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell numbernewcell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell expirationnewcell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell identidcell = new DataGridViewTextBoxCell();
                //DataTable idTypeTable = GlobalDataAccessor.Instance.DesktopSession.IdTypeTable;
                //DataTable idTypeTable = Support.Logic.CashlinxPawnSupportSession.Instance.IdTypeTable;

                //ArrayList idTypes = new ArrayList();
                if (idTypeTable.Rows.Count > 0)
                {
                    //changes for ticket PWNU00000201 : puts DL at the top of list
                    if (idTypeTable.Rows.Count > 0)
                    {


                        foreach (DataRow dr in idTypeTable.Rows)
                        {
                            idtypecombocell.Items.Add(dr["codedesc"]);
                        }
                        idtypecombocell.Items.Add("Select");
                        idtypecombocell.Value = "Select";
                    }
                }

                //DataTable idIssuerStateTable = GlobalDataAccessor.Instance.DesktopSession.StateTable;
                //DataTable idIssuerAgency = Support.Logic.CashlinxPawnSupportSession.Instance.StateTable;
                if (idIssuerStateTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in idIssuerStateTable.Rows)
                    {
                        issuercombocell.Items.Add(dr["state_code"].ToString());
                    }
                    issuercombocell.Items.Add("Select");
                }

                issuercombocell.Value = "Select";
                expirationnewcell.Value = "mm/dd/yyyy";
                numbernewcell.MaxInputLength = 20;
                expirationnewcell.MaxInputLength = 10;
                DataGridViewRow dgNewRow = new DataGridViewRow();
                dgNewRow.Cells.Insert(0, idtypecombocell);
                dgNewRow.Cells.Insert(1, issuercombocell);
                dgNewRow.Cells.Insert(2, numbernewcell);
                dgNewRow.Cells.Insert(3, expirationnewcell);
                dgNewRow.Cells.Insert(4, identidcell);
                dataGridViewCustomerID.Rows.Insert(e.RowIndex, dgNewRow);
                dataGridViewCustomerID.CurrentCell = dataGridViewCustomerID.Rows[e.RowIndex].Cells[0];
            }
        }
        /*__________________________________________________________________________________________*/
        public void populateCustomerIdentification(string strIdType, string strIdIssuer,
            string strIdNumber, string strIdExpDate, string strIdentId)
        {
            DataGridViewTextBoxCell idtypecell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell issuercell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell numbercell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell expirationcell = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell identidcell = new DataGridViewTextBoxCell();

            numbercell.MaxInputLength = 20;
            expirationcell.MaxInputLength = 10;

            idtypecell.Value = strIdType;
            issuercell.Value = strIdIssuer;
            numbercell.Value = strIdNumber;

            if (strIdExpDate != string.Empty)
                expirationcell.Value = strIdExpDate;
            if (strIdentId != string.Empty)
                identidcell.Value = strIdentId;

            DataGridViewRow dgRow = new DataGridViewRow();
            dgRow.Cells.Insert(0, idtypecell);
            dgRow.Cells.Insert(1, issuercell);
            dgRow.Cells.Insert(2, numbercell);
            dgRow.Cells.Insert(3, expirationcell);
            dgRow.Cells.Insert(4, identidcell);
            dataGridViewCustomerID.Rows.Add(dgRow);
        }
        /*__________________________________________________________________________________________*/
        public void populateCustomerIdentification(List<IdentificationVO> custIds)
        {
            dataGridViewCustomerID.Rows.Clear();
            string idTypeDesc;
            string idIssuer;
            string idNumber;
            string idExpiryDate;
            string identId;
            if (custIds.Count > 0)
            {
                foreach (var custid in custIds)
                {
                    idTypeDesc = custid.DatedIdentDesc;
                    if (Commons.IsStateIdDescription(idTypeDesc))
                        idIssuer = custid.IdIssuerCode;
                    else
                        idIssuer = custid.IdIssuer;

                    idNumber = custid.IdValue;
                    if (custid.IdExpiryData == DateTime.MaxValue)
                        idExpiryDate = string.Empty;
                    else
                        idExpiryDate = (custid.IdExpiryData).FormatDate();
                    identId = custid.IdentId;

                    var idtypecell = new DataGridViewTextBoxCell();
                    var issuercell = new DataGridViewTextBoxCell();
                    var numbercell = new DataGridViewTextBoxCell();
                    var expirationcell = new DataGridViewTextBoxCell();
                    var identidcell = new DataGridViewTextBoxCell();
                    numbercell.MaxInputLength = 20;
                    expirationcell.MaxInputLength = 10;
                    idtypecell.Value = idTypeDesc;
                    issuercell.Value = idIssuer;
                    numbercell.Value = idNumber;
                    expirationcell.Value = idExpiryDate;
                    identidcell.Value = identId;

                    var dgRow = new DataGridViewRow();
                    dgRow.Cells.Insert(0, idtypecell);
                    dgRow.Cells.Insert(1, issuercell);
                    dgRow.Cells.Insert(2, numbercell);
                    dgRow.Cells.Insert(3, expirationcell);
                    dgRow.Cells.Insert(4, identidcell);
                    dataGridViewCustomerID.Rows.Add(dgRow);

                }
                //SR 2/16/2010 Roles and resources check added
                //check the privileges of the logged in user to determine 
                //if the user can edit id type and agency
                //if (!(SecurityProfileProcedures.CanUserModifyResource("EDITGOVTIDTYPEAGENCY", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, GlobalDataAccessor.Instance.DesktopSession)))
                //{
                //    dataGridViewCustomerID.Columns[0].ReadOnly = true;
                //    dataGridViewCustomerID.Columns[1].ReadOnly = true;
                //}
                //check the privileges of the logged in user to determine
                //if the user can edit id number
                //if (!(SecurityProfileProcedures.CanUserModifyResource("EDITGOVTIDNUMBER", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, GlobalDataAccessor.Instance.DesktopSession)))
                //{
                //    dataGridViewCustomerID.Columns[2].ReadOnly = true;
                //}
            }
        }
        /// <summary>
        /// Gets all the identification data entered in the datagridview
        /// </summary>
        /// <returns></returns>
        //WCM 4/5/12 Remove the assignment for the IDTypeTable & IdIssuerStateTable and use member properties
        /*__________________________________________________________________________________________*/
        public List<IdentificationVO> getIdentificationData()
        {
            customerIdentities = new List<IdentificationVO>();
            //if (dataValid && CashlinxDesktopSession.Instance != null && CashlinxDesktopSession.Instance != null)
            if (dataValid && CashlinxPawnSupportSession.Instance != null && CashlinxPawnSupportSession.Instance != null)
            {
                //DataTable idTypeTable = GlobalDataAccessor.Instance.DesktopSession.IdTypeTable;
                //DataTable idIssuerStateTable = GlobalDataAccessor.Instance.DesktopSession.StateTable;

                var idTypeDesc = string.Empty;
                var idTypeCode = string.Empty;
                var idIssuer = string.Empty;
                var idNumber = string.Empty;
                var idExpiryDate = string.Empty;
                var identId = string.Empty;
                var idIssuerCode = string.Empty;

                for (int j = 0; j < dataGridViewCustomerID.Rows.Count - 1; j++)
                {
                    DataGridViewRow dgvr = dataGridViewCustomerID.Rows[j];
                    if (dgvr.Cells[0].Value != null && dgvr.Cells[0].Value.ToString() != "Select")
                    {
                        checkExpiryDate(j);
                        idTypeDesc = (string)dgvr.Cells[0].EditedFormattedValue;
                        idTypeCode = "";

                        //Get the code for the selected ID Type
                        foreach (DataRow dr in idTypeTable.Rows)
                        {
                            if (dr["codedesc"].ToString() == idTypeDesc)
                            {
                                idTypeCode = dr["code"].ToString();
                            }
                        }

                        if (idTypeCode == string.Empty)
                            idTypeCode = idTypeDesc;

                        //If the ID type is not state based, get the country code
                        //idissuercode is the 2 letter code for the state or country
                        //idissuer is the full state name or country name
                        if (Commons.IsStateIdDescription(idTypeDesc))
                        {
                            idIssuerCode = (string)dgvr.Cells[1].EditedFormattedValue;
                            //Get the full state name for the selected state ID issuer
                            foreach (DataRow dr in idIssuerStateTable.Rows)
                            {
                                if (dr["state_code"].ToString() == idIssuerCode)
                                {
                                    idIssuer = dr["state_name"].ToString();
                                }
                            }

                            if (idIssuer == string.Empty)
                            {
                                idIssuer = idIssuerCode;
                            }
                        }
                        else
                        {
                            idIssuerCode = Commons.GetCountryData(dgvr.Cells[1].EditedFormattedValue.ToString());
                            idIssuer = dgvr.Cells[1].EditedFormattedValue.ToString();
                        }

                        idNumber = (string)dgvr.Cells[2].EditedFormattedValue;
                        //Since checkExpiryDate is called before the edited value
                        //would have been converted and stored in the value field
                        idExpiryDate = (string)dgvr.Cells[3].Value;
                        identId = (string)dgvr.Cells[4].Value;
                        var newid = new IdentificationVO
                        {
                            IdType = idTypeCode,
                            IdValue = idNumber,
                            IdIssuer = idIssuer,
                            DatedIdentDesc = idTypeDesc,
                            IdIssuerCode = idIssuerCode
                        };
                        try
                        {
                            newid.IdExpiryData = DateTime.Parse(idExpiryDate);
                        }
                        catch (Exception)
                        {
                            newid.IdExpiryData = DateTime.MaxValue;
                        }

                        if (identId == null)
                            newid.IdentId = "";
                        else
                            newid.IdentId = identId;
                        customerIdentities.Add(newid);

                    }
                }
                return customerIdentities;
            }
            else
            {
                return null;
            }
        }
        /*__________________________________________________________________________________________*/
        private void checkExpiryDate(int rowIdx)
        {
            string expiryDate;
            DateTime dtExpiry;
            expiryDate = ((string)dataGridViewCustomerID.Rows[rowIdx].Cells[3].EditedFormattedValue).FormatStringAsDate();
            if (expiryDate.Equals("mm/dd/yyyy"))
            {
                expiryDate = "";
                dataGridViewCustomerID.Rows[rowIdx].Cells[3].Value = "";
            }
            if (expiryDate != string.Empty)
            {
                try
                {
                    dtExpiry = Convert.ToDateTime(expiryDate);
                    dataGridViewCustomerID.Rows[rowIdx].Cells[3].Value = dtExpiry.FormatDate();
                }
                catch (Exception)
                {
                    dataGridViewCustomerID.RowHeadersVisible = true;
                    dataValid = false;
                    dataGridViewCustomerID.Rows[rowIdx].ErrorText = Commons.GetMessageString("InvalidDate");
                }
            }
        }
        #endregion
        #region EVENTS & ACTIONS
        /*__________________________________________________________________________________________*/
        private void dataGridViewCustomerID_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
               /* if ((SecurityProfileProcedures.CanUserModifyResource(
                        "EDITGOVTIDTYPEAGENCY", 
                        GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, 
                        GlobalDataAccessor.Instance.DesktopSession)) ||
                (SecurityProfileProcedures.CanUserModifyResource(
                    "EDITGOVTIDNUMBER", 
                    GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, 
                    GlobalDataAccessor.Instance.DesktopSession)))
                {
                    addRowToEnter(e);
                }
                else */
                    addRowToEnter(e);
               // dataGridViewCustomerID_CellContentClick(sender, e);
            }
        }
        //WCM 4/5/12 Remove the assignment for the IDTypeTable, IdIssuerStateTable and IdIssuerCountryTable and use member properties
        /*__________________________________________________________________________________________*/
        private void changeCellDataToEdit(DataGridViewCellEventArgs e)
        {
            string idTypeDesc;
            string idIssuer;
            string idNumber;
            string idExpiryDate;
            string identId;

            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                if (dataGridViewCustomerID.Rows[e.RowIndex].Cells[0].GetType() == typeof(DataGridViewTextBoxCell))
                {
                    idTypeDesc = (string)dataGridViewCustomerID.Rows[e.RowIndex].Cells[0].Value;
                    idIssuer = (string)dataGridViewCustomerID.Rows[e.RowIndex].Cells[1].Value;
                    idNumber = (string)dataGridViewCustomerID.Rows[e.RowIndex].Cells[2].Value;
                    idExpiryDate = (string)dataGridViewCustomerID.Rows[e.RowIndex].Cells[3].Value;
                    if (idExpiryDate.Equals(string.Empty))
                        idExpiryDate = "mm/dd/yyyy";
                    identId = (string)dataGridViewCustomerID.Rows[e.RowIndex].Cells[4].Value;
                    DataGridViewComboBoxCell idtypecell = new DataGridViewComboBoxCell();
                    DataGridViewComboBoxCell issuercell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell numbercell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell expirationcell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell identidcell = new DataGridViewTextBoxCell();
                    //DataTable idTypeTable = GlobalDataAccessor.Instance.DesktopSession.IdTypeTable;
                    //ArrayList idTypes = new ArrayList();
                    if (idTypeTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in idTypeTable.Rows)
                        {
                            idtypecell.Items.Add(dr["codedesc"]);

                        }
                        idtypecell.Items.Add("Select");
                    }


                    idtypecell.Value = idTypeDesc;

                    if (Commons.IsStateIdDescription(idTypeDesc))
                    {
                        //var idIssuerStateTable = GlobalDataAccessor.Instance.DesktopSession.StateTable;
                        if (idIssuerStateTable.Rows.Count > 0)
                        {

                            foreach (DataRow dr in idIssuerStateTable.Rows)
                            {
                                issuercell.Items.Add(dr["state_code"].ToString());
                            }
                            issuercell.Items.Add("Select");

                        }
                    }
                    else
                    {
                        //DataTable idIssuerAgency = GlobalDataAccessor.Instance.DesktopSession.CountryTable;
                        if (idIssuerCountryTable.Rows.Count > 0)
                        {
                            foreach (DataRow dr in idIssuerCountryTable.Rows)
                            {
                                issuercell.Items.Add(dr["country_name"].ToString());
                            }
                            issuercell.Items.Add("Select");
                        }
                    }
                    issuercell.Value = idIssuer;

                    numbercell.Value = idNumber;
                    expirationcell.Value = idExpiryDate;
                    identidcell.Value = identId;
                    dataGridViewCustomerID.Rows.RemoveAt(e.RowIndex);
                    numbercell.MaxInputLength = 20;
                    expirationcell.MaxInputLength = 10;

                    DataGridViewRow dgNewRow = new DataGridViewRow();
                    dgNewRow.Cells.Insert(0, idtypecell);
                    dgNewRow.Cells.Insert(1, issuercell);
                    dgNewRow.Cells.Insert(2, numbercell);
                    dgNewRow.Cells.Insert(3, expirationcell);
                    dgNewRow.Cells.Insert(4, identidcell);
                    dataGridViewCustomerID.Rows.Insert(e.RowIndex, dgNewRow);
                    dataGridViewCustomerID.CurrentCell = dataGridViewCustomerID.Rows[e.RowIndex].Cells[0];
                }
            }

            if (e.ColumnIndex == 3)
            {
                dataValid = true;
                checkExpiryDate(e.RowIndex);
            }
            if (dataValid)
                dataGridViewCustomerID.RowHeadersVisible = false;
        }
        //WCM 4/5/12 Remove the assignment for the IDTypeTable & IdIssuerStateTable and use member properties
        /*__________________________________________________________________________________________*/
        private void dataGridViewCustomerID_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string idTypeDesc;
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0 && dataGridViewCustomerID.Rows[e.RowIndex].Cells[0].GetType() == typeof(DataGridViewComboBoxCell))
                {
                    idTypeDesc = (string)dataGridViewCustomerID.Rows[e.RowIndex].Cells[0].Value;
                    var issuercell = new DataGridViewComboBoxCell();
                    if (Commons.IsStateIdDescription(idTypeDesc))
                    {
                        //DataTable idIssuerStateTable = GlobalDataAccessor.Instance.DesktopSession.StateTable;
                        if (idIssuerStateTable.Rows.Count > 0)
                        {

                            foreach (DataRow dr in idIssuerStateTable.Rows)
                            {
                                issuercell.Items.Add(dr["state_code"].ToString());
                            }
                            issuercell.Items.Add("Select");

                        }
                    }
                    else
                    {
                        //DataTable idIssuerAgency = GlobalDataAccessor.Instance.DesktopSession.CountryTable;
                        if (idIssuerCountryTable.Rows.Count > 0)
                        {
                            foreach (DataRow dr in idIssuerCountryTable.Rows)
                            {
                                issuercell.Items.Add(dr["country_name"].ToString());
                            }
                            issuercell.Items.Add("Select");
                        }
                    }

                    dataGridViewCustomerID.Rows[e.RowIndex].Cells[1] = issuercell;

                }
                if (e.ColumnIndex == 3)
                {
                    dataValid = true;
                    checkExpiryDate(e.RowIndex);
                }


            }
            if (dataValid)
                dataGridViewCustomerID.RowHeadersVisible = false;
        }
        /*__________________________________________________________________________________________*/
        private void dataGridViewCustomerID_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCustomerID.Rows[e.RowIndex].IsNewRow && e.ColumnIndex == 0 &&
                dataGridViewCustomerID.Rows[e.RowIndex].Cells[0].Value == null)
            {
                dataGridViewCustomerID.Rows[e.RowIndex].Cells[0].Value = "Select";
                dataGridViewCustomerID.Rows[e.RowIndex].Cells[1].Value = "Select";


            }
            if (e.ColumnIndex == 3 && dataGridViewCustomerID.Rows[e.RowIndex].Cells[0].Value != null &&
                dataGridViewCustomerID.Rows[e.RowIndex].Cells[3].Value != null && dataGridViewCustomerID.Rows[e.RowIndex].Cells[3].Value.ToString() == "mm/dd/yyyy")
            {
                dataGridViewCustomerID.Rows[e.RowIndex].Cells[3].Value = "";
            }
        }
        /*__________________________________________________________________________________________*/
        private void dataGridViewCustomerID_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 3)
                {
                    checkExpiryDate(e.RowIndex);
                }
            }
        }
        /*__________________________________________________________________________________________*/
        private void dataGridViewCustomerID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dataGridViewCustomerID.Rows[e.RowIndex].IsNewRow)
                {
                    //if ((SecurityProfileProcedures.CanUserModifyResource("EDITGOVTIDTYPEAGENCY", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, GlobalDataAccessor.Instance.DesktopSession)) ||
                    //    (SecurityProfileProcedures.CanUserModifyResource("EDITGOVTIDNUMBER", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, GlobalDataAccessor.Instance.DesktopSession)))

                        changeCellDataToEdit(e);
                }
                else
                    changeCellDataToEdit(e);
            }
            if (dataValid)
                dataGridViewCustomerID.RowHeadersVisible = false;
        }
        #endregion
    }
}
