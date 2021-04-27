
using System.Collections.Generic;

namespace IF_Model
{
    /// <summary>
    /// 从前台提交上来的数据
    /// </summary>
    public class DataField
    {
        private string _fieldname;
        private string _fieldtext = "";
        private string _fieldtype = "";
        private object _value = "";
        /// <summary>
        /// 字段名
        /// </summary>
        public string Fieldname
        {
            set { _fieldname = value; }
            get { return _fieldname; }
        }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType
        {
            set { _fieldtype = value; }
            get { return _fieldtype; }
        }
        /// <summary>
        /// 字段值描述
        /// </summary>
        public string Fieldtext
        {
            set { _fieldtext = value; }
            get { return _fieldtext; }
        }

        /// <summary>
        /// 字段的值
        /// </summary>
        public object Value
        {
            set { _value = value; }
            get { return _value; }
        }

    }
}
