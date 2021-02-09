using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class DishesSpecDetailMap: EntityTypeConfiguration<DishesSpecDetail>
    {

        public DishesSpecDetailMap()
        {
            this.ToTable("DishesSpecDetail");
            this.HasKey(m => m.DishesSpecDetailId);
            this.Property(m => m.DishesSpecDetailId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.DishesSpecId);
            this.Property(m => m.Descript);
            this.Property(m => m.OrignPrice);
            this.Property(m => m.RealPrice);
            HasRequired(m => m.DishesSpec);

        }

    }
}

