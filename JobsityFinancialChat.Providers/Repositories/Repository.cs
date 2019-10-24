using JobsityFinancialChat.Domain.Data;
using System;

namespace JobsityFinancialChat.Providers.Repositories
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public object NullOrValue<R>(R? value) where R : struct
        {
            return value.HasValue ? (object)value.Value : DBNull.Value;
        }
    }
}
