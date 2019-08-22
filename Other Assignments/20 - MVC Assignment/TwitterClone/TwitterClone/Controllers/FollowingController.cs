using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class FollowingController : Controller
    {
        TwitterCloneEntities db = new TwitterCloneEntities();

        public ActionResult UnFollowUser(string follower_loginUser)
        {
            char delim = '|';
            string[] substrs = follower_loginUser.Split(delim);
            string follower = substrs[0];
            following following = db.followings.Where(x => x.following_id == follower).FirstOrDefault();
            db.followings.Remove(following);
            db.SaveChanges();

            Session["user_id"] = substrs[1];
            //string str = TempData["user_id"].ToString();
            //string str1 = substrs[1];

            return RedirectToAction("Index", "Tweet", new { user_id = Session["user_id"].ToString() });
        }

        public ActionResult FollowUser(FormCollection collection)
        {
            string searchUserId = Convert.ToString(collection["searchUserId"]);
            string loginUsrId = Convert.ToString(Session["user_id"]);
            following following = new following { following_id = searchUserId, user_id = loginUsrId };
            db.followings.Add(following);
            db.SaveChanges();

            return RedirectToAction("Index", "Tweet", new { user_id = Session["user_id"].ToString() });
        }
    }
}