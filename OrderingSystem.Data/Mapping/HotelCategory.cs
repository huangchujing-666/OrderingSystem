using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class HotelCategoryMap: EntityTypeConfiguration<HotelCategory>
    {

        public HotelCategoryMap()
        {
            this.ToTable("HotelCategory");
            this.HasKey(m => m.HotelCategoryId);
            this.Property(m => m.HotelCategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name); 
            this.Property(m => m.CreateTime);
        }

    }
}

