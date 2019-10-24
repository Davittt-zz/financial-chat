using JobsityFinancialChat.Domain.Data;
using JobsityFinancialChat.Domain.Models.DB;
using JobsityFinancialChat.Providers.Repositories.Properties;
using System;
using System.Threading.Tasks;

namespace JobsityFinancialChat.Providers
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly UserServiceProvider _userServiceProvider;
        private readonly ApplicationDbContext _context;

        public DatabaseProvider(ApplicationDbContext context)
        {
            _context = context;
            _userServiceProvider = new UserServiceProvider(context);
        }

        #region User
       
        public async Task<ApplicationUser> GetUser(Guid id)
        {
            return await _userServiceProvider.GetUser(id);
        }
        public async Task<ApplicationUser> GetUser(string email)
        {
            return await _userServiceProvider.GetUser(email);
        }

        #endregion User

        #region General

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        #endregion General
    }
}