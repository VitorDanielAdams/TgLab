using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Domain.Entities;

namespace TGLabAPI.Tests.Domain
{
    [TestFixture]
    public class TransactionEntityTests
    {
        [Test]
        public void CreateTransaction_WithValidData_ShouldInitializeCorrectly()
        {
            var walletId = Guid.NewGuid();
            var betId = Guid.NewGuid();
            var value = 150.0;
            var transactionType = TransactionType.Bet;

            var transaction = new TransactionEntity(walletId, betId, value, transactionType);

            Assert.That(transaction.WalletId, Is.EqualTo(walletId));
            Assert.That(transaction.BetId, Is.EqualTo(betId));
            Assert.That(transaction.Value, Is.EqualTo(value));
            Assert.That(transaction.Type, Is.EqualTo(transactionType));
        }
    }
}
