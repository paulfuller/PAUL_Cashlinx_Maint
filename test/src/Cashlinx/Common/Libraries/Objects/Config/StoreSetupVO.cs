using System.Collections.Generic;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Type;

namespace Common.Libraries.Objects.Config
{
    public class StoreSetupVO
    {
        public SiteId StoreInfo;
        //All available resources for a given store
        public List<CashDrawerVO> AllCashDrawers;
        public List<CashDrawerUserVO> AllCashDrawerUsers;
        public List<CashDrawerUserVO> AvailCashDrawerUsers;
        public List<PeripheralTypeVO> AllPeripheralTypes;
        public List<PeripheralModelVO> AllPeripheralModels;
        public List<PeripheralVO> AllPeripherals;
        public List<WorkstationVO> AllWorkstations;
        public List<QuadType<UserVO, LDAPUserVO, string, string>> AllUsers;
        public Dictionary<string, List<PairType<string, PeripheralVO>>> WorkstationPeripheralMap;
        public Dictionary<string, List<PairType<string, PeripheralVO>>> PawnWorkstationPeripheralMap;

        //Pawn sec data
        //public List<PawnSecMachineVO> AllPawnSecMachines;
        public PawnSecVO PawnSecData;

        //Store mappings
        //public Dictionary<CashDrawerVO, List<CashDrawerUserVO>> AssignedCashDrawersToUsersMap;
        //public Dictionary<WorkstationVO, CashDrawerVO> WorkstationCashDrawerMap;

        //Insert lists
        //public List<CashDrawerVO> CashDrawerInserts;
        //public List<CashDrawerUserVO> CashDrawerUserInserts;
        //public List<PeripheralVO> PeripheralsInserts;
        //public List<WorkstationVO> WorkstationsInserts;
        //public List<QuadType<UserVO, LDAPUserVO, string, string>> UsersInserts;
        //public List<Dictionary<string, string>> AllPawnSecMachinesInserts;

        //Update lists
        //public List<CashDrawerVO> CashDrawerUpdates;
        //public List<CashDrawerUserVO> CashDrawerUserUpdates;
        //public List<PeripheralVO> PeripheralsUpdates;
        //public List<WorkstationVO> WorkstationsUpdates;
        //public List<TupleType<UserVO, string, string>> UsersUpdates;

        //Delete lists
        //public List<CashDrawerVO> CashDrawerDeletes;
        //public List<CashDrawerUserVO> CashDrawerUserDeletes;
        //public List<PeripheralVO> PeripheralsDeletes;
        //public List<WorkstationVO> WorkstationsDeletes;
        //public List<TupleType<UserVO, string, string>> UsersDeletes;

        /// <summary>
        /// 
        /// </summary>
        public StoreSetupVO()
        {
            StoreInfo = new SiteId();
            AllCashDrawers = new List<CashDrawerVO>();
            AllCashDrawerUsers = new List<CashDrawerUserVO>();
            AvailCashDrawerUsers = new List<CashDrawerUserVO>();
            AllPeripherals = new List<PeripheralVO>();
            AllWorkstations = new List<WorkstationVO>();
            AllUsers = new List<QuadType<UserVO, LDAPUserVO, string, string>>();
            AllPeripheralTypes = new List<PeripheralTypeVO>();
            AllPeripheralModels = new List<PeripheralModelVO>();

            //Store mappings
            WorkstationPeripheralMap = new Dictionary<string, List<PairType<string, PeripheralVO>>>();
            PawnWorkstationPeripheralMap = new Dictionary<string, List<PairType<string, PeripheralVO>>>();
            //Insert lists
            //CashDrawerInserts = new List<CashDrawerVO>();
            //CashDrawerUserInserts = new List<CashDrawerUserVO>();
            //PeripheralsInserts = new List<PeripheralVO>();
            //WorkstationsInserts = new List<WorkstationVO>();
            //UsersInserts = new List<QuadType<UserVO, LDAPUserVO, string, string>>();

            //Update lists
            //CashDrawerUpdates = new List<CashDrawerVO>();
            //CashDrawerUserUpdates = new List<CashDrawerUserVO>();
            //PeripheralsUpdates = new List<PeripheralVO>();
            //WorkstationsUpdates = new List<WorkstationVO>();
            //UsersUpdates = new List<TupleType<UserVO, string, string>>();

            //Delete lists
            //CashDrawerDeletes = new List<CashDrawerVO>();
            //CashDrawerUserDeletes = new List<CashDrawerUserVO>();
            //PeripheralsDeletes = new List<PeripheralVO>();
            //WorkstationsDeletes = new List<WorkstationVO>();
            //UsersDeletes = new List<TupleType<UserVO, string, string>>();
        }
    }
}
