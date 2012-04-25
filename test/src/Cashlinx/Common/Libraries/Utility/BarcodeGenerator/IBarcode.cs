namespace Common.Libraries.Utility.BarcodeGenerator
{
    interface IBarcode
    {
        string Encoded_Value
        {
            get;
        }//Encoded_Value

        string RawData
        {
            get;
        }//Raw_Data

        string FormattedData
        {
            get;
        }//FormattedData
    }//interface
}//namespace
