using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _18ADOAssignment.Models
{
    public class DBCommunicator
    {
        private string _connecitoinString;
        public DBCommunicator()
        {
            _connecitoinString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        }

        public List<SupplierInfo> GetSupplierList()
        {
            string sqlString = "SELECT [SupplierId] ,[SupplierName],[Address],[City],[ContactNo],[Email] FROM [dbo].[SupplierInfo]";
            List<SupplierInfo> result = new List<SupplierInfo>();
            using (SqlConnection connection =  new SqlConnection(_connecitoinString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(sqlString, connection);
                //command.Parameters.AddWithValue("@Param1", paramValue);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new SupplierInfo()
                        {
                            SupplierId = (int)reader["SupplierId"],
                            SupplierName = (string)reader["SupplierName"],
                            City = (string)reader["Address"],
                            Address = (string)reader["City"],
                            ContactNo = (decimal)reader["ContactNo"],
                            Email = (string)reader["Email"],
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
             
            return result;
        }

        public List<ProductDetails> GetProductsList()
        {
            string sqlString = "SELECT  [ProductId],[ProductName],[SupplierId] FROM [dbo].[ProductDetails]";
            List<ProductDetails> result = new List<ProductDetails>();
            using (SqlConnection connection = new SqlConnection(_connecitoinString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(sqlString, connection);
                //command.Parameters.AddWithValue("@Param1", paramValue);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new ProductDetails()
                        {
                            ProductId = (int)reader["ProductId"],
                            ProductName = (string)reader["ProductName"],
                            SupplierId = (int)reader["SupplierId"],
                             
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="sqlParamerters"></param>
        /// <returns></returns>
        private SqlDataReader GetExceuteReader(string sqlString, SqlParameter[] sqlParamerters = null)
        {
            using (SqlConnection connection =
           new SqlConnection(_connecitoinString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(sqlString, connection);
                //command.Parameters.AddWithValue("@Param1", paramValue);
                if (sqlParamerters != null)
                {
                    command.Parameters.AddRange(sqlParamerters);
                }
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    return reader;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

    }
}