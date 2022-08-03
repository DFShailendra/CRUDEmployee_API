using API.model;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace API.DataAccessLayer
{
    public class EmployeeDAL
    {
        IConfiguration _configuration;
        public string connectionstring;
        public EmployeeDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionstring = _configuration["ConnectionStrings:DefaultConnection"];
        }

        public DataTable GetAllEmployee()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("SP_Employee_GetAll", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dataSet);
                        dataTable = dataSet.Tables[0];
                        conn.Close();
                        scope.Complete();
                    }
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw new System.Exception("TransactionAbortedException Message: {0}", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataTable;
        }


        public Employee GetEmployeeById(int ID)
        {
            Employee objEmployee = new Employee();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("SP_Employee_GetById", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmployeeId", ID);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader["EmployeeId"] != DBNull.Value) { objEmployee.EmployeeId = (int)reader["EmployeeId"]; }
                                if (reader["Name"] != DBNull.Value) { objEmployee.Name = (string)reader["Name"]; }
                                if (reader["City"] != DBNull.Value) { objEmployee.City = (string)reader["City"]; }
                                if (reader["Department"] != DBNull.Value) { objEmployee.Department = (int)reader["Department"]; }
                                if (reader["Gender"] != DBNull.Value) { objEmployee.Gender = (int)reader["Gender"]; }
                                if (reader["PhoneNumber"] != DBNull.Value) { objEmployee.PhoneNumber = (string)reader["PhoneNumber"]; }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                        reader.Close();
                        conn.Close();
                        scope.Complete();
                    }
                }
            }
            catch (TransactionAbortedException ex)
            {
                throw new System.Exception("TransactionAbortedException Message: {0}", ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objEmployee;
        }

        public bool InsertEmployee(Employee objEmployee)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SP_Employee_Insert", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = objEmployee.Name;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = objEmployee.City.Trim();
                    cmd.Parameters.Add("@Department", SqlDbType.Int).Value = objEmployee.Department;
                    cmd.Parameters.Add("@Gender", SqlDbType.Int).Value = objEmployee.Gender;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 10).Value = objEmployee.PhoneNumber;

                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result == 1;

        }

        public bool UpdateEmployee(Employee objEmployee)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SP_Employee_Update", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = objEmployee.EmployeeId;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = objEmployee.Name;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = objEmployee.City.Trim();
                    cmd.Parameters.Add("@Department", SqlDbType.Int).Value = objEmployee.Department;
                    cmd.Parameters.Add("@Gender", SqlDbType.Int).Value = objEmployee.Gender;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 10).Value = objEmployee.PhoneNumber;

                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result == 1;

        }

        public bool InsertUpdate(Employee objEmployee)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SP_Employee_InsertUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = objEmployee.EmployeeId;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = objEmployee.Name;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = objEmployee.City.Trim();
                    cmd.Parameters.Add("@Department", SqlDbType.Int).Value = objEmployee.Department;
                    cmd.Parameters.Add("@Gender", SqlDbType.Int).Value = objEmployee.Gender;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 10).Value = objEmployee.PhoneNumber;

                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result == 1;
        }
        public bool DeleteEmployee(int ID)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SP_Employee_Delete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = ID;
                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result == 1;
        }

    }
}
