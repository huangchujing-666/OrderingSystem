using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BusinessImageMap: EntityTypeConfiguration<BusinessImage>
    {

        public BusinessImageMap()
        {
            this.ToTable("BusinessImage");
            this.HasKey(m => m.BusinessImageId);
            this.Property(m => m.BusinessImageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.Type);
            this.Property(m=>m.SortNo);

            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);

            this.Property(m => m.CreatePersonId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditPersonId);
            this.Property(m => m.EditTime);


            HasRequired(t => t.BaseImage);
            HasRequired(t => t.BusinessInfo);
        }

    }
}

