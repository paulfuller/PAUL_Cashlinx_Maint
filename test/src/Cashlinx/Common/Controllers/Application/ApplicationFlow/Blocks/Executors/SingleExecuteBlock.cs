using System;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;

namespace Common.Controllers.Application.ApplicationFlow.Blocks.Executors
{
    public class SingleExecuteBlock : BaseBlock
    {
        private ActionBlock executeBlock;

        public ActionBlock ExecBlock
        {
            get
            {
                return (this.executeBlock);
            }
            set
            {
                this.executeBlock = value;
            }
        }

        public void setExecBlock(Func<object,object> axn)
        {
            this.executeBlock = new ActionBlock(this.Name + "-SingleExecute", axn);
        }

        public SingleExecuteBlock()
        {            
        }

        public SingleExecuteBlock(
            string nm)
        {
            if (string.IsNullOrEmpty(nm))
            {
                throw new ApplicationException("Cannot create SingleExecuteBlock");
            }
            this.setName(nm);
        }

        public SingleExecuteBlock(
            string nm,
            Func<object,object> axn)
        {
            if (string.IsNullOrEmpty(nm))
            {
                throw new ApplicationException("Cannot create SingleExecuteBlock");
            }
            this.setName(nm);
            this.executeBlock = new ActionBlock(this.Name + "-SingleExecute", axn);
        }

        public SingleExecuteBlock(
            string nm,
            FxnBlock axn)
        {
            if (string.IsNullOrEmpty(nm))
            {
                throw new ApplicationException("Cannot create SingleExecuteBlock");
            }
            this.setName(nm);
            this.executeBlock = new ActionBlock(this.Name + "-SingleExecute", axn);
        }

        public SingleExecuteBlock(
            string nm,
            ActionBlock axn)
        {
            if (string.IsNullOrEmpty(nm))
            {
                throw new ApplicationException("Cannot create SingleExecuteBlock");
            }
            this.setName(nm);
            this.executeBlock = axn;
        }

        public override bool execute()
        {
            this.executeBlock.Notifier = this.Notifier;
            bool rt = this.executeBlock.execute();
            return (rt);
        }
    }
}