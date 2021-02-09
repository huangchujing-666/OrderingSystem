using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class CustomerMap: EntityTypeConfiguration<Customer>
    {

        public CustomerMap()
        {
            this.ToTable("Customer");
            this.HasKey(m => m.CustomerId);
            this.Property(m => m.CustomerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.CardNo);
            this.Property(m => m.CardType);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.Mobile);
            this.Property(m => m.Name); 
        }

    }
}

