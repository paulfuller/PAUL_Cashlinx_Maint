//***********************************************************
// 3-mar-2010  rjm  added code to pad left the receipt
//                  number with zeros to make it 14 chars long
//
// 29-mar-2010 rjm  changed disclaimer
//*************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.String;

//using System.Convert;

namespace Common.Libraries.Utility.Shared
{
    public static class PrintingUtilities
    {
        //public static readonly string DEFAULTTMPDIR = @"C:\tmp\";
        /// <summary>
        /// 
        /// </summary>
        /// 
        public static string PrintReceipt(Dictionary<string, string> data, bool displayOnly, out string convertedFullfileName)
        {
            convertedFullfileName = string.Empty;
            //Console.WriteLine("In PrintReceipt Dictionary");
            //Console.ReadLine();
            string sMessage = null;

            try
            {
                Hashtable data1 = new Hashtable();
                data1 = DictionaryStrStr2Hashtable(data);
                sMessage = PrintReceipt(data1, displayOnly, out convertedFullfileName);
                return sMessage;
            }
            catch (System.Exception e)
            {
                //Console.WriteLine("FAILED TO PRINT DOCUMENT: \n" + e.ToString());
                //Console.WriteLine("Press Enter to Exit.");
                //Console.ReadLine();
                return string.Format("600 FAILED TO PRINT DOCUMENT: " + "\n{0}", e);
            }
        }

        public static string PrintReceipt(Hashtable data, bool displayOnly, out string convertedFullFileName)
        {
            convertedFullFileName = string.Empty;
            //Console.WriteLine("In PrintReceipt Hashtable");
            //Console.ReadLine();
            string resultMessage = "0 SUCCESS";

            string tmpStr = null;
            string inFile = null;
            string inFileBaseName = null;
            string outFile = null;
            string printFile = null;
            int howMany = 0;
            int printerPort = 0;
            string printerIP = "0";  // Dev printer "172.21.14.13", Public printer "172.21.12.30"

            //string tmpDir = DEFAULTTMPDIR;         // Ending with '\' to define file name easily
            //NOTE: Fix #0127 - Directory structure configured enforcement
            string tmpDir =
            SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.
            BaseLogPath + "\\";
            //string logDir = @"C:\tmp\logs\";    // Ending with '\' to define file name easily
            //string logFile1 = logDir + "PrintAndDisplayDocument_01.log";
            //string logFile2 = logDir + "PrintAndDisplayDocument_02.log";
            //string logFile = null;
            //long maxLogFileSize = 2000;
            //string sMessage = null;
            StringBuilder sb = new StringBuilder();
            if (data != null && data.Count > 0)
            {
                foreach (object o in data.Keys)
                {
                    if (o == null)
                        continue;
                    object val = data[o];
                    if (val == null)
                        continue;
                    sb.Append("- ");
                    sb.Append(o.ToString());
                    sb.Append(" = ");
                    sb.Append(val.ToString());
                }
            }
            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Data input dump:\n" + sb);
            if (FileLogger.Instance.IsLogDebug)
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintAndDisplayDocument Start");

            string timeStamp = DateTime.Now.ToString("yyMMddHHmmssf");

            if (!Directory.Exists(tmpDir))
                Directory.CreateDirectory(tmpDir);

            //logFile = getLogFileName();
            //logFile = getLogFileName(@"C:\tmpj2\logs2","logf1.log","logf2.log",50000L);

            string curDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseTemplatePath + "\\";

            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, System.String.Empty);
            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintReceipt Start");
            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Current Directory is " + curDir);

            if (data["##TEMPLATEFILENAME##"] != null)
            {
                string inFileName = (string)data["##TEMPLATEFILENAME##"];
                inFile = curDir + inFileName;
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "##TEMPLATEFILENAME## = [" + inFile + "]");
                inFileBaseName = Path.GetFileName(inFile);
                //inFile = resourcesDir + inFileBaseName;
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Full inFile = [" + inFile + "]");
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Full inFile is " + inFile);
                ///
                /// When we use absolute path common these 3 lines out.-- END.
                /// 
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Full inFile = [" + inFile + "]");
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintReceipt Done: 2 MISSING SYSTEM VARIABLE ##TEMPLATEFILENAME##");
                return "2 MISSING SYSTEM VARIABLE ##TEMPLATEFILENAME##";
            }

