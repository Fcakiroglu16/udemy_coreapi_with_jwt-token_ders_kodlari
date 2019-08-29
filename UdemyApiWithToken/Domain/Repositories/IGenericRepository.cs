using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UdemyApiWithToken.Domain.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);

        // context.category.where(x=>x.product.price>3000)
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        // context.category.where(x=>x.product.price>3000)
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);

        Task Add(T entry); // asekron geriye birşey dönmeyecekseniz task

        void Update(T entry);

        Task Delete(int id);
    }
}