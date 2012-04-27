/********************************************************************
* PawnObjects.VO.Customer
* CustomerVO
* Customer object - inclues identities, phone and address, email and notes
* Sreelatha Rengarajan 4/1/2009 Updated
 * SR 4/6/2010 IN addidentity set all the other identities except the one being added as latest - PWNU00000571
************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Objects.Customer
{
    [Serializable]
    public class CustomerVO
    {
        //Nested VO attributes
        private List<AddressVO> addresses;
        private List<ContactVO> contacts;
        private List<IdentificationVO> identities;
        private List<CustomerEmailVO> emails;
        private List<CustomerNotesVO> notes;
        private List<StoreCreditVO> storecredit;



        //Simple attributes / accessors (Properties)
        public string CustomerNumber
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }

        public DateTime DateOfBirth
        {
            get;
            set;
        }
        public string CustTitle
        {
            get;
            set;
        }
        public string CustTitleSuffix
        {
            get;
            set;
        }
        public string MiddleInitial
        {
            get;
            set;
        }
        public string SocialSecurityNumber
        {
            get;
            set;
        }
        public string Race
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }
        public string Occupation
        {
            get;
            set;
        }
        public string CompanyName
        {
            get;
            set;
        }
        public string EyeColor
        {
            get;
            set;
        }
        public string HairColor
        {
            get;
            set;
        }
        public string Height
        {
            get;
            set;
        }
        public int Weight
        {
            get;
            set;
        }
        public string PartyId
        {
            get;
            set;
        }

        public DateTime CustomerSince
        {
            get;
            set;
        }

        public string PreferredContactMethod
        {
            get;
            set;
        }
        public string NoCallFlag
        {
            get;
            set;
        }
        public string NoMailFlag
        {
            get;
            set;
        }
        public string NoFaxFlag
        {
            get;
            set;
        }
        public string NoEmailFlag
        {
            get;
            set;
        }
        public string OptOutFlag
        {
            get;
            set;
        }
        public string ReminderContact
        {
            get;
            set;
        }
        public string PreferredCallTime
        {
            get;
            set;
        }
        public string ReceivePromotionOffers
        {
            get;
            set;
        }
        public string HearAboutUs
        {
            get;
            set;
        }
        public string NegotiationLanguage
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public bool NewCustomer
        {
            get;
            set;
        }

        public int ActiveLoanCount
        {
            get;
            set;
        }

        public string CustHomeAddress
        {
            get
            {
                AddressVO addr = ((getHomeAddress() ?? getMailingAddress()) ?? getWorkAddress()) ?? getAdditionalAddress();
                if (addr != null)
                {
                    if (!string.IsNullOrEmpty(addr.Address2))
                    {
                        return addr.Address1 + "," + addr.Address2 + "," + addr.City + "," + addr.State_Code + "," +
                               addr.ZipCode;
                    }
                    return addr.Address1 + "," + addr.City + "," + addr.State_Code + "," + addr.ZipCode;
                }
                return(string.Empty);
            }

        }

        public string CustomerName
        {
            get
            {
                if (string.IsNullOrEmpty(MiddleInitial))
                {
                    return this.FirstName + " " + this.LastName;
                }
                return this.FirstName + " " + this.MiddleInitial + " " + this.LastName;
            }
        }

        public List<AddressVO> CustomerAddress
        {
            get
            {
                return addresses;
            }
            set
            {
                addresses = value;
            }
        }

        public List<CustomerEmailVO> CustomerEmails
        {
            get
            {
                return emails;
            }
            set
            {
                emails = value;
            }
        }

        public List<CustomerNotesVO> CustomerNotes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
            }
        }

        public List<IdentificationVO> CustomerIDs
        {
            get
            {
                return identities;
            }
            set
            {
                identities = value;
            }
        }

        public List<ContactVO> CustomerContacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
            }
        }

        public List<StoreCreditVO> CustomerStoreCredits
        {
            get
            {
                return storecredit;
            }
            set
            {
                storecredit = value;
            }
        }

        public string BackgroundCheckRefNumber
        {
            get;
            set;
        }

        public CustomerVO()
        {
            this.initialize();
        }






        protected void initialize()
        {
            this.addresses = new List<AddressVO>();
            this.contacts = new List<ContactVO>();
            this.identities = new List<IdentificationVO>();
            this.emails = new List<CustomerEmailVO>();
            this.notes = new List<CustomerNotesVO>();
            this.storecredit = new List<StoreCreditVO>();
            this.FirstName = "";
            this.LastName = "";
            this.MiddleInitial = "";
            this.DateOfBirth = DateTime.MaxValue;
            this.Weight = 0;
            this.Age = 0;
            this.Height = "";
            this.HairColor = "";
            this.EyeColor = "";
            this.CustomerNumber = "";
            this.CustTitle = "";
            this.CustTitleSuffix = "";
            this.Gender = "";
            this.PartyId = "";
            this.Race = "";
            this.SocialSecurityNumber = "";
            this.CustomerSince = DateTime.MaxValue;
            this.HearAboutUs = "";
            this.NegotiationLanguage = "";
            this.NoCallFlag = "";
            this.NoEmailFlag = "";
            this.NoFaxFlag = "";
            this.NoMailFlag = "";
            this.OptOutFlag = "";
            this.Occupation = "";
            this.PreferredCallTime = "";
            this.PreferredContactMethod = "";
            this.ReceivePromotionOffers = "";
            this.ReminderContact = "";
            this.Occupation = "";
            this.CompanyName = "";
            this.NewCustomer = true;
            this.BackgroundCheckRefNumber = "";




        }


        //Count accessors
        /// <summary>
        /// Returns the number of addresses related to this customer
        /// </summary>
        public int NumberAddresses
        {
            get
            {
                if (this.addresses == null)
                    return (-1);
                return (this.addresses.Count);
            }
        }

        /// <summary>
        /// Returns the number of contacts related to this customer
        /// </summary>
        public int NumberContacts
        {
            get
            {
                if (this.contacts == null)
                    return (-1);
                return (this.contacts.Count);
            }
        }

        /// <summary>
        /// Returns the number of identities related to this customer
        /// </summary>
        public int NumberIdentities
        {
            get
            {
                if (this.identities == null)
                    return (-1);
                return (this.identities.Count);
            }
        }

        //----- List accessors -----//
        /// <summary>
        /// Add an address to this customer
        /// </summary>
        /// <param name="vo"></param>
        public void addAddress(AddressVO vo)
        {
            if (vo == null || this.addresses == null || this.addresses.Contains(vo))
                return;
            this.addresses.Add(new AddressVO(vo));
        }

        /// <summary>
        /// Get an address belonging to this customer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public AddressVO getAddress(int i)
        {
            if (i < 0 || this.addresses == null || i >= this.addresses.Count)
                return (null);
            return (this.addresses[i]);
        }

        /// <summary>
        /// Add a contact to this customer
        /// </summary>
        /// <param name="vo"></param>
        public void addContact(ContactVO vo)
        {
            if (vo == null || this.contacts == null || this.contacts.Contains(vo))
                return;
            this.contacts.Add(new ContactVO(vo));
        }


        /// <summary>
        /// Add a store credit to this customer
        /// </summary>
        /// <param name="vo"></param>
        public void addStoreCredit(StoreCreditVO vo)
        {
            if (vo == null || this.storecredit == null || this.storecredit.Contains(vo))
                return;
            this.storecredit.Add(vo);
        }



        /// <summary>
        /// Get a contact object that matches the contact type
        /// </summary>
        /// <param name="contactType"></param>
        /// <returns></returns>
        public ContactVO getContact(string contactType)
        {
            if (contactType.Trim().Length == 0 || this.contacts == null)
                return (null);
            ContactVO contObj = this.contacts.Find(delegate(ContactVO contactObj)
            {
                return (contactObj.TypeOfContact() == contactType);
            });

            return contObj;

        }


        
        /// <summary>
        /// Get a contact object that matches the contact type,areacode, phone number, extension and country code provided
        /// overloaded method
        /// </summary>
        /// <param name="contactType"></param>
        /// <param name="areaCode"></param>
        /// <param name="phoneNum"></param>
        /// <param name="extension"></param>
        /// <param name="countryCode"></param>
        /// <param name="isPrimary"></param>
        /// <returns></returns>
        public ContactVO getContact(string contactType, string areaCode, string phoneNum,
            string extension, string countryCode, string isPrimary)
        {
            if (this.contacts == null)
                return (null);
            ContactVO contObj = this.contacts.Find(delegate(ContactVO contactobj)
            {
                return (contactobj.ContactType == contactType &&
                    contactobj.ContactAreaCode == areaCode &&
                    contactobj.ContactPhoneNumber == phoneNum &&
                    contactobj.ContactExtension == extension &&
                    contactobj.CountryDialNumCode == countryCode &&
                    contactobj.TeleusrDefText == isPrimary
                    );
            });

            return contObj;

        }

        /// <summary>
        /// For the IDtype code, number and issuer name passed in check
        /// if any such identification exists for the current customer
        /// </summary>
        /// <param name="idTypeCode"></param>
        /// <param name="idNumber"></param>
        /// <param name="idIssuer"></param>
        /// <returns></returns>
        public IdentificationVO getIdentity(string idTypeCode, string idNumber, string idIssuer)
        {
            if (this.NumberIdentities == 0 || idTypeCode.Trim().Length == 0 || this.identities == null)
                return (null);
            IdentificationVO custId = this.identities.Find(delegate(IdentificationVO idObj)
            {
                return (idObj.IdType == idTypeCode && idObj.IdValue == idNumber && idObj.IdIssuer == idIssuer);
            });

            return custId;
        }

        /// <summary>
        /// For the id type code, number, issuer and expiry date passed in
        /// check if any such identification exists for the current customer
        /// </summary>
        /// <param name="idTypeCode"></param>
        /// <param name="idNumber"></param>
        /// <param name="idIssuer"></param>
        /// <param name="idExpiryDate"></param>
        /// <returns></returns>
        public IdentificationVO getIdentity(string idTypeCode, string idNumber, string idIssuer, string idExpiryDate)
        {
            if (idTypeCode.Trim().Length == 0 || this.identities == null)
                return (null);
            IdentificationVO custId = this.identities.Find(delegate(IdentificationVO idObj)
            {
                return (idObj.IdType == idTypeCode && idObj.IdValue == idNumber && idObj.IdIssuerCode == idIssuer && (idObj.IdExpiryData).FormatDate() == idExpiryDate);
            });

            return custId;
        }


        /// <summary>
        /// Return the ID objects for the customer that matches the ID type passed in
        /// </summary>
        /// <param name="strIdType"></param>
        /// <returns></returns>
        public List<IdentificationVO> getIdentifications(string strIdType)
        {
            if (strIdType.Trim().Length == 0)
            {
                return null;
            }

            List<IdentificationVO> custIds = this.identities.FindAll(delegate(IdentificationVO idObj)
            {
                return (idObj.IdType == strIdType);
            });

            return custIds;


        }

        /// <summary>
        /// Returns all the identifications for this customer
        /// </summary>
        /// <returns></returns>
        public List<IdentificationVO> getAllIdentifications()
        {
            if (this.NumberIdentities > 0)
            {

                List<IdentificationVO> allIdentifications = (from id in this.identities
                                          orderby id.SortOrder
                                          select id).ToList();

                return allIdentifications;
                
            }

            return this.identities;
        }

        /// <summary>
        /// Returns the ID objects of the customer that matches the type and issuer code passed in
        /// </summary>
        /// <param name="strIdType"></param>
        /// <param name="strIdIssuer"></param>
        /// <returns></returns>
        public IdentificationVO getIdByTypeandIssuer(string strIdType, string strIdIssuer)
        {
            if (strIdType.Trim().Length == 0 || strIdIssuer.Trim().Length == 0)
            {
                return null;
            }
            IdentificationVO custId = this.identities.Find(delegate(IdentificationVO idObj)
            {
                return (idObj.IdType == strIdType && idObj.IdIssuerCode == strIdIssuer);
            });

            return custId;

        }





        /// <summary>
        /// Add an identity to this customer
        /// </summary>
        /// <param name="vo"></param>
        public void addIdentity(IdentificationVO vo)
        {
            if (vo == null || this.identities == null || this.identities.Contains(vo))
                return;
            int index = 0;
            string code = vo.IdType;
            if (code == CustomerIdTypes.DRIVERLIC.ToString())
                index = (int)CustomerIdTypes.DRIVERLIC;
            if (code == CustomerIdTypes.SI.ToString())
                index = (int)CustomerIdTypes.SI;
            if (code == CustomerIdTypes.RI.ToString())
                index = (int)CustomerIdTypes.RI;
            if (code == CustomerIdTypes.MC.ToString())
                index = (int)CustomerIdTypes.MC;
            if (code == CustomerIdTypes.MI.ToString())
                index = (int)CustomerIdTypes.MI;
            if (code == CustomerIdTypes.PASSPORT.ToString())
                index = (int)CustomerIdTypes.PASSPORT;
            if (code == CustomerIdTypes.GI.ToString())
                index = (int)CustomerIdTypes.GI;
            if (code == CustomerIdTypes.II.ToString())
                index = (int)CustomerIdTypes.II;
            if (code == CustomerIdTypes.CW.ToString())
                index = (int)CustomerIdTypes.CW;
            //if (code == CustomerIdTypes.FFL.ToString())
            //    index = (int)CustomerIdTypes.FFL;
            if (code == CustomerIdTypes.OT.ToString())
                index = (int)CustomerIdTypes.OT;
            vo.SortOrder = index;
            //Madhu 12/07/2010 fix for bugzilla defect 10
            //foreach (IdentificationVO id in this.identities)
            //    id.IsLatest = false;
            this.identities.Add(vo);



        }

        /// <summary>
        /// Method to remove all identities from the customer object
        /// </summary>
        public void removeIdentities()
        {
            if (this.identities != null)
            {
                int ids = this.identities.Count;
                for (int i = ids - 1; i >= 0; i--)
                {
                    this.identities.RemoveAt(i);
                }
            }

        }


        /// <summary>
        /// Get an identity belonging to this customer
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public IdentificationVO getIdentity(int i)
        {
            if (i < 0 || this.identities == null || i >= this.identities.Count)
                return (null);
            return (this.identities[i]);
        }

        /// <summary>
        /// Returns the first identity object
        /// Ideally this has to be the one whose isLatest=true but
        /// that is not being passed back from the stored procedure so the customer pulled
        /// back from the db may not have this set but for a new customer or if its a new id
        /// entered for an existing customer this value is set in code
        /// </summary>
        /// <returns></returns>
        public IdentificationVO getFirstIdentity()
        {
            if (this.identities != null && this.identities.Count > 0)
            {
                IdentificationVO idObj = this.identities.Find(
                    identObj => identObj.IsLatest);

                if (idObj == null)
                {
                    return(this.identities[0]);
                }
                return(idObj);
            }
            return(null);
        }

        /// <summary>
        /// Sets the IsLatest to true for the id object passed in
        /// </summary>
        /// <param name="idObj"></param>
        public void updateLatestIdentity(IdentificationVO idObj)
        {
            if (idObj != null)
            {
                IdentificationVO custId = getIdentity(idObj.IdType, idObj.IdValue, idObj.IdIssuerCode, (idObj.IdExpiryData).FormatDate());
                if (custId != null)
                    custId.IsLatest = true;
            }
        }

        /// <summary>
        /// Returns the contact type which is set as the 
        /// primary phone in the system
        /// </summary>
        /// <returns></returns>
        public string CustPhoneType()
        {
            ContactVO primaryContact = this.getPrimaryContact();
            if (primaryContact != null)
            {
                return(primaryContact.ContactType);
            }
            return(string.Empty);
        }


        /// <summary>
        /// Returns the contact object of the customer
        /// that is set as primary
        /// </summary>
        /// <returns></returns>
        public ContactVO getPrimaryContact()
        {
            ContactVO custContact = this.contacts.Find(delegate(ContactVO contactObj)
            {
                return (contactObj.TeleusrDefText == "true");
            });
            return custContact;


        }

        /// <summary>
        /// Returns the home address of the customer
        /// </summary>
        /// <returns></returns>
        public AddressVO getHomeAddress()
        {


            AddressVO custAddr = (from addr in this.addresses
                                  where addr.ContactTypeCode == CustomerAddressTypes.HOME_ADDRESS &&
                                  addr.ContMethodTypeCode == "POSTALADDR"
                                  select addr).FirstOrDefault();


            return custAddr;
        }

        /// <summary>
        /// Returns the work address of the customer
        /// </summary>
        /// <returns></returns>
        public AddressVO getWorkAddress()
        {
            AddressVO custAddr = (from addr in this.addresses
                                  where addr.ContactTypeCode == CustomerAddressTypes.WORK_ADDRESS &&
                                  addr.ContMethodTypeCode == "POSTALADDR"
                                  select addr).FirstOrDefault();

            return custAddr;
        }

        /// <summary>
        /// Returns the mailing address of the customer
        /// </summary>
        /// <returns></returns>
        public AddressVO getMailingAddress()
        {
            AddressVO custAddr = (from addr in this.addresses
                                  where addr.ContactTypeCode == CustomerAddressTypes.MAILING_ADDRESS &&
                                  addr.ContMethodTypeCode == "POSTALADDR"
                                  select addr).FirstOrDefault();

            return custAddr;


        }

        /// <summary>
        /// Returns the additional address for the customer
        /// </summary>
        /// <returns></returns>
        public AddressVO getAdditionalAddress()
        {
            AddressVO custAddr = this.addresses.Find(delegate(AddressVO addrObj)
            {
                return (addrObj.ContactTypeCode == CustomerAddressTypes.ADDITIONAL_ADDRESS && addrObj.ContMethodTypeCode == "POSTALADDR");
            });

            return custAddr;

        }

        /// <summary>
        /// Add an email struct to the customer object
        /// </summary>
        /// <param name="vo"></param>
        public void addEmail(CustomerEmailVO vo)
        {
            if (this.emails.Contains(vo))
                return;
            this.emails.Add(vo);
        }

        /// <summary>
        /// Gives the primary email address of the customer
        /// </summary>
        /// <returns></returns>
        public CustomerEmailVO getPrimaryEmail()
        {
            CustomerEmailVO custemail = this.emails.Find(delegate(CustomerEmailVO emailObj)
            {
                return (emailObj.EmailAddressType == CustomerEmailTypes.PRIMARY_EMAIL);
            });
            return custemail;
        }



        /// <summary>
        /// Gives the alternate email address of the customer
        /// </summary>
        /// <returns></returns>
        public CustomerEmailVO getAlternateEmail()
        {
            CustomerEmailVO custemail = this.emails.Find(delegate(CustomerEmailVO emailObj)
            {
                return (emailObj.EmailAddressType == CustomerEmailTypes.SECONDARY_EMAIL);
            });
            return custemail;

        }

        public void removeEmails()
        {
            if (this.emails.Count != 0)
            {
                int emailCount = this.emails.Count;
                for (int i = emailCount - 1; i >= 0; i--)
                {
                    this.emails.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Add the notes to the customer
        /// </summary>
        /// <param name="vo"></param>
        public void addNotes(CustomerNotesVO vo)
        {
            if (this.notes.Contains(vo))
                return;
            this.notes.Add(vo);
        }



        public List<CustomerNotesVO> getAllCommentNotes()
        {

            var custNotes = from note in this.notes
                            orderby note.UpdatedDate descending
                            select note;

            return custNotes.ToList();


        }

        /// <summary>
        /// Gives the latest comment entered for the customer
        /// </summary>
        /// <returns></returns>
        public CustomerNotesVO getLatestNote()
        {
            CustomerNotesVO custNotes = (from note in this.notes
                                         where note.Code != CustomerNoteTypes.PHYSICALDESCNOTES
                                         orderby note.UpdatedDate descending
                                         select note).FirstOrDefault();
            return custNotes;


        }

        /// <summary>
        /// Gives the physical description comment stored for the customer
        /// </summary>
        /// <returns></returns>
        public CustomerNotesVO getPhysicalDescNote()
        {

            CustomerNotesVO physicalNote = (from note in this.notes
                                            where note.Code == CustomerNoteTypes.PHYSICALDESCNOTES
                                            select note).FirstOrDefault();
            return physicalNote;
        }

        /// <summary>
        /// Deletes the physical description note and creates a new one since there can only be one
        /// physical description comment for a customer
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="store"></param>
        /// <param name="noteDate"></param>
        /// <param name="userId"></param>
        /// <param name="contactProdNoteId"> </param>
        public void updatePhysicalDescNote(string comment, string store, DateTime noteDate, string userId, string contactProdNoteId)
        {

            removePhysicalNote();
            var physicalNote = new CustomerNotesVO
                               {
                                   ContactNote = comment,
                                   CustomerProductNoteId = contactProdNoteId,
                                   StoreNumber = store,
                                   CreationDate = noteDate,
                                   CreatedBy = userId,
                                   Code = CustomerNoteTypes.PHYSICALDESCNOTES,
                                   UpdatedDate = noteDate
                               };
            this.addNotes(physicalNote);


        }

        /// <summary>
        /// Removes all comments including physical description for the customer from the object
        /// </summary>
        public void removeNotes()
        {
            if (this.notes.Count != 0)
            {
                int noteCount = this.notes.Count;
                for (int i = noteCount - 1; i >= 0; i--)
                {
                    this.notes.RemoveAt(i);
                }
            }
        }

        private void removePhysicalNote()
        {
            CustomerNotesVO custNote = this.getPhysicalDescNote();
            if (custNote.ContactNote != null)
            {
                int physicalNoteIdx = this.notes.IndexOf(custNote);
                this.notes.RemoveAt(physicalNoteIdx);
            }

        }


        /// <summary>
        /// If home address exists, updates it if not adds a new address object for the customer
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="unit"></param>
        /// <param name="city"></param>
        /// <param name="zipcode"></param>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <param name="vnotes"> </param>
        public void updateHomeAddress(string address1, string address2, string unit, string city, string zipcode, string country, string state, string vnotes)
        {
            bool homeAddressUpdated = false;
            AddressVO customerAddr = (from addr in this.addresses
                                      where addr.ContactTypeCode == CustomerAddressTypes.HOME_ADDRESS
                                      && addr.ContMethodTypeCode == "POSTALADDR"
                                      select addr).FirstOrDefault();
            if (customerAddr != null)
            {

                customerAddr.Address1 = address1;
                customerAddr.Address2 = address2;
                customerAddr.UnitNum = unit;
                customerAddr.City = city;
                customerAddr.ZipCode = zipcode;
                customerAddr.State_Code = state;
                customerAddr.Country_Name = country;
                customerAddr.Notes = vnotes;
                homeAddressUpdated = true;

            }
            if (!(homeAddressUpdated))
            {
                AddressVO custAddr = new AddressVO();
                custAddr.Address1 = address1;
                custAddr.Address2 = address2;
                custAddr.UnitNum = unit;
                custAddr.City = city;
                custAddr.ZipCode = zipcode;
                custAddr.State_Code = state;
                custAddr.Country_Name = country;
                custAddr.Notes = vnotes;
                custAddr.ContactTypeCode = CustomerAddressTypes.HOME_ADDRESS;
                custAddr.ContMethodTypeCode = "POSTALADDR";
                this.addAddress(custAddr);

            }

        }

        /// <summary>
        /// If mailing address exists for the customer, updates it else creates a new address object
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="unit"></param>
        /// <param name="city"></param>
        /// <param name="zipcode"></param>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <param name="vnotes"></param>
        public void updateMailingAddress(string address1, string address2, string unit, string city, string zipcode, string country, string state, string vnotes)
        {
            bool mailingAddrUpdated = false;
            AddressVO customerAddr = (from addr in this.addresses
                                      where addr.ContactTypeCode == CustomerAddressTypes.MAILING_ADDRESS
                                      && addr.ContMethodTypeCode == "POSTALADDR"
                                      select addr).FirstOrDefault();
            if (customerAddr != null)
            {
                customerAddr.Address1 = address1;
                customerAddr.Address2 = address2;
                customerAddr.UnitNum = unit;
                customerAddr.City = city;
                customerAddr.ZipCode = zipcode;
                customerAddr.State_Code = state;
                customerAddr.Country_Name = country;
                customerAddr.Notes = vnotes;
                mailingAddrUpdated = true;
            }
            if (!(mailingAddrUpdated))
            {
                AddressVO custAddr = new AddressVO();
                custAddr.Address1 = address1;
                custAddr.Address2 = address2;
                custAddr.UnitNum = unit;
                custAddr.City = city;
                custAddr.ZipCode = zipcode;
                custAddr.State_Code = state;
                custAddr.Country_Name = country;
                custAddr.Notes = vnotes;
                custAddr.ContactTypeCode = CustomerAddressTypes.MAILING_ADDRESS;
                custAddr.ContMethodTypeCode = "POSTALADDR";
                this.addAddress(custAddr);

            }

        }

        /// <summary>
        /// If work address exists, updates it else creates a new address object
        /// </summary>
        /// <param name="address1"></param>
        /// <param name="address2"></param>
        /// <param name="unit"></param>
        /// <param name="city"></param>
        /// <param name="zipcode"></param>
        /// <param name="country"></param>
        /// <param name="state"></param>
        /// <param name="vnotes"></param>
        public void updateWorkAddress(string address1, string address2, string unit, string city, string zipcode, string country, string state, string vnotes)
        {
            bool workAddrUpdated = false;
            AddressVO customerAddr = (from addr in this.addresses
                                      where addr.ContactTypeCode == CustomerAddressTypes.WORK_ADDRESS
                                      && addr.ContMethodTypeCode == "POSTALADDR"
                                      select addr).FirstOrDefault();
            if (customerAddr != null)
            {
                customerAddr.Address1 = address1;
                customerAddr.Address2 = address2;
                customerAddr.UnitNum = unit;
                customerAddr.City = city;
                customerAddr.ZipCode = zipcode;
                customerAddr.State_Code = state;
                customerAddr.Country_Name = country;
                customerAddr.Notes = vnotes;
                workAddrUpdated = true;

            }
            if (!(workAddrUpdated))
            {
                AddressVO custAddr = new AddressVO();
                custAddr.Address1 = address1;
                custAddr.Address2 = address2;
                custAddr.UnitNum = unit;
                custAddr.City = city;
                custAddr.ZipCode = zipcode;
                custAddr.State_Code = state;
                custAddr.Country_Name = country;
                custAddr.Notes = vnotes;
                custAddr.ContactTypeCode = CustomerAddressTypes.WORK_ADDRESS;
                custAddr.ContMethodTypeCode = "POSTALADDR";
                this.addAddress(custAddr);
            }

        }

        /// <summary>
        /// Updates an existing phone number with the values passed in and if it is not there
        /// creates a new contact object to add to the customer
        /// </summary>
        /// <param name="contactType"></param>
        /// <param name="contactAreaCode"></param>
        /// <param name="contactNumber"></param>
        /// <param name="extension"></param>
        /// <param name="countryCode"></param>
        /// <param name="telecomType"></param>
        /// <param name="isPrimary"></param>
        public void updatePhoneNumber(
            string contactType,
            string contactAreaCode,
            string contactNumber,
            string extension,
            string countryCode,
            string telecomType,
            string isPrimary)
        {
            bool phoneUpdated = false;
            ContactVO customerContact = (from contact in this.contacts
                                         where contact.ContactType == contactType
                                         && contact.TelecomNumType == telecomType
                                         select contact).FirstOrDefault();
            if (customerContact != null)
            {
                customerContact.ContactAreaCode = contactAreaCode;
                customerContact.ContactPhoneNumber = contactNumber;
                customerContact.ContactExtension = extension;
                customerContact.CountryDialNumCode = countryCode;
                customerContact.TelecomNumType = telecomType;
                customerContact.TeleusrDefText = isPrimary;
                phoneUpdated = true;

            }
            if (!(phoneUpdated))
            {
                ContactVO custContact = new ContactVO();
                custContact.ContactType = contactType;
                custContact.ContactAreaCode = contactAreaCode;
                custContact.ContactPhoneNumber = contactNumber;
                custContact.ContactExtension = extension;
                custContact.CountryDialNumCode = countryCode;
                custContact.TelecomNumType = telecomType;
                custContact.TeleusrDefText = isPrimary;
                this.addContact(custContact);
            }
        }

        /// <summary>
        /// Removes all the phone numbers associated to the customer
        /// </summary>
        public void removePhoneNumbers()
        {
            if (this.contacts.Count != 0)
            {
                int phoneCount = this.contacts.Count;
                for (int i = phoneCount - 1; i >= 0; i--)
                {
                    this.contacts.RemoveAt(i);
                }
            }
        }


        /// <summary>
        /// Returns true if the customer has an unexpired
        /// FFL identification in his list of Ids else
        /// returns false
        /// </summary>
        /// <returns></returns>
        public bool HasFederalFirearmsLicense()
        {
            var custId = from customerId in this.identities
                         where customerId.IdType == CustomerIdTypes.FFL.ToString()
                         select customerId.IdValue;
            if (custId.Any())
                return true;
            return false;
        }

        /// <summary>
        /// Returns true if the customer has an unexpired
        /// concealed weapons permit in his list of Ids
        /// else returns false
        /// </summary>
        /// <returns></returns>
        public bool HasConcealedWeaponsPermit()
        {
            var custId = from customerId in this.identities
                         where customerId.IdType == CustomerIdTypes.CW.ToString()
                         select customerId.IdValue;
            if (custId.Any())
                return true;
            return false;
        }


        /// <summary>
        /// Returns true if the customer has an unexpired
        /// concealed weapons permit in his list of Ids
        /// else returns false
        /// </summary>
        /// <returns></returns>
        public bool HasValidConcealedWeaponsPermitInState(string strState, DateTime currentDate)
        {
            var custId = from customerId in this.identities
                         where customerId.IdType == CustomerIdTypes.CW.ToString()
                         && customerId.IdIssuerCode == strState
                         && customerId.IdExpiryData.FormatDate() != string.Empty
                         && customerId.IdExpiryData >= currentDate
                         select customerId.IdValue;
            return custId.Any();
        }
    }




}
