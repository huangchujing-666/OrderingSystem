
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public  class ActivityMinusMap: EntityTypeConfiguration<ActivityMinus>
    {

        public ActivityMinusMap()
        {
            this.ToTable("ActivityMinus");
            this.HasKey(m => m.ActivityMinusId);
            this.Property(m => m.ActivityMinusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.AchiveAmount);
            this.Property(m => m.MinusAmount);
        }

	}
}

