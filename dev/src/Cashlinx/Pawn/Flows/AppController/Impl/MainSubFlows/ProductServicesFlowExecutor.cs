using System;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class ProductServicesFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "ProductServicesFlowExecutor";
        private Form parentForm;

        /// <summary>
        /// Main execution function for NewPawnLoanFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            ShowForm ProductServicesBlk = CommonAppBlocks.Instance.CreateProductServicesBlock(this.parentForm, this.productServicesFormNavAction);
            if (!ProductServicesBlk.execute())
            {
                throw new ApplicationException("Cannot execute Product Services block");
            }
            return (true);
        }

        /// <summary>
        /// NavBox OnAction Handler for Lookup Customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void productServicesFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Product Services form navigation action handler received invalid data");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void executeNextState()
        {
            object evalExecFlag = this.executorFxn(null);
            if (evalExecFlag == null || ((bool)(evalExecFlag)) == false)
            {
                throw new ApplicationException("Cannot execute form.");
            }
        }

        public ProductServicesFlowExecutor(Form parentForm)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
    }
}
