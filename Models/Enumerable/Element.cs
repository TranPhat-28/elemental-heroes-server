using System.Text.Json.Serialization;

namespace elemental_heroes_server.Models.Enumerable
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Element
    {
        Fire = 1,
        Water = 2,
        Wind = 3,
        Electric = 4,
        Earth = 5
    }
}