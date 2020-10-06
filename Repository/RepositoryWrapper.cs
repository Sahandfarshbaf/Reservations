using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IContributorRepository _contributor;
        private IContributorPaymentRepository _contributorPayment;
        private IContributorTicketRepository _contributorTicket;
        private IMeetingRepository _meeting;
        private IMeetingTicketRepository _meetingTicket;
        private IMeetingSpeakerRepository _meetingSpeaker;
        private ISpeakerRepository _speaker;


        public IContributorRepository Contributor => _contributor ??= new ContributorRepository(_repoContext);
        public IContributorPaymentRepository ContributorPayment => _contributorPayment ??= new ContributorPaymentRepository(_repoContext);
        public IContributorTicketRepository ContributorTicket => _contributorTicket ??= new ContributorTicketRepository(_repoContext);
        public IMeetingRepository Meeting => _meeting ??= new MeetingRepository(_repoContext);
        public IMeetingTicketRepository MeetingTicket => _meetingTicket ??= new MeetingTicketRepository(_repoContext);
        public IMeetingSpeakerRepository MeetingSpeaker => _meetingSpeaker ??= new MeetingSpeakerRepository(_repoContext);
        public ISpeakerRepository Speaker => _speaker ??= new SpeakerRepository(_repoContext);



        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }



        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
