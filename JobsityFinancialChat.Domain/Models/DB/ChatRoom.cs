using System;
using System.Collections.Generic;

namespace JobsityFinancialChat.Domain.Models.DB
{
    public class Chatroom
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ApplicationUserChatroom> Members { get; set; }

        public Chatroom() {
            Members = new List<ApplicationUserChatroom>();
        }
    }
}
