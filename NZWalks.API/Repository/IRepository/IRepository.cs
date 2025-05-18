using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using System.Linq.Expressions;

namespace NZWalks.API.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T?> GetByIdAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> CreateAsync(T region);
        Task<T?> DeleteAsync(Guid id);
    }
}
