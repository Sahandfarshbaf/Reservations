using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;
using Entities;
using Entities.Models;

namespace Repository
{
   public class MeetingRepository : RepositoryBase<Meeting>, IMeetingRepository
    {
       public MeetingRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
       {
       }
    }
}
