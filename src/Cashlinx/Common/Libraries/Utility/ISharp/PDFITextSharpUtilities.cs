using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Logger;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Common.Libraries.Utility.ISharp
{
    public static class PDFITextSharpUtilities
    {
        /// <summary>
        /// Hide Pdf sharp library from outside
        /// with internalized class fields
        /// </summary>
        public class PdfSharpTools : IDisposable
        {
            public enum Justification : int
            {
                LEFT = 0,
                CENTER = 1,
                RIGHT = 2
            }

            internal PdfReader Reader { private set; get; }
            internal PdfStamper Stamper { private set; get; }
            internal StreamWriter StamperOutput { private set; get; }

            public bool Initialized { private set; get; }
            public bool ReadyForRead { private set; get; }
            public bool ReadyForWrite { private set; get; }

            public string InputFileName { private set; get; }
            public string OutputFileName { private set; get; }

            /// <summary>
            /// Ctor that prepares for reads only
            /// </summary>
            /// <param name="inputFileName"></param>
            public PdfSharpTools(string inputFileName)
            {
                this.Initialized = false;
                this.ReadyForRead = false;
                this.ReadyForWrite = false;

                if (string.IsNullOrEmpty(inputFileName) ||
                    !File.Exists(@inputFileName))
                {
                    return;
                }

                this.InputFileName = inputFileName;
                this.Initialized = true;

                //Create reader
                this.Reader = new PdfReader(@inputFileName);
                this.ReadyForRead = true;
            }

            /// <summary>
            /// Prepare internals for pdf stamping
            /// </summary>
            /// <param name="outputFileName"></param>
            public void PrepForStamping(string outputFileName)
            {
                if (!this.Initialized || !this.ReadyForRead || this.ReadyForWrite)
                {
                    return;
                }

                try
                {
                    //Create file output stream
                    this.StamperOutput = new StreamWriter(@outputFileName);

                    //Create stamper
                    this.Stamper = new PdfStamper(this.Reader, this.StamperOutput.BaseStream);

                    //Set proper flags
                    this.ReadyForWrite = true;

                    //Set output file name
                    this.OutputFileName = outputFileName;
                }
                catch
                {
                    //Cannot write with this output file, do not set flags
                    return;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="fieldData"></param>
            /// <param name="flattenOutput"></param>
            /// <returns></returns>
            public bool StampPdfOutputFile(
                Dictionary<string, string> fieldData,
                bool flattenOutput)
            {
                if (!this.Initialized || !this.ReadyForRead ||
                    !this.ReadyForWrite ||
                    CollectionUtilities.isEmpty(fieldData))
                {
                    return (false);
                }

                this.Stamper.FormFlattening = flattenOutput;

                //Get fields to stamp
                AcroFields fields = this.Stamper.AcroFields;

                //Loop through each dictionary entry and set fields
                foreach (string curKey in fieldData.Keys)
                {
                    if (string.IsNullOrEmpty(curKey))
                        continue;
                    string keyVal = fieldData[curKey];

                    //Set field
                    bool fieldSet = fields.SetField(curKey, keyVal);

                    if (!fieldSet && FileLogger.Instance.IsLogDebug)
                    {
                        FileLogger.Instance.logMessage(
                            LogLevel.WARN,
                            this,
                            "Could not set field {0} with value {1} in PDF {2}",
                            curKey,
                            keyVal,
                            this.OutputFileName);
                    }
                }

                //Close the stamper
                //this.Stamper.FormFlattening = true;
                this.Stamper.Close();
                this.StamperOutput = null;
                this.ReadyForWrite = false;
                return (true);
            }


            #region Implementation of IDisposable

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            public void Dispose()
            {
                if (this.StamperOutput != null)
                {
                    this.StamperOutput.Flush();
                    this.StamperOutput.Close();
                    this.StamperOutput.Dispose();
                    this.StamperOutput = null;
                }

                //Clear all fields
                this.Stamper = null;
                this.Reader = null;
                this.InputFileName = null;
                this.OutputFileName = null;
                this.Initialized = false;
                this.ReadyForRead = false;
                this.ReadyForWrite = false;
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="tools"></param>
        public static void OpenPDFFile(string fileName, out PdfSharpTools tools)
        {
            //Reset output object
            tools = null;

            //Create tools
            tools = new PdfSharpTools(fileName);
        }

        /// <summary>
        /// Merges multiple pdf files into one file
        /// </summary>
        /// <param name="destinationFile">The file to be created after the merge is complete.</param>
        /// <param name="sourceFiles">String array of all the files to be merged.</param>
        /// <returns></returns>
        public static bool MergePDFFiles(string destinationFile, string[] sourceFiles)
        {
            var wasPDFMergeSuccessful = false;

            try
            {
                var f = 0;
                var outFile = destinationFile;

                Document document = null;
                PdfCopy writer = null;
                
                while (f < sourceFiles.Length)
                {
                    // Create a reader for a certain document
                    var reader = new PdfReader(sourceFiles[f]);

                    // Retrieve the total number of pages
                    var n = reader.NumberOfPages;

                    //Trace.WriteLine("There are " + n + " pages in " + sourceFiles[f]);
                    if (f == 0)
                    {
                        // Step 1: Creation of a document-object
                        document = new Document(reader.GetPageSizeWithRotation(1));

                        // Step 2: Create a writer that listens to the document
                        writer = new PdfCopy(document, new FileStream(outFile, FileMode.Create));

                        // Step 3: Open the document
                        document.Open();
                    }

                    // Step 4: Add content
                    for (var i = 0; i < n; )
                    {
                        ++i;

                        var page = writer.GetImportedPage(reader, i);
                        
                        writer.AddPage(page);
                    }

                    var form = reader.AcroForm;

                    if (form != null)
                        writer.CopyAcroForm(reader);

                    f++;
                }

                // Step 5: Close the document
                document.Close();

                wasPDFMergeSuccessful = true;
            }
            catch (System.Exception exception)
            {
                //handle exception
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "PDFITextSharpUtilities",
                        "Exception thrown while merging PDF files",
                        exception);
                }

                wasPDFMergeSuccessful = false;
            }

            return wasPDFMergeSuccessful;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tools"></param>
        /// <param name="outputFileName"></param>
        /// <param name="flattenForm"></param>
        /// <param name="fieldData"></param>
        /// <returns></returns>
        public static bool StampSimplePDFWithFormFields(
            PdfSharpTools tools,
            string outputFileName, bool flattenForm,
            Dictionary<string, string> fieldData)
        {
            if (tools == null)
                return (false);
            //Prepare the tools for stamping
            tools.PrepForStamping(outputFileName);

            //Ensure that the tools are ready
            if (tools.ReadyForWrite)
            {
                //Loop through the dictionary
                if (!tools.StampPdfOutputFile(fieldData, flattenForm))
                {
                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR, "PDFITextSharpUtilities", "Error occurred while stamping pdf file {0} from input file {1}", tools.OutputFileName, tools.InputFileName);
                    }
                    return (false);
                }
            }

            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="ipPort"></param>
        /// <param name="numberCopies"></param>
        /// <param name="tools"></param>
        /// <returns></returns>
        public static bool PrintOutputPDFFile(string ipAddress, string ipPort, int numberCopies, PdfSharpTools tools)
        {
            if (tools == null ||
                string.IsNullOrEmpty(ipAddress) ||
                string.IsNullOrEmpty(ipPort))
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "PDFITextSharpUtilities",
                        "Cannot print PDF file {0} to {1}:{2}",
                        ((tools == null) ? "null" :
                        ((string.IsNullOrEmpty(tools.OutputFileName) ? "null" : tools.OutputFileName))),
                        (string.IsNullOrEmpty(ipAddress) ? "null" : ipAddress),
                        (string.IsNullOrEmpty(ipPort) ? "null" : ipPort));

                }
                return (false);
            }

            //Correct number copies if less than one
            if (numberCopies < 1)
            {
                if (FileLogger.Instance.IsLogWarn)
                {
                    FileLogger.Instance.logMessage(LogLevel.WARN, "PDFITextSharpUtilities",
                        "Number of copies specified is invalid. Changed number of copies to 1 instead of {0}",
                        numberCopies);
                }
                numberCopies = 1;
            }

            //Validate port number
            int ipPortNum = Utilities.GetIntegerValue(ipPort);
            if (ipPortNum < 0 || ipPortNum > 65535)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "PDFITextSharpUtilities",
                        "IP Port Invalid(Must be a number between 0 and 65535.  Cannot print {0} to {1}:{2}",
                        tools.OutputFileName, ipAddress, ipPort);
                }
                return (false);
            }
            try
            {
                //Open printer socket
                var tcpClient = new TcpClient(ipAddress, ipPortNum);
                NetworkStream networkStream = tcpClient.GetStream();
                var binaryWriter = new BinaryWriter(networkStream);

                //Read entire data file into the byte buffer
                FileStream inStream = File.OpenRead(tools.OutputFileName);
                var binaryReader = new BinaryReader(inStream);
                var inLength = (int)binaryReader.BaseStream.Length;
                byte[] buffer = binaryReader.ReadBytes(inLength);

                //Repeat the binary write operation for as many copies as requested
                for (int i = 1; i <= numberCopies; ++i)
                {
                    binaryWriter.Write(buffer);
                    binaryWriter.Flush();
                }

                //Close binary streams
                binaryWriter.Close();
                binaryReader.Close();
            }
            catch (System.Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "PDFITextSharpUtilities",
                        "Exception thrown while printing {0} to {1}:{2} :: {3}",
                        tools.OutputFileName,
                        ipAddress, ipPort,
                        eX);
                }
                return (false);
            }

            return (true);
        }

        /// <summary>
        /// Generates a blank PdfPCell object that spans multiple columns, forming a blank line of text
        /// </summary>
        /// <param name="colSpan"></param>
        /// <returns></returns>
        public static PdfPCell GenerateBlankLine(int colSpan)
        {
            var blankLineCell = new PdfPCell(new Phrase(" "));
            blankLineCell.Colspan = colSpan;
            blankLineCell.Border = PdfPCell.NO_BORDER;
            return (blankLineCell);
        }

        /// <summary>
        /// Generates a graphical line by utilizing the border settings of blank text cells
        /// </summary>
        /// <param name="lineColor">Color of the line</param>
        /// <param name="colSpan">Number of columns to span</param>
        /// <param name="lineHeight">Point height of the </param>
        /// <returns></returns>
        public static PdfPCell GenerateLine(BaseColor lineColor, int colSpan, float lineHeight)
        {
            var lineCell = new PdfPCell(new Phrase(" "));
            if (lineHeight < 0.1f)
                lineHeight = 0.1f;
            lineCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            lineCell.Colspan = colSpan;
            lineCell.FixedHeight = 1.0f;
            lineCell.Border = PdfPCell.TOP_BORDER;
            lineCell.BorderColorTop = lineColor;
            lineCell.BorderWidthTop = lineHeight;
            return (lineCell);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colSpan"></param>
        /// <returns></returns>
        public static PdfPCell GenerateBlankCell(int colSpan)
        {
            //Default blank cell
            var blankCell = new PdfPCell(new Phrase(string.Empty));
            blankCell.Colspan = colSpan;
            blankCell.Border = PdfPCell.NO_BORDER;
            blankCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            blankCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            return (blankCell);
        }

        /// <summary>
        /// Sets majority of default data for cells
        /// </summary>
        /// <param name="cell">Reference to cell being modified</param>
        /// <param name="colSpan">Number of columns to span</param>
        /// <param name="align">Alignment value (0 = left, 1 = center, 2 = right)</param>
        public static void SetDefaultCellStyle(ref PdfPCell cell, int colSpan, int align)
        {
            if (cell == null)
                return;
            cell.HorizontalAlignment =
                ((align == (int)PdfSharpTools.Justification.LEFT) ? PdfPCell.ALIGN_LEFT :
                ((align == (int)PdfSharpTools.Justification.CENTER) ? PdfPCell.ALIGN_CENTER : PdfPCell.ALIGN_RIGHT));
            cell.Border = PdfPCell.NO_BORDER;
            cell.Colspan = colSpan;
        }

        /// <summary>
        /// Generates a text cell with a specified font
        /// </summary>
        /// <param name="text">Text to display in the cell</param>
        /// <param name="f">Font to use in displaying the text</param>
        /// <returns></returns>
        public static PdfPCell GenerateTextCell(string text, Font f)
        {
            var newCell = new PdfPCell(new Phrase(text, f));
            return (newCell);
        }
    }
}
