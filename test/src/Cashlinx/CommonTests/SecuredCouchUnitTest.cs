using System;
using System.IO;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Controllers.Database.Couch.Impl;
using Common.Libraries.Objects.Doc;

namespace CommonTests
{
    /// <summary>
    /// Summary description for SecuredCouchUnitTest
    /// </summary>
   // [TestClass]
    public class SecuredCouchUnitTest
    {
        public Document.DocTypeNames DocumentType { set; get; }
        public static string hostname = "localhost";
        public static string portName = "5984";
        public static string secWebPort = "6984";
        public static string dbName = "perfdb";
        public static bool isSec = false;
        private string userid = "mydbuser";
        private string pass = "mydbuser";

        private int perfTestNo = 5000;

        public SecuredCouchUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }
       private void setSecDBUser()
       {
            userid = "secdbuser";
            pass = "secdbuser";
       }


        private void setmyDBUser()
        {
            userid = "mydbuser";
            pass = "mydbuser";
        }

       /* private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }*/

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

       /* [TestMethod]
        public void SimpleAddTest()
        {
            //dbName = "secdb";
            setSecDBUser();
            string msg = string.Empty;
            bool result=false;
            string fileName = "C:\\Program Files\\Phase2App\\templates\\Report031620110858461303709.pdf";
            addDocumentToCouch(fileName,out msg,out result);

            Console.WriteLine(msg);
            Assert.IsTrue(result);
        }*/

        /*[TestMethod]
        public void MultiSizeAddTest()
        {
            //dbName = "secdb";
            setSecDBUser();
            addThisDoc("C:\\Program Files\\Phase2App\\templates\\couchtest\\7mb.pdf");
            addThisDoc("C:\\Program Files\\Phase2App\\templates\\couchtest\\11mb.pdf");
            addThisDoc("C:\\Program Files\\Phase2App\\templates\\couchtest\\14mb.pdf");
            addThisDoc("C:\\Program Files\\Phase2App\\templates\\couchtest\\16mb.pdf");
            addThisDoc("C:\\Program Files\\Phase2App\\templates\\couchtest\\21mb.pdf");
            addThisDoc("C:\\Program Files\\Phase2App\\templates\\couchtest\\32mb.pdf");
        }*/

        /*private void addThisDoc(string fileName)
        {
           
            bool result;
            string msg;
      
            DateTime totalTimeS = DateTime.Now;
            addDocumentToCouch(fileName, out msg, out result);
            DateTime totalTimeE = DateTime.Now;
            printTime("time for doc " + fileName, totalTimeE - totalTimeS);
            Assert.IsTrue(result);
        }*/

       /* [TestMethod]
        public void IncorrectUserAddTest()
        {
            //dbName = "secdb";
            setmyDBUser();
            string msg = string.Empty;
            bool result = false;
            string fileName = "C:\\Program Files\\Phase2App\\templates\\Report031620110858461303709.pdf";
            addDocumentToCouch(fileName, out msg, out result);
            Assert.IsFalse(result);
            //TestContext.WriteLine("Hello");
            Console.WriteLine(msg);
        }*/
        
        //set server session value to 20 secs
       /* [TestMethod]
        public void testDocAddWithSessionExpiry()
        {

           // dbName = "secdb";
            setSecDBUser();

            //string start = DateTime.Now;
            int i = 0;
            string msg = string.Empty;
            bool result = false;
          
            while (true)
            {
                if (i > 3) break;
                string fileName = "C:\\Program Files\\Phase2App\\templates\\Report031620110858461303709.pdf";
                addDocumentToCouch(fileName, out msg, out result);
                Console.WriteLine(msg);
                if (!result)break;
                Thread.Sleep(20000);
                i++;
            }
            Assert.IsTrue(result);
        }*/

       /* [TestMethod]
        public void RandomDocName()
        {
            int min = 1;
            int max = 3;
            Random random = new Random();
            List<string> fileName=new List<string>();
            fileName.Add("C:\\Program Files\\Phase2App\\templates\\couchtest\\random\\eDeviceTemplate.pdf-1108051155362.pdf");
            fileName.Add("C:\\Program Files\\Phase2App\\templates\\couchtest\\random\\FBCFtemplate.TX-1108311603240.pdf");
            fileName.Add("C:\\Program Files\\Phase2App\\templates\\couchtest\\random\\ticketfmtL.TX-1108101349266");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(fileName[random.Next(min, max)]);
            }
        }*/

       /* private string getRandomDocName(List<string> fileName)
        {
            int min = 1;
            int max = fileName.Count;
            Random random = new Random();
            return fileName[random.Next(min, max)];
        }*/


