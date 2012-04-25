using Common.Libraries.Objects;
using iTextSharp.text;

namespace Reports
{
    class CACCSalesReport : ReportBase
    {

        public CACCSalesReport(IPdfLauncher pdfLauncher)
            : base("CACC Sales Analysis", 
                "NOTE:  The figures for SALES only includes actual sales.  It does not include reductions of inventory for Transfers-Out, Police Seizures, etc..", "", pdfLauncher)
        {
            showReportDate = false;
            heading aHeading = new heading()
                               {
                                       align = Element.ALIGN_LEFT,
                                       width = 114f,
                                       format = "{0}",
                                       showTotal = false,
                                       field_align = -1,
                                       header_align = -1,
                                       Translator = null
                                   };
            
            aHeading.text = "CACC Type";
            aHeading.fieldName = "CAT_DESC";
            headers.Add(aHeading);

            aHeading.text = "Beginning\nInventory";
            aHeading.fieldName = "START_INVENTORY";
            aHeading.width = 38f;
            aHeading.align = Element.ALIGN_CENTER;
            headers.Add(aHeading);

            aHeading.text = "Quantity\nSold";
            aHeading.fieldName = "NR_SOLD";
            aHeading.width = 32f;
            headers.Add(aHeading);

            aHeading.text = "Average\nCost";
            aHeading.fieldName = "AVG_COST";        
            aHeading.align = Element.ALIGN_RIGHT;
            aHeading.format = "{0:0.00}";
            headers.Add(aHeading);

            aHeading.text = "Average\nRetail";
            aHeading.fieldName = "AVG_RETAIL"; 
            headers.Add(aHeading);

            aHeading.text = "Gross\nProfit %";
            aHeading.fieldName = "GROSS_PROFIT"; 
            aHeading.align = Element.ALIGN_CENTER;
            aHeading.format = "{0:0.0}";
            headers.Add(aHeading);

            aHeading.text = "Quantity\nAdded";
            aHeading.fieldName = "QTY_ADDED";
            aHeading.format = "{0}";
            aHeading.width = 34f;
            headers.Add(aHeading);

            aHeading.text = "Charge\nOff";
            aHeading.fieldName = "CHARGE_OFF";
            aHeading.width = 32f;
            headers.Add(aHeading);

            aHeading.text = "Charge\nOn";
            aHeading.fieldName = "CHARGE_ON";
            aHeading.width = 32f;
            headers.Add(aHeading);

            aHeading.text = "Ending\nInventory";
            aHeading.fieldName = "END_INVENTORY";
            aHeading.width = 36f;
            headers.Add(aHeading);

            aHeading.text = "End Inv\nAvg Cost";
            aHeading.fieldName = "END_INV_COST";
            aHeading.align = Element.ALIGN_RIGHT;
            aHeading.format = "{0:0.00}";
            headers.Add(aHeading);

            aHeading.text = "Turns";
            aHeading.fieldName = "TURNS";
            aHeading.width = 38f;
            aHeading.align = Element.ALIGN_RIGHT;
            aHeading.format = "{0}";
            headers.Add(aHeading);
        }

    }
}
