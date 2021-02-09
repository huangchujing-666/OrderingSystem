using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class TicketMap : EntityTypeConfiguration<Ticket>
    {

        public TicketMap()
        {
            this.ToTable("Ticket");
            this.HasKey(m => m.TicketId);
            this.Property(m => m.TicketId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.OrignPrice);
            this.Property(m => m.RealPrice);
            //this.Property(m => m.RelatedTicketId);
            //this.Property(m => m.RelatedTicketHotelId);
            //this.Property(m => m.RelatedTicketRoomId);
            this.Property(m => m.Special);
            this.Property(m => m.Remark);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.Notice);
            this.Property(m => m.UseCount);
            this.Property(m => m.Rules);
            this.Property(m => m.BindCard);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);

            HasMany(m => m.TicketRelateRoom).WithRequired(n => n.Ticket);
            HasMany(m => m.TicketRelateTicket).WithRequired(n => n.Ticket);
            //HasRequired(t => t.TicketRelateRoom);
            //HasMany(m => m.TicketRelateRoom).WithRequired(n => n.Ticket);
        }

    }
}

