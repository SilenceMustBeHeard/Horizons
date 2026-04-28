using Horizons.Data.Models.Messages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Config.Messages;

public class SystemInboxMessageConfig : BaseMessageConfig<SystemInboxMessage>
{
    public override void Configure(EntityTypeBuilder<SystemInboxMessage> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedSystemMessages)
            .HasForeignKey(m => m.ReceiverId);

        builder
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentSystemMessages)
            .HasForeignKey(m => m.SenderId);
    }
}