
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class OrderDetailMap: EntityTypeConfiguration<OrderDetail>
    {

        public OrderDetailMap()
        {
            this.ToTable("OrderDetail");
            this.HasKey(m => m.OrderDetailId);
            this.Property(m => m.OrderDetailId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.OrderNo);
            this.Property(m => m.OrderId);
            this.Property(m => m.DishesId);
            this.Property(m => m.ProductId);
            this.Property(m => m.OrignAmount);
            this.Property(m => m.RealAmount);
            this.Property(m => m.OrderTime);
            this.Property(m => m.Count);
            this.Property(m => m.DishesSpecDetailIds);
            this.Property(m => m.RoomId);
            this.Property(m => m.TicketId);
            this.Property(m => m.CustomerName);
            this.Property(m => m.PhoneNo);
            HasRequired(t => t.Order);
            HasRequired(t => t.Ticket);
            HasRequired(t => t.Dishes);
            HasRequired(t => t.Room);
            HasRequired(t => t.Product);
        }
         
    }
}

