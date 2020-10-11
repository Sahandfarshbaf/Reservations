using System;
using System.Collections.Generic;
using System.Text;
using Contarcts;


namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IContributorRepository Contributor { get; }
        IContributorPaymentRepository ContributorPayment { get; }
        IContributorTicketRepository ContributorTicket { get; }
        IMeetingRepository Meeting { get; }
        IMeetingTicketRepository MeetingTicket { get; }
        IMeetingSpeakerRepository MeetingSpeaker { get; }
        ISpeakerRepository Speaker { get; }
        IMeetingTicketParamRepository MeetingTicketParam { get; }

        void Save();
    }
}