using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TwitterClone.Models;

namespace TwitterClone.Controllers
{
    public class TweetController : Controller
    {
        TwitterCloneEntities db = new TwitterCloneEntities();
        TweetsAndPeople tp = new TweetsAndPeople();

        // GET: Tweet
        public ActionResult Index(string user_id)
        {
            if (!string.IsNullOrEmpty(user_id))
            {
                TempData["USERS_FOLLOWING"] = db.followings.Count(x => x.user_id == user_id);
                TempData["USERS_FOLLOWING_YOU"] = db.followings.Count(x => x.following_id == user_id);
                TempData["NO_OF_TWEETS"] = db.Tweets.Count(x => x.user_id == user_id);
                TempData["user_id"] = user_id;

                Session["user_id"] = user_id;
                Session["FullName"] = db.People.Where(x => x.user_id == user_id).FirstOrDefault().fullName;

                List<Tweet> listOfTweets = db.Tweets.Where(x => x.user_id == user_id).ToList();

                List<following> listOfUsersYouFollow = db.followings.Where(x => x.user_id == user_id).ToList();
                //List<Person> listOfUsersYouFollow_List = new List<Person>();
                List<FollowingAndFollowers> listOfUsersYouFollow_List = new List<FollowingAndFollowers>();
                foreach (var foll in listOfUsersYouFollow)
                {
                    //listOfUsersYouFollow_List.Add(db.People.Where(x => x.user_id == foll.following_id).FirstOrDefault());
                    Person p = db.People.Where(x => x.user_id == foll.following_id || x.fullName == foll.following_id).FirstOrDefault();
                    listOfUsersYouFollow_List.Add(new FollowingAndFollowers { follower_id = p.user_id, user_id = user_id, fullname = p.fullName, followingType = "YOU_FOLLOW" });
                }

                List<following> listOfUsersFollowYou = db.followings.Where(x => x.following_id == user_id).ToList();
                //List<Person> listOfUsersFollowYou_List = new List<Person>();
                List<FollowingAndFollowers> listOfUsersFollowYou_List = new List<FollowingAndFollowers>();
                foreach (var foll in listOfUsersFollowYou)
                {
                    //listOfUsersFollowYou_List.Add(db.People.Where(x => x.user_id == foll.user_id).FirstOrDefault());
                    Person p = db.People.Where(x => x.user_id == foll.user_id).FirstOrDefault();
                    listOfUsersFollowYou_List.Add(new FollowingAndFollowers { user_id = p.user_id, fullname = p.fullName, followingType = "FOLLOW_YOU" });
                }

                List<Person> listOfTwitterUsers = db.People.Where(x => x.active == true).ToList();

                tp.Tweets = listOfTweets;
                tp.People = listOfTwitterUsers;
                tp.UsersYouFollow = listOfUsersYouFollow_List;
                tp.UsersFollowYou = listOfUsersFollowYou_List;
                return View(tp);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        public ActionResult GetTwitterUsers()
        {
            return View();
        }

        // GET: Tweet/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tweet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tweet/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "message,created")] Tweet twt)
        {
            try
            {
                // TODO: Add insert logic here
                Tweet tweet = new Tweet();
                tweet.user_id = Convert.ToString(Session["user_id"]);
                tweet.message = twt.message;
                tweet.created = twt.created;
                db.Tweets.Add(tweet);
                db.SaveChanges();

                return RedirectToAction("Index", "Tweet", new { user_id = Convert.ToString(Session["user_id"]) });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(FormCollection collection) //int id, string tweetText
        {
            try
            {
                // TODO: Add update logic here
                int tweetId = Convert.ToInt32(collection["tweetId"]);
                Tweet tweet = db.Tweets.Where(x => x.tweet_id == tweetId).FirstOrDefault();
                Tweet newTweet = new Tweet { user_id = Session["user_id"].ToString(), message = collection["editTweet"] };
                db.Tweets.Remove(tweet);
                db.Tweets.Add(newTweet);
                db.SaveChanges();

                return RedirectToAction("Index", "Tweet", new { user_id = Convert.ToString(Session["user_id"]) });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                Tweet tweet = db.Tweets.Where(x => x.tweet_id == id).FirstOrDefault();
                db.Tweets.Remove(tweet);
                db.SaveChanges();

                return RedirectToAction("Index", "Tweet", new { user_id = Convert.ToString(Session["user_id"]) });
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestTweet()
        {
            var phone = new Tweet();
            return PartialView("TestTweet", phone);
        }
    }
}
