using System;

namespace Common.Libraries.Objects.Inquiry
{
    class LoanCriteria
    {
        bool isInitialized = false;

        int errorLevel;
        string errorMessage;

        enum searchDateType { MADE, DUE, PFI_ELIG, STATUS, PFI_NOTICE, PFI_MAILER };
        enum searchTicketType { CURRENT, PREVIOUS, ORIGINAL };
        enum searchStatus {HOLD, PFI_WORKING, ACTIVE, INACTIVE };

        bool byDate = false;
        searchDateType dateType;
        searchTicketType ticketType;
        DateTime startDate, endDate;
        int lowTicketNumber, highTicketNumber;
        double lowAmount, highAmount;
        string status;
        bool pfiMailer;
        string userID;
        enum sortField {AMOUNT, TICKET, STATUS, USER };
        sortField sortBy;
        string sortDir; // asc / desc

    }
}
