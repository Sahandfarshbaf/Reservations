using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;
using Entities;
using Entities.Models;

namespace Repository
{
   public class ContributorRepository : RepositoryBase<Contributor>, IContributorRepository
    {
       public ContributorRepository(RepositoryContext repositoryContext)
           : base(repositoryContext)
       {
       }
    }
}
