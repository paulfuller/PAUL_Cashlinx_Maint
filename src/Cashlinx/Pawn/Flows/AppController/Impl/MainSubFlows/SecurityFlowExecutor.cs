using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Controllers.Application.ApplicationFlow.Impl.Common;
using Common.Controllers.Application.ApplicationFlow.Navigation;

namespace Pawn.Flows.AppController.Impl.MainSubFlows
{
    public class SecurityFlowExecutor : SingleExecuteBlock
    {
        public static readonly string NAME = "SecurityFlowExecutor";

        public enum SecurityFlowState
        {
            SelectEmployee,
            SecurityProfile,
            AddEmployee,
            Exit,
            Cancel

        }

        private SecurityFlowState nextState;
        private Form parentForm;
        private FxnBlock endStateNotifier;

        /// <summary>
        /// Main execution function for SecurityFlowExecutor
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        private object executorFxn(object inputData)
        {
            if (inputData == null)
                return (false);
            SecurityFlowState inputState = (SecurityFlowState)inputData;

            switch (inputState)
            {
                case SecurityFlowState.SelectEmployee:
                    ShowForm selEmployeeBlk = CommonAppBlocks.Instance.SelectEmployeeFormBlock(this.parentForm, this.selectEmployeeFormNavAction);
                    if (!selEmployeeBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute SelectEmployee block");
                    }

                    break;
                case SecurityFlowState.SecurityProfile:
                    ShowForm secProfileBlk = CommonAppBlocks.Instance.SecurityProfileFormBlock(this.parentForm, this.securityProfileFormNavAction);
                    if (!secProfileBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Security Profile block");
                    }
                    break;

                case SecurityFlowState.AddEmployee:
                    ShowForm addEmpBlk = CommonAppBlocks.Instance.AddEmployeeFormBlock(this.parentForm, this.addEmployeeFormNavAction);
                    if (!addEmpBlk.execute())
                    {
                        throw new ApplicationException("Cannot execute Add Employee block");
                    }
                    break;


                case SecurityFlowState.Cancel:

                    if (this.endStateNotifier != null)
                        this.endStateNotifier.execute();
                    break;


                default:
                    throw new ApplicationException("Invalid Security flow state");
            }

            return (true);
        }



        /// <summary>
        /// NavBox OnAction Handler for Select Employee Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void selectEmployeeFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Select Employee form navigation action handler received invalid data");
            }

            NavBox selectEmpNavBox = (NavBox)sender;
            NavBox.NavAction action = selectEmpNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    if (selectEmpNavBox.IsCustom)
                    {
                        string custDet = selectEmpNavBox.CustomDetail;
                        if (custDet.Equals("EmployeeDetails", StringComparison.OrdinalIgnoreCase))
                            this.nextState = SecurityFlowState.SecurityProfile;
                        else
                            this.nextState = SecurityFlowState.Cancel;
                    }
                    else
                        this.nextState = SecurityFlowState.Cancel;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = SecurityFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Select Employee");
            }

            this.executeNextState();
        }

        /// <summary>
        /// Action class for Security Profile Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void securityProfileFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Security Profile form navigation action handler received invalid data");
            }

            NavBox secProfileNavBox = (NavBox)sender;
            NavBox.NavAction action = secProfileNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }

            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = SecurityFlowState.SelectEmployee;
                    break;

                case NavBox.NavAction.CANCEL:
                    this.nextState = SecurityFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Security Profile");
            }
            this.executeNextState();
        }


        /// <summary>
        /// NavBox OnAction Handler for Add Employee Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void addEmployeeFormNavAction(object sender, object data)
        {
            if (sender == null || data == null)
            {
                throw new ApplicationException("Add Employee form navigation action handler received invalid data");
            }

            NavBox addEmployeeNavBox = (NavBox)sender;
            NavBox.NavAction action = addEmployeeNavBox.Action;
            if (action == NavBox.NavAction.BACKANDSUBMIT)
            {
                GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
                action = NavBox.NavAction.SUBMIT;
            }
            switch (action)
            {
                case NavBox.NavAction.SUBMIT:
                    this.nextState = SecurityFlowState.SelectEmployee;
                    break;
                case NavBox.NavAction.CANCEL:
                    this.nextState = SecurityFlowState.Cancel;
                    break;
                default:
                    throw new ApplicationException("" + action.ToString() + " is not a valid state for Add Employee Form");
            }

            this.executeNextState();
        }




        /// <summary>
        /// 
        /// </summary>
        private void executeNextState()
        {
            object evalExecFlag = this.executorFxn(this.nextState);
            if (evalExecFlag == null || ((bool)(evalExecFlag)) == false)
            {
                throw new ApplicationException("Cannot execute the next state: " + this.nextState.ToString());
            }
        }

        public SecurityFlowExecutor(Form parentForm, FxnBlock eStateNotifier)
            : base(NAME)
        {
            this.parentForm = parentForm;
            this.endStateNotifier = eStateNotifier;
            this.nextState = SecurityFlowState.SelectEmployee;
            this.setExecBlock(this.executorFxn);
            this.executeNextState();
        }
    }
}


