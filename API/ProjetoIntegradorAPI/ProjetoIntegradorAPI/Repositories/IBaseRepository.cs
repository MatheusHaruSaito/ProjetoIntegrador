using Microsoft.AspNetCore.Mvc;

namespace ProjetoIntegradorAPI.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> AddAsync(T PostEntity);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(Guid Id);
        public Task<T> Delete(Guid Id);
        public Task<T> Update(T UpdateEntity);

    }
}
