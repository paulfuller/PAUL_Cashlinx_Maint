using System;
using System.Collections.Generic;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Type;

namespace Common.Controllers.Application.ApplicationFlow.Blocks.Logical
{
    public class IfThenElseBlock : BaseBlock
    {
        public enum IfElseExecState
        {
            IFBLOCK = 0,
            ELSEBLOCK = 1
        }

        private List<PairType<ConditionBlock, ActionBlock>> ifElseBlockPairs;
        private Dictionary<string, bool> ifMap;
        private ActionBlock elseBlock;

        private ActionBlock execBlock;
        private IfElseExecState execState;

        public IfElseExecState ExecState
        {
            get
            {
                return (this.execState);
            }
        }

        public ActionBlock ExecBlock
        {
            get
            {
                return (this.execBlock);
            }
        }

        public ActionBlock ExecElseBlock
        {
            get
            {
                return (this.elseBlock);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="data"></param>
        private void internalNotifier(object src, object data)
        {
            if (src != null && data != null)
            {
                ifMap.Add(((ConditionBlock)src).Name, (bool)data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm"></param>
        /// <param name="iFxns"></param>
        /// <param name="elseFxn"></param>
        public IfThenElseBlock(
            string nm,
            List<PairType<FxnBlock,FxnBlock>> iFxns,
            FxnBlock elseFxn)
        {
            if (string.IsNullOrEmpty(nm) ||
                CollectionUtilities.isEmpty(iFxns))
            {
                throw new ApplicationException("Cannot create IfThenElse block");
            }

            //Set the necessary fields
            this.ifMap = new Dictionary<string, bool>();
            this.setName(nm);
            this.ifElseBlockPairs = new List<PairType<ConditionBlock, ActionBlock>>();
            this.elseBlock = new ActionBlock(nm + "-ElseAction", elseFxn);
            int cnt = 0;

            //Create the if blocks
            foreach (PairType<FxnBlock, FxnBlock> fxn in iFxns)
            {
                FxnBlock condFxn = fxn.Left;
                FxnBlock actFxn = fxn.Right;

                //Create condition block
                ConditionBlock cndBlk = new ConditionBlock(nm + "-Condition-" + cnt, fxn.Left);
                //Create action block
                ActionBlock actBlk = new ActionBlock(nm + "-Action-" + cnt, fxn.Right);

                //Create bound pair of condition to action
                PairType<ConditionBlock, ActionBlock> ifElsBlk = 
                    new PairType<ConditionBlock, ActionBlock>(cndBlk, actBlk);

                //Add this to the if else block pair list
                this.ifElseBlockPairs.Add(ifElsBlk);

                //Increase count
                cnt++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm"></param>
        /// <param name="iFxns"></param>
        /// <param name="elseFxn"></param>
        public IfThenElseBlock(
            string nm,
            List<PairType<Func<object,object>, Func<object,object>>> iFxns,
            Func<object,object> elseFxn)
        {
            if (string.IsNullOrEmpty(nm) ||
                CollectionUtilities.isEmpty(iFxns))
            {
                throw new ApplicationException("Cannot create IfThenElse block");
            }

            //Set the necessary fields
            this.ifMap = new Dictionary<string, bool>();
            this.setName(nm);
            this.ifElseBlockPairs = new List<PairType<ConditionBlock, ActionBlock>>();
            this.elseBlock = new ActionBlock(nm + "-ElseAction", elseFxn);
            int cnt = 0;

            //Create the if blocks
            foreach (PairType<Func<object,object>, Func<object,object>> fxn in iFxns)
            {
                Func<object, object> condFxn = fxn.Left;
                Func<object, object> actFxn = fxn.Right;

                //Create condition block
                ConditionBlock cndBlk = new ConditionBlock(nm + "-Condition-" + cnt, fxn.Left);
                //Create action block
                ActionBlock actBlk = new ActionBlock(nm + "-Action-" + cnt, fxn.Right);

                //Create bound pair of condition to action
                PairType<ConditionBlock, ActionBlock> ifElsBlk =
                    new PairType<ConditionBlock, ActionBlock>(cndBlk, actBlk);

                //Add this to the if else block pair list
                this.ifElseBlockPairs.Add(ifElsBlk);

                //Increase count
                cnt++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nm"></param>
        /// <param name="iFxns"></param>
        /// <param name="elseFxn"></param>
        public IfThenElseBlock(
            string nm,
            List<PairType<ConditionBlock, ActionBlock>> iFxns,
            ActionBlock elseFxn)
        {
            if (string.IsNullOrEmpty(nm) ||
                CollectionUtilities.isEmpty(iFxns))
            {
                throw new ApplicationException("Cannot create IfThenElse block");
            }

            //Set the necessary fields
            this.ifMap = new Dictionary<string, bool>();
            this.setName(nm);
            this.ifElseBlockPairs = iFxns;
            this.elseBlock = elseFxn;
        }
        
        /// <summary>
        /// Execute the if else logic. The first if that succeeds will
        /// have its action block executed, then the if else block will
        /// stop its execution.  If no if blocks succeed, the else block 
        /// will execute
        /// </summary>
        /// <param name="notifier"></param>
        /// <returns></returns>
        public override bool execute()
        {
            if (CollectionUtilities.isEmpty(this.ifElseBlockPairs) ||
                this.elseBlock == null)
            {
                return (false);
            }

            //Execute the if statements to see if any evaluate to true, and if
            //so, execute the associated action
            bool executedBlk = false;
            bool execStatus = false;
            foreach (PairType<ConditionBlock, ActionBlock> ifBlk in this.ifElseBlockPairs)
            {
                if (ifBlk == null)
                    continue;
                //Execute the current condition
                ifBlk.Left.Notifier = this.internalNotifier;
                execStatus = ifBlk.Left.execute();
                if (execStatus && this.ifMap.ContainsKey(ifBlk.Left.Name))
                {
                    //If the block executed and evaluated true, execute the associated
                    //action block
                    bool evalVal = this.ifMap[ifBlk.Left.Name];
                    if (evalVal)
                    {
                        bool actExecStatus = ifBlk.Right.execute();
                        if (actExecStatus)
                        {
                            executedBlk = true;
                            this.execBlock = ifBlk.Right;
                            this.execState = IfElseExecState.IFBLOCK;
                            if (this.Notifier != null)
                            {
                                this.Notifier.Invoke(this, ifBlk.Right);
                            }
                            break;
                        }
                    }
                }
            }

            //Check to see if any ifblock evaluated to true.  If not,
            //execute else block
            if (!executedBlk)
            {
                execStatus = this.elseBlock.execute();
                if (execStatus)
                {
                    executedBlk = true;
                    this.execBlock = this.elseBlock;
                    this.execState = IfElseExecState.ELSEBLOCK;
                    if (this.Notifier != null)
                    {
                        this.Notifier.Invoke(this, this.elseBlock);
                    }
                }
            }

            return (executedBlk);
        }
    }
}
