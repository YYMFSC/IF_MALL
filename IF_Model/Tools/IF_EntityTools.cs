
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace IF_Model
{

    /// <summary>
    /// 实体框架工具类
    /// 前后台数据转换
    /// </summary>
    public class IF_EntityTools
    {
        /// <summary>
        /// 实体复制
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="ignorePoperties"></param>
        /// <returns></returns>
        public static void EntityCopy<TSource, TTarget>(TSource source, TTarget target, string ignorePoperties)
        {
            List<string> ignoreP = new List<string>();
            if (!string.IsNullOrEmpty(ignorePoperties))
            {
                ignoreP = ignorePoperties.ToLower().Split(',').ToList();
            }
            ignoreP.Add("entitykey");
            ignoreP.Add("entitystate");
            var tFields = target.GetType().GetProperties();
            var sFields = source.GetType().GetProperties();
            foreach (var item in tFields)
            {
                if (!ignoreP.Contains(item.Name.ToLower()))
                {
                    foreach (var si in sFields)
                    {
                        if (si.Name == item.Name)
                        {
                            object svalue = si.GetValue(source, null);
                            object tvalue = item.GetValue(target, null);
                            if (svalue != null && !svalue.Equals(tvalue))
                            {

                                item.SetValue(target, svalue, null);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将前台传来的筛选字段JSON信息转换为对应的实体
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public List<SearchField> getSearchFieldFromJson(string json)
        {
            return ConvertFromJson<SearchField>(json);
        }

        public List<T> ConvertFromJson<T>(string json) where T : class
        {
            List<T> sl = new List<T>();
            if (!string.IsNullOrWhiteSpace(json))
            {
                sl = (List<T>)JsonConvert.DeserializeObject<List<T>>(json);
            }
            return sl;
        }

        public T ConvertObjectFromJson<T>(string json) where T : class
        {
            if (!string.IsNullOrWhiteSpace(json))
            {
                T sl = (T)JsonConvert.DeserializeObject<T>(json);
                return sl;
            }
            return null;
        }

        /// <summary>
        /// 将前台传来的表单信息JSON信息转换为对应的实体
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public List<DataField> getDataFieldFromJson(string json)
        {
            return ConvertFromJson<DataField>(json);
        }

        /// <summary>
        /// 将查询字段转换为查询语句
        /// </summary>
        /// <param name="sf">查询字段</param>
        /// <returns></returns>
        public string ConventToSearchText(SearchField sf, bool isef = false)
        {
            string text = "";
            if (sf.AndOr == Enum_SearchFieldAndOr.Or)
            {
                text += " or (" + sf.fieldname;
            }
            else
            {
                text += " and (" + sf.fieldname;
            }
            if (sf.type != Enum_SearchFieldType.Num && isef == false && !sf.optype.Equals(Enum_SearchFieldOPType.like))
            {

                sf.value = "'" + sf.value + "'";
            }
            if (sf.type != Enum_SearchFieldType.Num && isef && !sf.optype.Equals(Enum_SearchFieldOPType.like))
            {

                sf.value = "\"" + sf.value + "\"";
            }
            switch (sf.optype)
            {
                case Enum_SearchFieldOPType.bigger: text += " > " + sf.value; break;
                case Enum_SearchFieldOPType.smaller: text += " < " + sf.value; break;
                case Enum_SearchFieldOPType.like: text += " like '%" + sf.value + "%'"; break;
                case Enum_SearchFieldOPType.equal: text += " = " + sf.value; break;
                case Enum_SearchFieldOPType.notequal: text += " <> " + sf.value; break;
                default: break;
            }
            text += ")";
            return text;
        }
    }

    /// <summary>
    /// 从实体类获取SearchField（多条件查询）列表
    /// </summary>
    //public class SearchFieldListFromModel : EflyingEntityTools
    //{
    //    public List<SearchField> sf = new List<SearchField>();
    //    public SearchFieldListFromModel(List<Type> modellist)
    //    {
    //        foreach (Type model in modellist)
    //        {
    //            PropertyInfo[] modelperoperties = model.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    //            foreach (PropertyInfo modelproperty in modelperoperties)
    //            {
    //                object[] objs = modelproperty.GetCustomAttributes(typeof(SearchFieldAttribute), true);
    //                if (objs.Length > 0)
    //                {
    //                    if (((SearchFieldAttribute)objs[0]).IsAllowQuery)
    //                    {
    //                        SearchField field = new SearchField
    //                        {
    //                            fieldname = modelproperty.Name,
    //                            clientOptype = getAllowOptype(((SearchFieldAttribute)objs[0])),
    //                            titlename = ((SearchFieldAttribute)objs[0]).Title,
    //                            type = ((SearchFieldAttribute)objs[0]).Type,
    //                            url = ((SearchFieldAttribute)objs[0]).Url,
    //                            data = ((SearchFieldAttribute)objs[0]).Data
    //                        };

    //                        sf.Add(field);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

}
