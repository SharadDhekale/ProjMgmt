using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HelloService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HelloService.svc or HelloService.svc.cs at the Solution Explorer and start debugging.
    public class HelloService : IHelloService
    {
        /// <summary>
        /// SayHello should take name as argument and return the wishes (Good Morning or Good Afternoon or Good Evening) based on time
        /// </summary>
        /// <param name="name">takes name as argument</param>
        /// <returns>return the wishes based on time</returns>
        public string SayHello(string name)
        {
            DateTime currentTime = DateTime.Now;
            string returnResult = string.Empty;
            try
            {
                if (currentTime.Hour >= 0 && currentTime.Hour < 12)
                {
                    returnResult = $" Good Morning, {name}";
                }
                else if (currentTime.Hour >= 12 && currentTime.Hour < 18)
                {
                    returnResult = $" Good Afternoon, {name}";
                }
                else if (currentTime.Hour >= 18)
                {
                    returnResult = $" Good Evening, {name}";
                }
            }
            catch (Exception ex)
            {
                returnResult = $"something went wrong ;( , {name} , Error Message = {ex.Message }";
            }


            return returnResult;
        }
        /// <summary>
        /// TodayProgram should take name as argument and return the Happy weekend or Enjoy Working day
        /// </summary>
        /// <param name="name">takes name as argument</param>
        /// <returns>return greetings</returns>
        public string TodayProgram(string name)
        {
            DateTime currentTime = DateTime.Now;
            string returnResult = string.Empty;
            try
            {
                if (currentTime.DayOfWeek == DayOfWeek.Saturday || currentTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    returnResult = $" Happy weekend, { name}";
                }
                else
                {
                    returnResult = $" Enjoy Working day, { name}";
                }
            }
            catch (Exception ex)
            {
                returnResult = $"something went wrong ;( , {name} , Error Message = {ex.Message }";
            }

            return returnResult;
        }
    }
}
