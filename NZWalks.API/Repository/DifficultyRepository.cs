using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repository.IRepository;

namespace NZWalks.API.Repository
{
    public class DifficultyRepository : Repository<Difficulty>, IDifficultRepository
    {
        private readonly NZWalkDbContext dbContext;

        public DifficultyRepository(NZWalkDbContext dbContext) :base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task<Difficulty?> UpdateAsync(Guid id, Difficulty difficulty)
        {
            var difficultyDomian = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (difficultyDomian != null)
            {
                return null;
            }
            difficultyDomian.Name = difficulty.Name;
            await dbContext.SaveChangesAsync();
            return difficultyDomian;
        }
    }
}
