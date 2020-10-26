using System.Collections.Generic;
using System.Threading.Tasks;
using TestApriorit.Core;
using TestApriorit.Core.Models;
using TestApriorit.Core.Services;

namespace TestApriorit.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            await _unitOfWork.Employees.AddAsync(newEmployee);
            await _unitOfWork.CommitAsync();

            return newEmployee;
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _unitOfWork.Employees.Remove(employee);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _unitOfWork.Employees.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _unitOfWork.Employees.GetByIdAsync(id);
        }

        public async Task UpdateEmployee(Employee employeeToBeUpdated, Employee employee)
        {
            employeeToBeUpdated.FirstName = employee.FirstName;
            employeeToBeUpdated.LastName = employee.LastName;

            await _unitOfWork.CommitAsync();
        }
    }
}
