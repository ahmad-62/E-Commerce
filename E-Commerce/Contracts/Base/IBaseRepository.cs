using E_Commerce.Models;
using System.Linq.Expressions;

namespace E_Commerce.Contracts.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAysnc();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> Get(Expression<Func<T,bool>>? Criteria=null, params Expression<Func<T, object>>[] includes);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria = null, params Expression<Func<T, object>>[] includes);
        Task<T>Find(Expression<Func<T,bool>>? criteria=null,params   Func<IQueryable<T>,IQueryable<T>>? [] includes);
        Task<IEnumerable<T>> Get2(Expression<Func<T, bool>>? criteria = null, params Func<IQueryable<T>, IQueryable<T>> [] includes);
         void RemoveRange(IEnumerable<T> list);
    }
}
