using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class DishesRelateLableMap: EntityTypeConfiguration<DishesRelateLable>
    {

        public DishesRelateLableMap()
        {
            this.ToTable("DishesRelateLable");
            this.HasKey(m => m.DishesRelateLableId);
            this.Property(m => m.DishesRelateLableId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.DishesId);
            this.Property(m => m.DishesLableId);
             
            HasRequired(t => t.DishesLable);
        }

    }
}

