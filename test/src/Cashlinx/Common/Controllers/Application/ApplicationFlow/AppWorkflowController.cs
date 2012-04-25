using System;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl;

//using AppControllerWorkFlows.Sequential.Customer;
//using AppControllerWorkFlows.Sequential.Pawn;

namespace Common.Controllers.Application.ApplicationFlow
{
    public sealed class AppWorkflowController : MarshalByRefObject
    {
        private DesktopSession desktopSession;
        private MainFlowExecutorBase mainFlowExecutor;
        
        public AppWorkflowController(DesktopSession dSession, MainFlowExecutorBase mainInstance)
        {
            this.desktopSession = dSession;
            this.mainFlowExecutor = mainInstance;            
        }

        public void invokeWorkflow(string trigger, Form parentForm, FxnBlock endNavCall)
        {
            this.mainFlowExecutor.ExecBlock.Action.InputParameter = trigger;
            this.mainFlowExecutor.ParentForm = parentForm;
            this.mainFlowExecutor.EndStateNotifier = endNavCall;
            this.mainFlowExecutor.execute();
        }

        public void invokeWorkflow(string trigger, Form parentForm, FxnBlock endNavCall, SingleExecuteBlock parentFlow)
        {
            this.mainFlowExecutor.ExecBlock.Action.InputParameter = trigger;
            this.mainFlowExecutor.ParentForm = parentForm;
            this.mainFlowExecutor.ParentFlowExecutor = parentFlow;
            this.mainFlowExecutor.EndStateNotifier = endNavCall;
            this.mainFlowExecutor.execute();
        }
    }
}
