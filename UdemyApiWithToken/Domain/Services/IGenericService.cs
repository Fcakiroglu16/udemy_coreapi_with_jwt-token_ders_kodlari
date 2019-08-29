using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UdemyApiWithToken.Domain.Responses;

namespace UdemyApiWithToken.Domain.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<BaseResponse<T>> GetById(int id);

        // context.category.where(x=>x.product.price>3000)
        Task<BaseResponse<IEnumerable<T>>> GetWhere(Expression<Func<T, bool>> predicate);

        // context.category.where(x=>x.product.price>3000)
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);

        Task<BaseResponse<T>> Add(T entry); // asekron geriye birşey dönmeyecekseniz task

        Task<BaseResponse<T>> Update(T entry);

        Task<BaseResponse<T>> Delete(int id);
    }
}