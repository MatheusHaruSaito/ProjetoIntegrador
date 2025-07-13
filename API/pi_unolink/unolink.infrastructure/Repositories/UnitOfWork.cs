using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using unolink.domain.Core.Interfaces;
using unolink.infrastructure.Context;

namespace unolink.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDataContext _context;
        public UnitOfWork(ApplicationDataContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync() > 0;   
        }
    }
}