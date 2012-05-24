/********************************************************************
* PawnObjects.VO.Customer
* CustomerVO
* Customer object - inclues identities, phone and address, email and notes
* Sreelatha Rengarajan 4/1/2009 Updated
 * SR 4/6/2010 IN addidentity set all the other identities except the one being added as latest - PWNU00000571
************************************************************************************************************/

using System;

namespace Common.Libraries.Objects.Customer
{
    [Serializable]
    public class VendorVO
    {
        //Simple attributes / accessors (Properties)
        public string ID
        {
            get;
            set;
        }
        public string TaxID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }


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
        public string ZipCode
        {
            get;
            set;
        }
        public string Zip4
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
        public string Comment
        {
            get;
            set;
        }
        public string Ffl
        {
            get;
            set;
        }
        public string CreationStore
        {
            get;
            set;
        }
        public bool ActiveFlag
        {
            get;
            set;
        }
        public string ContactName
        {
            get;
            set;
        }
        public String ContactPhone
        {
            get;
            set;
        }
        public string ContactPhone2
        {
            get;
            set;
        }

        public string faxPhone
        {
            get;
            set;
        }
        
        public DateTime CreationDate
        {
            get;
            set;
        }


        public bool NewVendor
        {
            get;
            set;
        }



        public VendorVO()
        {
            this.initialize();
        }






        private void initialize()
        {
            this.ID = "";
            this.TaxID = "";
            this.Name = "";
            this.Address1 = "";
            this.Address2 = "";
            this.ZipCode = "";
            this.Zip4 = "";
            this.City = "";
            this.State = "";
            this.Comment = "";
            this.Ffl = "";
            this.CreationStore = "";
            this.ActiveFlag = false;
            this.ContactName = "";
            this.ContactPhone = "";
            this.ContactPhone2 = "";
            this.faxPhone = "";
            this.CreationDate = DateTime.Now;
            this.NewVendor = true;
        }

    }

}
