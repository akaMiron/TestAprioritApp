using TestApriorit.Core.Models;
using TestApriorit.Core.Repositories;

namespace TestApriorit.Data.Repositories
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(TestAprioritDbContext context)
               : base(context)
        { }

        private TestAprioritDbContext TestAprioritDbContext
        {
            get { return Context as TestAprioritDbContext; }
        }
    }
}
