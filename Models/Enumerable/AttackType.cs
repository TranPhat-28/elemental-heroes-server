using System.Text.Json.Serialization;

namespace elemental_heroes_server.Models.Enumerable
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttackType
    {
        Melee = 1,
        Ranged = 2
    }
}