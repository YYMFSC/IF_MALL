using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Model.Tools
{
    public static class EFCommonSort
    {
        /// <summary>
        /// 单个排序通用方法
        /// </summary>
        /// <typeparam name="Tkey">排序字段</typeparam>
        /// <param name="data">要排序的数据</param>
        /// <param name="orderWhere">排序条件</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns>排序后的集合</returns>
        public static IQueryable<T> CommonSort<T, Tkey>(IQueryable<T> data,
            Expression<Func<T, Tkey>> orderWhere, bool isDesc) where T : class
        {
            if (isDesc)
            {
                return data.OrderByDescending(orderWhere);
            }
            else
            {
                return data.OrderBy(orderWhere);
            }
        }

        /// <summary>
        /// 多个排序通用方法
        /// </summary>
        /// <typeparam name="Tkey">排序字段</typeparam>
        /// <param name="data">要排序的数据</param>
        /// <param name="orderWhereAndIsDesc">字典集合(排序条件,是否倒序)</param>
        /// <returns>排序后的集合</returns>
        public static IQueryable<T> CommonSort<T>(IQueryable<T> data,
            params OrderModelField[] orderByExpression) where T : class
        {
            //创建表达式变量参数
            var parameter = Expression.Parameter(typeof(T), "o");
            if (orderByExpression != null && orderByExpression.Length > 0)
            {
                for (int i = 0; i < orderByExpression.Length; i++)
                {
                    var orderStr = orderByExpression[i].PropertyName; //排序名称
                    var orderT = orderStr.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);//分割对象属性

                    MemberExpression propertyAccess = null;
                    PropertyInfo property = null;
                    string propertyFile;
                    var propertyName = orderT[0]; //排序字段名（可能为对象）
                    //根据属性名获取属性
                    property = typeof(T).GetProperty(propertyName);
                    var ppt = property.PropertyType;
                    if (orderT.Count() > 1)
                    {
                        propertyAccess = Expression.MakeMemberAccess(parameter, property);

                        for (int j = 1; j < orderT.Length; j++)
                        {
                            propertyFile = orderT[j]; //排序字段名
                            property = property.PropertyType.GetProperty(propertyFile);
                            //创建一个访问属性的表达式

                            propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                            ppt = property.PropertyType;
                        }
                    }
                    else
                    {
                        propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    }

                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    string OrderName = "";
                    if (i > 0)
                    {
                        OrderName = orderByExpression[i].IsDESC ? "ThenByDescending" : "ThenBy";
                    }
                    else
                        OrderName = orderByExpression[i].IsDESC ? "OrderByDescending" : "OrderBy";

                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(T), ppt },
                    data.Expression, Expression.Quote(orderByExp));

                    data = data.Provider.CreateQuery<T>(resultExp);
                }
            }
            return data;
        }
    }

    public struct OrderModelField
    {
        public bool IsDESC { get; set; }
        public string PropertyName { get; set; }
    }

}
