using System;

namespace Common.Controllers.Application.ApplicationFlow.Blocks.Base
{
    public class ActionBlock : BaseBlock
    {
        private FxnBlock action;
        
        /// <summary>
        /// 
        /// </summary>
        public FxnBlock Action
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm"></param>
        public ActionBlock(string nm)
        {
            this.setName(nm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm"></param>
        /// <param name="actType"></param>
        /// <param name="cNotifyFxn"></param>
        public ActionBlock(
            string nm,
            FxnBlock fxn)
        {
            this.setName(nm);
            this.action = fxn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm"></param>
        /// <param name="fxn"></param>
        public ActionBlock(
            string nm,
            Func<object, object> fxn)
        {
            this.setName(nm);
            this.action = new FxnBlock();
            this.action.Function = fxn;
        }

        /// <summary>
        /// Execute all Function blocks.  Will 
        /// return <code>false</code> if and only if
        /// any one of the function blocks fails to execute
        /// </summary>
        /// <returns>Overall success of the execution</returns>
        public override bool execute()
        {
            //Execute the fxn in order
            bool rt = this.action.execute();

            //If the notifier is not null, 
            //and the status is true, make a notify call
            //boolean value not needed
            if (rt && this.Notifier != null)
            {
                this.Notifier.Invoke(this, null);
            }
            return (rt);
        }
    }
}