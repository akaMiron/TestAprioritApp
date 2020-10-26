using System.Collections.Generic;
using System.Threading.Tasks;
using TestApriorit.Core.Models;

namespace TestApriorit.Core.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> CreateEmployee(Employee newEmployee);
        Task UpdateEmployee(Employee employeeToBeUpdated, Employee employee);
        Task DeleteEmployee(Employee employee);
    }
}
