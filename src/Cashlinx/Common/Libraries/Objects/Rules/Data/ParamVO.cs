using System;
using Common.Libraries.Objects.Rules.Type;

namespace Common.Libraries.Objects.Rules.Data
{
    [Serializable()]
    public class ParamVO
    {
        public DataTypeVO DataType { get; set; }
        public AliasVO Alias       { get; set; }
        public string Code         { get; set; }
        public object Value        { get; set; }
        public string StoreNumber  { get; set; }
        public string State        { get; set; }
        public string Company      { get; set; }
        public bool Cacheable      { get; set; }

        public ParamVO()
        {
            this.DataType = new DataTypeVO();
            this.Alias = new AliasVO();
            this.Code = string.Empty;
            this.Value = null;
            this.StoreNumber = string.Empty;
            this.State = string.Empty;
            this.Company = string.Empty;
            this.Cacheable = false;
        }
    }
}
