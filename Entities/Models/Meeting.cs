using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Meeting
    {
        public Meeting()
        {

            MeetingSpeaker = new HashSet<MeetingSpeaker>();
            MeetingTicket = new HashSet<MeetingTicket>();
        }

        public long MeetingId { get; set; }
        public string MeetingTitle { get; set; }
        public long? MeetingDate { get; set; }
        public string MeetingPlace { get; set; }
        public bool? IsActive { get; set; }
        public long? CreateDate { get; set; }


        public virtual ICollection<MeetingSpeaker> MeetingSpeaker { get; set; }
        public virtual ICollection<MeetingTicket> MeetingTicket { get; set; }
    }
}
