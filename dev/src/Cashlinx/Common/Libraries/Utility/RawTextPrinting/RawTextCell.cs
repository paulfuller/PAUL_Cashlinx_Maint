
namespace Common.Libraries.Utility.RawTextPrinting
{
    public class RawTextCell : RawTextElement
    {
        public const char Space = ' ';

        public RawTextCell(int width, RawTextFlags flags)
        {
            Width = width;
            Flags = flags;
        }

        public RawTextFlags Flags { get; set; }
        public string Value { get; set; }
        public int Width { get; set; }

        public RawTextValue GetValue()
        {
            var rawTextValue = new RawTextValue();
            rawTextValue.Value = GetRawTextValue();

            rawTextValue.StartCodes = GetRawPrinterCodes();
            rawTextValue.EndCodes = string.Empty;

            if (Bold)
            {
                rawTextValue.StartCodes += PrinterCode.BoldOn;
                rawTextValue.EndCodes += PrinterCode.BoldOff;
            }
            if (Italic)
            {
                rawTextValue.StartCodes += PrinterCode.ItalicOn;
                rawTextValue.EndCodes += PrinterCode.ItalicOff;
            }
            if (Underline)
            {
                rawTextValue.StartCodes += PrinterCode.UnderlineOn;
                rawTextValue.EndCodes += PrinterCode.UnderlineOff;
            }

            return rawTextValue;
        }

        private string GetRawTextValue()
        {
            var value = Value;
            var width = Width;

            if (string.IsNullOrWhiteSpace(value))
            {
                value = string.Empty;
            }
            if (value.Length > width)
            {
                value = value.Substring(0, width);
            }

            if (FlagIsSet(RawTextFlags.ForceUpper))
            {
                value = value.ToUpper();
            }
            else if (FlagIsSet(RawTextFlags.ForceLower))
            {
                value = value.ToLower();
            }

            if (FlagIsNotSet(RawTextFlags.Center))
            {
                if (FlagIsNotSet(RawTextFlags.Right))
                {
                    width *= -1;
                }

                var format = "{0," + width + "}";
                return string.Format(format, value);
            }
            else
            {
                if (value.Length == width)
                {
                    return value;
                }

                if (value.Length == width - 1)
                {
                    return value + Space;
                }

                var difference = width - value.Length;
                var leftWidth = difference / 2;
                var rightWidth = difference - leftWidth;

                var format = "{0," + leftWidth + "}{1}{0," + rightWidth + "}";

                return string.Format(format, Space, value);
            }
        }

        private bool FlagIsSet(RawTextFlags flag)
        {
            return FlagIsSet(Flags, flag);
        }

        private bool FlagIsNotSet(RawTextFlags flag)
        {
            return FlagIsNotSet(Flags, flag);
        }
    }
}
