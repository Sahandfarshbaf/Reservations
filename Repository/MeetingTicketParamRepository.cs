using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;
using Entities;
using Entities.Models;

namespace Repository
{
   public class MeetingTicketParamRepository : RepositoryBase<MeetingTicketParam>, IMeetingTicketParamRepository
    {
       public MeetingTicketParamRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
       {
       }
    }
}
