using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Domain.Entities.Common;
using TgLabApi.Domain.Entities.Player;
using TGLabAPI.Domain.Entities;

namespace TgLabApi.Domain.Entities.Transaction
{
    public class TransactionEntity : BaseEntity
    {
        public TransactionEntity(Guid walletId, Guid betId, double value, TransactionType type)
        {
            WalletId = walletId;
            BetId = betId;
            Value = value;
            Type = type;
        }

        [ForeignKey("WalletId")]
        public WalletEntity WalletEntity { get; set; }
        public Guid WalletId { get; set; }
        [ForeignKey("BetId")]
        public BetEntity BetEntity { get; set; }
        public Guid BetId { get; set; }
        public double Value { get; set; }
        public TransactionType Type { get; set; }
    }
}
