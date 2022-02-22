using ADODBConnection.Contracts.DomainModels;
using ADODBConnection.Contracts.MultiDBConnection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ADODBConnection.DAL.Services
{
    public class EmployeeEFDBService : IEmployeeDBService
    {
        public EmployeeContext _employeeDbContext;

        public DBConnectionType dBConnectionType { get; set; } = DBConnectionType.EF;

        public EmployeeEFDBService(EmployeeContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        /// <summary>
        /// To get the list of all available employees
        /// </summary>
        public IEnumerable<Employee> GetAllEmployees()
        {
             return _employeeDbContext.Employees;
        }

        /// <summary>
        /// To Add new employee record
        /// </summary>
        /// <param name="employee"></param>
        public int AddEmployee(Employee employee)
        {
           
            _employeeDbContext.Employees.Add(employee);  
            _employeeDbContext.SaveChanges();
            return 1;
        }

        /// <summary>
        /// To Update the records of a particluar employee
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="employee"></param>
        public int UpdateEmployee(int Id, Employee employee)
        {
            var emp = _employeeDbContext.Employees.FirstOrDefault(x=> x.Id == Id);

            if (emp != null)
            {
                emp.Id = Id;
                emp.Name = employee.Name;
                emp.Department = employee.Department;
                emp.Gender = employee.Gender;
                emp.City = employee.City;

                _employeeDbContext.SaveChanges();
                return 1;
            }
            else return 0;
           
        }

        /// <summary>
        /// Get the details of a particular employee
        /// </summary>
        /// <param name="id"></param>
        public Employee GetEmployeeData(int id)
        {
            return _employeeDbContext.Employees.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// To Delete the record on a particular employee
        /// </summary>
        /// <param name="id"></param>
        public int DeleteEmployee(int id)
        {
            var emp = _employeeDbContext.Employees.FirstOrDefault(x => x.Id == id);

            if (emp != null)
            {
                _employeeDbContext.Remove(emp);
                _employeeDbContext.SaveChanges();
                return 1;
            }
            else return 0;
        }
    }
}
