using E_Commerce.Contracts.Base;
using E_Commerce.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace E_Commerce.Implementation.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext context;
        public BaseRepository(AppDbContext context)
        {
            this.context = context;

        }

        public async Task<T> Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            
        }

        public async Task<T> Find(Expression<Func<T, bool>>? criteria = null,  params Func<IQueryable<T>, IQueryable<T>>?[] includes )
        {
            IQueryable<T> query = context.Set<T>();
            if(includes != null)
            {
                foreach(var include in includes)
                    query= include(query);
            }
            return await query.FirstOrDefaultAsync(criteria);

        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Apply criteria if provided
           

            return await query.SingleOrDefaultAsync(criteria);
        }


        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> Criteria, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query=context.Set<T>();
            if(includes!=null)
                foreach(var include in includes)
                    query=query.Include(include);
            if(Criteria!=null)
                query.Where(Criteria);
               return  await query.ToListAsync();
        }

            public async Task<IEnumerable<T>> Get2(Expression<Func<T, bool>>? criteria = null, params Func<IQueryable<T>, IQueryable<T>>?[] includes)
            {
                IQueryable<T> query=context.Set<T>();
                query=query.Where(criteria);
                if (includes != null)
                    foreach (var include in includes)
                        query = include(query);
                return await query.ToListAsync();
            }

        public async Task<IEnumerable<T>> GetAllAysnc()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {

            return await context.Set<T>().FindAsync(id);
        }

        public void RemoveRange(IEnumerable<T> list)
        {
            context.Set<T>().RemoveRange(list);
        }

        public T Update(T entity)
        {
            context.Update(entity);
            return entity;
        }
    }
}
