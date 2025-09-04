// This file defines the SignalR hub for our chat application.
// A hub is the main class that a client connects to.

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

// Inherit from the Hub base class to create a new hub.
// The hub is used to handle real-time communication.
public class ChatHub : Hub
{
    /// <summary>
    /// Sends a message to all connected clients.
    /// This method can be called from a client.
    /// </summary>
    /// <param name="user">The name of the user sending the message.</param>
    /// <param name="message">The message content.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendMessage(string user, string message)
    {
        // The 'Clients.All' property targets all connected clients.
        // The 'SendAsync' method invokes a method on the client side.
        // The first parameter, "ReceiveMessage", is the name of the client-side function to call.
        // The following parameters are the arguments for that function.
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}