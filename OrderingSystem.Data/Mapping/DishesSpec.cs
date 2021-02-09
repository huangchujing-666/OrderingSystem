using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class DishesSpecMap: EntityTypeConfiguration<DishesSpec>
    {

        public DishesSpecMap()
        {
            this.ToTable("DishesSpec");
            this.HasKey(m => m.DishesSpecId);
            this.Property(m => m.DishesSpecId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.DishesId);


            HasMany(m => m.DishesSpecDetailList).WithRequired(n => n.DishesSpec);
        }
    }
}

