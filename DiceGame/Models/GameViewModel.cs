using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiceGame.Models
{
    public class GameViewModel
    {
        public DesignedGame designedGame { get; set; }
        public OnlineGame onlineGame { get; set; }
    }
}