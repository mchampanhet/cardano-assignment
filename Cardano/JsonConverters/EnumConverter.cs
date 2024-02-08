using Cardano.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cardano.JsonConverters
{
    internal class EnumConverter<T> : JsonConverter<T>
    {
        private readonly JsonConverter<T> _converter;
        private readonly Type _underlyingType;

        public EnumConverter() : this(null) { }

        public EnumConverter(JsonSerializerOptions options)
        {
            // for performance, use the existing converter if available
            if (options != null)
            {
                _converter = (JsonConverter<T>)options.GetConverter(typeof(T));
            }
            var test = typeof(T);
            // cache the underlying type
            _underlyingType = Nullable.GetUnderlyingType(typeof(T));
        }

        public override bool CanConvert(Type typeToConvert)
        {
            //return typeof(T).IsAssignableFrom(typeToConvert);
            return true;
        }

        public override T Read(ref Utf8JsonReader reader,
            Type typeToConvert, JsonSerializerOptions options)
        {
            if (_converter != null)
            {
                return _converter.Read(ref reader, _underlyingType, options);
            }

            string value = reader.GetString();

            if (String.IsNullOrEmpty(value)) return default;

            // for performance, parse with ignoreCase:false first.
            if (!Enum.TryParse(_underlyingType, value,
                ignoreCase: false, out object result)
            && !Enum.TryParse(_underlyingType, value,
                ignoreCase: true, out result))
            {
                result = null;
            }

            return (T)result;
        }

        public override void Write(Utf8JsonWriter writer,
            T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
