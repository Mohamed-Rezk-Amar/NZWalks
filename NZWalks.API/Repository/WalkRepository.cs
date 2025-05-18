using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repository.IRepository;

namespace NZWalks.API.Repository
{
    public class WalkRepository : Repository<Walk>, IWalkRepository
    {
        private readonly NZWalkDbContext dbContext;

        public WalkRepository(NZWalkDbContext dbContext) :base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var regionFromDb = await dbContext.Walks.FirstOrDefaultAsync(u => u.Id == id);
            if (regionFromDb == null)
            {
                return null;
            }
            
           
            regionFromDb.Name = walk.Name;
            regionFromDb.Description = walk.Description;
            regionFromDb.LengthInKm = walk.LengthInKm;
            regionFromDb.WalkImageUrl = walk.WalkImageUrl;
            regionFromDb.DifficultyId = walk.DifficultyId;
            regionFromDb.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return walk;
        }

    }
}
