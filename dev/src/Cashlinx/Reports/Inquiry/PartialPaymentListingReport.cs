using iTextSharp.text;
using Common.Controllers.Application;
using Common.Libraries.Objects;

namespace Reports.Inquiry
{
    public class PartialPaymentListingReport : ReportBase
    {
        public PartialPaymentListingReport(IPdfLauncher pdfLauncher)
            : base("Partial Payments Listing",
                " ", " ", pdfLauncher)
        {
            reportType = "Inquiry";

            reportPageSize = PageSize.LETTER.Rotate();

            showReportDate = false;
            heading aHeading = new heading()
            {
                align = Element.ALIGN_CENTER,
                width = 50f,
                format = "{0}",
                showTotal = false,
                field_align = -1,
                header_align = -1,
                Translator = null
            };

            aHeading.text = "Loan #";
            aHeading.fieldName = "TICKET_NUMBER";
            headers.Add(aHeading);

            aHeading.text = "Loan Date Made";
            aHeading.fieldName = "DATE_MADE";
            aHeading.format = "{0:d}";
            headers.Add(aHeading);

            aHeading.width = 110f;
            aHeading.text = "Customer Name";
            aHeading.fieldName = "cust_name";
            aHeading.format = "{0}";
            headers.Add(aHeading);

            aHeading.width = 60f;
            aHeading.text = "Date Of Birth";
            aHeading.fieldName = "birthdate";
            aHeading.format = "{0:d}";
            //aHeading.align = Element.ALIGN_RIGHT;
            headers.Add(aHeading);

            aHeading.text = "Loan Amount";
            aHeading.fieldName = "LOAN_AMOUNT";
            aHeading.format = "{0:C2}";
            headers.Add(aHeading);

            aHeading.text = "New Principal Amount";
            aHeading.fieldName = "CURRENT_PRIN_AMOUNT";
            aHeading.format = "{0:C2}";
            headers.Add(aHeading);

            aHeading.width = 80f;
            aHeading.text = "Partial Payment Date Paid";
            aHeading.fieldName = "PP_DATE";
            aHeading.format = "{0:d}";
            headers.Add(aHeading);

            aHeading.width = 60f;
            aHeading.text = "Partial Payment Amount";
            aHeading.fieldName = "PP_AMOUNT";
            aHeading.format = "{0:C2}";
            headers.Add(aHeading);

            aHeading.text = "Partial Payment Status";
            aHeading.fieldName = "status_cd";
            aHeading.format = "{0}";
            headers.Add(aHeading);
        }
    }
}
