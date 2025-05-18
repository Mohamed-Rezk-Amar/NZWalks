using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository.IRepository
{
    public interface IDifficultRepository : IRepository<Difficulty>
    {
        Task<Difficulty?> UpdateAsync(Guid id,Difficulty difficulty); 
    }
}
