using ADODBConnection.Contracts.DomainModels;
using ADODBConnection.Contracts.MultiDBConnection;
using ADODBConnection.DAL.Services;
using System.Collections.Generic;
using static ADODBConnection.Startup;

namespace ADODBConnection.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDBServiceResolver _employeeDBServiceResolver;
        private IEmployeeDBService _employeeDBService;

        public EmployeeService(EmployeeDBServiceResolver employeeDBServiceResolver)
        {
            _employeeDBServiceResolver = employeeDBServiceResolver;
            _employeeDBService = _employeeDBServiceResolver(DBConnectionType.ADO);
        }

        public int AddEmployee(Employee employee)
        {
            return _employeeDBService.AddEmployee(employee);
        }

        public int DeleteEmployee(int id)
        {
            return _employeeDBService.DeleteEmployee(id);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeDBService.GetAllEmployees();
        }

        public Employee GetEmployeeData(int id)
        {
            return _employeeDBService.GetEmployeeData(id);
        }

        public int UpdateEmployee(int id, Employee employee)
        {
            return _employeeDBService.UpdateEmployee(id, employee);
        }

        public void SetDBConnection(DBConnectionType type)
        {
            if (_employeeDBService?.dBConnectionType != type)
            {
                _employeeDBService = _employeeDBServiceResolver(type);
            }
        }
    }
}
