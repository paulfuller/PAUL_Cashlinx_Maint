using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Audit.Logic
{
    public class FtpHelper
    {
        public FtpHelper(string host, string userName, string password)
        {
            Host = host;
            UserName = userName;
            Password = password;
        }

        public string Host { get; private set; }
        public string Password { get; private set; }
        public string UserName { get; private set; }
        public bool UseSsl { get; set; }

        public FtpStatusCode DownloadFile(string remotePath, string localPath, bool useBinary)
        {
            Uri uri = CreateUri(remotePath);

            FileStream fileStream = new FileStream(localPath, FileMode.Create);
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = useBinary;
            request.Credentials = CreateNetworkCredentials();
            request.EnableSsl = UseSsl;

            ServicePointManager.ServerCertificateValidationCallback = delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true;
            };

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            stream.CopyTo(fileStream);
            FtpStatusCode statusCode = response.StatusCode;
            response.Close();
            fileStream.Close();

            return statusCode;
        }

        public FtpStatusCode UploadFile(string remotePath, string localPath, bool useBinary)
        {
            Uri uri = CreateUri(remotePath);


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = useBinary;
            request.Credentials = CreateNetworkCredentials();
            request.EnableSsl = UseSsl;

            FileStream stream = File.OpenRead(localPath);
            Stream requestStream = request.GetRequestStream();
            stream.CopyTo(requestStream);

            requestStream.Close();

            ServicePointManager.ServerCertificateValidationCallback = delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true;
            };

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            FtpStatusCode statusCode = response.StatusCode;
            response.Close();
            stream.Close();

            return statusCode;
        }

        private Uri CreateUri(string remotePath)
        {
            UriBuilder builder = new UriBuilder();
            builder.Scheme = Uri.UriSchemeFtp;
            builder.Host = Host;
            builder.Path = remotePath;

            return builder.Uri;
        }

        private ICredentials CreateNetworkCredentials()
        {
            return new NetworkCredential(UserName, Password);
        }
    }
}
