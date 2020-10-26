using System;
using System.Threading.Tasks;
using TestApriorit.Core.Repositories;

namespace TestApriorit.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IPositionRepository Positions { get; }
        IEmployeePositionRepository EmployeePositions { get; }

        Task<int> CommitAsync();
    }
}
