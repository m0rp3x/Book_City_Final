using Microsoft.AspNetCore.SignalR;
using WebApplication2.Models;

namespace WebApplication2.Support;

public class ChatHub : Hub
{
    private static List<string> _users = new List<string>();
    private static List<string> _messages = new List<string>();

    public override Task OnConnectedAsync()
    {
        // добавляем пользователя в список
        string userName = Context.User.Identity.Name;
        if (!_users.Contains(userName))
        {
            _users.Add(userName);
            Clients.All.SendAsync("UserList", _users);
        }

        // отправляем историю сообщений новому пользователю
        Clients.Caller.SendAsync("ReceiveHistory", _messages);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        // удаляем пользователя из списка
        string userName = Context.User.Identity.Name;
        _users.Remove(userName);
        Clients.All.SendAsync("UserList", _users);

        return base.OnDisconnectedAsync(exception);
    }

    public async Task Send(string message, string userName)
    {
        // добавляем сообщение в историю и отправляем его всем пользователям
        _messages.Add($"{userName}: {message}");
        await Clients.All.SendAsync("Receive", message, userName);
    }
}
