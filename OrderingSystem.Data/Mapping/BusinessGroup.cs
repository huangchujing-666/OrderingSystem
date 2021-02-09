using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class BusinessGroupMap : EntityTypeConfiguration<BusinessGroup>
    {

        public BusinessGroupMap()
        {
            this.ToTable("BusinessGroup");
            this.HasKey(m => m.BusinessGroupId);
            this.Property(m => m.BusinessGroupId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BaseImageId); 
            this.Property(m => m.Sort);
            this.Property(m => m.BusinessTypeId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime); 
            HasRequired(t => t.BaseImage);
            HasMany(m => m.BusinessGroupImageList).WithRequired(n => n.BusinessGroup);
        }

    }
}

