/************************************************************************
 * Namespace:       PawnUtilities.Shared
 * Class:           ...
 * 
 * Description      The file manages multiple enums, structs, and classes
 *                  associated to Pawn Loan data, analytics, and rules.
 * 
 * History
 * David D Wise, Initial Development
 * Sreelatha Rengarajan 10/23/2009 Added  properties in the fee struct
 * SR 6/2/2010 Added enum for user info record
 * SR 7/26/2010 Added enum for cash transfer types and cash transfer status and
 * transfer details record
 * SR 2/14/2011 Added enum for StoreCreditStatus
 * **********************************************************************/

using System;
using System.Collections.Generic;
using System.Data;

namespace Support.Libraries.Utility
{
    public enum SupportProductType : int
    {
        NONE = 0,
        PAWN = 1,
        BUY = 2,
        SALE = 3,
        LAYAWAY=4,
        ALL = 5,
        PDL = 6
    }
}
