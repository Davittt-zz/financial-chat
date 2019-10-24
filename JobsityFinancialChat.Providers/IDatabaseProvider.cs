using JobsityFinancialChat.Domain.Models.DB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobsityFinancialChat.Providers
{
    public interface IDatabaseProvider
    {
        #region User

        Task<ApplicationUser> GetUser(string email);

        #endregion User

        #region Chatroom

        Task<Chatroom> CreateChatroom(Guid userId, string name);

        Task<Chatroom> Join(ApplicationUser user, Guid chatroomId);

        Task<List<Chatroom>> GetChatrooms();

        #endregion Chatroom

        #region Messages

        Task<IEnumerable<Message>> GetMessages(Guid chatroomId);

        Task<Message> SaveMessage(Message message);

        #endregion Messages

        #region General

        Task Save();

        #endregion General
    }
}