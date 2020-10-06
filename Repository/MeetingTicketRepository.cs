using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;
using Entities;
using Entities.Models;

namespace Repository
{
   public class MeetingTicketRepository : RepositoryBase<MeetingTicket>, IMeetingTicketRepository
    {
       public MeetingTicketRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
       {
       }
    }
}
