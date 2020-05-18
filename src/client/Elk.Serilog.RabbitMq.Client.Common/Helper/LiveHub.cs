using Elk.Serilog.RabbitMq.Client.Model;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Elk.Serilog.RabbitMq.Client.Common.Helper
{
    public class LiveHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return Clients.Client(Context.ConnectionId).SendAsync("SetConnectionId", Context.ConnectionId);
        }

        public async Task<string> ConnectGroup(string customerName, string connectionId)
        {
            await Groups.AddToGroupAsync(connectionId, customerName);
            return $"{connectionId} Added {customerName}";
        }

        public Task PushNotify(Customer customer)
        {
            return Clients.Group(customer.Name).SendAsync("ChangeCustomerData", customer);
        }
    }
}
