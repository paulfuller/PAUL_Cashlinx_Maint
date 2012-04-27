/********************************************************************
* PawnObjects.VO.Customer
* AddressVO
* Address object - inclues all the address information
 * Address1, Address 2, Unit, City, state code, state name
 * Zip code, country code, country name, notes, alternate string
 * Contact type code, contact method type code
* Sreelatha Rengarajan 4/1/2009 Updated
*******************************************************************/

using System;

namespace Common.Libraries.Objects.Customer
{
    [Serializable]
    public class AddressVO
    {
        public string Address1
        {
            get;
            set;
        }
        public string Address2
        {
            get;
            set;
        }
        public string UnitNum
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public string State_Code
        {
            get;
            set;
        }
        public string State_Name
        {
            get;
            set;
        }

        public string ZipCode
        {
            get;
            set;
        }
        public string Country_Code
        {
            get;
            set;
        }
        public string Country_Name
        {
            get;
            set;
        }
        public string Notes
        {
            get;
            set;
        }
        public string ContactTypeCode
        {
            get;
            set;
        }
        public string ContMethodTypeCode
        {
            get;
            set;
        }
        public string CustAddress
        {
            get
            {
                if (string.IsNullOrEmpty(Address1))
                {
                    return Address1 + "," + Address2 + "," + City + "," + State_Code + "," + ZipCode;
                }
                return Address1 + "," + City + "," + State_Code + "," + ZipCode;
            }
            set
            {
                
            }
   
        }

        public string AlternateString
        {
            get;
            set;
        }

        private void initialize()
        {
            this.Address1 = string.Empty;
            this.Address2 = string.Empty;
            this.City = string.Empty;
            this.State_Code = string.Empty;
            this.State_Name = string.Empty;
            this.ZipCode = string.Empty;
            this.UnitNum = string.Empty;
            this.Country_Code = string.Empty;
            this.Country_Name = string.Empty;
            this.Notes = string.Empty;
            this.ContactTypeCode = string.Empty;
            this.ContMethodTypeCode = string.Empty;
            this.CustAddress = string.Empty;
            this.AlternateString = string.Empty;
        }

        public AddressVO()
        {
            this.initialize();
        }

        public AddressVO(AddressVO avo)
        {
            if (avo != null)
            {
                this.Address1 = avo.Address1;
                this.Address2 = avo.Address2;
                this.City =avo.City;
                this.State_Code = avo.State_Code;
                this.State_Name = avo.State_Name;
                this.ZipCode = avo.ZipCode;
                this.UnitNum = avo.UnitNum;
                this.Country_Code = avo.Country_Code;
                this.Country_Name = avo.Country_Name;
                this.Notes = avo.Notes;
                this.ContactTypeCode = avo.ContactTypeCode;
                this.ContMethodTypeCode = avo.ContMethodTypeCode;
                this.CustAddress = avo.CustAddress;
                this.AlternateString = avo.AlternateString;
            }
            else
            {
                this.initialize();
            }
        }

        public string getCombinedAddress()
        {
            if (string.IsNullOrEmpty(Address2))
            {
                return Address1;
            }
            return Address1 + "," + Address2;
        }

    }
}
