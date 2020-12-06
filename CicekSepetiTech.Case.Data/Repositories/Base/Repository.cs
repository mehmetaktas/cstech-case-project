using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepetiTech.Case.Data.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CaseDbContext Context;

        public Repository(CaseDbContext context)
        {
            this.Context = context;
        }

        public async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public virtual IQueryable<T> TableNoTracking => Context.Set<T>().AsNoTracking();
        public virtual IQueryable<T> Table => Context.Set<T>();
    }
}