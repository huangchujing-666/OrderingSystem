using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class DishesRelateSpecMap: EntityTypeConfiguration<DishesRelateSpec>
    {
        public DishesRelateSpecMap()
        {
            this.ToTable("DishesRelateSpec");
            this.HasKey(m => m.DishesRelateSpecId);
            this.Property(m => m.DishesRelateSpecId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.DishesId);
            this.Property(m => m.DishesSpecId);

            HasRequired(t => t.DishesSpec);
        }

    }
}

