using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class SpeakerRepository : RepositoryBase<Speaker>, ISpeakerRepository
    {
        public SpeakerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
