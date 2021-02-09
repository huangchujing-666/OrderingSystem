using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BaseImageMap: EntityTypeConfiguration<BaseImage>
    {

        public BaseImageMap()
        {
            this.ToTable("BaseImage");
            this.HasKey(m => m.BaseImageId);
            this.Property(m => m.BaseImageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Source);
            this.Property(m => m.Path);
            this.Property(m => m.Title);
            this.Property(m => m.CreateTime);
        }

    }
}

