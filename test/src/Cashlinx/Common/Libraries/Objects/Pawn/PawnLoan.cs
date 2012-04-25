using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Pawn
{
    [Serializable]
    public class PawnLoan:CustomerProductDataVO
    {
        private static readonly int BRACKET_OFFSET = 3;
        public decimal CurrentPrincipalAmount { get; set; }

        public DateTime DueDate                     {get; set;}
        public DateTime DateMade
        {
            get;
            set;
        }
        public decimal DailyAmount
        {
            get;
            set;
        }
        public DateTime NewDueDate
        {
            get;
            set;
        }
        public decimal ExtensionAmount
        {
            get;
            set;
        }
        public int ExtensionID
        {
            get;
            set;
        }
        public string ExtensionNotAllowedReason
        {
            get;
            set;
        }
        public ExtensionTerms ExtensionType
        {
            get;
            set;
        }

        public decimal InterestAmount               {get; set;}
        public decimal InterestRate                 {get; set;}
        public bool IsExtensionAllowed              {get; set;}
        public bool IsExtended                      {get; set;}
        public bool PartialPaymentPaid { get; set; }
        public DateTime LastDayOfGrace              {get; set;}
        public bool LoanUpAllowed                   {get; set;}
        public decimal PickupLateRef
        {
            get;
            set;
        }
        public decimal OtherTranLateRef
        {
            get;
            set;
        }

        public decimal LateRefPick
        {
            get;
            set;
        }
        public CustLoanLostTicketFee LostTicketInfo {get; set;}
        public DateTime NewMadeDate
        {
            get;
            set;
        }
        public decimal MPR                          {get; set;}
        public char NegotiableFinanceCharge         {get; set;}
        public char NegotiableServiceCharge         {get; set;}
        public object  ObjectUnderwritePawnLoanVO   {get; set;}
        public string OrgShopAlias
        {
            get;
            set;
        }
        
        public int OrigTicketNumber                 {get; set;}
        public string PawnAppId                     {get; set;}

        public bool PaydownAllowed                  {get; set;}
        public string PaydownAllowedReason          {get; set; }
        public decimal PaydownAmount
        {
            get;
            set;
        }

        public DateTime NewPfiEligible
        {
            get;
            set;
        }

        public DateTime PfiNote                     {get; set;}
        public DateTime NewPfiNote
        {
            get;
            set;
        }

        public bool PickupAllowed                   {get; set;}
        public string PickupNotAllowedReason
        {
            get;
            set;
        }
        public decimal PickupAmount                 {get; set;}
        public int PrevTicketNumber                 {get; set;}
        public char PrintNotice                     {get; set;}
        public string PuCustNumber { get; set; }

        public bool RenewalAllowed                  {get; set;}
        public string RenewalNotAllowedReason       {get; set;}
        public decimal RenewalAmount                {get; set;}
        public List<LoanRevision> Revisions         {get; set;}
        public decimal  ServiceCharge               {get; set;}
        public string ServiceMessage
        {
            get;
            set;
        }
        public bool ServiceAllowed
        {
            get;
            set;
        }

        public bool StorageFeeAllowed               {get; set;}

        public int TicketLength                     {get; set;}

        public decimal TotalExtensionAmount { get; set; }

        public int DocType { get; set; }

        public string EntityId
        {
            get;
            set;
        }
        public List<PartialPayment> PartialPayments { get; set; }
        public int CyclesLate;
        /*public int DaysIntoCycle;
        public DateTime LastCycleEnd;
        public DateTime NextCycleEnd;
        public int PPDaysAref;
        public int PPDaysCredit;*/
        public decimal PickupLateFinAmount;
        public decimal PickupLateServAmount;
        public decimal OtherTranLateFinAmount;
        public decimal OtherTranLateServAmount;

        public bool PfiMailerSent { get; set; }

        public DateTime LastExtensionPaid;



        public DateTime LastPartialPaymentDate;


        /// <summary>
        /// 
        /// </summary>
        public PawnLoan()
        {
            MPR                 = 0.0M;
            InterestAmount      = 0.0M;
            InterestRate        = 0.0M;
            Amount              = 0.0M;
            ExtensionAmount = 0.0M;
            TicketLength        = 0;
            PrintNotice         = 'N';
            Items = new List<Item>();
            ServiceMessage = "";
            PickupNotAllowedReason = "";
            ExtensionNotAllowedReason = "";
            
            LateRefPick = 0.0M;
            OriginalFees = new List<Fee>();
            PartialPayments = new List<PartialPayment>();
 
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateTicketLength()
        {
            if (this.Items.Count > 0)
            {
                int iTagNumber = 0;
                foreach (Item item in this.Items)
                {
                    iTagNumber++;
                    if(item.TicketDescription != null)
                        this.TicketLength += item.TicketDescription.Length + BRACKET_OFFSET + iTagNumber.ToString().Length;
                }
            }
        }
    }
}
