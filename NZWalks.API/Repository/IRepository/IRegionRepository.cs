using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository.IRepository
{
    public interface IRegionRepository : IRepository<Region>
    {
        
        Task<Region?> UpdateAsync(Guid id , Region region);
        
    }
}

