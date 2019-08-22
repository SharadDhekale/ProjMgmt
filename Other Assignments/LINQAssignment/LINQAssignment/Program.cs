using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQAssignment
{
    class Program
    {
        static List<Order> orderList = new List<Order> {
                new Order(){ Id=1, ItemName="Item1", OrderDate= Convert.ToDateTime("02/01/2018"), Quantity=3 },
                new Order(){ Id=2, ItemName="Item5", OrderDate= Convert.ToDateTime("05/05/2019"), Quantity=10 },
                new Order(){ Id=3, ItemName="Item3", OrderDate= Convert.ToDateTime("02/01/2019"), Quantity=5 },
                new Order(){ Id=4, ItemName="Item3", OrderDate= Convert.ToDateTime("04/03/2019"), Quantity=7 },
                new Order(){ Id=5, ItemName="Item1",OrderDate= Convert.ToDateTime("02/09/2019"), Quantity=9 },
                new Order(){ Id=6, ItemName="Item1", OrderDate= Convert.ToDateTime("04/01/2019"), Quantity=5 },
                new Order(){ Id=7, ItemName="Item5", OrderDate= Convert.ToDateTime("02/03/2019"), Quantity=7 },
                new Order(){ Id=8, ItemName="Item3", OrderDate= Convert.ToDateTime("05/09/2019"), Quantity=9 },
                 new Order(){ Id=9, ItemName="Item9", OrderDate= Convert.ToDateTime("05/09/2019"), Quantity=90 },
                new Order(){ Id=10, ItemName="Item0", OrderDate= Convert.ToDateTime("05/09/2019"), Quantity=0 },
            };
        static List<Item> ItemList = new List<Item> {
                new Item(){ ItemName="Item1", Price=10.5M },
                new Item(){ ItemName="Item3", Price=30 },
                new Item(){ ItemName="Item5", Price=50.10M},
            };

        static int[] intArry = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        static void Main(string[] args)
        {
            Console.WriteLine("Order List before Grouping and sorting");
            foreach (var item in orderList)
            {
                Console.WriteLine($" Item ID ={ item.Id} , " +
                    $"Name ={item.ItemName}, " +
                    $"Order Date ={item.OrderDate}, " +
                    $"Quatity={item.Quantity} ");
            }
            //Console.WriteLine(" Int Array Item");
            //foreach (var item in intArry)
            //{
            //    Console.Write($"{item},");
            //}
            Console.WriteLine("");
            //DisplayEvenNumbers();
             CubeFinder();
            //DisplayOrdersDatenQty();
            //DisplayOrdersGrpByMonth();
            //DisplayOrdersPrice();
            // LargetsQty();
            // Console.WriteLine("");

            //OrderBeforeJan();
            Console.ReadLine();
        }

        private static void DisplayEvenNumbers()
        {
            var filter = intArry.Where(x => x % 2 == 0).ToList();
            Console.WriteLine($"Total Number of Even Number = { filter.Count()}");
            foreach (var item in filter)
            {
                Console.Write($" {item},");
            }
        }
        private static void OrderBeforeJan()
        {
            Console.WriteLine("Find if there are any orders placed before Jan of this year");
            var filter = orderList.Where(x => x.OrderDate < Convert.ToDateTime("01/01/2019"));
            Console.WriteLine("");
            Console.WriteLine("Total Number of Orders=" + filter.Count());

            //foreach (var item in filter)
            //{
            //    Console.WriteLine($" Item ID ={ item.Id} , " +
            //        $"Name ={item.ItemName}, " +
            //        $"Order Date ={item.OrderDate}, " +
            //        $"Quatity={item.Quantity} ");
            //}
        }
        private static void LargetsQty()
        {
            Console.WriteLine("Display largest quantity in a single order");
            orderList.Reverse();
            var largestQty = orderList.Where(x => x.Quantity > 0).FirstOrDefault();

            Console.WriteLine($"  Item Name={largestQty.ItemName} Quantity={ largestQty.Quantity}");
        }
        private static void DisplayOrdersPrice()
        {

            Console.WriteLine("Item List");

            foreach (var item in ItemList)
            {
                Console.WriteLine($" Item Name ={ item.ItemName} , " +
                 $"Price ={item.Price}, "
               );
            }
            Console.WriteLine("");


            var orderedList = (from ol in orderList
                               group ol by ol.OrderDate.Month.ToString() into oList
                               select new { Month = oList.Key, oList }).ToList();
            Console.WriteLine("");
            Console.WriteLine("Order List After Grouping and sorting");
            for (int i = 0; i < orderedList.Count(); i++)
            {
                Console.WriteLine($" Month ={ orderedList[i].Month}");
                var orderByDate = orderedList[i].oList
                    .OrderByDescending(x => x.OrderDate)
                    .Join(ItemList, li => li.ItemName, itm => itm.ItemName,
                            (li, itm) => new
                            {
                                OrderId = li.Id,
                                ItemName = itm.ItemName,
                                OrderDate = li.OrderDate,
                                TotalPrice = li.Quantity * itm.Price
                            });
                ;
                foreach (var item in orderByDate)
                {
                    Console.WriteLine($" Order ID ={ item.OrderId} , " +
                  $"Item Name ={item.ItemName}, " +
                  $"Order Date ={item.OrderDate}, " +
                  $"Total Price={item.TotalPrice} ");
                }
                Console.WriteLine("");
            }

        }
        private static void DisplayOrdersGrpByMonth()
        {
            List<Order> orderList = new List<Order> {
                new Order(){ Id=1, ItemName="Item1", OrderDate= Convert.ToDateTime("02/01/2019"), Quantity=3 },
                new Order(){ Id=2, ItemName="Item5", OrderDate= Convert.ToDateTime("05/05/2019"), Quantity=10 },
                new Order(){ Id=3, ItemName="Item8", OrderDate= Convert.ToDateTime("02/01/2019"), Quantity=5 },
                new Order(){ Id=4, ItemName="Item3", OrderDate= Convert.ToDateTime("04/03/2019"), Quantity=7 },
                new Order(){ Id=5, ItemName="Item15", OrderDate= Convert.ToDateTime("02/09/2019"), Quantity=9 },
                new Order(){ Id=6, ItemName="Item7", OrderDate= Convert.ToDateTime("04/01/2019"), Quantity=5 },
                new Order(){ Id=7, ItemName="Item6", OrderDate= Convert.ToDateTime("02/03/2019"), Quantity=7 },
                new Order(){ Id=8, ItemName="Item10", OrderDate= Convert.ToDateTime("05/09/2019"), Quantity=9 },
            };
            Console.WriteLine("Order List before Grouping and sorting");
            foreach (var item in orderList)
            {
                Console.WriteLine($" Item ID ={ item.Id} , " +
                    $"Name ={item.ItemName}, " +
                    $"Order Date ={item.OrderDate}, " +
                    $"Quatity={item.Quantity} ");
            }
            var orderedList = (from i in orderList
                               group i by i.OrderDate.Month.ToString() into oList
                               select new { Month = oList.Key, oList }).ToList();
            Console.WriteLine("");
            Console.WriteLine("Order List After Grouping and sorting");
            for (int i = 0; i < orderedList.Count(); i++)
            {
                Console.WriteLine($" Month ={ orderedList[i].Month}");
                var orderByDate = orderedList[i].oList.OrderByDescending(x => x.OrderDate);
                foreach (var item in orderedList[i].oList)
                {
                    Console.WriteLine($" Item ID ={ item.Id} , " +
                  $"Name ={item.ItemName}, " +
                  $"Order Date ={item.OrderDate}, " +
                  $"Quatity={item.Quantity} ");
                }
                Console.WriteLine("");
            }

        }

        /// <summary>
        /// 3.Create an Order class that has order id, item name, order date and quantity. 
        /// Create a collection of Order objects.
        /// Display the data day wise from most recently ordered to
        ///least recently ordered and by quantity from highest to lowest.
        /// </summary>
        private static void DisplayOrdersDatenQty()
        {
            List<Order> orderList = new List<Order> {
                new Order(){ Id=1, ItemName="Item1", OrderDate= Convert.ToDateTime("02/01/2019"), Quantity=3 },
                new Order(){ Id=2, ItemName="Item5", OrderDate= Convert.ToDateTime("02/05/2019"), Quantity=10 },
                new Order(){ Id=3, ItemName="Item8", OrderDate= Convert.ToDateTime("02/01/2019"), Quantity=5 },
                new Order(){ Id=4, ItemName="Item3", OrderDate= Convert.ToDateTime("02/03/2019"), Quantity=7 },
                new Order(){ Id=5, ItemName="Item15", OrderDate= Convert.ToDateTime("02/09/2019"), Quantity=9 },

            };
            Console.WriteLine("Order List before sort");
            foreach (var item in orderList)
            {
                Console.WriteLine($" Item ID ={ item.Id} , " +
                    $"Name ={item.ItemName}, " +
                    $"Order Date ={item.OrderDate}, " +
                    $"Quatity={item.Quantity} ");
            }

            var orderbyQty = orderList
                            .OrderByDescending(x => x.OrderDate)
                            .ThenByDescending(x => x.Quantity);
            Console.WriteLine("Order List After sort");
            foreach (var item in orderbyQty)
            {
                Console.WriteLine($" Item ID ={ item.Id} , " +
                    $"Name ={item.ItemName}, " +
                    $"Order Date ={item.OrderDate}, " +
                    $"Quatity={item.Quantity} ");
            }
        }

        /// <summary>
        /// Que#1
        /// </summary>
        private static void CubeFinder()
        {
            Console.WriteLine("Array elements : ");
            int[] intarry = new int[] { 10, 4, 8, 3, 9, 45, 11, 20 };
            foreach (var item in intarry)
            {
                Console.Write($"{item }, ");
            }
            Console.WriteLine("");

            var filter = from i in intarry
                         let cube = (i * i * i)
                         where cube > 100 && cube < 1000
                         select new { ArrayItem = i, cube };

            Console.WriteLine("Find the cube of the numbers that are greater than 100 but less than 1000 using LINQ");
            foreach (var item in filter)
            {
                Console.WriteLine($" Number= {item.ArrayItem} :: Cube ={item.cube}");
            }
        }
    }
}
