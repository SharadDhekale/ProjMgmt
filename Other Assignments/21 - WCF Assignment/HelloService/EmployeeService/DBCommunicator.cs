using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService
{
    public class DBCommunicator
    {
        string ConnectionString = string.Empty;
        public DBCommunicator()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["CnnDBContext"].ConnectionString;
        }

        public List<Employee> EmployeeList()
        {
            List<Employee> empList = new List<Employee>();
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                string sqlQuery = "SELECT EmpNo,FirstName,LastName,Dept FROM [DotNetAssignments].[dbo].[EmpDetails] ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Employee e = new Employee();
                    e.EmpNumber = int.Parse(dr["EmpNo"].ToString());
                    e.FirstName = dr["FirstName"].ToString();
                    e.LastName = dr["LastName"].ToString();
                    e.Dept = dr["Dept"].ToString();
                    empList.Add(e);
                }
                return empList;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public Employee EmployeeDetails(int empId)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                string sqlQuery = "SELECT EmpNo,FirstName,LastName,Dept FROM [DotNetAssignments].[dbo].[EmpDetails] WHERE EmpNo= @empId";
                SqlCommand cmd = new SqlCommand(sqlQuery, con)
                {
                    //SqlCommand cmd = new SqlCommand("GetCustomerById", con);
                    CommandType = CommandType.Text
                };
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@empId",
                    Value = empId
                };
                cmd.Parameters.Add(param);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Employee e = new Employee();
                    e.EmpNumber = int.Parse(dr["EmpNo"].ToString());
                    e.FirstName = dr["FirstName"].ToString();
                    e.LastName = dr["LastName"].ToString();
                    e.Dept = dr["Dept"].ToString();
                    return e;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool DeleteEmployee(int empId)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                string sqlQuery = "DELETE FROM [DotNetAssignments].[dbo].[EmpDetails] WHERE EmpNo= @empId";
                SqlCommand cmd = new SqlCommand(sqlQuery, con)
                {
                    //SqlCommand cmd = new SqlCommand("GetCustomerById", con);
                    CommandType = CommandType.Text
                };
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@empId",
                    Value = empId
                };
                cmd.Parameters.Add(param);
                int noOfRows = cmd.ExecuteNonQuery();
                return noOfRows > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AddEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                string sqlQuery = $"INSERT INTO [DotNetAssignments].[dbo].[EmpDetails]" +
                    $"(EmpNumber,FirstName,LastName,Dept) " +
                    $"VALUES (@EmpNumber,@FirstName,@LastName,@Dept)";
                SqlCommand cmd = new SqlCommand(sqlQuery, con)
                {
                    CommandType = CommandType.Text
                };
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@EmpNumber",
                    Value = emp.EmpNumber
                };
                cmd.Parameters.Add(param);
                param = new SqlParameter()
                {
                    ParameterName = "@FirstName",
                    Value = emp.FirstName
                };
                cmd.Parameters.Add(param);
                param = new SqlParameter()
                {
                    ParameterName = "@LastName",
                    Value = emp.LastName
                };
                cmd.Parameters.Add(param);
                param = new SqlParameter()
                {
                    ParameterName = "@Dept",
                    Value = emp.Dept
                };
                cmd.Parameters.Add(param);

                int noOfRows = cmd.ExecuteNonQuery();
                return noOfRows > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public bool UpdateEmployee(Employee emp)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                string sqlQuery = $"UPDATE [DotNetAssignments].[dbo].[EmpDetails]" +
                    $"SET EmpNumber=@EmpNumber,FirstName=@FirstName,LastName=@LastName,Dept=@Dept) "  ;
                SqlCommand cmd = new SqlCommand(sqlQuery, con)
                {
                    CommandType = CommandType.Text
                };
                SqlParameter param = new SqlParameter()
                {
                    ParameterName = "@EmpNumber",
                    Value = emp.EmpNumber
                };
                cmd.Parameters.Add(param);
                param = new SqlParameter()
                {
                    ParameterName = "@FirstName",
                    Value = emp.FirstName
                };
                cmd.Parameters.Add(param);
                param = new SqlParameter()
                {
                    ParameterName = "@LastName",
                    Value = emp.LastName
                };
                cmd.Parameters.Add(param);
                param = new SqlParameter()
                {
                    ParameterName = "@Dept",
                    Value = emp.Dept
                };
                cmd.Parameters.Add(param);

                int noOfRows = cmd.ExecuteNonQuery();
                return noOfRows > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
