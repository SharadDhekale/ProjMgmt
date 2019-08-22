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

            try
            {
                ServiceHost hellohost = new ServiceHost(typeof(HelloService.HelloService));
                hellohost.Open();
                Console.WriteLine($"WCF Hello Service Host Started @ {DateTime.Now}");

                ServiceHost calchost = new ServiceHost(typeof(CalculatorService.Calculator));
                calchost.Open();
                Console.WriteLine($"WCF Calculator Service Host Started @ {DateTime.Now}");
                Console.ReadLine();
                hellohost.Close();
                calchost.Close();
            }
            catch (Exception)
            {

                throw;
            }
            

        }
    }
}
