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

        public ChatHub(UserManager<ApplicationUser> userManager,
            IDatabaseProvider databaseProvider)
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
            var senderEmail = Context.User.Identity.Name;

            var sender = await _databaseProvider.GetUser(senderEmail);               
        }

        public override async Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;
            if (string.IsNullOrEmpty(name))
            {
                await base.OnConnectedAsync();
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, name);
                await base.OnConnectedAsync();
            }
        }
    }
}