using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGLabAPI.Domain.Entities
{
    public enum BetStatus
    {
        Pending = 0,
        Win = 1,
        Lose = 2,
        Canceled = 3,
    }

    public enum TransactionType
    {
        Bet = 0,
        Reward = 1,
        Cancelled = 2,
        Refund = 3,
        Bonus = 4,
    }

    public enum Color
    {
        Green = 0,
        Black = 1,
        Red = 2
    }
}
