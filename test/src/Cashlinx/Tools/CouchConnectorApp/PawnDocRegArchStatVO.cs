using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouchConsoleApp
{
    public class PawnDocRegArchStatVO
    {
        private int ID;
        private int DocRegID;
        private int StorageID;
        private DateTime ArchDate;
        private int TargetDocDB;
        private int UserID;
        private char Status;
        private int ErrorID;
        private bool IsSuccess;
        private string ErrorMSG;
    }
}
