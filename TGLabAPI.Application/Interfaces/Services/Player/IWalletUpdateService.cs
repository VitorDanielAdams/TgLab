using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGLabAPI.Application.Interfaces.Services.Player
{
    public interface IWalletUpdateService
    {
        Task SendMessage(Guid id, double balance);
    }
}
