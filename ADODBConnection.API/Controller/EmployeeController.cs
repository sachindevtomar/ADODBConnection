using ADODBConnection.API.Helpers;
using ADODBConnection.Contracts.DomainModels;
using ADODBConnection.Contracts.MultiDBConnection;
using ADODBConnection.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ADODBConnection.API.Controller
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [MultiDBConnectionControllerAttribute]
    public class EmployeeController : ControllerBase, IMultiDBConnectionSetter
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _employeeService.GetAllEmployees();
        }

        [HttpPost]
        public int Add([FromBody] Employee employee)
        {
            return _employeeService.AddEmployee(employee);
        }

        [HttpGet("{id}", Name = "Get")]
        public Employee Get(int id)
        {
            return _employeeService.GetEmployeeData(id);
        }

        [HttpPut("{id}")]
        public int Put(int id, [FromBody] Employee employee)
        {
            return _employeeService.UpdateEmployee(id,employee);
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return _employeeService.DeleteEmployee(id);
        }
        [NonAction]
        public void SetDBConnection(DBConnectionType type)
        {
            _employeeService.SetDBConnection(type);
        }
    }
}
