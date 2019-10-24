using JobsityFinancialChat.Domain.Models.DB;
using System.Threading.Tasks;

namespace JobsityFinancialChat.Providers
{
    public interface IDatabaseProvider
    {
        #region User
        Task<ApplicationUser> GetUser(string email);


        #endregion User


        #region General

        Task Save();

        #endregion General
    }
}
