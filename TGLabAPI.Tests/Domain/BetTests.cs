
using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Domain.Entities;

namespace TGLabAPI.Tests.Domain
{
    [TestFixture]
    public class BetEntityTests
    {
        [Test]
        public void CreateBet_WithValidData_ShouldInitializeCorrectly()
        {
            var playerId = Guid.NewGuid();
            var value = 100.0;
            var color = Color.Red;

            var bet = new BetEntity(playerId, value, BetStatus.Pending, color);

            Assert.That(bet.PlayerId, Is.EqualTo(playerId));
            Assert.That(bet.Value, Is.EqualTo(value));
            Assert.That(bet.Status, Is.EqualTo(BetStatus.Pending));
            Assert.That(bet.Color, Is.EqualTo(color));
            Assert.IsFalse(bet.IsCanceled);
            Assert.IsNull(bet.ValueReward);
        }

        [Test]
        public void CancelBet_WhenBetIsNotCanceled_ShouldChangeStatusToCanceled()
        {
            var bet = new BetEntity(Guid.NewGuid(), 100.0, BetStatus.Pending, Color.Black);

            bet.Cancel();

            Assert.IsTrue(bet.IsCanceled);
            Assert.That(bet.Status, Is.EqualTo(BetStatus.Canceled));
        }

        [Test]
        public void CancelBet_WhenBetIsAlreadyCanceled_ShouldNotChangeStatusAgain()
        {
            var bet = new BetEntity(Guid.NewGuid(), 100.0, BetStatus.Canceled, Color.Black);

            bet.Cancel();

            Assert.IsTrue(bet.IsCanceled);
            Assert.That(bet.Status, Is.EqualTo(BetStatus.Canceled));
        }

        [Test]
        public void WinBet_ShouldSetStatusToWinAndCalculateReward()
        {
            var bet = new BetEntity(Guid.NewGuid(), 50.0, BetStatus.Pending, Color.Red);

            bet.Win();

            Assert.That(bet.Status, Is.EqualTo(BetStatus.Win));
            Assert.That(bet.ValueReward, Is.EqualTo(100.0));
        }

        [Test]
        public void LoseBet_ShouldSetStatusToLose()
        {
            var bet = new BetEntity(Guid.NewGuid(), 50.0, BetStatus.Pending, Color.Red);

            bet.Lose();

            Assert.That(bet.Status, Is.EqualTo(BetStatus.Lose));
            Assert.IsNull(bet.ValueReward);
        }
    }
}
