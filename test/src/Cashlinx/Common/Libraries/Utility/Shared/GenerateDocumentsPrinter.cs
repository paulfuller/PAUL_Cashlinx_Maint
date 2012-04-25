using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common.Controllers.Security;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;

//using System.Convert;

namespace Common.Libraries.Utility.Shared
{
    public static class GenerateDocumentsPrinter
    {
        public static readonly string DEFAULTPRINTPORT = "9100";
        //public static readonly string DEFAULTTMPDIR = @"C:\tmp\";

        public static string PrintReceipt(Dictionary<string, string> data, bool displayOnly, out string convertedFullFileName)
        {            
            string rt = PrintingUtilities.PrintReceipt(data, displayOnly, out convertedFullFileName);

            MessageBox.Show("Print utilities for PrintReceipt returned : " + rt);
            return (rt);
        }

        /// <summary>
        /// Print and display loan ticket
        /// </summary>
        //public static string PrintAndDisplayDocument(Dictionary<string, string> data1)
        public static string PrintAndDisplayDocument(Hashtable data, bool displayOnly, out string convertedFullFileName)
        {
            convertedFullFileName = string.Empty;
            const string resultMessage = "0 SUCCESS";
            string inFile = null;
            string inFileBaseName = null;
            string outFile = null;
            string printFile = null;
            string displayFile = null;
            int howMany = 0;
            int printerPort = 0;
            string printerIP = "0";  // Dev printer "172.21.14.13", Public printer "172.21.12.30"
            string printerIP2 = "0";
            int printerPort2 = 0;

            //NOTE: #0127 Fix paths to configured directory
            string tmpDir =
                    SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
                            BaseLogPath + "\\";
            //string tmpDir = DEFAULTTMPDIR;         // Ending with '\' to define file name easily

            //string logDir = @"C:\tmp\logs\";    // Ending with '\' to define file name easily
            //string logFile = null;
            //string sMessage = null;
            string timeStamp = DateTime.Now.ToString("yyMMddHHmmssf");

            if (!Directory.Exists(tmpDir))
                Directory.CreateDirectory(tmpDir);

            //logFile = getLogFileName();*/

            string curDir =
                    SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
                            BaseTemplatePath + "\\";

            var sb = new StringBuilder();
            if (data != null && data.Count > 0)
            {
                foreach (object o in data.Keys)
                {
                    if (o == null)
                        continue;
                    var val = data[o];
                    if (val == null)
                        continue;
                    sb.Append("- ");
                    sb.Append(o.ToString());
                    sb.Append(" = ");
                    sb.Append(val.ToString());
                }
            }


            if (data != null)
            {
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Data input dump:\n" + sb);
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Start");
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Current Directory is " + curDir);

                if (data.ContainsKey("##TEMPLATEFILENAME##"))
                {
                    var inFileName = (string)data["##TEMPLATEFILENAME##"];
                    inFile = curDir + inFileName;
                    if (FileLogger.Instance.IsLogDebug)FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "##TEMPLATEFILENAME## = [" + inFile + "]");
                    inFileBaseName = Path.GetFileName(inFile);
                    //inFile = resourcesDir + inFileBaseName;
                    if (FileLogger.Instance.IsLogDebug)FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Full inFile = [" + inFile + "]");
                    if (FileLogger.Instance.IsLogDebug)FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Full inFile is " + inFile);
                }
                else
                {
                    if (FileLogger.Instance.IsLogDebug)FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: 2 MISSING SYSTEM VARIABLE ##TEMPLATEFILENAME##");
                    if (FileLogger.Instance.IsLogDebug)FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                    if (FileLogger.Instance.IsLogDebug)FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "MISSING SYSTEM VARIABLE ##TEMPLATEFILENAME##");
                    if (FileLogger.Instance.IsLogDebug)FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Press Enter to Exit.");
                    return "2 MISSING SYSTEM VARIABLE ##TEMPLATEFILENAME##";
                }

