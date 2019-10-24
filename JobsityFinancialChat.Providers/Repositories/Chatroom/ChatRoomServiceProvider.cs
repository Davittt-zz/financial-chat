using JobsityFinancialChat.Domain.Data;
using JobsityFinancialChat.Domain.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobsityFinancialChat.Providers.Repositories.ChatRoom
{
    internal class ChatroomServiceProvider : Repository<Chatroom>
    {
        public ChatroomServiceProvider(ApplicationDbContext context) : base(context)
        {
        }

        internal async Task<Chatroom> CreateChatroom(Guid userId, string name)
        {
            var result = await _context.AddAsync(new Chatroom
            {
                Name = name
            });

            var chatroom = result.Entity;


            _context.SaveChanges();

            await _context.AddAsync(new ApplicationUserChatroom
            {
                ApplicationUserId = userId,
                ChatroomId = chatroom.Id
            });

            _context.SaveChanges();

            chatroom.Members = null;

            return chatroom;
        }

        internal async Task<Chatroom> Join(ApplicationUser user, Guid chatroomId)
        {
            var userResult = await _context.Users.FindAsync(user.Id);

            var chatroom = await _context.Chatrooms.FindAsync(chatroomId);

            _context.AddRange(new ApplicationUserChatroom()
            {
                ApplicationUserId = userResult.Id,
                ChatroomId = chatroom.Id
            });

            _context.SaveChanges();

            chatroom.Members = null;

            return chatroom;
        }

        internal async Task<List<Chatroom>> GetAll() {

            return await _context.Chatrooms.ToListAsync();
        }
    }
}