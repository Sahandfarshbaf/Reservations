using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ContributorPaymentRepository : RepositoryBase<ContributorPayment>, IContributorPaymentRepository
    {
        public ContributorPaymentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

    }
}
