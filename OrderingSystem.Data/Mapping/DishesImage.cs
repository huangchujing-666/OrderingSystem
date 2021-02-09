using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class DishesImageMap: EntityTypeConfiguration<DishesImage>
    {

        public DishesImageMap()
        {
            this.ToTable("DishesImage");
            this.HasKey(m => m.DishesImageId);
            this.Property(m => m.DishesImageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.DishesId);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.Type);

            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);

            this.Property(m => m.CreatePersonId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditPersonId);
            this.Property(m => m.EditTime);


            HasRequired(t => t.BaseImage);
            HasRequired(t => t.Dishes);
        }
    }
}

