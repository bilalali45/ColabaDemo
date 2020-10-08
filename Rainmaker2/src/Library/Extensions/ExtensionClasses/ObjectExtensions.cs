using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Extensions.ExtensionClasses
{

    /// <summary>
    /// C# extension method for fast object cloning.
    /// Based upon the great net-object-deep-copy GitHub project by Alexey Burtsev, released under the MIT license.
    /// 
    /// https://github.com/Burtsev-Alexey/net-object-deep-copy
    /// </summary>
    public static partial class ObjectExtensions
    {
        #region Extension Methods For Object Cloning

        /// <summary>
        /// The Clone Method that will be recursively used for the deep clone.
        /// </summary>
        private static readonly MethodInfo CloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Returns TRUE if the type is a primitive one, FALSE otherwise.
        /// </summary>
        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(String)) return true;
            return (type.IsValueType && type.IsPrimitive);
        }

        /// <summary>
        /// Returns a Deep Clone / Deep Copy of an object using a recursive call to the CloneMethod specified above.
        /// </summary>
        public static Object DeepClone(this Object obj)
        {
            return DeepClone_Internal(obj, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
        }

        


        #region Helpers


        /// <summary>
        /// Returns a Deep Clone / Deep Copy of an object of type T using a recursive call to the CloneMethod specified above.
        /// </summary>


        private static Object DeepClone_Internal(Object obj, IDictionary<Object, Object> visited)
        {
            if (obj == null) return null;
            var typeToReflect = obj.GetType();
            if (IsPrimitive(typeToReflect)) return obj;
            if (visited.ContainsKey(obj)) return visited[obj];
            if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
            var cloneObject = CloneMethod.Invoke(obj, null);
            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (!IsPrimitive(arrayType))
                {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(DeepClone_Internal(clonedArray.GetValue(indices), visited), indices));
                }

            }
            visited.Add(obj, cloneObject);
            CopyFields(obj, visited, cloneObject, typeToReflect);
            RecursiveCopyBaseTypePrivateFields(obj, visited, cloneObject, typeToReflect);
            return cloneObject;
        }

        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && !filter(fieldInfo)) continue;
                if (IsPrimitive(fieldInfo.FieldType)) continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = DeepClone_Internal(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }


        #endregion


        /***********************************************************************************************************/

        #endregion
            
        public static bool HasValue(this object obj)
        {
            return obj != null;
        }

        public static string ToJson(this object value)
        {
            if (value == null) return null;

            try
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string json = JsonConvert.SerializeObject(value,formatting:Formatting.Indented,settings: jsonSerializerSettings);
                return json;
            }
            catch (Exception)
            {
                // ignored
            }
            
            return null;
        }

        public static string ToJson(this object value,
                                    JsonSerializerSettings jsonSerializerSettings)
        {
            if (value == null) return null;

            try
            {
               

                string json = JsonConvert.SerializeObject(value, formatting: Formatting.Indented, settings: jsonSerializerSettings);
                return json;
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

    }


    internal class ReferenceEqualityComparer : EqualityComparer<Object>
    {
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }
        public override int GetHashCode(object obj)
        {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }




}

