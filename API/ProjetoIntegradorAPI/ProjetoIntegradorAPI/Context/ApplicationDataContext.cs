using Microsoft.EntityFrameworkCore;
using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.Context
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
    }
}
