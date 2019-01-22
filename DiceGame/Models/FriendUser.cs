using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiceGame.Models
{
    public class FriendUser
    {
        public IEnumerable< User> user { get; set; }
        public IEnumerable<Friends> friends { get; set; }
    }
}