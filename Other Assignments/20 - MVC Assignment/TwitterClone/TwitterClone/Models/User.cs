using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TwitterClone.Models
{
    public class User
    {
        [DisplayName("User Name:")]
        [Required(ErrorMessage ="User Name is required")]
        public string Username { get; set; }

        [DisplayName("Password:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Pwd { get; set; }

        [DisplayName("Full Name:")]
        [Required(ErrorMessage = "Full Name is required")]
        public string Fullname { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [DisplayName("Email:")]
        [Required(ErrorMessage = "Email is required")]
        public string Useremail { get; set; }
    }
}