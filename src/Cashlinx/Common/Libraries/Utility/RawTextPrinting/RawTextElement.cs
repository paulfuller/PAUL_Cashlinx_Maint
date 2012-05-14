using System.Collections.Generic;
using System.Text;

namespace Common.Libraries.Utility.RawTextPrinting
{
    public abstract class RawTextElement
    {
        protected RawTextElement()
        {
            PrinterCodes = new List<PrinterCode>();
        }

        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }

        private List<PrinterCode> PrinterCodes { get; set; }

        public bool AddPrinterCode(PrinterCode code)
        {
            PrinterCodes.Add(code);
            return true;
        }

        protected bool FlagIsSet(RawTextFlags flags, RawTextFlags flag)
        {
            return (flags & flag) != 0;
        }

        protected bool FlagIsNotSet(RawTextFlags flags, RawTextFlags flag)
        {
            return (flags & flag) == 0;
        }

        protected string GetRawPrinterCodes()
        {
            var codes = new StringBuilder();

            foreach (var printerCode in PrinterCodes)
            {
                codes.Append(printerCode);
            }

            return codes.ToString();
        }
    }
}
