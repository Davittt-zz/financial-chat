using System.ComponentModel.DataAnnotations;

namespace JobsityFinancialChat.Domain.API.User
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}