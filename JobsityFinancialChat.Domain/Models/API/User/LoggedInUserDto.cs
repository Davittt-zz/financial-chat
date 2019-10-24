using Newtonsoft.Json;
using System;

namespace JobsityFinancialChat.Domain.API.User
{
    public class LoggedInUserDto
    {
        public string Token { get; set; }

        public long Expires { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

    }
}
