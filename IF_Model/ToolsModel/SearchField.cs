
using System.Collections.Generic;

namespace IF_Model
{
    /// <summary>
    /// 系统复杂查询的字段对象
    /// </summary>
    public class SearchField
    {
        private string _fieldname;
        private object _objecttype = "";
        private Enum_SearchFieldType _type = Enum_SearchFieldType.Text;
        private Enum_SearchFieldOPType _optype = Enum_SearchFieldOPType.equal;
        private List<AllowOperatorList> _clientOptype;
        private Enum_SearchFieldAndOr _AndOr;
        private object _value = "";
        private string _titlename = "";
        private string _url = "";
        private string _data = "";

        /// <summary>
        /// 字段名
        /// </summary>
        public string fieldname
        {
            set { _fieldname = value; }
            get { return _fieldname; }
        }

        public string titlename
        {
            set { _titlename = value; }
            get { return _titlename; }
        }
        /// <summary>
        /// 字段所属的对象
        /// </summary>
        public object objecttype
        {
            set { _objecttype = value; }
            get { return _objecttype; }
        }
        /// <summary>
        /// 字段类型，不同的类型会对生成不同的输入框
        /// </summary>
        public Enum_SearchFieldType type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 筛选字段的操作类型，例如：等于、不等于
        /// </summary>
        public Enum_SearchFieldOPType optype
        {
            set { _optype = value; }
            get { return _optype; }
        }
        /// <summary>
        /// 筛选字段的值
        /// </summary>
        public object value
        {
            set { _value = value; }
            get { return _value; }
        }
        public Enum_SearchFieldAndOr AndOr
        {
            set { _AndOr = value; }
            get { return _AndOr; }
        }
        public List<AllowOperatorList> clientOptype
        {
            set { _clientOptype = value; }
            get { return _clientOptype; }
        }
        /// <summary>
        /// 下拉列表型的字段的URL
        /// </summary>
        public string url
        {
            set { _url = value; }
            get { return _url; }
        }
        public string data
        {
            set { _data = value; }
            get { return _data; }
        }
    }

    /// <summary>
    /// 允许的运算符列表
    /// </summary>
    public class AllowOperatorList
    {
        private string _opTypeText;
        private Enum_SearchFieldOPType _opType;
        public string OPTypeText
        {
            set { _opTypeText = value; }
            get { return _opTypeText; }
        }
        public Enum_SearchFieldOPType OPType
        {
            set { _opType = value; }
            get { return _opType; }
        }
    }

}
