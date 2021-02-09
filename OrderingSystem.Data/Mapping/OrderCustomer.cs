using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class OrderCustomerMap: EntityTypeConfiguration<OrderCustomer>
    {

        public OrderCustomerMap()
        {
            this.ToTable("OrderCustomer");
            this.HasKey(m => m.OrderCustomerId);
            this.Property(m => m.OrderCustomerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.CustomerId);
            this.Property(m => m.OrderId);
          
             
            HasRequired(t => t.Order);
            HasRequired(t => t.Customer);
        }

    }
}

