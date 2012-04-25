using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using System;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Impl;

namespace Support.Flows
{
    public class MainFlowExecutor : MainFlowExecutorBase
    {
        public const string NEWPAWNLOAN = "newpawnloan";

        private FxnBlock endStateNotifier;
        public void setEndStateNotifier(FxnBlock fB)
        {
            this.endStateNotifier = fB;
        }

        protected override object executorFxn(object inputData)
        {
            if (inputData == null || (!(inputData is string)))
            {
                return (null);
            }

            string menuTrigger = (string)inputData;
            //Only for example
            if (menuTrigger.Equals(NEWPAWNLOAN, StringComparison.OrdinalIgnoreCase))
            {
/*                //Orchestrate the new pawn loan flow
                this.newPawnLoanFlowExecutor =
                new NewPawnLoanFlowExecutor(this.ParentForm, this.endStateNotifier);*/
            }

            return (null);
        }

        public MainFlowExecutor() : base(GlobalDataAccessor.Instance.DesktopSession)
        {
            //this.newPawnLoanFlowExecutor = null;
            this.setExecBlock(executorFxn);
            this.endStateNotifier = null;
        }
    }
}