using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TwitterClone.Models
{
    public class FollowingAndFollowers
    {
        public string follower_id { get; set; }
        [DisplayName("Twitter User")]
        public string fullname { get; set; }
        public string followingType { get; set; }
        public string user_id { get; set; }
    }
}