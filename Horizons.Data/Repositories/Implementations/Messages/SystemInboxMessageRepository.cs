using Horizons.Data.Models.Messages;
using Horizons.Data.Repositories.Implementations.Base;
using Horizons.Data.Repositories.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Repositories.Implementations.Messages;

public class SystemInboxMessageRepository : RepositoryAsync<SystemInboxMessage, Guid>, ISystemInboxMessageRepository
{
    public SystemInboxMessageRepository(AppDbContext context) : base(context)
    {
    }



}