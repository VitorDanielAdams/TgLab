using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Application.DTOs.Player.Request;
using TgLabApi.Application.DTOs.Player.Result;

namespace TGLabAPI.Application.Interfaces.Services.Player
{
    public interface IPlayerService
    {
        Task<GetPlayerResult> CreatePlayer(CreatePlayerRequest newPlayer);
    }
}
