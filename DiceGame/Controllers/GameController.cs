using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiceGame.Models;

namespace DiceGame.Controllers
{
    public class GameController : Controller
    {
        DiceModel db = new DiceModel();
        // GET: Game
        public ActionResult GameIndex()
        {
            db.OnlineGames.Find(1).Score1 = 10;
            db.SaveChanges();
            Session["gameID"] = 1;
            return View();
        }

        [HttpPost]
        public void updateGame(int current0,int current1, int total0, int total1,int turn )
        {


            var id = Session["gameID"];
            db.OnlineGames.Find(1).Current1= current0;
            db.OnlineGames.Find(1).Current2 = current1;
            db.OnlineGames.Find(1).Score1 = total1;
            db.OnlineGames.Find(1).Score2 = total0;
            db.OnlineGames.Find(1).Turn = turn;
            db.SaveChanges();

            
        }
    }
}