using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BusinessBannerImageMap: EntityTypeConfiguration<BusinessBannerImage>
    {

        public BusinessBannerImageMap()
        {
            this.ToTable("BusinessBannerImage");
            this.HasKey(m => m.BusinessBannerImageId);
            this.Property(m => m.BusinessBannerImageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.Descript);
            this.Property(m => m.CreateTime);
            this.Property(m => m.SortNo);
            this.Property(m=>m.Module);
            HasRequired(t => t.BaseImage);
        }

    }
}

