using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BE_Homnayangi
{
    public class SignalRServer : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task OrderStatusNotify(string message)
        {
            await Clients.All.SendAsync("OrderStatusChanged", message);
        }
    }
}
