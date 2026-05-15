using Horizons.Data.Models.Messages;
using Horizons.Data.Repositories.Implementations.Base;
using Horizons.Data.Repositories.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Repositories.Implementations.Messages;

public class ContactMessageRepository : RepositoryAsync<ContactMessage, Guid>, IContactMessageRepository
{
    public ContactMessageRepository(AppDbContext context) : base(context)
    {
    }


}
