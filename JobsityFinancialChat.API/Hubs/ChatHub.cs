using JobsityFinancialChat.API.HubModels;
using JobsityFinancialChat.Domain.Models.API;
using JobsityFinancialChat.Domain.Models.DB;
using JobsityFinancialChat.Logic;
using JobsityFinancialChat.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Text;
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
            var text = message.Text.ToLower();
            message.SendDate = DateTime.Now;

            if (text.Contains("/stock_code="))
            {
                StringBuilder command = new StringBuilder(text);

                command = command.Replace("/stock_code=", "");

                var response = await StockService.Instance.GetStock(command.ToString());

                if (response != null)
                {
                    message.Text = response.Symbol.ToUpper() + " quote is $" + response.Open + " per share.";
                    message.MessageType = (int)MessageTypeEnum.Command;

                    await Clients.Group(message.ChatroomId.ToString()).SendAsync("Send", message);
                }
                else
                {
                    message.Text = command.ToString() + " could not be retrieved.";
                    message.MessageType = (int)MessageTypeEnum.Error;

                    await Clients.Group(message.ChatroomId.ToString()).SendAsync("OnMetadataMessage", message);
                }
            }
            else
            {
                Message newMessage = new Message
                {
                    ChatroomId = message.ChatroomId,
                    Text = message.Text,
                    SenderUserId = message.UserId,
                    ReadDate = DateTime.Now,
                    SendDate = DateTime.Now
                };

                var sender = await _databaseProvider.SaveMessage(newMessage);

                await _databaseProvider.Save();

                await Clients.Group(message.ChatroomId.ToString()).SendAsync("Send", message);
            }
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