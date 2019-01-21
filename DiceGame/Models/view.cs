using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiceGame.Models
{
    public class view
    {

        public FinishedGame FinishedGames { get; set; }
        public User user { set; get; }
        public IEnumerable<DesignedGame> DesignedGames { get; set; }

    }
}