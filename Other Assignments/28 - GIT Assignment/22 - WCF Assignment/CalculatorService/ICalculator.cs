using System.ServiceModel;

namespace CalculatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICalculator
    {
        [OperationContract]
        int Addition(int firstNo, int SecondNo);
        [OperationContract]
        int Substraction(int firstNo, int SecondNo);

        [OperationContract]
        int Multiplication(int firstNo, int SecondNo);

        [OperationContract]
        decimal Division(int firstNo, int SecondNo); 
    }
     
}
