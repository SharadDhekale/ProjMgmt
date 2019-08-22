using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterClone.Models
{
    public class TweetsAndPeople
    {
        public List<Tweet> Tweets { get; set; }
        public List<Person> People { get; set; }
        public List<FollowingAndFollowers> UsersYouFollow { get; set; }
        public List<FollowingAndFollowers> UsersFollowYou { get; set; }
    }
}