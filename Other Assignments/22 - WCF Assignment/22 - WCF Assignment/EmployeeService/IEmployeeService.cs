using System.Collections.Generic;
using System.ServiceModel;
namespace EmployeeService
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        bool AddEmployee(Employee emp);

        [OperationContract]
        List<Employee> RetreiveEmployees();

        [OperationContract]
        Employee RetreiveEmployeeByID(int empId);

        [OperationContract]
        bool UpdateEmployee(Employee emp);

        [OperationContract]
        bool DeleteEmployee(int empId);

    }
}
