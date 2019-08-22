using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOAssignment.Models
{
    public class Customer
    {
        public int Custid { get; set; }
        public string Custname { get; set; }
        public string CustAddress { get; set; }
        public DateTime DOB { get; set; }
        public decimal Salary { get; set; }
    }
}