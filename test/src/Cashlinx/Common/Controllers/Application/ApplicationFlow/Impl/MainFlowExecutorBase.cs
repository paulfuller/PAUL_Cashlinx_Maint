using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Blocks.Executors;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;

namespace Common.Controllers.Application.ApplicationFlow.Impl
{
    public class MainFlowExecutorBase : SingleExecuteBlock
    {
        private DesktopSession desktopSession;

        public Form ParentForm { set; get; }

        public SingleExecuteBlock ParentFlowExecutor { set; get; }

        public FxnBlock EndStateNotifier { set; get; }

        private Dictionary<string, string> primaryFlowExecutors;

        public MainFlowExecutorBase(
            DesktopSession dSession) : base("MainFlowExecutorBaseBlock")
        {
            this.desktopSession = dSession;
            this.primaryFlowExecutors = new Dictionary<string, string>();
            this.EndStateNotifier = null;
            this.ParentForm = null;
            this.ParentFlowExecutor = null;
        }

        public virtual void AddFlowExecutor(string trigger, string flowExecutorType)
        {
            this.primaryFlowExecutors.Add(trigger, flowExecutorType);
        }

        protected virtual object executorFxn(object inputData)
        {
            if (inputData == null || (!(inputData is string)))
            {
                return null;
            }

            var trigger = (string)inputData;
            if (CollectionUtilities.isNotEmptyContainsKey(this.primaryFlowExecutors, trigger))
            {
                var t = Type.GetType(this.primaryFlowExecutors[trigger]);
                try
                {
                    //Kick-off the flow
                    if (t != null)
                    {
                        Activator.CreateInstance(t, desktopSession, this.ParentForm, this.EndStateNotifier);
                    }
                    else
                    {
                        throw new Exception(string.Format("Type for trigger {0} is null", trigger));
                    }
                }
                catch (Exception eX)
                {
                    BasicExceptionHandler.Instance.AddException("Exception thrown while creating flow executor from a Type", eX);
                    if (FileLogger.Instance.IsLogError)
                    {
                        if (t != null)
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                           "Cannot initiate flow executor type {0} for trigger {1}: Exception thrown: {2}",
                                                           t.FullName, trigger, eX);
                        }
                        else
                        {
                            FileLogger.Instance.logMessage(LogLevel.ERROR, this,
                                                           "Cannot initiate flow executor type {0} for trigger {1}: Exception thrown: {2}",
                                                           "null", trigger, eX);
                            
                        }
                    }
                }
            }

            return null;
        }

    }
}
