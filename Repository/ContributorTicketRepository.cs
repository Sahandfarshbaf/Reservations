using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;
using Entities;
using Entities.Models;

namespace Repository
{
  public  class ContributorTicketRepository : RepositoryBase<ContributorTicket>, IContributorTicketRepository
    {
        public ContributorTicketRepository(RepositoryContext repositoryContext)
          : base(repositoryContext)
      {
      }
    }
}
