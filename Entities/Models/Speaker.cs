using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Speaker
    {
        public Speaker()
        {
            MeetingSpeaker = new HashSet<MeetingSpeaker>();
        }

        public long SpeakerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MeetingSpeaker> MeetingSpeaker { get; set; }
    }
}
