using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiceGame.Models
{
    public class request
    {
        public int Id { get; set; }
        public int gameId { get; set; }
        public string user { get; set; }
        public DateTime requestTime { get; set; }
    }
}