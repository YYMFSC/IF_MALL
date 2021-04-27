
using System;
namespace IF_Model
{
    /// <summary>
    /// 内部枚举的扩展特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumAttribute : System.Attribute
    {
        public EnumAttribute(bool isshow, string memo)
        {
            this.IsShow = isshow;
            this.Memo = memo;
        }

        private bool _isshow;
        private string _memo;

        public virtual bool IsShow
        {
            set { _isshow = value; }
            get { return _isshow; }
        }

        public virtual string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }

    }

}
