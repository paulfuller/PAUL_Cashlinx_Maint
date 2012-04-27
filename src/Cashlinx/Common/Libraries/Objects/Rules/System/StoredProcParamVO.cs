using System.Data;
using Common.Libraries.Objects.Rules.Type;

namespace Common.Libraries.Objects.Rules.System
{
    public class StoredProcParamVO
    {        
        public DataTypeVO ParamType        { get; set; }
        public string ParamName            { get; set; }
        public int ParamOrder              { get; set; }
        public int ParamSize               { get; set; }
        public ParameterDirection ParamDir { get; set; }

        public StoredProcParamVO()
        {
            this.ParamType = new DataTypeVO();
            this.ParamName = "";
            this.ParamOrder = -1;
            this.ParamSize = 0;
            this.ParamDir = ParameterDirection.Input;
        }
    }
}
