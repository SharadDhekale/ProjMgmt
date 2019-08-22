using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHelloService" in both code and config file together.
    [ServiceContract]
    public interface IHelloService
    {
        /// <summary>
        /// SayHello should take name as argument and return the wishes (Good Morning or Good Afternoon or Good Evening) based on time
        /// </summary>
        /// <param name="name">takes name as argument</param>
        /// <returns>return the wishes based on time</returns>
        [OperationContract]
        string SayHello(string name);

        /// <summary>
        /// TodayProgram should take name as argument and return the Happy weekend or Enjoy Working day
        /// </summary>
        /// <param name="name">takes name as argument</param>
        /// <returns>return greetings</returns>
        [OperationContract]
        string TodayProgram(string name);

    }
 
}
