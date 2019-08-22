using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMVCClient.Models
{
    public class HelloRequest
    {
        public int wcfOperations { get; set; }
        public string Name { get; set; }
    }
}