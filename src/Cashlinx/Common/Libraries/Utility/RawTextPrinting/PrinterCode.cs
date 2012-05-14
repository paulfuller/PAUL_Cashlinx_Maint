
namespace Common.Libraries.Utility.RawTextPrinting
{
    public class PrinterCode
    {
        public const char Escape = '\x1B';
        public const char Bracket = '\x5B';
        public const char FormFeed = '\xC';

        public PrinterCode(string mtplSequence)
        {
            MtplSequence = mtplSequence;
            NonMtplSequence = mtplSequence.Replace("<CSI>", string.Concat(Escape, Bracket));
        }

        public override string ToString()
        {
            return NonMtplSequence;
        }

        public string MtplSequence { get; private set; }
        public string NonMtplSequence { get; private set; }

        public static readonly PrinterCode BoldOff = new PrinterCode("<CSI>22m");
        public static readonly PrinterCode BoldOn = new PrinterCode("<CSI>1m");
        public static readonly PrinterCode HorizontalSpacing171Cpi = new PrinterCode("<CSI>7w");
        public static readonly PrinterCode ItalicOff = new PrinterCode("<CSI>23m");
        public static readonly PrinterCode ItalicOn = new PrinterCode("<CSI>3m");
        public static readonly PrinterCode LineDensity8Dpi = new PrinterCode("<CSI>4z");
        public static readonly PrinterCode UnderlineOff = new PrinterCode("<CSI>24m");
        public static readonly PrinterCode UnderlineOn = new PrinterCode("<CSI>4m");
    }
}
