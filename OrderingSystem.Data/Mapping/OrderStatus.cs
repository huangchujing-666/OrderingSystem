using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class OrderStatusMap: EntityTypeConfiguration<OrderStatus>
    {

        public OrderStatusMap()
        {
            this.ToTable("OrderStatus");
            this.HasKey(m => m.OrderStatusId);
            this.Property(m => m.OrderStatusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
        }

    }
}

