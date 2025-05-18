using NZWalks.API.Data;
using NZWalks.API.Repository.IRepository;

namespace NZWalks.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NZWalkDbContext dbContext;
        public IRegionRepository Region { get; private set; }
        public IWalkRepository Walk { get; private set; }
        public IDifficultRepository Difficult { get; private set; }

        public UnitOfWork(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
            Region = new RegionRepository(dbContext);
            Walk = new WalkRepository(dbContext);
            Difficult =new DifficultyRepository(dbContext);
        }
    }
}
