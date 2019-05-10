﻿using System.Threading.Tasks;

namespace UdemyApiWithToken.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UdemyApiWithTokenDBContext context;

        public UnitOfWork(UdemyApiWithTokenDBContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}