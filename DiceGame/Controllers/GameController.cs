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
        public ActionResult GameIndex(int id)
        {

            OnlineGame ong = db.OnlineGames.Find(id);
            DesignedGame dg = db.DesignedGames.Find(ong.designedGameId);
            var viewModel = new GameViewModel
            {
                onlineGame = ong,
                designedGame = dg
            };
            if(dg.DiceCount == 2)
            {
                return RedirectToAction("GameIndex2");
            }
            
            return View(ong);
        }

        public ActionResult GameIndex2(int id)
        {

            OnlineGame ong = db.OnlineGames.Find(id);
            DesignedGame dg = db.DesignedGames.Find(ong.designedGameId);
            var viewModel = new GameViewModel
            {
                onlineGame = ong,
                designedGame = dg
            };

            return View(viewModel);
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

        
        public ActionResult requestGame(int id)
        {
            
            request v = db.requests.Where(m => m.gameId == id).FirstOrDefault();
            
            if(v == null)
            {
                request req = new request();
                req.gameId = id;
                req.user = (string)Session["username"];
                req.requestTime = DateTime.Now;
                db.requests.Add(req);
                db.SaveChanges();

    //            return RedirectToAction("waitGmae", new RouteValueDictionary(
    //new { controller = "Game", action = "waitGmae", id = id })); 
            }
            else
            {
                OnlineGame ong = new OnlineGame();
                DesignedGame game = db.DesignedGames.Find(id);
                db.DesignedGames.Find(id).CountPlayed += 1;
                ong.designedGameId = id;
                ong.Player1User = v.user;
                ong.Player2User = (string)Session["username"];
                ong.game = game;
                db.OnlineGames.Add(ong);
                db.SaveChanges();

    //            return RedirectToAction("GameIndex" , new RouteValueDictionary(
    //new { controller = "Game", action = "GameIndex", OnlineGame = game }));
            }
            return RedirectToAction("AllGames");
            
        }

    //    public ActionResult waitGmae(int id)
    //    {
    //        var v = db.requests.Where(m => m.gameId == id).FirstOrDefault();
    //        System.Threading.Thread.Sleep(2000);
    //        var us = (string)Session["username"];
    //        OnlineGame ongame = db.OnlineGames.Where(m => m.Player1User == us & m.game.Id == id).FirstOrDefault();
    //        if(ongame == null)
    //        {

    //            request req = db.requests.Where(m => m.user == us & m.gameId == id).FirstOrDefault();
    //            db.requests.Remove(req);
    //            return RedirectToAction("AllGames");
    //        }
    //        else
    //        {
    //            OnlineGame ong = new OnlineGame();
    //            DesignedGame game = db.DesignedGames.Find(id);
    //            ong.game.Id = id;
    //            ong.Player1User = v.user;
    //            ong.Player2User = (string)Session["username"];
    //            ong.game = game;
    //            return RedirectToAction("GameIndex", new RouteValueDictionary(
    //new { controller = "Game", action = "GameIndex", OnlineGame = game }));
    //        }

    //    }

        public ActionResult myOnlineGame()
        {
            var user = (string)Session["username"];
            return View(db.OnlineGames.Where(m => m.Player1User == user || m.Player2User == user).AsEnumerable().ToList());
        }

        public ActionResult GiveReviewUser()
        {
            return View();
        }
        public ActionResult GiveReviewGame()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveReviewGame(GameComment gaco)
        {
            gaco.CommenterUser = (string)Session["username"];
            gaco.date = DateTime.Now;
            db.CommentGames.Add(gaco);
            db.SaveChanges();
            db.DesignedGames.Find(gaco.Id).TotalScore += gaco.rate;
            db.SaveChanges();
            db.DesignedGames.Find(gaco.Id).numScore += 1;
            db.SaveChanges();
            db.DesignedGames.Find(gaco.Id).AverageScore = gaco.rate / db.DesignedGames.Find(gaco.Id).numScore;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SaveReviewUser(UserComment usco)
        {
            usco.CommenterUser = (string)Session["username"];
            int id = db.Users.Where(m => m.UserName == usco.User).FirstOrDefault().Id;
            usco.date = DateTime.Now;
            db.CommentUsers.Add(usco);
            db.SaveChanges();
            db.Users.Find(id).RateTotal += usco.rate;
            db.SaveChanges();
            db.Users.Find(id).RateNum += 1;
            db.SaveChanges();
            db.Users.Find(id).RateMean = usco.rate / db.DesignedGames.Find(usco.Id).numScore;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


    }
}