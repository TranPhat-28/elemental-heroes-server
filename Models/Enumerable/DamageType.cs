using System.Text.Json.Serialization;

namespace elemental_heroes_server.Models.Enumerable
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DamageType
    {
        Physical = 1,
        Magic = 2
    }
}