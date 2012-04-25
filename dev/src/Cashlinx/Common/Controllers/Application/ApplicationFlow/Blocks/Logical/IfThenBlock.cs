using System;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;

namespace Common.Controllers.Application.ApplicationFlow.Blocks.Logical
{
    public class IfThenBlock : BaseBlock
    {
        private ConditionBlock ifLogic;
        private ActionBlock action;
        private bool ifLogicValue;

        public ConditionBlock Condition
        {
            get
            {
                return (this.ifLogic);
            }
            set
            {
                this.ifLogic = value;
            }
        }

        public ActionBlock Action
        {
            get
            {
                return (this.action);
            }
            set
            {
                this.action = value;
            }
        }

        public void setConditionAndAction(
            Func<object, object> condAxn,
            Func<object, object> actAxn)
        {
            this.Condition = new ConditionBlock(this.Name + "-Condition", condAxn);
            this.Action = new ActionBlock(this.Name + "-Action", actAxn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="data"></param>
        private void internalNotifier(object src, object data)
        {
            this.ifLogicValue = false;
            if (data != null)
            {
                this.ifLogicValue = (bool)data;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm"></param>
        /// <param name="ifFxn"></param>
        /// <param name="actionFxns"></param>
        public IfThenBlock(
            string nm)
        {
            this.setName(nm);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm"></param>
        /// <param name="ifFxn"></param>
        /// <param name="actionFxns"></param>
        public IfThenBlock(
            string nm,
            FxnBlock ifFxn,
            FxnBlock actionFxn)
        {
            this.setName(nm);
            this.ifLogic = new ConditionBlock(this.Name + "-Condition", ifFxn);
            this.action = new ActionBlock(this.Name + "-Action", actionFxn);
        }

        public IfThenBlock(
            string nm,
            Func<object,object> ifFxn,
            Func<object,object> actionFxn)
        {
            this.setName(nm);
            this.ifLogic = new ConditionBlock(this.Name + "-Condition", ifFxn);
            this.action = new ActionBlock(this.Name + "-Action", actionFxn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm"></param>
        /// <param name="ifBlk"></param>
        /// <param name="actBlk"></param>
        public IfThenBlock(
            string nm,
            ConditionBlock ifBlk,
            ActionBlock actBlk)
        {
            this.setName(nm);
            this.ifLogic = ifBlk;
            this.action = actBlk;
        }

        //Only execute the action block if the condition block
        //evaluates to true
        public override bool execute()
        {
            bool rt = false;
            this.ifLogic.Notifier = this.internalNotifier;
            bool execStatus = this.ifLogic.execute();
            if (execStatus && this.ifLogicValue)
            {
                rt = this.action.execute();
            }

            if (rt && this.Notifier != null)
            {
                this.Notifier.Invoke(this, this.action);
            }

            return (rt);
        }
    }
}
