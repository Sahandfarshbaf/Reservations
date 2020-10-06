using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ContributorPayment
    {
        public long ContributorPaymentId { get; set; }
        public long? ContributorTicketId { get; set; }
        public string TransactionCode { get; set; }
        public long? TransactionDate { get; set; }
        public long? Amount { get; set; }
        public int? RefId { get; set; }
        public int FinalStatusId { get; set; }

        public virtual ContributorTicket ContributorTicket { get; set; }
    }
}
