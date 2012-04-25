using System.Collections.Generic;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Config;

namespace PawnStoreSetupTool.Data
{
    internal class PawnStoreSetupData
    {
        public const int DEFAULT_PERIPHERAL_SZ = 6;
        //Store data needed for setup
        public SiteId StoreData;
        public List<ShopCalendarVO> StoreCalendar;
        public List<UserVO> StoreUsers;
        public CashDrawerVO StoreSafe;
        public List<CashDrawerVO> CashDrawers;
        public List<CashDrawerUserVO> CashDrawerUsers;
        public List<EsbServiceVO> EsbServices;
        public List<DatabaseServiceVO> DbServices;
        public List<WorkstationVO> Workstations;
        public List<PeripheralVO> Peripherals;
        public List<PeripheralTypeVO> PeripheralTypes;
        public List<PeripheralModelVO> PeripheralModelTypes;
 
        public PawnStoreSetupData()
        {
            StoreData = new SiteId();
            StoreCalendar = new List<ShopCalendarVO>();
            StoreUsers = new List<UserVO>();
            StoreSafe = new CashDrawerVO();
            CashDrawers = new List<CashDrawerVO>();
            CashDrawerUsers = new List<CashDrawerUserVO>();
            EsbServices = new List<EsbServiceVO>();
            DbServices = new List<DatabaseServiceVO>();
            Workstations = new List<WorkstationVO>();
            Peripherals = new List<PeripheralVO>();
            PeripheralTypes = new List<PeripheralTypeVO>(DEFAULT_PERIPHERAL_SZ);
            PeripheralModelTypes = new List<PeripheralModelVO>(DEFAULT_PERIPHERAL_SZ);
        }

    }
}
