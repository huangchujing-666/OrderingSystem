
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BusinessPayConfigMap: EntityTypeConfiguration<BusinessPayConfig>
    {

        public BusinessPayConfigMap()
        {
            this.ToTable("BusinessPayConfig");
            this.HasKey(m => m.BusinessPayConfigId);
            this.Property(m => m.BusinessPayConfigId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.CreateTime);
            this.Property(m => m.BusinessInfoId);
        }
        
    }
}

