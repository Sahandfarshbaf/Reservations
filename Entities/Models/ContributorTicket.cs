using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ContributorTicket
    {
        public ContributorTicket()
        {
            ContributorPayment = new HashSet<ContributorPayment>();
        }

        public long ContributorTicketId { get; set; }
        public long? ContributorId { get; set; }
        public long? MeetingTicketId { get; set; }

        public virtual Contributor Contributor { get; set; }
        public virtual MeetingTicket MeetingTicket { get; set; }
        public virtual ICollection<ContributorPayment> ContributorPayment { get; set; }
    }
}
