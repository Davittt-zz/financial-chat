using System;
using System.Collections.Generic;
using System.Text;

namespace JobsityFinancialChat.Domain.Models.API.Chatroom
{
    public class ChatroomModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ChatroomMemberModel> Members { get; set; }
    }
}
