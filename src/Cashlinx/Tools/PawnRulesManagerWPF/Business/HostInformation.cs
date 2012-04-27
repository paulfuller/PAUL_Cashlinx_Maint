using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Common.Libraries.Utility.Logger;

namespace PawnRulesManagerWPF.Business
{
    #region Host Information - DO NOT MODIFY
    class HostInformation
    {
        #region Class Fields
        byte[] _address;
        public string IPAddress;
        public string MACAddress;
        private const int ETHERNET_ADAPTER_TYPE = 6;

        #endregion

        #region Constructor
        public HostInformation(byte[] b, AdapterInfo info)
        {
            if (b == null)
                throw new ArgumentNullException("b");
            if (b.Length != 8)
                throw new ArgumentOutOfRangeException("b");
            _address = new byte[b.Length];
            Array.Copy(b, _address, b.Length);
        }

        public HostInformation(TempFileLogger tLogger)
        {
            IPAddress = String.Empty;
            MACAddress = String.Empty;

            try
            {
                List<AdapterInfo> information = GetAdapterInformation();

                foreach (AdapterInfo info in information)
                {
                    if (info.Type == ETHERNET_ADAPTER_TYPE)
                    {
                        IPAddress = info.IPAddressList.IPAddressString;
                        MACAddress = info.Address[0].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) +
                                     ":" +
                                     info.Address[1].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) +
                                     ":" +
                                     info.Address[2].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) +
                                     ":" +
                                     info.Address[3].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) +
                                     ":" +
                                     info.Address[4].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) +
                                     ":" +
                                     info.Address[5].ToString("X2", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception e)
            {
                if (tLogger != null && tLogger.IsInitialized)
                {
                    tLogger.logMessage(LogLevel.FATAL, this, "Cannot retrieve IP address and/or MAC address: {0} - {1}", e.Message, e);
                }
                throw new Exception("Cannot retrieve MAC address or IP address");
            }
        }
        #endregion

        #region Public Fields
        public byte[] Address { get { return _address; } }
        #endregion

        #region Overloads
        public override string ToString()
        {
            return Address[0].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) + ":" +
                    Address[1].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) + ":" +
                    Address[2].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) + ":" +
                    Address[3].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) + ":" +
                    Address[4].ToString("X2", System.Globalization.CultureInfo.InvariantCulture) + ":" +
                    Address[5].ToString("X2", System.Globalization.CultureInfo.InvariantCulture);
        }
        #endregion

        #region Public Methods
        public List<HostInformation> GetMacAddresses()
        {
            int size = 0;
            
            int r = GetAdaptersInfo(null, ref size);
            if ((r != IPConfigConst.ERROR_SUCCESS) && (r != IPConfigConst.ERROR_BUFFER_OVERFLOW))
            {
                return null;
            }
            Byte[] buffer = new Byte[size];
            r = GetAdaptersInfo(buffer, ref size);
            if (r != IPConfigConst.ERROR_SUCCESS)
            {
                return null;
            }
            AdapterInfo Adapter = new AdapterInfo();
            
            ByteArray_To_IPAdapterInfo(ref Adapter, buffer, Marshal.SizeOf(Adapter));

            List<HostInformation> addresses = new List<HostInformation>();
            do
            {
                addresses.Add(new HostInformation(Adapter.Address, Adapter));
                IntPtr p = Adapter.NextPointer;
                if (p != IntPtr.Zero)
                {
                    IntPtr_To_IPAdapterInfo(ref Adapter, p, Marshal.SizeOf(Adapter));
                }
                else
                {
                    break;
                }
            } while (true);

            return addresses;
        }

        public List<AdapterInfo> GetAdapterInformation()
        {
            int size = 0;

            int r = GetAdaptersInfo(null, ref size);
            if ((r != IPConfigConst.ERROR_SUCCESS) && (r != IPConfigConst.ERROR_BUFFER_OVERFLOW))
            {
                return null;
            }
            Byte[] buffer = new Byte[size];
            r = GetAdaptersInfo(buffer, ref size);
            if (r != IPConfigConst.ERROR_SUCCESS)
            {
                return null;
            }
            AdapterInfo Adapter = new AdapterInfo();

            ByteArray_To_IPAdapterInfo(ref Adapter, buffer, Marshal.SizeOf(Adapter));

            List<AdapterInfo> adapters = new List<AdapterInfo>();
            do
            {
                IntPtr p = Adapter.NextPointer;
                if (p != IntPtr.Zero)
                {
                    IntPtr_To_IPAdapterInfo(ref Adapter, p, Marshal.SizeOf(Adapter));
                    adapters.Add(Adapter);
                }
                else
                {
                    break;
                }
            } while (true);

            return adapters;
        }
        #endregion

        #region Win32 Mapping - DO NOT MODIFY
        // glue definitions into windows
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct IPAddrString
        {
            public IntPtr NextPointer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 * 4)]
            public String IPAddressString;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4 * 4)]
            public String IPMaskString;
            public int Context;
        }

        public class IPConfigConst
        {
            public const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;
            public const int MAX_ADAPTER_NAME_LENGTH = 256;
            public const int MAX_ADAPTER_ADDRESS_LENGTH = 8;
            public const int ERROR_BUFFER_OVERFLOW = 111;
            public const int ERROR_SUCCESS = 0;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct AdapterInfo
        {
            public IntPtr NextPointer;
            public int ComboIndex;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = IPConfigConst.MAX_ADAPTER_NAME_LENGTH + 4)]
            public string AdapterName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = IPConfigConst.MAX_ADAPTER_DESCRIPTION_LENGTH + 4)]
            public string Description;
            public int AddressLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = IPConfigConst.MAX_ADAPTER_ADDRESS_LENGTH)]
            public Byte[] Address;
            public int Index;
            public int Type;
            public int DhcpEnabled;
            public IntPtr CurrentIPAddress;
            public IPAddrString IPAddressList;
            public IPAddrString GatewayList;
            public IPAddrString DhcpServer;
            public Boolean HaveWins;
            public IPAddrString PrimaryWinsServer;
            public IPAddrString SecondaryWinsServer;
            public int LeaseObtained;
            public int LeaseExpires;
        }
        [DllImport("Iphlpapi.dll", CharSet = CharSet.Auto)]
        private static extern int GetAdaptersInfo(Byte[] PAdapterInfoBuffer, ref int size);
        [DllImport("Kernel32.dll", EntryPoint = "CopyMemory")]
        private static extern void ByteArray_To_IPAdapterInfo(ref AdapterInfo dst, Byte[] src, int size);
        [DllImport("Kernel32.dll", EntryPoint = "CopyMemory")]
        private static extern void IntPtr_To_IPAdapterInfo(ref AdapterInfo dst, IntPtr src, int size);
        #endregion
    }
    #endregion
}
