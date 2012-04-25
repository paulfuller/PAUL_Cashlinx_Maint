using iTextSharp.text;
using Reports;

namespace Audit.Reports 
{
    class MissingItemsReport : ReportBase
    {
        protected string returnDefault (object DBValue)
        {
            return "Missing";
        }

        protected string ICNSpacing(object DBValue)
        {
            string ICN = DBValue.ToString();

            if (ICN.Length != 18)
                return DBValue.ToString();

            return string.Format("{0} {1} {2} {3} {4} {5}", ICN.Substring(0, 5), ICN.Substring(5, 1), ICN.Substring(6, 6),
                ICN.Substring(12, 1), ICN.Substring(13, 3), ICN.Substring(16, 2));
        }

        public MissingItemsReport(string title)
            : base(title, "", "", Common.Libraries.Utility.Shared.PdfLauncher.Instance)
        {
            base.reportType = "Audit";
            base.showReportDate = false;
            base.showGroupCount = true;
            this.GroupByFooterTitle = "Total Missing Items:";

            heading aHeading = new heading()
            {
                    align = Element.ALIGN_LEFT, 
                    width = 55f, 
                    format = "{0}", 
                    showTotal = false, 
                    field_align = -1, 
                    header_align = -1,
                    formatICN = false,
                    Translator = null
            }; 

            aHeading.text = "Audit Indicator";
            aHeading.fieldName = "TRAK_NEW_FLAG";
            aHeading.Translator = returnDefault;
            aHeading.groupBy = true;
            headers.Add(aHeading);

            aHeading.text = "ICN";
            aHeading.fieldName = "ICN";
            aHeading.align = Element.ALIGN_CENTER;
            aHeading.width = 85f;
            aHeading.groupBy = false;
            aHeading.Translator = ICNSpacing;
            //aHeading.formatICN = true; // this looks nice w/ bolded numbers and spaces, but it boggs down the report tremendously
            headers.Add(aHeading);

            aHeading.text = "Status";
            aHeading.fieldName = "STATUS_CD";
            aHeading.width = 40f;
            aHeading.formatICN = false;
            aHeading.Translator = null;
            headers.Add(aHeading);

            aHeading.text = "Cost";
            aHeading.fieldName = "PFI_AMOUNT";
            aHeading.format = "{0:n}";
            aHeading.field_align = Element.ALIGN_RIGHT;
            aHeading.width = 40f;
            aHeading.showTotal = true;
            headers.Add(aHeading);

            aHeading.text = "Merchandise Description";
            aHeading.fieldName = "MD_DESC";
            aHeading.format = "{0}";
            aHeading.header_align = Element.ALIGN_CENTER;
            aHeading.field_align = Element.ALIGN_LEFT;
            aHeading.showTotal = false;
            aHeading.width = 200f;
            headers.Add(aHeading);
        }


    }
}
