using Common.Libraries.Objects;
using iTextSharp.text;

namespace Reports
{
    class JewelryCountSummaryReport : ReportBase
    {

        public JewelryCountSummaryReport(IPdfLauncher pdfLauncher)
            : base("Jewelry Count Summary", "", "Transaction Total:", pdfLauncher)
        {
            showReportDate = false;
            heading aHeading = new heading()
                               {
                                   align = Element.ALIGN_CENTER,
                                   width = 70f,
                                   format="{0}",
                                   showTotal = false,
                                   field_align = -1,
                                   header_align = -1,
                                   Translator = null
                               };
            
            aHeading.text = "Jewelry Case";
            aHeading.fieldName = "JCASE";
            headers.Add(aHeading);

            aHeading.text = "Sales";
            aHeading.fieldName = "SALES";
            aHeading.showTotal = true;
            headers.Add(aHeading);

            aHeading.text = "Layaways";
            aHeading.fieldName = "LAYAWAYS";
            headers.Add(aHeading);

            aHeading.text = "Transfer Out/OS";
            aHeading.fieldName = "TRANSFERSOUT";        
            headers.Add(aHeading);

            aHeading.text = "Returns";
            aHeading.fieldName = "RETURNS"; 
            headers.Add(aHeading);

            aHeading.text = "Police Seize";
            aHeading.fieldName = "SEIZES"; 
            headers.Add(aHeading);

            aHeading.text = "Case Total";
            aHeading.fieldName = "TOTAL";
            headers.Add(aHeading);
        }

    }
}
