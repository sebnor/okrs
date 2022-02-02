using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OKRs.Web.Services
{
    internal class JsonTextSerializer : ITextSerializer
    {
        private readonly JsonSerializer _serializer;

        public JsonTextSerializer(IEnumerable<JsonConverter> converters)
            : this(JsonSerializer.Create(new JsonSerializerSettings { Converters = converters.ToList(), NullValueHandling = NullValueHandling.Ignore }))
        {
        }

        private JsonTextSerializer(JsonSerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public void Serialize<T>(TextWriter writer, T graph)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var jsonWriter = new JsonTextWriter(writer)
            {
                Formatting = Newtonsoft.Json.Formatting.None
            };
            _serializer.Serialize(jsonWriter, graph);
            writer.Flush();
        }

        public void Serialize(TextWriter writer, object graph)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            var jsonWriter = new JsonTextWriter(writer)
            {
                Formatting = Newtonsoft.Json.Formatting.None
            };
            _serializer.Serialize(jsonWriter, graph);
            writer.Flush();
        }

        public T Deserialize<T>(TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            var jsonReader = new JsonTextReader(reader);
            return _serializer.Deserialize<T>(jsonReader);
        }

        public object Deserialize(Type type, TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            var jsonReader = new JsonTextReader(reader);
            return _serializer.Deserialize(jsonReader, type);
        }
    }
}
