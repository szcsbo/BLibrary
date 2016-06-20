using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BLibrary.Utility
{
    public static class EntityExtension
    {
        public static IEnumerable<TDestObject> Clone<TDestObject>(this IEnumerable srcCollection, params string[] excludedProperties) where TDestObject : new()
        {
            var result = new List<TDestObject>();
            foreach (var srcItem in srcCollection)
            {
                var destItem = new TDestObject();
                srcItem.CopyTo(destItem, excludedProperties);
                result.Add(destItem);
            }

            return result;
        }

        public static void CopyTo(this object srcObj, object destObj, params string[] excludedProperties)
        {
            if (srcObj == null || destObj == null)
            {
                return;
            }

            var srcType = srcObj.GetType();
            var destType = destObj.GetType();

            var srcProperties = srcType.GetProperties().Where(p => p.CanRead);//BindingFlags.Public | BindingFlags.SetField | BindingFlags.GetField);
            var destProperties = destType.GetProperties().Where(p => p.CanWrite
                && (excludedProperties != null && !excludedProperties.Contains(p.Name)));//BindingFlags.Public | BindingFlags.SetField | BindingFlags.GetField);


            foreach (var prop in destProperties)
            {
                // ReSharper disable PossibleMultipleEnumeration
                var srcProp = srcProperties.FirstOrDefault(p => p.Name == prop.Name);

                // ReSharper restore PossibleMultipleEnumeration
                if (srcProp == null)
                {
                    continue;
                }

                var value = srcProp.GetValue(srcObj, null);
                if ((srcProp.PropertyType != prop.PropertyType)
                    && (!((srcProp.PropertyType.IsEnum && prop.PropertyType.IsAssignableFrom(typeof(int)))
                        || (prop.PropertyType.IsEnum && srcProp.PropertyType.IsAssignableFrom(typeof(int)))
                        || (srcProp.PropertyType.IsAssignableFrom(typeof(bool)) && prop.PropertyType.IsAssignableFrom(typeof(int)))
                        || (prop.PropertyType.IsAssignableFrom(typeof(bool)) && srcProp.PropertyType.IsAssignableFrom(typeof(int)))))
                    )
                {
                    var converter = TypeDescriptor.GetConverter(prop.PropertyType);
                    if (!converter.CanConvertFrom(srcProp.PropertyType))
                    {
                        //not enum and unable to convert
                        continue;
                    }

                    value = converter.ConvertFrom(value);
                }


                prop.SetValue(destObj, value, null);
            }
        }

        public static T ConvertToEntity<T>(this string xmlStr) where T : new()
        {
            T model = new T();

            if (string.IsNullOrEmpty(xmlStr))
            {
                return model;
            }

            XmlDocument xmldoc = new XmlDocument();

            xmldoc.LoadXml(xmlStr);

            XmlNodeList xmlNodes = xmldoc.DocumentElement.ChildNodes;

            foreach (XmlNode node in xmlNodes)
            {
                foreach (PropertyInfo property in model.GetType().GetProperties())
                {
                    if (node.InnerText != "[Null]")
                    {
                        if (node.Name.ToLower() == property.Name.ToLower())
                        {
                            property.SetValue(model, Convert.ChangeType(node.InnerText, property.PropertyType));
                        }
                    }
                    else
                    {
                        property.SetValue(model, null, null);
                    }
                }
            }

            return model;
        }
    }
}
