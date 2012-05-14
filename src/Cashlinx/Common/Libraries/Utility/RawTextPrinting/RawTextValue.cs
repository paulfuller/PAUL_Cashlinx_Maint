
namespace Common.Libraries.Utility.RawTextPrinting
{
    public class RawTextValue
    {
        public RawTextValue()
        {
            Value = StartCodes = EndCodes = string.Empty;
        }

        public string EndCodes { get; set; }
        public int Length
        {
            get { return Value.Length; }
        }
        public string StartCodes { get; set; }
        public string Value { get; set; }

        public string GetFullValue()
        {
            return string.Format("{0}{1}{2}", StartCodes, Value, EndCodes);
        }

        public override string ToString()
        {
            return GetFullValue();
        }
    }
}
