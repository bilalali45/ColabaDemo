using System.Reflection;
using Repository.Pattern.Base;

namespace RainMaker.Common.Extensions
{
   public static class Mapping
    {
        
       public static  void Map<T,TK>(this T source, TK destination)  where T :class
       {
            
           foreach (PropertyInfo propertyInfo in source.GetType().GetProperties())
           {

               var destinationPropertyInfo = destination.GetType().GetProperty(propertyInfo.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

               if (destinationPropertyInfo != null)
                   destinationPropertyInfo.SetValue(destination, propertyInfo.GetValue(source));
           }
       }

        /// <summary>
        ///  Maps All Properties Except Primary Key, Navigational Properties and ObjectState   
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TK"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void MapEntityProperties<T, TK>(this T source, TK destination) where T : EntityBase
        {

            foreach (PropertyInfo propertyInfo in source.GetType().GetProperties())
            {
                if (propertyInfo.Name.ToLower()=="id" || 
                    propertyInfo.Name.ToLower() == "objectstate" ||
                    //propertyInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute>() != null
                    propertyInfo.GetGetMethod().IsVirtual)
                {
                    continue;
                }

                var destinationPropertyInfo = destination.GetType().GetProperty(propertyInfo.Name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

                if (destinationPropertyInfo != null)
                    destinationPropertyInfo.SetValue(destination, propertyInfo.GetValue(source));
            }
        }

    }
}
