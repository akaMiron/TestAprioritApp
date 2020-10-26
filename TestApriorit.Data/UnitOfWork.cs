using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApriorit.Core;
using TestApriorit.Core.Repositories;
using TestApriorit.Data.Configurations;
using TestApriorit.Data.Repositories;

namespace TestApriorit.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TestAprioritDbContext _context;
        private EmployeeRepository _employeeRepository;
        private PositionRepository _positionRepository;
        private EmployeePositionRepository _employeePositionRepository;

        public UnitOfWork(TestAprioritDbContext context)
        {
            this._context = context;
        }

        public IEmployeeRepository Employees => _employeeRepository = _employeeRepository ?? new EmployeeRepository(_context);

        public IPositionRepository Positions => _positionRepository = _positionRepository ?? new PositionRepository(_context);

        public IEmployeePositionRepository EmployeePositions => _employeePositionRepository = _employeePositionRepository ?? new EmployeePositionRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
