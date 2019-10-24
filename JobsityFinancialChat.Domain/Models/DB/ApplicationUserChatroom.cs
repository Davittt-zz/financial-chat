using System;
using System.Collections.Generic;
using System.Text;

namespace JobsityFinancialChat.Domain.Models.DB
{
    public class ApplicationUserChatroom
    {
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid ChatroomId { get; set; }
        public Chatroom Chatroom { get; set; }
    }
}
