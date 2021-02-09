using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class DishesCategoryMap: EntityTypeConfiguration<DishesCategory>
    {

        public DishesCategoryMap()
        {
            this.ToTable("DishesCategory");
            this.HasKey(m => m.DishesCategoryId);
            this.Property(m => m.DishesCategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.CreateTime);
        }

    }
}

