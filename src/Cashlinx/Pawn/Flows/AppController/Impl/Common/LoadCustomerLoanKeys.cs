using System.Collections.Generic;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Logical;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Pawn;
using Pawn.Logic;

namespace Pawn.Flows.AppController.Impl.Common
{
    public class LoadCustomerLoanKeys : IfThenBlock
    {
        private string customerNumber;

        private object canLoadLoanKeys(object data)
        {
            DesktopSession cds = 
                GlobalDataAccessor.Instance.DesktopSession;

            //Ensure we have an active customer before allowing
            //the loan keys call
            if (cds.ActiveCustomer != null &&
                !string.IsNullOrEmpty(cds.ActiveCustomer.CustomerNumber))
            {
                this.customerNumber = cds.ActiveCustomer.CustomerNumber;
                //cds.L
                return (true);
            }
            return (false);
        }

        private object loadLoanKeys(object data)
        {
            DesktopSession cds = 
                GlobalDataAccessor.Instance.DesktopSession;
            //Get the minimum store interest charge from business rule
            decimal minFinanceCharge = new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetStoreMinimumIntCharge();
            string errorCode;
            string errorDesc;
            List<PawnLoan> pawnLoanKeys;
            //Check if pawn loan keys is already loaded for this customer in session
            //if yes do not call SP again
            if (cds.PawnLoanKeys.Count > 0 && cds.PawnLoanKeys[0].CustomerNumber == cds.ActiveCustomer.CustomerNumber)
            {
                return true;
            }
            cds.PawnLoanKeys.Clear();
            bool spCallRes =
                CustomerLoans.GetLoanKeys(this.customerNumber, minFinanceCharge, ShopDateTime.Instance.ShopDate,
                out pawnLoanKeys, out errorCode, out errorDesc);
            cds.PawnLoanKeys.AddRange(pawnLoanKeys.AsEnumerable());
            return (spCallRes);
        }
            

        public LoadCustomerLoanKeys() : base("LoadCustomerLoanKeys")
        {
            this.customerNumber = string.Empty;
            this.setConditionAndAction(this.canLoadLoanKeys,
                this.loadLoanKeys);
        }

    }
}
