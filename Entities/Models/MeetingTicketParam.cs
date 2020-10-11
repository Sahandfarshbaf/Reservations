using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public partial class MeetingTicketParam
    {
        public long MeetingTicketParamId { get; set; }
        public long? MeetingTicketId { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual MeetingTicket MeetingTicket { get; set; }
    }
}
