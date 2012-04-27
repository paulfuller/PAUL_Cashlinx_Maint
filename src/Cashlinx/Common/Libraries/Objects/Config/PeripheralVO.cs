using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Libraries.Utility.Collection;

namespace Common.Libraries.Objects.Config
{
    public class PeripheralVO
    {
        public enum PeripheralType : int
        {
            UNKNOWN = 0,
            LASER = 1,
            RECEIPT = 2,
            INTERMEC = 3,
            MOPRINTER = 4,
            DOCUMENT = 5,
            PAYMENT = 6
        }

        [Browsable(false)]
        public string Id
        { 
            set; get;
        }
        [Browsable(true)]
        public string Name
        {
            set;
            get;
        }
        [Browsable(true)]
        public string IPAddress
        {
            set;
            get;
        }
        [Browsable(true)]
        public string Port
        {
            set;
            get;
        }
        [Browsable(false)]
        public string WindowsPrintName
        {
            set;
            get;
        }
        [Browsable(false)]
        public int PrefOrder
        {
            set; get;
        }
        [Browsable(false)]
        public string StoreId
        {
            set; get;
        }
        [Browsable(false)]
        public bool IsWindowsPrinter
        {
            set;
            get;
        }
        [Browsable(false)]
        public PeripheralTypeVO PeriphType
        {
            set;
            get;
        }
        [Browsable(true)]
        public PeripheralType LogicalType
        {
            set; get;
        }
        [Browsable(false)]
        [ReadOnly(true)]
        public string PeripheralTypeName
        {
            get
            {
                if (this.PeriphType == null)
                    return (string.Empty);
                return (this.PeriphType.PeripheralTypeName);
            }
        }

        private string peripheralTypeId;
        private List<string> peripheralTypeNames;

        public PeripheralVO()
        {
            this.Id = string.Empty;
            this.peripheralTypeNames = new List<string>();
            this.Clear();
        }

        private void Clear()
        {
            Name = string.Empty;
            IPAddress = string.Empty;
            Port = string.Empty;
            WindowsPrintName = string.Empty;
            IsWindowsPrinter = false;
            LogicalType = PeripheralType.UNKNOWN;
            PeriphType = new PeripheralTypeVO();
            PeriphType.PeripheralTypeName = string.Empty;
            PeriphType.PeripheralModel = new PeripheralModelVO();
            StoreId = string.Empty;
            PrefOrder = 1;
            peripheralTypeId = string.Empty;
        }

        public void SetPeripheralTypeNames(List<string> pNames)
        {
            if (CollectionUtilities.isNotEmpty(pNames) && 
                this.peripheralTypeNames.Count == 0)
            {
                this.peripheralTypeNames.AddRange(pNames);
            }
        }

        public void SetPeripheralTypeNames(List<PeripheralTypeVO> pNames)
        {
            if (CollectionUtilities.isNotEmpty(pNames) &&
                this.peripheralTypeNames.Count == 0)
            {
                foreach(var pVo in pNames)
                {
                    if (pVo == null)
                        continue;
                    this.peripheralTypeNames.Add(pVo.PeripheralTypeName);
                }
            }
        }

        public void SetId(string guid)
        {
            this.Id = guid;
        }

        public void SetPeripheralTypeId(string ptId)
        {
            this.peripheralTypeId = ptId;
        }


        public bool CalculatePeripheralTypeFromLogicalType(List<PeripheralTypeVO> pTypeList)
        {
            if (CollectionUtilities.isEmpty(pTypeList) ||
                LogicalType == PeripheralType.UNKNOWN)
            {
                return(false);
            }

            var logStr = LogicalType.ToString("g").ToLowerInvariant();
            var pName = this.peripheralTypeNames.Find(x => x.ToLowerInvariant().Contains(logStr));
            if (string.IsNullOrEmpty(pName))
            {
                return (false);
            }

            var pType = pTypeList.Find(x => x.PeripheralTypeName.Equals(pName, StringComparison.OrdinalIgnoreCase));
            if (pType == null)
            {
                return (false);
            }

            this.PeriphType = pType;

            return (true);
        }

        public bool CalculatePeripheralTypeFromTypeId(List<PeripheralTypeVO> pTypeList)
        {
            if (CollectionUtilities.isEmpty(pTypeList) ||
                string.IsNullOrEmpty(this.peripheralTypeId))
            {
                return (false);
            }

            var pType = pTypeList.Find(
                x => x.PeripheralTypeId.Equals(this.peripheralTypeId, StringComparison.OrdinalIgnoreCase));
            if (pType == null)
            {
                return (false);
            }

            this.PeriphType = pType;

            return (true);
        }

        public void CalculateLogicalTypeFromPeripheralType()
        {
            if (string.IsNullOrEmpty(this.peripheralTypeId) ||
                string.IsNullOrEmpty(this.PeriphType.PeripheralTypeName))
            {
                return;
            }

            var enumArr = Enum.GetNames(typeof(PeripheralType));
            foreach(var enumStr in enumArr)
            {
                if (string.IsNullOrEmpty(enumStr))
                    continue;

                var pNameFnd = this.PeriphType.PeripheralTypeName.ToLowerInvariant().Contains(enumStr.ToLowerInvariant());
                if (pNameFnd)
                {
                    this.LogicalType = (PeripheralType)Enum.Parse(typeof (PeripheralType), enumStr);
                    break;
                }
            }

        }
    }
}
