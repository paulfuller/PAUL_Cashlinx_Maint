using System;

namespace Common.Libraries.Utility.Shared
{
    public class StringDBMap : System.Attribute
    {
        private string _strVal;
        private string _dbVal;

        public StringDBMap(string str, string db)
        {
            _strVal = str;
            _dbVal = db;
        }

        public string strValue
        {
            get { return _strVal; }
        }

        public string dbValue
        {
            get { return _dbVal; }
        }
    }


    public class StringDBMap_Enum<T>
    {
        private delegate string valueAccessor(StringDBMap attr);

        public static string displayValue(T e)
        {
            return getAttr(e, (StringDBMap a) => { return a.strValue; });
        }

        public static string toDBValue(T e)
        {
            return getAttr(e, (StringDBMap a) => { return a.dbValue; });
        }


        public static T fromString(string s)
        {
            T retval = default(T);
            System.Type t = retval.GetType();
            Array names = System.Enum.GetValues(t);

            for (int i = 0; i < names.Length; i++)
            {
                Object thisName = names.GetValue(i);
                System.Reflection.FieldInfo fi = t.GetField(thisName.ToString());

                StringDBMap[] attrs = fi.GetCustomAttributes(typeof(StringDBMap), false) as StringDBMap[];

                if (attrs.Length > 0)
                {

                    if (attrs[0].strValue == s)
                    {
                        retval = (T)thisName;
                        break;
                    }

                }

            }

            return retval;
        }


        private static string getAttr(T e, valueAccessor getValue)
        {
            var retval = string.Empty;

            System.Type t = e.GetType();

            System.Reflection.FieldInfo fi = t.GetField(e.ToString());

            if (fi != null)
            {
                StringDBMap[] attrs = fi.GetCustomAttributes(typeof(StringDBMap), false) as StringDBMap[];

                if (attrs.Length > 0)
                {
                    retval = getValue(attrs[0]);
                }
            }

            return retval;
        }



        public static string[] displayValues()
        {
            System.Type typ = typeof(T);
            Array names = Enum.GetValues(typ);
            string[] retval = new string[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                System.Reflection.FieldInfo fi = typ.GetField(names.GetValue(i).ToString());

                StringDBMap[] attrs = fi.GetCustomAttributes(typeof(StringDBMap), false) as StringDBMap[];


                if (attrs.Length > 0)
                {
                    retval[i] = attrs[0].strValue;
                }
            }

            return retval;
        }
    }
}