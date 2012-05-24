using System;
using System.Collections.Generic;
using System.Data;
using Common.Libraries.Utility.Type;

namespace Common.Libraries.Objects.Mapper
{
    /*
     * Utilizing thread safe lock free singleton strategy 
     * Singleton implementation strategy from http://www.yoda.arachsys.com/csharp/singleton.html
     */
    /// <summary>
    /// 
    /// </summary>
    public sealed class DataTypeVOMapper
    {   
        private Dictionary<DataTypeConstants.PawnDataType, PairType<TypeCode, SqlDbType>> mapping;
        private List<DataTypeConstants.PawnDataType> keys;
        static readonly DataTypeVOMapper instance = new DataTypeVOMapper();

        static DataTypeVOMapper()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static DataTypeVOMapper Instance
        {
            get
            {
                return (instance);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                return (this.keys.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public DataTypeConstants.PawnDataType this[int i]
        {
            get
            {
                if (i < 0 || i >= this.keys.Count)return(DataTypeConstants.PawnDataType.STRING);
                return (this.keys[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public PairType<TypeCode, SqlDbType> this[DataTypeConstants.PawnDataType p]
        {
            get
            {
                if (!this.mapping.ContainsKey(p)) return (null);
                return (this.mapping[p]);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DataTypeVOMapper()
        {
            this.mapping = new Dictionary<DataTypeConstants.PawnDataType, PairType<TypeCode, SqlDbType>>();
            this.keys = new List<DataTypeConstants.PawnDataType>();

            foreach (PairType<DataTypeConstants.PawnDataType, PairType<TypeCode, SqlDbType>> p in DataTypeConstants.DATATYPES)
            {
                if (p == null) continue;
                DataTypeConstants.PawnDataType curKey = p.Left;
                PairType<TypeCode, SqlDbType> curVal = p.Right;
                this.mapping.Add(curKey, curVal);
                this.keys.Add(curKey);
            }
        }
    }
}
