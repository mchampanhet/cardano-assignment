using Cardano.Enums;
using Cardano.JsonConverters;
using System.Text.Json.Serialization;

namespace Cardano.Models
{
    internal class GleifResponseLegalAddress
    {
        [JsonConverter(typeof(EnumConverter<CountryEnum?>))]
        public CountryEnum? country { get; set; }
    }
}
