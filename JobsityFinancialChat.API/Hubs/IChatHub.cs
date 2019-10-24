using JobsityFinancialChat.API.HubModels;
using System.Threading.Tasks;

namespace JobsityFinancialChat.API.Hubs
{
    public interface IChatHub
    {
        Task Send(MessageModel message);

        Task OnConnectedAsync();
    }

}
