using Accolades.Maije.Crosscutting.Comparers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Accolades.Maije.Crosscutting.Extensions
{
    public static class ObjectExtensions
    {
        private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Deep clone an object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="originalObject">The object to clone</param>
        /// <returns>The clone object</returns>
        public static T DeepClone<T>(this T originalObject)
        {
            return (T)InternalCopy(originalObject, new Dictionary<object, object>(new ReferenceEqualityComparer()));
        }

        /// <summary>
        /// Create an internal copy
        /// </summary>
        /// <param name="originalObject">The original object</param>
        /// <param name="visited">The dictionnary of visited properties</param>
        /// <returns></returns>
        private static object InternalCopy(object originalObject, IDictionary<object, object> visited)
        {
            if (originalObject == null) return null;

            var typeToReflect = originalObject.GetType();

            if (typeToReflect.IsPrimitive()) return originalObject;

            if (visited.ContainsKey(originalObject)) return visited[originalObject];

            if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;

            var cloneObject = CloneMethod.Invoke(originalObject, null);

            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (arrayType.IsPrimitive() == false)
                {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
                }

            }

            visited.Add(originalObject, cloneObject);
            CopyFields(originalObject, visited, cloneObject, typeToReflect);
            RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);

            return cloneObject;
        }

        /// <summary>
        /// Recursive copy base on type private fields
        /// </summary>
        /// <param name="originalObject">The original object</param>
        /// <param name="visited">The visited properties</param>
        /// <param name="cloneObject">The clone object</param>
        /// <param name="typeToReflect">The type to reflect</param>
        private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        /// <summary>
        /// Copy fields from an object
        /// </summary>
        /// <param name="originalObject">The original object</param>
        /// <param name="visited">Already visited fields</param>
        /// <param name="cloneObject">The clone object</param>
        /// <param name="typeToReflect">The type to reflect</param>
        /// <param name="bindingFlags">The fields binding flags</param>
        /// <param name="filter"></param>
        private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false) continue;
                if (fieldInfo.FieldType.IsPrimitive()) continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = InternalCopy(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }
    }
}
