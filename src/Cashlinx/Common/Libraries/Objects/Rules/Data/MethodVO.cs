using System;
using System.Collections.Generic;

namespace Common.Libraries.Objects.Rules.Data
{
    [Serializable()]
    public class MethodVO
    {
        public string MethodName { get; set; }
        /// <summary>
        /// Must be the FULLY QUALIFIED type name.
        /// </summary>
        public string ReturnTypeName { get; set; }
        public List<MethodParamVO> Params { get; set; }
    }

    [Serializable()]
    public class MethodParamVO
    {        
        /// <summary>
        /// Must be the FULLY QUALIFIED type name.
        /// </summary>
        public string TypeName { get; set; }
        public string KeyName { get; set; }
        public bool IsOutParam { get; set; }
        public string ParamValue { get; set; }
    }

}
