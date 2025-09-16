using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Concurrent;

// This class defines the server-side hub for real-time communication.
public class ChatHub : Hub
{
    // A thread-safe dictionary to map a user's ID to their connection ID.
    private static ConcurrentDictionary<string, string> connectedUsers = new ConcurrentDictionary<string, string>();

    // This method is called by a client when they first connect.
    public async Task JoinChat(string userId)
    {
        // Add the user to our dictionary.
        connectedUsers.AddOrUpdate(userId, Context.ConnectionId, (key, oldValue) => Context.ConnectionId);

        // Notify the user that they are connected.
        await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "System", $"You are now connected, {userId}.");
    }

    // This method is called by a client to send a message to a specific user.
    public async Task SendMessage(string senderId, string recipientId, string message)
    {
        // Look up the recipient's connection ID from our dictionary.
        if (connectedUsers.TryGetValue(recipientId, out string recipientConnectionId))
        {
            // Send the message to the specific recipient.
            await Clients.Client(recipientConnectionId).SendAsync("ReceiveMessage", senderId, message);
        }
    }

    // This method is called when a client disconnects.
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // Remove the user from our dictionary when they disconnect.
        var user = connectedUsers.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
        if (user != null)
        {
            connectedUsers.TryRemove(user, out _);
        }
        await base.OnDisconnectedAsync(exception);
    }
}
