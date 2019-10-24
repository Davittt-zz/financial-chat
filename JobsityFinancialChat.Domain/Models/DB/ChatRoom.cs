using System;
using System.Collections.Generic;

namespace JobsityFinancialChat.Domain.Models.DB
{
    public class ChatRoom
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ApplicationUserChatroom> Members { get; set; }
    }
}
