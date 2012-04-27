/********************************************************************
* PawnObjects.VO.Customer
* IdentificationVO
* Identification object that has the identity details of a customer
* Sreelatha Rengarajan 4/1/2009 Updated
*******************************************************************/

using System;

namespace Common.Libraries.Objects.Customer
{
    [Serializable]
    public class IdentificationVO
    {
        public string IdIssuer
        {
            get;
            set;
        }
        public string IdIssuerCode
        {
            get;
            set;
        }
        public string IdValue
        {
            get;
            set;
        }
        public string IdType
        {
            get;
            set;
        }
        public DateTime IdExpiryData
        {
            get;
            set;
        }
        public string IDDesc
        {
            get;
            set;
        }
        public string DatedIdentDesc
        {
            get;
            set;
        }

        public string IdentId
        {
            get;
            set;
        }
        public bool IsLatest
        {
            get;
            set;
        }
        public DateTime IdIssueDate
        {
            get;
            set;
        }
        public DateTime CreationDate
        {
            get;
            set;
        }
        public int SortOrder
        {
            get;
            set;
        }

        public IdentificationVO()
        {
            this.initialize();
        }


        public IdentificationVO(IdentificationVO ivo)
        {
            if (ivo != null)
            {
                this.IdIssuer = ivo.IdIssuer;
                this.IdIssuerCode = ivo.IdIssuerCode;
                this.IdType = ivo.IdType;
                this.IdValue = ivo.IdValue;
                this.IDDesc = ivo.IDDesc;
                this.DatedIdentDesc = ivo.DatedIdentDesc;
                this.IdExpiryData = ivo.IdExpiryData;
                this.IdentId = ivo.IdentId;
                this.IsLatest = ivo.IsLatest;
                this.IdIssueDate = ivo.IdIssueDate;
                this.CreationDate = ivo.CreationDate;
            }
            else
            {
                this.initialize();
            }
        }

        private void initialize()
        {
            this.IdIssuer = "";
            this.IdIssuerCode = "";
            this.IdType = "";
            this.IdValue = "";
            this.IDDesc = "";
            this.DatedIdentDesc = "";
            this.IdExpiryData = DateTime.MaxValue;
            this.IdentId = "";
            this.IsLatest = false;
            this.IdIssueDate = DateTime.MaxValue;
            this.CreationDate = DateTime.MaxValue;
            this.SortOrder = 1;
        }

        public string IDData
        {
            get
            {
                return this.IdType + "-" + this.IdValue;
            }
        }

        public bool IsSameIDType(IdentificationVO compareToObj)
        {
            if (this.IdType == compareToObj.IdType)
                return true;
            else
                return false;
        }

        public bool IsSameIDDesc(IdentificationVO compareToObj)
        {
            if (this.DatedIdentDesc == compareToObj.DatedIdentDesc)
                return true;
            else
                return false;
        }


        
    }
}
