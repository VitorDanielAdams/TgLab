using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Domain.Entities.Player;

namespace TGLabAPI.Tests.Domain
{
    [TestFixture]
    public class WalletTests
    {
        [Test]
        public void UpdateBalance_WithPositiveAmount_ShouldIncreaseBalance()
        {
            Guid playerId = Guid.NewGuid();
            var amount = 100;
            var coin = "BRL";

            var wallet = new WalletEntity(playerId, amount, coin);

            wallet.UpdateAmount(amount);

            Assert.That(wallet.Amount, Is.EqualTo(amount));
        }

        [Test]
        public void DecrementBalance_WithSufficientBalance_ShouldDecreaseBalance()
        {
            Guid playerId = Guid.NewGuid();
            var amount = 100;
            var amountToDecrement = 50;
            var amountExpected = 50;
            var coin = "BRL";

            var wallet = new WalletEntity(playerId, amount, coin);

            var value = amount - amountToDecrement;
            wallet.UpdateAmount(value);

            Assert.That(wallet.Amount, Is.EqualTo(amountExpected));
        }

        [Test]
        public void DecrementBalance_WithInsufficientBalance_ShouldThrowInvalidOperationException()
        {
            Guid playerId = Guid.NewGuid();
            var amount = -1;
            var coin = "BRL";

            var wallet = new WalletEntity(playerId, amount, coin);

            var ex = Assert.Throws<InvalidOperationException>(() => wallet.UpdateAmount(amount));
            Assert.That(ex.Message, Is.EqualTo("Saldo insuficiente."));
        }

        [Test]
        public void IsSufficient_WithSufficientBalance_ShouldReturnTrue()
        {
            var playerId = Guid.NewGuid();
            var initialAmount = 100.0;
            var amountToCheck = 50.0;
            var coin = "BRL";

            var wallet = new WalletEntity(playerId, initialAmount, coin);

            var result = wallet.IsSufficient(amountToCheck);

            Assert.IsTrue(result);
        }
    }
}
