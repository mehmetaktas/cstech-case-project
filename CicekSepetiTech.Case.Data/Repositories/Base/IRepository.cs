using System.Threading.Tasks;
using System.Linq;

namespace CicekSepetiTech.Case.Data.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<int> SaveChangesAsync();
        IQueryable<T> TableNoTracking { get; }
        IQueryable<T> Table { get; }
    }
}