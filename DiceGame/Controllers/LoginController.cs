using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiceGame.Models;

namespace DiceGame.Controllers
{
    public class LoginController : Controller
    {

        private DiceModel db = new DiceModel();

        public LoginController()
        {
            
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string username , string password)
        {
            User user = db.Users.Where(u => u.UserName == username && u.Password == password).FirstOrDefault();
            if (user != null)
            {


                db.Users.Where(u => u.UserName == username && u.Password == password).First().Online = 1;
                db.SaveChanges();
              
                Session["username"] = db.Users.Where(u => u.UserName == user.UserName).First().UserName;
                Session["designedGame"] = db.DesignedGames.Where(d => d.DesignerUser == user.UserName).ToList();
                Session["finishedGame1"] = db.FinishedGames.Where(d => d.Player1User == user.UserName ).ToList();
                Session["finishedGame2"] = db.FinishedGames.Where(d => d.Player2User == user.UserName).ToList();
                Session["usr"] = user;
                Session["friends"] = user.Friends;
              //  Session["playedGame"]= _context.
                return RedirectToAction("Index", "Home");
            }
            else if (db.Admins.Where(u => u.Username == username && u.Password == password).Any())
            {
                return RedirectToAction("AdminIndex", "Home");
            }
            else
            {
                return RedirectToAction("login", "Login");
            }

            
        }

        
        public ActionResult signUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signup(User userModel)
        {
            Session["username"] = userModel.UserName;
            Session["usr"] = userModel;
            Session["friends"] = userModel.Friends;
            userModel.Online = 1;
            db.Users.Add(userModel);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            var usr = (string)Session["username"];
            User user = db.Users.Where(u => u.UserName == usr).FirstOrDefault();
            if ( user!= null)
            {
                db.Users.Where(u => u.UserName == usr).First().Online = 0;
                db.SaveChanges();

            }
            Session.Clear();
            return RedirectToAction("login");
        }




    }
}