using EmployeeAPI.model;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace EmployeeAPI.DataAccessLayer

{
    public class RRFDAL
    {
        IConfiguration _configuration;
        public string connectionstring;

        public RRFDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionstring = _configuration["ConnectionStrings:DefaultConnection"];
        }

        public DataTable GetAllRRFRecords()
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("SP_RRF_SelectAll", conn);
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

        public RRF GetRRFRecordById(int ID)
        {
            RRF objRRF = new RRF();
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(connectionstring))
                    {
                        SqlCommand cmd = new SqlCommand("SP_RRF_SelectById", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RRFId", ID);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader["ManagerId"] != DBNull.Value) { objRRF.ManagerId = (int)reader["ManagerId"]; }
                                if (reader["ClientId"] != DBNull.Value) { objRRF.ClientId = (int)reader["ClientId"]; }
                                if (reader["ProjectId"] != DBNull.Value) { objRRF.ProjectId = (int)reader["ProjectId"]; }
                                if (reader["SubmissionDate"] != DBNull.Value) { objRRF.SubmissionDate = (System.DateTime)reader["SubmissionDate"]; }
                                if (reader["RoleId"] != DBNull.Value) { objRRF.RoleId = (int)reader["RoleId"]; }
                                if (reader["IsBillable"] != DBNull.Value) { objRRF.IsBillable = (int)reader["IsBillable"]; }
                                if (reader["BillingRate"] != DBNull.Value) { objRRF.BillingRate = (decimal)reader["BillingRate"]; }
                                if (reader["BillingStartDate"] != DBNull.Value) { objRRF.BillingStartDate = (System.DateTime)reader["BillingStartDate"]; }
                                if (reader["PositionTypeId"] != DBNull.Value) { objRRF.PositionTypeId = (int)reader["PositionTypeId"]; }
                                if (reader["IsInternalResource"] != DBNull.Value) { objRRF.IsInternalResource = (int)reader["IsInternalResource"]; }
                                if (reader["IdentifiedResourceId"] != DBNull.Value) { objRRF.IdentifiedResourceId = (int)reader["IdentifiedResourceId"]; }
                                if (reader["NumberOfPosition"] != DBNull.Value) { objRRF.NumberOfPosition = (int)reader["NumberOfPosition"]; }
                                if (reader["PayroleType"] != DBNull.Value) { objRRF.PayroleType = (int)reader["PayroleType"]; }
                                if (reader["ApprovedByResourceId"] != DBNull.Value) { objRRF.ApprovedByResourceId = (int)reader["ApprovedByResourceId"]; }
                                if (reader["PrimaryTechnologies"] != DBNull.Value) { objRRF.PrimaryTechnologies = (string)reader["PrimaryTechnologies"]; }
                                if (reader["MinimumYearsOfExperienceId"] != DBNull.Value) { objRRF.MinimumYearsOfExperienceId = (int)reader["MinimumYearsOfExperienceId"]; }
                                if (reader["NiceToHaveSkills"] != DBNull.Value) { objRRF.NiceToHaveSkills = (string)reader["NiceToHaveSkills"]; }
                                if (reader["JobLocation"] != DBNull.Value) { objRRF.JobLocation = (string)reader["JobLocation"]; }
                                if (reader["IsRemotely"] != DBNull.Value) { objRRF.IsRemotely = (int)reader["IsRemotely"]; }
                                if (reader["InterviewByResourceId"] != DBNull.Value) { objRRF.InterviewByResourceId = (int)reader["InterviewByResourceId"]; }
                                if (reader["JobDescription"] != DBNull.Value) { objRRF.JobDescription = (string)reader["JobDescription"]; }
                                if (reader["OtherInputs"] != DBNull.Value) { objRRF.OtherInputs = (string)reader["OtherInputs"]; }
                                if (reader["Remark"] != DBNull.Value) { objRRF.Remark = (string)reader["Remark"]; }
                                if (reader["CreateBy"] != DBNull.Value) { objRRF.CreateBy = (string)reader["CreateBy"]; }
                                if (reader["CreateDate"] != DBNull.Value) { objRRF.CreateDate = (System.DateTime)reader["CreateDate"]; }
                                if (reader["UpdateBy"] != DBNull.Value) { objRRF.UpdateBy = (string)reader["UpdateBy"]; }
                                if (reader["UpdateDate"] != DBNull.Value) { objRRF.UpdateDate = (System.DateTime)reader["UpdateDate"]; }
                                if (reader["RRFId"] != DBNull.Value) { objRRF.RRFId = (int)reader["RRFId"]; }
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
                //throw ex;
                // writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
                //throw ex.Message; 
            }

            return objRRF;
        }

        public bool InsertUpdateRRFRecord(RRF objRRF)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SP_RRF_InsertUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RRFId", SqlDbType.Int).Value = objRRF.RRFId;
                    cmd.Parameters.Add("@ManagerId", SqlDbType.Int).Value = objRRF.ManagerId;
                    cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = objRRF.ClientId;
                    cmd.Parameters.Add("@ProjectId", SqlDbType.Int).Value = objRRF.ProjectId;
                    cmd.Parameters.Add("@SubmissionDate", SqlDbType.DateTime).Value = objRRF.SubmissionDate;
                    cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = objRRF.RoleId;
                    cmd.Parameters.Add("@IsBillable", SqlDbType.Int).Value = objRRF.IsBillable;
                    cmd.Parameters.Add("@BillingRate", SqlDbType.Int).Value = objRRF.BillingRate;
                    cmd.Parameters.Add("@BillingStartDate", SqlDbType.DateTime).Value = objRRF.BillingStartDate;
                    cmd.Parameters.Add("@PositionTypeId", SqlDbType.Int).Value = objRRF.PositionTypeId;
                    cmd.Parameters.Add("@IsInternalResource", SqlDbType.Int).Value = objRRF.IsInternalResource;
                    cmd.Parameters.Add("@IdentifiedResourceId", SqlDbType.Int).Value = objRRF.IdentifiedResourceId;
                    cmd.Parameters.Add("@NumberOfPosition", SqlDbType.Int).Value = objRRF.NumberOfPosition;
                    cmd.Parameters.Add("@PayroleType", SqlDbType.Int).Value = objRRF.PayroleType;
                    cmd.Parameters.Add("@ApprovedByResourceId", SqlDbType.Int).Value = objRRF.ApprovedByResourceId;
                    cmd.Parameters.Add("@PrimaryTechnologies", SqlDbType.NVarChar).Value = objRRF.PrimaryTechnologies;
                    cmd.Parameters.Add("@MinimumYearsOfExperienceId", SqlDbType.Int).Value = objRRF.MinimumYearsOfExperienceId;
                    cmd.Parameters.Add("@NiceToHaveSkills", SqlDbType.Int).Value = objRRF.NiceToHaveSkills;
                    cmd.Parameters.Add("@JobLocation", SqlDbType.Int).Value = objRRF.JobLocation;
                    cmd.Parameters.Add("@IsRemotely", SqlDbType.NVarChar).Value = objRRF.IsRemotely;
                    cmd.Parameters.Add("@InterviewByResourceId", SqlDbType.Int).Value = objRRF.InterviewByResourceId;
                    cmd.Parameters.Add("@JobDescription", SqlDbType.NVarChar).Value = objRRF.JobDescription;
                    cmd.Parameters.Add("@OtherInputs", SqlDbType.NVarChar).Value = objRRF.OtherInputs;
                    cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = objRRF.Remark;
                    cmd.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = '1';

                    //how to add UserId?
                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
                //throw ex.Message; 
            }

            return result == 1;

        }

        public bool DeleteRRFRecord(int ID)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstring))
                {
                    SqlCommand cmd = new SqlCommand("SP_RRF_Delete", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RRFId", SqlDbType.Int).Value = ID;
                    //how to add userid?
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
