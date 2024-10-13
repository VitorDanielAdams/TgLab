
using System.Net.WebSockets;
using TGLabAPI.Application.DTOs.Auth;

namespace TGLabAPI.Infrastructure.WebSockets
{
    public class WebSocketConnectionManager
    {
        private readonly Dictionary<Guid, WebSocket> _sockets = new();
        private readonly UserContext _userContext;

        public WebSocketConnectionManager(UserContext userContext)
        {
            _userContext = userContext;
        }

        public WebSocket AddSocket(WebSocket socket)
        {
            var connectionId = _userContext.Id;
            _sockets.Add(connectionId, socket);
            return socket;
        }

        public WebSocket GetSocketById(Guid id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public Dictionary<Guid, WebSocket> GetAllSockets()
        {
            return _sockets;
        }

        public async Task RemoveSocket(Guid id)
        {
            if (_sockets.ContainsKey(id))
            {
                var socket = _sockets[id];
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by the WebSocketManager", CancellationToken.None);
                _sockets.Remove(id);
            }
        }
    }
}
