

namespace Pawn.Flows.AppController.Impl.InquiryFlows
{
    class LoanInquiryFlowExecutor //: SingleExecuteBlock
    {
        // map => Reports --(view)--> LoanCriteria --(find)--> LoanList --(view)--> 
        //      LoanDetail --(view)--> ItemDetail 
        //      ItemDetail --(revisions)--> RevisionType
        //      RevisionType --(OK)--> Cost, RetailPrice, or MerchandiseDescription Revision s
        //
        //      * --(refine)--> **back to LoanCriteria, remove intermediate from list
        //      * --(back)--> * - 1 from History
    }
}
