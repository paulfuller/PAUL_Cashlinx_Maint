using System;

namespace Common.Libraries.Objects.Rules.Type
{
    [Serializable()]
    public class DataTypeVO
    {
        public string Id   { get; set; }
        public string Code { get; set; }
        public TypeCode DType { get; set; }

        public DataTypeVO()
        {
            this.Id = string.Empty;
            this.Code = string.Empty;
            this.DType = TypeCode.Empty;
        }
    }
}
