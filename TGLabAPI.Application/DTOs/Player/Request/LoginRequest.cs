using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgLabApi.Application.DTOs.Player.Request
{
    public record LoginRequest(
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        string Email,

        [Required(ErrorMessage = "A senha é obrigatória.")]
        string Password
    );
}