        /*[TestMethod]
        public void DocAddPerfTest()
        {

            // dbName = "secdb";
            setSecDBUser();

            //string start = DateTime.Now;
            int i = 0;
            string msg = string.Empty;
            bool result = false;
            DateTime totalTimeS = DateTime.Now;

            TimeSpan tot = new TimeSpan();
            TimeSpan high = new TimeSpan();
            TimeSpan low = new TimeSpan();

            List<string> fileNameList = new List<string>();
            fileNameList.Add("C:\\Program Files\\Phase2App\\templates\\couchtest\\random\\eDeviceTemplate.pdf-1108051155362.pdf");
            fileNameList.Add("C:\\Program Files\\Phase2App\\templates\\couchtest\\random\\FBCFtemplate.TX-1108311603240.pdf");
            fileNameList.Add("C:\\Program Files\\Phase2App\\templates\\couchtest\\random\\ticketfmtL.TX-1108101349266.ps");

            while (true)
            {
                if (i == perfTestNo) break;
                string fileName = getRandomDocName(fileNameList);
                DateTime begin = DateTime.Now;
                addDocumentToCouch(fileName, out msg, out result);
                Console.WriteLine(msg);
                if (!result) break;
                DateTime end = DateTime.Now;
                 printTime("added "+i, (end-begin));
                if ((end - begin)> high)
                {
                    high = (end - begin);
                }
                if ((end - begin)< low)
                {
                    low = (end - begin);
                }

                i++;
            }
            DateTime totalTimeE = DateTime.Now;
            //Console.WriteLine("Ave time to add :" + (TimeSpan.FromTicks((tot.Ticks)/ perfTestNo).TotalMilliseconds + " Msec:" + (tot.TotalSeconds / perfTestNo) + " Secs");
            printTime("Max time to add", high);
            printTime("Min time to add", low);
            printTime("total add " + i +" Docs ", totalTimeE - totalTimeS);
            Assert.IsTrue(result);
        }*/
        
       /* [TestMethod]
        public void documentGetTest()
        {
           // dbName = "secdb";
            setSecDBUser();
            Assert.IsTrue(getDocumentByID("0064cd50-5a8b-4d46-a584-42e5a39d1f38"));
        }*/

       /* [TestMethod]
        public void GetALL()
        {
            setSecDBUser();
            var CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            IDictionary<string, object>[] dict = CouchConnectorObj.doTestGetALL();
            if (CouchConnectorObj.Error)
            {
                Console.WriteLine(CouchConnectorObj.Message);
                Assert.Fail();

            }
            else
            {
                Console.WriteLine("Total doc count :" + dict.Length);
            }
        }*/

       /* [TestMethod]
        public void GetALLAndVerify()
        {
            setSecDBUser();
            var CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            DateTime totalTimeS = DateTime.Now;
            IDictionary<string, object>[] dict = CouchConnectorObj.doTestGetALL();
            Console.WriteLine("Total docs :" + dict.Length);
            DateTime totalTimeE = DateTime.Now;
            printTime("Pull ALL " + dict.Length + " Docs ", totalTimeE - totalTimeS);
            if (CouchConnectorObj.Error)
            {
                Console.WriteLine(CouchConnectorObj.Message);
                Assert.Fail();

            }
            else
            {
                Console.WriteLine("Total doc count :" + dict.Length);
                totalTimeS = DateTime.Now;
                foreach (IDictionary<string, object> dictionary in dict)
                {
                  getDocumentByID(dictionary["id"].ToString());
                }
                 totalTimeE = DateTime.Now;
                printTime("Total get individual doc " + dict.Length + " Docs ", totalTimeE - totalTimeS);
            }
        }*/

