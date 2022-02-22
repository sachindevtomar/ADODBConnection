using ADODBConnection.API.Helpers;
using ADODBConnection.Contracts.DomainModels;
using System.Collections.Generic;

namespace ADODBConnection.Services
{
    public interface IEmployeeService: IMultiDBConnectionSetter
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeData(int id);
        int AddEmployee(Employee employee);
        int UpdateEmployee(int Id, Employee employee);
        int DeleteEmployee(int id);
    }
}
