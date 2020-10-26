using System.Collections.Generic;
using System.Threading.Tasks;
using TestApriorit.Core.Models;
using TestApriorit.Core.ViewModels;

namespace TestApriorit.Core.Services
{
    public interface IEmployeePositionService
    {
        Task<IEnumerable<EmployeePositionViewModel>> GetAllEmployeePositions(string sortOrder, int pageNumber, int pageSize);
        Task<EmployeePosition> GetEmployeePositionById(int id);
        Task<int> GetEmployeePositionsCount();
        Task<EmployeePositionViewModel> CreateEmployeePosition(EmployeePositionViewModel newEmployeePosition);
        Task UpdateEmployeePosition(EmployeePosition employeePositionToBeUpdated, EmployeePosition employeePosition);
        Task DeleteEmployeePosition(EmployeePosition EmployeePosition);
    }
}
