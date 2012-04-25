
namespace Common.Libraries.Objects
{
    public interface IPdfLauncher
    {
        void ShowPDFFile(string pdfFilePath, bool waitForExit);
    }
}
