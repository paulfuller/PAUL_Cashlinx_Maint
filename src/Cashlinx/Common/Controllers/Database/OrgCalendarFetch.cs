using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace Common.Controllers.Database
{
    class OrgCalendarFetch
    {

        public static OleDbConnection getConnected()
        {
            /*string connectionString = "provider=MSDAORA;data source=CLXD;user id=ccsowner;password=ferrari";
            OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
            return myOleDbConnection;*/
            return (null);
        }


        public string getPartyRoleId(string storeNumber)
        {
            /*OleDbConnection oleDbConnection = getConnected();
            OleDbCommand myOleDbCommand = oleDbConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT storeid, storenumber, storename, partyroleid FROM store WHERE storenumber = '" + storeNumber + "'";
            oleDbConnection.Open();
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            myOleDbDataReader.Read();
            string partyRolId = myOleDbDataReader["partyroleid"].ToString();
            myOleDbDataReader.Close();
            oleDbConnection.Close();
            return partyRolId;*/
            return ("null");
        }


        public List<String> getOrgId(string partyRoleId, DateTime inputDate)
        {
            /*OleDbConnection oleDbConnection1 = getConnected();
            OleDbCommand myOleDbCommand1 = oleDbConnection1.CreateCommand();
            bool boolResult = false;
            List<String> trueList = new List<String>();
            myOleDbCommand1.CommandText = "SELECT calenderdate, orgcalenderid, partyroleid, workdayflag, holidaydesc, holidayduedateflag, NVL(dayofweek, 'DayOfWeekMissing') dayofweek, decode(opentime, null, 'CAMENULL',opentime) opentime, decode(closetime, null, 'CAMENULL',closetime) closetime FROM orgcalender WHERE partyroleid = '" + partyRoleId + "'";
            oleDbConnection1.Open();
            OleDbDataReader myOleDbDataReader1 = myOleDbCommand1.ExecuteReader();
            while (myOleDbDataReader1.Read())
            {
                string orgId = myOleDbDataReader1["orgcalenderid"].ToString();
                string partyroleid = myOleDbDataReader1["partyroleid"].ToString();
                string workdayflag = myOleDbDataReader1["workdayflag"].ToString();
                string holidaydesc = myOleDbDataReader1["holidaydesc"].ToString();
                string holidayduedateflag = myOleDbDataReader1["holidayduedateflag"].ToString();
                string dayofweek = myOleDbDataReader1["dayofweek"].ToString();
                string opentime = myOleDbDataReader1["opentime"].ToString();
                string closetime = myOleDbDataReader1["closetime"].ToString();
                DateTime calendardate = (DateTime)myOleDbDataReader1["calenderdate"];
                boolResult = DateTime.Equals(calendardate, inputDate);
                bool isSat = isSaturday(inputDate);
                if ((boolResult.Equals(true) && opentime.Equals("CAMENULL") && closetime.Equals("CAMENULL") && workdayflag.Equals("0")) || (isSat.Equals(true)))
                {
                    trueList.Add(calendardate.ToString());
                }

            }
            myOleDbDataReader1.Close();
            oleDbConnection1.Close();

            return trueList;*/
            return new List<string>();
        }


        public static bool isSaturday(DateTime dtInput)
        {
            return ((dtInput.DayOfWeek == DayOfWeek.Saturday)) ? true : false;
        }
    }

}
