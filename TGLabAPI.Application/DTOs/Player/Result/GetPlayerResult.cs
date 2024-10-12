using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgLabApi.Application.DTOs.Player.Result
{
    public record GetPlayerResult(string Name, string Email, double Amount);
}
