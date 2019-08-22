using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ADOAssignment.Models
{
    public class DBCommunicator
    {
        string ConnectionString = string.Empty;
        public DBCommunicator()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["MovieDBContext"].ConnectionString;
        }

        public Customer CustomerDetails(int custId)
        {

            //SqlConnection con = new SqlConnection(ConnectionString);
            //SqlDataAdapter da = new SqlDataAdapter("GetCustomerById", con);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //DataSet ds = new DataSet();
            //da.Fill(ds, "CustomerDetails");
            //return ds;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCustomerById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@custId", custId);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Customer c = new Customer();
                    c.Custid = int.Parse(dr["Custid"].ToString());
                    c.Custname = dr["Custname"].ToString();
                    c.CustAddress = dr["CustAddress"].ToString();
                    c.DOB = DateTime.Parse(dr["DOB"].ToString());
                    c.Salary = decimal.Parse(dr["Salary"].ToString());
                    return c;
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

        public List<Customer> CustomerList()
        {

            List<Customer> custList = null;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetCustomerList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                custList = new List<Customer>();
                while (dr.Read())
                {
                    Customer c = new Customer();
                    c.Custid = int.Parse(dr["Custid"].ToString());
                    c.Custname = dr["Custname"].ToString();
                    c.CustAddress = dr["CustAddress"].ToString();
                    c.DOB = DateTime.Parse(dr["DOB"].ToString());
                    c.Salary = decimal.Parse(dr["Salary"].ToString());
                    custList.Add(c);
                }
                
                return custList;
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

        public int DeleteCustomer(int custId)
        {
            int recordCount = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DeleteCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@custId", custId);
                recordCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
            return recordCount;
        }
        public int AddCustomer(Customer customer)
        {
            int recordCount = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@custId", customer.Custid);
                cmd.Parameters.AddWithValue("@Custname", customer.Custname);
                cmd.Parameters.AddWithValue("@CustAddress", customer.CustAddress);
                cmd.Parameters.AddWithValue("@DOB", customer.DOB);
                cmd.Parameters.AddWithValue("@Salary", customer.Salary);
                recordCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
            return recordCount;
        }
        public int UpdateCustomer(Customer customer)
        {
            int recordCount = 0;
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCustomer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@custId", customer.Custid);
                cmd.Parameters.AddWithValue("@Custname", customer.Custname);
                cmd.Parameters.AddWithValue("@CustAddress", customer.CustAddress);
                cmd.Parameters.AddWithValue("@DOB", customer.DOB);
                cmd.Parameters.AddWithValue("@Salary", customer.Salary);
                recordCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
            return recordCount;
        }
    }
}