using System;
using System.Net.Sockets;
using Common.Libraries.Utility.Logger;

namespace Common.Controllers.Network
{
    public class SlaveConnection : IDisposable
    {
        //Connection to the MasterSlaveController listening on the slave machine
        public const string NAME = "SlaveConnection";
        public const int PORTMIN = 0;
        public const int PORTMAX = 65535;
        public TcpClient SlaveCxn { private set; get; }
        public string MachineName { private set; get; }
        public string SlaveCxnMachineName { private set; get; }
        public int SlaveCxnMachinePort { private set; get; }
        public bool Connected { private set; get; }
        public FileLogger SlaveCxnLogger { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        private void logMsg(LogLevel level, string msg)
        {
            if (this.SlaveCxnLogger == null ||
                string.IsNullOrEmpty(msg))
            {
                return;
            }
            try
            {
                SlaveCxnLogger.logMessage(level, NAME, msg);
            }
            catch(Exception eX)
            {
                //Do nothing
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        /// <param name="vars"></param>
        private void logMsg(LogLevel level, string msg, params object[] vars)
        {
            if (this.SlaveCxnLogger == null || 
                vars == null || vars.Length <= 0 ||
                string.IsNullOrEmpty(msg))
            {
                return;
            }

            try
            {
                SlaveCxnLogger.logMessage(level, NAME, msg, vars);
            }
            catch (Exception eX)
            {
                //Do nothing
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisMacName"></param>
        /// <param name="cxnMacName"></param>
        /// <param name="cxnMacPort"></param>
        public SlaveConnection(string thisMacName, string cxnMacName, int cxnMacPort)
        {
            this.SlaveCxnLogger = null;
            this.SlaveCxnMachineName = cxnMacName;
            this.SlaveCxnMachinePort = cxnMacPort;
            this.MachineName = thisMacName;
            this.Connected = false;
            this.SlaveCxn = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            //If already initialized, return success
            if (this.Connected) 
            {
                return (true);
            }

            if (string.IsNullOrEmpty(this.SlaveCxnMachineName) ||
                this.SlaveCxnMachinePort < PORTMIN || this.SlaveCxnMachinePort > PORTMAX)
            {
                logMsg(LogLevel.ERROR, "Connection parameters are invalid.");
                return (false);
            }

            try
            {
                //Creating a connection to the MasterSlaveController listening on the slave machine
                this.SlaveCxn = new TcpClient(this.SlaveCxnMachineName, this.SlaveCxnMachinePort);
                logMsg(LogLevel.INFO,
                       "Creating connection from this machine ({0}) to ({1}:{2})...",
                       this.MachineName,
                       this.SlaveCxnMachineName,
                       this.SlaveCxnMachinePort);
                    
                if (this.SlaveCxn.Connected)
                {
                    this.Connected = true;
                    logMsg(LogLevel.INFO, "Connection to slave machine successful");
                }
            }
            catch (Exception eX)
            {
                logMsg(LogLevel.ERROR,
                       "Exception thrown when connecting - {0} Source: {1}",
                       eX.ToString(),
                       eX.Source);
                return (false);
            }
            

            return (this.Connected);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            if (!this.Connected)
            {
                if (this.SlaveCxn != null)
                {
                    this.SlaveCxn.Close();
                }
                this.SlaveCxn = null;
            }
            else
            {
                if (this.SlaveCxn != null)
                {
                    //Disconnect with the option to reuse the socket set to false
                    this.SlaveCxn.Close();
                    this.SlaveCxn = null;
                    this.Connected = false;
                }
                this.Connected = false;
                this.SlaveCxn = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Disconnect();
        }
    }
}
