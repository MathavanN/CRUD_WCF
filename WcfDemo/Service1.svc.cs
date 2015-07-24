using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDemo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public static String ConnectionString = ConfigurationManager.ConnectionStrings["MyDbConn"].ConnectionString;

        public List<Student> GetStudents()
        {
            List<Student> studentList = new List<Student>();
            DataTable resourceTable = new DataTable();
            SqlDataReader resultReader = null;
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand("GetStudent", connection);
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                connection.Open();
                resultReader = command.ExecuteReader();
                resourceTable.Load(resultReader);
                resultReader.Close();
                connection.Close();

                studentList = (from DataRow dr in resourceTable.Rows
                               select new Student()
                               {
                                   StudentId = Convert.ToInt64(dr["StudentId"]),
                                   FirstName = dr["FirstName"].ToString(),
                                   LastName = dr["LastName"].ToString(),
                                   RegisterNo = dr["RegisterNo"].ToString(),
                                   Department = dr["Department"].ToString()
                               }).ToList();
            }
            catch (Exception exception)
            {
                if (resultReader != null || connection.State == ConnectionState.Open)
                {
                    resultReader.Close();
                    connection.Close();
                }
                throw new FaultException<ExceptionMessage>(new ExceptionMessage(exception.Message));
            }

            return studentList;
        }

        public void AddStudents(Student student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("AddStudent", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@StudentId", student.StudentId));
                    command.Parameters.Add(new SqlParameter("@FirstName", student.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", student.LastName));
                    command.Parameters.Add(new SqlParameter("@RegisterNo", student.RegisterNo));
                    command.Parameters.Add(new SqlParameter("@Department", student.Department));
                    object result = command.ExecuteScalar();
                    connection.Close();
                }
            }
            catch (SqlException exception)
            {
                throw new FaultException<ExceptionMessage>(new ExceptionMessage(exception.Message));
            }
        }

        public void DeleteStudent(long StudentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DeleteStudent", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@StudentId", StudentId));
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException exception)
            {
                throw new FaultException<ExceptionMessage>(new ExceptionMessage(exception.Message));
            }
        }
    }
}
