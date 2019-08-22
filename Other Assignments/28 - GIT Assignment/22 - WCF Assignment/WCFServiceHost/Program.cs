using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
namespace WCFServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost emphost = new ServiceHost(typeof(EmployeeService.EmployeeService));
            try
            {
               
                emphost.Open();
                Console.WriteLine($"Employee Service Host Started @ {DateTime.Now}");

                 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            Console.ReadLine();
            emphost.Close();

        }
    }
}
