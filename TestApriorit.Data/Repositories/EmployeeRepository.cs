using TestApriorit.Core.Models;
using TestApriorit.Core.Repositories;

namespace TestApriorit.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TestAprioritDbContext context)
               : base(context)
        { }

        private TestAprioritDbContext TestAprioritDbContext
        {
            get { return Context as TestAprioritDbContext; }
        }
    }
}
