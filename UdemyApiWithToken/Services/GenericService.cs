using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UdemyApiWithToken.Domain;
using UdemyApiWithToken.Domain.Repositories;
using UdemyApiWithToken.Domain.Responses;
using UdemyApiWithToken.Domain.Services;
using UdemyApiWithToken.Domain.UnitOfWork;

namespace UdemyApiWithToken.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> genericRepository;
        private readonly IUnitOfWork<UdemyApiWithTokenDBContext> unitOfWork;

        public GenericService(IGenericRepository<T> genericRepository, IUnitOfWork<UdemyApiWithTokenDBContext> unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<T>> Add(T entry)
        {
            try
            {
                await this.genericRepository.Add(entry);
                await this.unitOfWork.CompleteAsync();
                return new BaseResponse<T>(entry);
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<int> CountWhere(Expression<Func<T, bool>> predicate)
        {
            return await genericRepository.CountWhere(predicate);
        }

        public async Task<BaseResponse<T>> Delete(int id)
        {
            try
            {
                T t = await this.genericRepository.GetById(id);

                if (t != null)
                {
                    await this.genericRepository.Delete(id);
                    await this.unitOfWork.CompleteAsync();
                    return new BaseResponse<T>(t);
                }
                else
                {
                    return new BaseResponse<T>("id sahip satır bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<BaseResponse<T>> GetById(int id)
        {
            try
            {
                T t = await this.genericRepository.GetById(id);

                if (t != null)
                {
                    return new BaseResponse<T>(t);
                }
                else
                {
                    return new BaseResponse<T>("id sahip satır bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }

        public async Task<BaseResponse<IEnumerable<T>>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> t = await this.genericRepository.GetWhere(predicate);

            return new BaseResponse<IEnumerable<T>>(t);
        }

        public async Task<BaseResponse<T>> Update(T entry)
        {
            try
            {
                this.genericRepository.Update(entry);
                await this.unitOfWork.CompleteAsync();
                return new BaseResponse<T>(entry);
            }
            catch (Exception ex)
            {
                return new BaseResponse<T>(ex.Message);
            }
        }
    }
}