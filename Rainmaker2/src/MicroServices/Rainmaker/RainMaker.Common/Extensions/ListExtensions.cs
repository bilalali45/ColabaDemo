using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Dynamic;
using System.Linq;

namespace RainMaker.Common.Extensions
{

    static public class ListExtensions
    {
        public static List<T> SetInactiveItems<T>(this List<T> list, string displayName)
        {

            var field = typeof(T).GetProperty("IsActive");
            var displayNamePropertyInfo = typeof(T).GetProperty(displayName);

            foreach (var item in list)
            {
                if (field != null && (bool)field.GetValue(item) == false)
                {
                    var name = (string)displayNamePropertyInfo.GetValue(item);
                    name += Constants.InActiveSymbol;
                    displayNamePropertyInfo.SetValue(item, name);
                }
            }

            return list;
        }

        public static List<T> GetDuplicateRecords<T>(this List<T> list, string column)
        {
            var newList = new List<int>();
            var duplicateList = new List<T>();

            var types = typeof(T).GetType();
            var properties = typeof(T).GetProperties();


            foreach (var item in list)
            {

                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.CanRead && propertyInfo.Name.ToLower() == column.ToLower())
                    {

                        var val = propertyInfo.GetValue(item, null);

                        if (newList.Contains(val.ToInt()))
                            duplicateList.Add(item);
                        else
                            newList.Add(val.ToInt());
                        break;
                    }
                }
            }

            return duplicateList;
        }

        public static List<T> RemoveDuplicateRecords<T>(this List<T> list, string column)
        {
            var newList = new List<int>();
            var properties = typeof(T).GetProperties();

            var removedList = new List<T>();

            foreach (var item in list)
            {

                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.CanRead && propertyInfo.Name.ToLower() == column.ToLower())
                    {

                        var val = propertyInfo.GetValue(item, null);

                        if (newList.Contains(val.ToInt()))
                            removedList.Add(item);
                        else
                            newList.Add(val.ToInt());
                        break;
                    }
                }
            }
            foreach (var item in removedList)
            {
                list.Remove(item);
            }
            return list;
        }

        public static List<object> ConvertAllToString<T>(this List<T> list)
        {

            System.Collections.Generic.List<object> list2 = new List<object>();

            foreach (var item in list)
            {
                dynamic obj = new ExpandoObject();


                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.CanRead)
                    {

                        var val = propertyInfo.GetValue(item, null);
                        //row.Add(val==null?null:val.ToString()); 
                        ((IDictionary<string, object>)obj).Add(propertyInfo.Name, (val == null ? null : val.ToString()));

                    }
                }

                list2.Add(obj);
            }



            return list2;
        }

        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        public static DataTable ToDataTable<T>(this List<T> items, string ignoreColumns=null)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (ignoreColumns != null)
            {
                var arrIgnoreColumns = ignoreColumns.Split(',');
                props = props.Where(p => !arrIgnoreColumns.Contains(p.Name)).ToArray();
            }

            foreach (PropertyInfo prop in props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

    }
}
