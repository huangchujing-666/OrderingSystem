using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class DishesLableMap: EntityTypeConfiguration<DishesLable>
    {

        public DishesLableMap()
        {
            this.ToTable("DishesLable");
            this.HasKey(m => m.DishesLableId);
            this.Property(m => m.DishesLableId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.DishesId);
        }

    }
}

