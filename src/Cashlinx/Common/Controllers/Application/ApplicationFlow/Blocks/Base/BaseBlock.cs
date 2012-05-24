using System;

namespace Common.Controllers.Application.ApplicationFlow.Blocks.Base
{
    public delegate void NotifyHandler(object src, object data);

    public abstract class BaseBlock
    {
        public string Name { protected set; get; }

        public NotifyHandler Notifier { set; get; }

        protected void setName(string nm)
        {
            if (string.IsNullOrEmpty(nm))
            {
                throw new ApplicationException("Cannot create block - Name is invalid");
            }
            this.Name = nm;
        }

        public abstract bool execute(); 
    }
}