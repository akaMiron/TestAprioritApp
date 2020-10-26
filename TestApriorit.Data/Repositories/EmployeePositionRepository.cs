using TestApriorit.Core.Models;
using TestApriorit.Core.Repositories;

namespace TestApriorit.Data.Repositories
{
    public class EmployeePositionRepository : Repository<EmployeePosition>, IEmployeePositionRepository
    {
        public EmployeePositionRepository(TestAprioritDbContext context)
               : base(context)
        { }

        private TestAprioritDbContext TestAprioritDbContext
        {
            get { return Context as TestAprioritDbContext; }
        }
    }
}