            if (data["##IPADDRESS01##"] != null)
            {
                printerIP = (string)data["##IPADDRESS01##"];
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "##IPADDRESS01## = [" + printerIP + "]");
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintReceipt Done: 3 MISSING SYSTEM VARIABLE ##IPADDRESS01##");
                return "3 MISSING SYSTEM VARIABLE ##IPADDRESS01##";
            }

            if (data["##PORTNUMBER01##"] != null)
            {
                tmpStr = (string)data["##PORTNUMBER01##"];
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "##PORTNUMBER01## = [" + tmpStr + "]");
                if (StringUtilities.IsInteger(tmpStr))
                {
                    printerPort = Convert.ToInt32(tmpStr);
                    // Will add test IP and port here, If failed try again or try 2nd option.
                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintReceipt Done: 4 ##PORTNUMBER01## IS NOT AN INTEGER");
                    return "4 ##PORTNUMBER01## IS NOT AN INTEGER";
                }
            }
            else
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintReceipt Done: 5 MISSING SYSTEM VARIABLE ##PORTNUMBER01##");
                return "5 MISSING SYSTEM VARIABLE ##PORTNUMBER01##";
            }

            /************************************************/
            //Set receipt how many to one always
            howMany = 1;
            /************************************************/

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
                return "7 Template File " + inFile + " Not Found";
            }

            outFile = tmpDir + inFileBaseName + "-" + timeStamp + ".ps";
            printFile = outFile;

            // Call SedReceipt() to replace variables with values.
            string convertMessage = SedReceipt(inFile, outFile, data); //data Hashtable
            convertedFullFileName = outFile;
            if (!convertMessage.Equals("0 SUCCESS"))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, null, "PrintReceipt error occurred: " + convertMessage);
                return convertMessage;
            }

            if (!File.Exists(printFile))
            {
                FileLogger.Instance.logMessage(LogLevel.ERROR, null, "PrintReceipt error occurred: 20 Document File " + printFile + " Not Found");
                return "20 Document File " + printFile + " Not Found";
            }

            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Receipt variable replacement complete, beginning print network operation");
            }

            if (howMany > 0 && !displayOnly)
            {
                if (FileLogger.Instance.IsLogDebug)
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintReceipt data fileName = [" + printFile + "]; printerIP = [" + printerIP +
                                                                         "]; printerPort = [" + printerPort + "]; howMany = [" + howMany + "]");
                }

                // Print document
                string printMessage = printDocument(printFile, printerIP, printerPort, howMany);

                if (printMessage.Equals("0 SUCCESS"))
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "printDocument Successful");
                }
                else
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, null, "printDocument Error: Message = " + printMessage);
                }
            }
            else if (displayOnly)
            {
                resultMessage = "0 SUCCESS";
            }

            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "PrintReceipt Done: " + resultMessage);
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
            string convertMessage = "0 SUCCESS";

            if (File.Exists(outFile))
            {
                FileInfo fileInfo = new FileInfo(outFile);
                DateTime writeDateTime = fileInfo.LastWriteTime;

                System.String timeStamp = writeDateTime.ToString("yyyyMMddHHmmssffffff");
                System.String bakFileName = outFile + "." + timeStamp;
                try
                {
                    fileInfo.MoveTo(bakFileName);
                }
                catch (System.Exception)
                {
                    return "10 Can Not move File " + outFile + "to " + bakFileName;
                }
            }

            if (File.Exists(inFile))
            {
                try
                {
                    FileStream inStream = File.OpenRead(inFile);
                    FileStream outStream = File.OpenWrite(outFile);
                    BinaryReader streamBinaryReader = new BinaryReader(inStream);
                    BinaryWriter streamBinaryWriter = new BinaryWriter(outStream);

                    string logDir =
                    SecurityAccessor.Instance.EncryptConfig.ClientConfig.
                    GlobalConfiguration.BaseLogPath + "\\";
                    if (!Directory.Exists(logDir))
                        Directory.CreateDirectory(logDir);
                    string logFile = logDir + "DocPrintAndDisplay.log";
                    FileStream logStream = File.OpenWrite(logFile);
                    StreamWriter logStreamWriter = new StreamWriter(logStream);
                    logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + " Start");

                    //
                    //This section is for barcode print, in case Variable "104lnh_ticket" not be set value 
                    //
                    if ((data["lnh_ticket"] != null) && (data["104lnh_ticket"] == null))
                    {
                        string tmpStr = "104" + (string)data["lnh_ticket"];
                        data.Add("104lnh_ticket", tmpStr);
                    }

                    bool isVariable = false;
                    bool withKey = false;

                    StringBuilder variable = new StringBuilder();
                    byte[] buffer = new Byte[1];
                    int bytesRead = 0;
                    int i = 0;
                    int iVar = 0;

                    bytesRead = streamBinaryReader.Read(buffer, 0, 1);
                    while (bytesRead > 0)
                    {
                        int c = buffer[0];

                        //i++;
                        // [0-9] => 48-57, [A-Z] => 65-90, [_]= 95, [a-z] => 97-122

                        //if (i < 1000)
                        //{
                        //Console.WriteLine("[" + c + "]");
                        //}
                        //Console.Read();

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
                                    i++;
                                    withKey = false;
                                    var value = (string)data[variableStr];
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        string dataValue;
                                        iVar++;
                                        logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yy HH:mm:ss.fff") +
                                                                  " [" + variableStr +
                                                                  "] = [" + value + "]");

                                        if (value.Length > 66) //75
                                        {
                                            value = value.Substring(0, 66);
                                        }
                                        //////////////////////////////////////
                                        /* */
                                        /*
                                        /// _BARDATA_nn and _BARDATA_H_nn are used for EPSON TM-T88IV
                                        /// Receipt priner. 
                                        /// _BARDATA_nn printer 128code format bar code without HRI
                                        /// _BARDATA_H_nn printer 128code format bar code with HRI(below)
                                        ///*/
                                        if (variableStr.IndexOf("_BARDATA_", System.StringComparison.OrdinalIgnoreCase) < 0)
                                        {
                                            dataValue = value;
                                        }
                                        else
                                        {
                                            if (variableStr.IndexOf("_BARDATA_H_", System.StringComparison.OrdinalIgnoreCase) < 0)
                                            {
                                                dataValue = Get128BarCodeString(value, 0);
                                            }
                                            else
                                            {
                                                dataValue = Get128BarCodeString(value, 2);
                                            }
                                        }
                                        /* */
                                        //////////////////////////////////////
                                        //char[] buf = value.ToCharArray();
                                        char[] buf = dataValue.ToCharArray();
                                        for (int k = 0; k < buf.Length; k++)
                                        {
                                            streamBinaryWriter.Write((byte)buf[k]);
                                        }
                                    }
                                    else
                                    {
                                        if (variableStr.Length > 2 && variableStr.Length < 15)
                                        {
                                            logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yy HH:mm:ss.fff") +
                                                                      " [" + variableStr + "] not found");
                                        }
                                        char[] buf = variableStr.ToCharArray();
                                        for (int k = 0; k < buf.Length; k++)
                                        {
                                            streamBinaryWriter.Write((byte)buf[k]);
                                        }
                                    }
                                }
                                else
                                {
                                    char[] buf = variableStr.ToCharArray();
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
                    logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yy HH:mm:ss.fff") + " Done");
                    logStreamWriter.Flush();
                    logStreamWriter.Close();

                    return convertMessage;
                }
                catch (System.Exception ec)
                {
                    BasicExceptionHandler.Instance.AddException("generateDocuments Failed", ec);
                    return "11 Convert Failed: " + "\n" + ec;
                }
            }
            return "12 Template File " + inFile + " Not Found";
        }

        /// <summary>
        /// Print Document
        /// </summary>
        /// <param name="printFile"></param>
        /// <param name="printerIP"></param>
        /// <param name="printerPort"></param>
        /// <param name="howMany"></param>
        public static string printDocument(string printFile, string printerIP, int printerPort, int howMany)
        {
            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Printing " + printFile + "...STARTED");
            }

            string returnMessage = "0 SUCCESS";
            try
            {
                //printerIP = "1.2.3.4"; // For error test
                //printerPort = 24567;   // For error test
                if (File.Exists(printFile))
                {
                    TcpClient tcpClient = new TcpClient(printerIP, printerPort);
                    NetworkStream networkStream = tcpClient.GetStream();
                    BinaryWriter binaryWriter = new BinaryWriter(networkStream);

                    FileStream inStream = File.OpenRead(printFile);
                    BinaryReader binaryReader = new BinaryReader(inStream);
                    int inLength = (int)binaryReader.BaseStream.Length;
                    byte[] buffer = new byte[inLength];
                    buffer = binaryReader.ReadBytes(inLength);
                    for (int iCount = 1; iCount <= howMany; iCount++)
                    {
                        if (FileLogger.Instance.IsLogDebug)
                        {
                            FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Printing copy {0}", iCount);
                        }
                        binaryWriter.Write(buffer);
                        binaryWriter.Flush();
                    }
                    //binaryWriter.Flush();
                    binaryWriter.Close();
                    binaryReader.Close();
                    networkStream.Dispose();
                    tcpClient.Close();
                    returnMessage = "0 SUCCESS";
                }
                else
                {
                    returnMessage = "Document " + printFile + " Not Found";
                }
            }
            catch (SocketException ex)
            {
                returnMessage = "Printer Socket Error: IP is " + printerIP + " PORT is " +
                                printerPort + System.Environment.NewLine + "Exception: " + ex;
            }
            catch (System.Exception ep)
            {
                returnMessage = "Printer Socket Error: " + ep;
            }

            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Printing " + printFile + "...FINISHED");
            }

            return (returnMessage);
        }

        public static string SedDocument(string inFile, string outFile, Hashtable data)
        {
            Console.WriteLine("In SedDocument Hashtable");
            //Console.ReadLine();
            string convertMessage = null;
            try
            {
                Dictionary<string, string> data1 = new Dictionary<string, string>();
                data1 = Hashtable2DictionaryStrStr(data);
                convertMessage = SedDocument(inFile, outFile, data1);
                return convertMessage;
            }
            catch (System.Exception e)
            {
                //Console.WriteLine("SedDocument Failed: " + e.ToString());
                //Console.WriteLine("Press Enter to Exit.");
                //Console.ReadLine();
                return "501 SedDocument Failed: " + "\n" + e.ToString();
            }
        }

        public static string SedDocument(string inFile, string outFile, Dictionary<string, string> data)
        {
            //Console.WriteLine("In SedDocument Dictionary");
            //Console.ReadLine();
            string convertMessage = "0 SUCCESS";
            string logDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\";
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            string logFile = logDir + "SedDocument.log";
            FileStream logStream = File.OpenWrite(logFile);
            StreamWriter logStreamWriter = new StreamWriter(logStream);
            string timsS = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            logStreamWriter.WriteLine(timsS + " Start");
            logStreamWriter.WriteLine(timsS + " inFile = [" + inFile + "]");
            logStreamWriter.WriteLine(timsS + " outFile = [" + outFile + "]");
            logStreamWriter.WriteLine(timsS + " logFile = [" + logFile + "]");

            if (File.Exists(outFile))
            {
                FileInfo fileInfo = new FileInfo(outFile);
                DateTime writeDateTime = fileInfo.LastWriteTime;

                System.String timeStamp = writeDateTime.ToString("yyyyMMddHHmmssfff");
                System.String bakFileName = outFile + "." + timeStamp;
                try
                {
                    fileInfo.MoveTo(bakFileName);
                }
                catch (System.Exception)
                {
                    return "10 Can Not move File " + outFile + "to " + bakFileName;
                }
            }

            if (File.Exists(inFile))
            {
                try
                {
                    FileStream inStream = File.OpenRead(inFile);
                    FileStream outStream = File.OpenWrite(outFile);
                    BinaryReader streamBinaryReader = new BinaryReader(inStream);
                    BinaryWriter streamBinaryWriter = new BinaryWriter(outStream);

                    StringBuilder inFileString = new StringBuilder();
                    byte[] buffer = new Byte[1];
                    int bytesRead = 0;
                    bytesRead = streamBinaryReader.Read(buffer, 0, 1);
                    while (bytesRead > 0)
                    {
                        int c = buffer[0];
                        //Console.Write("[" + c + "] ");
                        inFileString.Append((char)c);
                        bytesRead = streamBinaryReader.Read(buffer, 0, 1);
                    }
                    streamBinaryReader.Close();
                    //Console.ReadLine();
                    int aLen = 0;

                    ICollection<string> keys = data.Keys;

                    //long count = keys.LongCount;

                    foreach (string str in keys)
                    {
                        if (str.Length >= 2)
                        {
                            if (!str.Substring(0, 2).Equals("##"))
                            {
                                aLen = aLen + 1;
                            }
                        }
                        else
                        {
                            aLen = aLen + 1;
                        }
                    }
                    //Console.WriteLine("Length = [" + aLen + "]");
                    string[] keyStr = new string[aLen];

                    int idx = 0;
                    foreach (string str in keys)
                    {
                        if (str.Length >= 2)
                        {
                            if (!str.Substring(0, 2).Equals("##"))
                            {
                                keyStr[idx] = str;
                                idx = idx + 1;
                            }
                        }
                        else
                        {
                            keyStr[idx] = str;
                            idx = idx + 1;
                        }
                    }

                    //Console.WriteLine("Original Array");
                    for (int i = 0; i < keyStr.Length; i++)
                    {
                        Console.WriteLine(keyStr[i]);
                    }
                    for (int pass = 1; pass < keyStr.Length; pass++)
                    {
                        for (int i = 0; i < keyStr.Length - 1; i++)
                        {
                            if (keyStr[i].Length < keyStr[i + 1].Length)
                            {
                                string hold = keyStr[i];
                                keyStr[i] = keyStr[i + 1];
                                keyStr[i + 1] = hold;
                            }
                        }
                    }

                    //Console.WriteLine("New Array");
                    /*for (int i = 0; i < keyStr.Length; i++)
                    {
                        Console.WriteLine(keyStr[i]);
                    }*/
                    for (int i = 0; i < keyStr.Length; i++)
                    {
                        if (keyStr[i].IndexOf("_BARDATA_", System.StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            inFileString.Replace(keyStr[i], data[keyStr[i]]);
                        }
                        else
                        {
                            if (keyStr[i].IndexOf("_BARDATA_H_", StringComparison.OrdinalIgnoreCase) < 0)
                            {
                                string barCodeString = Get128BarCodeString(data[keyStr[i]], 0);
                                inFileString.Replace(keyStr[i], barCodeString);
                            }
                            else
                            {
                                string barCodeString = Get128BarCodeString(data[keyStr[i]], 2);
                                inFileString.Replace(keyStr[i], barCodeString);
                            }
                        }
                    }

                    char[] buf = inFileString.ToString().ToCharArray();
                    for (int k = 0; k < buf.Length; k++)
                    {
                        streamBinaryWriter.Write((byte)buf[k]);
                    }

                    streamBinaryWriter.Flush();
                    streamBinaryWriter.Close();
                    //streamBinaryReader.Close();
                    logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + " Done");
                    logStreamWriter.Flush();
                    logStreamWriter.Close();

                    return convertMessage;
                }
                catch (System.Exception ec)
                {
                    return "101 Convert Failed: " + "\n" + ec;
                }
            }
            return "102 Original File " + inFile + " Not Found";
        }

        /////////////////////////////////////////////////
        public static string SedReceipt(string inFile, string outFile, Hashtable data)
        {
            string convertMessage = null;
            try
            {
                Dictionary<string, string> data1 = new Dictionary<string, string>();
                data1 = Hashtable2DictionaryStrStr(data);
                convertMessage = SedReceipt(inFile, outFile, data1);
                return convertMessage;
            }
            catch (System.Exception e)
            {
                return "501 SedDocument Failed: " + "\n" + e.ToString();
            }
        }

        public static string SedReceipt(string inFile, string outFile, Dictionary<string, string> data)
        {
            string convertMessage = "0 SUCCESS";
            string logDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\";
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            string logFile = logDir + "SedDocument.log";
            FileStream logStream = File.OpenWrite(logFile);
            StreamWriter logStreamWriter = new StreamWriter(logStream);
            string timsS = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            logStreamWriter.WriteLine(timsS + " Start");
            logStreamWriter.WriteLine(timsS + " inFile = [" + inFile + "]");
            logStreamWriter.WriteLine(timsS + " outFile = [" + outFile + "]");
            logStreamWriter.WriteLine(timsS + " logFile = [" + logFile + "]");

            if (File.Exists(outFile))
            {
                FileInfo fileInfo = new FileInfo(outFile);
                DateTime writeDateTime = fileInfo.LastWriteTime;

                System.String timeStamp = writeDateTime.ToString("yyyyMMddHHmmssfff");
                System.String bakFileName = outFile + "." + timeStamp;
                try
                {
                    fileInfo.MoveTo(bakFileName);
                }
                catch (System.Exception)
                {
                    return "10 Can Not move File " + outFile + "to " + bakFileName;
                }
            }

            if (File.Exists(inFile))
            {
                try
                {
                    FileStream inStream = File.OpenRead(inFile);
                    FileStream outStream = File.OpenWrite(outFile);
                    BinaryReader streamBinaryReader = new BinaryReader(inStream);
                    BinaryWriter streamBinaryWriter = new BinaryWriter(outStream);

                    StringBuilder inFileString = new StringBuilder();
                    byte[] buffer = new Byte[1];
                    int bytesRead = 0;
                    bytesRead = streamBinaryReader.Read(buffer, 0, 1);
                    while (bytesRead > 0)
                    {
                        int c = buffer[0];
                        //Console.Write("[" + c + "] ");
                        inFileString.Append((char)c);
                        bytesRead = streamBinaryReader.Read(buffer, 0, 1);
                    }
                    streamBinaryReader.Close();
                    //Console.ReadLine();
                    int aLen = 0;
                    int dLen = 0;

                    ICollection<string> keys = data.Keys;

                    //long count = keys.LongCount;

                    foreach (string str in keys)
                    {
                        if (str.Length >= 2)
                        {
                            if (!str.Substring(0, 2).Equals("##") &&
                                !str.Substring(0, 6).Equals("DETAIL"))
                            {
                                aLen = aLen + 1;
                            }
                            if (str.Substring(0, 6).Equals("DETAIL"))
                            {
                                dLen = dLen + 1;
                            }
                        }
                        else
                        {
                            aLen = aLen + 1;
                        }
                    }
                    //Console.WriteLine("aLength = [" + aLen + "]");
                    string[] keyStr = new string[aLen];

                    //Console.WriteLine("dLength = [" + dLen + "]");
                    string[] detailStr = new string[dLen];

                    int idx = 0;
                    int idx2 = 0;
                    foreach (string str in keys)
                    {
                        //Console.WriteLine("Current String = [" + str + "]");
                        if (str.Length >= 2)
                        {
                            if (!str.Substring(0, 2).Equals("##") &&
                                !str.Substring(0, 6).Equals("DETAIL"))
                            {
                                keyStr[idx] = str;
                                idx = idx + 1;
                                //Console.WriteLine("Current String = [" + str + "] is key");
                            }
                            if (str.Substring(0, 6).Equals("DETAIL"))
                            {
                                detailStr[idx2] = str;
                                idx2 = idx2 + 1;
                                //Console.WriteLine("Current String = [" + str + "] is detail");
                            }
                        }
                        else
                        {
                            keyStr[idx] = str;
                            idx = idx + 1;
                        }
                    }

                    for (int pass = 1; pass < keyStr.Length; pass++)
                    {
                        for (int i = 0; i < keyStr.Length - 1; i++)
                        {
                            if (keyStr[i].Length < keyStr[i + 1].Length)
                            {
                                string hold = keyStr[i];
                                keyStr[i] = keyStr[i + 1];
                                keyStr[i + 1] = hold;
                            }
                        }
                    }
                    for (int pass = 1; pass < detailStr.Length; pass++)
                    {
                        for (int i = 0; i < detailStr.Length - 1; i++)
                        {
                            if (System.String.CompareOrdinal(detailStr[i], detailStr[i + 1]) > 0)
                            {                                
                                string hold = detailStr[i];
                                detailStr[i] = detailStr[i + 1];
                                detailStr[i + 1] = hold;
                            }
                        }
                    }
                    // Replace regular keys Begin
                    var customerEmployeeSignature = new Dictionary<string, string>();
                    for (int i = 0; i < keyStr.Length; i++)
                    {
                        if (keyStr[i].IndexOf("_BARDATA_", System.StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            if (keyStr[i].Contains("Customer_Employee_Signatures"))
                            {
                                customerEmployeeSignature.Add(keyStr[i], data[keyStr[i]]);
                            }
                            else
                            {
                                inFileString.Replace(keyStr[i], data[keyStr[i]]);
                            }
                        }
                        else
                        {
                            if (keyStr[i].IndexOf("_BARDATA_H_", System.StringComparison.OrdinalIgnoreCase) < 0)
                            {
                                string barCodeString = Get128BarCodeString(data[keyStr[i]], 0);
                                inFileString.Replace(keyStr[i], barCodeString);
                            }
                            else
                            {
                                string barCodeString = Get128BarCodeString(data[keyStr[i]], 2);
                                inFileString.Replace(keyStr[i], barCodeString);
                            }
                        }
                    }// Replace regular keys Done
                    StringBuilder signatureLines = new StringBuilder();
                    if (customerEmployeeSignature.Count > 0)
                    {
                        var sortedDict = (from entry in customerEmployeeSignature orderby entry.Key ascending select entry);
                        foreach (var signatureValue in sortedDict)
                        {
                            signatureLines.Append(GetEPSONprinterString(signatureValue.Value));
                        }
                        inFileString.Replace("Customer_Employee_Signatures", signatureLines.ToString());
                    }
                    else
                    {
                        inFileString.Replace("Customer_Employee_Signatures", string.Empty);
                    }

                    //Build Receipt_Detail_ITEMS Begin
                    StringBuilder ItemsBuffer = new StringBuilder();
                    for (int i = 0; i < detailStr.Length; i++)
                    {
                        string printerString = GetEPSONprinterString(data[detailStr[i]]);
                        ItemsBuffer.Append(printerString);
                    }
                    //GetEPSONprinterString
                    //Build Receipt_Detail_ITEMS Done

                    //Replace Receipt_Detail_ITEMS Begin
                    string itemsStr = ItemsBuffer.ToString();
                    inFileString.Replace("Receipt_Detail_ITEMS", itemsStr);
                    //Replace Receipt_Detail_ITEMS Done
                    //string toWrite;
                    char[] buf = inFileString.ToString().ToCharArray();
                    for (int k = 0; k < buf.Length; k++)
                    {
                        //toWrite = buf[k].ToString();
                        streamBinaryWriter.Write((byte)buf[k]);
                    }

                    streamBinaryWriter.Flush();
                    streamBinaryWriter.Close();
                    //streamBinaryReader.Close();
                    logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + " Done");
                    logStreamWriter.Flush();
                    logStreamWriter.Close();

                    return convertMessage;
                }
                catch (System.Exception ec)
                {
                    BasicExceptionHandler.Instance.AddException("SedReceipt failed", ec);
                    return "101 Convert Failed: " + "\n" + ec;
                }
            }
            return "102 Original File " + inFile + " Not Found";
        }

        /////////////////////////////////////////////////

        public static string Get128BarCodeString(string barCodeData, int flagOfHRI)
        {
            string paddedData;
            string barCodeString = null;
            int c = 0;
            string EMPTY_SPACE = System.String.Empty;
            string ESC = ((char)0x1b) + EMPTY_SPACE;
            string GS = ((char)0x1d) + EMPTY_SPACE;
            string CA_EXPR = ESC + ((char)0x61) + "" + ((char)0x1);
            string BARCODEHEIGHT = GS + ((char)0x68) + EMPTY_SPACE; //GS h n 1<= n <= 255
            string BARCODEWIDTH = GS + ((char)0x77) + EMPTY_SPACE;  //GS w n=2,3,4,5,6
            string PRINT128BARCODE = GS + "k" + ((char)73);//GS k m ONLY, need add n d1d2...dn
            string PRINTHRI = GS + "H"; //GS H n, n=0,1,2,3. 0 Not, 1 Abov, 2 Below, 3 both
            if (flagOfHRI < 0 || flagOfHRI > 3)
            {
                flagOfHRI = 0;
            }
            try
            {
                paddedData = barCodeData.PadLeft(14, '0');
                StringBuilder buffer = new StringBuilder();
                string printString1 = CA_EXPR +     // Center
                                      BARCODEHEIGHT + ((char)50) +    // Height
                                      BARCODEWIDTH + ((char)2) +      // Width
                                      PRINTHRI + ((char)flagOfHRI);   //GS H n
                buffer.Append(printString1);

                buffer.Append(PRINT128BARCODE);     // GS k m Pring Barcode Code128 m=73

                c = paddedData.Length + 2;         // n = 2 + length of data
                buffer.Append((char)c);             // n
                buffer.Append((char)123);           // d1
                buffer.Append((char)66);            // d2

                buffer.Append(paddedData);         // d3 to dn

                barCodeString = buffer.ToString();
                return barCodeString;
            }
            catch (System.Exception)
            {
                return barCodeString;
            }
        }

        //////////////////////////////////////////////////////////////
        public static string GetEPSONprinterString(string detailStr)
        {
            string printerString = null;

            string ONE_SPACE = " ";
            string EMPTY_SPACE = System.String.Empty;
            string ESC = ((char)0x1b) + EMPTY_SPACE;
            string LA_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
            //string CA_EXPR = ESC + ((char)0x61) + "" + ((char)0x1);
            //string RA_EXPR = ESC + ((char)0x61) + "" + ((char)0x2);
            string BOLD_EXPR = ESC + ((char)0x45) + "" + ((char)0x1);
            string BOLD_OFF_EXPR = ESC + ((char)0x45) + "" + ((char)0x0);
            //string RA_OFF_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
            string LF = ((char)0x0a) + EMPTY_SPACE;
            //string CR = ((char)0x0d) + EMPTY_SPACE;
            string CONSTANT_SINGLE_LINEBREAK = " -----------------------------------------";

            try
            {
                //StringBuilder buffer = new StringBuilder();
                if (detailStr.Substring(0, 3).Equals("<B>") ||
                    detailStr.Substring(0, 3).Equals("<b>"))
                {
                    printerString = (LA_EXPR + BOLD_EXPR + ONE_SPACE +
                                     detailStr.Substring(3, (detailStr.Length - 3)) + BOLD_OFF_EXPR + LF);
                    return printerString;
                }
                if (detailStr.Substring(0, 3).Equals("<R>") ||
                    detailStr.Substring(0, 3).Equals("<r>"))
                {
                    printerString = (LA_EXPR + ONE_SPACE +
                                     detailStr.Substring(3, (detailStr.Length - 3)) + LF);
                    return printerString;
                }
                if (detailStr.Substring(0, 3).Equals("<L>") ||
                    detailStr.Substring(0, 3).Equals("<l>"))
                {
                    printerString = (LA_EXPR + CONSTANT_SINGLE_LINEBREAK + LF);
                    return printerString;
                }
                if (detailStr.Substring(0, 3).Equals("<S>") ||
                    detailStr.Substring(0, 3).Equals("<s>"))
                {
                    printerString = (LA_EXPR + LF);
                    return printerString;
                }
                printerString = (LA_EXPR + detailStr + LF);
                return printerString;
            }
            catch (System.Exception)
            {
                return printerString;
            }
        }

        //////////////////////////////////////////////////////////////

        public static Hashtable DictionaryStrStr2Hashtable(Dictionary<string, string> data)
        {
            Hashtable data1 = new Hashtable();
            try
            {
                // Get a collection of the keys (names). 
                ICollection<string> c = data.Keys;

                foreach (string str in c)
                {
                    data1.Add(str, data[str]);
                }
                return data1;
            }
            catch (System.Exception)
            {
                return data1;
            }
        }

        public static Dictionary<string, string> Hashtable2DictionaryStrStr(Hashtable data)
        {
            Dictionary<string, string> data1 = new Dictionary<string, string>();
            try
            {
                //Loop through all items of a Hashtable
                IDictionaryEnumerator en = data.GetEnumerator();
                while (en.MoveNext())
                {
                    string strKey = en.Key.ToString();
                    string strValue = en.Value.ToString();
                    data1.Add(strKey, strValue);
                }
                return data1;
            }
            catch (System.Exception)
            {
                return data1;
            }
        }

        public static string CreateReceiptTemplateFile(string outFile)
        {
            string createMessage = "0 SUCCESS";
            string logDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\";
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            string logFile = logDir + "MyCreate.log";
            FileStream logStream = File.OpenWrite(logFile);
            StreamWriter logStreamWriter = new StreamWriter(logStream);
            string timsS = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
            logStreamWriter.WriteLine(timsS + " Start");

            if (File.Exists(outFile))
            {
                FileInfo fileInfo = new FileInfo(outFile);
                DateTime writeDateTime = fileInfo.LastWriteTime;

                System.String timeStamp = writeDateTime.ToString("yyyyMMddHHmmssfff");
                System.String bakFileName = outFile + "." + timeStamp;
                try
                {
                    fileInfo.MoveTo(bakFileName);
                }
                catch (System.Exception)
                {
                    return "10 Can Not move File " + outFile + "to " + bakFileName;
                }
            }

            //////////////////////////////////////////////////////////////
            string companyName = " Cash America International, Inc.";               //CA_
            string storeShortName = " store_short_name"; // Sample: DFW0001         //CA_
            string storeStreetAddr = " store_street_address";                       //CA_
            string storeCityStateZip = " store_city_state_zip";                     //CA_
            string storePhone = " store_phone";                                     //CA_
            string dateAndTime = " f_date_and_time"; // Format MM/DD/YYYY HH:MM PM/AM 
            string custName = " f_cust_name";
            //string maskedLoanNumber = "Loan Number: ****f_lnh_last4_ticket";
            string receiptNumber = " Receipt# receipt_number";                      //CA_
            //string formatedItem = "   1 Pawn Loan               f_lnh_amt";         //LA_
            string formatedItem = "Receipt_Detail_ITEMS";                           //LA_
            //string formatedfooter1 = "   Cash Paid To Customer     f_dir_csh";      //LA_
            //string formatedfooter2 = "   Paid by: Check            f_chk_amt";      //LA_
            //string formatedfooter3 = "   Check#: check_number";                     //LA_
            string disclaimer1 = " This Cash Disbursement Verification is";         //LA_
            string disclaimer2 = " a receipt only and is not meant to";            //LA_
            string disclaimer3 = " replace or alter the terms of any loan";         //LA_
            string disclaimer4 = " documents you may have received.";         //LA_
            string empNumber = " Employee: emp_number";                             //LA_
            //string formatedfooter5 = "_BARDATA_01"; // pawn loan number             //LA_
            //string s3 = " 123456789A123456789B123456789C123456789D12";

            string EMPTY_SPACE = "";
            string END_LINE = " ========================================";
            string CONSTANT_SINGLE_LINEBREAK = " -----------------------------------------";
            //string s2 = " 123456789A123456789B123456789C123456789D12";

            string ESC = ((char)0x1b) + EMPTY_SPACE;
            //string GS = ((char)0x1d) + EMPTY_SPACE;
            //string BARCODEHEIGHT = GS + ((char)0x68) + EMPTY_SPACE; //GS h n 1<= n <= 255
            //string BARCODEWIDTH = GS + ((char)0x77) + EMPTY_SPACE; //GS w n=2,3,4,5,6
            //string PRINT128BARCODE = GS + "k" + EMPTY_SPACE + ((char)73);//GS k m ONLY, need add n d1d2...dn
            string LF = ((char)0x0a) + EMPTY_SPACE;
            //string CR = ((char)0x0d) + EMPTY_SPACE;

            string LA_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
            string CA_EXPR = ESC + ((char)0x61) + "" + ((char)0x1);
            //string RA_EXPR = ESC + ((char)0x61) + "" + ((char)0x2);
            string CA_OFF_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
            //string BOLD_EXPR = ESC + ((char)0x45) + "" + ((char)0x1);
            //string BOLD_OFF_EXPR = ESC + ((char)0x45) + "" + ((char)0x0);
            //string RA_OFF_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);

            FileStream outStream = File.OpenWrite(outFile);
            StringBuilder buffer = new StringBuilder();
            BinaryWriter streamBinaryWriter = new BinaryWriter(outStream);

            buffer.Append(CA_EXPR + companyName + LF);
            buffer.Append(CA_EXPR + storeShortName + LF);
            buffer.Append(CA_EXPR + storeStreetAddr + LF);
            buffer.Append(CA_EXPR + storeCityStateZip + LF);
            buffer.Append(LF);
            buffer.Append(CA_EXPR + storePhone + LF);
            buffer.Append(CA_EXPR + dateAndTime + LF);
            buffer.Append(CA_EXPR + receiptNumber + LF);
            buffer.Append(LF);
            buffer.Append(CA_EXPR + custName + LF); //f_cust_name";
            buffer.Append(LF);
            buffer.Append(LA_EXPR + formatedItem + LF);
            buffer.Append(LF);
            buffer.Append(LA_EXPR + disclaimer1 + LF);
            buffer.Append(LA_EXPR + disclaimer2 + LF);
            buffer.Append(LA_EXPR + disclaimer3 + LF);
            buffer.Append(LA_EXPR + disclaimer4 + LF);
            buffer.Append(LF);
            buffer.Append(CA_EXPR + empNumber + CA_OFF_EXPR + LF);
            buffer.Append(LA_EXPR + CONSTANT_SINGLE_LINEBREAK + LF);
            buffer.Append(CA_EXPR + "_BARDATA_H_02" + LF); //Test printing with HRI(interpretation)
            buffer.Append(LA_EXPR + END_LINE + LF);
            buffer.Append(LF);
            buffer.Append(LF);
            buffer.Append(LF);
            buffer.Append(LF);
            buffer.Append(LF);
            buffer.Append(LF);
            buffer.Append(LF);

            char[] buf = buffer.ToString().ToCharArray();
            for (int k = 0; k < buf.Length; k++)
            {
                streamBinaryWriter.Write((byte)buf[k]);
            }
            streamBinaryWriter.Flush();
            streamBinaryWriter.Close();
            logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + " Done");
            logStreamWriter.Flush();
            logStreamWriter.Close();
            return createMessage;
            //////////////////////////////////////////////////////////////
        }

        /*public static void TestBarcodeCmdStr()
        {
        /////////////////////
        ///
        /// In Code 128 Bar code, There are Code A, Code B and Code C three sets.
        /// Code A contains ASCII 32 to 95  
        /// Code B contains ASCII 32 to 127
        /// Code C contains digits ONLY from 00 to 99
        /// In Code C if data is 234186, the data will be ((char)23) + ((char)41) + ((char)86)
        ///           if date is 23418,  the data will be ((char)02) + ((char)34) + ((char)18),
        ///           it will add a leading "0" in the bar code, if don't want this "0", using 
        ///           Code B or Code A. Also using Code C for "2341" then change to Code B or 
        ///           Code C for last digit "8"
        ///           ss = GS + "k" + ((char)73) +      // GS k m m = 73
        ///           ((char)7) +                       // n = 7
        ///           ((char)123) + ((char)67) +        // select(123), Code C(67)
        ///           ((char)23) + ((char)41) +         // 2341
        ///           ((char)123) + ((char)66) +        // select(123), Code B(66) or Code A(65)
        ///           "8"                               // 8
        /// 
        string tmpDir = @"C:\tmp\";
        string outFile = tmpDir + @"barcodetest.tpl";
        string printFile = outFile;
        string printerIP = "172.21.14.18";
        int printerPort = 9100;
        int howMany = 1;

        if (!Directory.Exists(tmpDir))
        Directory.CreateDirectory(tmpDir);

        StringBuilder buffer = new StringBuilder();

        string EMPTY_SPACE = "";
        string ESC = ((char)0x1b) + EMPTY_SPACE;
        string GS = ((char)0x1d) + EMPTY_SPACE;
        string BARCODEHEIGHT = GS + ((char)0x68) + EMPTY_SPACE; //GS h n 1<= n <= 255
        string BARCODEWIDTH = GS + ((char)0x77) + EMPTY_SPACE; //GS w n=2,3,4,5,6
        string PRINT128BARCODE = GS + "k" + EMPTY_SPACE + ((char)73);//GS k m ONLY, need add n d1d2...dn
        string LF = ((char)0x0a) + EMPTY_SPACE;
        string CR = ((char)0x0d) + EMPTY_SPACE;

        string LA_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
        string CA_EXPR = ESC + ((char)0x61) + "" + ((char)0x1);
        string RA_EXPR = ESC + ((char)0x61) + "" + ((char)0x2);
        string CA_OFF_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);
        string BOLD_EXPR = ESC + ((char)0x45) + "" + ((char)0x1);
        string BOLD_OFF_EXPR = ESC + ((char)0x45) + "" + ((char)0x0);
        string RA_OFF_EXPR = ESC + ((char)0x61) + "" + ((char)0x0);

        int c = 0;
        string barCodeData = "PAWN20145432";
        string ss = CA_EXPR +                   // Center
        BARCODEHEIGHT + ((char)60) +        // Height
        BARCODEWIDTH + ((char)2) +          // Width
        ((char)0x1d) + "H" + ((char)2) +    // Print HRI. 0 Not, 1 Above, 2 Below, 3 Both
        ((char)0x1d) + "k" + ((char)73);    // GS k m Pring Barcode Code128 m=73
        buffer.Append(ss);
        c = barCodeData.Length + 2;             // n = 2 + length of data
        //c = 12;
        buffer.Append((char)c);                 // n
        buffer.Append((char)123);               // d1
        buffer.Append((char)65);                // d2  65 CODE A, 66 CODE B, 67 CODE C
        buffer.Append(barCodeData);             // d3 to dn
        ss = EMPTY_SPACE + ((char)78) + ((char)111) + ((char)46) + ((char)123) + ((char)67) +
        ((char)12) + ((char)34) + ((char)123) + ((char)66) + "5";
        //buffer.Append(ss);

        // PRINT #1, gs$; "k"; CHR$(73); CHR$(10); CHR$(123); CHR$(66); CHR$(78); CHR$(111); 
        // CHR$(46); CHR$(123); CHR$(67); CHR$(12); CHR$(34); CHR$(56);

        buffer.Append(LF);
        buffer.Append(LF);
        buffer.Append(LF);
        //((char)14) +
        //((char)123) + ((char)66) + ((char)49) + ((char)51) + ((char)53) +
        //((char)50) + ((char)52) + ((char)54) + 
        //((char)55) + ((char)57) +
        //((char)50) + ((char)52) + ((char)54) +
        //((char)56);
        //((char)123) + ((char)67) + ((char)20) + ((char)14) + ((char)81) + 
        //((char)54) + ((char)32);

        //CA_EXPR + BARCODEHEIGHT + ((char)50) + BARCODEWIDTH + ((char)2) +
        //((char)0x1d) + "k" + ((char)73) + ((char)7) +
        //((char)123) + ((char)66) + ((char)49) + ((char)51) + ((char)53) +
        //((char)55) + ((char)57);
        //Esc  a    SOH GS   h     2    GS   w     STX GS   k     I(m)  n  {(d1) B(d2) 1   
        //[27] [97] [1] [29] [104] [50] [29] [119] [2] [29] [107] [73] [7] [123] [66] [49]
        // 3    5    7    9    LF   LF
        //[51] [53] [55] [57] [10] [10]
        //buffer.Append(ss+LF);
        buffer.Append(LF);

        /////////////// 

        FileStream outStream = File.OpenWrite(outFile);
        BinaryWriter binaryWriter = new BinaryWriter(outStream);

        char[] buf = buffer.ToString().ToCharArray();
        for (int k = 0; k < buf.Length; k++)
        {
        binaryWriter.Write((byte)buf[k]);
        }

        binaryWriter.Flush();
        binaryWriter.Close();
        string printMessage = printDocument(printFile, printerIP, printerPort, howMany);
        }*/

        public static string PutValueIntoDocument(string templateFile, string outFile, Hashtable data)
        {
            string convertMessage = "0 SUCCESS";
            string inFile = templateFile;

            if (File.Exists(outFile))
            {
                FileInfo fileInfo = new FileInfo(outFile);
                DateTime writeDateTime = fileInfo.LastWriteTime;

                System.String timeStamp = writeDateTime.ToString("yyyyMMddHHmmssffffff");
                System.String bakFileName = outFile + "." + timeStamp;
                try
                {
                    fileInfo.MoveTo(bakFileName);
                }
                catch (System.Exception)
                {
                    return "10 Can Not move File " + outFile + "to " + bakFileName;
                }
            }

            if (File.Exists(inFile))
            {
                try
                {
                    FileStream inStream = File.OpenRead(inFile);
                    FileStream outStream = File.OpenWrite(outFile);
                    BinaryReader streamBinaryReader = new BinaryReader(inStream);
                    BinaryWriter streamBinaryWriter = new BinaryWriter(outStream);

                    string logDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\";
                    
                    if (!Directory.Exists(logDir))
                        Directory.CreateDirectory(logDir);
                    string logFile = logDir + "DocPrintAndDisplay.log";
                    FileStream logStream = File.OpenWrite(logFile);
                    StreamWriter logStreamWriter = new StreamWriter(logStream);
                    logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + " Start");

                    //
                    //This section is for barcode print, in case Variable "104lnh_ticket" not be set value 
                    //
                    if ((data["lnh_ticket"] != null) && (data["104lnh_ticket"] == null))
                    {
                        string tmpStr = "104" + (string)data["lnh_ticket"];
                        data.Add("104lnh_ticket", tmpStr);
                    }

                    bool isVariable = false;
                    bool withKey = false;

                    StringBuilder variable = new StringBuilder();
                    byte[] buffer = new Byte[1];
                    int bytesRead = 0;
                    int i = 0;
                    int iVar = 0;

                    bytesRead = streamBinaryReader.Read(buffer, 0, 1);
                    while (bytesRead > 0)
                    {
                        int c = buffer[0];

                        //i++;
                        // [0-9] => 48-57, [A-Z] => 65-90, [_]= 95, [a-z] => 97-122

                        //if (i < 1000)
                        //{
                        //Console.WriteLine("[" + c + "]");
                        //}
                        //Console.Read();

                        if (c == '_')
                        {
                            withKey = true;
                        }
                        // [0-9] => 48-57, [A-Z] => 65-90, [_]= 95, [a-z] => 97-122

                        if ((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || c == '_' || (c >= 97 && c <= 122))
                        {
                            isVariable = true;
                            variable.Append((char)c);
                            //Console.WriteLine("Char = " + (char)c);
                            //Console.WriteLine("variable = " + variable);
                            //Console.Read();
                        }
                        else
                        {
                            if (isVariable)
                            {
                                //Console.WriteLine("");
                                string variableStr = variable.ToString();
                                if (withKey)
                                {
                                    i++;
                                    if (i < 100)
                                    {
                                        //Console.WriteLine("Var: " + variableStr);
                                    }
                                    //Console.Read();
                                    withKey = false;
                                    string value = (System.String)data[variableStr];
                                    if (value != null)
                                    {
                                        string dataValue = null;
                                        iVar++;
                                        //Console.WriteLine(iVar + " " + variableStr + " value = [" + value + "]");
                                        //Console.Read();
                                        /*logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yy HH:mm:ss.fff") +
                                        " [" + variableStr +
                                        "] = [" + value + "]");*/

                                        if (value.Length > 66) //75
                                        {
                                            value = value.Substring(0, 66);
                                        }
                                        //////////////////////////////////////
                                        /* */
                                        ///
                                        /// _BARDATA_nn and _BARDATA_H_nn are used for EPSON TM-T88IV
                                        /// Receipt priner. 
                                        /// _BARDATA_nn printer 128code format bar code without HRI
                                        /// _BARDATA_H_nn printer 128code format bar code with HRI(below)
                                        ///
                                        if (variableStr.IndexOf("_BARDATA_") < 0)
                                        {
                                            dataValue = value;
                                        }
                                        else
                                        {
                                            if (variableStr.IndexOf("_BARDATA_H_") < 0)
                                            {
                                                dataValue = Get128BarCodeString(value, 0);
                                            }
                                            else
                                            {
                                                dataValue = Get128BarCodeString(value, 2);
                                            }
                                        }
                                        /* */
                                        //////////////////////////////////////
                                        //char[] buf = value.ToCharArray();
                                        char[] buf = dataValue.ToCharArray();
                                        for (int k = 0; k < buf.Length; k++)
                                        {
                                            streamBinaryWriter.Write((byte)buf[k]);
                                        }
                                    }
                                    else
                                    {
                                        if (variableStr.Length > 2 && variableStr.Length < 15)
                                        {
                                            //Console.WriteLine("Variable [" + variableStr + "] not in data map");
                                            /*logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yy HH:mm:ss.fff") +
                                            " [" + variableStr + "] not found");*/
                                        }
                                        char[] buf = variableStr.ToCharArray();
                                        for (int k = 0; k < buf.Length; k++)
                                        {
                                            streamBinaryWriter.Write((byte)buf[k]);
                                        }
                                    }
                                }
                                else
                                {
                                    char[] buf = variableStr.ToCharArray();
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
                    logStreamWriter.WriteLine(DateTime.Now.ToString("MM/dd/yy HH:mm:ss.fff") + " Done");
                    logStreamWriter.Flush();
                    logStreamWriter.Close();

                    return convertMessage;
                }
                catch (System.Exception ec)
                {
                    //Console.WriteLine("Convert Failed: " + ec.ToString());
                    //Console.WriteLine("Press Enter to Exit.");
                    //Console.ReadLine();
                    return "11 Convert Failed: " + "\n" + ec.ToString();
                }
            }
            else
            {
                //Console.WriteLine("Convert Failed: Template Document " + inFile + " Not Found");
                //Console.WriteLine("Press Enter to Exit.");
                //Console.ReadLine();
                return "12 Template File " + inFile + " Not Found";
            }
        }

        public static string PutValueIntoDocument(string templateFile,
                                                  string outFile, Dictionary<string, string> data)
        {
            //Console.WriteLine("In PutValueIntoDocument Dictionary");
            //Console.ReadLine();
            string sMessage = null;
            try
            {
                Hashtable data1 = new Hashtable();
                data1 = DictionaryStrStr2Hashtable(data);
                sMessage = PutValueIntoDocument(templateFile, outFile, data1);
                return sMessage;
            }
            catch (System.Exception e)
            {
                //Console.WriteLine("PutValueIntoDocument Failed: " + e.ToString());
                //Console.WriteLine("Press Enter to Exit.");
                //Console.ReadLine();
                return "101 Convert Failed: " + "\n" + e.ToString();
            }
        }

        #region Raw Data Printing Setup
        /*
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr intPtr, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static void SendBytesToPrinter(string szPrinterName, string szPrintJobName, IntPtr intPtr, Int32 dwCount, out int iErrorCode)
        {
        iErrorCode = 0;
        Int32 dwWritten = 0;
        IntPtr hPrinter = new IntPtr(0);
        DOCINFOA di = new DOCINFOA();
        bool bSuccess = false;            // Assume failure unless you specifically succeed.

        di.pDocName = szPrintJobName;
        di.pDataType = "RAW";

        // Open the printer.
        if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
        {
        // Start a document.
        if (StartDocPrinter(hPrinter, 1, di))
        {
        // Start a page.
        if (StartPagePrinter(hPrinter))
        {
        // Write your bytes.
        bSuccess = WritePrinter(hPrinter, intPtr, dwCount, out dwWritten);
        EndPagePrinter(hPrinter);
        }
        EndDocPrinter(hPrinter);
        }
        ClosePrinter(hPrinter);
        }
        // If you did not succeed, GetLastError may give more information
        // about why not.
        if (bSuccess == false)
        {
        iErrorCode = Marshal.GetLastWin32Error();
        }
        }

        public static bool SendFileToPrinter(string szPrinterName, string szPrintJobName, string szFileName)
        {
        // Open the file.
        FileStream fs = new FileStream(szFileName, FileMode.Open);
        // Create a BinaryReader on the file.
        BinaryReader br = new BinaryReader(fs);
        // Dim an array of bytes big enough to hold the file's contents.
        Byte[] bytes = new Byte[fs.Length];
        // Unmanaged pointer.
        IntPtr pUnmanagedBytes = new IntPtr(0);
        int nLength;

        int ErrorCode = 0;

        nLength = Convert.ToInt32(fs.Length);
        // Read the contents of the file into the array.
        bytes = br.ReadBytes(nLength);
        // Allocate some unmanaged memory for those bytes.
        pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
        // Copy the managed byte array into the unmanaged array.
        Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
        // Send the unmanaged bytes to the printer.
        SendBytesToPrinter(szPrinterName, szPrintJobName, pUnmanagedBytes, nLength, out ErrorCode);
        // Free the unmanaged memory that you allocated earlier.
        Marshal.FreeCoTaskMem(pUnmanagedBytes);

        return ErrorCode > 0 ? false : true;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szPrintJobName, string szString)
        {
        IntPtr intPtr;
        Int32 dwCount;
        int ErrorCode = 0;
        // How many characters are in the string?
        dwCount = szString.Length;
        // Assume that the printer is expecting ANSI text, and then convert
        // the string to ANSI text.
        intPtr = Marshal.StringToCoTaskMemAnsi(szString);
        // Send the converted ANSI string to the printer.
        SendBytesToPrinter(szPrinterName, szPrintJobName, intPtr, dwCount, out ErrorCode);
        Marshal.FreeCoTaskMem(intPtr);
        return ErrorCode > 0 ? false : true;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
        [MarshalAs(UnmanagedType.LPStr)]
        public string pDocName;
        [MarshalAs(UnmanagedType.LPStr)]
        public string pOutputFile;
        [MarshalAs(UnmanagedType.LPStr)]
        public string pDataType;
        }*/
        #endregion

        public static bool SendASCIIStringToPrinter(string ipAddress, uint port, string data, out string returnString, bool pause = false)
        {
            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Printing [" + ipAddress+ ":" + port.ToString() + "] " + data + "...STARTED");
            }

            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    TcpClient tcpClient = new TcpClient(ipAddress, (int)port);
                    NetworkStream networkStream = tcpClient.GetStream();
                    BinaryWriter binaryWriter = new BinaryWriter(networkStream);
                    MemoryStream stringStream = new MemoryStream(Encoding.ASCII.GetBytes(data));
                    BinaryReader binaryReader = new BinaryReader(stringStream);
                    int inLength = (int)binaryReader.BaseStream.Length;
                    byte[] buffer = new byte[inLength];
                    buffer = binaryReader.ReadBytes(inLength);
                    binaryWriter.Write(buffer);
                    binaryWriter.Flush();
 
                    if (pause)
                    {
                        // Pause so we don't overload the printer
                        System.Threading.Thread.Sleep(1000);
                    }

                    binaryWriter.Close();
                    binaryReader.Close();
                    networkStream.Dispose();
                    tcpClient.Close();
                    returnString = "0 SUCCESS";
                }
                else
                {
                    returnString = "String passed is null or empty";
                    return (false);
                }
            }
            catch (SocketException ex)
            {
                returnString = "Printer Socket Error: IP is " + ipAddress + " PORT is " +
                               port + System.Environment.NewLine + "Exception: " + ex;

                if (FileLogger.Instance.IsLogDebug)
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, returnString);
                }

                return (false);
            }
            catch (System.Exception ep)
            {
                returnString = "Printer Socket Error: " + ep;

                if (FileLogger.Instance.IsLogDebug)
                {
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, returnString);
                }
                return (false);
            }

            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, null, "Printing string...FINISHED");
            }

            return (true);
        }

        public static void PrintBitmapDocument(Bitmap bitMap, DesktopSession desktopSession)
        {
            const string formName = "bitmap";

            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                desktopSession.LaserPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, "PrintingUtilities", "Printing bitmap document on {0}",
                                                   desktopSession.LaserPrinter);
                }
                MemoryStream memoryStream = new MemoryStream();
                bitMap.Save(memoryStream, ImageFormat.Bmp);
                memoryStream.Position = 0;
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                if (!GenerateDocumentsPrinter.printDocument(
                    binaryReader,
                    desktopSession.LaserPrinter.IPAddress,
                    desktopSession.LaserPrinter.Port, 
                    1))
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, "PrintingUtilities", "Failed to print bitmap document");
                    }
                }
            }
        }

        public static void PrintBitmapDocument(Bitmap bitMap, DesktopSession desktopSession, int howMany)
        {
            const string formName = "bitmap";

            if (howMany <= 0)
            {
                howMany = 1;
            }

            if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                desktopSession.LaserPrinter.IsValid)
            {
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, "PrintingUtilities", "Printing bitmap document {0} times on {1}", howMany,
                                                   desktopSession.LaserPrinter);
                }
                MemoryStream memoryStream = new MemoryStream();
                bitMap.Save(memoryStream, ImageFormat.Bmp);
                memoryStream.Position = 0;
                BinaryReader binaryReader = new BinaryReader(memoryStream);

                if (!GenerateDocumentsPrinter.printDocument(binaryReader,
                    desktopSession.LaserPrinter.IPAddress,
                    desktopSession.LaserPrinter.Port, howMany))
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, "PrintingUtilities", "Failed to print bitmap document");
                    }
                }
            }
        }
    }
}