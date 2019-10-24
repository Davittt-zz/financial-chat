using JobsityFinancialChat.Domain.Data;
using JobsityFinancialChat.Domain.Models.DB;
using JobsityFinancialChat.Providers.Repositories.ChatRoom;
using JobsityFinancialChat.Providers.Repositories.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobsityFinancialChat.Providers
{
    public class DatabaseProvider : IDatabaseProvider
    {
        private readonly ApplicationDbContext _context;
        private readonly UserServiceProvider _userServiceProvider;
        private readonly ChatroomServiceProvider _chatroomServiceProvider;
        private readonly MessageServiceProvider _messageServiceProvider;

        public DatabaseProvider(ApplicationDbContext context)
        {
            _context = context;
            _userServiceProvider = new UserServiceProvider(context);
            _chatroomServiceProvider = new ChatroomServiceProvider(context);
            _messageServiceProvider = new MessageServiceProvider(context);
        }

        #region User

        public async Task<ApplicationUser> GetUser(Guid id)
        {
            return await _userServiceProvider.GetUser(id);
        }
        public async Task<ApplicationUser> GetUser(string email)
        {
            return await _userServiceProvider.GetUser(email);
        }

        #endregion User

        #region Message
        public async Task<Message> SaveMessage(Message message)
        {
            return await _messageServiceProvider.SaveMessage(message);
        }

        public async Task<IEnumerable<Message>> GetMessages(Guid chatroomId)
        {
            return await _messageServiceProvider.GetMessages(chatroomId);
        }
       

        #endregion Message

        #region Chatroom

        public async Task<Chatroom> CreateChatroom(Guid userId, string name)
        {
            return await _chatroomServiceProvider.CreateChatroom(userId, name);
        }

        public async Task<Chatroom> Join(ApplicationUser user, Guid chatroomId)
        {
            return await _chatroomServiceProvider.Join(user, chatroomId);
        }

        public async Task<List<Chatroom>> GetChatrooms()
        {
            return await _chatroomServiceProvider.GetAll();
        }


        #endregion Chatroom

        #region General

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        #endregion General
    }
}