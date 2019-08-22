using System;
using System.ServiceModel;

namespace WeatherService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWeatherInfo
    {
        [OperationContract]
        Double celciustofarenheit(double temp);

        [OperationContract]
        Double farenheittocelcius(double temp);

     }
     
}
