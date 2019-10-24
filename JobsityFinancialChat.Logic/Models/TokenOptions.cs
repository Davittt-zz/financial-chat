
namespace JobsityFinancialChat.Logic.Models
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string ExpirationDays { get; set; }
        public string Secret { get; set; }
    }
}