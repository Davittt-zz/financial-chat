using JobsityFinancialChat.Domain.Data;
using JobsityFinancialChat.Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JobsityFinancialChat.Providers.Repositories.User
{
    internal class UserServiceProvider : Repository<ApplicationUser>
    {
        public UserServiceProvider(ApplicationDbContext context) : base(context)
        {
        }

        internal async Task<ApplicationUser> GetUser(Guid id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        internal async Task<ApplicationUser> GetUser(string email)
        {
            return await _context.Users
                .FirstAsync(x => x.Email == email);
        }
    }
}