using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elemental_heroes_server.Models
{
    public class SingleplayerTurnResult
    {
        public int TurnNo { get; set; } = 0;
        public int PlayerInitHp { get; set; } = 0;
        public int BotInitHp { get; set; } = 0;
        public int PlayerDamageDealt { get; set; } = 0;
        public int BotDamageDealt { get; set; } = 0;
        public int PlayerRemainingHp { get; set; } = 0;
        public int BotRemainingHp { get; set; } = 0;
    }
}