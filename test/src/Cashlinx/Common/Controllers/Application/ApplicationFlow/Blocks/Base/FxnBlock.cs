using System;

namespace Common.Controllers.Application.ApplicationFlow.Blocks.Base
{
    public class FxnBlock
    {
        public Func<object, object> Function { set; get; }

        public object InputParameter { set; get; }

        public object ReturnParameter { set; get; }

        public FxnBlock()
        {
            this.Function = null;
            this.InputParameter = null;
            this.ReturnParameter = null;
        }

        public bool execute()
        {
            if (Function != null)
            {
                this.ReturnParameter = this.Function(this.InputParameter);
                return (true);
            }
            return (false);
        }
    }
}