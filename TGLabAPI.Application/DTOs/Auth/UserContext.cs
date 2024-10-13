
namespace TGLabAPI.Application.DTOs.Auth
{
    public class UserContext
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public void Clean()
        {
            Id = Guid.Empty;
            Email = null;
        }
    }
}
