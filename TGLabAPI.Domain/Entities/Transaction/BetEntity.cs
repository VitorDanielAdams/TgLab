﻿using System;
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
    public class BetEntity : BaseEntity
    {
        public BetEntity(Guid playerId, double value, BetStatus status, double? valueReward = null)
        {
            PlayerId = playerId;
            Value = value;
            Status = status;
            ValueReward = valueReward;
            IsCanceled = status == BetStatus.Canceled;
        }

        [ForeignKey("PlayerId")]
        public PlayerEntity PlayerEntity { get; set; }
        public Guid PlayerId { get; set; }
        public double Value { get; set; }
        public BetStatus Status { get; set; }
        public double? ValueReward { get; set; }
        public bool IsCanceled { get; set; }

        public void Cancel()
        {
            if (IsCanceled && Status == BetStatus.Canceled) return;
            Status = BetStatus.Canceled;
            IsCanceled = true;
        }

    }
}
