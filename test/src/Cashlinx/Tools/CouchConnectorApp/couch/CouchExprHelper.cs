using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouchConsoleApp.couch
{
    public class CouchExprHelper
    {
        private static readonly CouchExprHelper expInst = new CouchExprHelper();

        public static CouchExprHelper getInstance()
        {
            return expInst;
        }


        public string adminSecExpr(List<string> adminNames, List<string> adminRoles,
           List<string> readerNames, List<string> readerRoles)
        {
            string tmp = null;
            string str = "{\"admins\":{\"names\":[AAAA], \"roles\":[BBBB]}, \"readers\":{\"names\":[\"CCCC\"],\"readers\":[DDDD]}}";
           

            /*string str = "\'" + "{\"admins\":{\"names\":[AAAA], \"roles\":[BBBB]}, \"readers\":{\"names\":[\"CCCC\"],\"readers\":[DDDD]}}" + "\'";
            str = "-u \"admin:adminadmin\" -d" + str;*/

            if (adminNames != null && adminNames.Count > 0)
            {
                tmp = string.Join(",", adminNames.ToArray());
                str = str.Replace("AAAA", tmp);
            }
            else
            {
                str = str.Replace("AAAA", "");
            }

            if (adminRoles != null && adminRoles.Count > 0)
            {
                tmp = string.Join(",", adminNames.ToArray());
                str = str.Replace("BBBB", tmp);
            }
            else
            {
                str = str.Replace("BBBB", "");
            }

            if (readerNames != null && readerNames.Count > 0)
            {
                tmp = string.Join(",", readerNames.ToArray());
                str = str.Replace("CCCC", tmp);
            }
            else
            {
                str = str.Replace("CCCC", "");
            }

            if (readerRoles != null && readerRoles.Count > 0)
            {
                tmp = string.Join(",", readerRoles.ToArray());
                str = str.Replace("DDDD", tmp);
            }
            else
            {
                str = str.Replace("DDDD", "");
            }

            return str;
        }
    }
}
