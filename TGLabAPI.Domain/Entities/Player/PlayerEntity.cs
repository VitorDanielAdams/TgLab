using TgLabApi.Domain.Entities.Common;
 
namespace TgLabApi.Domain.Entities.Player
{
    public class PlayerEntity : BaseEntity
    {
        public PlayerEntity(string name, string email, string password, int loses)
        {
            Name = name;
            Email = email;
            Password = password;
            Loses = loses;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Loses { get; set; }
    }
}
