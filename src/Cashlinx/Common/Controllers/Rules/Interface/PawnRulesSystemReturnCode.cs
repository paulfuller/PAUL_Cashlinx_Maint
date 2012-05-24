namespace Common.Controllers.Rules.Interface
{
    public class PawnRulesSystemReturnCode
    {
        public enum Code
        {
            SUCCESS,
            WARNING,
            ERROR,
            FATAL            
        };

        public Code ReturnCode { get; private set; }
        public string ReturnDesc { get; private set; }
        public bool HasDesc { get; private set; }

        public PawnRulesSystemReturnCode(Code c)
        {
            this.ReturnCode = c;
            this.ReturnDesc = null;
            this.HasDesc = false;
        }

        public PawnRulesSystemReturnCode(Code c, string d)
        {
            this.ReturnCode = c;
            this.ReturnDesc = d;
            this.HasDesc = true;
        }
    }
}
