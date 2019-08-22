using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _18ADOAssignment.Models
{
    public class SupplierInfo
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal ContactNo { get; set; }
        public string Email { get; set; }
    }
}