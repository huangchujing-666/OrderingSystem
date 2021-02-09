using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class DishesMap: EntityTypeConfiguration<Dishes>
    {
        public DishesMap()
        {
            this.ToTable("Dishes");
            this.HasKey(m => m.DishesId);
            this.Property(m => m.DishesId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.Descript);
            this.Property(m => m.OrignPrice);
            this.Property(m => m.RealPrice);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.DishesCategoryId);
            this.Property(m => m.SellCountPerMonth);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);

            this.Property(m => m.CreatePersonId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditPersonId);
            this.Property(m => m.EditTime);

            HasRequired(t => t.BaseImage);
            HasRequired(t => t.DishesCategory);

            HasMany(m => m.DishesRelateLableList).WithRequired(n => n.Dishes);
            HasMany(m => m.DishesRelateSpecList).WithRequired(n => n.Dishes);

        }
    }
}

