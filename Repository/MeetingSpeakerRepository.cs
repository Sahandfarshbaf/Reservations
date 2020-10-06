using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;
using Entities;
using Entities.Models;

namespace Repository
{
   public class MeetingSpeakerRepository : RepositoryBase<MeetingSpeaker>, IMeetingSpeakerRepository
    {
       public MeetingSpeakerRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
       {
       }
    }
}
