using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DiceGame.Models;

namespace DiceGame.Controllers
{
    public class GameController : Controller
    {
        DiceModel db = new DiceModel();
        // GET: Game
        public ActionResult GameIndex(OnlineGame game)
        {
            return View(game);
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

        public ActionResult AllGames()
        {
            return View(db.DesignedGames.AsEnumerable().ToList());
        }

        [HttpPost]
        public ActionResult requestGame(int id)
        {
            return RedirectToAction("GameIndex");
            var v = db.requests.Where(m => m.gameId == id).First();
            
            if(v == null)
            {
                request req = new request();
                req.gameId = id;
                req.user = (string)Session["username"];
                req.requestTime = DateTime.Now;
                db.requests.Add(req);
                db.SaveChanges();
                return RedirectToAction("waitGmae");
            }
            else
            {
                OnlineGame ong = new OnlineGame();
                DesignedGame game = db.DesignedGames.Find(id);
                ong.DesignedGameId = id;
                ong.Player1User = v.user;
                ong.Player2User = (string)Session["username"];
                ong.game = game;

                return RedirectToAction("GameIndex" , new RouteValueDictionary(
    new { controller = "Game", action = "GameIndex", OnlineGame = game }));
            }
            
        }

        public ActionResult waitGmae(int id)
        {
            var v = db.requests.Where(m => m.gameId == id).First();
            System.Threading.Thread.Sleep(2000);
            OnlineGame ongame = db.OnlineGames.Where(m => m.Player1User == (string)Session["username"] & m.DesignedGameId == id).First();
            if(ongame == null)
            {

                request req = db.requests.Where(m => m.user == (string)Session["username"] & m.gameId == id).First();
                db.requests.Remove(req);
                return RedirectToAction("AllGames");
            }
            else
            {
                OnlineGame ong = new OnlineGame();
                DesignedGame game = db.DesignedGames.Find(id);
                ong.DesignedGameId = id;
                ong.Player1User = v.user;
                ong.Player2User = (string)Session["username"];
                ong.game = game;
                return RedirectToAction("GameIndex", new RouteValueDictionary(
    new { controller = "Game", action = "GameIndex", OnlineGame = game }));
            }

        }
    }
}