using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class MeetingTicket
    {
        public MeetingTicket()
        {
            ContributorTicket = new HashSet<ContributorTicket>();
        }

        public long MeetingTicketId { get; set; }
        public long? MeetingId { get; set; }
        public int? Count { get; set; }
        public long? Price { get; set; }

        public virtual Meeting Meeting { get; set; }
        public virtual ICollection<ContributorTicket> ContributorTicket { get; set; }
    }
}
