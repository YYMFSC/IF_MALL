using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace IF_Model
{
    public class MSSQL_help
    {

        /// <summary>
        /// 初始化一个分页对象（带多条件查询的）
        /// </summary>
        /// <param name="sl">多条件查询对象</param>
        /// <param name="request">前台的POST表单</param>
        /// <returns></returns>
        public IF_SQLPager setPager(List<SearchField> sl, HttpRequest request)
        {
            string wherestring = "";
            if (sl != null)
            {
                foreach (SearchField sf in sl)
                {
                    if (string.IsNullOrEmpty(wherestring))
                    {
                        sf.AndOr = Enum_SearchFieldAndOr.And;
                    }
                    wherestring += new IF_EntityTools().ConventToSearchText(sf);
                }
            }
            if (!string.IsNullOrWhiteSpace(wherestring))
            {
                wherestring = " and (1=1 " + wherestring + ")";
            }
            IF_SQLPager pager = setNormalPager(request);
            pager.where += wherestring;
            return pager;
        }

        /// <summary>
        /// 初始化一个普通分页对象（不带多条件查询）
        /// </summary>
        /// <param name="request">前台的POST表单</param>
        /// <returns></returns>
        public IF_SQLPager setNormalPager(HttpRequest request)
        {
            IF_SQLPager pager = new IF_SQLPager
            {
                currentPage = Convert.ToInt32(request["pageIndex"]) + 1,
                sortField = request["sortField"],
                sortOrder = request["sortOrder"],
                pageSize = Convert.ToInt32(request["pageSize"])
            };
            return pager;
        }

        public IF_SQLPager setHashTablePager(Hashtable ht)
        {
            IF_SQLPager pager = new IF_SQLPager
            {
                currentPage = Convert.ToInt32(ht["pageIndex"] == null ? "0" : ht["pageIndex"].ToString()) + 1,
                sortField = ht["sortField"].ToString(),
                sortOrder = ht["sortOrder"].ToString(),
                pageSize = Convert.ToInt32(ht["pageSize"] == null ? "10" : ht["pageSize"].ToString())
            };
            return pager;
        }

        public Hashtable setDataHashtable(IF_SQLPager Pager)
        {
            Hashtable result = new Hashtable();
            result["data"] = Pager.datalist;
            result["total"] = Pager.count;
            return result;
        }

    }

}
