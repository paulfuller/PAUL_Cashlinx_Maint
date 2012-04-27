using System;
using System.Collections.Generic;
using Common.Libraries.Utility.String;

namespace Common.Libraries.Objects.Rules.System
{
    public class StoredProcVO
    {
        public ProcPackageVO ProcPackage { get; set; }
        public string Name { get; set; }
        private List<StoredProcParamVO> parameters;

        public int Count
        {
            get
            {
                return (this.parameters.Count);
            }
        }

        public StoredProcParamVO this[int i]
        {
            get
            {
                if(i < 0 || this.parameters == null || i >= this.parameters.Count) return (null);
                return (this.parameters[i]);
            }
        }

        public StoredProcParamVO this[string s]
        {
            get
            {
                if (parameters == null || parameters.Count <= 0 || StringUtilities.isEmpty(s)) return (null);
                foreach (StoredProcParamVO svo in parameters)
                {
                    if (svo != null && svo.ParamName.Equals(s, StringComparison.OrdinalIgnoreCase)) return (svo);
                }
                return (null);
            }
        }

        public void addStoredProcParam(StoredProcParamVO vo)
        {
            if (vo == null || this.parameters == null || this.parameters.Contains(vo))return;
            this.parameters.Add(vo);
        }

        public StoredProcVO()
        {
            this.ProcPackage = null;
            this.Name = "";
            this.parameters = new List<StoredProcParamVO>();
        }
    }
}
