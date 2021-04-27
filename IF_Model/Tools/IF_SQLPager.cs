using System.Collections;

namespace IF_Model
{
    /// <summary>
    /// ADO.NET数据库分页存储过程查询对象
    /// </summary>
    public class IF_SQLPager
    {
        private string _fieldName = "*";
        private string _where = " 1=1 ";
        private string _sortField = "";
        private string _sortOrder = "";
        private int _pageSize = 1;
        private int _currentPage = 20;
        private string _tempno = "";
        private string _tablename = "";
        private int _count = 0;
        private IEnumerable _datalist;

        public string fieldName
        {
            set { _fieldName = value; }
            get { return _fieldName; }
        }
        public string where
        {
            set { _where = value; }
            get { return _where; }
        }
        public string sortOrder
        {
            set { _sortOrder = value; }
            get { return _sortOrder; }
        }
        public string sortField
        {
            set { _sortField = value; }
            get { return _sortField; }
        }
        public int pageSize
        {
            set { _pageSize = value; }
            get { return _pageSize; }
        }
        public int currentPage
        {
            set { _currentPage = value; }
            get { return _currentPage; }
        }

        public string tempno
        {
            set { _tempno = value; }
            get { return _tempno; }
        }

        public string tablename
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        public int count
        {
            set { _count = value; }
            get { return _count; }
        }
        public IEnumerable datalist
        {
            set { _datalist = value; }
            get { return _datalist; }
        }
    }

}
