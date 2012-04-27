using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Pawn.Forms.UserControls
{
    public partial class PagedGridView : DataGridView
    {
        public string pageIndicatorControl;
        public string nextControl;

        public bool externalPagingControl = false;
        private int _nrPages = 1;
        public int currPage = 1;
        private int _pageSize = 1;
        public int currRow = 1;

        public int nrRecords
        {
            get
            {
                return _baseData.Count;
            }
        }

        private DataView _baseData;
        
        public int pageSize
        { 
            get
            {
                return _pageSize;
            }

            set
            {
                if (value > 0)
                    _pageSize = value;
                else
                {
                    throw new Exception("INVALID PAGE SIZE:  size must be > 0");
                }
            }
        }

        public int pageCount
        {
            get
            {
                return _nrPages;
            }
        }

        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }
            
            set
            {
                _baseData = (DataView) value;

                if (externalPagingControl && _pageSize > 1)
                {
                    _nrPages = (int) Math.Ceiling((decimal)_baseData.Count / _pageSize);

                    if (currPage > _nrPages)
                        currPage = 1;

                    changePage();
                }
                else
                    base.DataSource = _baseData.Table.DefaultView;
            }
        }

        public delegate void PagingEvent(object sender, PropertyChangedEventArgs e); // would be nice if this included indicator of old & new page

        public delegate void PagingControlEvent(object sender, EventArgs e);

        public PagingEvent onPageChange;
        public PagingControlEvent pagingVisibilityChange;

        public void nextPage(object sender, EventArgs e)
        {
            if (currPage + 1 <= _nrPages)
            {
                currPage++;
                changePage();
            }
        }

        public void prevPage(object sender, EventArgs e)
        {
            if (currPage - 1 >= 1)
            {
                currPage--;
                changePage();
            }
        }

        public void firstPage(object sender, EventArgs e)
        {
            currPage = 1;
            changePage();
        }

        public void lastPage(object sender, EventArgs e)
        {
            currPage = _nrPages;
            changePage();
        }

        public void nthPage(int n)
        {
            if (n >= 1 && n <= _nrPages)
            {
                currPage = n;
                changePage();
            }
        }

        private void changePage()
        {
            DataTable aTable = _baseData.Table.Clone();

            for (int i = (currPage - 1) * pageSize; i < pageSize * currPage && i < _baseData.Count; i++)
            {
                aTable.ImportRow(_baseData[i].Row);
            }

            base.DataSource = aTable;

            if (onPageChange != null)
            {
                onPageChange.Invoke(this, new PropertyChangedEventArgs("currPage"));
            }
        }

        private bool inSort = false;

        private void sortColumn(Object sender, EventArgs e)
        {
            if (!inSort)
            {
                inSort = true;
                string sortDir = "ASC";
                int sortedColIdx = SortedColumn.Index;
                ListSortDirection order = ListSortDirection.Ascending;

                if (this.SortOrder == SortOrder.Descending)
                {
                    sortDir = "DESC";
                    order = ListSortDirection.Descending;
                }

                _baseData.Sort = this.SortedColumn.Name + " " + sortDir;

                changePage();

                this.Sort(Columns[sortedColIdx], order);

                inSort = false;
            }
        }

        public void PagedGridView_Scroll(object sender, ScrollEventArgs a)
        {
            FirstDisplayedScrollingRowIndex = 0;
        }

        public PagedGridView()
        {
            InitializeComponent();
            Sorted += sortColumn;
            Scroll += PagedGridView_Scroll;
        }

        public PagedGridView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            Sorted += sortColumn;
            Scroll += PagedGridView_Scroll;
        }
    }
}