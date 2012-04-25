using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CouchConsoleApp.db;

namespace CouchConsoleApp.vo
{
    class ArchiveVO
    {
        public bool souceSet;
        public bool targetSet;
        public bool dbSet;
        public CouchVo sourceVO;
        public CouchVo targetVO;
        public DBConnector dbConnector;
       
    }

}
