using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Common.Controllers.Security;
using Common.Libraries.Objects;
using Common.Libraries.Utility.Logger;

namespace Common.Libraries.Utility.Shared
{
    public class PdfLauncher : MarshalByRefObject, IPdfLauncher
    {
        # region Singleton Code

        private static PdfLauncher pdfLauncher;
        private static readonly object padlock = new object();

        public static PdfLauncher Instance
        {
            get
            {
                lock (padlock)
                {
                    if (pdfLauncher == null)
                    {
                        pdfLauncher = new PdfLauncher();
                    }
                    return pdfLauncher;
                }
            }
        }

        static PdfLauncher()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        # endregion

        public void ShowPDFFile(string pdfFilePath, bool waitForExit)
        {
            var inFileName = new FileInfo(pdfFilePath);
            var confRef = SecurityAccessor.Instance.EncryptConfig;
            //FileInfo readerPath = new FileInfo(SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.AdobeReaderPath);
            var readerPath = new FileInfo(confRef.ClientConfig.GlobalConfiguration.AdobeReaderPath);
            if (!readerPath.Exists)
            {
                // Try Second Method
                readerPath = new FileInfo(SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.AdobeReaderPath);
            }
            /*
            if (!readerPath.Exists)
            {
                // Try third Method
                readerPath = new FileInfo("C:\\Program Files (x86)\\Adobe\\Reader 9.0\\Reader\\AcroRd32.exe");
            }*/
            if (readerPath.Exists)
            {
                if (inFileName.Exists)
                {
                    if (FileLogger.Instance != null && FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, "PdfLauncher", "Viewing PDF - FileName = " + inFileName + ", FullFileName = " + inFileName.FullName);
                    }
                    var procHandle = new Process
                    {
                        StartInfo =
                        {
                            FileName = readerPath.FullName,
                            CreateNoWindow = false,
                            Arguments = "\"" + inFileName.FullName + "\"",
                            WindowStyle = ProcessWindowStyle.Normal
                        }
                    };

                    if (waitForExit)
                    {
                        procHandle.Start();
                        procHandle.WaitForExit();
                        procHandle.Dispose();
                    }
                    else
                    {
                        procHandle.Start();
                    }
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, "PdfLauncher",
                                                       "-  Report " + readerPath.FullName + " has been viewed.");
                    }
                }
                else
                {
                    MessageBox.Show("Cannot show PDF file that does not exist");
                }
            }
            else
            {
                MessageBox.Show("Cannot find PDF viewer.");
            }
        }
    }
}
