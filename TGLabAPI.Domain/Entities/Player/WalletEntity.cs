using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Domain.Entities.Common;

namespace TgLabApi.Domain.Entities.Player
{
    public class WalletEntity : BaseEntity
    {
        public WalletEntity(Guid playerId, double amount, string currency)
        {
            PlayerId = playerId;
            Amount = amount;
            Currency = currency;
        }

        [ForeignKey("PlayerId")]
        public PlayerEntity PlayerEntity { get; set; }
        public Guid PlayerId { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }

        public bool IsSufficient(double value)
        {
            return Amount >= value;
        }

        public void UpdateAmount(double value)
        {
            if (value < 0) throw new InvalidOperationException("Saldo insuficiente.");
            Amount = value;
        }
    }
}
