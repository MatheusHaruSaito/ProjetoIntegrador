using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unolink.domain.Core.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        void Add(TEntity entity);
        Task<TEntity> GetById(TKey id);
        IUnitOfWork UnitOfWork { get; }
        void Update(TEntity entity);
    }
}