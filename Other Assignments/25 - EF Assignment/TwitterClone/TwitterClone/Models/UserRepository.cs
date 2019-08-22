using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterClone.Models
{
    public class UserRepository
    {
        static List<User> list = new List<User>()
        {
            new User(){ Username="u101",Pwd="123",Fullname="user 101",Useremail="u101@gmail.com"},
            new User(){ Username="u102",Pwd="123",Fullname="user 102",Useremail="u102@gmail.com"},
            new User(){ Username="u103",Pwd="123",Fullname="user 103",Useremail="u103@gmail.com"},
            new User(){ Username="u104",Pwd="123",Fullname="user 104",Useremail="u104@gmail.com"}
        };

        public List<User> GetAll()
        {
            return list;
        }

        public User ValidateUser(string uname, string pwd)
        {
            User userinfo = list.SingleOrDefault(i => i.Username == uname && i.Pwd == pwd);

            return userinfo;
        }

        public void Add(User user)
        {
            list.Add(user);
        }
    }
}