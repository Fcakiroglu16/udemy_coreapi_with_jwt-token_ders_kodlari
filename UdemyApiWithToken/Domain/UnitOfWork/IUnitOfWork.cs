using System.Threading.Tasks;

namespace UdemyApiWithToken.Domain.UnitOfWork
{
    public interface IUnitOfWork<T>
    {
        Task CompleteAsync();

        void Complete();
    }
}