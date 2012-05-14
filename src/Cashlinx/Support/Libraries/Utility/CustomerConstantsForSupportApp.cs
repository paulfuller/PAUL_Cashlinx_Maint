/********************************************************************
* PawnUtilities.Shared
* CustomerConstants
* Constants file that hold enums and constant strings used in
* all the customer related modules
* Sreelatha Rengarajan 4/1/2009 Initial version
 * Sreelatha Rengarajan 4/19/2010 Added creation date to mdse record
*******************************************************************/

using System.Collections.Generic;
using System.Linq;

namespace Support.Libraries.Utility
{
     
    public enum customerrecord
    {
        CUSTOMER_ROW_NUMBER = 0,
        CUSTOMER_NUMBER,
        CUST_LAST_NAME,
        CUST_FIRST_NAME,
        DATE_OF_BIRTH,
        CUST_TITLE,
        CUST_MIDDLE_NAME,
        SOCIAL_SECURITY_NUMBER,
        RACE,
        GENDER,
        EYE_COLOR,
        HAIR_COLOR,
        HEIGHT,
        WEIGHT,
        PARTY_ID,
        NAME_SUFFIX,
        PREFERCONTMTHDTEXT,
        NOCALLFLAG,
        NOMAILFLAG,
        NOEMAILFLAG,
        NOFAXFLAG,
        OPTOUTFLAG,
        REMINDERCONTACT,
        PREFERCONTTIME,
        RECEIVEPROMOTIONS,
        HOWDIDYOUHEAR,        
        CREATIONDATE,
        PREFERLANGCODE,
        EMPTITLE,
        EMPNAME = 29,
        MARITAL_STATUS,
        SPOUSE_FIRST_NAME,
        SPOUSE_LAST_NAME,
        SPOUSE_SSN,
        CUST_SEQUENCE_NUMBER,
        PRIVACY_NOTIFICATION_DATE,
        OPT_OUT_FLAG,
        STATUS,
        REASON_CODE,
        LAST_VERIFICATION_DATE,
        NEXT_VERIFICATION_DATE,
        COOLING_OFF_DATE_PDL,
        CUSTOMER_SINCE_PDL,
        SPANISH_FORM,
        PRBC,
        PLANBANKRUPTCY_PROTECTION,
        YEARS,
        MONTHS,
        OWN_HOME,
        MONTHLY_RENT,
        MILITARY_STATIONED_LOCAL
    }

    public enum categoryrecord
    {
        CATEGORYID = 0,
        CATEGORYNAME,
        DESCRIPTION
    }

    /*
    public enum CommentColumnOrder
    {
        dgColumnComment = 0,
        dgColumnDate,
        dgColumnUpDatedBy,
        dgColumnCategory,
        dgColumnEmpNumber,

    } */

    public enum customercommentrecord
    {
        COMMENTS = 0,
        DATA_MADE,
        MADE_BY,
        CATEGORY,
        EMPLOYEE_NBR,

    } 
}
