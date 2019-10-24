using System;
using System.Collections.Generic;
using System.Text;

namespace JobsityFinancialChat.Domain.Models.DB
{
    public class ApplicationUserChatroom
    {
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
