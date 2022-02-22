using ADODBConnection.Contracts.DomainModels;
using ADODBConnection.Contracts.MultiDBConnection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADODBConnection.DAL.Services
{
    public class EmployeeADODBService : IEmployeeDBService
    {
        string connectionString;

        public DBConnectionType dBConnectionType { get; set; } = DBConnectionType.ADO;

        public EmployeeADODBService(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("AppSettings").GetSection("DBConnectionString").Value;
        }

        /// <summary>
        /// To get the list of all available employees
        /// </summary>
        public IEnumerable<Employee> GetAllEmployees()
        {
            List<Employee> lstemployee = new List<Employee>();

            using (var con = SQLConnectionFactory.GetConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.City = rdr["City"].ToString();
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }

        /// <summary>
        /// To Add new employee record
        /// </summary>
        /// <param name="employee"></param>
        public int AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeName", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@City", employee.City);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return 1;
        }

        /// <summary>
        /// To Update the records of a particluar employee
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="employee"></param>
        public int UpdateEmployee(int id, Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", id);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@City", employee.City);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return 1;
        }

        /// <summary>
        /// Get the details of a particular employee
        /// </summary>
        /// <param name="id"></param>
        public Employee GetEmployeeData(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Employees WHERE Id= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee.Id = Convert.ToInt32(rdr["Id"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.City = rdr["City"].ToString();
                }
            }
            return employee;
        }

        /// <summary>
        /// To Delete the record on a particular employee
        /// </summary>
        /// <param name="id"></param>
        public int DeleteEmployee(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return 1;
        }
    }
}
