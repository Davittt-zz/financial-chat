using JobsityFinancialChat.Domain.Models.DB;
using JobsityFinancialChat.Logic.Models;

namespace JobsityFinancialChat.Logic.Interfaces
{
    public interface ITokenService
    {
        JsonWebToken GenerateJwtToken(string email, ApplicationUser user);
    }
}