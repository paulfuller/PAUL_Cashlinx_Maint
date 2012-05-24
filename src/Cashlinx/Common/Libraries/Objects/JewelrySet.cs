/**************************************************************************************
 * Namespace:       PawnObjects.Pawn.Includes
 * Class:           JewelrySet
 * 
 * Description      The class keeps the attribute description, type of the attribute,
 *                  prefix, suffix, etc.  Each category has multiple attributes.  These
 *                  are the fields that are displayed on UI and some fields require
 *                  user input.
 * 
 * History
 * David D Wise, 3-20-2009, Initial Development
 * 
 * ************************************************************************************/

using System.Collections.Generic;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects
{
    public class JewelrySet
    {
        public List<ItemAttribute>  ItemAttributeList       {get; set;} // 
        public int                  CaccLevel               {get; set;} //
        public int                  Category                {get; set;} //
        public string               CategoryDescription     {get; set;} //
        public string               Icn                     {get; set;}
        public int                  SubItemNumber           {get; set;}
        public string               TicketDescription       {get; set;} //
        public decimal              TotalStoneValue         {get; set;} //

    }
}
