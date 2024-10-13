using TgLabApi.Domain.Entities.Player;

namespace TGLabAPI.Tests.Domain
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void CreatePlayer_WithValidData_ShouldInitializeCorrectly()
        {
            var name = "test";
            var email = "test@example.com";
            var password = "Password123!";

            var player = new PlayerEntity(name, email, password);

            Assert.That(player.Name, Is.EqualTo(name));
            Assert.That(player.Email, Is.EqualTo(email));
            Assert.That(player.Password, Is.EqualTo(password));
        }
    }
}
