using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiceGame.Models;

namespace DiceGame.Controllers
{
    public class profileController : Controller
    {
        DiceModel db = new DiceModel();
        // GET: profile
        public ActionResult profileIndex()
        {
            var username = (string)Session["username"];
            User user = db.Users.Where(m => m.UserName == username).First();

            db.DesignedGames.Where(m => m.DesignerUser == username).AsEnumerable().ToList();
            return View(user);
        }

        public ActionResult EditProfile()
        {
            var username = (string)Session["username"];
            User user = db.Users.Where(m => m.UserName == username).First();
            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfiles(User user)
        {
            var username = (string)Session["username"];
            Session["username"] = null;
            User u = db.Users.Where(m => m.UserName == username).First();
            db.Users.Remove(u);
            db.SaveChanges();
            db.Users.Add(user);
            db.SaveChanges();
            Session["username"] = user.UserName;
            return RedirectToAction("profileIndex");
        }

        public ActionResult ProfileDesignedGame()
        {
            var username = (string)Session["username"];
            return View();
        }
        public ActionResult ProfileGames()
        {
            var username = (string)Session["username"];
            return View(db.FinishedGames.Where(m => m.Player1User == username || m.Player2User == username).AsEnumerable().ToList());
        }
    }
}