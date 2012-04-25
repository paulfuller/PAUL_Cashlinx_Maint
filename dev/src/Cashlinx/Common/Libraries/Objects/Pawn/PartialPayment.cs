using System;


namespace Common.Libraries.Objects.Pawn
{
    public class PartialPayment
    {
        public string CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public int PMT_ID {get;set;}
        
  
        public decimal PMT_AMOUNT {get;set;}
        public decimal PMT_PRIN_AMT {get;set;}
        public decimal PMT_INT_AMT  {get;set;}
        public decimal PMT_SERV_AMT {get;set;}
        public decimal CUR_AMOUNT  {get;set;}
        public decimal CUR_FIN_CHG {get;set;}
        public decimal Cur_Srv_Chg {get;set;}
        public int Cur_Term_Fin {get;set;}
        public decimal Cur_Int_Pct {get;set;}
        public string Status_cde  {get;set;}
        
        public string Pmt_Type  {get;set;}
        public string Pmt_Doc_Type {get;set;}
        public int ReceiptDetail_Number  {get;set;}
        public DateTime Date_Made  {get;set;}
        public DateTime Time_Made  {get;set;}
        public DateTime Status_Date {get;set;}
        public DateTime Status_Time {get;set;}
        public string Entity_Number {get;set;}
        public string Entity_Type {get;set;}
        public int LoanNumber { get; set; }
        public string UpdatedBy {get;set;}
        public DateTime UpdateDate { get; set; }
        public string StoreNumber { get; set; }
    }
}
