using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class HotelRelateCategoryMap : EntityTypeConfiguration<HotelRelateCategory>
    {

        public HotelRelateCategoryMap()
        {
            this.ToTable("HotelRelateCategory");
            this.HasKey(m => m.HotelRelateCategoryId);
            this.Property(m => m.HotelRelateCategoryId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BusinessInfoId); 
            this.Property(m => m.HotelCategoryId);
            this.Property(m => m.HotelRelateCategoryId);

            HasRequired(t => t.BusinessInfo);
            HasRequired(t => t.HotelCategory);
        }

    }
}

