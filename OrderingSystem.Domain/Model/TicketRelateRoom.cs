using System;
namespace OrderingSystem.Domain.Model
{
    public class TicketRelateRoom : IAggregateRoot
    {
        public virtual int TicketRelateRoomId { get; set; }
        public virtual int TicketId { get; set; }
        public virtual int BusinessInfoId { get; set; }
        public virtual int RoomId { get; set; }

        public virtual int Count { get; set; }

        public virtual Ticket Ticket { get;set;}
        public virtual Room Room { get;set; }
        public virtual BusinessInfo BusinessInfo { get;set; }

    }
}

