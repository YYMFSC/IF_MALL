using IF_Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace IF_Model
{

    /// <summary>
    /// 类型转换工具类
    /// </summary>
    public class ConvertUtil
    {

        /// <summary>
        /// 将Hashtable的键值赋值到指定的实体属性中
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public object SetValueFromHashtable(object obj, Hashtable ht)
        {
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            foreach (PropertyInfo item in propertyInfos)
            {
                Object value = ht[item.Name];
                if (value != null)
                {
                    value = SetValueType(item.PropertyType, value);
                    item.SetValue(obj, value, null);
                }
            }
            return obj;
        }

        /// <summary>
        /// 将DataField的值赋值到指定的实体属性中
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public object SetValueFromDataField(object obj, List<DataField> dfl)
        {
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            foreach (PropertyInfo item in propertyInfos)
            {
                DataField df = dfl.Where(n => n.Fieldname == item.Name).SingleOrDefault<DataField>();
                if (df != null)
                {
                    Object value = SetValueType(item.PropertyType, df.Value);
                    item.SetValue(obj, value, null);
                }
            }
            return obj;
        }

        /// <summary>
        /// 根据实体模型的类型设置到DataField
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dfl"></param>
        /// <returns></returns>
        public List<DataField> SetValueTypeToDataField(Type obj, List<DataField> dfl)
        {
            PropertyInfo[] propertyInfos = obj.GetProperties();
            foreach (PropertyInfo item in propertyInfos)
            {
                DataField df = dfl.Where(n => n.Fieldname == item.Name).SingleOrDefault<DataField>();
                if (df != null)
                {
                    df.FieldType = (item.PropertyType).FullName;
                    df.Value = SetValueType(item.PropertyType, df.Value);
                }
            }
            return dfl;
        }

        public object SetValueType(Type propertyType, object value)
        {
            //if (value == null)
            //    return "";
            //if (string.IsNullOrEmpty(value.ToString()) && propertyType.IsGenericType)
            //{
            //    return null;
            //}

            string typename = propertyType.FullName;
            Type genericType = propertyType;

            if (propertyType.IsEnum)
            {
                value = int.Parse(value.ToString());
                return value;
            }
            //else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            //{
            //    genericType = propertyType.GetGenericArguments()[0];
            //}
             
            //if (genericType == typeof(string))
            //{

            //}
            //else if (genericType == typeof(int))
            //{

            //}
            //else if (genericType == typeof(Int64))
            //{

            //}
            //else if (genericType == typeof(DateTime))
            //{

            //}
            //else if (genericType == typeof(decimal))
            //{

            //}
            //else if (genericType == typeof(Boolean))
            //{

            //}
            //else if (genericType == typeof(Guid))
            //{

            //}

            if (value != null)
            {
                switch (typename)
                {
                    case "System.String":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.DateTime":
                    case "System.Decimal":
                    case "System.Boolean":
                    case "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                    case "System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                    case "System.Nullable`1[[System.Guid, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                    case "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                    case "System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                        if (string.IsNullOrEmpty(value.ToString()))
                        {
                            value = null;
                            return value;
                        }
                        break;
                }
                switch (typename)
                {
                    case "System.String":
                        value = value.ToString();
                        break;
                    case "System.Int32":
                        value = int.Parse(value.ToString());
                        break;
                    case "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                        value = int.Parse(value.ToString());
                        break;
                    case "System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                        value = decimal.Parse(value.ToString());
                        break;
                    case "System.Int64":
                        value = Int64.Parse(value.ToString());
                        break;
                    case "System.DateTime":
                        value = DateTime.Parse(value.ToString());
                        break;
                    case "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                        value = DateTime.Parse(value.ToString());
                        break;
                    case "System.Decimal":
                        value = decimal.Parse(value.ToString());
                        break;
                    case "System.Boolean":
                        value = bool.Parse(value.ToString());
                        break;
                    case "System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                        value = bool.Parse(value.ToString());
                        break;
                    case "System.Guid":
                        if (string.IsNullOrEmpty(value.ToString()))
                        {
                            value = Guid.Empty;

                        }
                        else
                        {
                            value = new Guid(value.ToString());
                        }
                        break;
                    case "System.Nullable`1[[System.Guid, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                        value = new Guid(value.ToString());
                        break;

                };
            }
            return value;
        }

        public object SetValueType(object value)
        {
            if (value != null)
            {
                switch (value.GetType().FullName)
                {
                    case "System.Int32":
                        value = int.Parse(value.ToString());
                        break;
                    case "System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                        if (string.IsNullOrEmpty(value.ToString()))
                        {
                            value = null;
                        }
                        else
                        {
                            value = int.Parse(value.ToString());
                        }
                        break;
                    case "System.Int64":
                        value = Int64.Parse(value.ToString());
                        break;
                    case "System.DateTime":
                        value = DateTime.Parse(value.ToString());
                        break;
                    case "System.Decimal":
                        value = decimal.Parse(value.ToString());
                        break;
                    case "System.Boolean":
                        value = bool.Parse(value.ToString());
                        break;
                    case "System.Guid":
                        if (string.IsNullOrEmpty(value.ToString()))
                        {
                            value = Guid.Empty;
                        }
                        else
                        {
                            value = new Guid(value.ToString());
                        }
                        break;
                    case "System.Nullable`1[[System.Guid, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]":
                        if (string.IsNullOrEmpty(value.ToString()))
                        {
                            value = null;
                        }
                        else
                        {
                            value = new Guid(value.ToString());
                        }
                        break;
                };
            }
            return value;
        }

        #region DataTable 与 ArrayList、List转化

        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> list,string tableName="")
        {
            List<PropertyInfo> pList = new List<PropertyInfo>();
            Type type = typeof(T);
            DataTable dt = new DataTable();
            Array.ForEach<PropertyInfo>(type.GetProperties(),
                p =>
                {
                    pList.Add(p);
                    Type colType = p.PropertyType;
                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }
                    dt.Columns.Add(new DataColumn(p.Name, colType));
                });

            foreach (var item in list)
            {
                DataRow row = dt.NewRow();
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null) == null ? DBNull.Value : p.GetValue(item, null));
                dt.Rows.Add(row);
            }
            if (!string.IsNullOrWhiteSpace(tableName))
                dt.TableName = tableName;
            return dt;
        }

        /// <summary>
        /// Dictionary转DataTable
        /// 类型验证待完善
        /// </summary>
        public static DataTable ToDataTable<T,K>(Dictionary<T, K> dict, string tableName = "")
        {
            DataTable dt = new DataTable();
            foreach (var colName in dict.Keys)
            {
                Type valType = typeof(K);
                dt.Columns.Add(colName.ToString(), valType);
            }
            DataRow dr = dt.NewRow();
            foreach (KeyValuePair<T, K> item in dict)
            {
                if (item.Value == null)
                {
                    dr[item.Key.ToString()] = DBNull.Value;
                }
                else
                {
                    dr[item.Key.ToString()] = item.Value;
                }
            }
            dt.Rows.Add(dr);
            if (!string.IsNullOrWhiteSpace(tableName))
                dt.TableName = tableName;
            return dt;
        }

        /// <summary>
        /// 实体转Hashtable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Hashtable ToHashtable<T>(T entity)
        {
            Type type = typeof(T);
            Hashtable dt = new Hashtable();
            Array.ForEach<PropertyInfo>(type.GetProperties(),
                p =>
                {
                    dt[p.Name] = p.GetValue(entity, null) == null ? DBNull.Value : p.GetValue(entity, null);
                });
            return dt;
        }

        /// <summary>
        /// 实体转Hashtable
        /// </summary>
        public static Hashtable ToHashtable(object entity, Type entityType)
        {
            Hashtable dt = new Hashtable();
            Array.ForEach<PropertyInfo>(entityType.GetProperties(),
                p =>
                {
                    dt[p.Name] = p.GetValue(entity, null) == null ? DBNull.Value : p.GetValue(entity, null);
                });
            return dt;
        }

        public static ArrayList DataTable2ArrayList(DataTable data)
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow row = data.Rows[i];

                Hashtable record = new Hashtable();
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    object cellValue = row[j];
                    if (cellValue.GetType() == typeof(DBNull))
                    {
                        cellValue = null;
                    }
                    record[data.Columns[j].ColumnName] = cellValue;
                }
                array.Add(record);
            }
            return array;
        }

        public static List<Hashtable> DataTable2List(DataTable data)
        {
            List<Hashtable> array = new List<Hashtable>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow row = data.Rows[i];
                Hashtable record = new Hashtable();
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    object cellValue = row[j];
                    if (cellValue.GetType() == typeof(DBNull))
                    {
                        cellValue = null;
                    }
                    record[data.Columns[j].ColumnName] = cellValue;
                }
                array.Add(record);
            }
            return array;
        }

        #endregion

        /// <summary>  
        /// 合并两个DataTable列  
        /// </summary>  
        /// <param name="dt1"></param>  
        /// <param name="dt2"></param>  
        /// <returns></returns>  
        public static DataTable MergeDataTable(DataTable dt1, DataTable dt2)
        {
            //定义dt的行数   
            int dtRowCount = 0;
            //dt的行数为dt1或dt2中行数最大的行数   
            if (dt1.Rows.Count > dt2.Rows.Count)
            {
                dtRowCount = dt1.Rows.Count;
            }
            else
            {
                dtRowCount = dt2.Rows.Count;
            }
            DataTable dt = new DataTable();
            //向dt中添加dt1的列名   
            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                dt.Columns.Add(dt1.Columns[i].ColumnName);
            }
            //向dt中添加dt2的列名   
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                dt.Columns.Add(dt2.Columns[i].ColumnName);
            }
            for (int i = 0; i < dtRowCount; i++)
            {
                DataRow row = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    for (int k = 0; k < dt1.Columns.Count; k++) { if ((dt1.Rows.Count - 1) >= i) { row[k] = dt1.Rows[i].ItemArray[k]; } }
                    for (int k = 0; k < dt2.Columns.Count; k++) { if ((dt2.Rows.Count - 1) >= i) { row[dt1.Columns.Count + k] = dt2.Rows[i].ItemArray[k]; } }
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// 合并两个Hashtable的键值对(以第一个Hashtable为母版，第二个Hashtable具有和第一个Hashtable相同的键值时会被跳过)
        /// </summary>
        /// <param name="ht1">第一个HashTable</param>
        /// <param name="ht2">第二个HashTable</param>
        /// <returns></returns>
        public static Hashtable MergeHashTable(Hashtable ht1, Hashtable ht2)
        {
            foreach (DictionaryEntry child in ht2)
            {
                if (!ht1.ContainsKey(child.Key))
                {
                    ht1.Add(child.Key, child.Value);
                }
            }
            return ht1;
        }

        /// <summary>
        /// 将List<Hastable>转化成DataTable
        /// </summary>
        /// <param name="list">Hashtable的List集合</param>
        /// <returns>DataTable对象</returns>
        public static DataTable Convert2DataTable(List<Hashtable> list)
        {
            DataTable dt = new DataTable();
            if (list.Count == 0)
                return dt;

            foreach (string name in list[0].Keys)
                dt.Columns.Add(name);

            foreach (Hashtable item in list)
                dt.Rows.Add(new ArrayList(item.Values).ToArray());

            return dt;
        }

        /// <summary>
        /// 将Hastable转化成DataTable
        /// </summary>
        /// <param name="list">Hashtable</param>
        /// <returns>DataTable对象</returns>
        public static DataTable HashTable2DataTable(Hashtable ht)
        {
            DataTable dt = new DataTable();
            if (ht.Count == 0)
                return dt;

            foreach (string key in ht.Keys)
            {
                dt.Columns.Add(key);
            }
            DataRow row = dt.NewRow();
            foreach (DataColumn column in dt.Columns)
            {
                row[column.ColumnName] = ht[column.ColumnName];
            }
            dt.Rows.Add(row);

            return dt;
        }




        
    }
}
