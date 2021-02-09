using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class TicketRelateTicketMap : EntityTypeConfiguration<TicketRelateTicket>
    {

        public TicketRelateTicketMap()
        {
            this.ToTable("TicketRelateTicket");
            this.HasKey(m => m.TicketRelateTicketId);
            this.Property(m => m.TicketRelateTicketId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.TicketId);  
            this.Property(m => m.RelateTicketId);
            this.Property(m => m.Count);
            HasRequired(t => t.Ticket);
        }

    }
}

