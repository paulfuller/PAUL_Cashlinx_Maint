using System;

namespace Common.Controllers.Application.ApplicationFlow.Blocks.Base
{
    public class ConditionBlock : BaseBlock
    {
        private bool logicEvalValue;

        public bool Value
        {
            get
            {
                return (this.logicEvalValue);
            }
        }

        public FxnBlock LogicFxn
        {
            get;
            set;
        }

        public ConditionBlock(string nm)
        {
            this.setName(nm);
            this.logicEvalValue = false;
        }

        public ConditionBlock(string nm, FxnBlock cond)
        {
            this.setName(nm);
            this.LogicFxn = cond;
            this.logicEvalValue = false;
        }

        public ConditionBlock(string nm, Func<object, object> fxn)
        {
            this.LogicFxn = new FxnBlock();
            this.LogicFxn.Function = fxn;
            this.logicEvalValue = false;
        }

        /// <summary>
        /// Condition block execute, passes true or false to notifier
        /// per the condition block result.  Function returns execution
        /// status success or failure
        /// </summary>
        /// <param name="notifier"></param>
        /// <returns></returns>
        public override bool execute()
        {
            if (!this.LogicFxn.execute())
            {
                return (false);
            }

            object fxnResult = this.LogicFxn.ReturnParameter;
            this.logicEvalValue = false;
            if (fxnResult != null)
            {
                //If this is a boolean type, determine is true/false
                //value and return that
                if (fxnResult is Boolean)
                {
                    Boolean fxnResultBool = (Boolean)fxnResult;
                    this.logicEvalValue = fxnResultBool;
                }
                //Otherwise, since it is an unknown object and is
                //not null, then we must assume it is true
                else
                {
                    this.logicEvalValue = true;
                }
            }

            //Execute notifier if it is valid
            if (this.Notifier != null)
            {
                this.Notifier.Invoke(this, this.logicEvalValue);
            }

            return (true);
        }
    }
}
