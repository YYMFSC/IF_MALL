using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF_Model
{
    /// <summary>
    /// 处理结果实体类
    /// </summary>
    public class SystemResults
    {
        public const string SUCCESS = "Success";
        public const string FAILURE = "Failure";
        public const string NOTEXISTS = "NotExists";
        public const string HASEXISTS = "HasExists";

        public SystemResults() {
            this.return_code = FAILURE;
            this.return_msg = "操作失败";
        }

        /// <summary>
        /// 返回代码
        /// </summary>
        public string return_code { set; get; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { set; get; }
        /// <summary>
        /// 登录对象实体
        /// </summary>
        public object objectClass { set; get; }
        /// <summary>
        /// 相关信息
        /// </summary>
        public object objectClassInfo { set; get; }

        /// <summary>
        /// 操作成功返回的信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SystemResults returnSuccess(string msg = "操作成功")
        {
            this.return_msg = msg;
            this.return_code = SUCCESS;
            return this;
        }

        public SystemResults returnSuccess(object ob, string msg = "操作成功")
        {
            this.objectClass = ob;
            this.return_code = SUCCESS;
            this.return_msg = msg;
            return this;
        }
        /// <summary>
        /// 操作失败返回的信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SystemResults returnFailure(string msg = "操作失败")
        {
            this.return_msg = msg;
            this.return_code = FAILURE;
            return this;
        }

        /// <summary>
        /// 自定义返回的信息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SystemResults returnCustom(string msg, string code, object ob = null)
        {
            this.objectClass = ob;
            this.return_msg = msg;
            this.return_code = code;
            return this;
        }

        /// <summary>
        /// 记录不存在导致的操作失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SystemResults returnNotExists(string msg = "操作失败，可能的原因：记录不存在或已被删除")
        {
            this.return_msg = msg;
            this.return_code = NOTEXISTS;
            return this;
        }

        /// <summary>
        /// 记录已存在导致的操作失败
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public SystemResults returnHasExists(string msg = "操作失败，可能的原因：记录已存在")
        {
            this.return_msg = msg;
            this.return_code = HASEXISTS;
            return this;
        }

    }
}
