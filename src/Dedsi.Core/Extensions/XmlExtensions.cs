using System.Collections.Concurrent;
using System.Text;
using System.Xml.Serialization;

namespace Dedsi.Core.Extensions
{
    public static class XmlExtensions
    {
        private static readonly ConcurrentDictionary<Type, XmlSerializer> Serializers = new();

        /// <summary>
        /// 获取XmlSerializer实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static XmlSerializer GetSerializer(Type type)
        {
            return Serializers.GetOrAdd(type, t => new XmlSerializer(t));
        }

        /// <summary>
        /// xml反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T XmlDeserialize<T>(this Stream stream, Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            var serializer = GetSerializer(typeof(T));

            using (StreamReader sr = new StreamReader(stream, encoding))
            {
                return (T)serializer.Deserialize(sr);
            }
        }

        /// <summary>
        /// xml反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T XmlDeserialize<T>(this string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException(nameof(s));
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            var serializer = GetSerializer(typeof(T));

            // Use StringReader directly to avoid unnecessary byte conversion
            using (StringReader sr = new StringReader(s))
            {
                return (T)serializer.Deserialize(sr);
            }
        }

        /// <summary>
        /// xml反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            var serializer = GetSerializer(typeof(T));

            using (StringReader reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