        /*[TestMethod]
        public void GetALLAndRandomVerify()
        {
            setSecDBUser();
            var CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            DateTime totalTimeS = DateTime.Now;
            IDictionary<string, object>[] dict = CouchConnectorObj.doTestGetALL();
            if (CouchConnectorObj.Error)
            {
                Console.WriteLine(CouchConnectorObj.Message);
                Console.WriteLine("Skip test");
                Assert.Fail();
                return;
            }
            Console.WriteLine("Total docs :" + dict.Length);
            DateTime totalTimeE = DateTime.Now;
            printTime("Pull ALL " + dict.Length + " Docs ", totalTimeE - totalTimeS);
            if (CouchConnectorObj.Error)
            {
                Console.WriteLine(CouchConnectorObj.Message);
                Assert.Fail();

            }
            else
            {
                Console.WriteLine("Total doc count :" + dict.Length);
                List<string>docIDList=new List<string>();

                foreach (IDictionary<string, object> dictionary in dict)
                {
                    docIDList.Add(dictionary["id"].ToString());
                }

                totalTimeS = DateTime.Now;
                for (int i = 0; i < 100;i++ )
                {
                    getDocumentByID(getRandomDocName(docIDList));
                }
                totalTimeE = DateTime.Now;
                printTime("Total get individual doc " + dict.Length + " Docs ", totalTimeE - totalTimeS);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            setSecDBUser();
            string docID = "0064cd50-5a8b-4d46-a584-42e5a39d1f38";
            var CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            DateTime totalTimeS = DateTime.Now;
            CouchConnectorObj.Document_Delete(docID);
            Console.WriteLine(CouchConnectorObj.Error);
            Console.WriteLine(CouchConnectorObj.Message);
            DateTime totalTimeE = DateTime.Now;
            printTime("Delete time for Doc "+docID, totalTimeE - totalTimeS);
        }

        [TestMethod]
        public void DeleteALL()
        {
            setSecDBUser();
            var CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
            DateTime totalTimeS = DateTime.Now;
            IDictionary<string, object>[] dict = CouchConnectorObj.doTestGetALL();
            if(CouchConnectorObj.Error)
            {
                Console.WriteLine(CouchConnectorObj.Message);
                Console.WriteLine("Skip test");
                Assert.Fail();
                return;
            }
            Console.WriteLine("Total docs :" + dict.Length);
            DateTime totalTimeE = DateTime.Now;
            printTime("Pull ALL " + dict.Length + " Docs ", totalTimeE - totalTimeS);
            if (CouchConnectorObj.Error)
            {
                Console.WriteLine(CouchConnectorObj.Message);
                Assert.Fail();

            }
            else
            {
                Console.WriteLine("Total doc count :" + dict.Length);
                totalTimeS = DateTime.Now;
                foreach (IDictionary<string, object> dictionary in dict)
                {
                    CouchConnectorObj.Document_Delete(dictionary["id"].ToString());
                }
                totalTimeE = DateTime.Now;
                printTime("Total delete individual doc " + dict.Length + " Docs ", totalTimeE - totalTimeS);
            }
            
        }

        [TestMethod]
        public void RandomNumber()
        {
            int min = 1;
            int max = 25;
            Random random = new Random();
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(random.Next(min, max));  
            }
        }*/

        private void printTime(string msg, TimeSpan elapsedTime)
        {
            Console.WriteLine(String.Format("Time Taken for '{0}' is '{1}' Min : '{2}'Sec :'{3}' Msec", msg,
                elapsedTime.TotalMinutes, elapsedTime.TotalSeconds, elapsedTime.TotalMilliseconds));
        }
       
        private string addDocumentToCouch(String docName,out string msg,out bool added)
        {
            added = false;
            msg = string.Empty;

            DocumentType = Document.DocTypeNames.PDF;
            string fileName = docName;
            int lastSlashIdx = fileName.LastIndexOf('\\');
            string fName = fileName.Substring(lastSlashIdx + 1);
            var sErrCode = string.Empty;
            var sErrText = string.Empty;
            byte[] fileBytes = File.ReadAllBytes(fileName);
            Document couchDocObj = (Document)new Document_Couch(fileName, fName, DocumentType);
            couchDocObj.SetPropertyData("jak prop1", "value");
            string storageID = System.Guid.NewGuid().ToString();
            SecuredCouchConnector CouchConnectorObj = null;
            //storageID = "1234";
            if (couchDocObj.SetSourceData(fileBytes, storageID, false))
            {
                var dStorage = new DocStorage_CouchDB();

                CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);
                dStorage.SecuredAddDocument(CouchConnectorObj, ref couchDocObj, out sErrCode, out sErrText);
                if(sErrCode=="0")
                {
                    added = true;
                    msg = "Added" + fileName;
                }else
                {
                     added = false;
                     msg = sErrText;
                }
             
            }
            return storageID;
        }


        public bool getDocumentByID(string id)
        {
            var dStorage = new DocStorage_CouchDB();
            Document doc = new Document_Couch(id);
            var errCode = string.Empty;
            var errTxt = string.Empty;
            byte[] fileData;
            FileStream fStream = null;
            var CouchConnectorObj = new SecuredCouchConnector(hostname, portName, secWebPort, dbName, userid, pass, isSec);

            dStorage.SecuredGetDocument(CouchConnectorObj, ref doc, out errCode, out errTxt);
        
            if (CouchConnectorObj.Error)
            {
                Console.WriteLine(errTxt);
                return false;
            }
            else
            {
                Console.WriteLine(string.Format("Got Doc '{0}'  from db '{1}' by userID '{2}'", doc.FileId, dbName, userid));
                doc.GetSourceData(out fileData);
                if (fileData != null)
                {
                    fStream = File.Create(Directory.GetCurrentDirectory() +"//"+ doc.FileId + ".pdf");
                    fStream.Write(fileData, 0, fileData.Length);
                    Console.WriteLine("written to file :" + Directory.GetCurrentDirectory() + "//" + doc.FileId + ".pdf");
                    fStream.Flush();
                    fStream.Close();
                }
                return true;
            }
           
        }
    }
}
