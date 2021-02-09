using System;
namespace OrderingSystem.Domain.Model
{
    public class TicketRelateTicket : IAggregateRoot
    {
        public virtual int TicketRelateTicketId { get; set; }
        public virtual int RelateTicketId { get; set; }
        public virtual int TicketId { get; set; }

        public virtual int Count { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}

