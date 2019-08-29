using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UdemyApiWithToken.Domain.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly UdemyApiWithTokenDBContext context;
        private DbSet<T> table = null;

        public GenericRepository(UdemyApiWithTokenDBContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public async Task Add(T entry)
        {
            await table.AddAsync(entry);
        }

        public async Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return await table.CountAsync(predicate);
        }

        public async Task Delete(int id)
        {
            T existEntity = await GetById(id);

            table.Remove(existEntity);
        }

        public async Task<T> GetById(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).ToListAsync();
        }

        public void Update(T entry)
        {
            context.Entry(entry).State = EntityState.Modified;
        }
    }
}