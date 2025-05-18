using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository.IRepository
{
    public interface IWalkRepository :IRepository<Walk>
    {
        Task<Walk?> UpdateAsync(Guid id, Walk walk);

    }
}
