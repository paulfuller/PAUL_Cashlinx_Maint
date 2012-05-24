using System;
using System.Collections.Generic;

namespace Support.Libraries.Objects.Customer
{
    [Serializable]
    public class Category
    {
        public int CategoryId
        {
            get;
            set;
        }
        public string CategoryName
        {
            get;
            set;
        }
        //public string Description
        //{
        //    get;
        //    set;
        //}


        public Category()
        {
            this.initialize();
        }

        private void initialize()
        {
            //this.CategoryId = "";
            this.CategoryName = "";
            //this.Description = "";
        }

    }

}
