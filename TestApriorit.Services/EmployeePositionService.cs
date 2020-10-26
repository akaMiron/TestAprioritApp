using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApriorit.Core;
using TestApriorit.Core.Models;
using TestApriorit.Core.Services;
using TestApriorit.Core.ViewModels;

namespace TestApriorit.Services
{
    public class EmployeePositionService : IEmployeePositionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeePositionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<EmployeePositionViewModel> CreateEmployeePosition(EmployeePositionViewModel newEmployeePosition)
        {
            await _unitOfWork.Employees.AddAsync(newEmployeePosition.Employee);
            await _unitOfWork.CommitAsync();

            newEmployeePosition.EmployeePosition.EmployeeId = newEmployeePosition.Employee.EmployeeId;

            await _unitOfWork.EmployeePositions.AddAsync(newEmployeePosition.EmployeePosition);
            await _unitOfWork.CommitAsync();

            return newEmployeePosition;
        }

        public async Task DeleteEmployeePosition(EmployeePosition employeePosition)
        {
            _unitOfWork.EmployeePositions.Remove(employeePosition);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<EmployeePositionViewModel>> GetAllEmployeePositions(string sortOrder, int pageNumber, int pageSize)
        {
            List<EmployeePosition> employeePositions = new List<EmployeePosition>();

            switch (sortOrder)
            {
                case "asc":
                    employeePositions = (await _unitOfWork.EmployeePositions.GetAllAsync())
                        .OrderBy(x => x.EmployeeId)
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToList();
                    break;
                case "desc":
                    employeePositions = (await _unitOfWork.EmployeePositions.GetAllAsync())
                        .OrderByDescending(x => x.EmployeeId)
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .ToList();
                    break;
                default:
                    break;
            }

            List<EmployeePositionViewModel> employeePositionViewModel = new List<EmployeePositionViewModel>();

            foreach (var employeePosition in employeePositions)
            {
                employeePositionViewModel.Add(new EmployeePositionViewModel()
                {
                    Employee = await _unitOfWork.Employees.GetByIdAsync(employeePosition.EmployeeId),
                    EmployeePosition = new EmployeePosition
                    {
                        Position = _unitOfWork.Positions.GetByIdAsync(employeePosition.PositionId).Result,
                        EmployeeId = employeePosition.EmployeeId,
                        Hired = employeePosition.Hired,
                        Fired = employeePosition.Fired,
                        Salary = employeePosition.Salary,
                        EmployeePositionId = employeePosition.EmployeePositionId,
                        PositionId = employeePosition.PositionId
                    }
                });
            }

            return employeePositionViewModel;
                
        }

        public async Task<int> GetEmployeePositionsCount()
        {
            return (await _unitOfWork.EmployeePositions.GetAllAsync()).Count();

        }

        public async Task<EmployeePosition> GetEmployeePositionById(int id)
        {
            return await _unitOfWork.EmployeePositions.GetByIdAsync(id);
        }

        public async Task UpdateEmployeePosition(EmployeePosition employeePositionToBeUpdated, EmployeePosition employeePosition)
        {
            employeePositionToBeUpdated.EmployeeId = employeePosition.EmployeeId;
            employeePositionToBeUpdated.PositionId = employeePosition.PositionId;
            employeePositionToBeUpdated.Salary = employeePosition.Salary;
            employeePositionToBeUpdated.Hired = employeePosition.Hired;
            employeePositionToBeUpdated.Fired = employeePosition.Fired;

            await _unitOfWork.CommitAsync();
        }

    }
}
