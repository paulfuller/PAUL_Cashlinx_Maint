using System;
using System.Text.RegularExpressions;
using System.Text;

namespace Common.Libraries.Objects.Business
{
    public enum DocumentType
    {
        Acquisition = 0,
        Loan = 1,
        MerchandisePurchaseReceipt = 2,
        ResultOfNxtSale = 3,
        CaccDisposition = 4,
        TransferIn = 6,
        CaccItem = 7,
        TemporaryIcn = 8,
        NxtAndStandardDescriptor = 9
    }

    public class Icn
    {
        public const int ICN_LENGTH = 18;
        public const int MAX_SHORT_CODE_LENGTH = 10;
        public const char SHORT_CODE_SEPARATOR = '.';

        private int lastDigitOfYear;

        # region Constructors

        public Icn()
        {
            Initialized = false;
        }

        public Icn(string icn)
        {
            Initialized = false;
            ParseIcn(icn);
        }

        public Icn(int docNumber, int docType, int itemNumber, int lastDigitOfYear, int shopNumber, int subItemNumber)
        {
            DocumentNumber = docNumber;
            DocumentType = (DocumentType)docType;
            ItemNumber = itemNumber;
            LastDigitOfYear = lastDigitOfYear;
            ShopNumber = shopNumber;
            SubItemNumber = subItemNumber;
            Initialized = true;
        }

        # endregion

        # region Properties

        public int DocumentNumber { get; set; }
        public DocumentType DocumentType { get; set; }
        public string FormattedDocumentNumber
        {
            get { return DocumentNumber.ToString().PadLeft(6, '0'); }
        }
        public string FormattedDocumentType
        {
            get { return ((int)DocumentType).ToString()[0].ToString(); }
        }
        public string FormattedItemNumber
        {
            get { return ItemNumber.ToString().PadLeft(3, '0'); }
        }
        public string FormattedLastDigitOfYear
        {
            get { return LastDigitOfYear.ToString(); }
        }
        public string FormattedShopNumber
        {
            get { return ShopNumber.ToString().PadLeft(5, '0'); }
        }
        public string FormattedSubItemNumber
        {
            get { return SubItemNumber.ToString().PadLeft(2, '0'); }
        }
        public int ItemNumber { get; set; }
        public bool IsShortCodeOnly { get; private set; }
        public int LastDigitOfYear
        {
            get { return lastDigitOfYear; }
            set
            {
                string year = value.ToString();
                lastDigitOfYear = Convert.ToInt32(year.Substring(year.Length-1));
            }
        }
        public int ShopNumber { get; set; }
        public int SubItemNumber { get; set; }

        public bool Initialized { get; set; }

        # endregion

        # region Public Methods

        public string GetFullIcn()
        {
            return GetFullIcn(string.Empty);
        }

        public string GetFullIcn(string partSeparator)
        {
            if (!Initialized)
            {
                return string.Empty;
            }
            return string.Format("{0}{6}{1}{6}{2}{6}{3}{6}{4}{6}{5}",
                FormattedShopNumber,
                FormattedLastDigitOfYear,
                FormattedDocumentNumber,
                FormattedDocumentType,
                FormattedItemNumber,
                FormattedSubItemNumber,
                partSeparator);
        }

        public string GetVisualFullIcn()
        {
            return GetFullIcn(" ");
        }

        public string GetShortCode()
        {
            return GetShortCode(".");
        }

        public string GetShortCode(string separator)
        {
            if (!Initialized)
            {
                return string.Empty;
            }
            return string.Format("{0}{1}{2}", DocumentNumber.ToString(), separator, ItemNumber.ToString());
        }

        public string GetFullShortCode()
        {
            return GetFullShortCode(".");
        }

        public string GetFullShortCode(string separator)
        {
            if (!Initialized)
            {
                return string.Empty;
            }
            return string.Format("{0}{1}{2}", FormattedDocumentNumber, separator, FormattedItemNumber);
        }

        public void ParseIcn(string icn)
        {
            if (string.IsNullOrEmpty(icn))
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (char c in icn)
            {
                if (!Char.IsDigit(c) && !c.Equals(SHORT_CODE_SEPARATOR))
                {
                    continue;
                }

                sb.Append(c);
            }

            icn = sb.ToString();

            if (string.IsNullOrEmpty(icn))
            {
                return;
            }

            if (Regex.IsMatch(icn, "^[\\d]{" + ICN_LENGTH + "}$"))
            {
                ShopNumber = Convert.ToInt32(icn.Substring(0, 5));
                LastDigitOfYear = Convert.ToInt32(icn.Substring(5, 1));
                DocumentNumber = Convert.ToInt32(icn.Substring(6, 6));
                DocumentType = (DocumentType)Convert.ToInt32(icn.Substring(12, 1));
                ItemNumber = Convert.ToInt32(icn.Substring(13, 3));
                SubItemNumber = Convert.ToInt32(icn.Substring(16, 2));
                IsShortCodeOnly = false;
                Initialized = true;
            }
            else if (icn.Length <= MAX_SHORT_CODE_LENGTH && Regex.IsMatch(icn, "^[\\d]+\\.[\\d]+$"))
            {
                Match match = Regex.Match(icn, "^(?<doc>\\d{1,6})\\.{1,1}(?<item>\\d{1,3})$");
                if (match.Success)
                {
                    DocumentNumber = Convert.ToInt32(match.Groups["doc"].Value);
                    ItemNumber = Convert.ToInt32(match.Groups["item"].Value);
                    IsShortCodeOnly = true;
                    Initialized = true;
                }
            }
        }

        public override string ToString()
        {
            if (IsShortCodeOnly)
            {
                return GetShortCode();
            }
            else
            {
                return GetFullIcn();
            }
        }

        # endregion
    }
}
