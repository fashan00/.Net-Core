using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using MyMVC.Hubs;

namespace MyWebsite.Hubs {
    // HubName 一定要小寫開頭。
    // 如果沒有指定 HubName，第一個字元會被自動轉為小寫。例：ChatHub => chatHub
    [HubName ("chathub")]
    public class ChatHub : Hub<IChatHub> {
        private string Now => DateTime.Now.ToString ("HH:mm:ss");

        public override async Task OnConnected () {
            await Clients.All.ServerMessage ($"[{Now}] {Context.ConnectionId} joined");
        }

        public override async Task OnDisconnected (bool stopCalled) {
            await Clients.All.ServerMessage ($"[{Now}] {Context.ConnectionId} left");
        }

        // 這個方法是用來接收 Client 發出的 Message
        public Task ClientMessage (dynamic data) {
            string name = data.name.Value;
            string message = data.message.Value;

            return Clients.All.ServerMessage ($"[{Now}] {name}: {message}");
        }
    }
}