using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsityFinancialChat.Domain.Models.DB
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [NotMapped]
        public string RoleNames { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Active { get; set; }

        public virtual IEnumerable<ApplicationUserChatroom> Chatrooms { get; set; }

        public ApplicationUser()
        {
        }
    }
}
