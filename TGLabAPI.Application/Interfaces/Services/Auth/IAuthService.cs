using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Domain.Entities.Player;

namespace TGLabAPI.Application.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<string?> Authenticate(string email, string password);
    }
}