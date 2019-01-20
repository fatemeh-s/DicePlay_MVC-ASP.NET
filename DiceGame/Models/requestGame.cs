using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiceGame.Models
{
    public class requestGame
    {
        public requestGame()
        {

        }

        public int GameId { get; set; }
        public string user { get; set; }
        public TimeZone timeOfRequest { get; set; }
    }
}