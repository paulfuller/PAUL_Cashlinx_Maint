using Common.Libraries.Objects;
using iTextSharp.text;

namespace Reports.Inquiry
{
    public class CashTransferListingReport : ReportBase
    {
        public CashTransferListingReport(IPdfLauncher pdfLauncher)
            : base("Cash Transfer Listing",
                " ", " ", pdfLauncher)
        {
            reportPageSize = PageSize.LETTER.Rotate();
            
            showReportDate = false;
            heading aHeading = new heading()
                               {
                                       align = Element.ALIGN_LEFT,
                                       width = 50f,
                                       format = "{0}",
                                       showTotal = false,
                                       field_align = -1,
                                       header_align = -1,
                                       Translator = null
                                   };

            aHeading.text = "Transfer #";
            aHeading.fieldName = "TRANSFERNUMBER";
            headers.Add(aHeading);

            aHeading.width = 100f;
            aHeading.text = "Transfer Type";
            aHeading.fieldName = "TRANSFERTYPE";
            headers.Add(aHeading);

            aHeading.text = "Transfer Date / Time";
            aHeading.fieldName = "TRANSFERDATE";
            headers.Add(aHeading);

            aHeading.width = 60f;
            aHeading.text = "Transfer Amount";
            aHeading.fieldName = "TRANSFERAMOUNT";
            aHeading.format ="{0:C}";
            aHeading.align = Element.ALIGN_RIGHT;
            headers.Add(aHeading);

            aHeading.align = Element.ALIGN_CENTER;
            aHeading.format = "{0}";
            aHeading.width = 100f;
            aHeading.text = "Source";
            aHeading.fieldName = "SOURCE";
            headers.Add(aHeading);

            aHeading.text = "Destination";
            aHeading.fieldName = "DESTINATION";
            headers.Add(aHeading);

            aHeading.width = 75f;
            aHeading.text = "Status";
            aHeading.fieldName = "TRANSFERSTATUS";
            headers.Add(aHeading);

            aHeading.align = Element.ALIGN_LEFT;
            aHeading.text = "User Id";
            aHeading.fieldName = "USERID";
            headers.Add(aHeading);
        }

    }
}
