using System;
using System.Collections.Generic;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Logical;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects.Pawn;
using Support.Libraries.Objects.PDLoan;
using Support.Logic;

namespace Support.Flows.AppController.Impl.Common
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
            CashlinxPawnSupportSession cds = Support.Logic.CashlinxPawnSupportSession.Instance;
                //GlobalDataAccessor.Instance.DesktopSession;
            //Get the minimum store interest charge from business rule
            decimal minFinanceCharge = 0.0M; //new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).GetStoreMinimumIntCharge();
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

        private object loadPDLoanKeys(object data)
        {
            var cds = Support.Logic.CashlinxPawnSupportSession.Instance;
            cds.PDLoanKeys.Clear();

            string errorCode;
            string errorDesc;

            var PDLoanKeys = new List<PDLoan>();

            bool returnVal = Support.Controllers.Database.Procedures.CustomerLoans.GetPDLoanKeys(
                cds.ActiveCustomer.CustomerNumber, out PDLoanKeys, out errorCode, out errorDesc);


            cds.PDLoanKeys.AddRange(PDLoanKeys.AsEnumerable());

            //TODO: Replace this with DB call
            //PDLoan sampleLoan1 = new PDLoan();
            //sampleLoan1.PDLLoanNumber = "10000000015810";
            //sampleLoan1.Status = "CLOSED";
            //sampleLoan1.StatusDate = DateTime.Now;
            //sampleLoan1.Type = "P";
            //cds.PDLoanKeys.Add(sampleLoan1);

            //PDLoan sampleLoan2 = new PDLoan();
            //sampleLoan2.PDLLoanNumber = "10000000000020";
            //sampleLoan2.Status = "ACTIVE";
            //sampleLoan2.StatusDate = DateTime.Now;
            //sampleLoan2.Type = "I";
            //cds.PDLoanKeys.Add(sampleLoan2);

            return returnVal;

        }
        public LoadCustomerLoanKeys() : base("LoadCustomerLoanKeys")
        {
            this.customerNumber = string.Empty;
            this.setConditionAndAction(this.canLoadLoanKeys,
                this.loadLoanKeys);
            this.setConditionAndAction(this.canLoadLoanKeys,
                this.loadPDLoanKeys);
        }

    }
}
