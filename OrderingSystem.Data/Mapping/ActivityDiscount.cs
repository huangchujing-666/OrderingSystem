using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class ActivityDiscountMap : EntityTypeConfiguration<ActivityDiscount>
    {

        public ActivityDiscountMap()
        {
            this.ToTable("ActivityDiscount");
            this.HasKey(m => m.ActivityDiscountId);
            this.Property(m => m.ActivityDiscountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //this.Property(m => m.BusinessInfoId);
            this.Property(m => m.Discount);
        }
	}
}

