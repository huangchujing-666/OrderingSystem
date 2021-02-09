using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class OrderStatusLogMap: EntityTypeConfiguration<OrderStatusLog>
    {

        public OrderStatusLogMap()
        {
            this.ToTable("OrderStatusLog");
            this.HasKey(m => m.OrderStatusLogId);
            this.Property(m => m.OrderStatusLogId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.CreateTime);
            this.Property(m => m.OrderId);
            this.Property(m => m.Status);
            this.Property(m => m.StatusName); 
        }

    }
}

