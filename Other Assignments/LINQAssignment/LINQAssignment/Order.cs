using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQAssignment
{
   public class Order : IComparable<Order>
    {
        //id, item name, order date and quantity
        public int Id { get; set; }
        public string ItemName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }

        public int CompareTo(Order other)
        {
            return this.Quantity.CompareTo(other.Quantity);
        }
    }
}
