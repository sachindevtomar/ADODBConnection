using ADODBConnection.Contracts.DomainModels;
using ADODBConnection.Contracts.MultiDBConnection;
using System.Collections.Generic;

namespace ADODBConnection.DAL.Services
{
    public interface IEmployeeDBService : IMultiDBConnection
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeData(int id);
        int AddEmployee(Employee employee);
        int UpdateEmployee(int id, Employee employee);
        int DeleteEmployee(int id);
    }
}
