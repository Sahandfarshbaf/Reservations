using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Contributor
    {
        public Contributor()
        {
            ContributorTicket = new HashSet<ContributorTicket>();
        }

        public long ContributorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ContributorTicket> ContributorTicket { get; set; }
    }
}
