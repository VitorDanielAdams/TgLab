using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using TGLabAPI.Application.Interfaces.Services.Player;
using TGLabAPI.Infrastructure.WebSockets;

namespace TGLabAPI.Infrastructure.Services
{
    public class WalletUpdateService : IWalletUpdateService
    {
            private readonly WebSocketConnectionManager _connectionManager;

            public WalletUpdateService(WebSocketConnectionManager connectionManager)
            {
                _connectionManager = connectionManager;
            }

            public async Task SendMessage(Guid playerId, double amount)
            {
                var sockets = _connectionManager.GetAllSockets();
                var messageBuffer = Encoding.UTF8.GetBytes(amount.ToString());

                foreach (var socket in sockets)
                {
                    if (socket.Value.State == WebSocketState.Open)
                    {
                        await socket.Value.SendAsync(new ArraySegment<byte>(messageBuffer, 0, messageBuffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
        }
    }
