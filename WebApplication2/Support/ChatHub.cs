using Microsoft.AspNetCore.SignalR;
using WebApplication2.Models;

namespace WebApplication2.Support;

public class ChatHub : Hub
{
    HubCallerContext db;
    public async Task Send(string message, string username)
    {
        db = Context;
        await this.Clients.All.SendAsync("Receive", message, username = db.User?.Identity.Name);
    }
    
}