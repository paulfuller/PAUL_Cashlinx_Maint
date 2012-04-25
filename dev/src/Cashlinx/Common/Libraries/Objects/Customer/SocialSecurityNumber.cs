/*
 * 
 * References:
 * http://ssa-custhelp.ssa.gov/app/answers/detail/a_id/425
 * http://en.wikipedia.org/wiki/Social_Security_number#cite_note-FAQInvalid-26
 * http://www.breakingpar.com/bkp/home.nsf/0/87256B280015193F87256F6A0072B54C
 * 
 */

using System;

namespace Common.Libraries.Objects.Customer
{
    public class SocialSecurityNumber
    {
        public const char HYPHEN = '-';

        public SocialSecurityNumber(string socialSecurityNumber)
        {
            OriginalValue = socialSecurityNumber;
            ParseOriginalValue();
        }

        # region Properties

        public string AreaNumber { get; private set; }

        public string FormattedValue
        {
            get { return BuildFormattedValue(); }
        }

        public string GroupNumber { get; private set; }

        public bool IsValid
        {
            get { return IsValidSocialSecurityNumber(); }
        }

        public string OriginalValue { get; private set; }

        public string RawDigits { get; private set; }

        public string SerialNumber { get; private set; }

        # endregion

        # region Helper Methods

        private string BuildFormattedValue()
        {
            if (!IsValid)
            {
                return string.Empty;
            }

            return AreaNumber + HYPHEN + GroupNumber + HYPHEN + SerialNumber;
        }

        private bool IsValidSocialSecurityNumber()
        {
            if (string.IsNullOrWhiteSpace(AreaNumber)
                || string.IsNullOrWhiteSpace(GroupNumber)
                || string.IsNullOrWhiteSpace(SerialNumber))
            {
                return false;
            }

            int areaNumber = Convert.ToInt32(AreaNumber);
            int groupNumber = Convert.ToInt32(GroupNumber);
            int serialNumber = Convert.ToInt32(SerialNumber);

            if (areaNumber >= 734 && areaNumber <= 749)
            {
                return false;
            }

            if (areaNumber > 772)
            {
                return false;
            }

            if (areaNumber == 666)
            {
                return false;
            }

            if (areaNumber == 0 || groupNumber == 0 || serialNumber == 0)
            {
                return false;
            }

            return true;
        }

        private void ParseOriginalValue()
        {
            AreaNumber = GroupNumber = SerialNumber = string.Empty;

            if (string.IsNullOrWhiteSpace(OriginalValue))
            {
                return;
            }

            RawDigits = string.Empty;
            foreach (char c in OriginalValue)
            {
                if (char.IsWhiteSpace(c) || c.Equals(HYPHEN))
                {
                    continue;
                }

                if (!char.IsDigit(c))
                {
                    return;
                }

                RawDigits += c;
            }

            if (RawDigits.Length != 9)
            {
                return;
            }

            AreaNumber = RawDigits.Substring(0, 3);
            GroupNumber = RawDigits.Substring(3, 2);
            SerialNumber = RawDigits.Substring(5, 4);
        }

        # endregion
    }
}
