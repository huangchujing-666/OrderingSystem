using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BusinessGroupImageMap: EntityTypeConfiguration<BusinessGroupImage>
    {

        public BusinessGroupImageMap()
        {
            this.ToTable("BusinessGroupImage");
            this.HasKey(m => m.BusinessGroupImageId);
            this.Property(m => m.BusinessGroupImageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BusinessGroupId);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.Type); 
              
            this.Property(m => m.CreateTime); 
            this.Property(m => m.EditTime);
             
            HasRequired(t => t.BaseImage);
            HasRequired(t => t.BusinessGroup);
        }

    }
}

