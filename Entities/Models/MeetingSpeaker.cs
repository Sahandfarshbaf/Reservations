using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class MeetingSpeaker
    {
        public long MeetingSpeakerId { get; set; }
        public long? MeetingId { get; set; }
        public long? SpeakerId { get; set; }

        public virtual Meeting Meeting { get; set; }
        public virtual Speaker Speaker { get; set; }
    }
}
