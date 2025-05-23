﻿
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoIntegradorAPI.Context;

namespace ProjetoIntegradorAPI.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDataContext _applicationDataContext;
        public BaseRepository(ApplicationDataContext applicationDataContext)
        {
            _applicationDataContext = applicationDataContext;
        }

        public virtual async Task<T?> Delete(Guid Id)
        {
            T DeleteEntity = await GetByIdAsync(Id);
            _applicationDataContext.Set<T>().Remove(DeleteEntity);
            if(await _applicationDataContext.SaveChangesAsync() > 0)
            {
                return DeleteEntity;
            }
            return null;
        }

        public virtual async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _applicationDataContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid Id)
        {
            return await _applicationDataContext.Set<T>().FindAsync(Id) ;
        }

        public virtual async Task<T?> AddAsync(T PostEntity)
        {
            await _applicationDataContext.Set<T>().AddAsync(PostEntity);
            if(await _applicationDataContext.SaveChangesAsync() > 0)
            {
                return PostEntity;
            }
             
            return null;
        }

        public virtual async Task<T?> Update(T UpdateEntity)
        {
            _applicationDataContext.Set<T>().Update(UpdateEntity);
            await _applicationDataContext.SaveChangesAsync();
            return UpdateEntity;
        }
    }
}
