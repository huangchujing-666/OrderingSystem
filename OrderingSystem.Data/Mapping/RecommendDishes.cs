using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class RecommendDishesMap: EntityTypeConfiguration<RecommendDishes>
    {

        public RecommendDishesMap()
        {
            this.ToTable("RecommendDishes");
            this.HasKey(m => m.RecommendDishesId);
            this.Property(m => m.RecommendDishesId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BusinessCommentId);
            this.Property(m => m.DishesId);
        }

    }
}

