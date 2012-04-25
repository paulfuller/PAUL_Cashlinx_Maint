using System;
using System.Collections.Generic;
using Common.Libraries.Utility.String;

namespace Common.Libraries.Objects.Rules.System
{
    public class ProcPackageVO
    {
        public string PackageName { get; set; }
        private List<StoredProcVO> procedures;

        public int Count
        {
            get
            {
                return (this.procedures.Count);
            }
        }

        public StoredProcVO this[int i]
        {
            get
            {
                if (i < 0 || this.procedures == null || i >= this.procedures.Count) return (null);
                return (this.procedures[i]);
            }
        }

        public StoredProcVO this[string s]
        {
            get
            {
                if (this.procedures == null || this.procedures.Count <= 0 || StringUtilities.isEmpty(s)) return (null);
                foreach (StoredProcVO svo in this.procedures)
                {
                    if (svo == null) continue;
                    if (svo.Name.Equals(s, StringComparison.OrdinalIgnoreCase)) return (svo);
                }
                return (null);
            }
        }

        public void addStoredProc(StoredProcVO vo)
        {
            if(vo == null || this.procedures == null || this.procedures.Contains(vo)) return;
            this.procedures.Add(vo);
        }

        public ProcPackageVO()
        {
            this.PackageName = "";
            this.procedures = new List<StoredProcVO>();
        }
    }
}
