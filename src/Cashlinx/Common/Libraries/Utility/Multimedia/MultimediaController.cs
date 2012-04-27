using System;
using System.Text;
using Common.Controllers.Security;
using Common.Libraries.Utility.Multimedia.VideoPlayer;

namespace Common.Libraries.Utility.Multimedia
{
    public sealed class MultimediaController : MarshalByRefObject, IDisposable
    {
        #region Static Singleton Objects
        /// <summary>
        /// Singleton instance variable
        /// </summary>
#if __MULTI__
        // ReSharper disable InconsistentNaming
        static readonly object mutexObj = new object();
        static readonly Dictionary<int, MultimediaController> multiInstance =
            new Dictionary<int, MultimediaController>();
        // ReSharper restore InconsistentNaming
#else
        static readonly MultimediaController instance = new MultimediaController();
#endif
        /// <summary>
        /// Static constructor - forces compiler to initialize the object prior to any code access
        /// </summary>
        static MultimediaController()
        {
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }


        /// <summary>
        /// Static instance property accessor
        /// </summary>
        public static MultimediaController Instance
        {
            get
            {
#if (!__MULTI__)
                return (instance);
#else
                lock (mutexObj)
                {
                    int tId = Thread.CurrentThread.ManagedThreadId;
                    if (multiInstance.ContainsKey(tId))
                    {
                        return (multiInstance[tId]);
                    }
                    var mulC = new MultimediaController();
                    multiInstance.Add(tId, mulC);
                    return (mulC);
                }
#endif
            }
        }
        #endregion

        public static readonly string BARCODE_VIDEO = @"Barcode_Printer_Alignment.wmv";
        public static readonly string AMP_VIDEO     = @"wmv_videos_test_720x480.wmv";
        public static readonly string CABLE_VIDEO   = @"cable_placement520.wmv";

        public enum VideoFile
        {
            BarCodeVideo,
            AmpVideo,
            CableVideo
        }

        private string videoDirectory;

        public MultimediaController()
        {
            this.videoDirectory = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseMediaPath;
        }

        private string getVideoFileName(VideoFile videoFile)
        {
            var sb = new StringBuilder();
            sb.Append(this.videoDirectory);
            sb.Append("\\");
            switch (videoFile)
            {
                case VideoFile.AmpVideo:
                    sb.Append(AMP_VIDEO);
                    break;
                case VideoFile.BarCodeVideo:
                    sb.Append(BARCODE_VIDEO);
                    break;
                case VideoFile.CableVideo:
                    sb.Append(CABLE_VIDEO);
                    break;
                default:
                    return (null);
            }
            return (sb.ToString());
        }

        public videoPlayerForm createVideoPlayer(VideoFile videoFile)
        {
            videoPlayerForm vidFm = null;
            string vidFileName = string.Empty;
            vidFileName = this.getVideoFileName(videoFile);
            vidFm = new videoPlayerForm(vidFileName);
            return (vidFm);
        }

        #region IDisposable Members

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
