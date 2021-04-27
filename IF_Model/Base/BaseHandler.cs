using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace IF_Model
{
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        //实例化数据库上下文类
        public mallEntities mall = new mallEntities();
        public virtual void ProcessRequest(HttpContext context)
        {
            string methodName = context.Request["method"];
            //判断方法名是否存在
            if (string.IsNullOrEmpty(methodName))
            {
                context.Response.AppendHeader("sessionstatus", "methodNotExists");
                return;
            }

            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);
            method.Invoke(this, new object[] { context });
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void WriteSuccess(HttpContext context, object systemResultsSuccessObj = null, bool isEnd = false)
        {
            var obj = new SystemResults().returnSuccess(systemResultsSuccessObj);
            context.Response.Write(JSON.Encode(obj));
            if (isEnd)
                context.Response.End();
        }
        public void WriteFailure(HttpContext context, string systemResultsFailureStr = "", bool isEnd = false)
        {
            var obj = new SystemResults().returnFailure(systemResultsFailureStr);
            context.Response.Write(JSON.Encode(obj));
            if (isEnd)
                context.Response.End();
        }
        public void WriteInfo(HttpContext context, object successObj = null, bool isEnd = false)
        {
            context.Response.Write(JSON.Encode(successObj));
            if (isEnd)
                context.Response.End();
        }

        public IEnumerable<M> GetPagination<M>(IF_SQLPager pager, IQueryable<M> linq) where M : class
        {
            pager.count = linq.Count();
            OrderModelField orderModelField = new OrderModelField();
            orderModelField.PropertyName = pager.sortField;
            orderModelField.IsDESC = string.Compare("desc", pager.sortOrder, true) == 0;
            linq = EFCommonSort.CommonSort(linq, new OrderModelField[] { orderModelField });
            var lists = linq.Skip((pager.currentPage - 1) * pager.pageSize).Take(pager.pageSize).ToList();
            pager.datalist = lists;
            return lists;
        }
    }
}