                if (data.ContainsKey("##IPADDRESS01##"))
                {
                    printerIP = data["##IPADDRESS01##"].ToString();
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "##IPADDRESS01## = [" + printerIP + "]");
                }
                else
                {
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: 3 MISSING SYSTEM VARIABLE ##IPADDRESS01##");
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "MISSING SYSTEM VARIABLE ##IPADDRESS01##");
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Press Enter to Exit.");
                    //Console.ReadLine();
                    return "3 MISSING SYSTEM VARIABLE ##IPADDRESS01##";
                }

                string tmpStr;
                if (data.ContainsKey("##PORTNUMBER01##"))
                {
                    tmpStr = data["##PORTNUMBER01##"].ToString();
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "##PORTNUMBER01## = [" + tmpStr + "]");
                    if (StringUtilities.IsInteger(tmpStr))
                    {
                        printerPort = Convert.ToInt32(tmpStr);
                        // Will add test IP and port here, If failed try again or try 2nd option.
                    }
                    else
                    {
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: 4 ##PORTNUMBER00## IS NOT AN INTEGER");
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PORT MUST BE INTEGER");
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Press Enter to Exit.");
                        //Console.ReadLine();
                        return "4 ##PORTNUMBER01## IS NOT AN INTEGER";
                    }
                }
                else
                {
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: 5 MISSING SYSTEM VARIABLE ##PORTNUMBER01##");
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "MISSING SYSTEM VARIABLE ##PORTNUMBER01##");
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Press Enter to Exit.");
                    //Console.ReadLine();
                    return "5 MISSING SYSTEM VARIABLE ##PORTNUMBER01##";
                }

                if (data.ContainsKey("##HOWMANYCOPIES##"))
                {
                    object numCopies = data["##HOWMANYCOPIES##"];
                    if (numCopies != null)
                    {
                        tmpStr = numCopies.ToString();
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "##HOWMANYCOPIES## = [" + tmpStr + "]");
                        if (StringUtilities.IsInteger(tmpStr))
                        {
                            howMany = Convert.ToInt32(tmpStr);
                        }
                        else
                        {
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: 6 ##HOWMANYCOPIES## IS NOT AN INTEGER");
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "HOWMANYCOPIES MUST BE INTEGER");
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Press Enter to Exit.");
                            //Console.ReadLine();
                            return "6 ##HOWMANYCOPIES## IS NOT AN INTEGER";
                        }
                    }
                    else
                    {
                        return "6.1 ##HOWMANYCOPIES## MESSED UP";
                    }
                }
                else
                {
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: 7 MISSING SYSTEM VARIABLE ##HOWMANYCOPIES##");
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "MISSING SYSTEM VARIABLE ##HOWMANYCOPIES##");
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Press Enter to Exit.");
                    //Console.ReadLine();
                    return "6 MISSING SYSTEM VARIABLE ##HOWMANYCOPIES##";
                }

                /////// 2nd Option 
                if (data.ContainsKey("##IPADDRESS01##"))
                {
                    printerIP2 = data["##IPADDRESS01##"].ToString();
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "##IPADDRESS01## = [" + printerIP2 + "]");
                }

                if (data.ContainsKey("##PORTNUMBER01##"))
                {
                    tmpStr = data["##PORTNUMBER01##"].ToString();
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "##PORTNUMBER01## = [" + tmpStr + "]");
                    if (StringUtilities.IsInteger(tmpStr))
                    {
                        printerPort2 = Convert.ToInt32(tmpStr);
                    }
                }
            }
            else
            {
                if (FileLogger.Instance.IsLogWarn)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, null, "Data passed to PrintAndDisplay");
                }
            }
            //////

            if (inFile != null && File.Exists(inFile))
            {
                inFileBaseName = Path.GetFileName(inFile);
            }
            else
            {
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: 7 Template File " + inFile + " Not Found");
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Template File " + inFile + " Not Found");
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Press Enter to Exit.");
                //Console.ReadLine();
                return "7 Template File " + inFile + " Not Found";
            }

            outFile = tmpDir + inFileBaseName + "-" + timeStamp + ".ps";
            printFile = outFile;
            displayFile = tmpDir + inFileBaseName + "-" + timeStamp + ".pdf";

            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "generateDocuments() Start");
            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "inFile = [" + inFile + "];  outFile = [" + outFile + "]");
            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Path.GetFullPath(inFile) = [" + Path.GetFullPath(inFile) + "]");
            string convertMessage = generateDocuments(inFile, outFile, data);
            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "generateDocuments() Done");

            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "convertMessage: " + convertMessage);
            ////Console.ReadLine();
            if (!convertMessage.Equals("0 SUCCESS"))
            {
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: " + convertMessage);
                return convertMessage;
            }

            if (!File.Exists(printFile))
            {
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: 20 Document File " + printFile + " Not Found");
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Document File " + printFile + " Not Found");
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Press Enter to Exit.");
                return "20 Document File " + printFile + " Not Found";

            }

            if (howMany > 0)
            {
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "printDocument(Option 1) Start");
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "printFile = [" + printFile + "]; printerIP = [" + printerIP +
                        "]; printerPort = [" + printerPort + "]; howMany = [" + howMany + "]");
                //
                // Print document with option 1 printer
                //
                string printMessage = "0 SUCCESS";
                if (displayOnly == false)
                {
                    printMessage = printDocument(printFile, printerIP, printerPort, howMany);
                }

                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "printDocument(Option 1) Done");

                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "printMessage 1: " + printMessage);
                ////Console.ReadLine();
                if (printMessage.Equals("0 SUCCESS") && !displayOnly)
                {
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "(PRINT ON)printDocument(Option 1) Done");
                }
                else if (!displayOnly)
                {
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "(PRINT ON)printDocument(Option 1) Done: " + printMessage);
                    string saveError1 = printMessage;
                    if (!printerIP2.Equals("0") && printerPort2 > 0)
                    {
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "printDocument(Option 2) Start");
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "printFile = [" + printFile + "]; printerIP2 = [" + printerIP2 +
                                "]; printerPort2 = [" + printerPort2 + "]; howMany = [" + howMany + "]");
                        //
                        // If option one printer failed and try option 2
                        //
                        printMessage = printDocument(printFile, printerIP2, printerPort2, howMany);

                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "printDocument(Option 2) Done");

                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "printMessage 2: " + printMessage);
                        if (!printMessage.Equals("0 SUCCESS"))
                        {
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: " + printMessage);
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Error for: printerIP = " + printerIP + "  printerPort = " + printerPort + ":");
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, saveError1);
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Error for printerIP2 = " + printerIP2 + "  printerPort2 = " + printerPort2 + ":");
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, printMessage);
                        }
                    }
                    else
                    {
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: " + printMessage);
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "FAILED TO PRINT DOCUMENT");
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, printMessage);
                    }
                }
                else
                {
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, null, "Loan ticket printing is disabled");
                    }
                }

            }

            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "convertPS2PDF() Start");
            //
            // Convert ps file into pdf file and display pdf file.
            //
            
            string ps2pdfMessage = convertPS2PDF(printFile, displayFile, out convertedFullFileName);
            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "convertPS2PDF() Done");

            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "ps2pdfMessage: " + ps2pdfMessage);

            if (!ps2pdfMessage.Equals("0 SUCCESS"))
            {
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: " + ps2pdfMessage);
                return ps2pdfMessage;
            }

            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Done: " + resultMessage);

            return resultMessage;

        }

        /// <summary>
        /// Insert value into *.qxp file with Hashtable data
        /// </summary>
        /// <param name="inFile"></param>
        /// <param name="outFile"></param>
        /// <param name="data"></param>
        /// 
        public static string generateDocuments(string inFile, string outFile, Hashtable data)
        {
            const string convertMessage = "0 SUCCESS";

            if (File.Exists(outFile))
            {
                var fileInfo = new FileInfo(outFile);
                DateTime writeDateTime = fileInfo.LastWriteTime;

                System.String timeStamp = writeDateTime.ToString("yyyyMMddHHmmssffffff");
                System.String bakFileName = outFile + "." + timeStamp;
                try
                {
                    fileInfo.MoveTo(bakFileName);
                }
                catch (System.Exception eb)
                {
                    BasicExceptionHandler.Instance.AddException("generateDocuments convert failed", eb);
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", string.Format("Convert Failed: Can Not move File {0}to {1}", outFile, bakFileName));
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", string.Format("Detail ERROR: {0}", eb));
                    return string.Format("10 Can Not move File {0} to {1}", outFile, bakFileName);
                }
            }

            if (File.Exists(inFile))
            {
                try
                {
                    FileStream inStream = File.OpenRead(inFile);
                    FileStream outStream = File.OpenWrite(outFile);
                    var streamBinaryReader = new BinaryReader(inStream);
                    var streamBinaryWriter = new BinaryWriter(outStream);
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", string.Format("{0} Start", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.ffffff")));

                    //
                    //This section is for barcode print, in case Variable "104lnh_ticket" not be set value 
                    //
                    if ((data.ContainsKey("lnh_ticket")) && (!data.ContainsKey("104lnh_ticket")))
                    {
                        var tmpStr = string.Format("104{0}", data["lnh_ticket"]);
                        data.Add("104lnh_ticket", tmpStr);
                    }


                    bool isVariable = false;
                    bool withKey = false;

                    var variable = new StringBuilder();
                    var buffer = new Byte[1];

                    int bytesRead = streamBinaryReader.Read(buffer, 0, 1);
                    while (bytesRead > 0)
                    {
                        int c = buffer[0];

                        // [0-9] => 48-57, [A-Z] => 65-90, [_]= 95, [a-z] => 97-122

                        if (c == '_')
                        {
                            withKey = true;
                        }
                        // [0-9] => 48-57, [A-Z] => 65-90, [_]= 95, [a-z] => 97-122

                        if ((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || c == '_' || (c >= 97 && c <= 122))
                        {
                            isVariable = true;
                            variable.Append((char)c);
                        }
                        else
                        {
                            if (isVariable)
                            {
                                var variableStr = variable.ToString();
                                if (withKey)
                                {
                                    withKey = false;
                                    var value = string.Empty;
                                    if (data.ContainsKey(variableStr))
                                    {
                                        value = data[variableStr].ToString();
                                    }

                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        if (value.Length > 66) //75
                                        {
                                            value = value.Substring(0, 66);
                                        }
                                        var buf = value.ToCharArray();
                                        for (var k = 0; k < buf.Length; k++)
                                        {
                                            streamBinaryWriter.Write((byte)buf[k]);
                                        }
                                    }
                                    else
                                    {
                                        var buf = variableStr.ToCharArray();
                                        for (var k = 0; k < buf.Length; k++)
                                        {
                                            streamBinaryWriter.Write((byte)buf[k]);
                                        }
                                    }
                                }
                                else
                                {
                                    var buf = variableStr.ToCharArray();
                                    for (int k = 0; k < buf.Length; k++)
                                    {
                                        streamBinaryWriter.Write((byte)buf[k]);
                                    }
                                }
                                streamBinaryWriter.Write((byte)c);
                                isVariable = false;
                                variable = new StringBuilder();
                            }
                            else
                            {
                                streamBinaryWriter.Write((byte)c);
                            }
                        }
                        bytesRead = streamBinaryReader.Read(buffer, 0, 1);
                    }
                    streamBinaryWriter.Flush();
                    streamBinaryWriter.Close();
                    streamBinaryReader.Close();
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", string.Format("{0} Done", DateTime.Now.ToString("MM/dd/yy HH:mm:ss.fff")));
                    return convertMessage;
                }
                catch (System.Exception ec)
                {
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", string.Format("Convert Failed: {0}", ec));
                    return string.Format("11 Convert Failed: " + "\n{0}", ec);
                }
            }
            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", string.Format("Convert Failed: Template Document {0} Not Found", inFile));
            return string.Format("12 Template File {0} Not Found", inFile);
        }

        /// <summary>
        /// Print Document From file
        /// </summary>
        /// <param name="printFile"></param>
        /// <param name="printerIP"></param>
        /// <param name="printerPort"></param>
        /// <param name="howMany"></param>
        public static string printDocument(string printFile, string printerIP, int printerPort, int howMany)
        {

            if (FileLogger.Instance.IsLogInfo)
            {
                FileLogger.Instance.logMessage(LogLevel.INFO, "GenerateDocumentsPrinter", "Printing document {0} to {1}:{2} {3} time(s)",
                    printFile ?? "null", printerIP ?? "null", printerPort, howMany);
            }
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled == false)
            {
                if (FileLogger.Instance.IsLogWarn)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, "GenerateDocumentsPrinter", "Aborting print attempt. Application is not allowing print operations");
                }
                return ("0 SUCCESS");
            }

            if (string.IsNullOrEmpty(printFile) || string.IsNullOrEmpty(printerIP) || printerPort < 0 || printerPort > 65535 || howMany <= 0)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "GenerateDocumentsPrinter", "Invalid print file inputs");
                }
                return ("25 Invalid print inputs");
            }
            
            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG,
                                               "GenerateDocumentsPrinter",
                                               "Printing " + printFile);
            }

            const string printMessage = "0 SUCCESS";
            try
            {
                if (File.Exists(printFile))
                {
                    if (FileLogger.Instance.IsLogDebug)
                    {
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", "{0} exists, reading data", printFile);
                    }
                    var tcpClient = new TcpClient(printerIP, printerPort);
                    var networkStream = tcpClient.GetStream();
                    var binaryWriter = new BinaryWriter(networkStream);

                    var inStream = File.OpenRead(printFile);
                    var binaryReader = new BinaryReader(inStream);
                    var inLength = (int)binaryReader.BaseStream.Length;
                    var buffer = binaryReader.ReadBytes(inLength);
                    if (FileLogger.Instance.IsLogDebug)
                    {
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", "Data read, length: {0} bytes - Sending to printer", inLength);
                    }
                    for (int iCount = 1; iCount <= howMany; iCount++)
                    {
                        binaryWriter.Write(buffer);
                        binaryWriter.Flush();
                    }
                    if (FileLogger.Instance.IsLogDebug)
                    {
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", "Sent {0} bytes to the printer", inLength);
                    }
                    binaryWriter.Close();
                    binaryReader.Close();
                }
                else
                {
                    return "21 Document " + printFile + " Not Found";
                }
                return printMessage;
            }
            catch (SocketException ex)
            {
                BasicExceptionHandler.Instance.AddException("Socket exception occurred during printing", ex);
                return string.Format("22 Printer Socket ERROR: IP is {0} PORT is {1}{2}{3}", printerIP, printerPort, Environment.NewLine, ex);
            }
            catch (System.Exception ep)
            {
                BasicExceptionHandler.Instance.AddException("Exception occurred during printing", ep);
                return string.Format("23 Printer Socket ERROR: {0}", ep);
            }
        }

        /// <summary>
        /// Print Document From Byte Array
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="printerIP"></param>
        /// <param name="printerPort"></param>
        /// <param name="howMany"></param>
        public static string printDocument(byte[] buffer, string printerIP, int printerPort, int howMany)
        {
            if (FileLogger.Instance.IsLogInfo)
            {
                FileLogger.Instance.logMessage(LogLevel.INFO, "GenerateDocumentsPrinter", "Printing buffer to {0}:{1} {2} time(s)",
                    printerIP ?? "null", printerPort, howMany);
            }
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled == false)
            {
                if (FileLogger.Instance.IsLogWarn)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, "GenerateDocumentsPrinter", "Aborting print attempt. Application is not allowing print operations");
                }
                return ("0 SUCCESS");
            }

            if (buffer == null || buffer.Length <= 0 || string.IsNullOrEmpty(printerIP) || printerPort < 0 || printerPort > 65535 || howMany <= 0)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "GenerateDocumentsPrinter", "Invalid print buffer inputs");
                }
                return ("25 Invalid print inputs");
            }

            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG,
                                               "GenerateDocumentsPrinter",
                                               "Printing data buffer containing {0} bytes", buffer.Length);
            }

            const string printMessage = "0 SUCCESS";
            try
            {
                var tcpClient = new TcpClient(printerIP, printerPort);
                var networkStream = tcpClient.GetStream();
                var binaryWriter = new BinaryWriter(networkStream);

                if (FileLogger.Instance.IsLogDebug)
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", "Data length: {0} bytes - Sending to printer", buffer.Length);
                }
                for (var iCount = 1; iCount <= howMany; iCount++)
                {
                    binaryWriter.Write(buffer);
                    binaryWriter.Flush();
                }
                if (FileLogger.Instance.IsLogDebug)
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, "GenerateDocumentsPrinter", "Sent data to the printer");
                }
                binaryWriter.Close();
                return (printMessage);
            }
            catch (SocketException ex)
            {
                BasicExceptionHandler.Instance.AddException("Socket exception occurred during printing", ex);
                return string.Format("22 Printer Socket ERROR: IP is {0} PORT is {1}{2}{3}", printerIP, printerPort, Environment.NewLine, ex);
            }
            catch (System.Exception ep)
            {
                BasicExceptionHandler.Instance.AddException("Exception occurred during printing", ep);
                return string.Format("23 Printer Socket ERROR: {0}", ep);
            }
        }

        /// <summary>
        /// Print Document from a binary stream
        /// </summary>
        /// <param name="binaryReader"></param>
        /// <param name="printerIP"></param>
        /// <param name="printerPort"></param>
        /// <param name="howMany"></param>
        public static bool printDocument(BinaryReader binaryReader, string printerIP, int printerPort, int howMany)
        {
            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled == false)
            {
                if (FileLogger.Instance.IsLogWarn)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, null, "Aborting print attempt. Application is not allowing print operations");
                    return (true);
                }
            }
            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Printing raw binary stream ... Please Wait.....");

            bool rt = true;
            try
            {
                if (binaryReader != null && 
                    binaryReader.BaseStream != Stream.Null &&
                    binaryReader.BaseStream.CanRead)
                {
                    var tcpClient = new TcpClient(printerIP, printerPort);
                    NetworkStream networkStream = tcpClient.GetStream();
                    var binaryWriter = new BinaryWriter(networkStream);

                    var inLength = (int)binaryReader.BaseStream.Length;
                    byte[] buffer = binaryReader.ReadBytes(inLength);

                    for (int iCount = 1; iCount <= howMany; iCount++)
                    {
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Printing copy " + iCount.ToString());

                        binaryWriter.Write(buffer);
                        binaryWriter.Flush();
                    }
                    binaryWriter.Close();
                    binaryReader.Close();
                }
                else
                {
                    rt = false;
                    if (FileLogger.Instance.IsLogError)
                        FileLogger.Instance.logMessage(LogLevel.ERROR,
                            "GenerateDocumentsPrinter", "Binary reader is invalid");
                    BasicExceptionHandler.Instance.AddException("BinaryReader is invalid", new ApplicationException("Binary reader is invalid"));
                }
            }
            catch (SocketException ex)
            {
                rt = false;
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "GenerateDocumentsPrinter",
                        "Socket exception occurred");
                BasicExceptionHandler.Instance.AddException("Socket exception", new ApplicationException("Socket exception while printing", ex));
            }
            catch (System.Exception ep)
            {
                rt = false;
                if (FileLogger.Instance.IsLogError)
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "GenerateDocumentsPrinter",
                        "Exception occurred while printing");
                BasicExceptionHandler.Instance.AddException("Exception occurred while printing", new ApplicationException("Exception while printing", ep));
            }

            return (rt);
        }

        /// <summary>
        /// 1) Convert PostScript file into PDF file with GhostScript SoftWare gswin32c.exe
        /// 2) Using IE to display PDF file 
        /// </summary>
        /// <param name="inFile"></param>
        /// <param name="outFile"></param>
        /// <param name="outFileFullName"> </param>
        public static string convertPS2PDF(string inFile, string outFile, out string outFileFullName)
        {
            outFileFullName = string.Empty;
            const string ps2pdfMessage = "0 SUCCESS";
            var inFileInfo = new FileInfo(inFile);
            var outFileInfo = new FileInfo(outFile);
            var inFileFullName = inFileInfo.FullName;
            outFileFullName = outFileInfo.FullName;

            try
            {
                if (File.Exists(inFileFullName))
                {
                    var ps2Pdf = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.GhostScriptPath;
                    if (ps2Pdf != null && File.Exists(ps2Pdf))
                    {
                        var procHandle = new Process();
                        string argString = null;
                        argString = string.Format("-dCompatibilityLevel#1.2 -q -dSAFER -dNOPAUSE -dBATCH -sDEVICE#pdfwrite -sOutputFile#" + "\"{0}\"  -dCompatibilityLevel#1.2 -c .setpdfwrite -f" + "\"{1}\"", outFileFullName, inFileFullName);
                        procHandle.StartInfo.FileName = ps2Pdf;
                        procHandle.StartInfo.Arguments = argString;
                        procHandle.StartInfo.CreateNoWindow = true;
                        procHandle.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        procHandle.Start();
                        procHandle.WaitForExit();

                        if (!File.Exists(outFileFullName))
                        {
                            if (FileLogger.Instance.IsLogDebug)
                                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, string.Format("File {0} Not Found.", outFileFullName));
                            return string.Format("31 Display File {0} Not Found.", outFileFullName);
                        }
                    }
                    else
                    {
                        if (FileLogger.Instance.IsLogDebug)
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, string.Format("ps2pdf software {0} Not Found.", ps2Pdf));
                        return string.Format("32 ps2pdf software {0} Not Found.", ps2Pdf);
                    }
                }
                else
                {
                    if (FileLogger.Instance.IsLogDebug)
                        FileLogger.Instance.logMessage(LogLevel.DEBUG, null, string.Format("File {0} Not Found.", inFileFullName));
                    return string.Format("33 psFile {0} Not Found.", inFileFullName);
                }
                return ps2pdfMessage;
            }
            catch (System.Exception e)
            {
                BasicExceptionHandler.Instance.AddException("convertPS2PDF failed", e);
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, string.Format("Error: {0}", e));
                return string.Format("34 Display Document Error: {0}", e);
            }
        }

        public static bool IsIPAddress(string ipAddy)
        {
            if (string.IsNullOrEmpty(ipAddy))
                return (false);
            var isIP = Regex.IsMatch(ipAddy, "^\\d{1,3}\\.d{1,3}\\.d{1,3}$");
            return (isIP);
        }
    }
}
