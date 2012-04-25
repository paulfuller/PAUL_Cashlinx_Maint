using System;
using System.Collections.Generic;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Libraries.Utility.Collection;

namespace Common.Controllers.Application.ApplicationFlow.Blocks.Executors
{
    public class MultiExecuteSyncBlock : BaseBlock
    {
        private List<ActionBlock> fxns;
        public static readonly string NAMEAPPEND = "-MultiExecuteSyncBlock-";

        public ActionBlock this[int index]
        {
            get
            {
                if (CollectionUtilities.isEmpty(this.fxns))
                    return (null);
                if (index < 0 || index >= this.fxns.Count)
                    return (null);
                return (this.fxns[index]);
            }
        }

        public MultiExecuteSyncBlock(string nm)
        {
            this.setName(nm);
            this.fxns = new List<ActionBlock>();
        }

        public void addBlock(Func<object, object> fxn)
        {
            string intNm = this.Name + NAMEAPPEND + this.fxns.Count;
            this.fxns.Add(new ActionBlock(intNm, fxn));
        }

        public void addBlock(FxnBlock fxn)
        {
            string intNm = this.Name + NAMEAPPEND + this.fxns.Count;
            this.fxns.Add(new ActionBlock(intNm, fxn));
        }

        public void addBlock(ActionBlock actFxn)
        {
            if (actFxn == null)
                return;
            this.fxns.Add(actFxn);
        }

        public override bool execute()
        {
            if (CollectionUtilities.isEmpty(this.fxns))
                return (false);

            bool rt = true;
            foreach (ActionBlock actBlk in this.fxns)
            {
                if (actBlk == null)
                    continue;
                actBlk.Notifier = this.Notifier;
                if (!actBlk.execute())
                {
                    rt = false;
                    break;
                }
            }
            return (rt);
        }
    }
}