using Newtonsoft.Json;
using System;
using System.IO;

namespace OKRs.Web.Services
{
    public static class TextSerializerExtensions
    {
        /// <summary>
        /// Serializes the given data object as a string.
        /// </summary>
        public static string Serialize<T>(this ITextSerializer serializer, T data)
        {
            using var writer = new StringWriter();
            serializer.Serialize(writer, data);
            return writer.ToString();
        }
        /// <summary>
        /// Serializes the given data object as a string.
        /// </summary>
        public static string Serialize(this ITextSerializer serializer, object data)
        {
            using var writer = new StringWriter();
            serializer.Serialize(writer, data);
            return writer.ToString();
        }

        /// <summary>
        /// Deserializes the specified string into an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="System.InvalidCastException">The deserialized object is not of type <typeparamref name="T"/>.</exception>
        public static T Deserialize<T>(this ITextSerializer serializer, string serialized)
        {
            using var reader = new StringReader(serialized);
            return serializer.Deserialize<T>(reader);
        }

        /// <summary>
        /// Deserializes the specified string into an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <exception cref="System.InvalidCastException">The deserialized object is not of type <typeparamref name="T"/>.</exception>
        public static object Deserialize(this ITextSerializer serializer, string serialized, Type type)
        {
            using var reader = new StringReader(serialized);
            return serializer.Deserialize(type, reader);
        }

        public static T SerializeToStream<T>(this object source, Func<MemoryStream, T> streamAction)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            using var jsonWriter = new JsonTextWriter(writer);

            var ser = new JsonSerializer();
            ser.Serialize(jsonWriter, source);
            jsonWriter.Flush();
            stream.Reset();
            return streamAction(stream);
        }
    }
}
