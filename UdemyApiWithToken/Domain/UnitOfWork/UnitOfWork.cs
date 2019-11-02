using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace UdemyApiWithToken.Domain.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : DbContext
    {
        private readonly T context;

        public UnitOfWork(T context)
        {
            this.context = context;
        }

        public void Complete()
        {
            this.context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}