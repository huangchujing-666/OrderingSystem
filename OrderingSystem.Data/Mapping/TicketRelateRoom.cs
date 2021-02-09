using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class TicketRelateRoomMap : EntityTypeConfiguration<TicketRelateRoom>
    {

        public TicketRelateRoomMap()
        {
            this.ToTable("TicketRelateRoom");
            this.HasKey(m => m.TicketRelateRoomId);
            this.Property(m => m.TicketRelateRoomId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
            this.Property(m => m.TicketId);
            this.Property(m => m.RoomId); 
            this.Property(m => m.BusinessInfoId); 
            this.Property(m => m.Count); 


            HasRequired(t => t.Room);
            HasRequired(t => t.Ticket);
            HasRequired(t => t.BusinessInfo);
        }

    }
}

