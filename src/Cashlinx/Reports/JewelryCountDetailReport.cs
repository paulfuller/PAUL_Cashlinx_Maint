using Common.Libraries.Objects;
using iTextSharp.text;

namespace Reports
{
    class JewelryCountDetailReport : ReportBase
    {

        public JewelryCountDetailReport(IPdfLauncher pdfLauncher)
            : base("Jewelry Count Worksheet", "", "", pdfLauncher)
        {
            showReportDate = false;
            GroupByFooterTitle = "Total Count for Case {0}:";

            heading aHeading = new heading()
                               {
                                   align = Element.ALIGN_CENTER,
                                   width = 45f,
                                   format="{0}",
                                   showTotal = false,
                                   field_align = -1,
                                   header_align = -1,
                                   Translator = null
                               };          
            
            aHeading.text = "Jewelry Case";
            aHeading.fieldName = "JCASE";
            aHeading.groupBy = true;
            headers.Add(aHeading);

            aHeading.text = "Transaction\nTime";
            aHeading.fieldName = "TRANS_TIME";
            aHeading.groupBy = false;
            headers.Add(aHeading);

            aHeading.text = "Employee #";
            aHeading.fieldName = "CREATEDBY";
            headers.Add(aHeading);

            aHeading.text = "Transaction\nStatus";
            aHeading.fieldName = "STATUS_CD";        
            headers.Add(aHeading);

            aHeading.text = "Transaction\nNumber";
            aHeading.fieldName = "DISP_DOC"; 
            headers.Add(aHeading);

            aHeading.text = "Short\nCode";
            aHeading.fieldName = "SHORT_CODE"; 
            headers.Add(aHeading);

            aHeading.text = "Merchandise Description";
            aHeading.fieldName = "MD_DESC";
            aHeading.field_align = Element.ALIGN_LEFT;
            aHeading.header_align = Element.ALIGN_CENTER;
            aHeading.width = 200f;
            headers.Add(aHeading);
        }

    }
}
