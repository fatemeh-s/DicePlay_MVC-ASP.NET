using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiceGame.Models;

namespace DiceGame.Views.Login
{
    public class ReviewController : Controller
    {
        DiceModel db = new DiceModel();
        // GET: Review
        public ActionResult GameReviewAdminIndex()
        {
            return View(db.CommentGames.AsEnumerable().ToList());
        }
        public ActionResult UserReviewAdminIndex()
        {
            return View(db.CommentUsers.AsEnumerable().ToList());
        }
        public ActionResult GameReviewUserIndex()
        {
            return View();
        }
        public ActionResult UserReviewUserIndex()
        {
            return View();
        }
        
        public ActionResult UserAccept(int id )
        {
            UserComment com = db.CommentUsers.Find(id);
            db.CommentUsers.Remove(com);
            db.SaveChanges();
            (db.Users.Where(x => x.UserName == com.User).First()).comments.Add(com);
            db.SaveChanges();
            //IList<UserComment> list = u.comments;
            //list.Add()
            return RedirectToAction("UserReviewAdminIndex");
        }

        public ActionResult UserDecline(int id)
        {
            UserComment com = db.CommentUsers.Find(id);
            db.CommentUsers.Remove(com);
            db.SaveChanges();
            return RedirectToAction("UserReviewAdminIndex");
        }

        public ActionResult GameAccept(int id)
        {
            GameComment com = db.CommentGames.Find(id);
            db.CommentGames.Remove(com);
            db.SaveChanges();
            (db.DesignedGames.Where(x => x.Id == com.GameId).First()).Comments.Add(com);
            db.SaveChanges();
            return RedirectToAction("GameReviewAdminIndex");
        }

        public ActionResult GameDecline(int id)
        {
            GameComment com = db.CommentGames.Find(id);
            db.CommentGames.Remove(com);
            db.SaveChanges();
            return RedirectToAction("GameReviewAdminIndex");
        }
    }
}