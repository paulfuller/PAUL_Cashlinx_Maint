using System;
using System.Collections.Generic;
using System.Threading;
using Common.Libraries.Utility.Type;

namespace Common.Controllers.Network
{
    public sealed class MasterSlaveController : MarshalByRefObject
    {
        #region Singleton Fields And Methods
        //Singleton instance variable
        static readonly MasterSlaveController instance = new MasterSlaveController();
        static readonly object mutexObj = new object();

        /// <summary>
        /// Static constructor - forces compiler to initialize the object prior to any code access
        /// </summary>
        static MasterSlaveController()
        {
        }

        /// <summary>
        /// Override this method to ensure it is not destroyed until the end
        /// of the applications execution life
        /// </summary>
        /// <returns>Null object</returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }

        /// <summary>
        /// Singleton instance accessor
        /// </summary>
        public MasterSlaveController Instance
        {
            get { return (instance); }
        }
        #endregion

        #region Enumerations / Constants / Delegate Definitions

        public enum ControllerMode
        {
            MASTER,
            SLAVE
        }

        public delegate bool InMsgConsumeFxn(
            object msgSrc, object msgObject, params object[] msgParams);

        public delegate bool OutMsgReceiveFxn(
                object msgDest, object msgObject, params object[] msgParams);

        public delegate bool CxnMonitorFxn(object cxnObject);

        #endregion

        //Mode of controller
        private ControllerMode controlMode;

        //Thread fields
        private Thread messageMonitor;
        private Thread connectionMonitor;

        //Message queues
        private static List<TupleType<object, object, object[]>> inboundMessageQueue;
        private static List<TupleType<object, object, object[]>> outboundMessageQueue;

        //Network (socket) connections
        private static List<SlaveConnection> slaveConnections;

        /// <summary>
        /// Controller constructor
        /// </summary>
        public MasterSlaveController()
        {
            
        }
    }
}
