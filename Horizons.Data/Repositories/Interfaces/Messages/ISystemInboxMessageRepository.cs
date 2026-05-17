using Horizons.Data.Models.Messages;
using Horizons.Data.Repositories.Interfaces.CRUD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Horizons.Data.Repositories.Interfaces.Messages;

 public interface ISystemInboxMessageRepository : IFullRepositoryAsync<SystemInboxMessage, Guid>
 {
       
 }