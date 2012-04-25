using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CouchConsoleApp.vo
{
    public class PreviousArchStatVO
    {
        private string _successCount;
        private string _getErrorCount;
        private string _addErrorCount;
        private string _delErrorCount;
        private DataTable _dataTable;
        public string SucessCount
        {
            set { this._successCount = value; }
            get { return this._successCount; }
        }

        public string GetErrorCount
        {
            set { this._getErrorCount = value; }
            get { return this._getErrorCount; }
        }

        public string AddErrorCount
        {
            set { this._addErrorCount = value; }
            get { return this._addErrorCount; }
        }

        public string DelErrorCount
        {
            set { this._delErrorCount = value; }
            get { return this._delErrorCount; }
        }

        public DataTable DataTableValue
        {
            set { this._dataTable = value; }
            get { return this._dataTable; }
        }
    }
}
