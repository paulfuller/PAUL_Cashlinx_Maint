using System;
using System.Collections;
using System.Collections.Generic;

namespace Support.Libraries.Objects.Customer
{
    [Serializable]
    public class CategoryVO  //: IEnumerable
    {
        private List<Category> categories;

        public List<Category> CommentCategories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
            }
        }
        /*
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public CategoryEnum GetEnumerator()
        {
            return new CategoryEnum(categories);
        }
        */
        public CategoryVO()
        {
            this.categories = new List<Category>();    
        }

    }

    /*__________________________________________________________________________________________*/
    /*
    public class CategoryEnum : IEnumerator
    {
        public Category[] _category;
        private int position = -1;

        public CategoryEnum(Category[] list)
        {
            _category = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _category.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public Category Current
        {
            get
            {
                try
                {
                    return _category[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    */
}
