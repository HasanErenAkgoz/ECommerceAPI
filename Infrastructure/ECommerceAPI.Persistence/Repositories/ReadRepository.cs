using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ECommerceAPIDbContext _context;

        public ReadRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
            => Table;

        public async Task<T> GetByIdAsync(string id)
            => await Table.FindAsync(Guid.Parse(id));

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
            => await Table.FirstOrDefaultAsync(expression);

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
            => Table.Where(expression);
    }
}
