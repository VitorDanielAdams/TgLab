using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgLabApi.Application.DTOs.Player.Request
{
    public record LoginRequest(string Email, string Password);
}
