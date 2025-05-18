using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repository.IRepository;

namespace NZWalks.API.Repository
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        private readonly NZWalkDbContext dbContext;

        public RegionRepository(NZWalkDbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }


        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var regionFromDb = await dbContext.Regions.FirstOrDefaultAsync(u => u.Id == id);
            if (regionFromDb == null)
            {
                return null;
            }

            regionFromDb.Code = region.Code;
            regionFromDb.Name = region.Name;
            regionFromDb.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return regionFromDb;

        }
    }
}
