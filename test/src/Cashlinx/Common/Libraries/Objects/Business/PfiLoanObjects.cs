using System;
using System.Collections.Generic;
using Common.Libraries.Objects.Customer;

namespace Common.Libraries.Objects.Business
{
    public class PFI_ProductData
    {
        public CustomerProductDataVO OriginalObject;
        public CustomerProductDataVO UpdatedObject;
        public DateTime SuspendDate;
        public List<PFI_Merged> MergedItems;
    }

    public class PFI_Merged
    {
        public Item NewItem;
        public List<Item> OriginalItems;
    }

    public class PFI_TransitionData
    {
        public PFI_ProductData pfiLoan;
        public DateTime TransitionDate;
    }
}
