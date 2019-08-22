using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherSericeClient
{
    class Program
    {
        static void Main(string[] args)
        {

            string userinput = "Y";
            while (userinput.Trim().ToUpper() == "Y")
            {
                TempConvertor();
                Console.WriteLine("Do you Want to Continue? Y/N");
                userinput = Console.ReadLine();
            }
         }

        static void TempConvertor()
        {
            try
            {
                WeatherService.WeatherInfoClient _client = new WeatherService.WeatherInfoClient();
                Console.WriteLine("Temprature Convertor");
                Console.WriteLine("Select Conversion");
                Console.WriteLine("1. celcius To Farenheit");
                Console.WriteLine("2. Farenheit To celcius ");
                int option = int.Parse(Console.ReadLine());
                if (option == 1)
                {
                    Console.WriteLine("Enter Temp in Clcius");
                    double temp = double.Parse(Console.ReadLine());
                    var response = _client.celciustofarenheit(temp);
                    Console.WriteLine("Temp in Farenheit : " + response);
                }
                else if (option == 2)
                {
                    Console.WriteLine("Enter Temp in Farenheit ");
                    double temp = double.Parse(Console.ReadLine());
                    var response = _client.farenheittocelcius(temp);
                    Console.WriteLine("Temp in Clcius : " + response);
                }
                else
                {
                    Console.WriteLine("Enter correct Option");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something wrong with Service/ Option selection " + ex.Message);
            }
        }
    }
}
