using JobsityFinancialChat.API.HubModels;
using JobsityFinancialChat.Domain.Models.DB;
using JobsityFinancialChat.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace JobsityFinancialChat.API.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        private readonly IDatabaseProvider _databaseProvider;

        public ChatHub(UserManager<ApplicationUser> userManager, IDatabaseProvider databaseProvider)
        {
            _userManager = userManager;
            _databaseProvider = databaseProvider;
        }

        /// <summary>
        /// This method receives data from SignalR clients. 
        /// It also sends a response message to all clients in the group
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Send(MessageModel message)
        {
            Message newMessage = new Message
            {
                ChatroomId = message.ChatroomId, 
                Text = message.Message, 
                SenderUserId = message.UserId, 
                ReadDate = DateTime.Now,
                SendDate = DateTime.Now
            };

            var sender = await _databaseProvider.SaveMessage(newMessage);

            await _databaseProvider.Save();

            var senderEmail = Context.User.Identity.Name;

            await Clients.Group(message.ChatroomId.ToString()).SendAsync("Send", message);          
        } 

        public Task JoinRoom(string chatroomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatroomId);
        }

        public Task LeaveRoom(string chatroomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, chatroomId);
        }
    }
}