using JobsityFinancialChat.Domain.Data;
using JobsityFinancialChat.Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsityFinancialChat.Providers.Repositories.ChatRoom
{
    internal class MessageServiceProvider : Repository<Chatroom>
    {
        public MessageServiceProvider(ApplicationDbContext context) : base(context)
        {
        }

        internal async Task<Message> SaveMessage(Message message)
        {
            await _context.AddAsync(message);

            return message;
        }

        internal async Task<IEnumerable<Message>> GetMessages(Guid chatroomId)
        {
            return await _context.Messages.Where(x => x.ChatroomId == chatroomId)
                .OrderByDescending(x => x.SendDate)
                .Take(50)
                .ToListAsync();
        }
    }
}