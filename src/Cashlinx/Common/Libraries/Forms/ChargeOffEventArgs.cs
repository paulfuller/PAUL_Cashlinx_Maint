using System;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms
{
    public class ChargeOffEventArgs : EventArgs
    {
        public ItemReason Reason { get; set; }
    }
}
