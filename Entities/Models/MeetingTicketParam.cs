using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class MeetingTicketParam
    {
        public long MeetingTicketParamId { get; set; }
        public long? MeetingTicketId { get; set; }
        public string Description { get; set; }

        public virtual MeetingTicket MeetingTicket { get; set; }
    }
}
