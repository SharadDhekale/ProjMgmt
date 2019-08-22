using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace EmployeeService
{
   
    public class EmployeeService : IEmployeeService
    {
        DBCommunicator dbObj = new DBCommunicator();
        public bool AddEmployee(Employee emp)
        {
            try
            {
                return dbObj.AddEmployee(emp);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteEmployee(int empId)
        {
            try
            {
                return dbObj.DeleteEmployee(empId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Employee RetreiveEmployeeByID(int empId)
        {
            try
            {
                return dbObj.EmployeeDetails(empId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public List<Employee> RetreiveEmployees()
        {
            try
            {
                return dbObj.EmployeeList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateEmployee(Employee emp)
        {
            try
            {
                return dbObj.UpdateEmployee(emp);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
