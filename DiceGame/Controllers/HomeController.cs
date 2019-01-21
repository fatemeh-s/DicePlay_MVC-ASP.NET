using DiceGame.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiceGame.Controllers
{
    public class HomeController : Controller
    {
        DiceModel db = new DiceModel();
        public ActionResult Index()
        {
            if(db.DesignedGames != null)
                ViewBag.bestDesigner = db.DesignedGames.OrderByDescending(x => x.TotalScore).FirstOrDefault();
            if (db.DesignedGames != null)
                ViewBag.NewBestDesigner = db.DesignedGames.OrderByDescending(x => x.TotalScore).OrderByDescending(y => y.DateDesign).FirstOrDefault();
            if (db.OnlineGames != null & db.OnlineGames != null)
                ViewBag.moreOnlineGame = from t in db.OnlineGames
                                     group t by t.designedGameId into g
                                     select new
                                     {
                                         SerialNumber = g.Key,
                                         uid = (from t2 in g select t2.designedGameId).Max()
                                     };

            return View(FindOnlines());
        }
        public List<User> FindOnlines()
        {
            using (DiceModel db = new DiceModel())
            {
                var onlines = db.Users.Where(u => u.Online == 1).AsEnumerable().ToList();
                return onlines;
            }
          
        }

        public ActionResult AdminIndex()
        {
            ViewBag.bestDesigner = db.DesignedGames.OrderByDescending(x => x.TotalScore).FirstOrDefault();
            ViewBag.NewBestDesigner = db.DesignedGames.OrderByDescending(x => x.TotalScore).OrderByDescending(y => y.DateDesign).FirstOrDefault();
            ViewBag.moreOnlineGame = from t in db.OnlineGames
                                     group t by t.designedGameId into g
                                     select new
                                     {
                                         SerialNumber = g.Key,
                                         uid = (from t2 in g select t2.designedGameId).Max()
                                     };
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult DesignGame()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveGame(DesignedGame game)
        {
            game.DesignerUser = (string)Session["username"];
            game.DateDesign = DateTime.Now;
            db.DesignedGames.Add(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Slider()
        {
            return View();
        }

        public ActionResult GuestIndex()
        {
            ViewBag.bestDesigner = db.DesignedGames.OrderByDescending(x => x.TotalScore).FirstOrDefault();
            ViewBag.NewBestDesigner = db.DesignedGames.OrderByDescending(x => x.TotalScore).OrderByDescending(y => y.DateDesign).FirstOrDefault();
            ViewBag.moreOnlineGame = from t in db.OnlineGames
                                     group t by t.designedGameId into g
                                     select new
                                     {
                                         SerialNumber = g.Key,
                                         uid = (from t2 in g select t2.designedGameId).Max()
                                     };
            Session["username"] = RandomGen2.Next();
            return View(db.Users.Where(m => m.Online == 1).AsEnumerable().ToList());
        }

        public static class RandomGen2
        {
            private static Random _global = new Random();
            [ThreadStatic]
            private static Random _local;

            public static int Next()
            {
                Random inst = _local;
                if (inst == null)
                {
                    int seed;
                    lock (_global) seed = _global.Next();
                    _local = inst = new Random(seed);
                }
                return inst.Next();
            }
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        public ActionResult AllUsers()
        {
            return View(db.Users.AsEnumerable().ToList());
        }


        public ActionResult invite(int id)
        {
            var myuser = (string)Session["username"];
            string username = db.Users.Find(id).UserName;
            db.Users.Where(m => m.UserName == myuser).FirstOrDefault().Friends.Add(username);
            db.SaveChanges();
            db.Users.Where(m => m.UserName == username).FirstOrDefault().Friends.Add(myuser);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}